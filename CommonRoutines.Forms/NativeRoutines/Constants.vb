Namespace NativeRoutines

    Public Module Constants

        Public Const WM_NCCALCSIZE As Integer = &H83
        Public Const WM_SYSCOMMAND As Integer = &H112
        Public Const SC_MINIMIZE As Integer = &HF020
        Public Const SC_RESTORE As Integer = &HF120
        Public Const WM_NCHITTEST As Integer = &H84

        Public Const HTCLIENT As Integer = 1
        Public Const HTLEFT As Integer = 10
        Public Const HTRIGHT As Integer = 11
        Public Const HTTOP As Integer = 12
        Public Const HTTOPLEFT As Integer = 13
        Public Const HTTOPRIGHT As Integer = 14
        Public Const HTBOTTOM As Integer = 15
        Public Const HTBOTTOMLEFT As Integer = 16
        Public Const HTBOTTOMRIGHT As Integer = 17

        Public Const WS_EX_LAYERED As Integer = &H80000
        Public Const WS_EX_TRANSPARENT As Integer = &H20
        Public Const LWA_COLORKEY As Integer = &H1
        Public Const LWA_ALPHA As Integer = &H2

    End Module

End Namespace