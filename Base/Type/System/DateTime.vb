Namespace CommonRoutines.Type

    Public Module System_DateTime

        Public Function ToDateTimeDB(obj As Object) As DateTime
            Return ToDateTimeDB(obj, Nothing)
        End Function

        Public Function ToDateTimeDB(obj As Object, defaultValue As DateTime) As DateTime
            If IsNullOrDefault(obj) Then
                Return defaultValue
            End If

            Return Convert.ToDateTime(obj.ToString(), GetCurrentCulture())
        End Function

    End Module

End Namespace