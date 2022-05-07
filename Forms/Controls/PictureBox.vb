Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Namespace CommonRoutines.Controls

    Public Class PictureBox
        Inherits Windows.Forms.PictureBox

        Private _BorderColor As Color = Color.RoyalBlue
        Private _BorderColor2 As Color = Color.HotPink
        Private _BorderCapStyle As DashCap = DashCap.Flat
        Private _BorderLineStyle As DashStyle = DashStyle.Solid
        Private _BorderSize As Integer = 2
        Private _GradientAngle As Single = 50.0!

        <Category("Common Routines")> Public Property BorderColor As Color
            Get
                Return _BorderColor
            End Get
            Set(value As Color)
                _BorderColor = value
                Invalidate()
            End Set
        End Property

        <Category("Common Routines")> Public Property BorderColor2 As Color
            Get
                Return _BorderColor2
            End Get
            Set(value As Color)
                _BorderColor2 = value
                Invalidate()
            End Set
        End Property

        <Category("Common Routines")> Public Property BorderCapStyle As DashCap
            Get
                Return _BorderCapStyle
            End Get
            Set(value As DashCap)
                _BorderCapStyle = value
            End Set
        End Property

        <Category("Common Routines")> Public Property BorderLineStyle As DashStyle
            Get
                Return _BorderLineStyle
            End Get
            Set(value As DashStyle)
                _BorderLineStyle = value
                Invalidate()
            End Set
        End Property

        <Category("Common Routines")> Public Property BorderSize As Integer
            Get
                Return _BorderSize
            End Get
            Set(value As Integer)
                _BorderSize = value
                Invalidate()
            End Set
        End Property

        <Category("Common Routines")> Public Property GradientAngle As Single
            Get
                Return _GradientAngle
            End Get
            Set(value As Single)
                _GradientAngle = value
                Invalidate()
            End Set
        End Property

        Public Sub New()
            Size = New Size(100, 100)
            SizeMode = PictureBoxSizeMode.Zoom
        End Sub

#Region " Overrides "

        Protected Overrides Sub OnPaint(pe As PaintEventArgs)
            MyBase.OnPaint(pe)

            Dim RectSmooth As Rectangle = Rectangle.Inflate(ClientRectangle, -1, -1)
            Dim RectBorder As Rectangle = Rectangle.Inflate(RectSmooth, -_BorderSize, -_BorderSize)

            Dim SmoothSize As Integer = 1
            If _BorderSize > 0 Then
                SmoothSize = _BorderSize * 3
            End If

            Using path As New GraphicsPath()
                path.AddEllipse(RectSmooth)

                Region = New Region(path)
            End Using

            pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias

            Using penSmooth As New Pen(Parent.BackColor, SmoothSize)
                pe.Graphics.DrawEllipse(penSmooth, RectSmooth)
            End Using

            If _BorderSize > 0 Then
                Using gradient As New LinearGradientBrush(RectBorder, _BorderColor, _BorderColor2, _GradientAngle)
                    Using pen As New Pen(gradient, _BorderSize)
                        pen.DashCap = _BorderCapStyle
                        pen.DashStyle = _BorderLineStyle

                        pe.Graphics.DrawEllipse(pen, RectBorder)
                    End Using
                End Using
            End If
        End Sub

        Protected Overrides Sub OnResize(e As EventArgs)
            MyBase.OnResize(e)

            Size = New Size(Width, Width)
        End Sub

#End Region

    End Class

End Namespace