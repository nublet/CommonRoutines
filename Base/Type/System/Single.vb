Namespace CommonRoutines.Type

    Public Module System_Single

        Public Function ToSingleDB(obj As Object) As Single
            Return ToSingleDB(obj, 0)
        End Function

        Public Function ToSingleDB(obj As Object, defaultValue As Single) As Single
            If IsNull(obj) Then
                Return defaultValue
            End If

            Dim Result As Single = 0

            If Single.TryParse(obj.ToString(), Result) Then
                Return Result
            End If

            Try
                Return CSng(obj)
            Catch ex As Exception
                Return defaultValue
            End Try
        End Function

    End Module

End Namespace