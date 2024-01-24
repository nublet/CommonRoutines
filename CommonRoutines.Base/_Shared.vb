Public Module _Shared

    Public Const ParameterSplit As String = "<<::>>"

    Friend Const _TB As Long = 1099511627776 ' 1024 * 1024 * 1024 * 1024
    Friend Const _GB As Long = 1073741824    ' 1024 * 1024 * 1024
    Friend Const _MB As Long = 1048576       ' 1024 * 1024
    Friend Const _KB As Long = 1024          ' 1024

    Public Function GetComputerName() As String
        Dim Result As String = ""

        Try
            Result = Environment.MachineName

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
            Result = Environment.UserName

            If Result.Contains("\"c) Then
                Result = Result.Substring(Result.IndexOf("\"c) + 1)
            End If
        Catch ex As Exception
        End Try

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

            result = result.Replace("\\", "\"c)
            result = result.Replace("\\", "\"c)
            If result.StartsWith("\"c) Then
                result = "\{0}".FormatWith(result)
            End If

            If shouldExist AndAlso Not IO.Directory.Exists(result) Then
                If fallbackDirectory.IsSet() Then
                    result = fallbackDirectory
                Else
                    result = ApplicationData
                End If
            End If

            result = result.Replace("\\", "\"c)
            result = result.Replace("\\", "\"c)
            If result.StartsWith("\"c) Then
                result = "\{0}".FormatWith(result)
            End If

            If Not IO.Directory.Exists(result) Then
                IO.Directory.CreateDirectory(result)
            End If

            If Not result.EndsWith("\"c) Then
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
                    Dim ExecutingAssembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
                    Dim ExecutingAssemblyPath As String = IO.Path.GetDirectoryName(ExecutingAssembly.Location)
                    Dim ExecutingAssemblyVersionInfo As FileVersionInfo = FileVersionInfo.GetVersionInfo(ExecutingAssembly.Location)

                    Dim DesktopDirectory As String = GetValidBaseDirectory(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), ApplicationData, True)
                    Dim Favorites As String = GetValidBaseDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Favorites), ApplicationData, True)
                    Dim UserProfile As String = GetValidBaseDirectory(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ApplicationData, True)

                    _BaseDirectories.Add(Enums.BaseDirectory.AppData, ApplicationData)
                    _BaseDirectories.Add(Enums.BaseDirectory.AppDataCompany, GetValidBaseDirectory("{0}{1}\".FormatWith(_BaseDirectories(Enums.BaseDirectory.AppData), ExecutingAssemblyVersionInfo.CompanyName), ApplicationData, False))
                    _BaseDirectories.Add(Enums.BaseDirectory.AppDataCompanyProduct, GetValidBaseDirectory("{0}{1}\".FormatWith(_BaseDirectories(Enums.BaseDirectory.AppDataCompany), ExecutingAssemblyVersionInfo.ProductName), _BaseDirectories(Enums.BaseDirectory.AppDataCompany), False))
                    _BaseDirectories.Add(Enums.BaseDirectory.AppDataCompanyProductVersion, GetValidBaseDirectory("{0}{1}\".FormatWith(_BaseDirectories(Enums.BaseDirectory.AppDataCompanyProduct), ExecutingAssemblyVersionInfo.FileVersion), _BaseDirectories(Enums.BaseDirectory.AppDataCompanyProduct), False))
                    _BaseDirectories.Add(Enums.BaseDirectory.Desktop, DesktopDirectory)
                    _BaseDirectories.Add(Enums.BaseDirectory.ExecutingAssembly, GetValidBaseDirectory("{0}\".FormatWith(ExecutingAssembly), ApplicationData, True))
                    _BaseDirectories.Add(Enums.BaseDirectory.Favorites, Favorites)
                    _BaseDirectories.Add(Enums.BaseDirectory.UserProfile, UserProfile)
                End SyncLock
            End If

            _BaseDirectories.TryAdd(baseDirectory, ApplicationData)

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
        Dim Level1Value As Dictionary(Of String, List(Of String)) = Nothing
        Dim Level2Value As List(Of String) = Nothing
        Dim Message As String = e.ErrorContext.Error.Message

        If Message.StartsWith("Could not find member ") Then
            Dim JSONPath As String = Message.Substring(Message.IndexOf(". Path ") + 2).Trim()

            Dim MemberName As String = Message.Substring(Message.IndexOf("member '") + 8)
            MemberName = MemberName.Substring(0, MemberName.IndexOf("'"c)).Trim()

            Dim ObjectName As String = Message.Substring(Message.IndexOf("type '") + 6)
            ObjectName = ObjectName.Substring(0, ObjectName.IndexOf("'"c)).Trim()

            If Not _JSONErrors.TryGetValue(ObjectName, Level1Value) Then
                Level1Value = New Dictionary(Of String, List(Of String))
                _JSONErrors.Add(ObjectName, Level1Value)
            End If

            If Not Level1Value.TryGetValue(MemberName, Level2Value) Then
                Level2Value = New List(Of String)
                Level1Value.Add(MemberName, Level2Value)
            End If

            If JSONPath.IsSet() Then
                Level2Value.Add("{0} {1}".FormatWith(JSONPath, LastJSONURL))
            Else
                Level2Value.Add(LastJSONURL)
            End If
        Else
            If Not _JSONErrors.TryGetValue("Misc", Level1Value) Then
                Level1Value = New Dictionary(Of String, List(Of String))
                _JSONErrors.Add("Misc", Level1Value)
            End If

            If Not Level1Value.TryGetValue(Message, Level2Value) Then
                Level2Value = New List(Of String)
                Level1Value.Add(Message, Level2Value)
            End If

            Level2Value.Add(LastJSONURL)
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