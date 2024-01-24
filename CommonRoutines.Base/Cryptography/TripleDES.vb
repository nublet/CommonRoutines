Namespace Cryptography

    Public Class TripleDES

        Private _Password() As Byte = Nothing
        Private _PasswordString As String = ""

        Public Property PasswordString As String
            Get
                Return _PasswordString
            End Get
            Set(value As String)
                _PasswordString = value
                _Password = Security.Cryptography.MD5.HashData(_UTF8.GetBytes(value))
            End Set
        End Property

        Public Sub New()
            Me.New("CommonRoutines.Base")
        End Sub

        Public Sub New(password As String)
            PasswordString = password
        End Sub

#Region " Decrypt "

        Public Function Decrypt(message As String) As String
            Return Decrypt(Convert.FromBase64String(message), _Password)
        End Function

        Public Shared Function Decrypt(message As String, password As String) As String
            Dim UTF8 As New Text.UTF8Encoding()

            Return Decrypt(Convert.FromBase64String(message), Security.Cryptography.MD5.HashData(UTF8.GetBytes(password)))
        End Function

        Public Shared Function Decrypt(message() As Byte, password() As Byte) As String
            Dim Results() As Byte = Nothing

            Using Provider As Security.Cryptography.TripleDES = Security.Cryptography.TripleDES.Create()
                Provider.Key = password
                Provider.Mode = Security.Cryptography.CipherMode.ECB
                Provider.Padding = Security.Cryptography.PaddingMode.PKCS7

                Try
                    Dim Decryptor As Security.Cryptography.ICryptoTransform = Provider.CreateDecryptor()
                    Results = Decryptor.TransformFinalBlock(message, 0, message.Length)
                Catch ex As Exception
                Finally
                    Provider.Clear()
                End Try
            End Using

            Dim UTF8 As New Text.UTF8Encoding()
            Return UTF8.GetString(Results)
        End Function

#End Region

#Region " Encrypt "

        Public Function Encrypt(message As String) As String
            Return Encrypt(_UTF8.GetBytes(message), _Password)
        End Function

        Public Shared Function Encrypt(message As String, password As String) As String
            Return Encrypt(_UTF8.GetBytes(message), Security.Cryptography.MD5.HashData(_UTF8.GetBytes(password)))
        End Function

        Public Shared Function Encrypt(message() As Byte, password() As Byte) As String
            Dim Results() As Byte = Nothing

            Using Provider As Security.Cryptography.TripleDES = Security.Cryptography.TripleDES.Create()
                Provider.Key = password
                Provider.Mode = Security.Cryptography.CipherMode.ECB
                Provider.Padding = Security.Cryptography.PaddingMode.PKCS7

                Try
                    Dim Encryptor As Security.Cryptography.ICryptoTransform = Provider.CreateEncryptor()
                    Results = Encryptor.TransformFinalBlock(message, 0, message.Length)
                Catch ex As Exception
                Finally
                    Provider.Clear()
                End Try
            End Using

            Return Convert.ToBase64String(Results)
        End Function

#End Region

#Region " Shared "

        Private Shared ReadOnly _UTF8 As New Text.UTF8Encoding()

#End Region

    End Class

End Namespace