Namespace CommonRoutines.Extensions

    Public Module System_String

        <Runtime.CompilerServices.Extension()> Public Function CleanHTML(s As String) As String
            Return s.Replace("\", "/").Replace("//", "/")
        End Function

        <Runtime.CompilerServices.Extension()> Public Function FileExists(s As String) As Boolean
            If s.IsNotSet() Then
                Return False
            End If

            If IO.File.Exists(s) Then
                Return True
            End If

            Return False
        End Function

        <Runtime.CompilerServices.Extension()> Public Function FormatWith(s As String, ParamArray args As Object()) As String
            If s.IsSet() Then
                Return String.Format(GetCurrentCulture(), s, args)
            End If

            Return ""
        End Function

        <Runtime.CompilerServices.Extension()> Public Function GetCleanLogLine(s As String, Optional indent As String = "", Optional title As String = "") As String
            If s.IsNotSet() Then
                Return ""
            End If

            Dim Result As String = s.Trim()

            Do While Result.Contains("  ")
                Result = Result.Replace("  ", " ")
            Loop

            title = "{0}{1}".FormatWith(indent, title)
            indent = ""
            For I As Integer = 1 To title.Length
                indent = " {0}".FormatWith(indent)
            Next

            Result = Result.ReplaceLineEndings()
            Result = Result.Replace(Environment.NewLine, "{0}{1}".FormatWith(Environment.NewLine, indent))

            Return "{0}{1}".FormatWith(title, Result)
        End Function

        <Runtime.CompilerServices.Extension()> Public Function GetSQLVersion(s As String) As String
            Try
                If Not s.Contains(".") Then
                    Return s
                End If

                Dim VersionParts() As String = s.Split("."c)

                If VersionParts.Length < 3 Then
                    Return s
                End If

                Dim Year As Integer = Type.ToIntegerDB(VersionParts(0))
                Dim Release As Integer = Type.ToIntegerDB(VersionParts(1))
                Dim ServicePack As Integer = Type.ToIntegerDB(VersionParts(2))

                Select Case Year
                    Case 16
                        Return "2022"
                    Case 15
                        Return "2019"
                    Case 14
                        Return "2017"
                    Case 13
                        If ServicePack >= 6300 Then
                            Return "2016 SP3"
                        ElseIf ServicePack >= 5026 Then
                            Return "2016 SP2"
                        ElseIf ServicePack >= 4001 Then
                            Return "2016 SP1"
                        Else
                            Return "2016"
                        End If
                    Case 12
                        If ServicePack >= 6024 Then
                            Return "2014 SP3"
                        ElseIf ServicePack >= 5000 Then
                            Return "2014 SP2"
                        ElseIf ServicePack >= 4100 Then
                            Return "2014 SP1"
                        Else
                            Return "2014"
                        End If
                    Case 11
                        If ServicePack >= 7001 Then
                            Return "2012 SP4"
                        ElseIf ServicePack >= 6020 Then
                            Return "2012 SP3"
                        ElseIf ServicePack >= 5058 Then
                            Return "2012 SP2"
                        ElseIf ServicePack >= 3000 Then
                            Return "2012 SP1"
                        Else
                            Return "2012"
                        End If
                    Case 10
                        Select Case Release
                            Case 50
                                If ServicePack >= 6000 Then
                                    Return "2008 R2 SP3"
                                ElseIf ServicePack >= 4000 Then
                                    Return "2008 R2 SP2"
                                ElseIf ServicePack >= 2500 Then
                                    Return "2008 R2 SP1"
                                Else
                                    Return "2008 R2"
                                End If
                            Case 0, 1
                                If ServicePack >= 6000 Then
                                    Return "2008 SP4"
                                ElseIf ServicePack >= 5500 Then
                                    Return "2008 SP3"
                                ElseIf ServicePack >= 4000 Then
                                    Return "2008 SP2"
                                ElseIf ServicePack >= 2531 Then
                                    Return "2008 SP1"
                                Else
                                    Return "2008"
                                End If
                        End Select
                    Case 9
                        If ServicePack >= 5000 Then
                            Return "2005 SP4"
                        ElseIf ServicePack >= 4035 Then
                            Return "2005 SP3"
                        ElseIf ServicePack >= 3042 Then
                            Return "2005 SP2"
                        ElseIf ServicePack >= 2047 Then
                            Return "2005 SP1"
                        Else
                            Return "2005"
                        End If
                    Case 8
                        If ServicePack >= 2039 Then
                            Return "2000 SP4"
                        ElseIf ServicePack >= 760 Then
                            Return "2000 SP3"
                        ElseIf ServicePack >= 532 Then
                            Return "2000 SP2"
                        ElseIf ServicePack >= 384 Then
                            Return "2000 SP1"
                        Else
                            Return "2000"
                        End If
                    Case 7
                        If ServicePack >= 1063 Then
                            Return "7.0 SP4"
                        ElseIf ServicePack >= 961 Then
                            Return "7.0 SP3"
                        ElseIf ServicePack >= 842 Then
                            Return "7.0 SP2"
                        ElseIf ServicePack >= 699 Then
                            Return "7.0 SP1"
                        Else
                            Return "7.0"
                        End If
                    Case 6
                        Select Case Release
                            Case 0
                                If ServicePack >= 151 Then
                                    Return "6.0 SP3"
                                ElseIf ServicePack >= 139 Then
                                    Return "6.0 SP2"
                                ElseIf ServicePack >= 124 Then
                                    Return "6.0 SP1"
                                Else
                                    Return "6.0"
                                End If
                            Case 50
                                If ServicePack >= 416 Then
                                    Return "6.5 SP5"
                                ElseIf ServicePack >= 281 Then
                                    Return "6.5 SP4"
                                ElseIf ServicePack >= 258 Then
                                    Return "6.5 SP3"
                                ElseIf ServicePack >= 240 Then
                                    Return "6.5 SP2"
                                ElseIf ServicePack >= 213 Then
                                    Return "6.5 SP1"
                                Else
                                    Return "6.5"
                                End If
                        End Select
                End Select
            Catch ex As Exception
                ex.ToLog(True)
            End Try

            Return s
        End Function

        <Runtime.CompilerServices.Extension()> Public Function GetVersionLong(s As String) As Long
            If s.IsNotSet() Then
                Return -1
            End If

            s = s.Trim()

            If s.ToLower().StartsWith("beta v") Then
                s = s.Substring(6).Trim()
            End If

            If s.Contains(" "c) Then
                s = s.Substring(0, s.IndexOf(" "c)).Trim()
            End If

            Dim VersionSplit As String() = s.Split("."c)

            If VersionSplit.Count() = 4 Then
                Return (CLng(VersionSplit(0)) * CLng(1000000000)) + (CLng(VersionSplit(1)) * CLng(1000000)) + (CLng(VersionSplit(2)) * CLng(1000)) + (CLng(VersionSplit(3)) * CLng(1))
            ElseIf VersionSplit.Count() = 3 Then
                Return (CLng(VersionSplit(0)) * CLng(1000000000)) + (CLng(VersionSplit(1)) * CLng(1000000)) + (CLng(VersionSplit(2)) * CLng(1000))
            ElseIf VersionSplit.Count() = 2 Then
                Return (CLng(VersionSplit(0)) * CLng(1000000000)) + (CLng(VersionSplit(1)) * CLng(1000000))
            ElseIf VersionSplit.Count() = 1 Then
                Return (CLng(VersionSplit(0)) * CLng(1000000000))
            End If

            Return 0
        End Function

        <Runtime.CompilerServices.Extension()> Public Function IsEqualTo(s As String, value As String) As Boolean
            Return s.IsEqualTo(value, Nothing)
        End Function

        <Runtime.CompilerServices.Extension()> Public Function IsEqualTo(s As String, value As String, stringComparison As StringComparison?) As Boolean
            If stringComparison Is Nothing OrElse Not stringComparison.HasValue Then
                stringComparison = Settings.StringComparison
            End If

            If s Is Nothing AndAlso value Is Nothing Then
                Return True
            End If

            If s Is Nothing OrElse value Is Nothing Then
                Return False
            End If

            Return s.Equals(value, stringComparison.Value)
        End Function

        <Runtime.CompilerServices.Extension()> Public Function IsNotEqualTo(s As String, value As String) As Boolean
            Return Not s.IsEqualTo(value, Nothing)
        End Function

        <Runtime.CompilerServices.Extension()> Public Function IsNotEqualTo(s As String, value As String, stringComparison As StringComparison?) As Boolean
            Return Not s.IsEqualTo(value, stringComparison)
        End Function

        <Runtime.CompilerServices.Extension()> Public Function IsNotSet(s As String) As Boolean
            If s Is Nothing Then
                Return True
            End If

            If String.IsNullOrWhiteSpace(s) Then
                Return True
            End If

            Return False
        End Function

        <Runtime.CompilerServices.Extension()> Public Function IsSet(s As String) As Boolean
            If s Is Nothing Then
                Return False
            End If

            If String.IsNullOrWhiteSpace(s) Then
                Return False
            End If

            Return True
        End Function

        <Runtime.CompilerServices.Extension()> Public Function ReplaceInvalidCharacters(s As String, replaceWith As String) As String
            Dim Result As String = s

            Result = Result.Replace("-", replaceWith)
            Result = Result.Replace("\", replaceWith)
            Result = Result.Replace("/", replaceWith)
            Result = Result.Replace("[", replaceWith)
            Result = Result.Replace("]", replaceWith)
            Result = Result.Replace(" ", replaceWith)
            Result = Result.Replace("?", replaceWith)
            Result = Result.Replace("!", replaceWith)
            Result = Result.Replace("__", replaceWith)

            Return Result
        End Function

        <Runtime.CompilerServices.Extension()> Public Function ReplaceLineEndings(s As String) As String
            Return s.ReplaceLineEndings(Environment.NewLine, True, True)
        End Function

        <Runtime.CompilerServices.Extension()> Public Function ReplaceLineEndings(s As String, replaceDoubles As Boolean, replaceTriples As Boolean) As String
            Return s.ReplaceLineEndings(Environment.NewLine, replaceDoubles, replaceTriples)
        End Function

        <Runtime.CompilerServices.Extension()> Public Function ReplaceLineEndings(s As String, newText As String) As String
            Return s.ReplaceLineEndings(newText, True, True)
        End Function

        <Runtime.CompilerServices.Extension()> Public Function ReplaceLineEndings(s As String, newText As String, replaceDoubles As Boolean, replaceTriples As Boolean) As String
            Dim Result As String = s

            Result = Result.Replace(ChrW(13), newText)
            Result = Result.Replace(vbLf, newText)
            Result = Result.Replace(vbCrLf, newText)
            Result = Result.Replace("{0}{1}".FormatWith(vbCr, newText), newText)
            Result = Result.Replace("{0}{1}".FormatWith(newText, vbCr), newText)
            Result = Result.Replace("{0}{1}".FormatWith(vbLf, newText), newText)
            Result = Result.Replace("{0}{1}".FormatWith(newText, vbLf), newText)

            If replaceTriples Then
                Dim FindText As String = "{0}{0}{0}".FormatWith(newText)
                Dim ReplaceText As String = "{0}{0}".FormatWith(newText)

                Do While Result.Contains(FindText)
                    Result = Result.Replace(FindText, ReplaceText)
                Loop
            End If

            If replaceDoubles Then
                Dim FindText As String = "{0}{0}".FormatWith(newText)

                Do While Result.Contains(FindText)
                    Result = Result.Replace(FindText, newText)
                Loop
            End If

            Return Result
        End Function

        <Runtime.CompilerServices.Extension()> Public Sub Append(ByRef s As String, value As String)
            s.Append("", value)
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub Append(ByRef s As String, seperator As String, value As String)
            If value.IsNotSet() Then
                Return
            End If

            If s.IsSet() Then
                s = "{0}{1}{2}".FormatWith(s, seperator, value)
            Else
                s = value
            End If
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub Append(ByRef s As String, ParamArray args As Object())
            s.Append("", args)
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub Append(ByRef s As String, seperator As String, ParamArray args As Object())
            For Each arg As Object In args
                s.Append(seperator, Type.ToStringDB(arg))
            Next
        End Sub

#Region " Logging "

        <Runtime.CompilerServices.Extension()> Public Sub ToLog(s As String)
            s.ToLog(False, "")
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub ToLog(s As String, silent As Boolean)
            s.ToLog(silent, "")
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub ToLog(s As String, title As String)
            s.ToLog(False, title)
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub ToLog(s As String, silent As Boolean, title As String)
            If s.IsNotSet() Then
                Return
            End If

            Errors.Log(s, silent, title)
        End Sub

#End Region

    End Module

End Namespace