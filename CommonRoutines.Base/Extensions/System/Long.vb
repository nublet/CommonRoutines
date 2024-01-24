Namespace Extensions

    Public Module System_Long

        <Runtime.CompilerServices.Extension()> Public Function GetTimeString(seconds As Long) As String
            Try
                Dim dblDays As Long = 0
                Dim dblHours As Long = 0
                Dim dblMinutes As Long = 0
                Dim dblSeconds As Long = seconds

                dblMinutes += dblSeconds \ 60
                dblSeconds = dblSeconds Mod 60
                dblHours += dblMinutes \ 60
                dblMinutes = dblMinutes Mod 60
                dblDays = dblHours \ 24
                dblHours = dblHours Mod 24

                If dblMinutes <= 0 Then
                    Return "{0}s".FormatWith(Format(dblSeconds, "##00"))
                ElseIf dblHours <= 0 Then
                    Return "{0}m:{1}s".FormatWith(Format(dblMinutes, "##00"), Format(dblSeconds, "##00"))
                ElseIf dblDays <= 0 Then
                    Return "{0}h:{1}m:{2}s".FormatWith(Format(dblHours, "##00"), Format(dblMinutes, "##00"), Format(dblSeconds, "##00"))
                Else
                    Return "{0}d:{1}h:{2}m:{3}s".FormatWith(Format(dblDays, "##00"), Format(dblHours, "##00"), Format(dblMinutes, "##00"), Format(dblSeconds, "##00"))
                End If
            Catch ex As Exception
                ex.ToLog()
            End Try

            Return ""
        End Function

        <Runtime.CompilerServices.Extension()> Public Function IsEqualTo(l As Long, value As Long) As Boolean
            Return l.Equals(value)
        End Function

        <Runtime.CompilerServices.Extension()> Public Function IsNotEqualTo(l As Long, value As Long) As Boolean
            Return Not l.Equals(value)
        End Function

    End Module

End Namespace