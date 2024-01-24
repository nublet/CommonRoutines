Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D

Namespace UserControls

    Public Class Button
        Inherits System.Windows.Forms.Button

        Private _BorderColor As Color = Color.PaleVioletRed
        Private _BorderRadius As Integer = 40
        Private _BorderSize As Integer = 0

        <Category("Common Routines")> Public Property BorderColor As Color
            Get
                Return _BorderColor
            End Get
            Set(value As Color)
                _BorderColor = value
                Invalidate()
            End Set
        End Property

        <Category("Common Routines")> Public Property BorderRadius As Integer
            Get
                Return _BorderRadius
            End Get
            Set(value As Integer)
                _BorderRadius = value
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

        Public Sub New()
            FlatStyle = FlatStyle.Flat
            FlatAppearance.BorderSize = 0
            Size = New Size(150, 40)
            BackColor = Color.MediumSlateBlue
            ForeColor = Color.White

            AddHandler Resize, AddressOf Me_Resize
        End Sub

        Private Sub Me_Resize(sender As Object, e As EventArgs)
            If _BorderRadius > Height Then
                BorderRadius = Height
            End If
        End Sub

        Private Sub Parent_BackColorChanged(sender As Object, e As EventArgs)
            Invalidate()
        End Sub

#Region " Overrides "

        <Browsable(True)> <Category("Common Routines")> <EditorBrowsable(EditorBrowsableState.Always)> Public Overrides Property BackColor As Color
            Get
                Return MyBase.BackColor
            End Get
            Set(value As Color)
                MyBase.BackColor = value
            End Set
        End Property

        <Browsable(True)> <Category("Common Routines")> <EditorBrowsable(EditorBrowsableState.Always)> Public Overrides Property Font As Font
            Get
                Return MyBase.Font
            End Get
            Set(value As Font)
                MyBase.Font = value
            End Set
        End Property

        <Browsable(True)> <Category("Common Routines")> <EditorBrowsable(EditorBrowsableState.Always)> Public Overrides Property ForeColor As Color
            Get
                Return MyBase.ForeColor
            End Get
            Set(value As Color)
                MyBase.ForeColor = value
            End Set
        End Property

        Protected Overrides Sub OnHandleCreated(e As EventArgs)
            MyBase.OnHandleCreated(e)

            AddHandler Parent.BackColorChanged, AddressOf Parent_BackColorChanged
        End Sub

        Protected Overrides Sub OnPaint(pevent As PaintEventArgs)
            MyBase.OnPaint(pevent)

            Dim RectSurface As Rectangle = ClientRectangle
            Dim RectBorder As Rectangle = Rectangle.Inflate(RectSurface, -_BorderSize, -_BorderSize)
            Dim SmoothSize As Integer = 2
            If _BorderSize > 0 Then
                SmoothSize = _BorderSize
            End If

            If _BorderRadius > 2 Then
                pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias

                Using path As GraphicsPath = GetFigurePath(_BorderRadius, RectSurface)
                    Region = New Region(path)

                    Using pen As New Pen(Parent.BackColor, SmoothSize)
                        pevent.Graphics.DrawPath(pen, path)
                    End Using
                End Using

                If _BorderSize >= 1 Then
                    Using path As GraphicsPath = GetFigurePath(_BorderRadius - _BorderSize, RectBorder)
                        Using pen As New Pen(_BorderColor, _BorderSize)
                            pevent.Graphics.DrawPath(pen, path)
                        End Using
                    End Using
                End If
            Else
                pevent.Graphics.SmoothingMode = SmoothingMode.None

                Region = New Region(RectSurface)

                If _BorderSize >= 1 Then
                    Using pen As New Pen(_BorderColor, _BorderSize)
                        pen.Alignment = PenAlignment.Inset

                        pevent.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1)
                    End Using
                End If
            End If
        End Sub

#End Region

#Region " Shared "

        Private Shared Function GetFigurePath(radius As Integer, rect As Rectangle) As GraphicsPath
            Dim CurveSize As Single = radius * 2.0!

            Dim Result As New GraphicsPath()

            Result.StartFigure()
            Result.AddArc(rect.X, rect.Y, CurveSize, CurveSize, 180, 90)
            Result.AddArc(rect.Right - CurveSize, rect.Y, CurveSize, CurveSize, 270, 90)
            Result.AddArc(rect.Right - CurveSize, rect.Bottom - CurveSize, CurveSize, CurveSize, 0, 90)
            Result.AddArc(rect.X, rect.Bottom - CurveSize, CurveSize, CurveSize, 90, 90)
            Result.CloseFigure()

            Return Result
        End Function

#End Region

    End Class

End Namespace