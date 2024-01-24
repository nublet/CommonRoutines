Imports System.Net.Http

Public Module HTTPHelpers

    Private ReadOnly _Empty As New Dictionary(Of String, String)

    Public Function DownloadFile(fileName As String, url As String, useCompression As Boolean) As Boolean
        Try
            If fileName.FileExists() Then
                IO.File.Delete(fileName)
            End If

            Dim Buffer(1024) As Byte

            Using Client As New HttpClient()
                If useCompression Then
                    Client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate")
                End If

                Client.DefaultRequestHeaders.CacheControl = New Headers.CacheControlHeaderValue() With {.NoCache = True}
                Client.Timeout = TimeSpan.FromMinutes(5)

                Using response As HttpResponseMessage = Client.GetAsync(url).Result
                    If Not response.IsSuccessStatusCode Then
                        Return False
                    End If

                    Using stream As IO.Stream = response.Content.ReadAsStreamAsync().Result
                        Using Writer As New IO.FileStream(fileName, IO.FileMode.Create, IO.FileAccess.Write)
                            Dim Count As Integer = stream.Read(Buffer, 0, Buffer.Length)

                            Do While (Count > 0)
                                Writer.Write(Buffer, 0, Count)
                                Count = stream.Read(Buffer, 0, Buffer.Length)
                            Loop
                        End Using
                    End Using
                End Using
            End Using

            Return True
        Catch ex As Exception
            ex.ToLog(True, "URL: {0}".FormatWith(url))
        End Try

        Return False
    End Function

    Public Function DownloadURL(fileName As String, url As String, ByRef client As HttpClient) As Boolean
        Return DownloadURL(0, fileName, 0, url, False, client, _Empty)
    End Function

    Public Function DownloadURL(fileName As String, url As String, usePost As Boolean, ByRef client As HttpClient) As Boolean
        Return DownloadURL(0, fileName, 0, url, usePost, client, _Empty)
    End Function

    Public Function DownloadURL(cacheFor As Integer, fileName As String, sleepFor As Integer, url As String, usePost As Boolean, ByRef client As HttpClient, ByRef headers As Dictionary(Of String, String)) As Boolean
        Try
            If fileName.FileExists() Then
                If cacheFor > 0 AndAlso DateDiff(DateInterval.Day, IO.File.GetLastWriteTimeUtc(fileName), GetCurrentDateUTC()) <= cacheFor Then
                    Return True
                End If

                IO.File.Delete(fileName)
            End If

            Dim FolderName As String = fileName.Substring(0, fileName.LastIndexOf("\"c))
            If Not IO.Directory.Exists(FolderName) Then
                IO.Directory.CreateDirectory(FolderName)
            End If

            Dim Request As New HttpRequestMessage(HttpMethod.Get, New Uri(url))

            If usePost Then
                Request.Method = HttpMethod.Post
            End If

            If headers IsNot Nothing AndAlso headers.Count > 0 Then
                For Each kvp As KeyValuePair(Of String, String) In headers
                    Request.Headers.Add(kvp.Key, kvp.Value)
                Next
            End If

            Using Response As HttpResponseMessage = client.SendAsync(Request).Result
                If sleepFor > 0 Then
                    Threading.Thread.Sleep(sleepFor)
                End If

                If Response.IsSuccessStatusCode Then
                    Using fileStream As IO.FileStream = IO.File.Create(fileName)
                        Using responseStream As IO.Stream = Response.Content.ReadAsStreamAsync().Result
                            responseStream.CopyTo(fileStream)
                        End Using
                    End Using

                    Return True
                Else
                    Dim Failed As String = "Request FAILED: {0} - {1}".FormatWith(Response.StatusCode, Response.ReasonPhrase)
                    Failed.ToLog(True, "URL: {0}".FormatWith(url))
                End If
            End Using
        Catch ex As Exception
            ex.ToLog(True, "URL: {0}".FormatWith(url))
        End Try

        Return False
    End Function

    Public Function GetResponseString(url As String, ByRef client As HttpClient) As String
        Return GetResponseString(0, url, False, client, _Empty)
    End Function

    Public Function GetResponseString(url As String, usePost As Boolean, ByRef client As HttpClient) As String
        Return GetResponseString(0, url, usePost, client, _Empty)
    End Function

    Public Function GetResponseString(sleepFor As Integer, url As String, usePost As Boolean, ByRef client As HttpClient, ByRef headers As Dictionary(Of String, String)) As String
        Try
            Dim Request As New HttpRequestMessage(HttpMethod.Get, New Uri(url))

            If usePost Then
                Request.Method = HttpMethod.Post
            End If

            If headers IsNot Nothing AndAlso headers.Count > 0 Then
                For Each kvp As KeyValuePair(Of String, String) In headers
                    Request.Headers.Add(kvp.Key, kvp.Value)
                Next
            End If

            Using Response As HttpResponseMessage = client.SendAsync(Request).Result
                If sleepFor > 0 Then
                    Threading.Thread.Sleep(sleepFor)
                End If

                If Response.IsSuccessStatusCode Then
                    Return Response.Content.ReadAsStringAsync().Result
                Else
                    Dim Failed As String = "Request FAILED: {0} - {1}".FormatWith(Response.StatusCode, Response.ReasonPhrase)
                    Failed.ToLog(True, "URL: {0}".FormatWith(url))
                End If
            End Using
        Catch ex As Exception
            ex.ToLog(True, "URL: {0}".FormatWith(url))
        End Try

        Return ""
    End Function

End Module