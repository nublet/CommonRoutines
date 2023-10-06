Namespace CommonRoutines.Forms

    Public Class MessageBox

        Private _PrimaryColor As Color = Color.CornflowerBlue

        Private ReadOnly _BorderSize As Integer = 2

        Public Property PrimaryColor As Color
            Get
                Return _PrimaryColor
            End Get
            Set(value As Color)
                _PrimaryColor = value
                BackColor = PrimaryColor
                TitlePanel.BackColor = PrimaryColor
            End Set
        End Property

#Region " Form Events "

        Public Sub New(text As String)
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            InitializeItems()

            PrimaryColor = _PrimaryColor
            MessageLabel.Text = text
            CaptionLabel.Text = ""

            SetFormSize()

            SetButtons(MessageBoxButtons.OK, MessageBoxDefaultButton.Button1)
        End Sub

        Public Sub New(text As String, caption As String)
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            InitializeItems()

            PrimaryColor = _PrimaryColor
            MessageLabel.Text = text
            CaptionLabel.Text = caption

            SetFormSize()

            SetButtons(MessageBoxButtons.OK, MessageBoxDefaultButton.Button1)
        End Sub

        Public Sub New(text As String, caption As String, buttons As MessageBoxButtons)
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            InitializeItems()

            PrimaryColor = _PrimaryColor
            MessageLabel.Text = text
            CaptionLabel.Text = caption

            SetFormSize()

            SetButtons(buttons, MessageBoxDefaultButton.Button1)
        End Sub

        Public Sub New(text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon)
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            InitializeItems()

            PrimaryColor = _PrimaryColor
            MessageLabel.Text = text
            CaptionLabel.Text = caption

            SetFormSize()

            SetButtons(buttons, MessageBoxDefaultButton.Button1)

            SetIcon(icon)
        End Sub

        Public Sub New(text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon, defaultButton As MessageBoxDefaultButton)
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            InitializeItems()

            PrimaryColor = _PrimaryColor
            MessageLabel.Text = text
            CaptionLabel.Text = caption

            SetFormSize()

            SetButtons(buttons, defaultButton)

            SetIcon(icon)
        End Sub

#End Region

        Private Sub InitializeItems()
            FormBorderStyle = FormBorderStyle.None
            Padding = New Padding(_BorderSize)
            MessageLabel.MaximumSize = New Size(550, 0)
            CloseButton.DialogResult = DialogResult.Cancel
            FirstButton.DialogResult = DialogResult.OK
            FirstButton.Visible = False
            SecondButton.Visible = False
            ThirdButton.Visible = False
        End Sub

        Private Sub SetButtons(buttons As MessageBoxButtons, defaultButton As MessageBoxDefaultButton)
            Dim xCenter As Integer = CInt((ButtonPanel.Width - FirstButton.Width) / 2)
            Dim yCenter As Integer = CInt((ButtonPanel.Height - FirstButton.Height) / 2)

            Select Case buttons
                Case MessageBoxButtons.OK
                    'OK Button
                    FirstButton.Visible = True
                    FirstButton.Location = New Point(xCenter, yCenter)
                    FirstButton.Text = "Ok"
                    FirstButton.DialogResult = DialogResult.OK 'Set DialogResult

                    'Set Default Button
                    SetDefaultButton(defaultButton)

                Case MessageBoxButtons.OKCancel
                    'OK Button
                    FirstButton.Visible = True
                    FirstButton.Location = New Point(CInt(xCenter - (FirstButton.Width / 2) - 5), yCenter)
                    FirstButton.Text = "Ok"
                    FirstButton.DialogResult = DialogResult.OK 'Set DialogResult

                    'Cancel Button
                    SecondButton.Visible = True
                    SecondButton.Location = New Point(CInt(xCenter + (SecondButton.Width / 2) + 5), yCenter)
                    SecondButton.Text = "Cancel"
                    SecondButton.DialogResult = DialogResult.Cancel 'Set DialogResult
                    SecondButton.BackColor = Color.DimGray

                    'Set Default Button
                    If defaultButton <> MessageBoxDefaultButton.Button3 Then
                        SetDefaultButton(defaultButton)
                    Else
                        SetDefaultButton(MessageBoxDefaultButton.Button1)
                    End If

                Case MessageBoxButtons.RetryCancel
                    'Retry Button
                    FirstButton.Visible = True
                    FirstButton.Location = New Point(CInt(xCenter - (FirstButton.Width / 2) - 5), yCenter)
                    FirstButton.Text = "Retry"
                    FirstButton.DialogResult = DialogResult.Retry 'Set DialogResult

                    'Cancel Button
                    SecondButton.Visible = True
                    SecondButton.Location = New Point(CInt(xCenter + (SecondButton.Width / 2) + 5), yCenter)
                    SecondButton.Text = "Cancel"
                    SecondButton.DialogResult = DialogResult.Cancel 'Set DialogResult
                    SecondButton.BackColor = Color.DimGray

                    'Set Default Button
                    If defaultButton <> MessageBoxDefaultButton.Button3 Then
                        SetDefaultButton(defaultButton)
                    Else
                        SetDefaultButton(MessageBoxDefaultButton.Button1)
                    End If

                Case MessageBoxButtons.YesNo
                    'Yes Button
                    FirstButton.Visible = True
                    FirstButton.Location = New Point(CInt(xCenter - (FirstButton.Width / 2) - 5), yCenter)
                    FirstButton.Text = "Yes"
                    FirstButton.DialogResult = DialogResult.Yes 'Set DialogResult

                    'No Button
                    SecondButton.Visible = True
                    SecondButton.Location = New Point(CInt(xCenter + (SecondButton.Width / 2) + 5), yCenter)
                    SecondButton.Text = "No"
                    SecondButton.DialogResult = DialogResult.No 'Set DialogResult
                    SecondButton.BackColor = Color.IndianRed

                    'Set Default Button
                    If defaultButton <> MessageBoxDefaultButton.Button3 Then
                        SetDefaultButton(defaultButton)
                    Else
                        SetDefaultButton(MessageBoxDefaultButton.Button1)
                    End If

                Case MessageBoxButtons.YesNoCancel
                    'Yes Button
                    FirstButton.Visible = True
                    FirstButton.Location = New Point(xCenter - FirstButton.Width - 5, yCenter)
                    FirstButton.Text = "Yes"
                    FirstButton.DialogResult = DialogResult.Yes 'Set DialogResult

                    'No Button
                    SecondButton.Visible = True
                    SecondButton.Location = New Point(xCenter, yCenter)
                    SecondButton.Text = "No"
                    SecondButton.DialogResult = DialogResult.No 'Set DialogResult
                    SecondButton.BackColor = Color.IndianRed

                    'Cancel Button
                    ThirdButton.Visible = True
                    ThirdButton.Location = New Point(xCenter + SecondButton.Width + 5, yCenter)
                    ThirdButton.Text = "Cancel"
                    ThirdButton.DialogResult = DialogResult.Cancel 'Set DialogResult
                    ThirdButton.BackColor = Color.DimGray

                    'Set Default Button
                    SetDefaultButton(defaultButton)

                Case MessageBoxButtons.AbortRetryIgnore
                    'Abort Button
                    FirstButton.Visible = True
                    FirstButton.Location = New Point(xCenter - FirstButton.Width - 5, yCenter)
                    FirstButton.Text = "Abort"
                    FirstButton.DialogResult = DialogResult.Abort 'Set DialogResult
                    FirstButton.BackColor = Color.Goldenrod

                    'Retry Button
                    SecondButton.Visible = True
                    SecondButton.Location = New Point(xCenter, yCenter)
                    SecondButton.Text = "Retry"
                    SecondButton.DialogResult = DialogResult.Retry 'Set DialogResult

                    'Ignore Button
                    ThirdButton.Visible = True
                    ThirdButton.Location = New Point(xCenter + SecondButton.Width + 5, yCenter)
                    ThirdButton.Text = "Ignore"
                    ThirdButton.DialogResult = DialogResult.Ignore 'Set DialogResult
                    ThirdButton.BackColor = Color.IndianRed

                    'Set Default Button
                    SetDefaultButton(defaultButton)
            End Select
        End Sub

        Private Sub SetDefaultButton(defaultButton As MessageBoxDefaultButton)
            Select Case defaultButton
                Case MessageBoxDefaultButton.Button1
                    FirstButton.Select()
                    FirstButton.ForeColor = Color.White
                    FirstButton.Font = New Font(FirstButton.Font, FontStyle.Underline)

                Case MessageBoxDefaultButton.Button2
                    SecondButton.Select()
                    SecondButton.ForeColor = Color.White
                    SecondButton.Font = New Font(SecondButton.Font, FontStyle.Underline)

                Case MessageBoxDefaultButton.Button3
                    ThirdButton.Select()
                    ThirdButton.ForeColor = Color.White
                    ThirdButton.Font = New Font(ThirdButton.Font, FontStyle.Underline)
            End Select
        End Sub

        Private Sub SetFormSize()
            Dim Widht As Integer = MessageLabel.Width + IconPictureBox.Width + BodyPanel.Padding.Left
            Dim Height As Integer = TitlePanel.Height + MessageLabel.Height + ButtonPanel.Height + BodyPanel.Padding.Top

            Size = New Size(Widht, Height)
        End Sub

        Private Sub SetIcon(icon As MessageBoxIcon)
            Select Case icon
                Case MessageBoxIcon.Error
                    IconPictureBox.Image = My.Resources._error
                    PrimaryColor = Color.FromArgb(224, 79, 95)
                    CloseButton.FlatAppearance.MouseOverBackColor = Color.Crimson
                    BugPictureBox.Visible = True

                Case MessageBoxIcon.Information
                    IconPictureBox.Image = My.Resources.information
                    PrimaryColor = Color.FromArgb(38, 191, 166)

                Case MessageBoxIcon.Question
                    IconPictureBox.Image = My.Resources.question
                    PrimaryColor = Color.FromArgb(10, 119, 232)

                Case MessageBoxIcon.Exclamation
                    IconPictureBox.Image = My.Resources.exclamation
                    PrimaryColor = Color.FromArgb(255, 140, 0)

                Case MessageBoxIcon.None
                    IconPictureBox.Image = My.Resources.chat
                    PrimaryColor = Color.CornflowerBlue
            End Select
        End Sub

#Region " Control Events "

        Private Sub BugPictureBox_Click(sender As Object, e As EventArgs) Handles BugPictureBox.Click
            Try
                Dim PSI As New ProcessStartInfo With {
                    .Arguments = "",
                    .CreateNoWindow = False,
                    .FileName = ErrorLogLocation,
                    .UseShellExecute = True,
                    .WindowStyle = ProcessWindowStyle.Maximized,
                    .WorkingDirectory = IO.Path.GetDirectoryName(ErrorLogLocation)
                }

                Process.Start(PSI)
            Catch ex As Exception
            End Try
        End Sub

        Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
            Close()
        End Sub

        Private Sub TitlePanel_MouseDown(sender As Object, e As MouseEventArgs) Handles TitlePanel.MouseDown
            NativeRoutines.ReleaseCapture()
            NativeRoutines.SendMessage(Handle, NativeRoutines.WM_SYSCOMMAND, &HF012, IntPtr.Zero)
        End Sub

#End Region

    End Class

End Namespace