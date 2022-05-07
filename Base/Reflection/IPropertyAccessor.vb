Namespace CommonRoutines.Reflection

    Public Interface IPropertyAccessor

        ReadOnly Property CanRead As Boolean
        ReadOnly Property CanWrite As Boolean
        ReadOnly Property Name As String
        ReadOnly Property PropertyInfo As System.Reflection.PropertyInfo

        Function GetValue(source As Object) As Object

        Sub SetValue(source As Object, value As Object)

    End Interface

End Namespace