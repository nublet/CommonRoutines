Imports System.Runtime.InteropServices
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Threading

Public Class SingleInstanceApplication

    Public Delegate Sub NewInstanceMessageHandler(sender As Object, message As Object)

    Private ReadOnly _FirstInstance As Boolean
    Private ReadOnly _InstanceCounter As Mutex
    Private ReadOnly _WindowCaption As String

    Private _IsDisposed As Boolean = False
    Private _NewMessage As NewInstanceMessageHandler
    Private _NotificationWindow As SIANativeWindow

    Public ReadOnly Property Exists() As Boolean
        Get
            Return Not _FirstInstance
        End Get
    End Property

    Private Sub New(windowCaption As String)
        _WindowCaption = windowCaption
        _InstanceCounter = New Mutex(False, _WindowCaption, _FirstInstance)
    End Sub

    Private Function NotifyPreviousInstance(message As Object) As Boolean
        Dim WindowHandle As IntPtr = NativeRoutines.FindWindow(Nothing, _WindowCaption)

        If WindowHandle <> IntPtr.Zero Then
            Dim BufferHandle As New GCHandle()

            Try
                Dim Buffer As Byte()
                Dim Data As New NativeRoutines.Structures.COPYDATASTRUCT()
                If message IsNot Nothing Then
                    Buffer = Serialize(message)
                    BufferHandle = GCHandle.Alloc(Buffer, GCHandleType.Pinned)

                    Data.dwData = IntPtr.Zero
                    Data.cbData = Buffer.Length
                    Data.lpData = BufferHandle.AddrOfPinnedObject()
                End If

                Dim DataHandle As GCHandle = GCHandle.Alloc(Data, GCHandleType.Pinned)

                Try
                    NativeRoutines.SendMessage(WindowHandle, NativeRoutines.Enums.WindowMessages.WM_COPYDATA, 0, DataHandle.AddrOfPinnedObject())
                    Return True
                Finally
                    DataHandle.Free()
                End Try
            Finally
                If BufferHandle.IsAllocated Then
                    BufferHandle.Free()
                End If
            End Try
        End If

        Return False
    End Function

    Private Sub Dispose()
        If _IsDisposed Then
            Return
        End If

        _IsDisposed = True

        _InstanceCounter.Close()

        _NotificationWindow?.DestroyHandle()
    End Sub

    Private Sub Init(ByRef newMessage As NewInstanceMessageHandler)
        _NewMessage = newMessage
        _NotificationWindow = New SIANativeWindow(_WindowCaption, Me)
    End Sub

#Region " SIANativeWindow "

    Private Class SIANativeWindow
        Inherits NativeWindow

        Private ReadOnly _Parent As SingleInstanceApplication

        Public Sub New(windowCaption As String, ByRef parent As SingleInstanceApplication)
            _Parent = parent

            Dim cp As New CreateParams With {
            .Caption = windowCaption,
            .Height = 0,
            .Width = 0,
            .X = 0,
            .Y = 0
        }

            CreateHandle(cp)
        End Sub

        Protected Overrides Sub WndProc(ByRef m As Message)
            If m.Msg = NativeRoutines.Enums.WindowMessages.WM_COPYDATA Then
                Dim Data As NativeRoutines.Structures.COPYDATASTRUCT = DirectCast(Marshal.PtrToStructure(m.LParam, GetType(NativeRoutines.Structures.COPYDATASTRUCT)), NativeRoutines.Structures.COPYDATASTRUCT)
                Dim Result As Object = Nothing

                If Data.cbData > 0 AndAlso Data.lpData <> IntPtr.Zero Then
                    Dim Buffer As Byte() = New Byte(Data.cbData - 1) {}
                    Marshal.Copy(Data.lpData, Buffer, 0, Buffer.Length)

                    Result = Deserialize(Buffer)
                End If

                _Parent._NewMessage(_Parent, Result)
            Else
                MyBase.WndProc(m)
            End If
        End Sub

    End Class

#End Region

#Region " Shared "

    Private Shared ReadOnly _DeserializeType As System.Type = GetType(Object)
    Private Shared ReadOnly _Instances As New Dictionary(Of String, SingleInstanceApplication)
    Private Shared ReadOnly _SerializerOptions As New Text.Json.JsonSerializerOptions() With {.WriteIndented = True}

    Public Shared ReadOnly Property AlreadyExists(windowCaption As String) As Boolean
        Get
            Dim Existing As SingleInstanceApplication = Nothing

            If Not _Instances.TryGetValue(windowCaption, Existing) Then
                Existing = New SingleInstanceApplication(windowCaption)
                _Instances.Add(windowCaption, Existing)
            End If

            Return Existing.Exists
        End Get
    End Property

    Private Shared Function Deserialize(buffer As Byte()) As Object
        Return Text.Json.JsonSerializer.Deserialize(buffer, _DeserializeType, _SerializerOptions)
    End Function

    Private Shared Function Serialize(obj As Object) As Byte()
        Return Text.Json.JsonSerializer.SerializeToUtf8Bytes(obj, _SerializerOptions)
    End Function

    Public Shared Function NotifyExistingInstance(message As Object, windowCaption As String) As Boolean
        Dim Existing As SingleInstanceApplication = Nothing

        If Not _Instances.TryGetValue(windowCaption, Existing) Then
            Existing = New SingleInstanceApplication(windowCaption)
            _Instances.Add(windowCaption, Existing)
        End If

        If Existing.Exists Then
            Return Existing.NotifyPreviousInstance(message)
        End If

        Return False
    End Function

    Public Shared Sub Close()
        Try
            For Each Current As SingleInstanceApplication In _Instances.Values
                Current.Dispose()
            Next
        Catch ex As Exception
            ex.ToLog(True)
        Finally
            _Instances.Clear()
        End Try
    End Sub

    Public Shared Sub Initialize(windowCaption As String, ByRef newMessage As NewInstanceMessageHandler)
        Dim Existing As SingleInstanceApplication = Nothing

        If Not _Instances.TryGetValue(windowCaption, Existing) Then
            Existing = New SingleInstanceApplication(windowCaption)
            _Instances.Add(windowCaption, Existing)
        End If

        Existing.Init(newMessage)
    End Sub

#End Region

End Class