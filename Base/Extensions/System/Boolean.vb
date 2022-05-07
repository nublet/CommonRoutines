Namespace CommonRoutines.Extensions

    Public Module System_Boolean

        <Runtime.CompilerServices.Extension()> Public Function IsEqualTo(b As Boolean, value As Boolean) As Boolean
            Return b.Equals(value)
        End Function

        <Runtime.CompilerServices.Extension()> Public Function IsNotEqualTo(b As Boolean, value As Boolean) As Boolean
            Return Not b.Equals(value)
        End Function

        <Runtime.CompilerServices.Extension()> Public Function GetString(b As Boolean) As String
            Return b.GetString("True", "False")
        End Function

        <Runtime.CompilerServices.Extension()> Public Function GetString(b As Boolean, trueValue As String, falseValue As String) As String
            If b Then
                Return trueValue
            End If

            Return falseValue
        End Function

    End Module

End Namespace