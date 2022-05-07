Namespace CommonRoutines

    Public Module _Shared

        Public Function GetScreenWorkingArea() As Rectangle
            Dim ScreenName As String = Settings.DefaultScreenName

            If ScreenName.IsNotSet() Then
                Return Screen.PrimaryScreen.WorkingArea
            Else
                Dim Screen As Screen = Screen.AllScreens.Where(Function(o) o.DeviceName.IsEqualTo(ScreenName)).FirstOrDefault()

                If Screen Is Nothing Then
                    Return Screen.PrimaryScreen.WorkingArea
                Else
                    Return Screen.WorkingArea
                End If
            End If
        End Function

        Public Sub CloseAndSave(ByRef mainForm As Form)
            Try
                If mainForm IsNot Nothing Then
                    Dim DeviceName As String = Screen.FromControl(mainForm).DeviceName
                    ' Windows XP appends random contents from memory after the null terminator.
                    If DeviceName.Contains(Chr(0)) Then
                        DeviceName = DeviceName.Substring(0, DeviceName.IndexOf(Chr(0)))
                    End If

                    Settings.DefaultScreenName = DeviceName
                End If

                Settings.SaveCache()
            Catch ex As Exception
                ex.ToLog()
            End Try
        End Sub

        Public Sub Initialise(forceFormBackColor As Boolean)
            Initialise("{0}Errors.log".FormatWith(GetBaseDirectory(Enums.BaseDirectory.AppDataCompanyProductVersion)), forceFormBackColor, "{0}Performance.log".FormatWith(GetBaseDirectory(Enums.BaseDirectory.AppDataCompanyProductVersion)))
        End Sub

        Public Sub Initialise(errorLogLocation As String, forceFormBackColor As Boolean, performanceCounterLocation As String)
            Try
                Settings.Initialise(errorLogLocation, performanceCounterLocation)

                Errors.Handlers.Add(New Errors.LogErrorDetailHandler(AddressOf LogErrorDetail))

                UITheme.LoadFromSettings()
            Catch ex As Exception
                ex.ToLog()
            End Try
        End Sub

#Region " Error Handler "

        Private Sub LogErrorDetail(silent As Boolean, ByRef errorDetail As Models.ErrorDetail)
            If silent Then
                Return
            End If

            MessageBox.Show(errorDetail.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Sub

#End Region

    End Module

End Namespace