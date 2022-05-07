Namespace CommonRoutines.Type

    Public Module System_Boolean

        Public Function ToBooleanDB(obj As Object) As Boolean
            Return ToBooleanDB(obj, False)
        End Function

        Public Function ToBooleanDB(obj As Object, defaultValue As Boolean) As Boolean
            If IsNull(obj) Then
                Return defaultValue
            End If

            Dim Value As Boolean = False

            If Boolean.TryParse(obj.ToString(), Value) Then
                Return Value
            End If

            Try
                Return CBool(obj)
            Catch ex As Exception
                Return defaultValue
            End Try
        End Function

    End Module

End Namespace