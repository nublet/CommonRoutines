Imports System.ComponentModel
Imports System.Drawing.Design
Imports System.Drawing.Drawing2D

Namespace CommonRoutines.Controls

    <DefaultEvent("OnSelectedIndexChanged")> Public Class ComboBox
        Inherits UserControl

        Public Event OnSelectedIndexChanged As EventHandler

        Private Const IconHeight As Integer = 6
        Private Const IconWidth As Integer = 14

        Private _BackColor As Color = Color.WhiteSmoke
        Private _IconColor As Color = Color.MediumSlateBlue
        Private _ListBackColor As Color = Color.FromArgb(230, 228, 245)
        Private _ListForeColor As Color = Color.DimGray
        Private _BorderColor As Color = Color.MediumSlateBlue
        Private _BorderSize As Integer = 1

        Private ReadOnly _Button As Windows.Forms.Button = Nothing
        Private ReadOnly _ComboBox As Windows.Forms.ComboBox = Nothing
        Private ReadOnly _Label As Windows.Forms.Label = Nothing

        <Category("Common Routines")> Public Shadows Property BackColor As Color
            Get
                Return _BackColor
            End Get
            Set(value As Color)
                _BackColor = value
                _Button.BackColor = value
                _Label.BackColor = value
            End Set
        End Property

        <Category("Common Routines")> Public Property BorderColor As Color
            Get
                Return _BorderColor
            End Get
            Set(value As Color)
                MyBase.BackColor = value
                _BorderColor = value
            End Set
        End Property

        <Category("Common Routines")> Public Property BorderSize As Integer
            Get
                Return _BorderSize
            End Get
            Set(value As Integer)
                Padding = New Padding(value)
                _BorderSize = value

                AdjustComboBoxDimensions()
            End Set
        End Property

        <Category("Common Routines")> Public Property DropDownStyle As ComboBoxStyle
            Get
                Return _ComboBox.DropDownStyle
            End Get
            Set(value As ComboBoxStyle)
                If value = ComboBoxStyle.Simple Then
                    Return
                End If

                _ComboBox.DropDownStyle = value
            End Set
        End Property

        <Category("Common Routines")> Public Property IconColor As Color
            Get
                Return _IconColor
            End Get
            Set(value As Color)
                _IconColor = value
                _Button.Invalidate()
            End Set
        End Property

        <Category("Common Routines")> Public Property ListBackColor As Color
            Get
                Return _ListBackColor
            End Get
            Set(value As Color)
                _ComboBox.BackColor = value
                _ListBackColor = value
            End Set
        End Property

        <Category("Common Routines")> Public Property ListForeColor As Color
            Get
                Return _ListForeColor
            End Get
            Set(value As Color)
                _ComboBox.ForeColor = value
                _ListForeColor = value
            End Set
        End Property

        Public Sub New()
            _Button = New Windows.Forms.Button()
            _ComboBox = New Windows.Forms.ComboBox()
            _Label = New Windows.Forms.Label()

            SuspendLayout()

            _Button.BackColor = BackColor
            _Button.Cursor = Cursors.Hand
            _Button.Dock = DockStyle.Right
            _Button.FlatAppearance.BorderSize = 0
            _Button.FlatStyle = FlatStyle.Flat
            _Button.Size = New Size(30, 30)

            AddHandler _Button.Click, AddressOf Button_Click
            AddHandler _Button.Paint, AddressOf Button_Paint

            _ComboBox.BackColor = _ListBackColor
            _ComboBox.Font = New Font(Font.Name, 10.0!)
            _ComboBox.ForeColor = _ListForeColor

            AddHandler _ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
            AddHandler _ComboBox.TextChanged, AddressOf ComboBox_TextChanged

            _Label.AutoSize = False
            _Label.BackColor = BackColor
            _Label.Dock = DockStyle.Fill
            _Label.Font = New Font(Font.Name, 10.0!)
            _Label.ForeColor = ForeColor
            _Label.TextAlign = ContentAlignment.MiddleLeft
            _Label.Padding = New Padding(8, 0, 0, 0)

            AddHandler _Label.Click, AddressOf Label_Click
            AddHandler _Label.MouseEnter, AddressOf Label_MouseEnter
            AddHandler _Label.MouseLeave, AddressOf Label_MouseLeave

            Controls.Add(_Label)
            Controls.Add(_Button)
            Controls.Add(_ComboBox)

            BackColor = _BorderColor
            Font = New Font(Font.Name, 10.0!)
            Padding = New Padding(_BorderSize)
            Size = New Size(200, 30)

            ResumeLayout()

            AdjustComboBoxDimensions()
        End Sub

        Private Sub Me_Load(sender As Object, e As EventArgs) Handles Me.Load
            AdjustComboBoxDimensions()
        End Sub

        Private Sub AdjustComboBoxDimensions()
            _ComboBox.Width = _Label.Width
            _ComboBox.Location = New Point(Width - Padding.Right - _ComboBox.Width, _Label.Bottom - _ComboBox.Height)

            If _ComboBox.Height >= Height Then
                Height = _ComboBox.Height + (_BorderSize * 2)
            End If
        End Sub

        Public Sub BeginUpdate()
            _ComboBox.BeginUpdate()
        End Sub

        Public Sub EndUpdate()
            _ComboBox.EndUpdate()
        End Sub

        Private Sub Button_Click(sender As Object, e As EventArgs)
            _ComboBox.Select()
            _ComboBox.DroppedDown = True
        End Sub

        Private Sub Button_Paint(sender As Object, e As PaintEventArgs)
            Dim RectIcon As New Rectangle(CInt((_Button.Width - IconWidth) / 2), CInt((_Button.Height - IconHeight) / 2), IconWidth, IconHeight)

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias

            Using path As New GraphicsPath
                path.AddLine(RectIcon.X, RectIcon.Y, CSng(RectIcon.X + (IconWidth / 2)), RectIcon.Bottom)
                path.AddLine(CSng(RectIcon.X + (IconWidth / 2)), RectIcon.Bottom, RectIcon.Right, RectIcon.Y)

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias

                Using pen As New Pen(_IconColor, 2)
                    e.Graphics.DrawPath(pen, path)
                End Using
            End Using
        End Sub

        Private Sub Label_Click(sender As Object, e As EventArgs)
            MyBase.OnClick(e)

            _ComboBox.Select()

            If _ComboBox.DropDownStyle = ComboBoxStyle.DropDownList Then
                _ComboBox.DroppedDown = True
            End If
        End Sub

        Private Sub Label_MouseEnter(sender As Object, e As EventArgs)
            MyBase.OnMouseEnter(e)
        End Sub

        Private Sub Label_MouseLeave(sender As Object, e As EventArgs)
            MyBase.OnMouseLeave(e)
        End Sub

        Private Sub ComboBox_SelectedIndexChanged(sender As Object, e As EventArgs)
            RaiseEvent OnSelectedIndexChanged(sender, e)

            _Label.Text = _ComboBox.Text
        End Sub

        Private Sub ComboBox_TextChanged(sender As Object, e As EventArgs)
            _Label.Text = _ComboBox.Text
        End Sub

#Region " ComboBox Properties "

        <Browsable(True)> <Category("Common Routines")> <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> <Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(UITypeEditor))> <EditorBrowsable(EditorBrowsableState.Always)> <Localizable(True)> Public Property AutoCompleteCustomSource As AutoCompleteStringCollection
            Get
                Return _ComboBox.AutoCompleteCustomSource
            End Get
            Set(value As AutoCompleteStringCollection)
                _ComboBox.AutoCompleteCustomSource = value
            End Set
        End Property

        <Browsable(True)> <Category("Common Routines")> <DefaultValue(AutoCompleteMode.None)> <EditorBrowsable(EditorBrowsableState.Always)> Public Property AutoCompleteMode As AutoCompleteMode
            Get
                Return _ComboBox.AutoCompleteMode
            End Get
            Set(value As AutoCompleteMode)
                _ComboBox.AutoCompleteMode = value
            End Set
        End Property

        <Browsable(True)> <Category("Common Routines")> <DefaultValue(AutoCompleteSource.None)> <EditorBrowsable(EditorBrowsableState.Always)> Public Property AutoCompleteSource As AutoCompleteSource
            Get
                Return _ComboBox.AutoCompleteSource
            End Get
            Set(value As AutoCompleteSource)
                _ComboBox.AutoCompleteSource = value
            End Set
        End Property

        <AttributeProvider(GetType(IListSource))> <Category("Common Routines")> <RefreshProperties(RefreshProperties.Repaint)> Public Property DataSource As Object
            Get
                Return _ComboBox.DataSource
            End Get
            Set(value As Object)
                _ComboBox.DataSource = value
            End Set
        End Property

        <Browsable(True)> <Category("Common Routines")> <DefaultValue(False)> Public Property FormattingEnabled As Boolean
            Get
                Return _ComboBox.FormattingEnabled
            End Get
            Set(value As Boolean)
                _ComboBox.FormattingEnabled = value
            End Set
        End Property

        <Browsable(False)> <Category("Common Routines")> <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> Public Property SelectedIndex As Integer
            Get
                Return _ComboBox.SelectedIndex
            End Get
            Set(value As Integer)
                _ComboBox.SelectedIndex = value
            End Set
        End Property

        <Bindable(True)> <Browsable(False)> <Category("Common Routines")> <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> Public Property SelectedItem As Object
            Get
                Return _ComboBox.SelectedItem
            End Get
            Set(value As Object)
                _ComboBox.SelectedItem = value
            End Set
        End Property

        <Category("Common Routines")> <DefaultValue(False)> Public Property Sorted As Boolean
            Get
                Return _ComboBox.Sorted
            End Get
            Set(value As Boolean)
                _ComboBox.Sorted = value
            End Set
        End Property

        <Category("Common Routines")> <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> <Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(UITypeEditor))> <Localizable(True)> <MergableProperty(False)> Public ReadOnly Property Items As Windows.Forms.ComboBox.ObjectCollection
            Get
                Return _ComboBox.Items
            End Get
        End Property

#End Region

#Region " ListControl Properties "

        <Category("Common Routines")> <DefaultValue("")> <Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(UITypeEditor))> <TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")> Public Property DisplayMember As String
            Get
                Return _ComboBox.DisplayMember
            End Get
            Set(value As String)
                _ComboBox.DisplayMember = value
            End Set
        End Property

        <Category("Common Routines")> <DefaultValue("")> <Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(UITypeEditor))> Public Property ValueMember As String
            Get
                Return _ComboBox.ValueMember
            End Get
            Set(value As String)
                _ComboBox.ValueMember = value
            End Set
        End Property

#End Region

#Region " Overrides "

        <Browsable(True)> <Category("Common Routines")> <EditorBrowsable(EditorBrowsableState.Always)> Public Overrides Property ForeColor As Color
            Get
                Return MyBase.ForeColor
            End Get
            Set(value As Color)
                _Label.ForeColor = value
                MyBase.ForeColor = value
            End Set
        End Property

        <Browsable(True)> <Category("Common Routines")> <EditorBrowsable(EditorBrowsableState.Always)> Public Overrides Property Font As Font
            Get
                Return MyBase.Font
            End Get
            Set(value As Font)
                MyBase.Font = value

                _ComboBox.Font = value
                _Label.Font = value

                AdjustComboBoxDimensions()
            End Set
        End Property

        <Browsable(True)> <Category("Common Routines")> <EditorBrowsable(EditorBrowsableState.Always)> Public Overrides Property Text As String
            Get
                Return _Label.Text
            End Get
            Set(value As String)
                _Label.Text = value
            End Set
        End Property

        Protected Overrides Sub OnResize(e As EventArgs)
            MyBase.OnResize(e)

            If DesignMode Then
                AdjustComboBoxDimensions()
            End If
        End Sub

        Private Sub InitializeComponent()
            Me.SuspendLayout()
            '
            'ComboBox
            '
            Me.Name = "ComboBox"
            Me.ResumeLayout(False)

        End Sub

#End Region

    End Class

End Namespace