Namespace CommonRoutines.Type

    Public Module _Shared

        Public Function GetValue(Of T)(obj As Object) As T
            Return GetValue(Of T)(obj, GetType(T).Name)
        End Function

        Public Function GetValue(Of T)(obj As Object, typeName As String) As T
            Return DirectCast(GetValue(obj, typeName), T)
        End Function

        Public Function GetValue(obj As Object, typeName As String) As Object
            Select Case typeName
                Case "Boolean"
                    Return ToBooleanDB(obj)
                Case "Byte[]", "Byte()"
                    Return ToByteArrayDB(obj)
                Case "DateTime"
                    Return ToDateTimeDB(obj)
                Case "Double"
                    Return ToDoubleDB(obj)
                Case "Integer", "Int32"
                    Return ToIntegerDB(obj)
                Case "Long", "Int64"
                    Return ToLongDB(obj)
                Case "Object"
                    Return obj
                Case "Single"
                    Return ToSingleDB(obj)
                Case "String"
                    Return ToStringDB(obj)
            End Select

            If IsNull(obj) Then
                Return Nothing
            End If

            Return obj
        End Function

        Public Function IsNull(obj As Object) As Boolean
            If obj Is Nothing Then
                Return True
            End If

            If Equals(obj, Nothing) Then
                Return True
            End If

            If obj.ToString().IsNotSet() Then
                Return True
            End If

            Return False
        End Function

        Public Function IsNullOrDefault(obj As Object) As Boolean
            If IsNull(obj) Then
                Return True
            End If

            Dim Type As System.Type = obj.GetType()

            If Nullable.GetUnderlyingType(Type) IsNot Nothing Then
                Return False
            End If

            Return Type.IsDefault(obj)
        End Function

    End Module

End Namespace