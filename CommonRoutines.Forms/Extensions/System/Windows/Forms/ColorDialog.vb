Namespace Extensions

    Public Module System_Windows_Forms_ColorDialog

        <Runtime.CompilerServices.Extension()> Public Function STAShowDialog(cd As ColorDialog) As DialogResult
            Dim State As New Models.DialogState With {
                .ColorDialog = cd
            }

            Dim SubThread As New Threading.Thread(AddressOf State.ShowColorDialog)

            SubThread.SetApartmentState(Threading.ApartmentState.STA)
            SubThread.Start()
            SubThread.Join()

            Return State.DialogResult
        End Function

    End Module

End Namespace