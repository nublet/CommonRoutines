Namespace Type

    Public Module System_Byte

        Public Function ToByteArrayDB(obj As Object) As Byte()
            Return ToByteArrayDB(obj, {})
        End Function

        Public Function ToByteArrayDB(obj As Object, defaultValue As Byte()) As Byte()
            If IsNullOrDefault(obj) Then
                Return defaultValue
            End If

            Return DirectCast(obj, Byte())
        End Function

    End Module

End Namespace