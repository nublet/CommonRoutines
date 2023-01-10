Namespace CommonRoutines.Extensions

    Public Module System_Windows_Forms_Control

        <Runtime.CompilerServices.Extension()> Public Function GetControls(Of T)(p As Control) As IEnumerable(Of T)
            Return p.Controls.OfType(Of T)()
        End Function

        <Runtime.CompilerServices.Extension()> Public Sub AddControl(p As Control, child As Control)
            p.Controls.Add(child)
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub ClearControls(p As Control)
            Do While p.Controls.Count > 0
                p.RemoveControl(p.Controls(0))
            Loop
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub DockAndBringToFront(c As Control)
            c.SuspendDrawing()

            c.Dock = DockStyle.Fill
            c.BringToFront()
            c.Visible = True

            c.ResumeDrawing(True)
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub RemoveControl(p As Control, child As Control)
            p.Controls.Remove(child)
            child.ClearControls()
            child.Dispose()
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub ResumeDrawing(c As Control)
            If c Is Nothing Then
                Return
            End If

            c.ResumeDrawing(True)
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub ResumeDrawing(c As Control, redraw As Boolean)
            If c Is Nothing Then
                Return
            End If

            c.ResumeLayout(True)

            NativeRoutines.SendMessage(c.Handle, NativeRoutines.Enums.WindowMessages.WM_SETREDRAW, 1, IntPtr.Zero)

            If redraw Then
                c.Refresh()
            End If
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub SetTheme(ByRef c As Control)
            If c Is Nothing Then
                Return
            End If

            Dim ControlType As System.Type = c.GetType()
            Dim FullName As String = "{0}.{1}".FormatWith(ControlType.Namespace, ControlType.Name)

            If FullName.StartsWith("DevComponents.DotNetBar") Then
                Return
            End If

            If FullName.StartsWith("ServiceRequestClient.Controls") Then
                c.BackColor = UITheme.BackColor
            ElseIf FullName.StartsWith("Tools.UserControls") Then
                c.BackColor = UITheme.BackColor
            ElseIf FullName.StartsWith("Utilities.Controls") Then
                c.BackColor = UITheme.BackColor
            ElseIf FullName.StartsWith("WoWSheets.UserControls") Then
                c.BackColor = UITheme.BackColor
            Else
                Select Case FullName
                    Case "CommonRoutines.Controls.Button"
                        Dim Item As Controls.Button = DirectCast(c, Controls.Button)
                        Item.BackColor = Item.Parent.BackColor
                        Item.BorderColor = UITheme.BorderColor
                        Item.ForeColor = UITheme.ForeColor

                        If Item.FlatStyle = FlatStyle.Flat Then
                            Item.FlatAppearance.BorderColor = UITheme.BorderColor
                            Item.UseVisualStyleBackColor = False
                        End If

                    Case "CommonRoutines.Controls.CheckBox"
                        Dim Item As Controls.CheckBox = DirectCast(c, Controls.CheckBox)
                        Item.BackColor = Item.Parent.BackColor
                        Item.ForeColor = UITheme.ForeColor
                        Item.CheckedColor = UITheme.ForeColor
                        Item.UncheckedColor = UITheme.SelectionBackColor
                        If UITheme.ForceUseTicks Then
                            Item.UseTick = True
                        End If

                    Case "CommonRoutines.Controls.ComboBox"
                        Dim Item As Controls.ComboBox = DirectCast(c, Controls.ComboBox)
                        Item.BackColor = Item.Parent.BackColor
                        Item.BorderColor = UITheme.BorderColor
                        Item.ForeColor = UITheme.ForeColor
                        Item.IconColor = UITheme.SelectionBackColor
                        Item.ListBackColor = UITheme.BackColor
                        Item.ListForeColor = UITheme.ForeColor

                        Return

                    Case "CommonRoutines.Controls.DateTimePicker"
                        Dim Item As Controls.DateTimePicker = DirectCast(c, Controls.DateTimePicker)
                        Item.BackColor = Item.Parent.BackColor
                        Item.BorderColor = UITheme.BorderColor
                        Item.ForeColor = UITheme.ForeColor

                    Case "CommonRoutines.Controls.DBDataGridView"
                        Dim Item As Controls.DBDataGridView = DirectCast(c, Controls.DBDataGridView)
                        Item.BackgroundColor = UITheme.BackColor
                        Item.ColumnHeadersDefaultCellStyle.BackColor = UITheme.BackColor
                        Item.ColumnHeadersDefaultCellStyle.Font = New Font(Item.ColumnHeadersDefaultCellStyle.Font.Name, UITheme.GridFontSize + 1)
                        Item.ColumnHeadersDefaultCellStyle.ForeColor = UITheme.ForeColor
                        Item.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                        Item.ColumnHeadersDefaultCellStyle.SelectionBackColor = UITheme.BackColor
                        Item.ColumnHeadersDefaultCellStyle.SelectionForeColor = UITheme.ForeColor
                        Item.DefaultCellStyle.BackColor = UITheme.BackColor
                        Item.DefaultCellStyle.Font = New Font(Item.ColumnHeadersDefaultCellStyle.Font.Name, UITheme.GridFontSize)
                        Item.DefaultCellStyle.ForeColor = UITheme.ForeColor
                        Item.DefaultCellStyle.Padding = New Padding(3, 0, 3, 0)
                        Item.DefaultCellStyle.SelectionBackColor = UITheme.SelectionBackColor
                        Item.DefaultCellStyle.SelectionForeColor = UITheme.SelectionForeColor
                        Item.GridColor = UITheme.LineColor
                        Item.RowTemplate.Height = TextRenderer.MeasureText("Sample", Item.DefaultCellStyle.Font).Height + 6

                    Case "CommonRoutines.Controls.DBTableLayoutPanel"
                        Dim Item As Controls.DBTableLayoutPanel = DirectCast(c, Controls.DBTableLayoutPanel)
                        Item.BackColor = UITheme.BackColor

                    Case "CommonRoutines.Controls.DropdownMenu"
                        Dim Item As Controls.DropdownMenu = DirectCast(c, Controls.DropdownMenu)
                        Item.PrimaryColor = UITheme.BackColor
                        Item.MenuItemTextColor = UITheme.ForeColor

                        Return

                    Case "CommonRoutines.Controls.ListResults"
                        Dim Item As Controls.ListResults = DirectCast(c, Controls.ListResults)
                        Item.BackColor = Item.Parent.BackColor
                        Item.BorderColor = UITheme.BorderColor
                        Item.BorderFocusColor = UITheme.SelectionBackColor
                        Item.ForeColor = UITheme.ForeColor

                        Return

                    Case "CommonRoutines.Controls.PictureBox"
                        Dim Item As Controls.PictureBox = DirectCast(c, Controls.PictureBox)
                        Item.BackColor = Item.Parent.BackColor
                        Item.ForeColor = UITheme.ForeColor
                        Item.BorderColor = UITheme.BorderColor
                        Item.BorderColor2 = Color.FromArgb(UITheme.BorderColor.ToArgb() Xor &HFFFFFF)

                    Case "CommonRoutines.Controls.ProgressBar"
                        Dim Item As Controls.ProgressBar = DirectCast(c, Controls.ProgressBar)
                        Item.BackColor = Item.Parent.BackColor
                        Item.BorderColor = UITheme.BorderColor
                        Item.ChannelColor = Item.Parent.BackColor
                        Item.ForeBackColor = Item.Parent.BackColor
                        Item.ForeColor = UITheme.ForeColor
                        Item.SliderColor = UITheme.LineColor

                    Case "CommonRoutines.Controls.ProgressBarBase"
                        Dim Item As Controls.ProgressBarBase = DirectCast(c, Controls.ProgressBarBase)
                        Item.BackColor = Item.Parent.BackColor
                        Item.BorderColor = UITheme.BorderColor
                        Item.ChannelColor = Item.Parent.BackColor
                        Item.ForeBackColor = Item.Parent.BackColor
                        Item.ForeColor = UITheme.ForeColor
                        Item.SliderColor = UITheme.LineColor

                    Case "CommonRoutines.Controls.RadioButton"
                        Dim Item As Controls.RadioButton = DirectCast(c, Controls.RadioButton)
                        Item.BackColor = Item.Parent.BackColor
                        Item.ForeColor = UITheme.ForeColor
                        Item.CheckedColor = UITheme.ForeColor
                        Item.UncheckedColor = UITheme.SelectionBackColor

                    Case "CommonRoutines.Controls.TextBox"
                        Dim Item As Controls.TextBox = DirectCast(c, Controls.TextBox)
                        Item.BackColor = Item.Parent.BackColor
                        Item.BorderColor = UITheme.BorderColor
                        Item.BorderFocusColor = UITheme.SelectionBackColor
                        Item.PlaceHolderColor = UITheme.LineColor
                        Item.ForeColor = UITheme.ForeColor

                        Return

                    Case "CommonRoutines.Controls.ToggleButton"
                        Dim Item As Controls.ToggleButton = DirectCast(c, Controls.ToggleButton)
                        Item.CheckedBackColor = UITheme.BorderColor
                        Item.CheckedToggleColor = UITheme.BorderColor
                        Item.UncheckedBackColor = UITheme.SelectionBackColor
                        Item.UncheckedToggleColor = UITheme.SelectionBackColor

                    Case "CommonRoutines.UITheme.Editor"
                        c.BackColor = UITheme.BackColor

                    Case "System.Windows.Forms.Button"
                        Dim Item As Button = DirectCast(c, Button)
                        Item.BackColor = Item.Parent.BackColor
                        Item.ForeColor = UITheme.ForeColor
                        If Item.FlatStyle = FlatStyle.Flat Then
                            Item.FlatAppearance.BorderColor = UITheme.BorderColor
                            Item.UseVisualStyleBackColor = False
                        End If

                    Case "System.Windows.Forms.CheckBox"
                        Dim Item As CheckBox = DirectCast(c, CheckBox)
                        Item.ForeColor = UITheme.ForeColor
                        Item.UseVisualStyleBackColor = True

                    Case "System.Windows.Forms.ComboBox"
                        Dim Item As ComboBox = DirectCast(c, ComboBox)
                        Item.BackColor = Item.Parent.BackColor
                        Item.ForeColor = UITheme.ForeColor

                    Case "System.Windows.Forms.DataGridView"
                        Dim Item As DataGridView = DirectCast(c, DataGridView)
                        Item.BackgroundColor = UITheme.BackColor
                        Item.ColumnHeadersDefaultCellStyle.BackColor = UITheme.BackColor
                        Item.ColumnHeadersDefaultCellStyle.Font = New Font(Item.ColumnHeadersDefaultCellStyle.Font.Name, UITheme.GridFontSize + 1)
                        Item.ColumnHeadersDefaultCellStyle.ForeColor = UITheme.ForeColor
                        Item.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                        Item.ColumnHeadersDefaultCellStyle.SelectionBackColor = UITheme.BackColor
                        Item.ColumnHeadersDefaultCellStyle.SelectionForeColor = UITheme.ForeColor
                        Item.DefaultCellStyle.BackColor = UITheme.BackColor
                        Item.DefaultCellStyle.Font = New Font(Item.ColumnHeadersDefaultCellStyle.Font.Name, UITheme.GridFontSize)
                        Item.DefaultCellStyle.ForeColor = UITheme.ForeColor
                        Item.DefaultCellStyle.Padding = New Padding(3, 0, 3, 0)
                        Item.DefaultCellStyle.SelectionBackColor = UITheme.SelectionBackColor
                        Item.DefaultCellStyle.SelectionForeColor = UITheme.SelectionForeColor
                        Item.GridColor = UITheme.LineColor
                        Item.RowTemplate.Height = TextRenderer.MeasureText("Sample", Item.DefaultCellStyle.Font).Height + 6

                    Case "System.Windows.Forms.DataVisualization.Charting.Chart"
                        Dim Item As DataVisualization.Charting.Chart = DirectCast(c, DataVisualization.Charting.Chart)

                        Item.BackColor = UITheme.BackColor

                        For Each Current As DataVisualization.Charting.ChartArea In Item.ChartAreas
                            Current.AxisX.SetTheme()
                            Current.AxisX2.SetTheme()
                            Current.AxisY.SetTheme()
                            Current.AxisY2.SetTheme()

                            Current.BackColor = UITheme.BackColor
                        Next

                        For Each Current As DataVisualization.Charting.Legend In Item.Legends
                            Current.BackColor = Color.Transparent
                            Current.ForeColor = UITheme.ForeColor
                        Next

                        For Each Current As DataVisualization.Charting.Series In Item.Series
                            Current.Color = UITheme.LineColor
                        Next

                    Case "System.Windows.Forms.Label"
                        Dim Item As Label = DirectCast(c, Label)
                        Item.BackColor = Item.Parent.BackColor
                        Item.ForeColor = UITheme.ForeColor

                    Case "System.Windows.Forms.Panel"
                        Dim Item As Panel = DirectCast(c, Panel)
                        Item.BackColor = UITheme.BackColor

                    Case "System.Windows.Forms.PictureBox"
                        Dim Item As PictureBox = DirectCast(c, PictureBox)
                        Item.BackColor = Item.Parent.BackColor

                    Case "System.Windows.Forms.TableLayoutPanel"
                        Dim Item As TableLayoutPanel = DirectCast(c, TableLayoutPanel)
                        Item.BackColor = UITheme.BackColor

                    Case "System.Windows.Forms.TextBox"
                        Dim Item As TextBox = DirectCast(c, TextBox)
                        Item.BackColor = Item.Parent.BackColor
                        Item.ForeColor = UITheme.ForeColor

                    Case "System.Windows.Forms.TreeView"
                        Dim Item As TreeView = DirectCast(c, TreeView)
                        Item.BackColor = UITheme.BackColor
                        Item.ForeColor = UITheme.ForeColor

                    Case "System.Windows.Forms.DateTimePicker"
                    Case "System.Windows.Forms.HScrollBar"
                    Case "System.Windows.Forms.VScrollBar"

                    Case Else
                        UITheme.NewControls.Add("case ""{0}""".FormatWith(FullName))
                End Select
            End If

            If Not c.HasChildren Then
                Return
            End If

            If c.Controls Is Nothing OrElse c.Controls.Count <= 0 Then
                Return
            End If

            For Each Current As Control In c.Controls
                Current.SetTheme()
            Next
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub SuspendDrawing(c As Control)
            If c Is Nothing Then
                Return
            End If

            c.SuspendLayout()

            NativeRoutines.SendMessage(c.Handle, NativeRoutines.Enums.WindowMessages.WM_SETREDRAW, 0, IntPtr.Zero)
        End Sub

    End Module

End Namespace