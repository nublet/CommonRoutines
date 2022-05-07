Namespace CommonRoutines.Extensions

    Public Module System_Windows_Forms_TextBox

        <Runtime.CompilerServices.Extension()> Public Sub BrowseForFile(t As TextBox, filter As String)
            t.BrowseForFile(filter, "")
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub BrowseForFile(t As TextBox, filter As String, title As String)
            Using OFD As New OpenFileDialog()
                If t.Text.IsSet() Then
                    OFD.FileName = t.Text
                End If

                If filter.IsSet Then
                    OFD.Filter = filter
                Else
                    OFD.Filter = ""
                End If

                If title.IsSet() Then
                    OFD.Title = title
                End If

                OFD.Multiselect = False
                OFD.RestoreDirectory = False
                OFD.ShowReadOnly = False
                OFD.SupportMultiDottedExtensions = True

                If OFD.STAShowDialog() = Windows.Forms.DialogResult.OK Then
                    t.Text = OFD.FileName
                End If
            End Using
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub BrowseForFolder(t As TextBox)
            t.BrowseForFolder("")
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub BrowseForFolder(t As TextBox, description As String)
            Using FBD As New FolderBrowserDialog()
                If t.Text.IsSet() Then
                    FBD.SelectedPath = t.Text
                End If

                If description.IsSet() Then
                    FBD.Description = description
                End If

                FBD.ShowNewFolderButton = True

                If FBD.STAShowDialog() = Windows.Forms.DialogResult.OK Then
                    t.Text = FBD.SelectedPath
                End If
            End Using
        End Sub

    End Module

End Namespace