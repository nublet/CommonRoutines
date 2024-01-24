Namespace UserForms

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class BorderlessForm
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
            Me.ClosePictureBox = New System.Windows.Forms.PictureBox()
            Me.MaximizePictureBox = New System.Windows.Forms.PictureBox()
            Me.MinimizePictureBox = New System.Windows.Forms.PictureBox()
            Me.TitlePanel.SuspendLayout()
            CType(Me.ClosePictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.MaximizePictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.MinimizePictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'TitlePanel
            '
            Me.TitlePanel.BackColor = System.Drawing.SystemColors.ActiveCaption
            Me.TitlePanel.Controls.Add(Me.MinimizePictureBox)
            Me.TitlePanel.Controls.Add(Me.MaximizePictureBox)
            Me.TitlePanel.Controls.Add(Me.ClosePictureBox)
            Me.TitlePanel.Dock = System.Windows.Forms.DockStyle.Top
            Me.TitlePanel.Location = New System.Drawing.Point(0, 0)
            Me.TitlePanel.Name = "TitlePanel"
            Me.TitlePanel.Size = New System.Drawing.Size(800, 30)
            Me.TitlePanel.TabIndex = 1
            '
            'ClosePictureBox
            '
            Me.ClosePictureBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ClosePictureBox.Cursor = System.Windows.Forms.Cursors.Hand
            Me.ClosePictureBox.Location = New System.Drawing.Point(770, 0)
            Me.ClosePictureBox.Margin = New System.Windows.Forms.Padding(0)
            Me.ClosePictureBox.Name = "ClosePictureBox"
            Me.ClosePictureBox.Size = New System.Drawing.Size(30, 30)
            Me.ClosePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
            Me.ClosePictureBox.TabIndex = 2
            Me.ClosePictureBox.TabStop = False
            '
            'MaximizePictureBox
            '
            Me.MaximizePictureBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.MaximizePictureBox.Cursor = System.Windows.Forms.Cursors.Hand
            Me.MaximizePictureBox.Location = New System.Drawing.Point(740, 0)
            Me.MaximizePictureBox.Margin = New System.Windows.Forms.Padding(0)
            Me.MaximizePictureBox.Name = "MaximizePictureBox"
            Me.MaximizePictureBox.Size = New System.Drawing.Size(30, 30)
            Me.MaximizePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
            Me.MaximizePictureBox.TabIndex = 3
            Me.MaximizePictureBox.TabStop = False
            '
            'MinimizePictureBox
            '
            Me.MinimizePictureBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.MinimizePictureBox.Cursor = System.Windows.Forms.Cursors.Hand
            Me.MinimizePictureBox.Location = New System.Drawing.Point(710, 0)
            Me.MinimizePictureBox.Margin = New System.Windows.Forms.Padding(0)
            Me.MinimizePictureBox.Name = "MinimizePictureBox"
            Me.MinimizePictureBox.Size = New System.Drawing.Size(30, 30)
            Me.MinimizePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
            Me.MinimizePictureBox.TabIndex = 4
            Me.MinimizePictureBox.TabStop = False
            '
            'BorderlessForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(800, 461)
            Me.Controls.Add(Me.TitlePanel)
            Me.Name = "BorderlessForm"
            Me.Text = "Form1"
            Me.TitlePanel.ResumeLayout(False)
            CType(Me.ClosePictureBox, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.MaximizePictureBox, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.MinimizePictureBox, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents TitlePanel As Panel
        Private WithEvents ClosePictureBox As PictureBox
        Private WithEvents MinimizePictureBox As PictureBox
        Private WithEvents MaximizePictureBox As PictureBox
    End Class

End Namespace