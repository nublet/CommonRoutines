Namespace CommonRoutines

    Public Module _Shared

        Public Const ParameterSplit As String = "<<::>>"

        Friend Const _TB As Long = 1099511627776 ' 1024 * 1024 * 1024 * 1024
        Friend Const _GB As Long = 1073741824    ' 1024 * 1024 * 1024
        Friend Const _MB As Long = 1048576       ' 1024 * 1024
        Friend Const _KB As Long = 1024          ' 1024

        Public Function GetComputerName() As String
            Dim Result As String = ""

            Try
                Result = My.Computer.Name

                If Result.IsNotSet() Then
                    Result = Environment.MachineName
                End If

                If Result.IsNotSet() Then
                    Result = Net.Dns.GetHostName()
                End If
            Catch ex As Exception
            End Try

            Return Result
        End Function

        Public Function GetCurrentCulture() As Globalization.CultureInfo
            If Settings.UseInvariantCulture Then
                Return Globalization.CultureInfo.InvariantCulture
            Else
                Return Globalization.CultureInfo.CurrentCulture
            End If
        End Function

        Public Function GetCurrentDate() As Date
            If Settings.UseUTCDate Then
                Return Date.UtcNow
            Else
                Return Date.Now
            End If
        End Function

        Public Function GetCurrentDateOffset() As DateTimeOffset
            If Settings.UseUTCDate Then
                Return DateTimeOffset.UtcNow
            Else
                Return DateTimeOffset.Now
            End If
        End Function

        Public Function GetCurrentDateUTC() As Date
            Return Date.UtcNow
        End Function

        Public Function GetListSortDescriptionCollection(Of T)(columnName As String, listSortDirection As ComponentModel.ListSortDirection) As ComponentModel.ListSortDescriptionCollection
            Return GetListSortDescriptionCollection(Of T)({New KeyValuePair(Of String, ComponentModel.ListSortDirection)(columnName, listSortDirection)})
        End Function

        Public Function GetListSortDescriptionCollection(Of T)(sortColumns As IDictionary(Of String, ComponentModel.ListSortDirection)) As ComponentModel.ListSortDescriptionCollection
            Return GetListSortDescriptionCollection(Of T)(sortColumns.ToArray())
        End Function

        Public Function GetListSortDescriptionCollection(Of T)(sortColumns As KeyValuePair(Of String, ComponentModel.ListSortDirection)()) As ComponentModel.ListSortDescriptionCollection
            Dim PDC As ComponentModel.PropertyDescriptorCollection = ComponentModel.TypeDescriptor.GetProperties(GetType(T))

            Dim Sorts As New List(Of ComponentModel.ListSortDescription)

            For Each kvp As KeyValuePair(Of String, ComponentModel.ListSortDirection) In sortColumns
                Dim PD As ComponentModel.PropertyDescriptor = PDC.Find(kvp.Key, True)
                If PD Is Nothing Then
                    Continue For
                End If

                Sorts.Add(New ComponentModel.ListSortDescription(PD, kvp.Value))
            Next

            Return New ComponentModel.ListSortDescriptionCollection(Sorts.ToArray())
        End Function

        Public Function GetUsername() As String
            Dim Result As String = ""

            Try
                Result = My.User.Name
            Catch ex As Exception
            End Try

            If Result.IsNotSet() Then
                Try
                    Result = Environment.UserName

                    If Result.Contains("\") Then
                        Result = Result.Substring(Result.IndexOf("\") + 1)
                    End If
                Catch ex As Exception
                End Try
            End If

            Return Result
        End Function

#Region " Directories "

        Private ReadOnly _BaseDirectories As New Dictionary(Of Enums.BaseDirectory, String)

        Private Function GetValidBaseDirectory(directoryPath As String, fallbackDirectory As String, shouldExist As Boolean) As String
            Dim result As String = directoryPath
            Try
                Dim ApplicationData As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)

                If result.IsNotSet() Then
                    If fallbackDirectory.IsSet() Then
                        result = fallbackDirectory
                    Else
                        result = ApplicationData
                    End If
                End If

                result = result.Replace("\\", "\")
                result = result.Replace("\\", "\")
                If result.StartsWith("\") Then
                    result = "\{0}".FormatWith(result)
                End If

                If shouldExist AndAlso Not IO.Directory.Exists(result) Then
                    If fallbackDirectory.IsSet() Then
                        result = fallbackDirectory
                    Else
                        result = ApplicationData
                    End If
                End If

                result = result.Replace("\\", "\")
                result = result.Replace("\\", "\")
                If result.StartsWith("\") Then
                    result = "\{0}".FormatWith(result)
                End If

                If Not IO.Directory.Exists(result) Then
                    IO.Directory.CreateDirectory(result)
                End If

                If Not result.EndsWith("\") Then
                    result = "{0}\".FormatWith(result)
                End If
            Catch ex As Exception
                ex.ToLog()
            End Try

            Return result
        End Function

        Public Function GetBaseDirectory(baseDirectory As Enums.BaseDirectory) As String
            Dim ApplicationData As String = ""

            Try
                ApplicationData = GetValidBaseDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "", True)

                If _BaseDirectories.Count <= 0 Then
                    SyncLock _BaseDirectories
                        Dim ExecutingAssembly As String = IO.Path.GetDirectoryName(New Uri(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath)

                        Dim DesktopDirectory As String = GetValidBaseDirectory(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), ApplicationData, True)
                        Dim Favorites As String = GetValidBaseDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Favorites), ApplicationData, True)
                        Dim UserProfile As String = GetValidBaseDirectory(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ApplicationData, True)

                        _BaseDirectories.Add(Enums.BaseDirectory.AppData, ApplicationData)
                        _BaseDirectories.Add(Enums.BaseDirectory.AppDataCompany, GetValidBaseDirectory("{0}{1}\".FormatWith(_BaseDirectories(Enums.BaseDirectory.AppData), My.Application.Info.CompanyName), ApplicationData, False))
                        _BaseDirectories.Add(Enums.BaseDirectory.AppDataCompanyProduct, GetValidBaseDirectory("{0}{1}\".FormatWith(_BaseDirectories(Enums.BaseDirectory.AppDataCompany), My.Application.Info.ProductName), _BaseDirectories(Enums.BaseDirectory.AppDataCompany), False))
                        _BaseDirectories.Add(Enums.BaseDirectory.AppDataCompanyProductVersion, GetValidBaseDirectory("{0}{1}\".FormatWith(_BaseDirectories(Enums.BaseDirectory.AppDataCompanyProduct), My.Application.Info.Version.ToString()), _BaseDirectories(Enums.BaseDirectory.AppDataCompanyProduct), False))
                        _BaseDirectories.Add(Enums.BaseDirectory.Desktop, DesktopDirectory)
                        _BaseDirectories.Add(Enums.BaseDirectory.ExecutingAssembly, GetValidBaseDirectory("{0}\".FormatWith(ExecutingAssembly), ApplicationData, True))
                        _BaseDirectories.Add(Enums.BaseDirectory.Favorites, Favorites)
                        _BaseDirectories.Add(Enums.BaseDirectory.UserProfile, UserProfile)
                    End SyncLock
                End If

                If Not _BaseDirectories.ContainsKey(baseDirectory) Then
                    SyncLock _BaseDirectories
                        _BaseDirectories.Add(baseDirectory, ApplicationData)
                    End SyncLock
                End If

                Return _BaseDirectories(baseDirectory)
            Catch ex As Exception
                ex.ToLog(True)
            End Try

            Return ApplicationData
        End Function

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

                StringBuilder.ToString().ToLog(True)
            End If
        End Sub

#End Region

    End Module

End Namespace