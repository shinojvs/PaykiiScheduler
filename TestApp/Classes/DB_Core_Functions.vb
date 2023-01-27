
Imports System.Data.OracleClient
Imports System.Data
Module DB_Core_Functions
#Region "SQL"

    Private Function GetConnection() As OracleConnection
        Dim conStr As String = ""
        conStr = String.Concat(String.Concat(String.Concat(String.Concat("Data Source=" + VendTek.AdministratorNet.ClsRegistry.GetSetting("eComm", "Service", "efresh") + ";", "User ID=", VendTek.AdministratorNet.ClsRegistry.GetSetting("eComm", "UID", "ec"), ";"), "Password=", VendTek.AdministratorNet.Tools.DecPW(VendTek.AdministratorNet.ClsRegistry.GetSetting("eComm", "PWD")), ";"), "Max Pool Size=", VendTek.AdministratorNet.ClsRegistry.GetSetting("eComm", "MaxPoolSize", "200"), ";"), "Pooling=true;")
        Return New OracleConnection(conStr)
    End Function

    Public Function ExecuteScalarFunction(ByVal QueryString As String) As Object

        Dim ReturnValue As Object
        Dim _SqlCmd As New OracleCommand(QueryString, GetConnection())
        Try
            _SqlCmd.CommandTimeout = 3600
            _SqlCmd.Connection.Open()
            ReturnValue = _SqlCmd.ExecuteScalar
            _SqlCmd.Connection.Close()
            _SqlCmd.Connection = Nothing
            GC.Collect()
            If IsDBNull(ReturnValue) Then Return "Null"
            Return ReturnValue
        Catch ex As Exception
            WriteToLogFile(" ExecuteScalarFunction EX:" & ex.Message, True)
            _SqlCmd.Connection.Close()
            Throw ex
        End Try

    End Function

    Public Function ReturnDataTable(ByVal SQLStr As String) As DataTable
        Dim _SqlCmd As New OracleCommand(SQLStr, GetConnection())
        _SqlCmd.CommandType = CommandType.Text
        _SqlCmd.CommandTimeout = 3600
        _SqlCmd.Connection.Open()
        Try
            Dim odr As OracleDataReader = _SqlCmd.ExecuteReader
            Dim dt As New DataTable()
            dt.Load(odr)
            _SqlCmd.Connection.Close()
            _SqlCmd.Connection = Nothing
            GC.Collect()
            Return dt
        Catch ex As Exception
            WriteToLogFile(" ReturnDataTable EX:" & ex.Message, True)
            _SqlCmd.Connection.Close()
            Return Nothing
        End Try

    End Function

    Public Function ExecuteNonQueryFunction(ByVal QueryString As String, Optional Timeout As Int16 = 0) As String

        Dim ReturnValue As Integer
        Dim _SqlCmd As New OracleCommand(QueryString, GetConnection())
        Try
            _SqlCmd.CommandTimeout = 3600
            _SqlCmd.Connection.Open()
            ReturnValue = _SqlCmd.ExecuteNonQuery
            _SqlCmd.Connection.Close()
            _SqlCmd.Connection = Nothing
            GC.Collect()
            Return ReturnValue.ToString
            Return False
        Catch ex As Exception
            WriteToLogFile(" ExecuteNonQueryFunction EX:" & ex.Message, True)
            _SqlCmd.Connection.Close()
            Throw ex
        End Try

    End Function


#End Region

End Module
