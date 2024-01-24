Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D

Namespace UserControls

    Public Class DateTimePicker
        Inherits System.Windows.Forms.DateTimePicker

        Private Const ArrowIconWidth As Integer = 17
        Private Const CalendarIconWidth As Integer = 34

        Private _CalendarIcon As Image = Nothing
        Private _IsDroppedDown As Boolean = False
        Private _IconButtonArea As RectangleF = Nothing

        Private _BorderColor As Color = Color.PaleVioletRed
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
            SetStyle(ControlStyles.UserPaint, True)

            Font = New Font(Font.Name, 9.5!)

            _CalendarIcon = GetResourceImage(My.Resources.calendarLight)
        End Sub

        Private Function GetIconButtonWidth() As Integer
            Dim TextWidth As Integer = TextRenderer.MeasureText(Text, Font).Width

            If TextWidth <= (Width - (CalendarIconWidth + 20)) Then
                Return CalendarIconWidth
            End If

            Return ArrowIconWidth
        End Function

#Region " Overrides "

        <Browsable(True)> <Category("Common Routines")> <EditorBrowsable(EditorBrowsableState.Always)> Public Overrides Property BackColor As Color
            Get
                Return MyBase.BackColor
            End Get
            Set(value As Color)
                If value.GetBrightness() >= 0.6! Then
                    _CalendarIcon = GetResourceImage(My.Resources.calendarDark)
                Else
                    _CalendarIcon = GetResourceImage(My.Resources.calendarLight)
                End If

                MyBase.BackColor = value
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

        Protected Overrides Sub OnCloseUp(eventargs As EventArgs)
            MyBase.OnCloseUp(eventargs)

            _IsDroppedDown = False
        End Sub

        Protected Overrides Sub OnDropDown(eventargs As EventArgs)
            MyBase.OnDropDown(eventargs)

            _IsDroppedDown = True
        End Sub

        Protected Overrides Sub OnHandleCreated(e As EventArgs)
            MyBase.OnHandleCreated(e)

            Dim IconWidth As Integer = GetIconButtonWidth()
            _IconButtonArea = New RectangleF(Width - IconWidth, 0, IconWidth, Height)
        End Sub

        Protected Overrides Sub OnKeyPress(e As KeyPressEventArgs)
            MyBase.OnKeyPress(e)

            e.Handled = True
        End Sub

        Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
            MyBase.OnMouseMove(e)

            If _IconButtonArea.Contains(e.Location) Then
                Cursor = Cursors.Hand
            Else
                Cursor = Cursors.Default
            End If
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Using g As Graphics = CreateGraphics()
                Dim ClientArea As New RectangleF(0, 0, Width - 0.5!, Height - 0.5!)
                Dim IconArea As New RectangleF(ClientArea.Width - CalendarIconWidth, 0, CalendarIconWidth, ClientArea.Height)

                Using brush As New SolidBrush(BackColor)
                    g.FillRectangle(brush, ClientArea)
                End Using

                Using stringFormat As New StringFormat()
                    stringFormat.LineAlignment = StringAlignment.Center

                    Using brush As New SolidBrush(ForeColor)
                        g.DrawString("   {0}".FormatWith(Text), Font, brush, ClientArea, stringFormat)
                    End Using
                End Using

                If _IsDroppedDown Then
                    Using brush As New SolidBrush(Color.FromArgb(50, 64, 64, 64))
                        g.FillRectangle(brush, IconArea)
                    End Using
                End If

                If _BorderSize >= 1 Then
                    Using pen As New Pen(_BorderColor, _BorderSize)
                        pen.Alignment = PenAlignment.Inset

                        g.DrawRectangle(pen, ClientArea.X, ClientArea.Y, ClientArea.Width, ClientArea.Height)
                    End Using
                End If

                g.DrawImage(_CalendarIcon, Width - _CalendarIcon.Width - 9, CSng((Height - _CalendarIcon.Height) / 2))
            End Using
        End Sub

#End Region

    End Class

End Namespace