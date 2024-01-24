Namespace Models

    Public Class ErrorDetail

        Public ReadOnly Property DateReceived As Date = Nothing
        Public ReadOnly Property Exception As Exception = Nothing
        Public ReadOnly Property Message As String = ""
        Public ReadOnly Property Stack As String = ""
        Public ReadOnly Property Title As String = ""

        Public Sub New(title As String, ByRef exception As Exception)
            _DateReceived = GetCurrentDate()
            _Exception = exception
            _Message = exception.Message
            _Title = title

            CheckDetails()
        End Sub

        Public Sub New(title As String, ByRef webException As Net.WebException)
            _DateReceived = GetCurrentDate()
            _Exception = webException
            _Message = webException.Message
            _Title = title

            CheckDetails()
        End Sub

        Public Sub New(message As String, title As String)
            _DateReceived = GetCurrentDate()
            _Exception = Nothing
            _Message = message
            _Title = title

            CheckDetails()
        End Sub

        Private Sub CheckDetails()
            Try
                If Exception Is Nothing Then
                    _Stack = ""
                Else
                    _Message = Exception.Message
                    _Stack = Exception.StackTrace

                    If Exception.InnerException IsNot Nothing Then
                        _Message = "{0}{1}   Inner Exceptions:".FormatWith(Message, Environment.NewLine)

                        Dim InnerException As Exception = Exception.InnerException
                        Do While InnerException IsNot Nothing
                            _Message = "{0}{1}      {2}".FormatWith(Message, Environment.NewLine, InnerException.Message)

                            InnerException = InnerException.InnerException
                        Loop
                    End If

                    If TypeOf Exception Is System.Reflection.ReflectionTypeLoadException Then
                        Dim LoadException As System.Reflection.ReflectionTypeLoadException = DirectCast(Exception, System.Reflection.ReflectionTypeLoadException)
                        If LoadException IsNot Nothing AndAlso LoadException.LoaderExceptions IsNot Nothing AndAlso LoadException.LoaderExceptions.Count > 0 Then
                            _Message = "{0}{1}   Loader Exceptions:".FormatWith(Message, Environment.NewLine)

                            For Each Current As Exception In LoadException.LoaderExceptions
                                _Message = "{0}{1}      {2}".FormatWith(Message, Environment.NewLine, Current.Message)
                            Next
                        End If
                    End If
                End If
            Catch ex As Exception
            End Try
        End Sub

        Public Sub WriteToFile(filename As String)
            Try
                Using writer As New IO.StreamWriter(filename, True)
                    writer.WriteLine("{0}, Version: {1}".FormatWith(_DateReceived.GetSQLString("dd MMMM yyyy - HH:mm:ss"), Settings.ProductVersion))

                    If Title.IsSet() Then
                        writer.WriteLine("   Title: {0}".FormatWith(Title.GetCleanLogLine()))
                    End If

                    If Message.IsSet() Then
                        writer.WriteLine("   Message: {0}".FormatWith(Message.GetCleanLogLine()))
                    End If

                    If Stack.IsSet() Then
                        writer.WriteLine("   Stack: {0}".FormatWith(Stack.GetCleanLogLine()))
                    End If

                    writer.WriteLine("")
                End Using
            Catch ex As Exception
            End Try
        End Sub

    End Class

End Namespace