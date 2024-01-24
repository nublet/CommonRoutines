Namespace Models

    Public Class ComboItem
        Inherits GenericChanged

        Private _Display As String
        Private _Value As String

        Public Property Display() As String
            Get
                Return _Display
            End Get
            Set(value As String)
                SetProperty(_Display, value, "Display")
            End Set
        End Property

        Public Property Value() As String
            Get
                Return _Value
            End Get
            Set(value As String)
                SetProperty(_Value, value, "Value")
            End Set
        End Property

        Public Sub New()

        End Sub

        Public Sub New(value As String)
            _Value = value
            _Display = value
        End Sub

        Public Sub New(value As String, display As String)
            _Value = value
            _Display = display
        End Sub

    End Class

End Namespace