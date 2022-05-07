Namespace CommonRoutines.Controls.DataGridViewColumns

    Public Class DateCell
        Inherits DataGridViewTextBoxCell

        Private _ParentColumn As [Date] = Nothing

        Private Function GetParentColumn() As [Date]
            If _ParentColumn Is Nothing Then
                _ParentColumn = DirectCast(OwningColumn, [Date])
            End If

            Return _ParentColumn
        End Function

        Public Sub New()
            MyBase.New()

            ValueType = GetType(Date)
        End Sub

        Protected Overrides Function GetFormattedValue(value As Object, rowIndex As Integer, ByRef cellStyle As DataGridViewCellStyle, valueTypeConverter As System.ComponentModel.TypeConverter, formattedValueTypeConverter As System.ComponentModel.TypeConverter, context As DataGridViewDataErrorContexts) As Object
            Try
                Dim DateValue As Date = Type.ToDateTimeDB(value)

                If DateValue.ToOADate() = 0.0! Then
                    value = ""
                Else
                    value = DateValue.GetSQLString(GetParentColumn().DateFormat)
                End If
            Catch ex As Exception
                ex.ToLog(True)

                value = ""
            End Try

            Return MyBase.GetFormattedValue(value, rowIndex, cellStyle, valueTypeConverter, formattedValueTypeConverter, context)
        End Function

    End Class

    Public Class [Date]
        Inherits DataGridViewColumn

        Public Property DateFormat As String = ""

        Public Sub New()
            MyBase.New(New DateCell())
        End Sub

        Public Sub New(dateFormat As String)
            MyBase.New(New DateCell())

            _DateFormat = dateFormat
        End Sub

    End Class

End Namespace