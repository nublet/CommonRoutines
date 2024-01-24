Namespace Type

    Public Module System_Decimal

        Public Function ToDecimalDB(obj As Object) As Decimal
            Return ToDecimalDB(obj, 0)
        End Function

        Public Function ToDecimalDB(obj As Object, defaultValue As Decimal) As Decimal
            If IsNull(obj) Then
                Return defaultValue
            End If

            Dim Result As Decimal = 0

            If Decimal.TryParse(obj.ToString(), Result) Then
                Return Result
            End If

            Try
                Return CDec(obj)
            Catch ex As Exception
                Return defaultValue
            End Try
        End Function

    End Module

End Namespace