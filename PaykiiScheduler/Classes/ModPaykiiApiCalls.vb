
Imports Newtonsoft.Json
Imports RestSharp
Imports System
Imports System.IO
Imports System.Text
Imports System.Web.Script.Serialization
Module ModPaykiiApiCalls

    Private _url As String = My.Settings.URL
    Private _x_api_key As String = My.Settings.x_api_key
    Private _token As String = My.Settings.token

#Region "Common Functions Variables"
    Private ROW_NOT_EXISTS As String = "ROW_NOT_EXISTS"
    Private ROW_EXISTS As String = "ROW_EXISTS"
    Private ROW_UPDATED As String = "ROW_UPDATED"
#End Region
    Private Function GetPaykiiApiResponse(paramApiName As String, paramJsonBody As String) As IRestResponse

        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 Or System.Net.SecurityProtocolType.Tls11 Or System.Net.SecurityProtocolType.Tls
        Dim client = New RestClient(_url + paramApiName)
        client.Timeout = 60 * 1000
        client.ClearHandlers()

        Dim request = New RestRequest(Method.POST)
        request.AddHeader("x-api-key", _x_api_key)
        request.AddHeader("Token", _token)
        request.AddHeader("Content-Type", "application/json")
        request.AddParameter("application/json", paramJsonBody, ParameterType.RequestBody)

        Dim response As IRestResponse = client.Execute(request)
        Return response

    End Function

#Region "PaykiiBillerCatalog"

    Function CheckBillerCatalogRowStatus(itm As ClsPaykii.BillerCatalogRes) As String

        Dim _sql As String = "" &
        "Select * From PAYKII_BILLER_CATALOG Where  BillerID='" & DBStr(itm.BillerID) & "' "

        Dim dt As New DataTable
        dt = ReturnDataTable(_sql)

        If (dt.Rows.Count > 0) Then
            If 1 = 1 AndAlso
              DBStr(dt.Rows(0)("BillerID")) = DBStr(itm.BillerID) And
              DBStr(dt.Rows(0)("CountryCode")) = DBStr(itm.CountryCode) And
              DBStr(dt.Rows(0)("CountryName")) = DBStr(itm.CountryName) And
              DBStr(dt.Rows(0)("BillerName")) = DBStr(itm.BillerName) And
              DBStr(dt.Rows(0)("BillerType")) = DBStr(itm.BillerType) And
              DBStr(dt.Rows(0)("BillerDescription")) = DBStr(itm.BillerDescription) Then

                Return ROW_EXISTS
            Else

                Return ROW_UPDATED

            End If
        Else

            Return ROW_NOT_EXISTS

        End If



    End Function

    Function ReturnJsonBodyBillerCatalogReq() As String
        Dim sbRequest As StringBuilder = New StringBuilder("")
        sbRequest.Append("{")
        sbRequest.Append(RetVariableWithinDoubleQuotes("LocationID") + ":" + RetVariableWithinDoubleQuotes("1"))
        sbRequest.Append(",")
        sbRequest.Append(RetVariableWithinDoubleQuotes("PointOfSaleID") + ":" + RetVariableWithinDoubleQuotes("1"))
        sbRequest.Append("}")
        Return sbRequest.ToString()
    End Function

    Sub UpdateBillerCatalogDB(itm As ClsPaykii.BillerCatalogRes)

        Dim _tempStr As String = "" +
         " Update PAYKII_BILLER_CATALOG set " &
         " ResponseCode='" & DBStr(itm.ResponseCode) & "', " &
         " ResponseMessage='" & DBStr(itm.ResponseMessage) & "', CatalogVersion='" & DBStr(itm.CatalogVersion) & "', " &
         " CountryCode='" & DBStr(itm.CountryCode) & "', CountryName='" & DBStr(itm.CountryName) & "', " &
         " BillerID='" & DBStr(itm.BillerID) & "', BillerName='" & DBStr(itm.BillerName) & "', " &
         " BillerType='" & DBStr(itm.BillerType) & "', BillerDescription='" & DBStr(itm.BillerDescription) & "', " &
         " INSERTEDTIME=INSERTEDTIME , INSERTEDFLAG=0 , " &
         " UPDATEDTIME=sysdate , UPDATEDFLAG=1 , " &
         " DELETEDTIME=DELETEDTIME , DELETEDFLAG=0 " &
         " Where BillerID='" & DBStr(itm.BillerID) & "'"

        ExecuteNonQueryFunction(_tempStr)

    End Sub

    Sub InsertBillerCatalogDB(itm As ClsPaykii.BillerCatalogRes)

        Dim _tempStr As String = "Insert into PAYKII_BILLER_CATALOG (" &
            " ResponseCode, ResponseMessage, CatalogVersion, CountryCode, CountryName, " &
            " BillerID, BillerName, BillerType, BillerDescription, " &
            " INSERTEDTIME, UPDATEDTIME, DELETEDTIME, " &
            " INSERTEDFLAG, UPDATEDFLAG, DELETEDFLAG " &
            " ) Values ( " &
            "'" & DBStr(itm.ResponseCode) & "','" & DBStr(itm.ResponseMessage) & "','" & DBStr(itm.CatalogVersion) & "'," &
            "'" & DBStr(itm.CountryCode) & "','" & DBStr(itm.CountryName) & "','" & DBStr(itm.BillerID) & "'," &
            "'" & DBStr(itm.BillerName) & "','" & DBStr(itm.BillerType) & "','" & DBStr(itm.BillerDescription) & "'," &
            " sysdate, to_date('2000-01-01','yyyy-mm-dd'), to_date('2000-01-01','yyyy-mm-dd'), " &
            " 1,0,0 " &
            ")"

        ExecuteNonQueryFunction(_tempStr)

    End Sub

    Sub UpdateSkuCatalogForBillerCatalogUpdate(itm As ClsPaykii.BillerCatalogRes)
        Dim _tempStr As String = "" +
         " Update PAYKII_SKU_CATALOG set " &
         " INSERTEDTIME=INSERTEDTIME , INSERTEDFLAG=0 , " &
         " UPDATEDTIME=sysdate , UPDATEDFLAG=1 , " &
         " DELETEDTIME=DELETEDTIME , DELETEDFLAG=0 " &
         " Where BillerID='" & DBStr(itm.BillerID) & "'"

        ExecuteNonQueryFunction(_tempStr)
    End Sub

    Sub UpdateSkuCatalogForBillerCatalogInsert(itm As ClsPaykii.BillerCatalogRes)
        Dim _tempStr As String = "" +
                     " Update PAYKII_SKU_CATALOG set " &
                     " INSERTEDTIME=sysdate , INSERTEDFLAG=1 , " &
                     " UPDATEDTIME=UPDATEDTIME , UPDATEDFLAG=0 , " &
                     " DELETEDTIME=DELETEDTIME , DELETEDFLAG=0 " &
                     " Where BillerID='" & DBStr(itm.BillerID) & "'"

        ExecuteNonQueryFunction(_tempStr)
    End Sub

    Sub UpdateBillerCatalogForDelete(dt_Data As DataTable)
        Dim _tempStr As String
        For Each dr As DataRow In dt_Data.Rows
            If dr("RowExist") = 0 Then
                _tempStr = "" +
                 "Update PAYKII_BILLER_CATALOG set " &
                 " INSERTEDTIME=INSERTEDTIME , INSERTEDFLAG=0 , " &
                 " UPDATEDTIME=UPDATEDTIME , UPDATEDFLAG=0 , " &
                 " DELETEDTIME=sysdate , DELETEDFLAG=1 " &
                " Where BillerID='" & dr("BillerID") & "'"

                ExecuteNonQueryFunction(_tempStr)

                _tempStr = "" +
                 " Update PAYKII_SKU_CATALOG set " &
                 " INSERTEDTIME=INSERTEDTIME , INSERTEDFLAG=0 , " &
                 " UPDATEDTIME=UPDATEDTIME , UPDATEDFLAG=0 , " &
                 " DELETEDTIME=sysdate , DELETEDFLAG=1 " &
                 " Where BillerID='" & dr("BillerID") & "'"

                ExecuteNonQueryFunction(_tempStr)

                WriteToLogFile("Deleted PAYKII_BILLER_CATALOG " + dr("BillerID"))
            End If
        Next
    End Sub

    Function PaykiiBillerCatalogUpdate(schedulerTime As String) As Boolean

        Try

            WriteToLogFile("PaykiiBillerCatalogUpdate Started")

            UpdatePaykiiSchedulerFields("LAST_RUN_TIME", " '" & schedulerTime & "' ", "PaykiiBillerCatalogUpdate", False)

            Dim response As IRestResponse = GetPaykiiApiResponse("BillerCatalog", ReturnJsonBodyBillerCatalogReq.ToString())
            Dim objRes = JsonConvert.DeserializeObject(Of List(Of ClsPaykii.BillerCatalogRes))(response.Content)
            Dim tempCount As Int16 = 0
            Dim _tempStr As String, _tempNumber As ULong = 0
            Dim dt_Data As New DataTable
            Dim pCheckRowStatus As String

            dt_Data = ReturnDataTable("Select a.*, 0 as RowExist From PAYKII_BILLER_CATALOG a")

            For Each itm As ClsPaykii.BillerCatalogRes In objRes

                pCheckRowStatus = CheckBillerCatalogRowStatus(itm)

                For Each dr As DataRow In dt_Data.Rows

                    If DBStr(dr("BillerID")).Equals(DBStr(itm.BillerID)) Then
                        dr("RowExist") = 1
                        Exit For
                    End If

                Next

                If (pCheckRowStatus = ROW_UPDATED) Then

                    UpdateBillerCatalogDB(itm)
                    UpdateSkuCatalogForBillerCatalogUpdate(itm)

                    WriteToLogFile("Updated PAYKII_BILLER_CATALOG " + DBStr(itm.BillerID))

                ElseIf (pCheckRowStatus = ROW_NOT_EXISTS) Then

                    InsertBillerCatalogDB(itm)
                    UpdateSkuCatalogForBillerCatalogInsert(itm)

                    WriteToLogFile("Added PAYKII_BILLER_CATALOG " + DBStr(itm.BillerID))

                End If

                _tempNumber += 1

                frmPaykiiIntegrator.txtLog.Text = String.Format("Processed {0} / {1} at {2}", _tempNumber.ToString, objRes.Count.ToString, Format(Now, "dd-MMM-yyyy HH:mm:ss"))
                Application.DoEvents()

            Next

            UpdateBillerCatalogForDelete(dt_Data)

            _tempStr = String.Format("Process Completed. JsonItems: {0}  DBItems: {1} at {2}", objRes.Count.ToString, dt_Data.Rows.Count.ToString, Format(Now, "dd-MMM-yyyy HH:mm:ss"))
            WriteToLogFile(_tempStr)

            UpdatePaykiiSchedulerFields("LAST_SUCCESFUL_RUN_TIME", " '" & schedulerTime & "' ", "PaykiiBillerCatalogUpdate", False)
            Return True

        Catch ex As Exception
            WriteToLogFile("PaykiiBillerCatalogUpdate Ex: " & ex.Message, True)
            UpdatePaykiiSchedulerFields("ERROR_MSG", ex.Message, "PaykiiBillerCatalogUpdate", True)
            Return False
        End Try
    End Function

#End Region

#Region "PaykiiSKUCatalog"
    Function CheckSKUCatalogRowStatus(itm As ClsPaykii.SKUCatalogRes) As String

        Dim _sql As String = "" &
        "Select * From PAYKII_SKU_CATALOG Where  BillerID='" & DBStr(itm.BillerID) & "' and SKU='" & DBStr(itm.SKU) & "'"

        Dim dt As New DataTable
        dt = ReturnDataTable(_sql)

        If (dt.Rows.Count > 0) Then
            If 1 = 1 AndAlso
                DBStr(dt.Rows(0)("BillerID")) = DBStr(itm.BillerID) And
                DBStr(dt.Rows(0)("SKU")) = DBStr(itm.SKU) And
                DBStr(dt.Rows(0)("DaysToPost")) = DBStr(itm.DaysToPost) And
                DBStr(dt.Rows(0)("Currency")) = DBStr(itm.Currency) And
                DBStr(dt.Rows(0)("BusinessDays")) = DBStr(itm.BusinessDays) And
                DBStr(dt.Rows(0)("PastDuePaymentAllowed")) = DBStr(itm.PastDuePaymentAllowed) And
                DBStr(dt.Rows(0)("Amount")) = DBStr(itm.Amount) And
                DBStr(dt.Rows(0)("Type")) = DBStr(itm.Type) And
                DBStr(dt.Rows(0)("Description")) = DBStr(itm.Description) And
                DBStr(dt.Rows(0)("InquiryAvailable")) = DBStr(itm.InquiryAvailable) And
                DBStr(dt.Rows(0)("ExcessPaymentAllowed")) = DBStr(itm.ExcessPaymentAllowed) And
                DBStr(dt.Rows(0)("PartialPaymentAllowed")) = DBStr(itm.PartialPaymentAllowed) Then

                Return ROW_EXISTS

            Else

                Return ROW_UPDATED

            End If

        Else

            Return ROW_NOT_EXISTS

        End If

    End Function

    Function ReturnJsonBodyReq() As String
        Dim sbRequest As StringBuilder = New StringBuilder("")
        sbRequest.Append("{")
        sbRequest.Append(RetVariableWithinDoubleQuotes("LocationID") + ":" + RetVariableWithinDoubleQuotes("1"))
        sbRequest.Append(",")
        sbRequest.Append(RetVariableWithinDoubleQuotes("PointOfSaleID") + ":" + RetVariableWithinDoubleQuotes("1"))
        sbRequest.Append("}")
        Return sbRequest.ToString()
    End Function

    Sub UpdateSKUCatalogDB(itm As ClsPaykii.SKUCatalogRes)
        Dim _tempStr As String = "" +
         " Update PAYKII_SKU_CATALOG set " &
         " ResponseCode='" & DBStr(itm.ResponseCode) & "', " &
         " ResponseMessage='" & DBStr(itm.ResponseMessage) & "', CatalogVersion='" & DBStr(itm.CatalogVersion) & "', " &
         " SKU='" & DBStr(itm.SKU) & "', DaysToPost=" & DBStr(itm.DaysToPost.ToString) & ", " &
         " Currency='" & DBStr(itm.Currency) & "', BillerID='" & DBStr(itm.BillerID) & "', " &
         " BusinessDays=" & DBStr(itm.BusinessDays.ToString) & ", PastDuePaymentAllowed=" & DBStr(itm.PastDuePaymentAllowed.ToString) & ", " &
         " Amount=" & DBStr(itm.Amount.ToString) & ", Type='" & DBStr(itm.Type) & "', " &
         " MinAmount=" & IIf(itm.MinAmount Is Nothing, "null", itm.MinAmount) & ", MaxAmount=" & IIf(itm.MaxAmount Is Nothing, "null", itm.MaxAmount) & ", " &
         " Description='" & DBStr(itm.Description) & "', InquiryAvailable=" & DBStr(itm.InquiryAvailable.ToString) & ", " &
         " ExcessPaymentAllowed=" & DBStr(itm.ExcessPaymentAllowed.ToString) & ", PartialPaymentAllowed=" & DBStr(itm.PartialPaymentAllowed.ToString) & ", " &
         " INSERTEDTIME=INSERTEDTIME , INSERTEDFLAG=0 , " &
         " UPDATEDTIME=sysdate , UPDATEDFLAG=1 , " &
         " DELETEDTIME=DELETEDTIME , DELETEDFLAG=0, Pkey='" & DBStr(itm.BillerID) + "_" + itm.SKU & "' " &
         " Where BillerID='" & DBStr(itm.BillerID) & "' and SKU='" & itm.SKU & "' "

        ExecuteNonQueryFunction(_tempStr)

    End Sub

    Sub InsertSKUCatalogDB(itm As ClsPaykii.SKUCatalogRes)

        Dim _tempStr As String = "Insert into PAYKII_SKU_CATALOG (" &
    " ResponseCode, ResponseMessage, CatalogVersion, SKU, DaysToPost, " &
    " Currency, BillerID, BusinessDays, PastDuePaymentAllowed, " &
    " Amount, Type, MinAmount, MaxAmount, " &
    " Description, InquiryAvailable, ExcessPaymentAllowed, PartialPaymentAllowed, " &
    " INSERTEDTIME, UPDATEDTIME, DELETEDTIME, " &
    " INSERTEDFLAG, UPDATEDFLAG, DELETEDFLAG, PKey " &
    " ) Values ( " &
    "'" & DBStr(itm.ResponseCode) & "','" & DBStr(itm.ResponseMessage) & "','" & DBStr(itm.CatalogVersion) & "'," &
    "'" & DBStr(itm.SKU) & "'," & DBStr(itm.DaysToPost.ToString) & ",'" & DBStr(itm.Currency) & "'," &
    "'" & DBStr(itm.BillerID) & "'," & DBStr(itm.BusinessDays.ToString) & "," & DBStr(itm.PastDuePaymentAllowed.ToString) & "," &
    "" & DBStr(itm.Amount.ToString) & ",'" & DBStr(itm.Type) & "'," & IIf(itm.MinAmount Is Nothing, "null", itm.MinAmount) & "," &
    "" & IIf(itm.MaxAmount Is Nothing, "null", itm.MaxAmount) & ",'" & DBStr(itm.Description) & "'," & DBStr(itm.InquiryAvailable.ToString) & "," &
    "" & DBStr(itm.ExcessPaymentAllowed.ToString) & "," & DBStr(itm.PartialPaymentAllowed.ToString) & "," &
    " sysdate, to_date('2000-01-01','yyyy-mm-dd'), to_date('2000-01-01','yyyy-mm-dd'), " &
    " 1,0,0,'" & DBStr(itm.BillerID) + "_" + itm.SKU & "' " &
    ")"

        ExecuteNonQueryFunction(_tempStr)

    End Sub

    Sub UpdateSKUCatalogForDelete(dt_Data As DataTable)
        Dim _tempStr As String
        For Each dr As DataRow In dt_Data.Rows

            If dr("RowExist") = 0 Then
                _tempStr = "" +
                 " Update PAYKII_SKU_CATALOG set " &
                 " INSERTEDTIME=INSERTEDTIME , INSERTEDFLAG=0 , " &
                 " UPDATEDTIME=UPDATEDTIME , UPDATEDFLAG=0 , " &
                 " DELETEDTIME=sysdate , DELETEDFLAG=1 " &
                 " Where BillerID='" & dr("BillerID") & "' and SKU='" & dr("sku") & "' "

                ExecuteNonQueryFunction(_tempStr)

                WriteToLogFile("Deleted PAYKII_SKU_CATALOG " + dr("BillerID"))
            End If
        Next
    End Sub


    Function PaykiiSKUCatalogUpdate(schedulerTime As String) As Boolean

        Try

            WriteToLogFile("PaykiiSKUCatalogUpdate Started")

            UpdatePaykiiSchedulerFields("LAST_RUN_TIME", " '" & schedulerTime & "' ", "PaykiiSKUCatalogUpdate", False)

            Dim response As IRestResponse = GetPaykiiApiResponse("SKUCatalog", ReturnJsonBodyReq())

            Dim objRes = JsonConvert.DeserializeObject(Of List(Of ClsPaykii.SKUCatalogRes))(response.Content)

            Dim tempCount As Int16 = 0

            Dim _tempStr As String, _tempNumber As ULong = 0

            Dim dt_Data As New DataTable
            Dim pCheckRowStatus As String

            dt_Data = ReturnDataTable("Select a.*, 0 as RowExist From PAYKII_SKU_CATALOG a")

            For Each itm As ClsPaykii.SKUCatalogRes In objRes

                pCheckRowStatus = CheckSKUCatalogRowStatus(itm)

                For Each dr As DataRow In dt_Data.Rows

                    If DBStr(dr("BillerID")).Equals(DBStr(itm.BillerID)) And DBStr(dr("SKU")).Equals(DBStr(itm.SKU)) Then
                        dr("RowExist") = 1
                        Exit For
                    End If

                Next

                If (pCheckRowStatus = ROW_UPDATED) Then

                    UpdateSKUCatalogDB(itm)

                    WriteToLogFile("Updated PAYKII_SKU_CATALOG " + DBStr(itm.BillerID))

                ElseIf (pCheckRowStatus = ROW_NOT_EXISTS) Then

                    InsertSKUCatalogDB(itm)

                    WriteToLogFile("Added PAYKII_SKU_CATALOG " + DBStr(itm.BillerID))

                End If

                _tempNumber += 1

                frmPaykiiIntegrator.txtLog.Text = String.Format("Processed {0} / {1} at {2}", _tempNumber.ToString, objRes.Count.ToString, Format(Now, "dd-MMM-yyyy HH:mm:ss"))
                Application.DoEvents()

            Next

            UpdateSKUCatalogForDelete(dt_Data)

            _tempStr = String.Format("Process Completed. JsonItems: {0}  DBItems: {1} at {2}", objRes.Count.ToString, dt_Data.Rows.Count.ToString, Format(Now, "dd-MMM-yyyy HH:mm:ss"))
            WriteToLogFile(_tempStr)

            UpdatePaykiiSchedulerFields("LAST_SUCCESFUL_RUN_TIME", " '" & schedulerTime & "' ", "PaykiiSKUCatalogUpdate", False)
            Return True

        Catch ex As Exception
            WriteToLogFile("PaykiiSKUCatalogUpdate Ex: " & ex.Message, True)
            UpdatePaykiiSchedulerFields("ERROR_MSG", ex.Message, "PaykiiSKUCatalogUpdate", True)
            Return False
        End Try
    End Function

#End Region

#Region "PaykiiIOCatalog"
    Function CheckIOCatalogRowStatus(itm As ClsPaykii.IOCatalogRes) As String

        Dim _sql As String = "" &
        "Select * From PAYKII_IO_CATALOG Where  BillerID='" & DBStr(itm.BillerID) & "' and SKU='" & DBStr(itm.SKU) & "' and  IOID=" & itm.IOID.ToString

        Dim dt As New DataTable
        dt = ReturnDataTable(_sql)

        If (dt.Rows.Count > 0) Then
            If 1 = 1 AndAlso
                DBStr(dt.Rows(0)("BillerID")) = DBStr(itm.BillerID) And
                DBStr(dt.Rows(0)("SKU")) = DBStr(itm.SKU) And
                DBStr(dt.Rows(0)("Name")) = DBStr(itm.Name) And
                DBStr(dt.Rows(0)("Datatype")) = DBStr(itm.Datatype) And
                DBStr(dt.Rows(0)("MinLength")) = DBStr(itm.MinLength) And
                DBStr(dt.Rows(0)("MaxLength")) = DBStr(itm.MaxLength) And
                DBStr(dt.Rows(0)("Type")) = DBStr(itm.Type) And
                DBStr(dt.Rows(0)("Description")) = DBStr(itm.Description) And
                DBStr(dt.Rows(0)("BillerID")) = DBStr(itm.BillerID) And
                DBStr(dt.Rows(0)("Operation")) = DBStr(itm.Operation) And
                DBStr(dt.Rows(0)("IOID")) = DBStr(itm.IOID) Then

                Return ROW_EXISTS
            Else

                Return ROW_UPDATED

            End If

        Else
            Return ROW_NOT_EXISTS
        End If



    End Function

    Function ReturnJsonBodyIOCatalogReq()
        Dim sbRequest As StringBuilder = New StringBuilder("")
        sbRequest.Append("{")
        sbRequest.Append(RetVariableWithinDoubleQuotes("LocationID") + ":" + RetVariableWithinDoubleQuotes("1"))
        sbRequest.Append(",")
        sbRequest.Append(RetVariableWithinDoubleQuotes("PointOfSaleID") + ":" + RetVariableWithinDoubleQuotes("1"))
        sbRequest.Append("}")
        Return sbRequest.ToString()
    End Function

    Sub UpdateIOCatalogDB(itm As ClsPaykii.IOCatalogRes)
        Dim _tempStr As String = "" +
         " Update PAYKII_IO_CATALOG set " &
         " ResponseCode='" & DBStr(itm.ResponseCode) & "', " &
         " ResponseMessage='" & DBStr(itm.ResponseMessage) & "', CatalogVersion='" & DBStr(itm.CatalogVersion) & "', " &
         " SKU='" & DBStr(itm.SKU) & "', Name='" & DBStr(itm.Name) & "', " &
         " Datatype='" & DBStr(itm.Datatype) & "', ValidLengths=" & IIf(itm.ValidLengths Is Nothing, "null", "'" & itm.ValidLengths & "'") & ", " &
         " MinLength=" & DBStr(itm.MinLength.ToString) & ", MaxLength=" & DBStr(itm.MaxLength.ToString) & ", " &
         " Type=" & DBStr(itm.Type.ToString) & ", Description='" & DBStr(itm.Description) & "', " &
         " BillerID='" & itm.BillerID & "', Operation=" & itm.Operation.ToString & ", " &
         " IOID=" & DBStr(itm.IOID.ToString) & ", " &
         " INSERTEDTIME=INSERTEDTIME , INSERTEDFLAG=0 , " &
         " UPDATEDTIME=sysdate , UPDATEDFLAG=1 , " &
         " DELETEDTIME=DELETEDTIME , DELETEDFLAG=0 " &
         " Where BillerID='" & DBStr(itm.BillerID) & "' and SKU='" & DBStr(itm.SKU) & "' and IOID=" & DBStr(itm.IOID) & ""

        ExecuteNonQueryFunction(_tempStr)

    End Sub

    Sub InsertIOCatalogDB(itm As ClsPaykii.IOCatalogRes)

        Dim _tempStr As String = "Insert into PAYKII_IO_CATALOG (" &
            " ResponseCode, ResponseMessage, CatalogVersion, SKU, Name, " &
            " Datatype, ValidLengths, MinLength, MaxLength, " &
            " Type, Description, BillerID, Operation, IOID, " &
            " INSERTEDTIME, UPDATEDTIME, DELETEDTIME, " &
            " INSERTEDFLAG, UPDATEDFLAG, DELETEDFLAG " &
            " ) Values ( " &
            "'" & DBStr(itm.ResponseCode) & "','" & DBStr(itm.ResponseMessage) & "','" & DBStr(itm.CatalogVersion) & "'," &
            "'" & DBStr(itm.SKU) & "','" & DBStr(itm.Name) & "','" & DBStr(itm.Datatype) & "'," &
            "" & IIf(itm.ValidLengths Is Nothing, "null", "'" & itm.ValidLengths & "'") & "," & DBStr(itm.MinLength.ToString) & "," & DBStr(itm.MaxLength.ToString) & "," &
            "" & DBStr(itm.Type.ToString) & ",'" & DBStr(itm.Description) & "','" & DBStr(itm.BillerID) & "'," &
            "" & DBStr(itm.Operation.ToString) & "," & DBStr(itm.IOID.ToString) & "," &
            " sysdate, to_date('2000-01-01','yyyy-mm-dd'), to_date('2000-01-01','yyyy-mm-dd'), " &
            " 1,0,0 " &
            ")"

        ExecuteNonQueryFunction(_tempStr)

    End Sub

    Sub UpdateIoCatalogForBillerCatalogUpdateDB(itm As ClsPaykii.IOCatalogRes)

        Dim _tempStr As String = "" +
         " Update PAYKII_SKU_CATALOG set " &
         " INSERTEDTIME=INSERTEDTIME , INSERTEDFLAG=0 , " &
         " UPDATEDTIME=sysdate , UPDATEDFLAG=1 , " &
         " DELETEDTIME=DELETEDTIME , DELETEDFLAG=0 " &
         " Where BillerID='" & itm.BillerID & "' and SKU='" & itm.SKU & "' "

        ExecuteNonQueryFunction(_tempStr)

    End Sub

    Sub UpdateIoCatalogForBillerCatalogInsertDB(itm As ClsPaykii.IOCatalogRes)

        Dim _tempStr As String = "" +
         " Update PAYKII_SKU_CATALOG set " &
         " INSERTEDTIME=INSERTEDTIME , INSERTEDFLAG=0 , " &
         " UPDATEDTIME=sysdate , UPDATEDFLAG=1 , " &
         " DELETEDTIME=DELETEDTIME , DELETEDFLAG=0 " &
            " Where BillerID='" & itm.BillerID & "' and SKU='" & itm.SKU & "' "

        ExecuteNonQueryFunction(_tempStr)

    End Sub

    Sub UpdateIoCatalogForDelete(dt_Data As DataTable)

        Dim _tempStr As String
        For Each dr As DataRow In dt_Data.Rows
            If dr("RowExist") = 0 Then
                _tempStr = "" +
                 "Update PAYKII_IO_CATALOG set " &
                 " INSERTEDTIME=INSERTEDTIME , INSERTEDFLAG=0 , " &
                 " UPDATEDTIME=UPDATEDTIME , UPDATEDFLAG=0 , " &
                 " DELETEDTIME=sysdate , DELETEDFLAG=1 " &
                " Where BillerID='" & DBStr(dr("BillerID").ToString) & "' and SKU='" & DBStr(dr("SKU").ToString) & "' and IOID=" & DBStr(dr("IOID").ToString) & ""

                ExecuteNonQueryFunction(_tempStr)

                _tempStr = "" +
                 " Update PAYKII_SKU_CATALOG set " &
                 " INSERTEDTIME=INSERTEDTIME , INSERTEDFLAG=0 , " &
                 " UPDATEDTIME=sysdate , UPDATEDFLAG=1 , " &
                 " DELETEDTIME=DELETEDTIME , DELETEDFLAG=0 " &
                 " Where BillerID='" & DBStr(dr("BillerID").ToString) & "' and SKU='" & DBStr(dr("SKU").ToString) & "'"

                ExecuteNonQueryFunction(_tempStr)

                WriteToLogFile("Deleted PAYKII_IO_CATALOG " + " Where BillerID='" & DBStr(dr("BillerID").ToString) & "' and SKU='" & DBStr(dr("SKU").ToString) & "' and IOID=" & DBStr(dr("IOID").ToString) & "")

            End If
        Next
    End Sub

    Function PaykiiIOCatalogUpdate(schedulerTime As String) As Boolean

        Try

            WriteToLogFile("PaykiiIOCatalogUpdate Started")

            UpdatePaykiiSchedulerFields("LAST_RUN_TIME", " '" & schedulerTime & "' ", "PaykiiIOCatalogUpdate", False)

            Dim response As IRestResponse = GetPaykiiApiResponse("IOCatalog", ReturnJsonBodyIOCatalogReq())

            Dim objRes = JsonConvert.DeserializeObject(Of List(Of ClsPaykii.IOCatalogRes))(response.Content)

            Dim tempCount As Int16 = 0

            Dim _tempStr As String, _tempNumber As ULong = 0

            Dim dt_Data As New DataTable
            Dim pCheckRowStatus As String

            dt_Data = ReturnDataTable("Select a.*, 0 as RowExist From PAYKII_IO_CATALOG a")

            For Each itm As ClsPaykii.IOCatalogRes In objRes

                pCheckRowStatus = CheckIOCatalogRowStatus(itm)

                For Each dr As DataRow In dt_Data.Rows

                    If DBStr(dr("BillerID")).Equals(DBStr(itm.BillerID)) And DBStr(dr("SKU")).Equals(DBStr(itm.SKU)) And DBStr(dr("IOID")).Equals(DBStr(itm.IOID)) Then
                        dr("RowExist") = 1
                        Exit For
                    End If

                Next

                If (pCheckRowStatus = ROW_UPDATED) Then

                    UpdateIOCatalogDB(itm)

                    UpdateIoCatalogForBillerCatalogUpdateDB(itm)

                    WriteToLogFile("Updated PAYKII_IO_CATALOG " + DBStr(itm.BillerID))

                ElseIf (pCheckRowStatus = ROW_NOT_EXISTS) Then

                    InsertIOCatalogDB(itm)

                    UpdateIoCatalogForBillerCatalogInsertDB(itm)

                    WriteToLogFile("Added PAYKII_IO_CATALOG " + DBStr(itm.BillerID) + " " + DBStr(itm.SKU))

                End If

                _tempNumber += 1

                frmPaykiiIntegrator.txtLog.Text = String.Format("Processed {0} / {1} at {2}", _tempNumber.ToString, objRes.Count.ToString, Format(Now, "dd-MMM-yyyy HH:mm:ss"))
                Application.DoEvents()

            Next

            UpdateIoCatalogForDelete(dt_Data)

            _tempStr = String.Format("Process Completed. JsonItems: {0}  DBItems: {1} at {2}", objRes.Count.ToString, dt_Data.Rows.Count.ToString, Format(Now, "dd-MMM-yyyy HH:mm:ss"))
            WriteToLogFile(_tempStr)

            UpdatePaykiiSchedulerFields("LAST_SUCCESFUL_RUN_TIME", " '" & schedulerTime & "' ", "PaykiiIOCatalogUpdate", False)
            Return True

        Catch ex As Exception
            WriteToLogFile("PaykiiIOCatalogUpdate Ex: " & ex.Message, True)
            UpdatePaykiiSchedulerFields("ERROR_MSG", ex.Message, "PaykiiIOCatalogUpdate", True)
            Return False
        End Try
    End Function

#End Region

#Region "PaykiiDailyFXRatePerBillerType"

    Function CheckDailyFXRatePerBillerTypeRowStatus(itm As ClsPaykii.DailyFXRatePerBillerTypeRes, currencyIdx As Integer, billerTypeIdx As Integer) As String

        Dim _sql As String = "" &
        "Select * From PAYKII_FX_RATE " &
        " Where SETTLEMENTCURRENCY='" & DBStr(itm.SettlementCurrencies(currencyIdx).SettlementCurrency) & "' and " &
         " BILLERTYPE='" & DBStr(itm.SettlementCurrencies(currencyIdx).BillerTypes(billerTypeIdx).BillerType) & "' "

        Dim dt As New DataTable
        dt = ReturnDataTable(_sql)

        If (dt.Rows.Count > 0) Then
            If 1 = 1 AndAlso
                DBStr(dt.Rows(0)("FXDATE")) = DBStr(itm.FXDate) And
                DBStr(dt.Rows(0)("SETTLEMENTCURRENCY")) = DBStr(itm.SettlementCurrencies(currencyIdx).SettlementCurrency) And
                DBStr(dt.Rows(0)("BILLERTYPE")) = DBStr(itm.SettlementCurrencies(currencyIdx).BillerTypes(billerTypeIdx).BillerType) And
                DBStr(dt.Rows(0)("FXRATE")) = DBStr(itm.SettlementCurrencies(currencyIdx).BillerTypes(billerTypeIdx).FXRate) And
                DBStr(dt.Rows(0)("BASECURRENCY")) = DBStr(itm.BaseCurrency) Then

                Return ROW_EXISTS

            Else

                Return ROW_UPDATED

            End If
        Else

            Return ROW_NOT_EXISTS

        End If

    End Function

    Function ReturnJsonBodyFxCatalogReq() As String

        Dim sbRequest As StringBuilder = New StringBuilder("")
        sbRequest.Append("{")
        sbRequest.Append(RetVariableWithinDoubleQuotes("BaseCurrency") + ":" + RetVariableWithinDoubleQuotes(My.Settings.OpCurrencyISO))
        sbRequest.Append(",")
        sbRequest.Append(RetVariableWithinDoubleQuotes("FXDate") + ":" + RetVariableWithinDoubleQuotes(Format(Now, "yyyyMMdd")))
        sbRequest.Append("}")

        Return sbRequest.ToString()

    End Function

    Sub UpdateFxCatalogDB(itm As ClsPaykii.DailyFXRatePerBillerTypeRes, i As Int16, j As Int16)

        Dim _tempStr As String = "" +
         " Update PAYKII_FX_RATE set " &
         " FXDATE='" & DBStr(itm.FXDate) & "', SETTLEMENTCURRENCY='" & DBStr(itm.SettlementCurrencies(i).SettlementCurrency) & "', " &
         " BILLERTYPE='" & DBStr(itm.SettlementCurrencies(i).BillerTypes(j).BillerType) & "', " &
         " FXRATE='" & DBStr(itm.SettlementCurrencies(i).BillerTypes(j).FXRate) & "', BASECURRENCY='" & DBStr(itm.BaseCurrency) & "', " &
         " INSERTEDTIME=INSERTEDTIME , INSERTEDFLAG=0 , " &
         " UPDATEDTIME=sysdate , UPDATEDFLAG=1 , " &
         " DELETEDTIME=DELETEDTIME , DELETEDFLAG=0 " &
         " Where SETTLEMENTCURRENCY='" & DBStr(itm.SettlementCurrencies(i).SettlementCurrency) & "' and " &
         " BILLERTYPE='" & DBStr(itm.SettlementCurrencies(i).BillerTypes(j).BillerType) & "' "

        ExecuteNonQueryFunction(_tempStr)

    End Sub

    Sub InsertFxCatalogDB(itm As ClsPaykii.DailyFXRatePerBillerTypeRes, i As Int16, j As Int16)

        Dim _tempStr As String = "Insert into PAYKII_FX_RATE (" &
        " FXDATE, SETTLEMENTCURRENCY, BILLERTYPE, FXRATE, BASECURRENCY, " &
        " INSERTEDTIME, UPDATEDTIME, DELETEDTIME, " &
        " INSERTEDFLAG, UPDATEDFLAG, DELETEDFLAG " &
        " ) Values ( " &
        "'" & DBStr(itm.FXDate) & "','" & DBStr(itm.SettlementCurrencies(i).SettlementCurrency) & "','" & DBStr(itm.SettlementCurrencies(i).BillerTypes(j).BillerType) & "'," &
        "'" & DBStr(itm.SettlementCurrencies(i).BillerTypes(j).FXRate) & "','" & DBStr(itm.BaseCurrency) & "'," &
        " sysdate, to_date('2000-01-01','yyyy-mm-dd'), to_date('2000-01-01','yyyy-mm-dd'), " &
        " 1,0,0 " &
        ")"

        ExecuteNonQueryFunction(_tempStr)

    End Sub

    Function PaykiiDailyFXRatePerBillerTypeUpdate(schedulerTime As String) As Boolean

        Try

            WriteToLogFile("PaykiiFXRateUpdate Started")

            UpdatePaykiiSchedulerFields("LAST_RUN_TIME", " '" & schedulerTime & "' ", "PaykiiFXRateUpdate", False)

            Dim response As IRestResponse = GetPaykiiApiResponse("DailyFXRatePerBillerType", ReturnJsonBodyFxCatalogReq())

            Dim objRes = JsonConvert.DeserializeObject(Of List(Of ClsPaykii.DailyFXRatePerBillerTypeRes))("[ " + response.Content + " ]")

            WriteToLogFile("FxRate: " + vbNewLine + response.Content)

            Dim tempCount As Int16 = 0

            Dim _tempStr As String, _tempNumber As ULong = 0

            Dim dt_Data As New DataTable
            Dim pCheckRowStatus As String

            dt_Data = ReturnDataTable("Select a.*, 0 as RowExist From PAYKII_FX_RATE a")


            For Each itm As ClsPaykii.DailyFXRatePerBillerTypeRes In objRes

                For i As Int16 = 0 To itm.SettlementCurrencies.Count - 1

                    For j As Int16 = 0 To itm.SettlementCurrencies(i).BillerTypes.Count - 1

                        pCheckRowStatus = CheckDailyFXRatePerBillerTypeRowStatus(itm, i, j)

                        For Each dr As DataRow In dt_Data.Rows

                            If dr("SETTLEMENTCURRENCY").Equals(DBStr(itm.SettlementCurrencies(i).SettlementCurrency)) And dr("BILLERTYPE").Equals(DBStr(itm.SettlementCurrencies(i).BillerTypes(j).BillerType)) Then
                                dr("RowExist") = 1
                                Exit For
                            End If

                        Next

                        If (pCheckRowStatus = ROW_UPDATED) Then

                            UpdateFxCatalogDB(itm, i, j)

                            WriteToLogFile("Updated Paykii FxRate " + DBStr(itm.SettlementCurrencies(i).SettlementCurrency) + " " + DBStr(itm.SettlementCurrencies(i).BillerTypes(j).BillerType))

                        ElseIf (pCheckRowStatus = ROW_NOT_EXISTS) Then

                            InsertFxCatalogDB(itm, i, j)

                            WriteToLogFile("Added Paykii FxRate " + DBStr(itm.SettlementCurrencies(i).SettlementCurrency) + " " + DBStr(itm.SettlementCurrencies(i).BillerTypes(j).BillerType))

                        End If

                        _tempNumber += 1

                        frmPaykiiIntegrator.txtLog.Text = String.Format("Processed {0} at {1}", _tempNumber.ToString, Format(Now, "dd-MMM-yyyy HH:mm:ss"))
                        Application.DoEvents()

                    Next

                Next

            Next

            _tempStr = String.Format("Process Completed. JsonItems: {0}  DBItems: {1} at {2}", objRes.Count.ToString, dt_Data.Rows.Count.ToString, Format(Now, "dd-MMM-yyyy HH:mm:ss"))
            WriteToLogFile(_tempStr)

            UpdatePaykiiSchedulerFields("LAST_SUCCESFUL_RUN_TIME", " '" & schedulerTime & "' ", "PaykiiFXRateUpdate", False)
            Return True

        Catch ex As Exception
            WriteToLogFile("Paykii FxRate Ex: " & ex.Message, True)
            UpdatePaykiiSchedulerFields("ERROR_MSG", ex.Message, "PaykiiFXRateUpdate", True)
            Return False
        End Try
    End Function

#End Region

#Region "PaykiiDailyFXRatePerBillerType_old"

    'Function PaykiiFXRateUpdate(schedulerTime As String) As Boolean

    '    Try

    '        WriteToLogFile("PaykiiFXRateUpdate Started")

    '        UpdatePaykiiSchedulerFields("LAST_RUN_TIME", " '" & schedulerTime & "' ", "FXRateUpdate", False)

    '        Dim sbRequest As StringBuilder = New StringBuilder("")
    '        sbRequest.Append("{")
    '        sbRequest.Append(RetVariableWithinDoubleQuotes("BaseCurrency") + ":" + RetVariableWithinDoubleQuotes(My.Settings.OpCurrencyISO))
    '        sbRequest.Append(",")
    '        sbRequest.Append(RetVariableWithinDoubleQuotes("FXDate") + ":" + RetVariableWithinDoubleQuotes(Format(Now, "yyyyMMdd")))
    '        sbRequest.Append("}")

    '        Dim response As IRestResponse = GetPaykiiApiResponse("DailyFXRatePerBillerType", sbRequest.ToString())

    '        'Dim objRes = JsonConvert.DeserializeObject(Of List(Of ClsPaykii.DailyFXRatePerBillerTypeRes))(response.Content)

    '        'Dim sr As New StreamReader("C:\Temp\FxRates.txt")

    '        'Dim _tempFxRates As String = sr.ReadToEnd

    '        'MsgBox(_tempFxRates)

    '        Dim objRes = JsonConvert.DeserializeObject(Of List(Of ClsPaykii.DailyFXRatePerBillerTypeRes))("[ " + response.Content + " ]")

    '        Dim tempCount As Int16 = 0

    '        Dim _tempStr As String, _tempNumber As ULong = 0

    '        'Dim dt_Data As New DataTable
    '        'Dim pExist As UInt16 = 0, pUpdated As UInt16 = 0, pAdd As UInt16 = 0

    '        'dt_Data = ReturnDataTable("Select a.*, 0 as RowExist From PAYKII_FX_RATE a")

    '        For Each itm As ClsPaykii.DailyFXRatePerBillerTypeRes In objRes

    '            For i As Int16 = 0 To itm.SettlementCurrencies.Count - 1

    '                For j As Int16 = 0 To itm.SettlementCurrencies(i).BillerTypes.Count - 1

    '                    _tempStr = "" +
    '                     " Delete from PAYKII_FX_RATE " &
    '                     " Where SETTLEMENTCURRENCY='" & DBStr(itm.SettlementCurrencies(i).SettlementCurrency) &
    '                     "' and BILLERTYPE='" & DBStr(itm.SettlementCurrencies(i).BillerTypes(j).BillerType) & "' "

    '                    ExecuteNonQueryFunction(_tempStr)

    '                    _tempStr = "Insert into PAYKII_FX_RATE (" &
    '" FXDATE, SETTLEMENTCURRENCY, BILLERTYPE, FXRATE, BASECURRENCY, " &
    '" INSERTEDTIME, UPDATEDTIME, DELETEDTIME, " &
    '" INSERTEDFLAG, UPDATEDFLAG, DELETEDFLAG " &
    '" ) Values ( " &
    '"'" & DBStr(itm.FXDate) & "','" & DBStr(itm.SettlementCurrencies(i).SettlementCurrency) & "','" & DBStr(itm.SettlementCurrencies(i).BillerTypes(j).BillerType) & "'," &
    '"'" & DBStr(itm.SettlementCurrencies(i).BillerTypes(j).FXRate) & "','" & DBStr(itm.BaseCurrency) & "'," &
    '" sysdate, to_date('2000-01-01','yyyy-mm-dd'), to_date('2000-01-01','yyyy-mm-dd'), " &
    '" 1,0,0 " &
    '")"

    '                    ExecuteNonQueryFunction(_tempStr)

    '                    _tempStr = "" +
    '                     " Update PAYKII_FX_RATE set " &
    '                     " INSERTEDTIME=INSERTEDTIME , INSERTEDFLAG=0 , " &
    '                     " UPDATEDTIME=sysdate , UPDATEDFLAG=1 , " &
    '                     " DELETEDTIME=DELETEDTIME , DELETEDFLAG=0 " &
    '                     " Where SETTLEMENTCURRENCY='" & DBStr(itm.SettlementCurrencies(i).SettlementCurrency) & "' and BILLERTYPE='" & DBStr(itm.SettlementCurrencies(i).BillerTypes(j).BillerType) & "' "

    '                    ExecuteNonQueryFunction(_tempStr)

    '                    WriteToLogFile("Added PAYKII_FX_RATE " + DBStr(itm.SettlementCurrencies(i).SettlementCurrency) + " " + DBStr(itm.SettlementCurrencies(i).BillerTypes(j).BillerType))

    '                Next

    '            Next

    '            _tempNumber += 1

    '            frmPaykiiIntegrator.txtLog.Text = String.Format("Processed {0} / {1} at {2}", _tempNumber.ToString, objRes.Count.ToString, Format(Now, "dd-MMM-yyyy HH:mm:ss"))
    '            Application.DoEvents()

    '        Next

    '        _tempStr = String.Format("Process Completed. JsonItems: {0} {1}", objRes.Count.ToString, Format(Now, "dd-MMM-yyyy HH:mm:ss"))
    '        WriteToLogFile(_tempStr)

    '        UpdatePaykiiSchedulerFields("LAST_SUCCESFUL_RUN_TIME", " '" & schedulerTime & "' ", "FXRateUpdate", False)
    '        Return True

    '    Catch ex As Exception
    '        WriteToLogFile("PaykiiFXRateUpdate Ex: " & ex.Message, True)
    '        UpdatePaykiiSchedulerFields("ERROR_MSG", ex.Message, "FXRateUpdate", True)
    '        Return False
    '    End Try
    'End Function

#End Region



    Function UpdatePaykiiSchedulerFields(fieldName As String, fieldValue As String, apiName As String, useSingleQuotesForFieldValue As Boolean) As Boolean
        Dim _str As String = "" +
            "Update PAYKII_SCHEDULER_CONFIG set " + fieldName + " = " &
            IIf(useSingleQuotesForFieldValue, "'" & fieldValue & "'", "" & fieldValue & "") & " Where API_NAME= '" & apiName & "'"
        ExecuteNonQueryFunction(_str)
        Return True
    End Function

    Private Function RetVariableWithinDoubleQuotes(pStr As String) As String
        Return Chr(34) + pStr + Chr(34)
    End Function

    ''' <summary>
    ''' 'Values to string Null as N/A for Paykii
    ''' </summary>
    ''' <param name="_str"></param>
    ''' <returns></returns>
    Function DBStr(_str As Object) As String
        If _str Is Nothing OrElse IsDBNull(_str) Then
            _str = ""
            Return _str
        End If
        _str = CStr(_str)
        _str = _str.Replace("'", "''")
        Return _str
    End Function

    Private Sub WritePaykiiResponseMsg(paramErrCode As String)

        Dim msg As String = ""
        Dim desc As String = ""

        Select Case paramErrCode

            Case "00"
                msg = "Process complete"
                desc = "The transaction sent to Paykii's API was successfully processed"
            Case "01"
                msg = "Authentication error"
                desc = "The Token used in the request is invalid"
            Case "02"
                msg = "Insufficient funds"
                desc = "The ENTITY has insufficient funds available to cover the payment sent"
            Case "03"
                msg = "Invalid biller"
                desc = "The Biller sent in the BillerID field does not exist in Paykii's current catalog"
            Case "04"
                msg = "Biller unavailable"
                desc = "The Biller was unable to process the request sent"
            Case "05"
                msg = "Request timeout"
                desc = "The Biller did not respond to the transaction request"
            Case "06"
                msg = "Invalid SKU"
                desc = "The SKU sent in the request does not exist in Paykii's current catalog"
            Case "07"
                msg = "Invalid reference"
                desc = "The Inputs sent in the request are incorrect"
            Case "08"
                msg = "Invalid amount"
                desc = "The Amount sent in the payment request is incorrect"
            Case "09"
                msg = "Duplicate transaction"
                desc = "The payment sent is seen as a duplicate transaction by Paykii or the Biller"
            Case "10"
                msg = "Transaction in progress"
                desc = "The payment is still in progress of being posted to the Biller, and once the process is complete, it will be updated to reflect its final status"
            Case "11"
                msg = "Inquiry not available for SKU"
                desc = "The Biller has no inquiry capability for the SKU sent"
            Case "12"
                msg = "Past due payment not available for SKU"
                desc = "The Biller does not accept payments past the due date for this SKU"
            Case "13"
                msg = "Sender information incomplete"
                desc = "The Sender's information on the payment request is incomplete"
            Case "14"
                msg = "Error processing transaction"
                desc = "Paykii was unable to process the transaction sent"
            Case "15"
                msg = "Transaction not found"
                desc = "The transaction inquired through the VerifyPaymentStatus endpoint was not found in Paykii's records"
            Case "16"
                msg = "Payments for reference exceeded"
                desc = "The Biller reports that the Account for which the payment was sent has received the maximum amount of payments for the day"
            Case "17"
                msg = "Account status invalid"
                desc = "The Biller reports that the Inputs sent are linked to an account that is Inactive or Blocked and no payments can be posted to it through Paykii's API"
            Case "18"
                msg = "Amount out of allowed range"
                desc = "The Amount of the payment sent is lower than the Minimum or higher than the Maximum amount allowed by the Biller for this SKU"
            Case "19"
                msg = "Data not found"
                desc = "The search criteria used in the request returned no records"
            Case "20"
                msg = "Request format invalid"
                desc = "The request sent to Paykii's API does not comply with the structure and content expected"
            Case Else
                msg = "Unknown"
                desc = "Unknown"
        End Select

        WriteToLogFile(String.Format("Paykii Response Code :{0}. Message: {1}. Description{2}.", paramErrCode, msg, desc), True)

    End Sub

End Module
