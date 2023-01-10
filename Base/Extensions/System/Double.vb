Namespace CommonRoutines.Extensions

    Public Module System_Double

        <Runtime.CompilerServices.Extension()> Public Function GetWithOrdinalSuffix(d As Double) As String
            If d <= 0 Then
                Return d.ToString()
            End If

            Dim Value As String = d.ToString()

            If Value.EndsWith("11") Then
                Return "{0}th".FormatWith(Value)
            End If
            If Value.EndsWith("12") Then
                Return "{0}th".FormatWith(Value)
            End If
            If Value.EndsWith("13") Then
                Return "{0}th".FormatWith(Value)
            End If
            If Value.EndsWith("1") Then
                Return "{0}st".FormatWith(Value)
            End If
            If Value.EndsWith("2") Then
                Return "{0}nd".FormatWith(Value)
            End If
            If Value.EndsWith("3") Then
                Return "{0}rd".FormatWith(Value)
            End If

            Return "{0}th".FormatWith(Value)
        End Function

        <Runtime.CompilerServices.Extension()> Public Function GetTimeString(seconds As Double) As String
            Return CLng(seconds).GetTimeString()
        End Function

        <Runtime.CompilerServices.Extension()> Public Function IsEqualTo(d As Double, value As Double) As Boolean
            Return d.Equals(value)
        End Function

        <Runtime.CompilerServices.Extension()> Public Function IsNotEqualTo(d As Double, value As Double) As Boolean
            Return Not d.Equals(value)
        End Function

    End Module

End Namespace