Imports System.Runtime.InteropServices

Namespace NativeRoutines.Structures

    <StructLayout(LayoutKind.Sequential)> Public Structure COPYDATASTRUCT

        Public dwData As IntPtr
        Public cbData As Integer
        Public lpData As IntPtr

    End Structure

End Namespace