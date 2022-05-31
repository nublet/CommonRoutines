Namespace CommonRoutines.Extensions

    Public Module System_Integer

        <Runtime.CompilerServices.Extension()> Public Function GetWithOrdinalSuffix(i As Integer) As String
            If i <= 0 Then
                Return i.ToString()
            End If

            Dim Value As String = i.ToString()

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