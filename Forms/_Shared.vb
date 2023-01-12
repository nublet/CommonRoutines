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

        Public Sub Initialise(forceFormBackColor As Boolean, useInvariantCulture As Boolean)
            Initialise("{0}Errors.log".FormatWith(GetBaseDirectory(Enums.BaseDirectory.AppDataCompanyProductVersion)), forceFormBackColor, "{0}Performance.log".FormatWith(GetBaseDirectory(Enums.BaseDirectory.AppDataCompanyProductVersion)), useInvariantCulture)
        End Sub

        Public Sub Initialise(errorLogLocation As String, forceFormBackColor As Boolean, performanceCounterLocation As String, useInvariantCulture As Boolean)
            Try
                Settings.Initialise(errorLogLocation, performanceCounterLocation, useInvariantCulture)

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

#Region " JSON "

        Private ReadOnly _JSONErrors As New Dictionary(Of String, Dictionary(Of String, List(Of String)))

        Public Property LastJSONURL As String = ""

        Public Sub ClearJSONErrors()
            _JSONErrors.Clear()
        End Sub

        Public Sub JSONError(sender As Object, e As Newtonsoft.Json.Serialization.ErrorEventArgs)
            Dim Message As String = e.ErrorContext.Error.Message

            If Message.StartsWith("Could not find member ") Then
                Dim JSONPath As String = Message.Substring(Message.IndexOf(". Path ") + 2).Trim()

                Dim MemberName As String = Message.Substring(Message.IndexOf("member '") + 8)
                MemberName = MemberName.Substring(0, MemberName.IndexOf("'"c)).Trim()

                Dim ObjectName As String = Message.Substring(Message.IndexOf("type '") + 6)
                ObjectName = ObjectName.Substring(0, ObjectName.IndexOf("'"c)).Trim()

                If Not _JSONErrors.ContainsKey(ObjectName) Then
                    _JSONErrors.Add(ObjectName, New Dictionary(Of String, List(Of String)))
                End If

                If Not _JSONErrors(ObjectName).ContainsKey(MemberName) Then
                    _JSONErrors(ObjectName).Add(MemberName, New List(Of String))
                End If

                If JSONPath.IsSet() Then
                    _JSONErrors(ObjectName)(MemberName).Add("{0} {1}".FormatWith(JSONPath, LastJSONURL))
                Else
                    _JSONErrors(ObjectName)(MemberName).Add(LastJSONURL)
                End If
            Else
                If Not _JSONErrors.ContainsKey("Misc") Then
                    _JSONErrors.Add("Misc", New Dictionary(Of String, List(Of String)))
                End If

                If Not _JSONErrors("Misc").ContainsKey(Message) Then
                    _JSONErrors("Misc").Add(Message, New List(Of String))
                End If

                _JSONErrors("Misc")(Message).Add(LastJSONURL)
            End If

            e.ErrorContext.Handled = True
        End Sub

        Public Sub LogJSONErrors()
            If _JSONErrors.Count > 0 Then
                Dim StringBuilder As New Text.StringBuilder()

                StringBuilder.AppendLine("JSON Errors: ")
                For Each ObjectName As String In _JSONErrors.Keys.OrderBy(Function(o) o)
                    StringBuilder.Append("   ")
                    StringBuilder.AppendLine(ObjectName)
                    For Each MemberName As String In _JSONErrors(ObjectName).Keys.OrderBy(Function(o) o)
                        StringBuilder.Append("      ")
                        StringBuilder.AppendLine(MemberName)

                        StringBuilder.Append("         ")
                        StringBuilder.AppendLine(_JSONErrors(ObjectName)(MemberName)(0))
                        'For Each Current As String In kvp.Value.Distinct().OrderBy(Function(o) o)
                        'StringBuilder.Append("      ")
                        'StringBuilder.AppendLine(Current)
                        'Next
                    Next
                Next

                Using writer As New IO.StreamWriter(Settings.ErrorDetailLocation, True)
                    writer.WriteLine(StringBuilder.ToString())
                    writer.WriteLine("")
                End Using
            End If
        End Sub

#End Region

    End Module

End Namespace