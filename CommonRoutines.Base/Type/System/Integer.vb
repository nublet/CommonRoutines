Namespace Type

    Public Module System_Integer

        Public Function ToIntegerDB(obj As Object) As Integer
            Return ToIntegerDB(obj, -1)
        End Function

        Public Function ToIntegerDB(obj As Object, defaultValue As Integer) As Integer
            If IsNull(obj) Then
                Return defaultValue
            End If

            Dim Result As Integer = defaultValue

            If Integer.TryParse(obj.ToString(), Result) Then
                Return Result
            End If

            Try
                Return CInt(obj)
            Catch ex As Exception
                Return defaultValue
            End Try
        End Function

    End Module

End Namespace