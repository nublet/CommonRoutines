Namespace CommonRoutines.Extensions

    Public Module System_Exception

        <Runtime.CompilerServices.Extension()> Public Function GetErrorString(ByRef e As Exception, indent As String) As String
            Return e.GetErrorString(indent, "")
        End Function

        <Runtime.CompilerServices.Extension()> Public Function GetErrorString(ByRef e As Exception, indent As String, prefix As String) As String
            If e Is Nothing OrElse e.Message.IsNotSet() Then
                Return ""
            End If
            Dim Result As String = ""

            Try
                Result.Append(e.Message.GetCleanLogLine("{0}{1}".FormatWith(Settings.Indent, indent), "Message: "))
                Result.Append(vbNewLine, e.StackTrace.GetCleanLogLine("{0}{1}".FormatWith(Settings.Indent, indent), "Stack: "))

                If TypeOf e Is System.Reflection.ReflectionTypeLoadException Then
                    Dim rtle As System.Reflection.ReflectionTypeLoadException = DirectCast(e, System.Reflection.ReflectionTypeLoadException)

                    If rtle Is Nothing OrElse rtle.LoaderExceptions Is Nothing OrElse rtle.LoaderExceptions.Count <= 0 Then
                    Else
                        Result.Append(vbNewLine, "{0}Loader Exceptions".FormatWith(Settings.Indent))

                        For Each Current As Exception In rtle.LoaderExceptions
                            Result.Append(vbNewLine, Current.GetErrorString("{0}{0}{1}".FormatWith(Settings.Indent, indent), "{0}{0}Exception".FormatWith(Settings.Indent)))
                        Next
                    End If
                End If

                Result.Append(vbNewLine, e.InnerException.GetErrorString("{0}{1}".FormatWith(Settings.Indent, indent), "{0}Inner Exception".FormatWith(Settings.Indent)))
            Catch ex As Exception
            End Try

            Return Result
        End Function

        <Runtime.CompilerServices.Extension()> Public Sub ToLog(ByRef e As Exception)
            e.ToLog(False, "")
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub ToLog(ByRef e As Exception, silent As Boolean)
            e.ToLog(silent, "")
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub ToLog(ByRef e As Exception, title As String)
            e.ToLog(False, title)
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub ToLog(ByRef e As Exception, silent As Boolean, title As String)
            If e Is Nothing OrElse e.Message.IsNotSet() Then
                Return
            End If

            Errors.Log(silent, title, e)
        End Sub

    End Module

End Namespace