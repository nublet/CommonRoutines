Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Namespace CommonRoutines.Controls

    Public Class CheckBox
        Inherits Windows.Forms.CheckBox

        Private _CheckedColor As Color = Color.MediumSlateBlue
        Private _UncheckedColor As Color = Color.Gray

        <Category("Common Routines")> Public Property CheckedColor As Color
            Get
                Return _CheckedColor
            End Get
            Set(value As Color)
                _CheckedColor = value
                Invalidate()
            End Set
        End Property

        <Category("Common Routines")> Public Property UncheckedColor As Color
            Get
                Return _UncheckedColor
            End Get
            Set(value As Color)
                _UncheckedColor = value
                Invalidate()
            End Set
        End Property

        Public Sub New()
            Padding = New Padding(10, 0, 0, 0)
            MinimumSize = New Size(0, 21)
        End Sub

#Region " Overrides "

        Protected Overrides Sub OnPaint(pevent As PaintEventArgs)
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias

            Dim BorderSize As Integer = Height - 4
            Dim CheckSize As Single = Height - 8.0!

            Dim RectBorder As New Rectangle(2, CInt((Height - BorderSize) / 2), BorderSize, BorderSize)
            Dim RectCheck As New RectangleF(RectBorder.X + ((RectBorder.Width - CheckSize) / 2), (Height - CheckSize) / 2, CheckSize, CheckSize)

            pevent.Graphics.Clear(BackColor)

            If Checked Then
                Using pen As New Pen(_CheckedColor, 1.6!)
                    pevent.Graphics.DrawRectangle(pen, RectBorder)
                End Using
                Using brush As New SolidBrush(_CheckedColor)
                    pevent.Graphics.FillRectangle(brush, RectCheck)
                End Using
            Else
                Using pen As New Pen(_UncheckedColor, 1.6!)
                    pevent.Graphics.DrawRectangle(pen, RectBorder)
                End Using
            End If

            Using brush As New SolidBrush(ForeColor)
                pevent.Graphics.DrawString(Text, Font, brush, BorderSize + 8, CSng((Height - TextRenderer.MeasureText(Text, Font).Height) / 2))
            End Using
        End Sub

#End Region

    End Class

End Namespace