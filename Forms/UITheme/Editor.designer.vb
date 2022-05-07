Namespace CommonRoutines.UITheme

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class Editor
        Inherits System.Windows.Forms.UserControl

        'UserControl overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.CancelButton = New System.Windows.Forms.Button()
            Me.PreviewButton = New System.Windows.Forms.Button()
            Me.SaveButton = New System.Windows.Forms.Button()
            Me.FormBackgroundLabel = New System.Windows.Forms.Label()
            Me.FormBackgroundPreviewLabel = New System.Windows.Forms.Label()
            Me.BackgroundPreviewLabel = New System.Windows.Forms.Label()
            Me.BackgroundLabel = New System.Windows.Forms.Label()
            Me.ForegroundPreviewLabel = New System.Windows.Forms.Label()
            Me.ForegroundLabel = New System.Windows.Forms.Label()
            Me.LineColorPreviewLabel = New System.Windows.Forms.Label()
            Me.LineColorLabel = New System.Windows.Forms.Label()
            Me.BorderColorPreviewLabel = New System.Windows.Forms.Label()
            Me.BorderColorLabel = New System.Windows.Forms.Label()
            Me.SelectionBackgroundPreviewLabel = New System.Windows.Forms.Label()
            Me.SelectionBackgroundLabel = New System.Windows.Forms.Label()
            Me.SelectionForegroundPreviewLabel = New System.Windows.Forms.Label()
            Me.SelectionForegroundLabel = New System.Windows.Forms.Label()
            Me.DefaultButton = New System.Windows.Forms.Button()
            Me.FromThemeButton = New System.Windows.Forms.Button()
            Me.GridFontSizeLabel = New System.Windows.Forms.Label()
            Me.GridFontSizeTextBox = New CommonRoutines.Controls.TextBox()
            Me.SuspendLayout()
            '
            'CancelButton
            '
            Me.CancelButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CancelButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(63, Byte), Integer))
            Me.CancelButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(107, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.CancelButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
            Me.CancelButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(124, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(181, Byte), Integer))
            Me.CancelButton.Location = New System.Drawing.Point(438, 386)
            Me.CancelButton.Margin = New System.Windows.Forms.Padding(5)
            Me.CancelButton.Name = "CancelButton"
            Me.CancelButton.Size = New System.Drawing.Size(80, 30)
            Me.CancelButton.TabIndex = 38
            Me.CancelButton.Text = "Cancel"
            Me.CancelButton.UseVisualStyleBackColor = False
            '
            'PreviewButton
            '
            Me.PreviewButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.PreviewButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(63, Byte), Integer))
            Me.PreviewButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(107, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.PreviewButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.PreviewButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
            Me.PreviewButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(124, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(181, Byte), Integer))
            Me.PreviewButton.Location = New System.Drawing.Point(8, 386)
            Me.PreviewButton.Margin = New System.Windows.Forms.Padding(5)
            Me.PreviewButton.Name = "PreviewButton"
            Me.PreviewButton.Size = New System.Drawing.Size(80, 30)
            Me.PreviewButton.TabIndex = 61
            Me.PreviewButton.Text = "Preview"
            Me.PreviewButton.UseVisualStyleBackColor = False
            '
            'SaveButton
            '
            Me.SaveButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SaveButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(63, Byte), Integer))
            Me.SaveButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(107, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.SaveButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
            Me.SaveButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(124, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(181, Byte), Integer))
            Me.SaveButton.Location = New System.Drawing.Point(348, 386)
            Me.SaveButton.Margin = New System.Windows.Forms.Padding(5)
            Me.SaveButton.Name = "SaveButton"
            Me.SaveButton.Size = New System.Drawing.Size(80, 30)
            Me.SaveButton.TabIndex = 67
            Me.SaveButton.Text = "Save"
            Me.SaveButton.UseVisualStyleBackColor = False
            '
            'FormBackgroundLabel
            '
            Me.FormBackgroundLabel.Cursor = System.Windows.Forms.Cursors.Default
            Me.FormBackgroundLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
            Me.FormBackgroundLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(124, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(181, Byte), Integer))
            Me.FormBackgroundLabel.Location = New System.Drawing.Point(5, 125)
            Me.FormBackgroundLabel.Margin = New System.Windows.Forms.Padding(5)
            Me.FormBackgroundLabel.Name = "FormBackgroundLabel"
            Me.FormBackgroundLabel.Size = New System.Drawing.Size(160, 30)
            Me.FormBackgroundLabel.TabIndex = 75
            Me.FormBackgroundLabel.Text = "Form Background"
            Me.FormBackgroundLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'FormBackgroundPreviewLabel
            '
            Me.FormBackgroundPreviewLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.FormBackgroundPreviewLabel.Cursor = System.Windows.Forms.Cursors.Hand
            Me.FormBackgroundPreviewLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
            Me.FormBackgroundPreviewLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(124, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(181, Byte), Integer))
            Me.FormBackgroundPreviewLabel.Location = New System.Drawing.Point(175, 125)
            Me.FormBackgroundPreviewLabel.Margin = New System.Windows.Forms.Padding(5)
            Me.FormBackgroundPreviewLabel.Name = "FormBackgroundPreviewLabel"
            Me.FormBackgroundPreviewLabel.Size = New System.Drawing.Size(150, 30)
            Me.FormBackgroundPreviewLabel.TabIndex = 76
            Me.FormBackgroundPreviewLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'BackgroundPreviewLabel
            '
            Me.BackgroundPreviewLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.BackgroundPreviewLabel.Cursor = System.Windows.Forms.Cursors.Hand
            Me.BackgroundPreviewLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
            Me.BackgroundPreviewLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(124, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(181, Byte), Integer))
            Me.BackgroundPreviewLabel.Location = New System.Drawing.Point(175, 5)
            Me.BackgroundPreviewLabel.Margin = New System.Windows.Forms.Padding(5)
            Me.BackgroundPreviewLabel.Name = "BackgroundPreviewLabel"
            Me.BackgroundPreviewLabel.Size = New System.Drawing.Size(150, 30)
            Me.BackgroundPreviewLabel.TabIndex = 78
            Me.BackgroundPreviewLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'BackgroundLabel
            '
            Me.BackgroundLabel.Cursor = System.Windows.Forms.Cursors.Default
            Me.BackgroundLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
            Me.BackgroundLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(124, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(181, Byte), Integer))
            Me.BackgroundLabel.Location = New System.Drawing.Point(5, 5)
            Me.BackgroundLabel.Margin = New System.Windows.Forms.Padding(5)
            Me.BackgroundLabel.Name = "BackgroundLabel"
            Me.BackgroundLabel.Size = New System.Drawing.Size(160, 30)
            Me.BackgroundLabel.TabIndex = 77
            Me.BackgroundLabel.Text = "Background"
            Me.BackgroundLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'ForegroundPreviewLabel
            '
            Me.ForegroundPreviewLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.ForegroundPreviewLabel.Cursor = System.Windows.Forms.Cursors.Hand
            Me.ForegroundPreviewLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
            Me.ForegroundPreviewLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(124, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(181, Byte), Integer))
            Me.ForegroundPreviewLabel.Location = New System.Drawing.Point(175, 85)
            Me.ForegroundPreviewLabel.Margin = New System.Windows.Forms.Padding(5)
            Me.ForegroundPreviewLabel.Name = "ForegroundPreviewLabel"
            Me.ForegroundPreviewLabel.Size = New System.Drawing.Size(150, 30)
            Me.ForegroundPreviewLabel.TabIndex = 80
            Me.ForegroundPreviewLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'ForegroundLabel
            '
            Me.ForegroundLabel.Cursor = System.Windows.Forms.Cursors.Default
            Me.ForegroundLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
            Me.ForegroundLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(124, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(181, Byte), Integer))
            Me.ForegroundLabel.Location = New System.Drawing.Point(5, 85)
            Me.ForegroundLabel.Margin = New System.Windows.Forms.Padding(5)
            Me.ForegroundLabel.Name = "ForegroundLabel"
            Me.ForegroundLabel.Size = New System.Drawing.Size(160, 30)
            Me.ForegroundLabel.TabIndex = 79
            Me.ForegroundLabel.Text = "Foreground"
            Me.ForegroundLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'LineColorPreviewLabel
            '
            Me.LineColorPreviewLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.LineColorPreviewLabel.Cursor = System.Windows.Forms.Cursors.Hand
            Me.LineColorPreviewLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
            Me.LineColorPreviewLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(124, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(181, Byte), Integer))
            Me.LineColorPreviewLabel.Location = New System.Drawing.Point(175, 165)
            Me.LineColorPreviewLabel.Margin = New System.Windows.Forms.Padding(5)
            Me.LineColorPreviewLabel.Name = "LineColorPreviewLabel"
            Me.LineColorPreviewLabel.Size = New System.Drawing.Size(150, 30)
            Me.LineColorPreviewLabel.TabIndex = 82
            Me.LineColorPreviewLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'LineColorLabel
            '
            Me.LineColorLabel.Cursor = System.Windows.Forms.Cursors.Default
            Me.LineColorLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
            Me.LineColorLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(124, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(181, Byte), Integer))
            Me.LineColorLabel.Location = New System.Drawing.Point(5, 165)
            Me.LineColorLabel.Margin = New System.Windows.Forms.Padding(5)
            Me.LineColorLabel.Name = "LineColorLabel"
            Me.LineColorLabel.Size = New System.Drawing.Size(160, 30)
            Me.LineColorLabel.TabIndex = 81
            Me.LineColorLabel.Text = "Line Color"
            Me.LineColorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'BorderColorPreviewLabel
            '
            Me.BorderColorPreviewLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.BorderColorPreviewLabel.Cursor = System.Windows.Forms.Cursors.Hand
            Me.BorderColorPreviewLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
            Me.BorderColorPreviewLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(124, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(181, Byte), Integer))
            Me.BorderColorPreviewLabel.Location = New System.Drawing.Point(175, 45)
            Me.BorderColorPreviewLabel.Margin = New System.Windows.Forms.Padding(5)
            Me.BorderColorPreviewLabel.Name = "BorderColorPreviewLabel"
            Me.BorderColorPreviewLabel.Size = New System.Drawing.Size(150, 30)
            Me.BorderColorPreviewLabel.TabIndex = 84
            Me.BorderColorPreviewLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'BorderColorLabel
            '
            Me.BorderColorLabel.Cursor = System.Windows.Forms.Cursors.Default
            Me.BorderColorLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
            Me.BorderColorLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(124, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(181, Byte), Integer))
            Me.BorderColorLabel.Location = New System.Drawing.Point(5, 45)
            Me.BorderColorLabel.Margin = New System.Windows.Forms.Padding(5)
            Me.BorderColorLabel.Name = "BorderColorLabel"
            Me.BorderColorLabel.Size = New System.Drawing.Size(160, 30)
            Me.BorderColorLabel.TabIndex = 83
            Me.BorderColorLabel.Text = "Border Color"
            Me.BorderColorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'SelectionBackgroundPreviewLabel
            '
            Me.SelectionBackgroundPreviewLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.SelectionBackgroundPreviewLabel.Cursor = System.Windows.Forms.Cursors.Hand
            Me.SelectionBackgroundPreviewLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
            Me.SelectionBackgroundPreviewLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(124, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(181, Byte), Integer))
            Me.SelectionBackgroundPreviewLabel.Location = New System.Drawing.Point(175, 205)
            Me.SelectionBackgroundPreviewLabel.Margin = New System.Windows.Forms.Padding(5)
            Me.SelectionBackgroundPreviewLabel.Name = "SelectionBackgroundPreviewLabel"
            Me.SelectionBackgroundPreviewLabel.Size = New System.Drawing.Size(150, 30)
            Me.SelectionBackgroundPreviewLabel.TabIndex = 86
            Me.SelectionBackgroundPreviewLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'SelectionBackgroundLabel
            '
            Me.SelectionBackgroundLabel.Cursor = System.Windows.Forms.Cursors.Default
            Me.SelectionBackgroundLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
            Me.SelectionBackgroundLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(124, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(181, Byte), Integer))
            Me.SelectionBackgroundLabel.Location = New System.Drawing.Point(5, 205)
            Me.SelectionBackgroundLabel.Margin = New System.Windows.Forms.Padding(5)
            Me.SelectionBackgroundLabel.Name = "SelectionBackgroundLabel"
            Me.SelectionBackgroundLabel.Size = New System.Drawing.Size(160, 30)
            Me.SelectionBackgroundLabel.TabIndex = 85
            Me.SelectionBackgroundLabel.Text = "Selection Background"
            Me.SelectionBackgroundLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'SelectionForegroundPreviewLabel
            '
            Me.SelectionForegroundPreviewLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.SelectionForegroundPreviewLabel.Cursor = System.Windows.Forms.Cursors.Hand
            Me.SelectionForegroundPreviewLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
            Me.SelectionForegroundPreviewLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(124, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(181, Byte), Integer))
            Me.SelectionForegroundPreviewLabel.Location = New System.Drawing.Point(175, 245)
            Me.SelectionForegroundPreviewLabel.Margin = New System.Windows.Forms.Padding(5)
            Me.SelectionForegroundPreviewLabel.Name = "SelectionForegroundPreviewLabel"
            Me.SelectionForegroundPreviewLabel.Size = New System.Drawing.Size(150, 30)
            Me.SelectionForegroundPreviewLabel.TabIndex = 88
            Me.SelectionForegroundPreviewLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'SelectionForegroundLabel
            '
            Me.SelectionForegroundLabel.Cursor = System.Windows.Forms.Cursors.Default
            Me.SelectionForegroundLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
            Me.SelectionForegroundLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(124, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(181, Byte), Integer))
            Me.SelectionForegroundLabel.Location = New System.Drawing.Point(5, 245)
            Me.SelectionForegroundLabel.Margin = New System.Windows.Forms.Padding(5)
            Me.SelectionForegroundLabel.Name = "SelectionForegroundLabel"
            Me.SelectionForegroundLabel.Size = New System.Drawing.Size(160, 30)
            Me.SelectionForegroundLabel.TabIndex = 87
            Me.SelectionForegroundLabel.Text = "Selection Foreground"
            Me.SelectionForegroundLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'DefaultButton
            '
            Me.DefaultButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.DefaultButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(63, Byte), Integer))
            Me.DefaultButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(107, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.DefaultButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.DefaultButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
            Me.DefaultButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(124, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(181, Byte), Integer))
            Me.DefaultButton.Location = New System.Drawing.Point(412, 5)
            Me.DefaultButton.Margin = New System.Windows.Forms.Padding(5)
            Me.DefaultButton.Name = "DefaultButton"
            Me.DefaultButton.Size = New System.Drawing.Size(105, 30)
            Me.DefaultButton.TabIndex = 89
            Me.DefaultButton.Text = "Default"
            Me.DefaultButton.UseVisualStyleBackColor = False
            '
            'FromThemeButton
            '
            Me.FromThemeButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.FromThemeButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(63, Byte), Integer))
            Me.FromThemeButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(107, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.FromThemeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.FromThemeButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
            Me.FromThemeButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(124, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(181, Byte), Integer))
            Me.FromThemeButton.Location = New System.Drawing.Point(412, 45)
            Me.FromThemeButton.Margin = New System.Windows.Forms.Padding(5)
            Me.FromThemeButton.Name = "FromThemeButton"
            Me.FromThemeButton.Size = New System.Drawing.Size(105, 30)
            Me.FromThemeButton.TabIndex = 90
            Me.FromThemeButton.Text = "From Theme"
            Me.FromThemeButton.UseVisualStyleBackColor = False
            '
            'GridFontSizeLabel
            '
            Me.GridFontSizeLabel.Cursor = System.Windows.Forms.Cursors.Default
            Me.GridFontSizeLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
            Me.GridFontSizeLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(124, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(181, Byte), Integer))
            Me.GridFontSizeLabel.Location = New System.Drawing.Point(5, 285)
            Me.GridFontSizeLabel.Margin = New System.Windows.Forms.Padding(5)
            Me.GridFontSizeLabel.Name = "GridFontSizeLabel"
            Me.GridFontSizeLabel.Size = New System.Drawing.Size(160, 33)
            Me.GridFontSizeLabel.TabIndex = 91
            Me.GridFontSizeLabel.Text = "Grid Font Size"
            Me.GridFontSizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'GridFontSizeTextBox
            '
            Me.GridFontSizeTextBox.BackColor = System.Drawing.SystemColors.Window
            Me.GridFontSizeTextBox.BorderColor = System.Drawing.Color.MediumSlateBlue
            Me.GridFontSizeTextBox.BorderFocusColor = System.Drawing.Color.HotPink
            Me.GridFontSizeTextBox.BorderRadius = 0
            Me.GridFontSizeTextBox.BorderSize = 1
            Me.GridFontSizeTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.GridFontSizeTextBox.ForeColor = System.Drawing.Color.DimGray
            Me.GridFontSizeTextBox.Location = New System.Drawing.Point(175, 285)
            Me.GridFontSizeTextBox.Margin = New System.Windows.Forms.Padding(5)
            Me.GridFontSizeTextBox.Multiline = False
            Me.GridFontSizeTextBox.Name = "GridFontSizeTextBox"
            Me.GridFontSizeTextBox.Padding = New System.Windows.Forms.Padding(10, 7, 10, 7)
            Me.GridFontSizeTextBox.PasswordChar = False
            Me.GridFontSizeTextBox.PlaceHolderColor = System.Drawing.Color.DarkGray
            Me.GridFontSizeTextBox.PlaceHolderText = ""
            Me.GridFontSizeTextBox.Size = New System.Drawing.Size(50, 33)
            Me.GridFontSizeTextBox.TabIndex = 92
            Me.GridFontSizeTextBox.UnderlinedStyle = False
            '
            'Editor
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(86, Byte), Integer))
            Me.Controls.Add(Me.GridFontSizeTextBox)
            Me.Controls.Add(Me.GridFontSizeLabel)
            Me.Controls.Add(Me.FromThemeButton)
            Me.Controls.Add(Me.DefaultButton)
            Me.Controls.Add(Me.SelectionForegroundPreviewLabel)
            Me.Controls.Add(Me.SelectionForegroundLabel)
            Me.Controls.Add(Me.SelectionBackgroundPreviewLabel)
            Me.Controls.Add(Me.SelectionBackgroundLabel)
            Me.Controls.Add(Me.BorderColorPreviewLabel)
            Me.Controls.Add(Me.BorderColorLabel)
            Me.Controls.Add(Me.LineColorPreviewLabel)
            Me.Controls.Add(Me.LineColorLabel)
            Me.Controls.Add(Me.ForegroundPreviewLabel)
            Me.Controls.Add(Me.ForegroundLabel)
            Me.Controls.Add(Me.BackgroundPreviewLabel)
            Me.Controls.Add(Me.BackgroundLabel)
            Me.Controls.Add(Me.FormBackgroundPreviewLabel)
            Me.Controls.Add(Me.FormBackgroundLabel)
            Me.Controls.Add(Me.SaveButton)
            Me.Controls.Add(Me.PreviewButton)
            Me.Controls.Add(Me.CancelButton)
            Me.DoubleBuffered = True
            Me.Margin = New System.Windows.Forms.Padding(5)
            Me.Name = "Editor"
            Me.Size = New System.Drawing.Size(522, 421)
            Me.ResumeLayout(False)

        End Sub
        Private WithEvents CancelButton As Button
        Private WithEvents PreviewButton As Button
        Private WithEvents SaveButton As Button
        Private WithEvents FormBackgroundLabel As Label
        Private WithEvents FormBackgroundPreviewLabel As Label
        Private WithEvents BackgroundPreviewLabel As Label
        Private WithEvents BackgroundLabel As Label
        Private WithEvents ForegroundPreviewLabel As Label
        Private WithEvents ForegroundLabel As Label
        Private WithEvents LineColorPreviewLabel As Label
        Private WithEvents LineColorLabel As Label
        Private WithEvents BorderColorPreviewLabel As Label
        Private WithEvents BorderColorLabel As Label
        Private WithEvents SelectionBackgroundPreviewLabel As Label
        Private WithEvents SelectionBackgroundLabel As Label
        Private WithEvents SelectionForegroundPreviewLabel As Label
        Private WithEvents SelectionForegroundLabel As Label
        Private WithEvents DefaultButton As Button
        Private WithEvents FromThemeButton As Button
        Private WithEvents GridFontSizeLabel As Label
        Friend WithEvents GridFontSizeTextBox As Controls.TextBox
    End Class

End Namespace