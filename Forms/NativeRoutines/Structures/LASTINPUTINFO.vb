﻿Imports System.Runtime.InteropServices

Namespace CommonRoutines.NativeRoutines.Structures

    <StructLayout(LayoutKind.Sequential)> Public Structure LASTINPUTINFO

        Public cbSize As UInteger
        Public dwTime As UInteger

    End Structure

End Namespace