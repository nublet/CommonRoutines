Imports System.Runtime.InteropServices

Namespace CommonRoutines.NativeRoutines

    Public Module _Shared

        Public Function GetIdleTime() As Long
            Try
                Dim LastInput As New Structures.LASTINPUTINFO

                LastInput.cbSize = CUInt(Marshal.SizeOf(LastInput))

                If GetLastInputInfo(LastInput) Then
                    Dim el As Long = LastInput.dwTime
                    Dim ui As Long = (Environment.TickCount - el)
                    If ui < 0 Then
                        ui = ui + UInteger.MaxValue + 1
                    End If

                    Return ui
                End If
            Catch ex As Exception
                ex.ToLog(True)
            End Try

            Return -1
        End Function

        Public Function GetIdleTimeSpan() As TimeSpan
            Return New TimeSpan(GetIdleTime() * 10000)
        End Function

    End Module

End Namespace