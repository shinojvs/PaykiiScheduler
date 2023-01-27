Public Class clsDing

#Region "Ding Global Classes"
    Public Class ErrorCodes
        Public Property code As String
        Public Property context As String
    End Class

#End Region


#Region "GetCountry"

    Public Class GetCountryRes
        Public Property resultCode As Integer
        Public Property errorCodes As List(Of ErrorCodes)
        Public Property items As List(Of CountryItems)

    End Class

    Public Class CountryItems
        Public Property countryIso As String
        Public Property countryName As String
        Public Property internationalDialingInformation As List(Of InternationalDialingInformation)
        Public Property regionCodes As List(Of String)

    End Class

    Public Class InternationalDialingInformation
        Public Property prefix As String
        Public Property minimumLength As Integer
        Public Property maximumLength As Integer
    End Class



#End Region

#Region "GetCurrencies"
    Public Class GetCurrencyRes
        Public Property resultCode As Integer
        Public Property errorCodes As List(Of ErrorCodes)
        Public Property items As List(Of CurrencyItems)

    End Class

    Public Class CurrencyItems
        Public Property currencyIso As String
        Public Property currencyName As String
    End Class

#End Region

#Region "GetRegions"
    Public Class GetRegionsRes
        Public Property resultCode As Integer
        Public Property errorCodes As List(Of ErrorCodes)
        Public Property items As List(Of RegionsItems)

    End Class

    Public Class RegionsItems
        Public Property regionCode As String
        Public Property regionName As String
        Public Property countryIso As String
    End Class

#End Region

#Region "GetProviders"

    Public Class GetProvidersRes
        Public Property resultCode As Integer
        Public Property errorCodes As List(Of ErrorCodes)
        Public Property items As List(Of ProviderItems)

    End Class

    Public Class ProviderItems
        Public Property providerCode As String
        Public Property countryIso As String
        Public Property name As String
        Public Property shortName As String
        Public Property validationRegex As String
        Public Property customerCareNumber As String
        Public Property regionCodes As List(Of String)
        Public Property paymentTypes As List(Of String)
        Public Property logoUrl As String
    End Class

#End Region

#Region "GetProviderStatus"
    Public Class GetProviderStatusRes
        Public Property resultCode As Integer
        Public Property errorCodes As List(Of ErrorCodes)
        Public Property items As List(Of ProviderStatusItems)

    End Class

    Public Class ProviderStatusItems
        Public Property providercode As String
        Public Property isprocessingtransfers As Boolean
        Public Property message As String
    End Class

#End Region

#Region "GetProducts"

    Public Class GetProductsRes
        Public Property resultCode As Integer
        Public Property errorCodes As List(Of ErrorCodes)
        Public Property items As List(Of ProductsItems)

    End Class

    Public Class ProductsItems
        Public Property providerCode As String
        Public Property skuCode As String
        Public Property localizationKey As String
        Public Property settingDefinitions As List(Of SettingDefinitions)
        Public Property maximum As Maximum
        Public Property minimum As Minimum
        Public Property commissionRate As Decimal
        Public Property processingMode As String
        Public Property redemptionMechanism As String
        Public Property benefits As List(Of String)
        Public Property validityPeriodIso As String
        Public Property uatNumber As String
        Public Property additionalInformation As String
        Public Property defaultDisplayText As String
        Public Property regionCode As String
        Public Property paymentTypes As List(Of String)
        Public Property lookupBillsRequired As Boolean
    End Class

    Public Class SettingDefinitions
        Public Property name As String
        Public Property description As String
        Public Property ismandatory As Boolean
    End Class

    Public Class Maximum
        Public Property customerFee As Decimal
        Public Property distributorFee As Decimal
        Public Property receiveValue As Decimal
        Public Property receiveCurrencyIso As String
        Public Property receiveValueExcludingTax As Decimal
        Public Property taxRate As Decimal
        Public Property taxName As String
        Public Property taxCalculation As String
        Public Property sendValue As Decimal
        Public Property sendCurrencyIso As String
    End Class

    Public Class Minimum
        Public Property customerFee As Decimal
        Public Property distributorFee As Decimal
        Public Property receiveValue As Decimal
        Public Property receiveCurrencyIso As String
        Public Property receiveValueExcludingTax As Decimal
        Public Property taxRate As Decimal
        Public Property taxName As String
        Public Property taxCalculation As String
        Public Property sendValue As Decimal
        Public Property sendCurrencyIso As String
    End Class



#End Region

#Region "GetProductDescriptions"
    Public Class GetProductDescriptionsRes
        Public Property resultCode As Integer
        Public Property errorCodes As List(Of ErrorCodes)
        Public Property items As List(Of ProductDescriptionsItems)

    End Class

    Public Class ProductDescriptionsItems
        Public Property displayText As String
        Public Property descriptionMarkdown As String
        Public Property readMoreMarkdown As String
        Public Property localizationKey As String
        Public Property languageCode As String
    End Class

#End Region

#Region "GetPromotions"
    Public Class GetPromotionsRes
        Public Property resultCode As Integer
        Public Property errorCodes As List(Of ErrorCodes)
        Public Property items As List(Of PromotionsItems)

    End Class

    Public Class PromotionsItems
        Public Property providerCode As String
        Public Property startUtc As String
        Public Property endUtc As String
        Public Property currencyIso As String
        Public Property validityPeriodIso As String
        Public Property minimumSendAmount As Decimal
        Public Property localizationKey As String
    End Class

#End Region

#Region "GetPromotionDescriptions"
    Public Class GetPromotionsDescRes
        Public Property resultCode As Integer
        Public Property errorCodes As List(Of ErrorCodes)
        Public Property items As List(Of PromotionsDescItems)
    End Class

    Public Class PromotionsDescItems
        Public Property dates As String
        Public Property headline As String
        Public Property termsAndConditionsMarkDown As String
        Public Property bonusValidity As String
        Public Property promotionType As String
        Public Property localizationKey As String
        Public Property languageCode As String
        Public Property sendAmounts As List(Of SendAmounts)
    End Class

    Public Class SendAmounts
        Public Property minimum As Decimal
        Public Property maximum As Decimal
        Public Property bonuses As List(Of Bonuses)
    End Class

    Public Class Bonuses
        Public Property bonusType As String
        Public Property quantity As Decimal
        Public Property validity As Int16
    End Class

#End Region

#Region "GetProviders for Android"

    Public Class Providers
        Public Property ProviderCode As String
        Public Property ProviderName As String
        Public Property TransferTypes As List(Of TransferTypes)
    End Class

    Public Class TransferTypes
        Public Property transferTypes As List(Of String)
    End Class

#End Region

End Class