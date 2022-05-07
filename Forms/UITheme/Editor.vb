'Imports Microsoft.Win32

Namespace CommonRoutines.UITheme

    Public Class Editor

        Private _IsLoaded As Boolean = False

        Private ReadOnly _ThemeValueChanged As ThemeValueChangedEventHandler

        'Private ReadOnly _UserPreferenceChanged As UserPreferenceChangedEventHandler

        Public Sub New(ByRef themeValueChanged As ThemeValueChangedEventHandler)
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            _ThemeValueChanged = themeValueChanged

            '_UserPreferenceChanged = New UserPreferenceChangedEventHandler(AddressOf SystemEvents_UserPreferenceChanged)
            'AddHandler SystemEvents.UserPreferenceChanged, _UserPreferenceChanged
        End Sub

        Private Sub Me_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
            'RemoveHandler SystemEvents.UserPreferenceChanged, _UserPreferenceChanged
        End Sub

        Private Sub SetLabelBackColors()
            Try
                BackgroundPreviewLabel.BackColor = UITheme.BackColor
                BorderColorPreviewLabel.BackColor = UITheme.BorderColor
                ForegroundPreviewLabel.BackColor = UITheme.ForeColor
                FormBackgroundPreviewLabel.BackColor = UITheme.FormBackColor
                LineColorPreviewLabel.BackColor = UITheme.LineColor
                SelectionBackgroundPreviewLabel.BackColor = UITheme.SelectionBackColor
                SelectionForegroundPreviewLabel.BackColor = UITheme.SelectionForeColor

                GridFontSizeTextBox.Text = UITheme.GridFontSize.ToString()
            Catch ex As Exception
                ex.ToLog()
            End Try
        End Sub

        Private Sub SetThemeColors()
            Try
                UITheme.BackColor = BackgroundPreviewLabel.BackColor
                UITheme.BorderColor = BorderColorPreviewLabel.BackColor
                UITheme.ForeColor = ForegroundPreviewLabel.BackColor
                UITheme.FormBackColor = FormBackgroundPreviewLabel.BackColor
                UITheme.LineColor = LineColorPreviewLabel.BackColor
                UITheme.SelectionBackColor = SelectionBackgroundPreviewLabel.BackColor
                UITheme.SelectionForeColor = SelectionForegroundPreviewLabel.BackColor

                Dim GridFontSize As Single = Type.ToSingleDB(GridFontSizeTextBox.Text.Trim())
                If GridFontSize > 0 Then
                    UITheme.GridFontSize = GridFontSize
                End If
            Catch ex As Exception
                ex.ToLog()
            End Try
        End Sub

        'Private Sub SystemEvents_UserPreferenceChanged(sender As Object, e As UserPreferenceChangedEventArgs)
        '    If e.Category = UserPreferenceCategory.General OrElse e.Category = UserPreferenceCategory.VisualStyle Then
        '        LoadTheme()
        '    End If
        'End Sub

        Public Sub StartTimer()
            If Not _IsLoaded Then
                SetLabelBackColors()

                _IsLoaded = True

                CancelButton.Enabled = False
                SaveButton.Enabled = False
            End If
        End Sub

#Region " Controls "

        Private Sub CancelButton_Click(sender As Object, e As EventArgs) Handles CancelButton.Click
            LoadFromSettings()

            SetLabelBackColors()

            _ThemeValueChanged()

            CancelButton.Enabled = False
            SaveButton.Enabled = False
        End Sub

        Private Sub DefaultButton_Click(sender As Object, e As EventArgs) Handles DefaultButton.Click
            Try
                BackgroundPreviewLabel.BackColor = Color.FromArgb(42, 45, 86)
                BorderColorPreviewLabel.BackColor = Color.FromArgb(107, 83, 255)
                ForegroundPreviewLabel.BackColor = Color.FromArgb(124, 141, 181)
                FormBackgroundPreviewLabel.BackColor = Color.FromArgb(24, 28, 63)
                LineColorPreviewLabel.BackColor = Color.FromArgb(73, 75, 111)
                SelectionBackgroundPreviewLabel.BackColor = Color.FromArgb(241, 122, 133)
                SelectionForegroundPreviewLabel.BackColor = Color.White
            Catch ex As Exception
                ex.ToLog()
            Finally
                CancelButton.Enabled = True
                SaveButton.Enabled = True
            End Try
        End Sub

        Private Sub FromThemeButton_Click(sender As Object, e As EventArgs) Handles FromThemeButton.Click
            Try
                Dim UserColorSet As Integer = NativeRoutines.GetImmersiveUserColorSetPreference(False, False)

                'Dim ThemeColor As Color = GetImmersiveColor(Enums.ImmersiveColor.ImmersiveStartSelectionBackground, UserColorSet)
                'Dim DarkColor = ControlPaint.Dark(ThemeColor)
                'Dim LightColor = ControlPaint.Light(ThemeColor)
                'For Each Current As CommonRoutines.Controls.Button In Me.Controls.OfType(Of Button)()
                '    Current.BackColor = ThemeColor
                'Next

                BackgroundPreviewLabel.BackColor = GetImmersiveColor(Enums.ImmersiveColor.ImmersiveStartBackground, UserColorSet)
                BorderColorPreviewLabel.BackColor = GetImmersiveColor(Enums.ImmersiveColor.ImmersiveStartFocusRect, UserColorSet)
                ForegroundPreviewLabel.BackColor = GetImmersiveColor(Enums.ImmersiveColor.ImmersiveStartPrimaryText, UserColorSet)
                FormBackgroundPreviewLabel.BackColor = GetImmersiveColor(Enums.ImmersiveColor.ImmersiveStartSystemTilesBackground, UserColorSet)
                LineColorPreviewLabel.BackColor = ControlPaint.Light(ForegroundPreviewLabel.BackColor)
                SelectionBackgroundPreviewLabel.BackColor = GetImmersiveColor(Enums.ImmersiveColor.ImmersiveStartHoverBackground, UserColorSet)
                SelectionForegroundPreviewLabel.BackColor = GetImmersiveColor(Enums.ImmersiveColor.ImmersiveStartHoverPrimaryText, UserColorSet)
            Catch ex As Exception
                ex.ToLog()
            Finally
                CancelButton.Enabled = True
                SaveButton.Enabled = True
            End Try
        End Sub

        Private Sub GridFontSizeTextBox_TxtChanged(sender As Object, e As EventArgs) Handles GridFontSizeTextBox.TxtChanged
            CancelButton.Enabled = True
            SaveButton.Enabled = True
        End Sub

        Private Sub PreviewButton_Click(sender As Object, e As EventArgs) Handles PreviewButton.Click
            Try
                SetThemeColors()

                _ThemeValueChanged()

                SetLabelBackColors()
            Catch ex As Exception
                ex.ToLog()
            End Try
        End Sub

        Private Sub PreviewLabel_Click(sender As Object, e As EventArgs) Handles SelectionForegroundPreviewLabel.Click, SelectionBackgroundPreviewLabel.Click, BackgroundPreviewLabel.Click, LineColorPreviewLabel.Click, FormBackgroundPreviewLabel.Click, ForegroundPreviewLabel.Click, BorderColorPreviewLabel.Click
            Try
                Dim Label As Label = DirectCast(sender, Label)

                Using CD As New ColorDialog()
                    CD.AllowFullOpen = True
                    CD.AnyColor = True
                    CD.Color = Label.BackColor
                    CD.FullOpen = True

                    If CD.STAShowDialog() = DialogResult.OK Then
                        Label.BackColor = CD.Color
                    End If
                End Using
            Catch ex As Exception
                ex.ToLog()
            Finally
                CancelButton.Enabled = True
                SaveButton.Enabled = True
            End Try
        End Sub

        Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
            Try
                Settings.Set("Theme.GridFontSize", Type.ToSingleDB(GridFontSizeTextBox.Text))

                Settings.Set("Theme.BackColor", BackgroundPreviewLabel.BackColor.ToArgb())
                Settings.Set("Theme.BorderColor", BorderColorPreviewLabel.BackColor.ToArgb())
                Settings.Set("Theme.ForeColor", ForegroundPreviewLabel.BackColor.ToArgb())
                Settings.Set("Theme.FormBackColor", FormBackgroundPreviewLabel.BackColor.ToArgb())
                Settings.Set("Theme.LineColor", LineColorPreviewLabel.BackColor.ToArgb())
                Settings.Set("Theme.SelectionBackColor", SelectionBackgroundPreviewLabel.BackColor.ToArgb())
                Settings.Set("Theme.SelectionForeColor", SelectionForegroundPreviewLabel.BackColor.ToArgb())

                SetThemeColors()

                CancelButton.Enabled = False
                SaveButton.Enabled = False

                _ThemeValueChanged()

                SetLabelBackColors()
            Catch ex As Exception
                ex.ToLog()
            End Try
        End Sub

#End Region

    End Class

End Namespace