Imports System.IO
Imports System.Net
Imports System.Text
Imports Newtonsoft.Json
Imports RestSharp
Imports System
Imports System.Web.Script.Serialization
Imports System.ComponentModel
Imports System.Data.OracleClient

Public Class frmPaykiiIntegrator


    Private dt As DataTable

    Sub Log(pData As String, Optional isError As Boolean = False)
        txtLog.Text += pData + vbNewLine
        'File.WriteAllText(Application.StartupPath + "\LogFiles\" + "Log_" + Format(Now, "ddMMyyyy.txt", pData)
        WriteToLogFile(pData, isError)
    End Sub

    Private Sub btnClear_Click_1(sender As Object, e As EventArgs) Handles btnClear.Click
        txtLog.Text = ""
    End Sub

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            WriteToLogFile("Started")

            txtUserName.Text = My.Settings.x_api_key
            txtPassword.Text = My.Settings.token
            txtURL.Text = My.Settings.URL

            If My.Application.CommandLineArgs.Count > 0 Then

                AutoExecuteAll()
                EndApplication() 'End Application

            End If
        Catch ex As Exception
            WriteToLogFile("_Load Ex: " & ex.Message, True)
            EndApplication() 'End Application
        End Try

    End Sub

    Function CheckApiRunConditions(dt As DataTable, fieldName As String, rowIndex As Int16) As Boolean

        If (dt.Rows(rowIndex)("API_NAME").Equals(fieldName)) = False Then
            WriteToLogFile(String.Format("Cannot Run. Api Name must be {0} at index {1}", fieldName, rowIndex))
            Return False
        End If

        If (dt.Rows(rowIndex)("STATUS").Equals("1")) = False Then
            WriteToLogFile(String.Format("Cannot Run. Status is 0 for API {0}", dt.Rows(rowIndex)("API_NAME")))
            Return False
        End If

        If DateDiff(DateInterval.Minute, CDate(dt.Rows(rowIndex)("LAST_RUN_TIME")), CDate(dt.Rows(rowIndex)("CURRENT_TIME"))) <
             CInt(dt.Rows(rowIndex)("FREQUENCY_IN_MINUTES")) Then
            WriteToLogFile(String.Format("Cannot Run. Last Run Time is {0} for {1}. Frequency in Minutes is {2}", dt.Rows(rowIndex)("LAST_RUN_TIME"), dt.Rows(rowIndex)("API_Name"), dt.Rows(rowIndex)("FREQUENCY_IN_MINUTES")))
            Return False
        End If

        Return True

    End Function

    Function AutoExecuteAll() As Boolean

        Dim dt As New DataTable
        dt = ReturnDataTable("Select SLNO, API_NAME, STATUS, FREQUENCY_IN_MINUTES, " &
                             " nvl(LAST_RUN_TIME,to_char(sysdate-1,'" & oracleDateFormat & "')) as  LAST_RUN_TIME, " &
                             " to_char(sysdate,'" & oracleDateFormat & "') as  CURRENT_TIME, " &
                             " LAST_SUCCESFUL_RUN_TIME, ERROR_MSG " &
                             " from PAYKII_SCHEDULER_CONFIG order by SLNO")

        If CheckApiRunConditions(dt, "PaykiiSKUCatalogUpdate", 0) Then PaykiiSKUCatalogUpdate(dt.Rows(0)("CURRENT_TIME"))
        If CheckApiRunConditions(dt, "PaykiiBillerCatalogUpdate", 1) Then PaykiiBillerCatalogUpdate(dt.Rows(1)("CURRENT_TIME"))
        If CheckApiRunConditions(dt, "PaykiiIOCatalogUpdate", 2) Then PaykiiIOCatalogUpdate(dt.Rows(2)("CURRENT_TIME"))
        If CheckApiRunConditions(dt, "PaykiiFXRateUpdate", 3) Then PaykiiDailyFXRatePerBillerTypeUpdate(dt.Rows(3)("CURRENT_TIME"))

        Return True

    End Function

    Private Sub btnGetBillerCatalog_Click_1(sender As Object, e As EventArgs) Handles btnGetBillerCatalog.Click
        Try

            PaykiiBillerCatalogUpdate(Format(Now, dotNetDateFormat))

            Log("Completed")

        Catch ex As Exception
            Log(ex.Message, True)
        End Try

    End Sub

    Sub EndApplication()
        Log("Application Ended")
        End
    End Sub

    Private Sub btnAuto_Click(sender As Object, e As EventArgs)
        AutoExecuteAll()
        MsgBox("Completed")
    End Sub

    Private Sub btnGetCurrencies_Click(sender As Object, e As EventArgs) Handles btnGetSKUCatalog.Click

        Try

            PaykiiSKUCatalogUpdate(Format(Now, dotNetDateFormat))

            Log("Completed")

        Catch ex As Exception
            Log(ex.Message, True)
        End Try

    End Sub

    Private Sub btnGetIOCatalog_Click(sender As Object, e As EventArgs) Handles btnGetIOCatalog.Click

        Try

            PaykiiIOCatalogUpdate(Format(Now, dotNetDateFormat))

            Log("Completed")

        Catch ex As Exception
            Log(ex.Message, True)
        End Try

    End Sub

    Private Sub btnGetFxRate_Click(sender As Object, e As EventArgs) Handles btnGetFxRate.Click

        Try

            PaykiiDailyFXRatePerBillerTypeUpdate(Format(Now, dotNetDateFormat))

            Log("Completed")

        Catch ex As Exception
            Log(ex.Message, True)
        End Try

    End Sub

    Private Sub frmPaykiiIntegrator_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        End
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CheckArInvalidation()
    End Sub

    Private Function CheckArInvalidation() As Boolean
        Dim ret As Boolean = True
        Dim payArInvc As Dictionary(Of String, Decimal) = New Dictionary(Of String, Decimal)()
        Dim paidArInvc As Dictionary(Of String, Decimal) = New Dictionary(Of String, Decimal)()

        Using conn As OracleConnection = New OracleConnection(String.Concat(String.Concat(String.Concat(String.Concat("Data Source=" + VendTek.AdministratorNet.ClsRegistry.GetSetting("eComm", "Service", "efresh") + ";", "User ID=", VendTek.AdministratorNet.ClsRegistry.GetSetting("eComm", "UID", "ec"), ";"), "Password=", VendTek.AdministratorNet.Tools.DecPW(VendTek.AdministratorNet.ClsRegistry.GetSetting("eComm", "PWD")), ";"), "Max Pool Size=", VendTek.AdministratorNet.ClsRegistry.GetSetting("eComm", "MaxPoolSize", "200"), ";"), "Pooling=true;"))
            conn.Open()

            Using cmdInvc As OracleCommand = New OracleCommand()
                cmdInvc.Connection = conn
                'cmdInvc.CommandText = " Select R.CINVNO as Refno,(R.total-nvl(R.totalPaid,0)) toPay, R.Duedate  from ar_consumable_register R where  R.status='C' and (R.paidstatus is null or lower(R.paidstatus)=:pUnderPaidStatus or lower(R.paidstatus)=:pUnpaidstatus)  and R.Merchantid=:pMerchantid order by ReportDate, CINVNO asc"
                'cmdInvc.Parameters.Add("pUnderPaidStatus", OracleType.NVarChar).Value = "underpaid"
                'cmdInvc.Parameters.Add("pUnpaidstatus", OracleType.NVarChar).Value = "unpaid"
                'cmdInvc.Parameters.Add("pMerchantid", OracleType.NVarChar).Value = "TESTMT1"

                cmdInvc.CommandText = " Select * from ar_consumable_register where rownum<10 "


                Using drArInvc As OracleDataReader = cmdInvc.ExecuteReader()
                    Dim balanceLeft As Decimal = 100
                    Dim toPay As Decimal
                    Dim invcRefno As String

                    While drArInvc.Read()
                        toPay = drArInvc("topay")
                        invcRefno = drArInvc("refno")

                        If balanceLeft > 0 Then

                            If toPay >= balanceLeft Then
                                payArInvc.Add(invcRefno, toPay - balanceLeft)
                                'Log(String.Format("To pay {0} for ArInvoice {1}", balanceLeft.ToString(), invcRefno), ECLogic.MerchantService.ENULOGOS.none)
                                'cltNotes.Append(String.Format("ArInvoice: {0} clear: {1};", invcRefno, balanceLeft.ToString()))
                                paidArInvc.Add(invcRefno, balanceLeft)
                                balanceLeft = 0
                            Else
                                payArInvc.Add(invcRefno, 0)
                                balanceLeft -= toPay
                                ' Log(String.Format("To pay {0} for payArInvc {1}", toPay.ToString(), invcRefno), ECLogic.MerchantService.ENULOGOS.none)
                                ' cltNotes.Append(String.Format("payArInvc: {0} clear: {1};", invcRefno, toPay.ToString()))
                                paidArInvc.Add(invcRefno, toPay)
                            End If
                        Else
                        End If
                    End While

                    drArInvc.Close()

                    'If payType = CInt(collectionType.CA) AndAlso balanceLeft > _merchant.eCashBalance - Terminal.eCashLimit Then
                    '    ret = False
                    '    ApiResultCode = "10"
                    'End If

                    'If balanceLeft > 0 Then
                    '    Log(String.Format(" {0} extra payment.Will be added to your ecash balance.", balanceLeft.ToString()), ECLogic.MerchantService.ENULOGOS.none)
                    'End If

                    ' paidCreditBalance = Amount - balanceLeft
                End Using
            End Using

            conn.Close()
        End Using

        Return ret
    End Function
End Class
