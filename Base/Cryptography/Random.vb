Imports System.Security.Cryptography

Namespace CommonRoutines.Cryptography

    Public Class Random
        Inherits RandomNumberGenerator

        Private Shared _RandomNumberGenerator As RandomNumberGenerator = Nothing

        Public Sub New()
            _RandomNumberGenerator = Create()
        End Sub

        Public Overrides Sub GetBytes(data() As Byte)
            _RandomNumberGenerator.GetBytes(data)
        End Sub

        Public Function NextDouble() As Double
            Dim Buffer As Byte() = New Byte(4) {}
            _RandomNumberGenerator.GetBytes(Buffer)
            Return CDbl(BitConverter.ToUInt32(Buffer, 0)) / UInt32.MaxValue
        End Function

        Public Function [Next](minValue As Integer, maxValue As Integer) As Integer
            Dim Range As Long = CLng(maxValue - minValue)
            Return CInt(CLng(Math.Floor(NextDouble() * Range)) + minValue)
        End Function

        Public Function [Next]() As Integer
            Return [Next](0, Int32.MaxValue)
        End Function

        Public Function [Next](maxValue As Integer) As Integer
            Return [Next](0, maxValue)
        End Function

    End Class

End Namespace