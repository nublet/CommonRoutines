Namespace Type

    Public Module System_Double

        Public Function ToDoubleDB(obj As Object) As Double
            Return ToDoubleDB(obj, 0.0!)
        End Function

        Public Function ToDoubleDB(obj As Object, defaultValue As Double) As Double
            If IsNull(obj) Then
                Return defaultValue
            End If

            Dim Result As Double = 0.0!

            If Double.TryParse(obj.ToString(), Result) Then
                Return Result
            End If

            Try
                Return CDbl(obj)
            Catch ex As Exception
                Return defaultValue
            End Try
        End Function

    End Module

End Namespace