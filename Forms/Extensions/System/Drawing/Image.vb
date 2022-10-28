Namespace CommonRoutines.Extensions

    Public Module System_Drawing_Image

        <Runtime.CompilerServices.Extension()> Public Function GetThumbnail(i As System.Drawing.Image, size As Integer) As Drawing.Image
            Return i.GetThumbnail(size, size)
        End Function

        <Runtime.CompilerServices.Extension()> Public Function GetThumbnail(i As System.Drawing.Image, width As Integer, height As Integer) As Drawing.Image
            If i.Width <= width AndAlso i.Height < height Then
                Return i
            End If
            If i.Width > i.Height Then
                If i.Width > width Then
                    height = CInt(width * i.Height / i.Width)
                End If
            Else
                If i.Height > height Then
                    width = CInt(height * i.Width / i.Height)
                End If
            End If
            Return i.GetThumbnailImage(width, height, Nothing, IntPtr.Zero)
        End Function

        <Runtime.CompilerServices.Extension()> Public Function GetForcedThumbnail(i As Drawing.Image, desiredWidth As Integer, desiredHeight As Integer) As Drawing.Image
            Try
                Dim NewHeight As Integer
                Dim NewX As Integer
                Dim NewWidth As Integer
                Dim NewY As Integer

                Dim OriginalHeight As Integer = i.Height
                Dim OriginalWidth As Integer = i.Width

                If OriginalHeight > desiredHeight OrElse OriginalWidth > desiredWidth Then
                    Dim RatioWH As Double = OriginalWidth / OriginalHeight

                    If OriginalWidth >= OriginalHeight Then
                        NewWidth = desiredWidth
                        NewHeight = CInt(desiredHeight / RatioWH)
                    Else
                        NewHeight = desiredHeight
                        NewWidth = CInt(desiredWidth * RatioWH)
                    End If
                Else
                    NewHeight = OriginalHeight
                    NewWidth = OriginalWidth
                End If

                NewX = CInt((desiredWidth - NewWidth) / 2)
                NewY = CInt((desiredHeight - NewHeight) / 2)

                Dim Result As New Drawing.Bitmap(desiredWidth, desiredHeight, Drawing.Imaging.PixelFormat.Format24bppRgb)

                Using g As Drawing.Graphics = Drawing.Graphics.FromImage(Result)
                    g.Clear(Drawing.Color.White)
                    g.InterpolationMode = Drawing.Drawing2D.InterpolationMode.NearestNeighbor
                    g.DrawImage(i, New Drawing.Rectangle(NewX, NewY, NewWidth, NewHeight), New Drawing.Rectangle(0, 0, OriginalWidth, OriginalHeight), Drawing.GraphicsUnit.Pixel)
                End Using

                Return Result
            Catch ex As Exception
                ex.ToLog(True)
            End Try

            Return i.GetThumbnail(desiredWidth, desiredHeight)
        End Function


    End Module

End Namespace