Namespace UserControls

    Public Interface ITab

        Property IsSelected As Boolean

        ReadOnly Property Button As Button
        ReadOnly Property Name As String
        ReadOnly Property OrderButton As Integer
        ReadOnly Property Text As String
        ReadOnly Property UserControl As UserControl

        Sub LoadArea()

    End Interface

End Namespace