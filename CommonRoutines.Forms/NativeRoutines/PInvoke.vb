Imports System.Runtime.InteropServices

Namespace NativeRoutines

    Public Module PInvoke

#Region " User32 "

        <DllImport("user32.dll", CharSet:=CharSet.Unicode, EntryPoint:="FindWindow")> Public Function FindWindow(lpClassName As String, lpWindowName As String) As IntPtr
        End Function

        <DllImport("user32.dll", CharSet:=CharSet.Unicode, EntryPoint:="GetLastInputInfo")> Public Function GetLastInputInfo(ByRef liInfo As Structures.LASTINPUTINFO) As Boolean
        End Function

        <DllImport("user32.dll", CharSet:=CharSet.Unicode, EntryPoint:="GetWindowLong")> Public Function GetWindowLong(hWnd As IntPtr, nIndex As Integer) As Integer
        End Function

        <DllImport("user32.dll", CharSet:=CharSet.Unicode, EntryPoint:="ReleaseCapture")> Public Sub ReleaseCapture()
        End Sub

        <DllImport("user32.dll", CharSet:=CharSet.Unicode, EntryPoint:="SetLayeredWindowAttributes")> Public Function SetLayeredWindowAttributes(hWnd As IntPtr, crKey As Integer, bAlpha As Byte, dwFlags As Integer) As Boolean
        End Function

        <DllImport("user32.dll", CharSet:=CharSet.Unicode, EntryPoint:="SetWindowLong")> Public Function SetWindowLong(hWnd As IntPtr, nIndex As Integer, dwNewLong As Integer) As Integer
        End Function

        <DllImport("user32.dll", CharSet:=CharSet.Unicode, EntryPoint:="SendMessage")> Public Sub SendMessage(hWnd As IntPtr, wMsg As Integer, wParam As Integer, lParam As IntPtr)
        End Sub

#End Region

#Region " UxTheme "

        <DllImport("uxtheme.dll", CharSet:=CharSet.Unicode, EntryPoint:="#95")> Public Function GetImmersiveColorFromColorSetEx(dwImmersiveColorSet As UInteger, dwImmersiveColorType As UInteger, bIgnoreHighContrast As Boolean, dwHighContrastCacheMode As UInteger) As UInteger
        End Function

        <DllImport("uxtheme.dll", CharSet:=CharSet.Unicode, EntryPoint:="#96")> Public Function GetImmersiveColorTypeFromName(pName As IntPtr) As UInteger
        End Function

        <DllImport("uxtheme.dll", CharSet:=CharSet.Unicode, EntryPoint:="#98")> Public Function GetImmersiveUserColorSetPreference(bForceCheckRegistry As Boolean, bSkipCheckOnFail As Boolean) As Integer
        End Function

#End Region

    End Module

End Namespace