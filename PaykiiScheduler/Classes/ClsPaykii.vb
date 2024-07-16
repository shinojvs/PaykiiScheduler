Public Class ClsPaykii

    'Public Class BillerCatalogRes
    '    Public List(Of BillerCatalog)
    'End Class

    Public Class BillerCatalogRes
        Public Property ResponseCode As String
        Public Property ResponseMessage As String
        Public Property CatalogVersion As String
        Public Property BillerType As String
        Public Property CountryName As String
        Public Property CountryCode As String
        Public Property BillerDescription As String
        Public Property BillerName As String
        Public Property BillerID As String
    End Class

    Public Class SKUCatalogRes
        Public Property ResponseCode As String
        Public Property ResponseMessage As String
        Public Property CatalogVersion As String
        Public Property SKU As String
        Public Property DaysToPost As Int16
        Public Property Currency As String
        Public Property BillerID As String
        Public Property BusinessDays As Int16
        Public Property PastDuePaymentAllowed As Int16
        Public Property Amount As Decimal
        Public Property Type As String
        Public Property MinAmount As String
        Public Property MaxAmount As String
        Public Property Description As String
        Public Property InquiryAvailable As Int16
        Public Property ExcessPaymentAllowed As Int16
        Public Property PartialPaymentAllowed As Int16
    End Class

    Public Class IOCatalogRes
        Public Property ResponseCode As String
        Public Property ResponseMessage As String
        Public Property CatalogVersion As String
        Public Property SKU As String
        Public Property Name As String
        Public Property Datatype As String
        Public Property ValidLengths As String
        Public Property MaxLength As Int16
        Public Property MinLength As Int16
        Public Property Type As Int16
        Public Property Description As String
        Public Property BillerID As String
        Public Property Operation As Int16
        Public Property IOID As Int16
    End Class


#Region "Forex"
    Public Class BillerType
        Public Property BillerType As String
        Public Property FXRate As String
    End Class

    Public Class SettlementCurrency
        Public Property SettlementCurrency As String
        Public Property BillerTypes As BillerType()
    End Class

    Public Class DailyFXRatePerBillerTypeRes
        Public Property ResponseCode As String
        Public Property FXDate As String
        Public Property ResponseMessage As String
        Public Property ResponseDateTime As String
        Public Property SettlementCurrencies As SettlementCurrency()
        Public Property BaseCurrency As String

    End Class
#End Region


End Class
