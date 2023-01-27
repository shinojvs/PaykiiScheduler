Module ModCommon

    Public usdRate As Decimal = CDec(My.Settings.UsdConverionRate)

    Public oracleDateFormat As String = "yyyy-mm-dd hh24:mi:ss"
    Public dotNetDateFormat As String = "yyyy-MM-dd HH:mm:ss"

    Public Sub WriteToLogFile(ByVal Info As String, Optional isError As Boolean = False)
        Try
            Dim objWriter As New System.IO.StreamWriter(My.Application.Info.DirectoryPath + "\LogFiles\" & IIf(isError, "Error", "") & "Log_" + Format(Now.Date, "ddMMMyyyy") + ".txt", True)
            objWriter.WriteLine(Info + " - " + Format(Now, "HH:mm:ss"))
            objWriter.Close()
        Catch ex As Exception
            MsgBox("Error while writing to text file Error:" & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 'Values to string Null as N/A for Ding
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

End Module
