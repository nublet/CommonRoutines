Namespace UserForms

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class MessageBox
        Inherits System.Windows.Forms.Form

        'Form overrides dispose to clean up the component list.
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
            Me.TitlePanel = New System.Windows.Forms.Panel()
            Me.CaptionLabel = New System.Windows.Forms.Label()
            Me.CloseButton = New System.Windows.Forms.Button()
            Me.ButtonPanel = New System.Windows.Forms.Panel()
            Me.BugPictureBox = New System.Windows.Forms.PictureBox()
            Me.ThirdButton = New System.Windows.Forms.Button()
            Me.SecondButton = New System.Windows.Forms.Button()
            Me.FirstButton = New System.Windows.Forms.Button()
            Me.BodyPanel = New System.Windows.Forms.Panel()
            Me.MessageLabel = New System.Windows.Forms.Label()
            Me.IconPictureBox = New System.Windows.Forms.PictureBox()
            Me.TitlePanel.SuspendLayout()
            Me.ButtonPanel.SuspendLayout()
            CType(Me.BugPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.BodyPanel.SuspendLayout()
            CType(Me.IconPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'TitlePanel
            '
            Me.TitlePanel.BackColor = System.Drawing.Color.CornflowerBlue
            Me.TitlePanel.Controls.Add(Me.CaptionLabel)
            Me.TitlePanel.Controls.Add(Me.CloseButton)
            Me.TitlePanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.TitlePanel.Location = New System.Drawing.Point(0, 0)
            Me.TitlePanel.Name = "TitlePanel"
            Me.TitlePanel.Size = New System.Drawing.Size(350, 35)
            Me.TitlePanel.TabIndex = 1
            '
            'CaptionLabel
            '
            Me.CaptionLabel.AutoSize = True
            Me.CaptionLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CaptionLabel.ForeColor = System.Drawing.Color.White
            Me.CaptionLabel.Location = New System.Drawing.Point(9, 8)
            Me.CaptionLabel.Name = "CaptionLabel"
            Me.CaptionLabel.Size = New System.Drawing.Size(86, 17)
            Me.CaptionLabel.TabIndex = 4
            Me.CaptionLabel.Text = "labelCaption"
            '
            'CloseButton
            '
            Me.CloseButton.Dock = System.Windows.Forms.DockStyle.Right
            Me.CloseButton.FlatAppearance.BorderSize = 0
            Me.CloseButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(79, Byte), Integer), CType(CType(95, Byte), Integer))
            Me.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.CloseButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.CloseButton.ForeColor = System.Drawing.Color.White
            Me.CloseButton.Location = New System.Drawing.Point(310, 0)
            Me.CloseButton.Name = "CloseButton"
            Me.CloseButton.Size = New System.Drawing.Size(40, 35)
            Me.CloseButton.TabIndex = 3
            Me.CloseButton.Text = "X"
            Me.CloseButton.UseVisualStyleBackColor = False
            '
            'ButtonPanel
            '
            Me.ButtonPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
            Me.ButtonPanel.Controls.Add(Me.BugPictureBox)
            Me.ButtonPanel.Controls.Add(Me.ThirdButton)
            Me.ButtonPanel.Controls.Add(Me.SecondButton)
            Me.ButtonPanel.Controls.Add(Me.FirstButton)
            Me.ButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.ButtonPanel.Location = New System.Drawing.Point(0, 90)
            Me.ButtonPanel.Name = "ButtonPanel"
            Me.ButtonPanel.Size = New System.Drawing.Size(350, 60)
            Me.ButtonPanel.TabIndex = 2
            '
            'BugPictureBox
            '
            Me.BugPictureBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.BugPictureBox.Cursor = System.Windows.Forms.Cursors.Hand
            Me.BugPictureBox.Location = New System.Drawing.Point(0, 30)
            Me.BugPictureBox.Margin = New System.Windows.Forms.Padding(0)
            Me.BugPictureBox.Name = "BugPictureBox"
            Me.BugPictureBox.Size = New System.Drawing.Size(30, 30)
            Me.BugPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
            Me.BugPictureBox.TabIndex = 3
            Me.BugPictureBox.TabStop = False
            Me.BugPictureBox.Visible = False
            '
            'ThirdButton
            '
            Me.ThirdButton.BackColor = System.Drawing.Color.SeaGreen
            Me.ThirdButton.FlatAppearance.BorderSize = 0
            Me.ThirdButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.ThirdButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ThirdButton.ForeColor = System.Drawing.Color.WhiteSmoke
            Me.ThirdButton.Location = New System.Drawing.Point(231, 12)
            Me.ThirdButton.Name = "ThirdButton"
            Me.ThirdButton.Size = New System.Drawing.Size(100, 35)
            Me.ThirdButton.TabIndex = 2
            Me.ThirdButton.Text = "button3"
            Me.ThirdButton.UseVisualStyleBackColor = False
            '
            'SecondButton
            '
            Me.SecondButton.BackColor = System.Drawing.Color.SeaGreen
            Me.SecondButton.FlatAppearance.BorderSize = 0
            Me.SecondButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.SecondButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.SecondButton.ForeColor = System.Drawing.Color.WhiteSmoke
            Me.SecondButton.Location = New System.Drawing.Point(125, 12)
            Me.SecondButton.Name = "SecondButton"
            Me.SecondButton.Size = New System.Drawing.Size(100, 35)
            Me.SecondButton.TabIndex = 1
            Me.SecondButton.Text = "button2"
            Me.SecondButton.UseVisualStyleBackColor = False
            '
            'FirstButton
            '
            Me.FirstButton.BackColor = System.Drawing.Color.SeaGreen
            Me.FirstButton.FlatAppearance.BorderSize = 0
            Me.FirstButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.FirstButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FirstButton.ForeColor = System.Drawing.Color.WhiteSmoke
            Me.FirstButton.Location = New System.Drawing.Point(19, 12)
            Me.FirstButton.Name = "FirstButton"
            Me.FirstButton.Size = New System.Drawing.Size(100, 35)
            Me.FirstButton.TabIndex = 0
            Me.FirstButton.Text = "button1"
            Me.FirstButton.UseVisualStyleBackColor = False
            '
            'BodyPanel
            '
            Me.BodyPanel.BackColor = System.Drawing.Color.WhiteSmoke
            Me.BodyPanel.Controls.Add(Me.MessageLabel)
            Me.BodyPanel.Controls.Add(Me.IconPictureBox)
            Me.BodyPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.BodyPanel.Location = New System.Drawing.Point(0, 35)
            Me.BodyPanel.Name = "BodyPanel"
            Me.BodyPanel.Padding = New System.Windows.Forms.Padding(10, 10, 0, 0)
            Me.BodyPanel.Size = New System.Drawing.Size(350, 55)
            Me.BodyPanel.TabIndex = 3
            '
            'MessageLabel
            '
            Me.MessageLabel.AutoSize = True
            Me.MessageLabel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.MessageLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.MessageLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(85, Byte), Integer))
            Me.MessageLabel.Location = New System.Drawing.Point(50, 10)
            Me.MessageLabel.MaximumSize = New System.Drawing.Size(600, 0)
            Me.MessageLabel.Name = "MessageLabel"
            Me.MessageLabel.Padding = New System.Windows.Forms.Padding(5, 5, 10, 15)
            Me.MessageLabel.Size = New System.Drawing.Size(110, 37)
            Me.MessageLabel.TabIndex = 1
            Me.MessageLabel.Text = "labelMessage"
            Me.MessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'IconPictureBox
            '
            Me.IconPictureBox.Dock = System.Windows.Forms.DockStyle.Left
            Me.IconPictureBox.Location = New System.Drawing.Point(10, 10)
            Me.IconPictureBox.Name = "IconPictureBox"
            Me.IconPictureBox.Size = New System.Drawing.Size(40, 45)
            Me.IconPictureBox.TabIndex = 0
            Me.IconPictureBox.TabStop = False
            '
            'MessageBox
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(350, 150)
            Me.Controls.Add(Me.BodyPanel)
            Me.Controls.Add(Me.ButtonPanel)
            Me.Controls.Add(Me.TitlePanel)
            Me.MinimumSize = New System.Drawing.Size(350, 150)
            Me.Name = "MessageBox"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "MessageBox"
            Me.TopMost = True
            Me.TitlePanel.ResumeLayout(False)
            Me.TitlePanel.PerformLayout()
            Me.ButtonPanel.ResumeLayout(False)
            CType(Me.BugPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
            Me.BodyPanel.ResumeLayout(False)
            Me.BodyPanel.PerformLayout()
            CType(Me.IconPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub

        Private WithEvents TitlePanel As Panel
        Private CaptionLabel As Label
        Private WithEvents CloseButton As Button
        Private ButtonPanel As Panel
        Private WithEvents ThirdButton As Button
        Private WithEvents SecondButton As Button
        Private WithEvents FirstButton As Button
        Private BodyPanel As Panel
        Private MessageLabel As Label
        Private WithEvents IconPictureBox As PictureBox
        Private WithEvents BugPictureBox As PictureBox
    End Class

End Namespace