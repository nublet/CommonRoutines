Namespace CommonRoutines.Reflection

    Public Interface IFieldAccessor

        ReadOnly Property FieldInfo As System.Reflection.FieldInfo
        ReadOnly Property Name As String

        Function GetValue(source As Object) As Object

        Sub SetValue(source As Object, value As Object)

    End Interface

End Namespace