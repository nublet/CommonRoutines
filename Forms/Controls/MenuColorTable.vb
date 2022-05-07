Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Namespace CommonRoutines.Controls

    Public Class MenuColorTable
        Inherits ProfessionalColorTable

        Public Sub New()

        End Sub

#Region " Overrides "

        Public Overrides ReadOnly Property ToolStripDropDownBackground As Color
            Get
                Return UITheme.BackColor
            End Get
        End Property

        Public Overrides ReadOnly Property MenuBorder As Color
            Get
                Return UITheme.BorderColor
            End Get
        End Property

        Public Overrides ReadOnly Property MenuItemBorder As Color
            Get
                Return UITheme.BorderColor
            End Get
        End Property

        Public Overrides ReadOnly Property MenuItemSelected As Color
            Get
                Return UITheme.SelectionBackColor
            End Get
        End Property

        Public Overrides ReadOnly Property ImageMarginGradientBegin As Color
            Get
                Return UITheme.FormBackColor
            End Get
        End Property

        Public Overrides ReadOnly Property ImageMarginGradientMiddle As Color
            Get
                Return UITheme.FormBackColor
            End Get
        End Property

        Public Overrides ReadOnly Property ImageMarginGradientEnd As Color
            Get
                Return UITheme.FormBackColor
            End Get
        End Property

#End Region

    End Class

End Namespace