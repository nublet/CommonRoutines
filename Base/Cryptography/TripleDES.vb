Namespace CommonRoutines.Cryptography

    Public Class TripleDES

        Private _Password() As Byte = Nothing
        Private _PasswordString As String = ""

        Public Property PasswordString As String
            Get
                Return _PasswordString
            End Get
            Set(value As String)
                _PasswordString = value

                Dim UTF8 As New Text.UTF8Encoding()
                Using Provider As New Security.Cryptography.MD5CryptoServiceProvider()
                    _Password = Provider.ComputeHash(UTF8.GetBytes(value))
                End Using
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

        Public Function Decrypt(message As String, password As String) As String
            Dim UTF8 As New System.Text.UTF8Encoding()

            Using Provider As New System.Security.Cryptography.MD5CryptoServiceProvider()
                Return Decrypt(Convert.FromBase64String(message), Provider.ComputeHash(UTF8.GetBytes(password)))
            End Using
        End Function

        Public Function Decrypt(message() As Byte, password() As Byte) As String
            Dim Results() As Byte = Nothing

            Using TDESProvider As New System.Security.Cryptography.TripleDESCryptoServiceProvider()
                TDESProvider.Key = password
                TDESProvider.Mode = System.Security.Cryptography.CipherMode.ECB
                TDESProvider.Padding = System.Security.Cryptography.PaddingMode.PKCS7

                Try
                    Dim Decryptor As System.Security.Cryptography.ICryptoTransform = TDESProvider.CreateDecryptor()
                    Results = Decryptor.TransformFinalBlock(message, 0, message.Length)
                Catch ex As Exception
                Finally
                    TDESProvider.Clear()
                End Try
            End Using

            Dim UTF8 As New System.Text.UTF8Encoding()
            Return UTF8.GetString(Results)
        End Function

#End Region

#Region " Encrypt "

        Public Function Encrypt(message As String) As String
            Dim UTF8 As New System.Text.UTF8Encoding()

            Return Encrypt(UTF8.GetBytes(message), _Password)
        End Function

        Public Function Encrypt(message As String, password As String) As String
            Dim UTF8 As New System.Text.UTF8Encoding()

            Using Provider As New System.Security.Cryptography.MD5CryptoServiceProvider()
                Return Encrypt(UTF8.GetBytes(message), Provider.ComputeHash(UTF8.GetBytes(password)))
            End Using
        End Function

        Public Function Encrypt(message() As Byte, password() As Byte) As String
            Dim Results() As Byte = Nothing

            Using TDESProvider As New System.Security.Cryptography.TripleDESCryptoServiceProvider()
                TDESProvider.Key = password
                TDESProvider.Mode = System.Security.Cryptography.CipherMode.ECB
                TDESProvider.Padding = System.Security.Cryptography.PaddingMode.PKCS7

                Try
                    Dim Encryptor As System.Security.Cryptography.ICryptoTransform = TDESProvider.CreateEncryptor()
                    Results = Encryptor.TransformFinalBlock(message, 0, message.Length)
                Catch ex As Exception
                Finally
                    TDESProvider.Clear()
                End Try
            End Using

            Return Convert.ToBase64String(Results)
        End Function

#End Region

    End Class

End Namespace