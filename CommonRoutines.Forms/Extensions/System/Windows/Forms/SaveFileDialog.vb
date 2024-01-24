Namespace Extensions

    Public Module System_Windows_Forms_SaveFileDialog

        <Runtime.CompilerServices.Extension()> Public Function STAShowDialog(sfd As SaveFileDialog) As DialogResult
            Dim State As New Models.DialogState With {
                .SaveFileDialog = sfd
            }

            Dim SubThread As New Threading.Thread(AddressOf State.ShowSaveFileDialog)

            SubThread.SetApartmentState(Threading.ApartmentState.STA)
            SubThread.Start()
            SubThread.Join()

            Return State.DialogResult
        End Function

    End Module

End Namespace