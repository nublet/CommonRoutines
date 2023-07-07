Namespace CommonRoutines.Extensions

    Public Module System_ULong

        <Runtime.CompilerServices.Extension()> Public Function IsEqualTo(l As ULong, value As ULong) As Boolean
            Return l.Equals(value)
        End Function

        <Runtime.CompilerServices.Extension()> Public Function IsNotEqualTo(l As ULong, value As ULong) As Boolean
            Return Not l.Equals(value)
        End Function

    End Module

End Namespace