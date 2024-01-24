Imports System.Drawing

Namespace UserControls

    Public MustInherit Class TabBase
        Implements ITab

        Protected Friend _Button As Button = Nothing
        Protected Friend _Name As String = ""
        Protected Friend _OrderButton As Integer = -1
        Protected Friend _Text As String = ""

        Public MustOverride Function GetUserControl() As UserControl

        Public MustOverride Sub LoadData()

        Protected Friend Sub CreateItems()
            _Name = "Tabs_{0}".FormatWith(_Text)
            _Name = _Name.Replace(" ", "")
            _Name = _Name.Replace("-", "_")

            _Button = New Button() With {
                .Anchor = (AnchorStyles.Top Or AnchorStyles.Left),
                .BorderRadius = 0,
                .BorderSize = 1,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Microsoft Sans Serif", 11.0!),
                .ForeColor = Color.FromArgb(124, 141, 181),
                .Location = New Point(13, 518),
                .Margin = New Padding(0),
                .Name = Me.Name.Replace("DA_", "bi"),
                .Padding = New Padding(0),
                .Size = New Size(148, 30),
                .TabIndex = 14,
                .Text = Me.Text,
                .UseVisualStyleBackColor = True
            }

            _Button.FlatAppearance.BorderColor = Color.FromArgb(107, 83, 255)
        End Sub

#Region " ITab "

        Private _IsSelected As Boolean = False

        Public Property IsSelected As Boolean Implements ITab.IsSelected
            Get
                Return _IsSelected
            End Get
            Set(value As Boolean)
                _IsSelected = value

                If _IsSelected Then
                    Button.BackColor = UITheme.SelectionBackColor
                    Button.ForeColor = UITheme.SelectionForeColor
                Else
                    Button.BackColor = UITheme.FormBackColor
                    Button.ForeColor = UITheme.ForeColor
                End If
            End Set
        End Property

        Public ReadOnly Property Button As Button Implements ITab.Button
            Get
                Return _Button
            End Get
        End Property

        Public ReadOnly Property Name As String Implements ITab.Name
            Get
                Return _Name
            End Get
        End Property

        Public ReadOnly Property OrderButton As Integer Implements ITab.OrderButton
            Get
                Return _OrderButton
            End Get
        End Property

        Public ReadOnly Property Text As String Implements ITab.Text
            Get
                Return _Text
            End Get
        End Property

        Public ReadOnly Property UserControl As UserControl Implements ITab.UserControl
            Get
                Return GetUserControl()
            End Get
        End Property

        Public Sub LoadArea() Implements ITab.LoadArea
            GetUserControl().DockAndBringToFront()

            LoadData()
        End Sub

#End Region

    End Class

End Namespace