Namespace Errors

    Public Module _Shared

        Public Delegate Sub LogErrorDetailHandler(silent As Boolean, ByRef errorDetail As Models.ErrorDetail)

        Private ReadOnly _ErrorDetails As New List(Of Models.ErrorDetail)
        Private ReadOnly _Handlers As New List(Of LogErrorDetailHandler)

        Public ReadOnly Property Handlers As List(Of LogErrorDetailHandler)
            Get
                Return _Handlers
            End Get
        End Property

        Public Function GetErrors() As IEnumerable(Of Models.ErrorDetail)
            Return _ErrorDetails.OrderByDescending(Function(o) o.DateReceived)
        End Function

        Public Function GetStackMethodName() As String
            Try
                'Dim StackTrace As New System.Diagnostics.StackTrace()
                'MethodName = StackTrace.GetFrame(1).GetMethod().Name
                Return New StackFrame(1, False).GetMethod().Name
            Catch ex As Exception
            End Try

            Return ""
        End Function

        Public Sub Application_ThreadException(sender As Object, e As Threading.ThreadExceptionEventArgs)
            Try
                e.Exception.ToLog(True, "Application.ThreadException - MethodName: {0}".FormatWith(GetStackMethodName()))
            Catch ex As Exception
            End Try
        End Sub

        Public Sub CurrentDomain_FirstChanceException(sender As Object, e As Runtime.ExceptionServices.FirstChanceExceptionEventArgs)
            Try
                e.Exception.ToLog(True, "CurrentDomain.FirstChanceException - MethodName: {0}".FormatWith(GetStackMethodName()))
            Catch ex As Exception
            End Try
        End Sub

        Public Sub CurrentDomain_UnhandledException(sender As Object, e As UnhandledExceptionEventArgs)
            Try
                DirectCast(e.ExceptionObject, Exception).ToLog(True, "CurrentDomain.UnhandledException - MethodName: {0}, IsTerminating: {1}".FormatWith(GetStackMethodName(), e.IsTerminating))
            Catch ex As Exception
            End Try
        End Sub

        Public Sub Log(silent As Boolean, title As String, ByRef e As Exception)
            Log(silent, New Models.ErrorDetail(title, e))
        End Sub

        Public Sub Log(silent As Boolean, title As String, ByRef e As Net.WebException)
            Log(silent, New Models.ErrorDetail(title, e))
        End Sub

        Public Sub Log(message As String, silent As Boolean, title As String)
            If message.IsNotSet Then
                Return
            End If

            Log(silent, New Models.ErrorDetail(message, title))
        End Sub

        Public Sub Log(ByRef errorDetail As Models.ErrorDetail)
            Log(False, errorDetail)
        End Sub

        Public Sub Log(silent As Boolean, errorDetail As Models.ErrorDetail)
            Try
                _ErrorDetails.Add(errorDetail)

                For Each Current As LogErrorDetailHandler In _Handlers
                    If Current Is Nothing Then
                        Continue For
                    End If

                    Current(silent, errorDetail)
                Next
            Catch ex As Exception
            End Try
        End Sub

    End Module

End Namespace