Imports System.Drawing.Drawing2D

Namespace CommonRoutines.Controls

    Public Class MenuRenderer
        Inherits ToolStripProfessionalRenderer

        Private ReadOnly _PrimaryColor As Color
        Private ReadOnly _TextColor As Color
        Private ReadOnly _ArrowThickness As Integer

        Public Sub New(isMainMenu As Boolean, primaryColor As Color, textColor As Color)
            MyBase.New(New MenuColorTable())

            _PrimaryColor = primaryColor
            _TextColor = textColor

            If isMainMenu Then
                _ArrowThickness = 3
            Else
                _ArrowThickness = 2
            End If
        End Sub

#Region " Overrides "

        Protected Overrides Sub OnRenderArrow(e As ToolStripArrowRenderEventArgs)
            Dim ArrowColor As Color = _TextColor
            Dim ArrowSize As New Size(5, 12)

            If e.Item.Selected Then
                ArrowColor = UITheme.SelectionForeColor
            End If

            Dim Rect As New Rectangle(e.ArrowRectangle.Location.X, CInt((e.ArrowRectangle.Height - ArrowSize.Height) / 2), ArrowSize.Width, ArrowSize.Height)

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias

            Using path As New GraphicsPath()
                path.AddLine(Rect.Left, Rect.Top, Rect.Right, CInt(Rect.Top + Rect.Height / 2))
                path.AddLine(Rect.Right, CInt(Rect.Top + Rect.Height / 2), Rect.Left, Rect.Top + Rect.Height)

                Using pen As New Pen(ArrowColor, _ArrowThickness)
                    e.Graphics.DrawPath(pen, path)
                End Using
            End Using
        End Sub

        Protected Overrides Sub OnRenderItemText(e As ToolStripItemTextRenderEventArgs)
            MyBase.OnRenderItemText(e)

            If e.Item.Selected Then
                e.Item.ForeColor = UITheme.SelectionForeColor
            Else
                e.Item.ForeColor = _TextColor
            End If
        End Sub

#End Region

    End Class

End Namespace