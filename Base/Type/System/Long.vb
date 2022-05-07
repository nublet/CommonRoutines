Namespace CommonRoutines.Type

    Public Module System_Long

        Public Function ToLongDB(obj As Object) As Long
            Return ToLongDB(obj, 0)
        End Function

        Public Function ToLongDB(obj As Object, defaultValue As Long) As Long
            If IsNull(obj) Then
                Return defaultValue
            End If

            Dim Result As Long = 0

            If Long.TryParse(obj.ToString(), Result) Then
                Return Result
            End If

            Try
                Return CLng(obj)
            Catch ex As Exception
                Return defaultValue
            End Try
        End Function

    End Module

End Namespace