Namespace CommonRoutines.Models

    Public Class DialogState

        Public DialogResult As DialogResult

        Public ColorDialog As ColorDialog
        Public FolderBrowserDialog As FolderBrowserDialog
        Public OpenFileDialog As OpenFileDialog
        Public PrintPreviewDialog As PrintPreviewDialog
        Public SaveFileDialog As SaveFileDialog

        Public Sub ShowColorDialog()
            DialogResult = ColorDialog.ShowDialog
        End Sub

        Public Sub ShowFolderBrowserDialog()
            DialogResult = FolderBrowserDialog.ShowDialog
        End Sub

        Public Sub ShowOpenFileDialog()
            DialogResult = OpenFileDialog.ShowDialog
        End Sub

        Public Sub ShowPrintPreviewDialog()
            DialogResult = PrintPreviewDialog.ShowDialog
        End Sub

        Public Sub ShowSaveFileDialog()
            DialogResult = SaveFileDialog.ShowDialog
        End Sub

    End Class

End Namespace