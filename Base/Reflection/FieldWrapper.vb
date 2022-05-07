Namespace CommonRoutines.Reflection

    Public Class FieldWrapper(Of TObject, TValue)
        Implements IFieldAccessor

        Private ReadOnly _FieldInfo As System.Reflection.FieldInfo

        Public ReadOnly Property FieldInfo As System.Reflection.FieldInfo Implements IFieldAccessor.FieldInfo
            Get
                Return _FieldInfo
            End Get
        End Property

        Public ReadOnly Property Name As String Implements IFieldAccessor.Name
            Get
                Return _FieldInfo.Name
            End Get
        End Property

        Public Sub New(ByRef fieldInfo As System.Reflection.FieldInfo)
            _FieldInfo = fieldInfo
        End Sub

        Public Function GetValue(source As Object) As Object Implements IFieldAccessor.GetValue
            If source Is Nothing Then
                Return Nothing
            End If

            Return _FieldInfo.GetValue(source)
        End Function

        Public Sub SetValue(source As Object, value As Object) Implements IFieldAccessor.SetValue
            If source Is Nothing Then
                Return
            End If

            _FieldInfo.SetValue(source, value)
        End Sub

    End Class

End Namespace