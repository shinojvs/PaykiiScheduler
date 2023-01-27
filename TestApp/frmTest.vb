Imports System.IO
Imports System.Net
Imports System.Text
Imports Newtonsoft.Json
Imports RestSharp
Imports System
Imports System.Web.Script.Serialization

Public Class frmOraFusion

    Private purchaseURL As String = "https://de-app.azurewebsites.net/voucher/rest"
    Private CredentialsUserName As String = "one_user_test"
    Private CredentialsPassword As String = "12qwaq@123"
    Private Key As String = "16789efb5c9e4cfcc39ce2504b234bd9"

    Private dt As DataTable

    Sub Log(pData As String, Optional isError As Boolean = False)
        txtLog.Text += pData + vbNewLine
        'File.WriteAllText(Application.StartupPath + "\LogFiles\" + "Log_" + Format(Now, "ddMMyyyy.txt", pData)
        WriteToLogFile(pData, isError)
    End Sub

    Public Sub WriteToLogFile(ByVal Info As String, Optional isError As Boolean = False)
        Try
            Dim objWriter As New System.IO.StreamWriter(My.Application.Info.DirectoryPath + "\LogFiles\" & IIf(isError, "Error", "") & "Log_" + Format(Now.Date, "ddMMMyyyy") + ".txt", True)
            objWriter.WriteLine(Info + " - " + Format(Now, "dd-MMM-yyy HH:mm:ss"))
            objWriter.WriteLine("********************************************************************************************")
            objWriter.Close()
        Catch ex As Exception
            MsgBox("Error while writing to text file Error:" & ex.Message)
        End Try
    End Sub



    Public Class GSPinData
        Public Product As String
        Public FaceValue As String
        Public VoucherCurrency As String
        Public PinCode As String
        Public SerialNumber As String
        Public Barcode As String
        Public Expiry As String
    End Class

    Public Class GSPinResponse
        Public data As GSPinData
        Public status As Boolean
        Public message As String
        Public orderId As String
        Public RefId As String
        Public receipt_text As String
    End Class

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Dim voucherExpiry As String = "2023-07-26 00:00:00"
        Dim dt As String = New DateTime(Int32.Parse(voucherExpiry.Substring(0, 4)), Int32.Parse(voucherExpiry.Substring(5, 2)), Int32.Parse(voucherExpiry.Substring(8, 2)), 23, 59, 59).ToString()
        MsgBox(dt)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        Dim sendData = New clsFusion.SendData

        Dim timeEvents = New List(Of clsFusion.TimeEvents)()
        Dim timeEvent As clsFusion.TimeEvents

        Dim timeEventAttributes = New List(Of clsFusion.TimeEventAttributes)()
        Dim timeEventAttribute As clsFusion.TimeEventAttributes

        timeEventAttribute = New clsFusion.TimeEventAttributes()
        timeEventAttributes.Add(timeEventAttribute)

        timeEvent = New clsFusion.TimeEvents()
        timeEvent.deviceId = "301"
        timeEvent.timeEventAttributes = timeEventAttributes
        timeEvents.Add(timeEvent)

        timeEvent = New clsFusion.TimeEvents()
        timeEvent.deviceId = "401"
        timeEvent.timeEventAttributes = timeEventAttributes
        timeEvents.Add(timeEvent)
        sendData.timeEvents = timeEvents


        'timeEvents.timeEventAttributes(0) = New clsFusion.TimeEventAttributes
        'Dim timeEventAttributes = New clsFusion.TimeEventAttributes
        'timeEventAttributes.name = ""
        'timeEventAttributes.value = ""
        'timeEvents.timeEventAttributes.Add(timeEventAttributes)
        'sendData.timeEvents.Add(timeEvents)
        'Dim timeEvents As New clsFusion.TimeEvents


        'sendData.timeEvents(0) = New clsFusion.TimeEvents
        'sendData.timeEvents(0).timeEventAttributes(0) = New clsFusion.TimeEventAttributes
        'Dim timeEvents As New clsFusion.TimeEvents
        'Dim timeEventAttributes As New clsFusion.TimeEventAttributes

        'timeEvents.eventDateTime = "2022-06-07T08:31:00.000+00:00"
        'timeEvents.reporterId = "2001"
        'timeEvents.timeEventAttributes(0) = timeEventAttributes

        'sendData.timeEvents(0).timeEventAttributes(0).name = ""

        'sendData.timeEvents(0) = timeEvents
        'sendData.timeEvents(0).timeEventAttributes(0) = timeEventAttributes

        Dim inputJson = (New JavaScriptSerializer()).Serialize(sendData)
        txtLog.Text = inputJson
    End Sub

    Private Sub btnClear_Click_1(sender As Object, e As EventArgs) Handles btnClear.Click
        txtLog.Text = ""
    End Sub

    Private Sub frmOraFusion_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Log("Started")

        If Now.Year > 2022 Or Now.Month > 10 Then
            Log("Please contact Admin")
            End
        End If

        txtUserName.Text = My.Settings.UserName
        txtPassword.Text = My.Settings.Password
        txtURL.Text = My.Settings.URL

        If My.Application.CommandLineArgs.Count > 0 Then
            Auto()
        End If

    End Sub

    Private Function GetUTCformattedDateTime(dt As DateTime)
        'Dim timeZone As TimeZoneInfo = TimeZoneInfo.Local
        'Dim utc As DateTime = TimeZoneInfo.ConvertTimeToUtc(dt, timeZone)
        Return Format(dt, "yyyy-MM-dd'T'HH:mm:ss.fff+00:00")
    End Function

    Private Sub btnSendTestData_Click(sender As Object, e As EventArgs) Handles btnSendTestData.Click
        Try
            Dim sendData = New clsFusion.SendData
            sendData.requestNumber = txtRequestNumber.Text
            sendData.sourceId = txtSourceID.Text
            sendData.requestTimestamp = GetUTCformattedDateTime(dtpRequestTimestamp.Value)

            Dim timeEvents = New List(Of clsFusion.TimeEvents)()

            Dim timeEvent As clsFusion.TimeEvents

            Dim timeEventAttributes = New List(Of clsFusion.TimeEventAttributes)()
            Dim timeEventAttribute As clsFusion.TimeEventAttributes

            timeEventAttribute = New clsFusion.TimeEventAttributes()
            timeEventAttribute.name = txtName.Text
            timeEventAttribute.value = txtValue.Text
            timeEventAttributes.Add(timeEventAttribute)

            timeEvent = New clsFusion.TimeEvents()
            timeEvent.deviceId = txtDeviceID.Text
            timeEvent.eventDateTime = GetUTCformattedDateTime(dtpEventDateTime.Value)
            timeEvent.supplierDeviceEvent = txtSupplierDeviceEvent.Text
            timeEvent.reporterId = txtReporterID.Text
            timeEvent.reporterIdType = txtReporterIDType.Text
            timeEvent.timeEventAttributes = timeEventAttributes
            timeEvents.Add(timeEvent)

            sendData.timeEvents = timeEvents

            Dim inputJson = (New JavaScriptSerializer()).Serialize(sendData)
            Log("Send Data= " + inputJson)

            '''''********************* Send to End Point ****************************'

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 Or System.Net.SecurityProtocolType.Tls11 Or System.Net.SecurityProtocolType.Tls

            Dim client = New RestClient(txtURL.Text)
            client.Timeout = 60 * 1000
            client.ClearHandlers()

            Dim request = New RestRequest(Method.POST)
            Dim enCodedCredentials As String = Convert.ToBase64String(Encoding.ASCII.GetBytes(txtUserName.Text + ":" + txtPassword.Text))
            Log("enCodedCredentials=" + enCodedCredentials)
            request.AddHeader("Authorization", "Basic " + enCodedCredentials)
            request.AddHeader("Content-Type", "application/json")
            Dim body = inputJson
            request.AddParameter("application/json", body, ParameterType.RequestBody)
            Dim response As IRestResponse = client.Execute(request)


            Log(response.Content)

            '''Dim client = New RestClient(txtURL.Text)
            ''client.Authenticator = New Authenticators.HttpBasicAuthenticator(txtUserName.Text, txtPassword.Text)
            ''client.Timeout = 60 * 1000
            ''client.ClearHandlers()
            ''client.AddHandler("application/json", New Deserializers.JsonDeserializer())
            ''Dim reqToken = New RestRequest(Method.POST)
            '''reqToken.AlwaysMultipartFormData = True
            '''reqToken.AddParameter("payload", mstrRequest)
            ''Dim response As IRestResponse = client.Execute(reqToken)
            '''txtData.Text = response.Content

            ''Dim objGSToken = JsonConvert.DeserializeObject(Of GSTokenResponse)(response.Content)

            ''MsgBox("Token:" & response.Content)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Log(Convert.ToBase64String(Encoding.ASCII.GetBytes("shinoj" + ":" + "123456")))
        'txtLog.Text = (Format(Now, "yyyy-MM-dd'T'HH:mm:ss.fffzzz"))
        'YYYY-MM-DDT12:30:00.000+00:00
        '2022-08-31T22:23:21.795+04:00
    End Sub

    Private Function FetchDBData() As Boolean
        Try
            dt = New DataTable
            dt = ReturnDataTable(My.Settings.Query.Replace("$AUTOID", ExecuteScalarFunction("select RecordCount from zzz_idv")))
            If dt.Rows.Count <= 0 Then
                Log("0 rows returned from DB")
                Return False
            End If
            DGVUsers.DataSource = dt
            Return True
        Catch ex As Exception
            Log(ex.Message)
            Return False
        End Try
    End Function

    Private Function SendDataDB() As Boolean
        Try
            'dt = New DataTable
            'dt = ReturnDataTable(My.Settings.Query.Replace("$AUTOID", ExecuteScalarFunction("select recordcount from zzz_idv")))
            'If dt.Rows.Count <= 0 Then
            '    Log("0 rows returned from DB")
            '    Return False
            'End If

            'DGVUsers.DataSource = dt

            Dim sendData = New clsFusion.SendData
            sendData.requestNumber = ExecuteScalarFunction("select sequenceNumber from zzz_idv ")
            sendData.requestTimestamp = GetUTCformattedDateTime(Now)

            Dim timeEvents = New List(Of clsFusion.TimeEvents)()

            Dim timeEvent As clsFusion.TimeEvents

            Dim timeEventAttributes = New List(Of clsFusion.TimeEventAttributes)()
            Dim timeEventAttribute As clsFusion.TimeEventAttributes

            timeEventAttribute = New clsFusion.TimeEventAttributes()
            timeEventAttribute.name = txtName.Text
            timeEventAttribute.value = txtValue.Text
            timeEventAttributes.Add(timeEventAttribute)

            For Each dr As DataRow In dt.Rows
                sendData.sourceId = dr("devnm").ToString
                timeEvent = New clsFusion.TimeEvents()
                timeEvent.deviceId = txtDeviceID.Text
                timeEvent.eventDateTime = GetUTCformattedDateTime(CDate(dr("PunchData")))
                timeEvent.supplierDeviceEvent = IIf(dr("PunchType") = 0, dr("devnm") + "_IN", dr("devnm") + "_OUT")
                timeEvent.reporterId = dr("EmpID")
                timeEvent.reporterIdType = txtReporterIDType.Text
                timeEvent.timeEventAttributes = timeEventAttributes
                timeEvents.Add(timeEvent)

            Next

            sendData.timeEvents = timeEvents

            Dim inputJson = (New JavaScriptSerializer()).Serialize(sendData)
            Log("Send Data= " + inputJson)

            'ExecuteNonQueryFunction("Update zzz_idv set SequenceNumber=SequenceNumber+1,RecordCount=" & dt.Rows(dt.Rows.Count - 1)("ID").ToString)

            'Return True

            '''''********************* Send to End Point ****************************'

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 Or System.Net.SecurityProtocolType.Tls11 Or System.Net.SecurityProtocolType.Tls

            Dim client = New RestClient(txtURL.Text)
            client.Timeout = 60 * 1000
            client.ClearHandlers()

            Dim request = New RestRequest(Method.POST)
            Dim enCodedCredentials As String = Convert.ToBase64String(Encoding.ASCII.GetBytes(txtUserName.Text + ":" + txtPassword.Text))
            Log("enCodedCredentials=" + enCodedCredentials)
            request.AddHeader("Authorization", "Basic " + enCodedCredentials)
            request.AddHeader("Content-Type", "application/json")
            Dim body = inputJson
            request.AddParameter("application/json", body, ParameterType.RequestBody)
            Dim response As IRestResponse = client.Execute(request)

            Log("Response: " + response.Content)


            If response.StatusCode = HttpStatusCode.Created Then
                ExecuteNonQueryFunction("Update zzz_idv set SequenceNumber=SequenceNumber+1,RecordCount=" & dt.Rows(dt.Rows.Count - 1)("ID").ToString)
                Log("Updated DB RecordCount= " + dt.Rows(dt.Rows.Count - 1)("ID").ToString)
            End If

            dt = Nothing
            GC.Collect()
            GC.Collect()

            Return True
        Catch ex As Exception
            Log(ex.Message, True)
            Return False
        End Try
    End Function

    Private Sub btnFetchDBData_Click(sender As Object, e As EventArgs) Handles btnFetchDBData.Click
        FetchDBData()
    End Sub

    Private Sub btnSendDataDB_Click(sender As Object, e As EventArgs) Handles btnSendDataDB.Click
        SendDataDB()
    End Sub

    Private Sub Auto()
        Try

            While FetchDBData() = True
                If SendDataDB() = False Then EndApplication()
            End While
            EndApplication()
        Catch ex As Exception
            Log(ex.Message, True)
            EndApplication()
        End Try

    End Sub

    Sub EndApplication()
        Log("Application Ended")
        End
    End Sub

    Private Sub btnAuto_Click(sender As Object, e As EventArgs) Handles btnAuto.Click
        Auto()
    End Sub
End Class
