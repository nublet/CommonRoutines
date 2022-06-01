Namespace CommonRoutines.Extensions

    Public Module System_Windows_Forms_Form

        <Runtime.CompilerServices.Extension()> Public Function IsUsingDefaultIcon(ByRef f As Form) As Boolean
            If Not f.ShowIcon Then
                Return False
            End If

            If f.Icon Is Nothing Then
                Return True
            End If

            Return False
        End Function

        <Runtime.CompilerServices.Extension()> Public Function GetCenteredFormSizeLocation(ByRef f As Form) As Models.FormSizeLocation
            Dim WorkingArea As Rectangle

            If f.StartPosition = FormStartPosition.CenterParent Then
                If f.ParentForm IsNot Nothing Then
                    WorkingArea = Screen.FromHandle(f.ParentForm.Handle).WorkingArea
                Else
                    WorkingArea = GetScreenWorkingArea()
                End If
            Else
                WorkingArea = GetScreenWorkingArea()
            End If

            Return New Models.FormSizeLocation With {
                .Height = f.Height,
                .Left = CInt(WorkingArea.X + ((WorkingArea.Width / 2) - (f.Width / 2))),
                .Top = CInt(WorkingArea.Y + ((WorkingArea.Height / 2) - (f.Height / 2))),
                .Width = f.Width
            }
        End Function

        <Runtime.CompilerServices.Extension()> Public Sub EnsureVisible(ByRef f As Form)
            f.WindowState = FormWindowState.Minimized
            f.WindowState = FormWindowState.Normal

            f.Opacity = 1
            f.Show()

            f.TopMost = True
            f.Focus()
            f.BringToFront()
            f.TopMost = False
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub CenterOnDefault(ByRef f As Form)
            f.StartPosition = FormStartPosition.CenterScreen

            f.StartUp()
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub SetTheme(f As Form)
            UITheme.NewControls.Clear()

            f.BackColor = UITheme.FormBackColor

            If Not f.HasChildren Then
                Return
            End If

            If f.Controls Is Nothing OrElse f.Controls.Count <= 0 Then
                Return
            End If

            For Each Current As Control In f.Controls
                Current.SetTheme()
            Next

            If UITheme.NewControls.Count > 0 Then
                Dim NewControls As String = String.Join(Environment.NewLine, UITheme.NewControls.Distinct().OrderBy(Function(o) o))
                NewControls.ToLog(True)
            End If
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub ShutDown(ByRef f As Form)
            f.ShutDown("")
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub ShutDown(ByRef f As Form, prefix As String)
            Dim Key As String = f.Name
            Key.Append(".", prefix, "FormSizeLocation")

            Dim FormSizeLocation As Models.FormSizeLocation = Settings.Get(Of Models.FormSizeLocation)(Key)

            If FormSizeLocation Is Nothing Then
                FormSizeLocation = New Models.FormSizeLocation
            End If

            If f.WindowState = FormWindowState.Maximized Then
                FormSizeLocation.IsMaximised = True
                FormSizeLocation.IsMinimized = False
            ElseIf f.WindowState = FormWindowState.Minimized Then
                FormSizeLocation.IsMaximised = False
                FormSizeLocation.IsMinimized = True
            Else
                FormSizeLocation.IsMaximised = False
                FormSizeLocation.IsMinimized = False
            End If

            FormSizeLocation.Height = f.Height
            FormSizeLocation.Left = f.Left
            FormSizeLocation.Top = f.Top
            FormSizeLocation.Width = f.Width

            Settings.Set(Key, FormSizeLocation)
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub StartUp(ByRef f As Form)
            f.StartUp("")
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub StartUp(ByRef f As Form, prefix As String)
            Dim FormSizeLocation As Models.FormSizeLocation = Nothing

            Select Case f.StartPosition
                Case FormStartPosition.CenterParent, FormStartPosition.CenterScreen
                    FormSizeLocation = f.GetCenteredFormSizeLocation()
                Case Else
                    Dim Key As String = f.Name
                    Key.Append(".", prefix, "FormSizeLocation")

                    FormSizeLocation = Settings.Get(Of Models.FormSizeLocation)(Key)

                    If FormSizeLocation Is Nothing Then
                        FormSizeLocation = New Models.FormSizeLocation With {
                            .Height = f.Height,
                            .Left = f.Left,
                            .Top = f.Top,
                            .Width = f.Width
                        }
                    End If
            End Select

            f.StartUp(FormSizeLocation)
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub StartUp(ByRef f As Form, formSizeLocation As Models.FormSizeLocation)
            If formSizeLocation.Height < 50 OrElse formSizeLocation.Width < 50 Then
                formSizeLocation.Height = f.Height
                formSizeLocation.Width = f.Width
            End If
            If formSizeLocation.Height < 50 OrElse formSizeLocation.Width < 50 Then
                formSizeLocation.Height = 300
                formSizeLocation.Width = 300
            End If

            Dim FormRectangle As New Rectangle(formSizeLocation.Left, formSizeLocation.Top, formSizeLocation.Width, formSizeLocation.Height)

            If formSizeLocation.Left < 0 OrElse formSizeLocation.Top < 0 OrElse Not FormRectangle.IsOnScreen() Then
                Dim WorkingArea As Rectangle = GetScreenWorkingArea()
                formSizeLocation.Left = CInt(WorkingArea.X + ((WorkingArea.Width / 2) - (formSizeLocation.Width / 2)))
                formSizeLocation.Top = CInt(WorkingArea.Y + ((WorkingArea.Height / 2) - (formSizeLocation.Height / 2)))
            End If

            f.Height = formSizeLocation.Height
            f.Left = formSizeLocation.Left
            f.Top = formSizeLocation.Top
            f.Width = formSizeLocation.Width

            If Settings.ForceIcon OrElse f.IsUsingDefaultIcon() Then
                f.ShowIcon = False
                f.Icon = DirectCast(Settings.Icon.Clone(), Icon)
                f.ShowIcon = True
            End If

            If Settings.ForceFormBackColor Then
                f.BackColor = UITheme.FormBackColor
            End If

            If formSizeLocation.IsMaximised Then
                f.WindowState = FormWindowState.Maximized
            ElseIf formSizeLocation.IsMinimized Then
                f.WindowState = FormWindowState.Minimized
            Else
                f.WindowState = FormWindowState.Normal
            End If

            f.BringToFront()
        End Sub

    End Module

End Namespace