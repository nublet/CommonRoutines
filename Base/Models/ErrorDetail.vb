Namespace CommonRoutines.Models

    Public Class ErrorDetail

        Private ReadOnly _ErrorDate As DateTime = Nothing
        Private ReadOnly _Exception As Exception = Nothing
        Private ReadOnly _Message As String = ""
        Private ReadOnly _Title As String = ""

        Private _Details As String = ""

        Public ReadOnly Property ErrorDate As DateTime
            Get
                Return _ErrorDate
            End Get
        End Property

        Public ReadOnly Property Details As String
            Get
                Return _Details
            End Get
        End Property

        Public ReadOnly Property Exception As Exception
            Get
                Return _Exception
            End Get
        End Property

        Public ReadOnly Property Message As String
            Get
                Return _Message
            End Get
        End Property

        Public ReadOnly Property Title As String
            Get
                Return _Title
            End Get
        End Property

        Public Sub New(title As String, ByRef exception As Exception)
            _ErrorDate = GetCurrentDate()
            _Exception = exception
            _Message = exception.Message
            _Title = title

            CheckDetails()
        End Sub

        Public Sub New(title As String, ByRef webException As Net.WebException)
            _ErrorDate = GetCurrentDate()
            _Exception = webException
            _Message = webException.Message
            _Title = title

            CheckDetails()
        End Sub

        Public Sub New(message As String, title As String)
            _ErrorDate = GetCurrentDate()
            _Exception = Nothing
            _Message = message
            _Title = title

            CheckDetails()
        End Sub

        Private Sub CheckDetails()
            _Details = "{0}, Version: {1}".FormatWith(_ErrorDate.GetSQLString("dd MMMM yyyy - HH:mm:ss"), Settings.ProductVersion)

            If Title.IsSet() Then
                _Details.Append(Environment.NewLine, Title.GetCleanLogLine(Settings.Indent, "Title: "))
            End If

            If _Exception Is Nothing Then
                _Details.Append(Environment.NewLine, _Message.GetCleanLogLine(Settings.Indent, "Message: "))
            Else
                _Details.Append(Environment.NewLine, _Exception.GetErrorString(Settings.Indent))
            End If
        End Sub

    End Class

End Namespace