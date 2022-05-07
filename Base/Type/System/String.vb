Namespace CommonRoutines.Type

    Public Module System_String

        Public Function ToStringDB(obj As Object) As String
            Return ToStringDB(obj, "")
        End Function

        Public Function ToStringDB(obj As Object, defaultValue As String) As String
            If IsNull(obj) Then
                Return defaultValue
            End If

            Return obj.ToString()
        End Function

    End Module

End Namespace