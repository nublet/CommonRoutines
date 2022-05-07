Imports System.Runtime.InteropServices

Namespace CommonRoutines.UITheme

    Public Module _Shared

        Public Event ThemeValueChanged()

        Private ReadOnly _NewControls As New List(Of String)

        Public Property BackColor As Color = Color.FromArgb(42, 45, 86)
        Public Property BorderColor As Color = Color.FromArgb(107, 83, 255)
        Public Property ForeColor As Color = Color.FromArgb(124, 141, 181)
        Public Property FormBackColor As Color = Color.FromArgb(24, 28, 63)
        Public Property GridFontSize As Single = 11
        Public Property LineColor As Color = Color.FromArgb(73, 75, 111)
        Public Property SelectionBackColor As Color = Color.FromArgb(241, 122, 133)
        Public Property SelectionForeColor As Color = Color.White

        Public ReadOnly Property NewControls As List(Of String)
            Get
                Return _NewControls
            End Get
        End Property

        Private Function GetColorFromDWord(colorSetEx As UInteger) As Color
            Dim AlphaByte As Byte = CByte((&HFF000000UI And colorSetEx) >> 24)
            Dim BlueByte As Byte = CByte((&HFF0000 And colorSetEx) >> 16)
            Dim GreenByte As Byte = CByte((&HFF00 And colorSetEx) >> 8)
            Dim RedByte As Byte = CByte((&HFF And colorSetEx) >> 0)

            Return Color.FromArgb(AlphaByte, RedByte, GreenByte, BlueByte)
        End Function

        Public Function GetImmersiveColor(immersiveColor As Enums.ImmersiveColor, userColorSet As Integer) As Color
            Dim ImmersiveColorName As String = immersiveColor.ToString()
            If ImmersiveColorName.IsNotSet() Then
                Return Color.White
            End If

            Dim ColorType = NativeRoutines.GetImmersiveColorTypeFromName(Marshal.StringToHGlobalUni(ImmersiveColorName))
            Dim ColorSetEx = NativeRoutines.GetImmersiveColorFromColorSetEx(CUInt(userColorSet), ColorType, False, 0)

            Return GetColorFromDWord(ColorSetEx)
        End Function

        Public Sub LoadFromSettings()
            Try
                GridFontSize = Settings.Get(Of Single)("Theme.GridFontSize")
                If GridFontSize <= 0 Then
                    GridFontSize = 11
                End If

                Dim SavedColor As String = Settings.Get(Of String)("Theme.BackColor")
                If SavedColor.IsNotSet() Then
                    Return
                End If

                BackColor = Color.FromArgb(Type.ToIntegerDB(SavedColor))

                SavedColor = Settings.Get(Of String)("Theme.BorderColor")
                If SavedColor.IsSet() Then
                    BorderColor = Color.FromArgb(Type.ToIntegerDB(SavedColor))
                End If

                SavedColor = Settings.Get(Of String)("Theme.ForeColor")
                If SavedColor.IsSet() Then
                    ForeColor = Color.FromArgb(Type.ToIntegerDB(SavedColor))
                End If

                SavedColor = Settings.Get(Of String)("Theme.FormBackColor")
                If SavedColor.IsSet() Then
                    FormBackColor = Color.FromArgb(Type.ToIntegerDB(SavedColor))
                End If

                SavedColor = Settings.Get(Of String)("Theme.LineColor")
                If SavedColor.IsSet() Then
                    LineColor = Color.FromArgb(Type.ToIntegerDB(SavedColor))
                End If

                SavedColor = Settings.Get(Of String)("Theme.SelectionBackColor")
                If SavedColor.IsSet() Then
                    SelectionBackColor = Color.FromArgb(Type.ToIntegerDB(SavedColor))
                End If

                SavedColor = Settings.Get(Of String)("Theme.SelectionForeColor")
                If SavedColor.IsSet() Then
                    SelectionForeColor = Color.FromArgb(Type.ToIntegerDB(SavedColor))
                End If
            Catch ex As Exception
                ex.ToLog()
            End Try
        End Sub

    End Module

End Namespace