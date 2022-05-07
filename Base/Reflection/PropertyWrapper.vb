Namespace CommonRoutines.Reflection

    Public Class PropertyWrapper(Of TObject, TValue)
        Implements IPropertyAccessor

        Private ReadOnly _GetMethod As Func(Of TObject, TValue)
        Private ReadOnly _PropertyInfo As System.Reflection.PropertyInfo
        Private ReadOnly _SetMethod As Action(Of TObject, TValue)

        Public ReadOnly Property CanRead As Boolean Implements IPropertyAccessor.CanRead
            Get
                Return _PropertyInfo.CanRead
            End Get
        End Property

        Public ReadOnly Property CanWrite As Boolean Implements IPropertyAccessor.CanWrite
            Get
                Return _PropertyInfo.CanWrite
            End Get
        End Property

        Public ReadOnly Property PropertyInfo As System.Reflection.PropertyInfo Implements IPropertyAccessor.PropertyInfo
            Get
                Return _PropertyInfo
            End Get
        End Property

        Public ReadOnly Property Name As String Implements IPropertyAccessor.Name
            Get
                Return _PropertyInfo.Name
            End Get
        End Property

        Public Sub New(ByRef propertyInfo As System.Reflection.PropertyInfo)
            _PropertyInfo = propertyInfo

            If CanRead Then
                Dim mi As System.Reflection.MethodInfo = _PropertyInfo.GetGetMethod(True)

                If mi IsNot Nothing Then
                    _GetMethod = DirectCast([Delegate].CreateDelegate(GetType(Func(Of TObject, TValue)), mi), Func(Of TObject, TValue))
                End If
            End If

            If CanWrite Then
                Dim mi As System.Reflection.MethodInfo = _PropertyInfo.GetSetMethod(True)

                If mi IsNot Nothing Then
                    _SetMethod = DirectCast([Delegate].CreateDelegate(GetType(Action(Of TObject, TValue)), mi), Action(Of TObject, TValue))
                End If
            End If
        End Sub

        Public Function GetValue(source As Object) As Object Implements IPropertyAccessor.GetValue
            If _GetMethod Is Nothing Then
                Return Nothing
            End If

            If source Is Nothing Then
                Return Nothing
            End If

            Return _GetMethod(DirectCast(source, TObject))
        End Function

        Public Sub SetValue(source As Object, value As Object) Implements IPropertyAccessor.SetValue
            If _SetMethod Is Nothing Then
                Return
            End If

            If source Is Nothing Then
                Return
            End If

            _SetMethod(DirectCast(source, TObject), Type.GetValue(Of TValue)(value))
        End Sub

    End Class

End Namespace