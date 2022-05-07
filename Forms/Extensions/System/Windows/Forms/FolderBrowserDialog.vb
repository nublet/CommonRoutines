Namespace CommonRoutines.Extensions

    Public Module System_Windows_Forms_FolderBrowserDialog

        <Runtime.CompilerServices.Extension()> Public Function STAShowDialog(fbd As FolderBrowserDialog) As DialogResult
            Dim State As New Models.DialogState With {
                .FolderBrowserDialog = fbd
            }

            Dim SubThread As New Threading.Thread(AddressOf State.ShowFolderBrowserDialog)

            SubThread.SetApartmentState(Threading.ApartmentState.STA)
            SubThread.Start()
            SubThread.Join()

            Return State.DialogResult
        End Function

    End Module

End Namespace