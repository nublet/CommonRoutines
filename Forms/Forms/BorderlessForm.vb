Imports System.ComponentModel

Namespace CommonRoutines.Forms

    Public Class BorderlessForm

        Private Const _ResizeAreaSize As Integer = 10

        Private _BorderSize As Integer = 2
        Private _FormSize As Size

        <Category("Common Routines")> Public Property BorderColor As Color
            Get
                Return BackColor
            End Get
            Set(value As Color)
                BackColor = value
            End Set
        End Property

        <Browsable(False)> <Category("Common Routines")> Public Property BorderSize As Integer
            Get
                Return _BorderSize
            End Get
            Set(value As Integer)
                If value < 1 Then
                    Return
                End If

                _BorderSize = value

                Padding = New Padding(value)

                Invalidate()
            End Set
        End Property

#Region " Form Events "

        Public Sub New()
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Padding = New Padding(_BorderSize)

            'Dim UserColorSet As Integer = NativeRoutines.GetImmersiveUserColorSetPreference(False, False)
            'TitlePanel.BackColor = UITheme.GetImmersiveColor(Enums.ImmersiveColor.ImmersiveStartSelectionBackground, UserColorSet)

            TitlePanel.BackColor = UITheme.FormBackColor

            CheckIcons()
        End Sub

        Private Sub Me_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
            Select Case WindowState
                Case FormWindowState.Maximized
                    Padding = New Padding(8, 8, 8, 0)
                Case FormWindowState.Normal
                    If Padding.Top.IsNotEqualTo(_BorderSize) Then
                        Padding = New Padding(_BorderSize)
                    End If
            End Select
        End Sub

#End Region

        Private Sub CheckIcons()
            If TitlePanel.BackColor.GetBrightness() >= 0.6! Then
                ClosePictureBox.Image = My.Resources.closeDark
                If WindowState = FormWindowState.Normal Then
                    MaximizePictureBox.Image = My.Resources.maximizeDark
                Else
                    MaximizePictureBox.Image = My.Resources.minimizeDark
                End If
                MinimizePictureBox.Image = My.Resources.minusDark
            Else
                ClosePictureBox.Image = My.Resources.closeLight
                If WindowState = FormWindowState.Normal Then
                    MaximizePictureBox.Image = My.Resources.maximizeLight
                Else
                    MaximizePictureBox.Image = My.Resources.minimizeLight
                End If
                MinimizePictureBox.Image = My.Resources.minusLight
            End If
        End Sub

#Region " Control Events "

        Private Sub ClosePictureBox_Click(sender As Object, e As EventArgs) Handles ClosePictureBox.Click
            Close()
        End Sub

        Private Sub MaximizePictureBox_Click(sender As Object, e As EventArgs) Handles MaximizePictureBox.Click
            If WindowState = FormWindowState.Normal Then
                _FormSize = ClientSize
                WindowState = FormWindowState.Maximized
            Else
                WindowState = FormWindowState.Normal
                Size = _FormSize
            End If

            CheckIcons()
        End Sub

        Private Sub MinimizePictureBox_Click(sender As Object, e As EventArgs) Handles MinimizePictureBox.Click
            _FormSize = ClientSize

            WindowState = FormWindowState.Minimized
        End Sub

        Private Sub TitlePanel_MouseDown(sender As Object, e As MouseEventArgs) Handles TitlePanel.MouseDown
            NativeRoutines.ReleaseCapture()
            NativeRoutines.SendMessage(Handle, NativeRoutines.WM_SYSCOMMAND, &HF012, IntPtr.Zero)
        End Sub

#End Region

#Region " Overrides "

        Protected Overrides Sub WndProc(ByRef m As Message)
            If m.Msg = NativeRoutines.WM_NCCALCSIZE AndAlso m.WParam.ToInt32() = 1 Then
                Return
            End If

            If m.Msg = NativeRoutines.WM_NCHITTEST Then
                MyBase.WndProc(m)

                If Me.WindowState = FormWindowState.Normal Then

                    If CInt(m.Result) = NativeRoutines.HTCLIENT Then
                        Dim screenPoint As New Point(m.LParam.ToInt32())
                        Dim clientPoint As Point = PointToClient(screenPoint)

                        If clientPoint.Y <= _ResizeAreaSize Then

                            If clientPoint.X <= _ResizeAreaSize Then
                                m.Result = CType(NativeRoutines.HTTOPLEFT, IntPtr)
                            ElseIf clientPoint.X < (Me.Size.Width - _ResizeAreaSize) Then
                                m.Result = CType(NativeRoutines.HTTOP, IntPtr)
                            Else
                                m.Result = CType(NativeRoutines.HTTOPRIGHT, IntPtr)
                            End If
                        ElseIf clientPoint.Y <= (Me.Size.Height - _ResizeAreaSize) Then

                            If clientPoint.X <= _ResizeAreaSize Then
                                m.Result = CType(NativeRoutines.HTLEFT, IntPtr)
                            ElseIf clientPoint.X > (Me.Width - _ResizeAreaSize) Then
                                m.Result = CType(NativeRoutines.HTRIGHT, IntPtr)
                            End If
                        Else

                            If clientPoint.X <= _ResizeAreaSize Then
                                m.Result = CType(NativeRoutines.HTBOTTOMLEFT, IntPtr)
                            ElseIf clientPoint.X < (Me.Size.Width - _ResizeAreaSize) Then
                                m.Result = CType(NativeRoutines.HTBOTTOM, IntPtr)
                            Else
                                m.Result = CType(NativeRoutines.HTBOTTOMRIGHT, IntPtr)
                            End If
                        End If
                    End If
                End If

                Return
            End If

            If m.Msg = NativeRoutines.WM_SYSCOMMAND Then
                Dim wParam As Integer = (m.WParam.ToInt32() And &HFFF0)
                If wParam = NativeRoutines.SC_MINIMIZE Then
                    _FormSize = ClientSize
                End If
                If wParam = NativeRoutines.SC_RESTORE Then
                    Size = _FormSize
                End If
            End If

            MyBase.WndProc(m)
        End Sub



#End Region

    End Class

End Namespace