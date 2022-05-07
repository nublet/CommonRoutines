Namespace CommonRoutines

    Public Module _Shared

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

        Public Function GetCurrentDate() As DateTime
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

    End Module

End Namespace