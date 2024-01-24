Namespace Type

    Public Module System_Date

        Public Function ToDateTimeDB(obj As Object) As Date
            Return ToDateTimeDB(obj, Nothing)
        End Function

        Public Function ToDateTimeDB(obj As Object, defaultValue As Date) As Date
            If IsNullOrDefault(obj) Then
                Return defaultValue
            End If

            Return Convert.ToDateTime(obj.ToString(), GetCurrentCulture())
        End Function

    End Module

End Namespace