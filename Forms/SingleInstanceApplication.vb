Imports System.Runtime.InteropServices
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Threading

Namespace CommonRoutines

    Public Class SingleInstanceApplication

        Public Const ParameterSplit As String = "<<::>>"

        Private ReadOnly _ID As String
        Private ReadOnly _InstanceCounter As Mutex
        Private ReadOnly _FirstInstance As Boolean

        Private _NotifcationWindow As SIANativeWindow

        Public ReadOnly Property Exists() As Boolean
            Get
                Return Not _FirstInstance
            End Get
        End Property

        Private Sub New()
            _ID = "SIA_" + GetAppId()
            _InstanceCounter = New Mutex(False, _ID, _FirstInstance)
        End Sub

        Private Function NotifyPreviousInstance(message As Object) As Boolean
            'First, find the window of the previous instance
            Dim handle As IntPtr = NativeRoutines.FindWindow(Nothing, _ID)
            If handle <> IntPtr.Zero Then
                'create a GCHandle to hold the serialized object. 
                Dim bufferHandle As New GCHandle()
                Try
                    Dim buffer As Byte()
                    Dim data As New NativeRoutines.Structures.COPYDATASTRUCT()
                    If message IsNot Nothing Then
                        'serialize the object into a byte array
                        buffer = Serialize(message)
                        'pin the byte array in memory
                        bufferHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned)

                        data.dwData = IntPtr.Zero
                        data.cbData = buffer.Length
                        'get the address of the pinned buffer
                        data.lpData = bufferHandle.AddrOfPinnedObject()
                    End If

                    Dim dataHandle As GCHandle = GCHandle.Alloc(data, GCHandleType.Pinned)
                    Try
                        NativeRoutines.SendMessage(handle, NativeRoutines.Enums.WindowMessages.WM_COPYDATA, 0, dataHandle.AddrOfPinnedObject())
                        Return True
                    Finally
                        dataHandle.Free()
                    End Try
                Finally
                    If bufferHandle.IsAllocated Then
                        bufferHandle.Free()
                    End If
                End Try
            End If
            Return False
        End Function

        Private Sub Dispose()
            _InstanceCounter.Close()
            If _NotifcationWindow IsNot Nothing Then
                _NotifcationWindow.DestroyHandle()
            End If
        End Sub

        Private Sub Init()
            _NotifcationWindow = New SIANativeWindow()
        End Sub

        Private Sub OnNewInstanceMessage(message As Object)
            RaiseEvent NewInstanceMessage(Me, message)
        End Sub

#Region " SIANativeWindow "

        'a utility window to communicate between application instances
        Private Class SIANativeWindow
            Inherits NativeWindow

            Public Sub New()
                Dim cp As New CreateParams With {
                .Caption = _Instance._ID,
                .Height = 0,
                .Width = 0,
                .X = 0,
                .Y = 0
            }
                'The window title is the same as the Id
                CreateHandle(cp)
            End Sub

            'The window procedure that handles notifications from new application instances
            Protected Overrides Sub WndProc(ByRef m As Message)
                If m.Msg = NativeRoutines.Enums.WindowMessages.WM_COPYDATA Then
                    'convert the message LParam to the WM_COPYDATA structure
                    Dim data As NativeRoutines.Structures.COPYDATASTRUCT = DirectCast(Marshal.PtrToStructure(m.LParam, GetType(NativeRoutines.Structures.COPYDATASTRUCT)), NativeRoutines.Structures.COPYDATASTRUCT)
                    Dim obj As Object = Nothing
                    If data.cbData > 0 AndAlso data.lpData <> IntPtr.Zero Then
                        'copy the native byte array to a .net byte array
                        Dim buffer As Byte() = New Byte(data.cbData - 1) {}
                        Marshal.Copy(data.lpData, buffer, 0, buffer.Length)
                        'deserialize the buffer to a new object
                        obj = Deserialize(buffer)
                    End If
                    _Instance.OnNewInstanceMessage(obj)
                Else
                    MyBase.WndProc(m)
                End If
            End Sub

        End Class

#End Region

#Region " Shared "

        Public Delegate Sub NewInstanceMessageHandler(sender As Object, message As Object)

        Public Shared Event NewInstanceMessage As NewInstanceMessageHandler

        Private Shared ReadOnly _Instance As New SingleInstanceApplication()

        Public Shared ReadOnly Property AlreadyExists() As Boolean
            Get
                Return _Instance.Exists
            End Get
        End Property

        Public Shared Function NotifyExistingInstance(message As Object) As Boolean
            If _Instance.Exists Then
                Return _Instance.NotifyPreviousInstance(message)
            End If
            Return False
        End Function

        Public Shared Function NotifyExistingInstance() As Boolean
            Return NotifyExistingInstance(Nothing)
        End Function

        Private Shared Function Deserialize(buffer As Byte()) As Object
            Using stream As New IO.MemoryStream(buffer)
                Return New BinaryFormatter().Deserialize(stream)
            End Using
        End Function

        Private Shared Function GetAppId() As String
            'Return IO.Path.GetFileName(Environment.GetCommandLineArgs()(0))
            Return My.Application.Info.ProductName
        End Function

        Private Shared Function Serialize(obj As [Object]) As Byte()
            Using stream As New IO.MemoryStream()
                Dim oBinaryFormatter As New BinaryFormatter()
                oBinaryFormatter.Serialize(stream, obj)
                Return stream.ToArray()
            End Using
        End Function

        Public Shared Sub Initialize()
            _Instance.Init()
        End Sub

        Public Shared Sub Close()
            Try
                If _Instance Is Nothing Then
                    _Instance.Dispose()
                End If
            Catch ex As Exception
                ex.ToLog(True)
            End Try
        End Sub

#End Region

    End Class

End Namespace