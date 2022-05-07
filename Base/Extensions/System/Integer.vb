Namespace CommonRoutines.Extensions

    Public Module System_Integer

        <Runtime.CompilerServices.Extension()> Public Function GetTimeString(seconds As Integer) As String
            Return CLng(seconds).GetTimeString()
        End Function

        <Runtime.CompilerServices.Extension()> Public Function IsEqualTo(i As Integer, value As Integer) As Boolean
            Return i.Equals(value)
        End Function

        <Runtime.CompilerServices.Extension()> Public Function IsNotEqualTo(i As Integer, value As Integer) As Boolean
            Return Not i.Equals(value)
        End Function

    End Module

End Namespace