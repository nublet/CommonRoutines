Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Namespace CommonRoutines.Controls

    Public Class DropdownMenu
        Inherits ContextMenuStrip

        Private _IsMainMenu As Boolean
        Private _MenuItemHeaderSize As Bitmap
        Private _MenuItemHeight As Integer = 25
        Private _MenuItemTextColor As Color = Color.DimGray
        Private _PrimaryColor As Color = Color.MediumSlateBlue

        <Browsable(False)> <Category("Common Routines")> Public Property IsMainMenu As Boolean
            Get
                Return _IsMainMenu
            End Get
            Set(value As Boolean)
                _IsMainMenu = value
            End Set
        End Property

        <Browsable(False)> <Category("Common Routines")> Public Property MenuItemHeight As Integer
            Get
                Return _MenuItemHeight
            End Get
            Set(value As Integer)
                _MenuItemHeight = value
            End Set
        End Property

        <Browsable(False)> <Category("Common Routines")> Public Property MenuItemTextColor As Color
            Get
                Return _MenuItemTextColor
            End Get
            Set(value As Color)
                _MenuItemTextColor = value
            End Set
        End Property

        <Browsable(False)> <Category("Common Routines")> Public Property PrimaryColor As Color
            Get
                Return _PrimaryColor
            End Get
            Set(value As Color)
                _PrimaryColor = value
            End Set
        End Property

        Public Sub New(container As IContainer)
            MyBase.New(container)
        End Sub

        Private Sub LoadMenuItemAppearance()
            If _IsMainMenu Then
                _MenuItemHeaderSize = New Bitmap(25, 45)
            Else
                _MenuItemHeaderSize = New Bitmap(15, _MenuItemHeight)
            End If

            For Each Current As ToolStripMenuItem In Items
                LoadToolStripMenuItemAppearance(Current)
            Next
        End Sub

        Private Sub LoadToolStripMenuItemAppearance(ByRef toolStripMenuItem As ToolStripMenuItem)
            toolStripMenuItem.ForeColor = _MenuItemTextColor
            toolStripMenuItem.ImageScaling = ToolStripItemImageScaling.None

            If toolStripMenuItem.Image Is Nothing Then
                toolStripMenuItem.Image = _MenuItemHeaderSize
            End If

            If Not toolStripMenuItem.HasDropDownItems Then
                Return
            End If

            If toolStripMenuItem.DropDownItems.Count <= 0 Then
                Return
            End If

            For Each Current As ToolStripMenuItem In toolStripMenuItem.DropDownItems
                LoadToolStripMenuItemAppearance(Current)
            Next
        End Sub

#Region " Overrides "

        Protected Overrides Sub OnHandleCreated(e As EventArgs)
            MyBase.OnHandleCreated(e)

            If Not DesignMode Then
                Renderer = New MenuRenderer(_IsMainMenu, _PrimaryColor, _MenuItemTextColor)

                LoadMenuItemAppearance()
            End If
        End Sub

#End Region

    End Class

End Namespace