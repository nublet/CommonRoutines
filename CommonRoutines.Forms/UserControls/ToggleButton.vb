Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D

Namespace UserControls

    Public Class ToggleButton
        Inherits CheckBox

        Private _UncheckedBackColor As Color = Color.Gray
        Private _UncheckedToggleColor As Color = Color.Gainsboro
        Private _CheckedBackColor As Color = Color.MediumSlateBlue
        Private _CheckedToggleColor As Color = Color.WhiteSmoke
        Private _SolidStyle As Boolean = False

        <Category("Common Routines")> Public Property UncheckedBackColor As Color
            Get
                Return _UncheckedBackColor
            End Get
            Set(value As Color)
                _UncheckedBackColor = value
                Invalidate()
            End Set
        End Property

        <Category("Common Routines")> Public Property UncheckedToggleColor As Color
            Get
                Return _UncheckedToggleColor
            End Get
            Set(value As Color)
                _UncheckedToggleColor = value
                Invalidate()
            End Set
        End Property

        <Category("Common Routines")> Public Property CheckedBackColor As Color
            Get
                Return _CheckedBackColor
            End Get
            Set(value As Color)
                _CheckedBackColor = value
                Invalidate()
            End Set
        End Property

        <Category("Common Routines")> Public Property CheckedToggleColor As Color
            Get
                Return _CheckedToggleColor
            End Get
            Set(value As Color)
                _CheckedToggleColor = value
                Invalidate()
            End Set
        End Property

        <Category("Common Routines")> <DefaultValue(True)> Public Property SolidStyle As Boolean
            Get
                Return _SolidStyle
            End Get
            Set(value As Boolean)
                _SolidStyle = value
                Invalidate()
            End Set
        End Property

        Public Sub New()

        End Sub

        Private Function GetFigurePath() As GraphicsPath
            Dim ArcSize As Integer = Height - 1
            Dim LeftArc As New Rectangle(0, 0, ArcSize, ArcSize)
            Dim RightArc As New Rectangle(Width - ArcSize - 2, 0, ArcSize, ArcSize)

            Dim Result As New GraphicsPath()

            Result.StartFigure()
            Result.AddArc(LeftArc, 90, 180)
            Result.AddArc(RightArc, 270, 180)
            Result.CloseFigure()

            Return Result
        End Function

#Region " Overrides "

        Public Overrides Property Text As String
            Get
                Return MyBase.Text
            End Get
            Set(value As String)

            End Set
        End Property

        Protected Overrides Sub OnPaint(pevent As PaintEventArgs)
            Dim ToggleSize As Integer = Height - 5

            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias
            pevent.Graphics.Clear(Parent.BackColor)

            Using path As GraphicsPath = GetFigurePath()
                If Checked Then
                    If SolidStyle Then
                        Using brush As New SolidBrush(_CheckedBackColor)
                            pevent.Graphics.FillPath(brush, path)
                        End Using
                    Else
                        Using pen As New Pen(_CheckedBackColor)
                            pevent.Graphics.DrawPath(pen, path)
                        End Using
                    End If

                    Using brush As New SolidBrush(_CheckedToggleColor)
                        pevent.Graphics.FillEllipse(brush, New Rectangle(Width - Height + 1, 2, ToggleSize, ToggleSize))
                    End Using
                Else
                    If SolidStyle Then
                        Using brush As New SolidBrush(_UncheckedBackColor)
                            pevent.Graphics.FillPath(brush, path)
                        End Using
                    Else
                        Using pen As New Pen(_UncheckedBackColor)
                            pevent.Graphics.DrawPath(pen, path)
                        End Using
                    End If

                    Using brush As New SolidBrush(_UncheckedToggleColor)
                        pevent.Graphics.FillEllipse(brush, New Rectangle(2, 2, ToggleSize, ToggleSize))
                    End Using
                End If
            End Using
        End Sub

#End Region

    End Class

End Namespace