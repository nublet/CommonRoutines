Namespace CommonRoutines.Models

    Public Class PerformanceCounter
        Implements IDisposable

        Private ReadOnly _Children As New List(Of PerformanceCounter)
        Private ReadOnly _GUID As String = ""
        Private ReadOnly _Parent As PerformanceCounter = Nothing
        Private ReadOnly _PerformanceType As Enums.PerformanceType = Enums.PerformanceType.Unknown
        Private ReadOnly _ThreadID As Integer = -1

        Public Property EndDate As DateTime = Nothing
        Public Property EndTicks As Long = 0
        Public Property Message As String = ""
        Public Property StartDate As DateTime = Nothing
        Public Property StartTicks As Long = 0

        Public ReadOnly Property Children As List(Of PerformanceCounter)
            Get
                Return _Children
            End Get
        End Property

        Public ReadOnly Property GUID As String
            Get
                Return _GUID
            End Get
        End Property

        Public ReadOnly Property HasParent As Boolean
            Get
                If _Parent Is Nothing Then
                    Return False
                End If
                Return True
            End Get
        End Property

        Public ReadOnly Property Parent As PerformanceCounter
            Get
                Return _Parent
            End Get
        End Property

        Public ReadOnly Property PerformanceType As Enums.PerformanceType
            Get
                Return _PerformanceType
            End Get
        End Property

        Public ReadOnly Property ThreadID As Integer
            Get
                Return _ThreadID
            End Get
        End Property

        Public Sub New(message As String, performanceType As Enums.PerformanceType)
            StartDate = GetCurrentDate()
            StartTicks = StartDate.Ticks

            _GUID = System.Guid.NewGuid().ToString().ToUpper()
            _Message = message
            _PerformanceType = performanceType
            _ThreadID = Threading.Thread.CurrentThread.ManagedThreadId

            _Parent = GetThreadCounter(_ThreadID)

            If _Parent IsNot Nothing Then
                _Parent.Children.Add(Me)
            End If

            Performance.Log(Enums.PerformanceState.Started, Me)

            SetThreadCounter(_ThreadID, Me)
        End Sub

        Private Function GetSeconds() As Double
            Return New TimeSpan((EndTicks - StartTicks)).TotalSeconds
        End Function

        Public Function GetMessage(indent As String) As String
            Dim Result As String = ""

            If _Parent Is Nothing Then
                Result = "{0}. {1}: {2:N3}s - {3}".FormatWith(_ThreadID, StartDate.GetSQLString("dd MMMM yyyy - HH:mm:ss"), GetSeconds(), _Message)
            Else
                Result = "{0}{1:N3}s: {2}".FormatWith(indent, GetSeconds(), _Message)
            End If

            If _Children.Count >= 0 Then
                Result.Append("{0}{1}".FormatWith(Environment.NewLine, indent), String.Join(Environment.NewLine, _Children.Select(Function(o) o.GetMessage("{0}{1}".FormatWith(indent, Settings.Indent)))))
            End If

            If _Parent Is Nothing Then
                Result.Append(Environment.NewLine)
            End If

            Return Result
        End Function

        Private Sub StopCounter()
            EndDate = GetCurrentDate()
            EndTicks = EndDate.Ticks

            Performance.Log(Enums.PerformanceState.Completed, Me)

            SetThreadCounter(_ThreadID, _Parent)
        End Sub

#Region " IDisposable "

        Private _IsDisposed As Boolean = False

        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not _IsDisposed Then
                If disposing Then
                    StopCounter()
                End If
            End If
            _IsDisposed = True
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

#End Region

#Region " Shared "

        Private Shared ReadOnly _ThreadCounter As New Dictionary(Of Integer, PerformanceCounter)

        Protected Friend Shared Function GetThreadCounter(threadID As Integer) As PerformanceCounter
            If _ThreadCounter.ContainsKey(threadID) Then
                Return _ThreadCounter(threadID)
            End If

            Return Nothing
        End Function

        Protected Friend Shared Sub SetThreadCounter(threadID As Integer, performanceCounter As PerformanceCounter)
            If _ThreadCounter.ContainsKey(threadID) Then
                _ThreadCounter(threadID) = performanceCounter
            Else
                _ThreadCounter.Add(threadID, performanceCounter)
            End If
        End Sub

#End Region

    End Class

End Namespace