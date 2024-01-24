Namespace Registry

    Public Module _Shared

        Private Function [Get](keyName As String, valueName As String, ByRef value As Object) As Boolean
            value = Nothing

            Try
                Dim SubKey As String = keyName.Substring(keyName.IndexOf("\"c, Settings.StringComparison) + 1).Replace("\"c, "\\")
                Dim TopKey As String = keyName.Substring(0, keyName.IndexOf("\"c))

                Dim RegistryHive As Microsoft.Win32.RegistryHive = GetRegistryHive(TopKey)

                Try
                    Using BaseKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.RegistryKey.OpenBaseKey(RegistryHive, Microsoft.Win32.RegistryView.Registry64)
                        Using Key As Microsoft.Win32.RegistryKey = BaseKey.OpenSubKey(SubKey, False)
                            If Key IsNot Nothing Then
                                value = Key.GetValue(valueName, Nothing)
                            End If
                        End Using
                    End Using

                    If value IsNot Nothing Then
                        Return True
                    End If
                Catch exInner As Exception
                    exInner.ToLog(True)
                    value = Nothing
                End Try

                Try
                    Using BaseKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.RegistryKey.OpenBaseKey(RegistryHive, Microsoft.Win32.RegistryView.Registry32)
                        Using Key As Microsoft.Win32.RegistryKey = BaseKey.OpenSubKey(SubKey, False)
                            If Key IsNot Nothing Then
                                value = Key.GetValue(valueName, Nothing)
                            End If
                        End Using
                    End Using

                    If value IsNot Nothing Then
                        Return True
                    End If
                Catch exInner As Exception
                    exInner.ToLog(True)
                    value = Nothing
                End Try
            Catch ex As Exception
                ex.ToLog(True)
                value = Nothing
            End Try

            If value Is Nothing Then
                Return False
            Else
                Return True
            End If
        End Function

        Private Function GetRegistryHive(hiveName As String) As Microsoft.Win32.RegistryHive
            Select Case hiveName.ToUpper()
                Case "HKEY_CLASSES_ROOT"
                    Return Microsoft.Win32.RegistryHive.ClassesRoot
                Case "HKEY_CURRENT_USER"
                    Return Microsoft.Win32.RegistryHive.CurrentUser
                Case "HKEY_LOCAL_MACHINE"
                    Return Microsoft.Win32.RegistryHive.LocalMachine
                Case "HKEY_USERS"
                    Return Microsoft.Win32.RegistryHive.Users
                Case "HKEY_CURRENT_CONFIG"
                    Return Microsoft.Win32.RegistryHive.CurrentConfig
            End Select

            Return Microsoft.Win32.RegistryHive.LocalMachine
        End Function

        Private Function [Set](keyName As String, registryValueKind As Microsoft.Win32.RegistryValueKind, registryView As Microsoft.Win32.RegistryView, valueName As String, value As Object) As Boolean
            Try
                Dim SubKey As String = keyName.Substring(keyName.IndexOf("\"c) + 1).Replace("\"c, "\\")
                Dim TopKey As String = keyName.Substring(0, keyName.IndexOf("\"c))

                Dim RegistryHive As Microsoft.Win32.RegistryHive = GetRegistryHive(TopKey)

                Using BaseKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.RegistryKey.OpenBaseKey(RegistryHive, registryView)
                    Using Key As Microsoft.Win32.RegistryKey = BaseKey.OpenSubKey(SubKey, True)
                        If Key Is Nothing Then
                            Using NewKey As Microsoft.Win32.RegistryKey = BaseKey.CreateSubKey(SubKey)
                                NewKey.SetValue(valueName, value, registryValueKind)
                            End Using
                        Else
                            Key.SetValue(valueName, value, registryValueKind)
                        End If
                    End Using
                End Using

                Return True
            Catch ex As Exception
                ex.ToLog(True)
            End Try

            Return False
        End Function

        Public Function DeleteSubKey(keyName As String, registryView As Microsoft.Win32.RegistryView) As Boolean
            Try
                Dim SubKey As String = keyName.Substring(keyName.IndexOf("\"c) + 1).Replace("\"c, "\\")
                Dim TopKey As String = keyName.Substring(0, keyName.IndexOf("\"c))

                Dim RegistryHive As Microsoft.Win32.RegistryHive = GetRegistryHive(TopKey)

                Using BaseKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.RegistryKey.OpenBaseKey(RegistryHive, registryView)
                    BaseKey.DeleteSubKey(SubKey, False)
                End Using

                Return True
            Catch ex As Exception
                ex.ToLog(True)
            End Try

            Return False
        End Function

        Public Function DeleteSubKeyTree(keyName As String, registryView As Microsoft.Win32.RegistryView) As Boolean
            Try
                Dim SubKey As String = keyName.Substring(keyName.IndexOf("\"c) + 1).Replace("\"c, "\\")
                Dim TopKey As String = keyName.Substring(0, keyName.IndexOf("\"c))

                Dim RegistryHive As Microsoft.Win32.RegistryHive = GetRegistryHive(TopKey)

                Using BaseKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.RegistryKey.OpenBaseKey(RegistryHive, registryView)
                    BaseKey.DeleteSubKeyTree(keyName, False)
                End Using

                Return True
            Catch ex As Exception
                ex.ToLog(True)
            End Try

            Return False
        End Function

        Public Function DeleteValue(keyName As String, registryView As Microsoft.Win32.RegistryView, valueName As String) As Boolean
            Try
                Dim SubKey As String = keyName.Substring(keyName.IndexOf("\"c) + 1).Replace("\"c, "\\")
                Dim TopKey As String = keyName.Substring(0, keyName.IndexOf("\"c))

                Dim RegistryHive As Microsoft.Win32.RegistryHive = GetRegistryHive(TopKey)

                Using BaseKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.RegistryKey.OpenBaseKey(RegistryHive, registryView)
                    Using Key As Microsoft.Win32.RegistryKey = BaseKey.OpenSubKey(SubKey, True)
                        Key?.DeleteValue(valueName, False)
                    End Using
                End Using

                Return True
            Catch ex As Exception
                ex.ToLog(True)
            End Try

            Return False
        End Function

        Public Function [Get](keyName As String, valueName As String, ByRef value As String) As Boolean
            Dim Result As Object = Nothing

            If [Get](keyName, valueName, Result) Then
                value = Type.ToStringDB(Result)
                Return True
            End If

            value = ""
            Return False
        End Function

        Public Function [Set](keyName As String, valueName As String, value As String) As Boolean
            Return [Set](keyName, Microsoft.Win32.RegistryValueKind.String, Microsoft.Win32.RegistryView.Registry64, valueName, CObj(value))
        End Function

    End Module

End Namespace