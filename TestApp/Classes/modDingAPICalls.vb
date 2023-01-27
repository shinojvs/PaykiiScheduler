
Imports Newtonsoft.Json
Imports RestSharp
Imports System
Imports System.IO
Imports System.Text
Imports System.Web.Script.Serialization

Module modDingAPICalls

    Private _url As String = My.Settings.URL
    Private _userName As String = My.Settings.UserName
    Private _password As String = My.Settings.Password

    Function UpdateDingSchedulerFields(fieldName As String, fieldValue As String, apiName As String, useSingleQuotesForFieldValue As Boolean) As Boolean
        Dim _str As String = "" +
            "Update DING_SCHEDULER_CONFIG set " + fieldName + " = " &
            IIf(useSingleQuotesForFieldValue, "'" & fieldValue & "'", "" & fieldValue & "") & " Where API_NAME= '" & apiName & "'"
        ExecuteNonQueryFunction(_str)
        Return True
    End Function
    Private Function GetDingAPIResponse(paramApiName As String) As IRestResponse

        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 Or System.Net.SecurityProtocolType.Tls11 Or System.Net.SecurityProtocolType.Tls
        Dim client = New RestClient(_url + paramApiName)
        client.Timeout = 60 * 1000
        client.ClearHandlers()

        Dim request = New RestRequest(Method.GET)
        Dim enCodedCredentials As String = Convert.ToBase64String(Encoding.ASCII.GetBytes(_userName + ":" + _password))
        request.AddHeader("Authorization", "Basic " + enCodedCredentials)
        request.AddHeader("Accept", "application/json")
        Dim response As IRestResponse = client.Execute(request)
        Return response

    End Function

    Function DingGetCountries() As Boolean
        Try
            Dim response As IRestResponse = GetDingAPIResponse("GetCountries")

            Dim objRes As clsDing.GetCountryRes
            objRes = JsonConvert.DeserializeObject(Of clsDing.GetCountryRes)(response.Content)

            Dim regionCodes As String = ""

            For Each itm As clsDing.CountryItems In objRes.items
                regionCodes = ""
                For i As Int16 = 0 To itm.regionCodes.Count - 1
                    regionCodes = regionCodes + itm.regionCodes(i)
                    If i <> itm.regionCodes.Count - 1 Then regionCodes = regionCodes + ","
                Next

                For Each intnlInfo As clsDing.InternationalDialingInformation In itm.internationalDialingInformation
                    ExecuteNonQueryFunction("Delete from  Ding_Countries where countryiso='" & itm.countryIso & "' and prefix='" & intnlInfo.prefix & "'")

                    ExecuteNonQueryFunction("Insert into Ding_Countries(countryiso, countryname, prefix," &
                " minimumlength, maximumlength, regioncodes) values('" & itm.countryIso & "','" & itm.countryName & "','" & intnlInfo.prefix & "','" & intnlInfo.minimumLength & "','" & intnlInfo.maximumLength & "','" & regionCodes & "')")
                Next

            Next

            Return True

        Catch ex As Exception
            WriteToLogFile("Get Countries Ex: " & ex.Message, True)
            Return False
        End Try
    End Function

    Function DingGetCurrencies() As Boolean
        Try
            Dim response As IRestResponse = GetDingAPIResponse("GetCurrencies")

            Dim objRes = JsonConvert.DeserializeObject(Of clsDing.GetCurrencyRes)(response.Content)

            For Each itm As clsDing.CurrencyItems In objRes.items

                ExecuteNonQueryFunction("Delete from  DING_CURRENCIES where currencyiso='" & itm.currencyIso.Replace("'", "''") & "'")

                ExecuteNonQueryFunction("Insert into DING_CURRENCIES(currencyiso, currencyname) values('" & itm.currencyIso.Replace("'", "''") & "','" & itm.currencyName.Replace("'", "''") & "')")

            Next

            Return True

        Catch ex As Exception
            WriteToLogFile("DingGetCurrencies Ex: " & ex.Message, True)

            Return False
        End Try
    End Function

#Region "DingRegionUpdate"
    Function CheckRegionExists(dr As DataRow, itm As clsDing.RegionsItems) As Boolean

        If 1 = 1 AndAlso
        DBStr(dr("regionCode")) = DBStr(itm.regionCode) AndAlso
        DBStr(dr("regionName")) = DBStr(itm.regionName) AndAlso
        DBStr(dr("countryIso")) = DBStr(itm.countryIso) Then

            Return True
        Else

            Return False

        End If

    End Function
    Function DingRegionUpdate(schedulerTime As String) As Boolean

        Try

            WriteToLogFile("DingRegionUpdate Started")

            UpdateDingSchedulerFields("LAST_RUN_TIME", " '" & schedulerTime & "' ", "RegionUpdate", False)

            Dim response As IRestResponse = GetDingAPIResponse("GetRegions")

            Dim objRes = JsonConvert.DeserializeObject(Of clsDing.GetRegionsRes)(response.Content)

            'Dim sr As New StreamReader(Application.StartupPath() + "\Files\GetProductDescSampleRes.txt")
            'Dim response = sr.ReadToEnd()
            'Dim objRes = JsonConvert.DeserializeObject(Of clsDing.GetProductDescriptionsRes)(response) 'shinoj note
            'GetProductsSampleRes


            Dim tempCount As Int16 = 0

            Dim _tempStr As String, _tempNumber As ULong = 0

            Dim dt_Data As New DataTable
            Dim pExist As UInt16 = 0, pUpdated As UInt16 = 0, pAdd As UInt16 = 0

            dt_Data = ReturnDataTable("Select a.*, 0 as RowExist From ding_regions a")

            For Each itm As clsDing.RegionsItems In objRes.items

                pExist = 0
                pUpdated = 0
                pAdd = 0

                For Each dr As DataRow In dt_Data.Rows

                    If (CheckRegionExists(dr, itm)) = True Then
                        dr("RowExist") = 1
                        pExist = 1
                        pUpdated = 0
                        Exit For
                    ElseIf dr("regioncode").Equals(DBStr(itm.regionCode)) Then
                        dr("RowExist") = 1
                        pExist = 1
                        pUpdated = 1
                        Exit For
                    End If

                Next

                If (pUpdated = 1) Then
                    _tempStr = "" +
                     "Update ding_regions set regionname='" & DBStr(itm.regionName) & "', countryiso='" & DBStr(itm.countryIso) & "' " &
                    " Where regioncode='" & DBStr(itm.regionCode) & "'"

                    ExecuteNonQueryFunction(_tempStr)

                    WriteToLogFile("Updated ding_regions " + DBStr(itm.regionCode))

                ElseIf pExist = 0 Then

                    _tempStr = "Insert into ding_regions(regioncode, regionname,countryiso)" &
                " values('" & DBStr(itm.regionCode) & "','" & DBStr(itm.regionName) & "','" & DBStr(itm.countryIso) & "')"

                    ExecuteNonQueryFunction(_tempStr)

                    WriteToLogFile("Added ding_regions " + DBStr(itm.regionCode))

                End If

                _tempNumber += 1

                _tempStr = String.Format("Processed {0} / {1} at {2}", _tempNumber.ToString, objRes.items.Count.ToString, Format(Now, "dd-MMM-yyyy HH:mm:ss"))
                frmDingIntegrator.txtLog.Text = _tempStr

                Application.DoEvents()

            Next

            'For Each dr As DataRow In dt_Data.Rows
            '    If dr("RowExist") = 0 Then
            '        ExecuteNonQueryFunction("Delete from ding_regions Where regioncode='" & dr("regioncode") & "'")
            '        WriteToLogFile("Deleted ding_regions " + dr("regioncode"))
            '    End If
            'Next

            _tempStr = String.Format("Process Completed. JsonItems: {0}  DBItems: {1} at {2}", objRes.items.Count.ToString, dt_Data.Rows.Count.ToString, Format(Now, "dd-MMM-yyyy HH:mm:ss"))
            WriteToLogFile(_tempStr)

            UpdateDingSchedulerFields("LAST_SUCCESFUL_RUN_TIME", " '" & schedulerTime & "' ", "RegionUpdate", False)
            Return True

        Catch ex As Exception
            WriteToLogFile("DingRegionUpdate Ex: " & ex.Message, True)
            UpdateDingSchedulerFields("ERROR_MSG", ex.Message, "RegionUpdate", True)
            Return False
        End Try
    End Function

#End Region

#Region "DingProviderUpdate"
    Function CheckProviderExists(dr As DataRow, itm As clsDing.ProviderItems, regionCodes As String, paymentTypes As String) As Boolean

        If 1 = 1 AndAlso
        DBStr(dr("providercode")) = DBStr(itm.providerCode) AndAlso
        DBStr(dr("countryIso")) = DBStr(itm.countryIso) AndAlso
        DBStr(dr("name")) = DBStr(itm.name) AndAlso
        DBStr(dr("shortName")) = DBStr(itm.shortName) AndAlso
        DBStr(dr("validationRegex")) = DBStr(itm.validationRegex) AndAlso
        DBStr(dr("customerCareNumber")) = DBStr(itm.customerCareNumber) AndAlso
        DBStr(dr("regionCodes")) = DBStr(regionCodes) AndAlso
        DBStr(dr("paymentTypes")) = DBStr(paymentTypes) AndAlso
        DBStr(dr("logoUrl")) = DBStr(itm.logoUrl) Then

            Return True
        Else

            Return False

        End If

    End Function
    Function DingProviderUpdate(schedulerTime As String) As Boolean

        Try

            WriteToLogFile("DingProviderUpdate Started")

            UpdateDingSchedulerFields("LAST_RUN_TIME", " '" & schedulerTime & "' ", "ProviderUpdate", False)

            Dim response As IRestResponse = GetDingAPIResponse("GetProviders")

            Dim objRes = JsonConvert.DeserializeObject(Of clsDing.GetProvidersRes)(response.Content)

            'Dim sr As New StreamReader(Application.StartupPath() + "\Files\GetProductDescSampleRes.txt")
            'Dim response = sr.ReadToEnd()
            'Dim objRes = JsonConvert.DeserializeObject(Of clsDing.GetProductDescriptionsRes)(response) 'shinoj note
            'GetProductsSampleRes


            Dim tempCount As Int16 = 0

            Dim _tempStr As String, _tempNumber As ULong = 0
            Dim regionCodes As String, paymentTypes As String

            Dim dt_Data As New DataTable
            Dim pExist As UInt16 = 0, pUpdated As UInt16 = 0, pAdd As UInt16 = 0

            dt_Data = ReturnDataTable("Select a.*, 0 as RowExist From ding_providers a")

            For Each itm As clsDing.ProviderItems In objRes.items

                pExist = 0
                pUpdated = 0
                pAdd = 0

                regionCodes = ""
                For i As Int16 = 0 To itm.regionCodes.Count - 1
                    regionCodes = regionCodes + itm.regionCodes(i)
                    If i <> itm.regionCodes.Count - 1 Then regionCodes = regionCodes + ","
                Next

                paymentTypes = ""
                For i As Int16 = 0 To itm.paymentTypes.Count - 1
                    paymentTypes = paymentTypes + itm.paymentTypes(i)
                    If i <> itm.paymentTypes.Count - 1 Then paymentTypes = paymentTypes + ","
                Next

                For Each dr As DataRow In dt_Data.Rows

                    If (CheckProviderExists(dr, itm, regionCodes, paymentTypes)) = True Then
                        dr("RowExist") = 1
                        pExist = 1
                        pUpdated = 0
                        Exit For
                    ElseIf dr("providercode").Equals(DBStr(itm.providerCode)) Then
                        dr("RowExist") = 1
                        pExist = 1
                        pUpdated = 1
                        Exit For
                    End If

                Next

                If (pUpdated = 1) Then
                    _tempStr = "" +
                     "Update ding_providers set " &
                     " countryIso='" & DBStr(itm.countryIso) & "', " &
                     " name='" & DBStr(itm.name) & "', shortName='" & DBStr(itm.shortName) & "', " &
                     " validationRegex='" & DBStr(itm.validationRegex) & "', customerCareNumber='" & DBStr(itm.customerCareNumber) & "', " &
                     " regionCodes='" & DBStr(regionCodes) & "', paymentTypes='" & DBStr(paymentTypes) & "', " &
                     " logoUrl='" & DBStr(itm.logoUrl) & "' " &
                    " Where providercode='" & DBStr(itm.providerCode) & "'"

                    ExecuteNonQueryFunction(_tempStr)

                    WriteToLogFile("Updated ding_providers " + DBStr(itm.providerCode))

                ElseIf pExist = 0 Then

                    _tempStr = "Insert into ding_providers (" &
                        " providerCode, countryIso, name, shortName, validationRegex, customerCareNumber, " &
                        " regionCodes, paymentTypes, logoUrl " &
                        " ) Values ( " &
                        "'" & DBStr(itm.providerCode) & "','" & DBStr(itm.countryIso) & "','" & DBStr(itm.name) & "'," &
                        "'" & DBStr(itm.shortName) & "','" & DBStr(itm.validationRegex) & "','" & DBStr(itm.customerCareNumber) & "'," &
                        "'" & DBStr(regionCodes) & "','" & DBStr(paymentTypes) & "','" & DBStr(itm.logoUrl) & "'" &
                        ")"

                    ExecuteNonQueryFunction(_tempStr)

                    WriteToLogFile("Added ding_providers " + DBStr(itm.providerCode))

                End If

                _tempNumber += 1

                frmDingIntegrator.txtLog.Text = String.Format("Processed {0} / {1} at {2}", _tempNumber.ToString, objRes.items.Count.ToString, Format(Now, "dd-MMM-yyyy HH:mm:ss"))
                Application.DoEvents()

            Next

            'For Each dr As DataRow In dt_Data.Rows
            '    If dr("RowExist") = 0 Then
            '        _tempStr = "Delete from ding_providers Where providercode='" & dr("providercode") & "'"
            '        ExecuteNonQueryFunction(_tempStr)

            '        WriteToLogFile("Deleted ding_providers " + dr("providercode"))
            '    End If
            'Next

            _tempStr = String.Format("Process Completed. JsonItems: {0}  DBItems: {1} at {2}", objRes.items.Count.ToString, dt_Data.Rows.Count.ToString, Format(Now, "dd-MMM-yyyy HH:mm:ss"))
            WriteToLogFile(_tempStr)

            UpdateDingSchedulerFields("LAST_SUCCESFUL_RUN_TIME", " '" & schedulerTime & "' ", "ProviderUpdate", False)
            Return True

        Catch ex As Exception
            WriteToLogFile("DingProviderUpdate Ex: " & ex.Message, True)
            UpdateDingSchedulerFields("ERROR_MSG", ex.Message, "ProviderUpdate", True)
            Return False
        End Try
    End Function

#End Region

#Region "DingProviderStatusUpdate"
    Function CheckProviderStatusExists(dr As DataRow, itm As clsDing.ProviderStatusItems) As Boolean

        If 1 = 1 AndAlso
        DBStr(dr("providercode")) = DBStr(itm.providercode) AndAlso
        DBStr(dr("isprocessingtransfers")) = DBStr(itm.isprocessingtransfers) AndAlso
            DBStr(dr("message")) = DBStr(itm.message) Then

            Return True
        Else

            Return False

        End If

    End Function
    Function DingProviderStatusUpdate(schedulerTime As String) As Boolean

        Try

            WriteToLogFile("DingProviderStatusUpdate Started")

            UpdateDingSchedulerFields("LAST_RUN_TIME", " '" & schedulerTime & "' ", "ProviderStatusUpdate", False)

            Dim response As IRestResponse = GetDingAPIResponse("GetProviderStatus")

            Dim objRes = JsonConvert.DeserializeObject(Of clsDing.GetProviderStatusRes)(response.Content)

            'Dim sr As New StreamReader(Application.StartupPath() + "\Files\GetProductDescSampleRes.txt")
            'Dim response = sr.ReadToEnd()
            'Dim objRes = JsonConvert.DeserializeObject(Of clsDing.GetProductDescriptionsRes)(response) 'shinoj note
            'GetProductsSampleRes


            Dim tempCount As Int16 = 0

            Dim _tempStr As String, _tempNumber As ULong = 0

            Dim dt_Data As New DataTable
            Dim pExist As UInt16 = 0, pUpdated As UInt16 = 0, pAdd As UInt16 = 0

            dt_Data = ReturnDataTable("Select a.*, 0 as RowExist From ding_provider_status a")

            For Each itm As clsDing.ProviderStatusItems In objRes.items

                pExist = 0
                pUpdated = 0
                pAdd = 0

                For Each dr As DataRow In dt_Data.Rows

                    If (CheckProviderStatusExists(dr, itm)) = True Then
                        dr("RowExist") = 1
                        pExist = 1
                        pUpdated = 0
                        Exit For
                    ElseIf dr("providercode").Equals(DBStr(itm.providercode)) Then
                        dr("RowExist") = 1
                        pExist = 1
                        pUpdated = 1
                        Exit For
                    End If

                Next

                If (pUpdated = 1) Then
                    _tempStr = "" +
                     "Update ding_provider_status set isprocessingtransfers='" & DBStr(itm.isprocessingtransfers) & "', message='" & DBStr(itm.message) & "' " &
                    " Where providercode='" & DBStr(itm.providercode) & "'"

                    ExecuteNonQueryFunction(_tempStr)

                    WriteToLogFile("Updated ding_provider_status " + DBStr(itm.providercode))

                ElseIf pExist = 0 Then

                    _tempStr = "Insert into ding_provider_status(providercode, isprocessingtransfers, message)" &
                " values('" & DBStr(itm.providercode) & "','" & DBStr(itm.isprocessingtransfers) & "','" & DBStr(itm.message) & "')"

                    ExecuteNonQueryFunction(_tempStr)

                    WriteToLogFile("Added ding_provider_status " + DBStr(itm.providercode))

                End If

                _tempNumber += 1

                frmDingIntegrator.txtLog.Text = String.Format("Processed {0} / {1} at {2}", _tempNumber.ToString, objRes.items.Count.ToString, Format(Now, "dd-MMM-yyyy HH:mm:ss"))
                Application.DoEvents()

            Next

            For Each dr As DataRow In dt_Data.Rows
                If dr("RowExist") = 0 Then
                    ExecuteNonQueryFunction("Delete from ding_provider_status Where providercode='" & dr("providercode") & "'")
                    WriteToLogFile("Deleted ding_provider_status " + dr("providercode"))
                End If
            Next

            _tempStr = String.Format("Process Completed. JsonItems: {0}  DBItems: {1} at {2}", objRes.items.Count.ToString, dt_Data.Rows.Count.ToString, Format(Now, "dd-MMM-yyyy HH:mm:ss"))
            WriteToLogFile(_tempStr)

            UpdateDingSchedulerFields("LAST_SUCCESFUL_RUN_TIME", " '" & schedulerTime & "' ", "ProviderStatusUpdate", False)
            Return True

        Catch ex As Exception
            WriteToLogFile("DingProviderStatusUpdate Ex: " & ex.Message, True)
            UpdateDingSchedulerFields("ERROR_MSG", ex.Message, "ProviderStatusUpdate", True)
            Return False
        End Try
    End Function

#End Region

#Region "DingProductUpdate"
    Function CheckProductExists(dr As DataRow, itm As clsDing.ProductsItems, benefits As String, paymentTypes As String) As Boolean

        If 1 = 1 AndAlso
        DBStr(dr("providerCode")) = DBStr(itm.providerCode) AndAlso
        DBStr(dr("skuCode")) = DBStr(itm.skuCode) AndAlso
        DBStr(dr("localizationKey")) = DBStr(itm.localizationKey) AndAlso
        DBStr(dr("maxcustomerFee")) = DBStr(itm.maximum.customerFee) AndAlso
        DBStr(dr("maxdistributorFee")) = DBStr(itm.maximum.distributorFee) AndAlso
        DBStr(dr("maxreceiveValue")) = DBStr(itm.maximum.receiveValue) AndAlso
        DBStr(dr("maxreceiveCurrencyIso")) = DBStr(itm.maximum.receiveCurrencyIso) AndAlso
        DBStr(dr("maxreceiveValueExcludingTax")) = DBStr(itm.maximum.receiveValueExcludingTax) AndAlso
        DBStr(dr("maxtaxRate")) = DBStr(itm.maximum.taxRate) AndAlso
        DBStr(dr("maxtaxName")) = DBStr(itm.maximum.taxName) AndAlso
        DBStr(dr("maxtaxCalculation")) = DBStr(itm.maximum.taxCalculation) AndAlso
        DBStr(dr("maxsendValue")) = DBStr(itm.maximum.sendValue) AndAlso
        DBStr(dr("maxsendCurrencyIso")) = DBStr(itm.maximum.sendCurrencyIso) AndAlso
        DBStr(dr("mincustomerFee")) = DBStr(itm.minimum.customerFee) AndAlso
        DBStr(dr("mindistributorFee")) = DBStr(itm.minimum.distributorFee) AndAlso
        DBStr(dr("minreceiveValue")) = DBStr(itm.minimum.receiveValue) AndAlso
        DBStr(dr("minreceiveCurrencyIso")) = DBStr(itm.minimum.receiveCurrencyIso) AndAlso
        DBStr(dr("minreceiveValueExcludingTax")) = DBStr(itm.minimum.receiveValueExcludingTax) AndAlso
        DBStr(dr("mintaxRate")) = DBStr(itm.minimum.taxRate) AndAlso
        DBStr(dr("mintaxName")) = DBStr(itm.minimum.taxName) AndAlso
        DBStr(dr("mintaxCalculation")) = DBStr(itm.minimum.taxCalculation) AndAlso
        DBStr(dr("minsendValue")) = DBStr(itm.minimum.sendValue) AndAlso
        DBStr(dr("minsendCurrencyIso")) = DBStr(itm.minimum.sendCurrencyIso) AndAlso
        DBStr(dr("commissionRate")) = DBStr(itm.commissionRate) AndAlso
        DBStr(dr("processingMode")) = DBStr(itm.processingMode) AndAlso
        DBStr(dr("redemptionMechanism")) = DBStr(itm.redemptionMechanism) AndAlso
        DBStr(dr("benefits")) = DBStr(benefits) AndAlso
        DBStr(dr("validityPeriodIso")) = DBStr(itm.validityPeriodIso) AndAlso
        DBStr(dr("additionalInformation")) = DBStr(itm.additionalInformation) AndAlso
        DBStr(dr("defaultDisplayText")) = DBStr(itm.defaultDisplayText) AndAlso
        DBStr(dr("paymentTypes")) = DBStr(paymentTypes) AndAlso
        DBStr(dr("lookupBillsRequired")) = DBStr(itm.lookupBillsRequired) Then

            Return True
        Else

            Return False

        End If

    End Function

    Function UpdateProduct2nd(isNewProduct As Boolean, skuCode As String) As Boolean
        Dim _tempStr = "" +
    " Update DING_PRODUCTS set DENOMINATIONTYPE=case when minsendvalue=maxsendvalue then 'Fixed' else 'Range' end, " +
    " CONVERSIONRATE=case when minsendvalue=maxsendvalue  then  null else " +
    " case when to_number(  minreceivevalue )/ to_number( minsendvalue ) < to_number( maxreceivevalue ) / to_number( maxsendvalue )  " +
    " then to_char( round( (to_number( minreceivevalue )/ to_number( minsendvalue ))/" & My.Settings.UsdConverionRate & ", 2)) else to_char( round( (to_number( maxreceivevalue )/ to_number( maxsendvalue ))/" & My.Settings.UsdConverionRate & ", 2)) end " +
    " end, " +
   " OPAMOUNT=case when minsendvalue=maxsendvalue then to_char(ceil(to_number(  minsendvalue )*" & My.Settings.UsdConverionRate & ")) else null end, " +
    " OPCURRENCYISO='" & My.Settings.OpCurrencyISO & "'," &
    IIf(isNewProduct, " STATUSCODE='1',", "") +
    " TRANSFERTYPE=(select b.transfertype from ding_products a inner join ding_recharge_types b on a.redemptionmechanism=b.redemptionmechanism and a.benefits=b.benefits where a.SKUCODE='" & DBStr(skuCode) & "')," +
    " USDCONVRATE='" & My.Settings.UsdConverionRate & "', " +
    " CONVERSIONRATE_SEND_RECEIVE= case when to_number(  minreceivevalue )/ to_number( minsendvalue ) < to_number( maxreceivevalue ) / to_number( maxsendvalue )  " +
    " then to_char( round( to_number( minreceivevalue )/ to_number( minsendvalue ), 2)) else to_char( round( to_number( maxreceivevalue )/ to_number( maxsendvalue ), 2)) end, " +
    " DATASET=0 " &
    IIf(isNewProduct, "", ", MODIFIED_DATE=sysdate ") &
    " where skucode='" & DBStr(skuCode) & "'"

        ExecuteNonQueryFunction(_tempStr)

        Return True

    End Function

    Function DingProductUpdate(schedulerTime As String) As Boolean
        Try

            WriteToLogFile("DingProductUpdate Started")

            UpdateDingSchedulerFields("LAST_RUN_TIME", " '" & schedulerTime & "' ", "ProductUpdate", False)

            Dim response As IRestResponse = GetDingAPIResponse("GetProducts")

            Dim objRes = JsonConvert.DeserializeObject(Of clsDing.GetProductsRes)(response.Content)

            'Dim sr As New StreamReader(Application.StartupPath() + "\Files\GetProductsSampleRes.txt")
            'Dim response = sr.ReadToEnd()
            'Dim objRes = JsonConvert.DeserializeObject(Of clsDing.GetProductsRes)(response) 'shinoj note
            ''GetProductsSampleRes

            Dim benefits As String
            Dim paymentTypes As String
            Dim tempCount As Int16 = 0

            Dim _tempStr As String, _tempNumber As ULong = 0, _skus As String = ""

            Dim dt_Data As New DataTable
            Dim productExist As UInt16 = 0, productUpdated As UInt16 = 0, productAdd As UInt16 = 0

            dt_Data = ReturnDataTable("Select a.*, 0 as RowExist From Ding_Products a")

            For Each itm As clsDing.ProductsItems In objRes.items

                benefits = ""
                For i As Int16 = 0 To itm.benefits.Count - 1
                    benefits = benefits + itm.benefits(i)
                    If i <> itm.benefits.Count - 1 Then benefits = benefits + ","
                Next

                paymentTypes = ""
                For i As Int16 = 0 To itm.paymentTypes.Count - 1
                    paymentTypes = paymentTypes + itm.paymentTypes(i)
                    If i <> itm.paymentTypes.Count - 1 Then paymentTypes = paymentTypes + ","
                Next

                productExist = 0
                productUpdated = 0
                productAdd = 0

                For Each dr As DataRow In dt_Data.Rows

                    If (CheckProductExists(dr, itm, benefits, paymentTypes)) = True Then
                        dr("RowExist") = 1
                        productExist = 1
                        productUpdated = 0
                        Exit For
                    ElseIf dr("skucode").Equals(DBStr(itm.skuCode)) Then
                        dr("RowExist") = 1
                        productExist = 1
                        productUpdated = 1
                        Exit For
                    End If

                Next

                If (productUpdated = 1) Then
                    _tempStr = "" +
                    " Update DING_PRODUCTS set " &
                    " providercode='" & DBStr(itm.providerCode) & "', skucode='" & DBStr(itm.skuCode) & "'," +
                    " localizationkey='" & DBStr(itm.localizationKey) & "', maxcustomerfee='" & DBStr(itm.maximum.customerFee) & "', " &
                    " maxdistributorfee='" & DBStr(itm.maximum.distributorFee) & "', maxreceivevalue='" & DBStr(itm.maximum.receiveValue) & "', maxreceivecurrencyiso='" & DBStr(itm.maximum.receiveCurrencyIso) & "', " &
                    " maxreceivevalueexcludingtax='" & DBStr(itm.maximum.receiveValueExcludingTax) & "', maxtaxrate='" & DBStr(itm.maximum.taxRate) & "', maxtaxname='" & DBStr(itm.maximum.taxName) & "', maxtaxcalculation='" & DBStr(itm.maximum.taxCalculation) & "'," &
                    " maxsendvalue='" & DBStr(itm.maximum.sendValue) & "',	maxsendcurrencyiso='" & DBStr(itm.maximum.sendCurrencyIso) & "', mincustomerfee='" & DBStr(itm.minimum.customerFee) & "', mindistributorfee='" & DBStr(itm.minimum.distributorFee) & "'," &
                    " minreceivevalue='" & DBStr(itm.minimum.receiveValue) & "', minreceivecurrencyiso='" & DBStr(itm.minimum.receiveCurrencyIso) & "', minreceivevalueexcludingtax='" & DBStr(itm.minimum.receiveValueExcludingTax) & "', mintaxrate='" & DBStr(itm.minimum.taxRate) & "'," &
                    " mintaxname='" & DBStr(itm.minimum.taxName) & "', mintaxcalculation='" & DBStr(itm.minimum.taxCalculation) & "', minsendvalue='" & DBStr(itm.minimum.sendValue) & "', minsendcurrencyIso='" & DBStr(itm.minimum.sendCurrencyIso) & "'," &
                    " commissionrate='" & DBStr(itm.commissionRate) & "', processingmode='" & DBStr(itm.processingMode) & "', redemptionmechanism='" & DBStr(itm.redemptionMechanism) & "', benefits='" & DBStr(benefits) & "'," &
                    " validityperiodiso='" & DBStr(itm.validityPeriodIso) & "', uatnumber='" & DBStr(itm.uatNumber) & "', additionalinformation='" & DBStr(itm.additionalInformation) & "', defaultdisplaytext='" & DBStr(itm.defaultDisplayText) & "'," &
                    " regioncode='" & DBStr(itm.regionCode) & "', paymenttypes='" & DBStr(paymentTypes) & "', lookupbillsrequired='" & DBStr(itm.lookupBillsRequired) & "',MODIFIED_DATE=sysdate where skucode='" & DBStr(itm.skuCode) & "'"

                    ExecuteNonQueryFunction(_tempStr)

                    UpdateProduct2nd(False, DBStr(itm.skuCode))

                ElseIf productExist = 0 Then

                    _tempStr = "Insert into DING_PRODUCTS(" &
                "providercode, skucode, localizationkey, maxcustomerfee, " &
                "maxdistributorfee,	maxreceivevalue, maxreceivecurrencyiso,	" &
                "maxreceivevalueexcludingtax, maxtaxrate, maxtaxname, maxtaxcalculation," &
                "maxsendvalue,	maxsendcurrencyiso,	mincustomerfee,	mindistributorfee," &
                "minreceivevalue, minreceivecurrencyiso, minreceivevalueexcludingtax, mintaxrate," &
                "mintaxname, mintaxcalculation,	minsendvalue, minsendcurrencyIso," &
                "commissionrate, processingmode, redemptionmechanism, benefits," &
                "validityperiodiso, uatnumber, additionalinformation, defaultdisplaytext," &
                "regioncode, paymenttypes, lookupbillsrequired,CREATED_DATE" +
                ")" &
                " values(" &
            "'" & DBStr(itm.providerCode) & "','" & DBStr(itm.skuCode) & "','" & DBStr(itm.localizationKey) & "','" & DBStr(itm.maximum.customerFee) & "'," &
            "'" & DBStr(itm.maximum.distributorFee) & "','" & DBStr(itm.maximum.receiveValue) & "','" & DBStr(itm.maximum.receiveCurrencyIso) & "'," &
            "'" & DBStr(itm.maximum.receiveValueExcludingTax) & "','" & DBStr(itm.maximum.taxRate) & "','" & DBStr(itm.maximum.taxName) & "','" & DBStr(itm.maximum.taxCalculation) & "'," &
            "'" & DBStr(itm.maximum.sendValue) & "','" & DBStr(itm.maximum.sendCurrencyIso) & "','" & DBStr(itm.minimum.customerFee) & "','" & DBStr(itm.minimum.distributorFee) & "'," &
            "'" & DBStr(itm.minimum.receiveValue) & "','" & DBStr(itm.minimum.receiveCurrencyIso) & "','" & DBStr(itm.minimum.receiveValueExcludingTax) & "','" & DBStr(itm.minimum.taxRate) & "'," &
            "'" & DBStr(itm.minimum.taxName) & "','" & DBStr(itm.minimum.taxCalculation) & "','" & DBStr(itm.minimum.sendValue) & "','" & DBStr(itm.minimum.sendCurrencyIso) & "'," &
            "'" & DBStr(itm.commissionRate) & "','" & DBStr(itm.processingMode) & "','" & DBStr(itm.redemptionMechanism) & "','" & DBStr(benefits) & "'," &
            "'" & DBStr(itm.validityPeriodIso) & "','" & DBStr(itm.uatNumber) & "','" & DBStr(itm.additionalInformation) & "','" & DBStr(itm.defaultDisplayText) & "'," &
            "'" & DBStr(itm.regionCode) & "','" & DBStr(paymentTypes) & "','" & DBStr(itm.lookupBillsRequired) & "',sysdate" &
            ")"
                    ExecuteNonQueryFunction(_tempStr)

                    WriteToLogFile("Added Product " + DBStr(itm.skuCode))

                    For Each sd As clsDing.SettingDefinitions In itm.settingDefinitions
                        'ExecuteNonQueryFunction("Delete from  DING_PRODUCTS_SD where localizationkey='" & itm.localizationKey & "'")
                        ExecuteNonQueryFunction("Insert into DING_PRODUCTS_SD(name, description, ismandatory, localizationkey)" &
                    " values('" & DBStr(sd.name) & "','" & DBStr(sd.description) & "','" & DBStr(sd.ismandatory) & "','" & DBStr(itm.localizationKey) & "')")
                    Next

                    UpdateProduct2nd(True, DBStr(itm.skuCode))

                    WriteToLogFile("Updated NewProduct " + DBStr(itm.skuCode))

                End If

                _tempNumber += 1

                frmDingIntegrator.txtLog.Text = String.Format("Processed {0} / {1} at {2}", _tempNumber.ToString, objRes.items.Count.ToString, Format(Now, "dd-MMM-yyyy HH:mm:ss"))
                Application.DoEvents()

            Next

            'For Each dr As DataRow In dt_Data.Rows
            '    If dr("RowExist") = 0 And dr("StatusCode").ToString.Equals("1") Then
            '        ExecuteNonQueryFunction("Update ding_products set StatusCode=0, StatusDesc='System Disabled' Where skucode='" & dr("skucode") & "'")
            '        WriteToLogFile("Updated Product Status to 0 " + dr("skucode"))
            '    End If
            'Next

            _tempStr = String.Format("Process Completed. JsonItems: {0}  DBItems: {1} at {2}", objRes.items.Count.ToString, dt_Data.Rows.Count.ToString, Format(Now, "dd-MMM-yyyy HH:mm:ss"))
            WriteToLogFile(_tempStr)

            UpdateDingSchedulerFields("LAST_SUCCESFUL_RUN_TIME", " '" & schedulerTime & "' ", "ProductUpdate", False)
            Return True

        Catch ex As Exception
            WriteToLogFile("DingProductUpdate Ex: " & ex.Message, True)
            UpdateDingSchedulerFields("ERROR_MSG", ex.Message, "ProductUpdate", True)
            Return False
        End Try

    End Function

#End Region

#Region "DingProductDescriptionUpdate"
    Function CheckProductDescriptionExists(dr As DataRow, itm As clsDing.ProductDescriptionsItems) As Boolean

        'If 1 = 1 AndAlso
        'DBStr(dr("DISPLAYTEXT")) = DBStr(itm.displayText) AndAlso
        'DBStr(dr("DESCRIPTIONMARKDOWN")) = DBStr(itm.descriptionMarkdown) AndAlso
        'DBStr(dr("READMOREMARKDOWN")) = DBStr(itm.readMoreMarkdown) AndAlso
        'DBStr(dr("LOCALIZATIONKEY")) = DBStr(itm.localizationKey) AndAlso
        'DBStr(dr("LANGUAGECODE")) = DBStr(itm.languageCode) Then

        If 1 = 1 AndAlso
        DBStr(dr("LOCALIZATIONKEY")) = DBStr(itm.localizationKey) AndAlso
        DBStr(dr("LANGUAGECODE")) = DBStr(itm.languageCode) Then

            Return True
        Else

            Return False

        End If

    End Function
    Function DingProductDescriptionUpdate(schedulerTime As String) As Boolean

        Try

            WriteToLogFile("DingProductDescriptionUpdate Started")

            UpdateDingSchedulerFields("LAST_RUN_TIME", " '" & schedulerTime & "' ", "ProductDescriptionUpdate", False)

            Dim response As IRestResponse = GetDingAPIResponse("GetProductDescriptions")

            Dim objRes = JsonConvert.DeserializeObject(Of clsDing.GetProductDescriptionsRes)(response.Content)

            'Dim sr As New StreamReader(Application.StartupPath() + "\Files\GetProductDescSampleRes.txt")
            'Dim response = sr.ReadToEnd()
            'Dim objRes = JsonConvert.DeserializeObject(Of clsDing.GetProductDescriptionsRes)(response) 'shinoj note
            'GetProductsSampleRes


            Dim tempCount As Int16 = 0

            Dim _tempStr As String, _tempNumber As ULong = 0

            Dim dt_Data As New DataTable
            Dim pExist As UInt16 = 0, pUpdated As UInt16 = 0, pAdd As UInt16 = 0

            dt_Data = ReturnDataTable("Select a.*, 0 as RowExist From DING_PRODUCT_DESCRIPTIONS a")

            For Each itm As clsDing.ProductDescriptionsItems In objRes.items

                pExist = 0
                pUpdated = 0
                pAdd = 0

                For Each dr As DataRow In dt_Data.Rows

                    If (CheckProductDescriptionExists(dr, itm)) = True Then
                        dr("RowExist") = 1
                        pExist = 1
                        pUpdated = 0
                        Exit For
                    ElseIf dr("localizationkey").Equals(DBStr(itm.localizationKey)) And dr("languagecode").Equals(DBStr(itm.languageCode)) Then
                        dr("RowExist") = 1
                        pExist = 1
                        pUpdated = 1
                        Exit For
                    End If

                Next

                If (pUpdated = 1) Then
                    _tempStr = "" +
                     "Update DING_PRODUCT_DESCRIPTIONS set displaytext='" & DBStr(itm.displayText) & "', descriptionmarkdown='" & DBStr(itm.descriptionMarkdown) & "'," &
                    " readmoremarkdown='" & DBStr(itm.readMoreMarkdown) & "', localizationkey='" & DBStr(itm.localizationKey) & "', languagecode='" & DBStr(itm.languageCode) & "' " &
                    " Where localizationkey='" & DBStr(itm.localizationKey) & "' and languagecode='" & DBStr(itm.languageCode) & "'"

                    ExecuteNonQueryFunction(_tempStr)

                    WriteToLogFile("Updated Product Description " + DBStr(itm.localizationKey))

                ElseIf pExist = 0 Then

                    _tempStr = "Insert into DING_PRODUCT_DESCRIPTIONS(displaytext, descriptionmarkdown," &
                    " readmoremarkdown, localizationkey, languagecode) " &
                " values('" & DBStr(itm.displayText) & "','" & DBStr(itm.descriptionMarkdown) & "','" &
                    DBStr(itm.readMoreMarkdown) & "','" & DBStr(itm.localizationKey) & "','" & DBStr(itm.languageCode) & "')"

                    ExecuteNonQueryFunction(_tempStr)

                    WriteToLogFile("Added Product Description " + DBStr(itm.localizationKey))

                End If

                _tempNumber += 1

                frmDingIntegrator.txtLog.Text = String.Format("Processed {0} / {1} at {2}", _tempNumber.ToString, objRes.items.Count.ToString, Format(Now, "dd-MMM-yyyy HH:mm:ss"))
                Application.DoEvents()

            Next

            For Each dr As DataRow In dt_Data.Rows
                If dr("RowExist") = 0 Then
                    ExecuteNonQueryFunction("Delete from DING_PRODUCT_DESCRIPTIONS Where localizationkey='" & dr("localizationkey") & "' and languagecode='" & dr("languagecode") & "'")
                    WriteToLogFile("Deleted Product Description " + dr("localizationkey"))
                End If
            Next

            _tempStr = String.Format("Process Completed. JsonItems: {0}  DBItems: {1} at {2}", objRes.items.Count.ToString, dt_Data.Rows.Count.ToString, Format(Now, "dd-MMM-yyyy HH:mm:ss"))
            WriteToLogFile(_tempStr)

            UpdateDingSchedulerFields("LAST_SUCCESFUL_RUN_TIME", " '" & schedulerTime & "' ", "ProductDescriptionUpdate", False)
            Return True

        Catch ex As Exception
            WriteToLogFile("DingProductDescriptionUpdate Ex: " & ex.Message, True)
            UpdateDingSchedulerFields("ERROR_MSG", ex.Message, "ProductDescriptionUpdate", True)
            Return False
        End Try
    End Function

#End Region

#Region "DingPromotionUpdate"
    Function CheckPromotionExists(dr As DataRow, itm As clsDing.PromotionsItems) As Boolean

        If 1 = 1 AndAlso
        DBStr(dr("providercode")) = DBStr(itm.providerCode) AndAlso
        DBStr(dr("startutc")) = DBStr(itm.startUtc) AndAlso
        DBStr(dr("endutc")) = DBStr(itm.endUtc) AndAlso
        DBStr(dr("currencyiso")) = DBStr(itm.currencyIso) AndAlso
        DBStr(dr("validityperiodiso")) = DBStr(itm.validityPeriodIso) AndAlso
        DBStr(dr("minimumsendamount")) = DBStr(itm.minimumSendAmount) AndAlso
        DBStr(dr("localizationkey")) = DBStr(itm.localizationKey) Then

            Return True
        Else

            Return False

        End If

    End Function
    Function DingPromotionUpdate(schedulerTime As String) As Boolean

        Try

            WriteToLogFile("DingPromotionUpdate Started")
            UpdateDingSchedulerFields("LAST_RUN_TIME", " '" & schedulerTime & "' ", "PromotionUpdate", False)

            Dim response As IRestResponse = GetDingAPIResponse("GetPromotions")

            Dim objRes = JsonConvert.DeserializeObject(Of clsDing.GetPromotionsRes)(response.Content)

            'Dim sr As New StreamReader(Application.StartupPath() + "\Files\GetProductDescSampleRes.txt")
            'Dim response = sr.ReadToEnd()
            'Dim objRes = JsonConvert.DeserializeObject(Of clsDing.GetProductDescriptionsRes)(response) 'shinoj note
            'GetProductsSampleRes


            Dim tempCount As Int16 = 0

            Dim _tempStr As String, _tempNumber As ULong = 0

            Dim dt_Data As New DataTable
            Dim pExist As UInt16 = 0, pUpdated As UInt16 = 0, pAdd As UInt16 = 0

            dt_Data = ReturnDataTable("Select a.*, 0 as RowExist From DING_PROMOTIONS a")

            For Each itm As clsDing.PromotionsItems In objRes.items

                pExist = 0
                pUpdated = 0
                pAdd = 0

                For Each dr As DataRow In dt_Data.Rows

                    If (CheckPromotionExists(dr, itm)) = True Then
                        dr("RowExist") = 1
                        pExist = 1
                        pUpdated = 0
                        Exit For
                    ElseIf dr("providercode").Equals(DBStr(itm.providerCode)) Then
                        dr("RowExist") = 1
                        pExist = 1
                        pUpdated = 1
                        Exit For
                    End If

                Next

                If (pUpdated = 1) Then
                    _tempStr = "" +
                     "Update DING_PROMOTIONS set " &
                     " startutc='" & DBStr(itm.startUtc) & "', " &
                     " endutc='" & DBStr(itm.endUtc) & "', currencyiso='" & DBStr(itm.currencyIso) & "', " &
                     " validityperiodiso='" & DBStr(itm.validityPeriodIso) & "', minimumsendamount='" & DBStr(itm.minimumSendAmount) & "', " &
                     " localizationkey='" & DBStr(itm.localizationKey) & "' " &
                    " Where providercode='" & DBStr(itm.providerCode) & "'"

                    ExecuteNonQueryFunction(_tempStr)

                    WriteToLogFile("Updated DING_PROMOTIONS " + DBStr(itm.providerCode))

                ElseIf pExist = 0 Then

                    _tempStr = "" &
                    "Insert into DING_PROMOTIONS(providercode, startutc, endutc, currencyiso, validityperiodiso," &
                    " minimumsendamount, localizationkey)" &
                    " values(" &
                    "'" & DBStr(itm.providerCode) & "','" & DBStr(itm.startUtc) & "','" & DBStr(itm.endUtc) & "', " &
                    "'" & DBStr(itm.currencyIso) & "','" & DBStr(itm.validityPeriodIso) & "','" & DBStr(itm.minimumSendAmount) & "', " &
                    "'" & DBStr(itm.localizationKey) & "'" &
                     ")"

                    ExecuteNonQueryFunction(_tempStr)

                    WriteToLogFile("Added DING_PROMOTIONS " + DBStr(itm.providerCode))

                End If

                _tempNumber += 1

                _tempStr = String.Format("Processed {0} / {1} at {2}", _tempNumber.ToString, objRes.items.Count.ToString, Format(Now, "dd-MMM-yyyy HH:mm:ss"))
                frmDingIntegrator.txtLog.Text = _tempStr

                Application.DoEvents()

            Next

            For Each dr As DataRow In dt_Data.Rows
                If dr("RowExist") = 0 Then
                    ExecuteNonQueryFunction("Delete from DING_PROMOTIONS Where providercode='" & dr("providercode") & "'")
                    WriteToLogFile("Deleted DING_PROMOTIONS " + dr("providercode"))
                End If
            Next

            _tempStr = String.Format("Process Completed. JsonItems: {0}  DBItems: {1} at {2}", objRes.items.Count.ToString, dt_Data.Rows.Count.ToString, Format(Now, "dd-MMM-yyyy HH:mm:ss"))
            WriteToLogFile(_tempStr)

            UpdateDingSchedulerFields("LAST_SUCCESFUL_RUN_TIME", " '" & schedulerTime & "' ", "PromotionUpdate", False)
            Return True

        Catch ex As Exception
            WriteToLogFile("DingPromotionUpdate Ex: " & ex.Message, True)
            UpdateDingSchedulerFields("ERROR_MSG", ex.Message, "PromotionUpdate", True)
            Return False
        End Try
    End Function

#End Region

#Region "DingPromotionDescUpdate"
    Function CheckPromotionDescExists(dr As DataRow, itm As clsDing.PromotionsDescItems) As Boolean

        If 1 = 1 AndAlso
        DBStr(dr("dates")) = DBStr(itm.dates) AndAlso
        DBStr(dr("headline")) = DBStr(itm.headline) AndAlso
        DBStr(dr("termsandconditionsmarkdown")) = DBStr(itm.termsAndConditionsMarkDown) AndAlso
        DBStr(dr("bonusvalidity")) = DBStr(itm.bonusValidity) AndAlso
        DBStr(dr("promotiontype")) = DBStr(itm.promotionType) AndAlso
        DBStr(dr("localizationkey")) = DBStr(itm.localizationKey) AndAlso
        DBStr(dr("languagecode")) = DBStr(itm.languageCode) Then

            Return True
        Else

            Return False

        End If

    End Function

    Function DingPromotionDescUpdate(schedulerTime As String) As Boolean

        Try

            WriteToLogFile("DingPromotionDescUpdate Started")
            UpdateDingSchedulerFields("LAST_RUN_TIME", " '" & schedulerTime & "' ", "PromotionDescUpdate", False)

            Dim response As IRestResponse = GetDingAPIResponse("GetPromotionDescriptions")

            Dim objRes = JsonConvert.DeserializeObject(Of clsDing.GetPromotionsDescRes)(response.Content)

            'Dim sr As New StreamReader(Application.StartupPath() + "\Files\GetProductDescSampleRes.txt")
            'Dim response = sr.ReadToEnd()
            'Dim objRes = JsonConvert.DeserializeObject(Of clsDing.GetProductDescriptionsRes)(response) 'shinoj note
            'GetProductsSampleRes

            Dim tempCount As Int16 = 0

            Dim _tempStr As String, _tempNumber As ULong = 0

            Dim dt_Data As New DataTable
            Dim pExist As UInt16 = 0, pUpdated As UInt16 = 0, pAdd As UInt16 = 0

            dt_Data = ReturnDataTable("Select a.*, 0 as RowExist From DING_PROMOTION_DESC a")

            For Each itm As clsDing.PromotionsDescItems In objRes.items

                pExist = 0
                pUpdated = 0
                pAdd = 0

                For Each dr As DataRow In dt_Data.Rows

                    If (CheckPromotionDescExists(dr, itm)) = True Then
                        dr("RowExist") = 1
                        pExist = 1
                        pUpdated = 0
                        Exit For
                    ElseIf dr("localizationkey").Equals(DBStr(itm.localizationKey)) AndAlso dr("languagecode").Equals(DBStr(itm.languageCode)) Then
                        dr("RowExist") = 1
                        pExist = 1
                        pUpdated = 1
                        Exit For
                    End If

                Next

                If (pUpdated = 1 Or pExist = 0) Then

                    ExecuteNonQueryFunction("Delete from  DING_PROMOTION_DESC where localizationkey='" & itm.localizationKey & "' and languagecode='" & DBStr(itm.languageCode) & "'")

                    _tempStr = "" &
                   " insert into DING_PROMOTION_DESC(dates, headline, termsandconditionsmarkdown, bonusvalidity, promotiontype, localizationkey, languagecode)" &
                   " values('" & DBStr(itm.dates) & "','" & DBStr(itm.headline) & "','" & DBStr(itm.termsAndConditionsMarkDown) & "','" & DBStr(itm.bonusValidity) & "','" & DBStr(itm.promotionType) & "','" & DBStr(itm.localizationKey) & "','" & DBStr(itm.languageCode) & "')"

                    ExecuteNonQueryFunction(_tempStr)

                    For Each sa As clsDing.SendAmounts In itm.sendAmounts

                        ExecuteNonQueryFunction("Delete from  DING_PROMOTION_DESC_SA where localizationkey='" & itm.localizationKey & "'")
                        ExecuteNonQueryFunction("Insert into DING_PROMOTION_DESC_SA(minimum, maximum, localizationkey)" &
                        " values('" & DBStr(sa.minimum) & "','" & DBStr(sa.maximum) & "','" & DBStr(itm.localizationKey) & "')")

                        For Each b As clsDing.Bonuses In sa.bonuses

                            ExecuteNonQueryFunction("Delete from  DING_PROMOTION_DESC_SA_B where localizationkey='" & itm.localizationKey & "'")
                            ExecuteNonQueryFunction("Insert into DING_PROMOTION_DESC_SA_B(bonustype, quantity, validity, localizationkey)" &
                    " values('" & DBStr(b.bonusType) & "','" & DBStr(b.quantity) & "','" & DBStr(b.validity) & "','" & DBStr(itm.localizationKey) & "')")

                        Next

                    Next

                    WriteToLogFile("Added DING_PROMOTION_DESC " + DBStr(itm.localizationKey))

                End If

                _tempNumber += 1

                _tempStr = String.Format("Processed {0} / {1} at {2}", _tempNumber.ToString, objRes.items.Count.ToString, Format(Now, "dd-MMM-yyyy HH:mm:ss"))
                frmDingIntegrator.txtLog.Text = _tempStr

                Application.DoEvents()

            Next

            For Each dr As DataRow In dt_Data.Rows
                If dr("RowExist") = 0 Then

                    ExecuteNonQueryFunction("Delete from DING_PROMOTION_DESC Where localizationKey='" & dr("localizationKey") & "' and languagecode='" & dr("languagecode") & "' ")
                    ExecuteNonQueryFunction("Delete from  DING_PROMOTION_DESC_SA where localizationkey='" & dr("localizationKey") & "'")
                    ExecuteNonQueryFunction("Delete from  DING_PROMOTION_DESC_SA_B where localizationkey='" & dr("localizationKey") & "'")

                    WriteToLogFile("Deleted DING_PROMOTION_DESC " + dr("localizationKey"))

                End If
            Next

            _tempStr = String.Format("Process Completed. JsonItems: {0}  DBItems: {1} at {2}", objRes.items.Count.ToString, dt_Data.Rows.Count.ToString, Format(Now, "dd-MMM-yyyy HH:mm:ss"))
            WriteToLogFile(_tempStr)

            UpdateDingSchedulerFields("LAST_SUCCESFUL_RUN_TIME", " '" & schedulerTime & "' ", "PromotionDescUpdate", False)
            Return True

        Catch ex As Exception
            WriteToLogFile("DingPromotionDescUpdate Ex: " & ex.Message, True)
            UpdateDingSchedulerFields("ERROR_MSG", ex.Message, "PromotionDescUpdate", True)
            Return False
        End Try
    End Function

#End Region
    Function GetRegionsByCountry() As Boolean
        Try

            Dim dt As New DataTable
            dt = ReturnDataTable("Select distinct countryiso from ding_countries")

            Dim response As IRestResponse
            Dim objRes

            For Each dr As DataRow In dt.Rows

                response = "GetRegions" + "?countryIsos=" + dr("countryiso")

                objRes = JsonConvert.DeserializeObject(Of clsDing.GetRegionsRes)(response.Content)

                For Each itm As clsDing.RegionsItems In objRes.items

                    ExecuteNonQueryFunction("Delete from  DING_REGIONS where regioncode='" & itm.regionCode.Replace("'", "''") & "' and countryiso='" & itm.countryIso.Replace("'", "''") & "'")

                    ExecuteNonQueryFunction("Insert into DING_REGIONS(regioncode, regionname,countryiso) values('" & itm.regionCode.Replace("'", "''") & "','" & itm.regionName.Replace("'", "''") & "','" & itm.countryIso.Replace("'", "''") & "')")

                Next

            Next
            Return True
        Catch ex As Exception
            WriteToLogFile(ex.Message, True)
            Return False
        End Try
    End Function


End Module
