Namespace CommonRoutines.Extensions

    Public Module System_Drawing_Image

        <Runtime.CompilerServices.Extension()> Public Function GetThumbnail(i As Image, size As Integer) As Image
            Return i.GetThumbnail(size, size)
        End Function

        <Runtime.CompilerServices.Extension()> Public Function GetThumbnail(i As Image, width As Integer, height As Integer) As Image
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

        <Runtime.CompilerServices.Extension()> Public Function GetForcedThumbnail(i As Image, desiredWidth As Integer, desiredHeight As Integer) As Image
            Dim sourceWidth As Integer = i.Width
            Dim sourceHeight As Integer = i.Height

            Dim sourceX As Integer = 0
            Dim sourceY As Integer = 0
            Dim destX As Integer = 0
            Dim destY As Integer = 0

            Dim nPercentW As Single = CSng(desiredWidth / sourceWidth)
            Dim nPercentH As Single = CSng(desiredHeight / sourceHeight)

            Dim nPercent As Single
            If nPercentH < nPercentW Then
                nPercent = nPercentH
                destX = Convert.ToInt16((desiredWidth - (sourceWidth * nPercent)) / 2)
            Else
                nPercent = nPercentW
                destY = Convert.ToInt16((desiredHeight - (sourceHeight * nPercent)) / 2)
            End If

            Dim destWidth As Integer = CInt((sourceWidth * nPercent))
            Dim destHeight As Integer = CInt((sourceHeight * nPercent))

            Dim Result As New Bitmap(desiredWidth, desiredHeight, Imaging.PixelFormat.Format24bppRgb)
            Result.SetResolution(i.HorizontalResolution, i.VerticalResolution)

            Using g As Graphics = Graphics.FromImage(Result)
                g.Clear(Color.Transparent)

                g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                g.DrawImage(i, New Rectangle(destX, destY, destWidth, destHeight), New Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel)
            End Using

            Result.MakeTransparent()

            Return Result
        End Function


    End Module

End Namespace