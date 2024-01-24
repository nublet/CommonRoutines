Namespace Extensions

    Public Module System_Windows_Forms_OpenFileDialog

        <Runtime.CompilerServices.Extension()> Public Function STAShowDialog(ofd As OpenFileDialog) As DialogResult
            Dim State As New Models.DialogState With {
                .OpenFileDialog = ofd
            }

            Dim SubThread As New Threading.Thread(AddressOf State.ShowOpenFileDialog)

            SubThread.SetApartmentState(Threading.ApartmentState.STA)
            SubThread.Start()
            SubThread.Join()

            Return State.DialogResult
        End Function

    End Module

End Namespace