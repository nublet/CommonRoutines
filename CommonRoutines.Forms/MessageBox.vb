Namespace MessageBox

    Public Module MessageBox

        Public Function Show(text As String) As DialogResult
            Dim Result As DialogResult

            Using newForm As New UserForms.MessageBox(text)
                Result = newForm.ShowDialog()
            End Using

            Return Result
        End Function

        Public Function Show(text As String, caption As String) As DialogResult
            Dim Result As DialogResult

            Using newForm As New UserForms.MessageBox(text, caption)
                Result = newForm.ShowDialog()
            End Using

            Return Result
        End Function

        Public Function Show(text As String, caption As String, buttons As MessageBoxButtons) As DialogResult
            Dim Result As DialogResult

            Using newForm As New UserForms.MessageBox(text, caption, buttons)
                Result = newForm.ShowDialog()
            End Using

            Return Result
        End Function

        Public Function Show(text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon) As DialogResult
            Dim Result As DialogResult

            Using newForm As New UserForms.MessageBox(text, caption, buttons, icon)
                Result = newForm.ShowDialog()
            End Using

            Return Result
        End Function

        Public Function Show(text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon, defaultButton As MessageBoxDefaultButton) As DialogResult
            Dim Result As DialogResult

            Using newForm As New UserForms.MessageBox(text, caption, buttons, icon, defaultButton)
                Result = newForm.ShowDialog()
            End Using

            Return Result
        End Function

        Public Function Show(owner As IWin32Window, text As String) As DialogResult
            Dim Result As DialogResult

            Using newForm As New UserForms.MessageBox(text)
                Result = newForm.ShowDialog(owner)
            End Using

            Return Result
        End Function

        Public Function Show(owner As IWin32Window, text As String, caption As String) As DialogResult
            Dim Result As DialogResult

            Using newForm As New UserForms.MessageBox(text, caption)
                Result = newForm.ShowDialog(owner)
            End Using

            Return Result
        End Function

        Public Function Show(owner As IWin32Window, text As String, caption As String, buttons As MessageBoxButtons) As DialogResult
            Dim Result As DialogResult

            Using newForm As New UserForms.MessageBox(text, caption, buttons)
                Result = newForm.ShowDialog(owner)
            End Using

            Return Result
        End Function

        Public Function Show(owner As IWin32Window, text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon) As DialogResult
            Dim Result As DialogResult

            Using newForm As New UserForms.MessageBox(text, caption, buttons, icon)
                Result = newForm.ShowDialog(owner)
            End Using

            Return Result
        End Function

        Public Function Show(owner As IWin32Window, text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon, defaultButton As MessageBoxDefaultButton) As DialogResult
            Dim Result As DialogResult

            Using newForm As New UserForms.MessageBox(text, caption, buttons, icon, defaultButton)
                Result = newForm.ShowDialog(owner)
            End Using

            Return Result
        End Function

    End Module

End Namespace