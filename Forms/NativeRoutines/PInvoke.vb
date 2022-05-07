Imports System.Runtime.InteropServices

Namespace CommonRoutines.NativeRoutines

    Public Module PInvoke

#Region " User32 "

        <DllImport("user32.dll", CharSet:=CharSet.Auto, EntryPoint:="FindWindow")> Public Function FindWindow(lpClassName As String, lpWindowName As String) As IntPtr
        End Function

        <DllImport("user32.dll", CharSet:=CharSet.Auto, EntryPoint:="GetLastInputInfo")> Public Function GetLastInputInfo(ByRef liInfo As Structures.LASTINPUTINFO) As Boolean
        End Function

        <DllImport("user32.dll", CharSet:=CharSet.Auto, EntryPoint:="ReleaseCapture")> Public Sub ReleaseCapture()
        End Sub

        <DllImport("user32.dll", CharSet:=CharSet.Auto, EntryPoint:="SendMessage")> Public Sub SendMessage(hWnd As IntPtr, wMsg As Integer, wParam As Integer, lParam As IntPtr)
        End Sub

#End Region

#Region " UxTheme "

        <DllImport("uxtheme.dll", CharSet:=CharSet.Auto, EntryPoint:="#95")> Public Function GetImmersiveColorFromColorSetEx(dwImmersiveColorSet As UInteger, dwImmersiveColorType As UInteger, bIgnoreHighContrast As Boolean, dwHighContrastCacheMode As UInteger) As UInteger
        End Function

        <DllImport("uxtheme.dll", CharSet:=CharSet.Auto, EntryPoint:="#96")> Public Function GetImmersiveColorTypeFromName(pName As IntPtr) As UInteger
        End Function

        <DllImport("uxtheme.dll", CharSet:=CharSet.Auto, EntryPoint:="#98")> Public Function GetImmersiveUserColorSetPreference(bForceCheckRegistry As Boolean, bSkipCheckOnFail As Boolean) As Integer
        End Function

#End Region

    End Module

End Namespace