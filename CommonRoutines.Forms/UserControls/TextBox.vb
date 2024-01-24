Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D

Namespace UserControls

    <DefaultEvent("TxtChanged")> Public Class TextBox
        Inherits UserControl

        Public Event TxtChanged As EventHandler

        Private _BorderColor As Color = Color.MediumSlateBlue
        Private _BorderFocusColor As Color = Color.HotPink
        Private _BorderRadius As Integer = 0
        Private _BorderSize As Integer = 2
        Private _IsFocused As Boolean = False
        Private _IsPasswordChar As Boolean = False
        Private _IsPlaceHolder As Boolean = False
        Private _PlaceHolderColor As Color = Color.DarkGray
        Private _PlaceHolderText As String = ""
        Private _UnderlinedStyle As Boolean = False

        Private ReadOnly _TextBox As System.Windows.Forms.TextBox = Nothing

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

        <Category("Common Routines")> Public Property Multiline As Boolean
            Get
                Return _TextBox.Multiline
            End Get
            Set(value As Boolean)
                _TextBox.Multiline = value
            End Set
        End Property

        <Category("Common Routines")> Public Property PasswordChar As Boolean
            Get
                Return _IsPasswordChar
            End Get
            Set(value As Boolean)
                _IsPasswordChar = value

                If Not _IsPlaceHolder Then
                    _TextBox.UseSystemPasswordChar = value
                End If
            End Set
        End Property

        <Category("Common Routines")> Public Property PlaceHolderColor As Color
            Get
                Return _PlaceHolderColor
            End Get
            Set(value As Color)
                _PlaceHolderColor = value

                If _IsPlaceHolder Then
                    _TextBox.ForeColor = value
                End If
            End Set
        End Property

        <Category("Common Routines")> Public Property PlaceHolderText As String
            Get
                Return _PlaceHolderText
            End Get
            Set(value As String)
                _PlaceHolderText = value
                _TextBox.Text = ""

                SetPlaceHolder()
            End Set
        End Property

        <Category("Common Routines")> <DefaultValue(False)> <RefreshProperties(RefreshProperties.Repaint)> Public Property [ReadOnly] As Boolean
            Get
                Return _TextBox.ReadOnly
            End Get
            Set(value As Boolean)
                _TextBox.ReadOnly = value
            End Set
        End Property

        <Category("Common Routines")> <DefaultValue(ScrollBars.None)> <Localizable(True)> Public Property ScrollBars As ScrollBars
            Get
                Return _TextBox.ScrollBars
            End Get
            Set(value As ScrollBars)
                _TextBox.ScrollBars = value
            End Set
        End Property

        <Browsable(True)> <Category("Common Routines")> <EditorBrowsable(EditorBrowsableState.Always)> Public Shadows Property Text As String
            Get
                If _IsPlaceHolder Then
                    Return ""
                End If

                Return _TextBox.Text
            End Get
            Set(value As String)
                RemovePlaceHolder()

                _TextBox.Text = value

                SetPlaceHolder()
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

        <Category("Common Routines")> Public Property WordWrap As Boolean
            Get
                Return _TextBox.WordWrap
            End Get
            Set(value As Boolean)
                _TextBox.WordWrap = value
            End Set
        End Property

        Public Sub New()
            _TextBox = New System.Windows.Forms.TextBox()

            SuspendLayout()

            _TextBox.BackColor = BackColor
            _TextBox.BorderStyle = BorderStyle.None
            _TextBox.Dock = DockStyle.Fill

            AddHandler _TextBox.Click, AddressOf TextBox_Click
            AddHandler _TextBox.Enter, AddressOf TextBox_Enter
            AddHandler _TextBox.KeyPress, AddressOf TextBox_KeyPress
            AddHandler _TextBox.Leave, AddressOf TextBox_Leave
            AddHandler _TextBox.MouseEnter, AddressOf TextBox_MouseEnter
            AddHandler _TextBox.MouseLeave, AddressOf TextBox_MouseLeave
            AddHandler _TextBox.TextChanged, AddressOf TextBox_TextChanged

            AutoScaleMode = AutoScaleMode.None
            BackColor = SystemColors.Window
            Controls.Add(Me._TextBox)
            Font = New Font("Microsoft Sans Serif", 9.5!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
            ForeColor = Color.DimGray
            Margin = New Padding(5)
            Name = "TextBox"
            Padding = New Padding(10, 7, 10, 7)
            Size = New Size(250, 30)

            ResumeLayout(False)

            PerformLayout()
        End Sub

        Private Sub RemovePlaceHolder()
            If _IsPlaceHolder AndAlso _PlaceHolderText.IsSet() Then
                _IsPlaceHolder = False

                _TextBox.Text = ""
                _TextBox.ForeColor = ForeColor
                If _IsPasswordChar Then
                    _TextBox.UseSystemPasswordChar = True
                End If
            End If
        End Sub

        Private Sub SetPlaceHolder()
            If _TextBox.Text.IsNotSet() AndAlso _PlaceHolderText.IsSet() Then
                _IsPlaceHolder = True

                _TextBox.Text = _PlaceHolderText
                _TextBox.ForeColor = _PlaceHolderColor
                If _IsPasswordChar Then
                    _TextBox.UseSystemPasswordChar = False
                End If
            End If
        End Sub

        Private Sub SetTextBoxRoundedRegion()
            Dim Path As GraphicsPath

            If Multiline Then
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

        Private Sub TextBox_Click(sender As Object, e As EventArgs)
            MyBase.OnClick(e)
        End Sub

        Private Sub TextBox_Enter(sender As Object, e As EventArgs)
            _IsFocused = True

            Invalidate()

            RemovePlaceHolder()
        End Sub

        Private Sub TextBox_KeyPress(sender As Object, e As KeyPressEventArgs)
            MyBase.OnKeyPress(e)
        End Sub

        Private Sub TextBox_Leave(sender As Object, e As EventArgs)
            _IsFocused = False

            Invalidate()

            SetPlaceHolder()
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

        Public Shadows Sub [Select](start As Integer, length As Integer)
            _TextBox.Select(start, length)
        End Sub

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