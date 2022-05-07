Namespace CommonRoutines.Controls.DataGridViewColumns

    Public Class BooleanCell
        Inherits DataGridViewTextBoxCell

        Private ReadOnly _ColorCells As Boolean = False

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(colorCells As Boolean)
            MyBase.New()

            _ColorCells = colorCells
        End Sub

        Protected Overrides Function GetFormattedValue(value As Object, rowIndex As Integer, ByRef cellStyle As DataGridViewCellStyle, valueTypeConverter As System.ComponentModel.TypeConverter, formattedValueTypeConverter As System.ComponentModel.TypeConverter, context As DataGridViewDataErrorContexts) As Object
            If Type.ToBooleanDB(value) Then
                value = "Yes"
                If _ColorCells Then
                    cellStyle.BackColor = Drawing.Color.LightGreen
                End If
            Else
                value = "No"
                If _ColorCells Then
                    cellStyle.BackColor = Drawing.Color.LightPink
                End If
            End If

            Return MyBase.GetFormattedValue(value, rowIndex, cellStyle, valueTypeConverter, formattedValueTypeConverter, context)
        End Function

    End Class

    Public Class [Boolean]
        Inherits DataGridViewColumn

        Public Sub New()
            MyBase.New(New BooleanCell(True))
        End Sub

        Public Sub New(colorCells As Boolean)
            MyBase.New(New BooleanCell(colorCells))
        End Sub

    End Class

End Namespace