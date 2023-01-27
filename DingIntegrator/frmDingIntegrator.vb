Imports System.IO
Imports System.Net
Imports System.Text
Imports Newtonsoft.Json
Imports RestSharp
Imports System
Imports System.Web.Script.Serialization

Public Class frmDingIntegrator


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
            Log("Started")

            txtUserName.Text = My.Settings.UserName
            txtPassword.Text = My.Settings.Password
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
                             " from DING_SCHEDULER_CONFIG order by SLNO")

        If CheckApiRunConditions(dt, "ProductUpdate", 0) Then DingProductUpdate(dt.Rows(0)("CURRENT_TIME"))
        If CheckApiRunConditions(dt, "ProductDescriptionUpdate", 1) Then DingProductDescriptionUpdate(dt.Rows(1)("CURRENT_TIME"))
        If CheckApiRunConditions(dt, "ProviderStatusUpdate", 2) Then DingProviderStatusUpdate(dt.Rows(2)("CURRENT_TIME"))
        If CheckApiRunConditions(dt, "ProviderUpdate", 3) Then DingProviderUpdate(dt.Rows(3)("CURRENT_TIME"))
        If CheckApiRunConditions(dt, "RegionUpdate", 4) Then DingRegionUpdate(dt.Rows(4)("CURRENT_TIME"))
        If CheckApiRunConditions(dt, "PromotionUpdate", 5) Then DingPromotionUpdate(dt.Rows(5)("CURRENT_TIME"))
        If CheckApiRunConditions(dt, "PromotionDescUpdate", 6) Then DingPromotionDescUpdate(dt.Rows(6)("CURRENT_TIME"))

        Return True

    End Function

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles btnGetCountries.Click
        Try

            'DingGetCountries()

            MsgBox("Not Available")

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub EndApplication()
        Log("Application Ended")
        End
    End Sub

    Private Sub btnAuto_Click(sender As Object, e As EventArgs) Handles btnAuto.Click
        AutoExecuteAll()
        MsgBox("Completed")
    End Sub

    Private Sub btnGetCurrencies_Click(sender As Object, e As EventArgs) Handles btnGetCurrencies.Click
        Try

            DingGetCurrencies()

            MsgBox("Completed")

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnGetRegions_Click(sender As Object, e As EventArgs) Handles btnGetRegionsByCountry.Click

        GetRegionsByCountry()
        MsgBox("Completed")

    End Sub

    Private Sub btnGetAllRegions_Click(sender As Object, e As EventArgs) Handles btnGetAllRegions.Click
        Try

            DingRegionUpdate(Format(Now, dotNetDateFormat))
            MsgBox("Completed")

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnGetProviders_Click(sender As Object, e As EventArgs) Handles btnGetProviders.Click
        Try

            DingProviderUpdate(Format(Now, dotNetDateFormat))

            MsgBox("Completed")

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnGetProviderStatus_Click(sender As Object, e As EventArgs) Handles btnGetProviderStatus.Click
        Try

            DingProviderStatusUpdate(Format(Now, dotNetDateFormat))
            MsgBox("Completed")

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnGetProductDescriptions_Click(sender As Object, e As EventArgs) Handles btnGetProductDescriptions.Click
        Try

            DingProductDescriptionUpdate(Format(Now, dotNetDateFormat))
            MsgBox("Completed")

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnGetPromotions_Click(sender As Object, e As EventArgs) Handles btnGetPromotions.Click
        Try

            DingPromotionUpdate(Format(Now, dotNetDateFormat))

            MsgBox("Completed")

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnGetPromotionsDesc_Click(sender As Object, e As EventArgs) Handles btnGetPromotionsDesc.Click
        Try

            DingPromotionDescUpdate(Format(Now, dotNetDateFormat))
            MsgBox("Completed")

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnGetProducts_Click(sender As Object, e As EventArgs) Handles btnGetProducts.Click
        Try

            DingProductUpdate(Format(Now, dotNetDateFormat))

            MsgBox("Completed")

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dt As New DataTable
        dt = ReturnDataTable("Select * From ding_products")
        DBStr(dt.Rows(0)(0))
    End Sub
End Class
