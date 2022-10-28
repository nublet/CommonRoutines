Namespace CommonRoutines.Extensions

    Public Module System_Net_WebException

        <Runtime.CompilerServices.Extension()> Public Function GetErrorString(ByRef e As Net.WebException, indent As String) As String
            Return e.GetErrorString(indent, "")
        End Function

        <Runtime.CompilerServices.Extension()> Public Function GetErrorString(ByRef e As Net.WebException, indent As String, prefix As String) As String
            If e Is Nothing OrElse e.Message.IsNotSet() Then
                Return ""
            End If
            Dim Result As String = ""

            Try
                Result.Append(e.Message.GetCleanLogLine("{0}{1}".FormatWith(Settings.Indent, indent), "Message: "))
                Result.Append(vbNewLine, e.StackTrace.GetCleanLogLine("{0}{1}".FormatWith(Settings.Indent, indent), "Stack: "))
                Result.Append(vbNewLine, e.InnerException.GetErrorString("{0}{1}".FormatWith(Settings.Indent, indent), "{0}Inner Exception".FormatWith(Settings.Indent)))
            Catch ex As Exception
            End Try

            Return Result
        End Function

        <Runtime.CompilerServices.Extension()> Public Sub ToLog(ByRef e As Net.WebException)
            e.ToLog(False, "")
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub ToLog(ByRef e As Net.WebException, silent As Boolean)
            e.ToLog(silent, "")
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub ToLog(ByRef e As Net.WebException, title As String)
            e.ToLog(False, title)
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub ToLog(ByRef e As Net.WebException, silent As Boolean, title As String)
            If e Is Nothing OrElse e.Message.IsNotSet() Then
                Return
            End If

            Errors.Log(silent, title, e)
        End Sub

    End Module

End Namespace