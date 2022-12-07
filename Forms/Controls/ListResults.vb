Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Namespace CommonRoutines.Controls

    <DefaultEvent("TxtChanged")> Public Class ListResults
        Inherits UserControl

        Public Event TxtChanged As EventHandler

        Private Const _Capacity As Long = CLng(0.5 * 1024 * 1024)

        Private _ChecksSinceLastUpdate As Integer = 0
        Private _IsRunning As Boolean = False

        Private _BorderColor As Color = Color.MediumSlateBlue
        Private _BorderFocusColor As Color = Color.HotPink
        Private _BorderRadius As Integer = 0
        Private _BorderSize As Integer = 2
        Private _IsFocused As Boolean = False
        Private _UnderlinedStyle As Boolean = False

        Private ReadOnly _MethodInvoker As MethodInvoker = Nothing
        Private ReadOnly _StringBuilder As System.Text.StringBuilder = Nothing
        Private ReadOnly _TextBox As Windows.Forms.TextBox = Nothing
        Private ReadOnly _UpdateTimer As Timers.Timer = Nothing

        Public Property Indent As Integer = 0

        <Category("Common Routines")> Public Property BorderColor As Color
            Get
                Return _BorderColor
            End Get
            Set(value As Color)
                _BorderColor = value
                Invalidate()
            End Set
        End Property

        <Category("Common Routines")> Public Property BorderFocusColor As Color
            Get
                Return _BorderFocusColor
            End Get
            Set(value As Color)
                _BorderFocusColor = value
            End Set
        End Property

        <Category("Common Routines")> Public Property BorderRadius As Integer
            Get
                Return _BorderRadius
            End Get
            Set(value As Integer)
                If value < 0 Then
                    Return
                End If

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

        <Category("Common Routines")> Public Property UnderlinedStyle As Boolean
            Get
                Return _UnderlinedStyle
            End Get
            Set(value As Boolean)
                _UnderlinedStyle = value
                Invalidate()
            End Set
        End Property

        Public ReadOnly Property ResultText As String
            Get
                If Disposing OrElse IsDisposed Then
                    Return ""
                End If

                If _TextBox Is Nothing OrElse _TextBox.IsDisposed Then
                    Return ""
                End If

                If _StringBuilder.Length > 0 Then
                    UpdateMessages()
                End If

                Return _TextBox.Text
            End Get
        End Property

        Public Sub New()
            InitializeComponent()

            _TextBox = New Windows.Forms.TextBox()

            SuspendLayout()

            _TextBox.BackColor = BackColor
            _TextBox.BorderStyle = BorderStyle.None
            _TextBox.Dock = DockStyle.Fill
            _TextBox.MaxLength = (_Capacity * 20) + 1
            _TextBox.Multiline = True
            _TextBox.ScrollBars = ScrollBars.Both
            _TextBox.WordWrap = False

            AddHandler _TextBox.Click, AddressOf TextBox_Click
            AddHandler _TextBox.Enter, AddressOf TextBox_Enter
            AddHandler _TextBox.KeyPress, AddressOf TextBox_KeyPress
            AddHandler _TextBox.Leave, AddressOf TextBox_Leave
            AddHandler _TextBox.MouseEnter, AddressOf TextBox_MouseEnter
            AddHandler _TextBox.MouseLeave, AddressOf TextBox_MouseLeave
            AddHandler _TextBox.TextChanged, AddressOf TextBox_TextChanged

            _UpdateTimer = New Timers.Timer With {
                .Interval = 1000
            }
            _UpdateTimer.Stop()
            AddHandler _UpdateTimer.Elapsed, AddressOf UpdateTimer_Elapsed

            _MethodInvoker = New MethodInvoker(AddressOf UpdateMessages)

            _StringBuilder = New Text.StringBuilder()
            _StringBuilder.EnsureCapacity(_Capacity)

            AutoScaleMode = AutoScaleMode.None
            BackColor = SystemColors.Window
            DoubleBuffered = True
            Controls.Add(Me._TextBox)
            Font = New Font("Microsoft Sans Serif", 9.5!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
            ForeColor = Color.DimGray
            Margin = New Padding(5)
            Name = "TextBox"
            Padding = New Padding(10, 7, 10, 7)
            Size = New Size(250, 30)

            ResumeLayout(False)

            PerformLayout()

            ClearResults()
        End Sub

        Private Function GetFigurePath(radius As Integer, rect As Rectangle) As GraphicsPath
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

        Private Sub AddMessageToBuilder(message As String)
            If (_StringBuilder.Length + message.Length) >= _StringBuilder.Capacity Then
                UpdateMessages()
            End If

            _StringBuilder.AppendLine(message)
            _ChecksSinceLastUpdate = 0

            StartTimer()
        End Sub

        Private Sub SetTextBoxRoundedRegion()
            Dim Path As GraphicsPath

            If _TextBox.Multiline Then
                Path = GetFigurePath(_BorderRadius - _BorderSize, _TextBox.ClientRectangle)
            Else
                Path = GetFigurePath(_BorderSize * 2, _TextBox.ClientRectangle)
            End If

            _TextBox.Region = New Region(Path)

            Path.Dispose()
        End Sub

        Private Sub UpdateControlHeight()
            If Not _TextBox.Multiline Then
                Dim TextHeight As Integer = TextRenderer.MeasureText("Text", Font).Height + 1
                _TextBox.Multiline = True
                _TextBox.MinimumSize = New Size(0, TextHeight)
                _TextBox.Multiline = False

                Height = _TextBox.Height + Padding.Top + Padding.Bottom
            End If
        End Sub

        Private Sub UpdateMessages()
            _UpdateTimer.Stop()

            _IsRunning = False

            If Disposing OrElse IsDisposed Then
                Return
            End If

            _ChecksSinceLastUpdate += 1

            If _StringBuilder.Length <= 0 Then
                Return
            End If

            If (_TextBox.TextLength + _StringBuilder.Length) >= _TextBox.MaxLength Then
                ClearResults()
            End If

            _TextBox.AppendText(_StringBuilder.ToString())
            _StringBuilder.Clear()

            _TextBox.ScrollToCaret()
        End Sub

        Public Sub AddMessage(message As String)
            Dim Line As String = GetCurrentDate().GetSQLString("[HH:mm:ss] ")

            For I As Integer = 0 To Indent - 1
                Line = "{0}{1}".FormatWith(Line, Settings.Indent)
            Next

            Line = "{0}{1}".FormatWith(Line, message)

            AddMessageToBuilder(Line)
        End Sub

        Public Sub ClearResults()
            If InvokeRequired Then
                Invoke(New MethodInvoker(AddressOf ClearResults))
                Return
            End If

            _TextBox.Clear()
            _TextBox.ClearUndo()
        End Sub

        Public Sub CopyToClipboard()
            My.Computer.Clipboard.Clear()
            My.Computer.Clipboard.SetText(_TextBox.Text)
        End Sub

        Public Sub StartTimer()
            If _IsRunning Then
                Return
            End If

            If Disposing OrElse IsDisposed Then
                Return
            End If

            _IsRunning = True

            _UpdateTimer.Stop()
            _UpdateTimer.Start()
        End Sub

        Private Sub UpdateTimer_Elapsed(sender As Object, e As Timers.ElapsedEventArgs)
            Invoke(_MethodInvoker)

            If _ChecksSinceLastUpdate < 20 Then
                StartTimer()
            End If
        End Sub

#Region " TextBox Events "

        Private Sub TextBox_Click(sender As Object, e As EventArgs)
            MyBase.OnClick(e)
        End Sub

        Private Sub TextBox_Enter(sender As Object, e As EventArgs)
            _IsFocused = True

            Invalidate()
        End Sub

        Private Sub TextBox_KeyPress(sender As Object, e As KeyPressEventArgs)
            MyBase.OnKeyPress(e)
        End Sub

        Private Sub TextBox_Leave(sender As Object, e As EventArgs)
            _IsFocused = False

            Invalidate()
        End Sub

        Private Sub TextBox_MouseEnter(sender As Object, e As EventArgs)
            MyBase.OnMouseEnter(e)
        End Sub

        Private Sub TextBox_MouseLeave(sender As Object, e As EventArgs)
            MyBase.OnMouseLeave(e)
        End Sub

        Private Sub TextBox_TextChanged(sender As Object, e As EventArgs)
            RaiseEvent TxtChanged(sender, e)
        End Sub

#End Region

#Region " Overrides "

        <Browsable(True)> <Category("Common Routines")> <EditorBrowsable(EditorBrowsableState.Always)> Public Overrides Property BackColor As Color
            Get
                Return MyBase.BackColor
            End Get
            Set(value As Color)
                _TextBox.BackColor = value
                MyBase.BackColor = value
            End Set
        End Property

        <Browsable(True)> <Category("Common Routines")> <EditorBrowsable(EditorBrowsableState.Always)> Public Overrides Property Font As Font
            Get
                Return MyBase.Font
            End Get
            Set(value As Font)
                _TextBox.Font = value
                MyBase.Font = value

                If DesignMode Then
                    UpdateControlHeight()
                End If
            End Set
        End Property

        <Browsable(True)> <Category("Common Routines")> <EditorBrowsable(EditorBrowsableState.Always)> Public Overrides Property ForeColor As Color
            Get
                Return MyBase.ForeColor
            End Get
            Set(value As Color)
                _TextBox.ForeColor = value
                MyBase.ForeColor = value
            End Set
        End Property

        Protected Overrides Sub OnLoad(e As EventArgs)
            MyBase.OnLoad(e)

            UpdateControlHeight()
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)

            If _BorderRadius > 1 Then
                Dim RectSmooth As Rectangle = ClientRectangle
                Dim RectBorder As Rectangle = Rectangle.Inflate(RectSmooth, -_BorderSize, -_BorderSize)

                Dim SmoothSize As Integer = 1
                If _BorderSize > 0 Then
                    SmoothSize = _BorderSize
                End If

                If _BorderRadius > 15 Then
                    SetTextBoxRoundedRegion()
                End If

                Using pathSmooth As GraphicsPath = GetFigurePath(_BorderRadius, RectSmooth)
                    Region = New Region(pathSmooth)

                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias

                    Using pen As New Pen(_BorderColor, BorderSize)
                        pen.Alignment = PenAlignment.Center

                        If _IsFocused Then
                            pen.Color = _BorderFocusColor
                        End If

                        Using penSmooth As New Pen(Parent.BackColor, SmoothSize)
                            e.Graphics.DrawPath(penSmooth, pathSmooth)
                        End Using

                        If _UnderlinedStyle Then
                            e.Graphics.SmoothingMode = SmoothingMode.None
                            e.Graphics.DrawLine(pen, 0, Height - 1, Width, Height - 1)
                        Else
                            Using path As GraphicsPath = GetFigurePath(_BorderRadius - _BorderSize, RectBorder)
                                e.Graphics.DrawPath(pen, path)
                            End Using
                        End If
                    End Using
                End Using
            Else
                Region = New Region(ClientRectangle)

                Using pen As New Pen(_BorderColor, BorderSize)
                    pen.Alignment = PenAlignment.Inset

                    If _IsFocused Then
                        pen.Color = _BorderFocusColor
                    End If

                    If _UnderlinedStyle Then
                        e.Graphics.DrawLine(pen, 0, Height - 1, Width, Height - 1)
                    Else
                        e.Graphics.DrawRectangle(pen, 0, 0, Width - 0.5!, Height - 0.5!)
                    End If
                End Using
            End If
        End Sub

        Protected Overrides Sub OnResize(e As EventArgs)
            MyBase.OnResize(e)

            If DesignMode Then
                UpdateControlHeight()
            End If
        End Sub

#End Region

    End Class

End Namespace