Namespace Cryptography

    Public Module _Shared

        Private ReadOnly _Bytes As Byte() = New Byte() {65, 112, 114, 111, 116, 101, 99, 73, 110, 116, 101, 114, 110, 97, 116, 105}

        Public Function CompressAndEncrypt(fileName As String) As String
            If Not fileName.FileExists() Then
                Return ""
            End If

            Return CompressAndEncrypt(IO.File.ReadAllBytes(fileName))
        End Function

        Public Function CompressAndEncrypt(ByRef data As Byte()) As String
            Try
                If data Is Nothing Then
                    Return ""
                End If
                If data.Length <= 0 Then
                    Return ""
                End If

                Dim TextString As String = ""

                Using input As New IO.MemoryStream(data)
                    Using output As New IO.MemoryStream()
                        Using stream As New IO.Compression.GZipStream(output, IO.Compression.CompressionLevel.Optimal)
                            input.CopyTo(stream)
                        End Using

                        TextString = Convert.ToBase64String(output.ToArray())
                    End Using
                End Using

                Using aes As Security.Cryptography.Aes = Security.Cryptography.Aes.Create()
                    aes.IV = _Bytes
                    aes.Key = _Bytes

                    Using cryptoTransform As Security.Cryptography.ICryptoTransform = aes.CreateEncryptor(aes.Key, aes.IV)
                        Using buffer As New IO.MemoryStream()
                            Using cryptoStream As New Security.Cryptography.CryptoStream(buffer, cryptoTransform, Security.Cryptography.CryptoStreamMode.Write)
                                Using writer As New IO.StreamWriter(cryptoStream, System.Text.Encoding.Unicode)
                                    writer.Write(TextString)
                                End Using

                                Return Convert.ToBase64String(buffer.ToArray())
                            End Using
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                ex.ToLog()
            End Try

            Return ""
        End Function

        Public Function DecryptAndDecompress(originalValue As String) As String
            Try
                If originalValue.IsNotSet() Then
                    Return ""
                End If

                Dim EncryptedData As Byte() = Convert.FromBase64String(originalValue)
                Dim CompressedValue As String = ""

                Using aes As Security.Cryptography.Aes = Security.Cryptography.Aes.Create()
                    aes.IV = _Bytes
                    aes.Key = _Bytes

                    Using cryptoTransform As Security.Cryptography.ICryptoTransform = aes.CreateDecryptor(aes.Key, aes.IV)
                        Using buffer As New IO.MemoryStream(EncryptedData)
                            Using stream As New Security.Cryptography.CryptoStream(buffer, cryptoTransform, Security.Cryptography.CryptoStreamMode.Read)
                                Using reader As New IO.StreamReader(stream, Text.Encoding.Unicode)
                                    CompressedValue = reader.ReadToEnd()
                                End Using
                            End Using
                        End Using
                    End Using
                End Using

                Using memoryStream As New IO.MemoryStream(Convert.FromBase64String(CompressedValue))
                    Using outputStream As New IO.MemoryStream()
                        Using gzipStream As New IO.Compression.GZipStream(memoryStream, IO.Compression.CompressionMode.Decompress)
                            gzipStream.CopyTo(outputStream)
                        End Using

                        Return Text.Encoding.UTF8.GetString(outputStream.ToArray())
                    End Using
                End Using
            Catch ex As Exception
                ex.ToLog(True)
            End Try

            Return ""
        End Function

    End Module

End Namespace