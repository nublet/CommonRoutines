Namespace CommonRoutines.Settings

    Public Module _Shared

        Private _LongDatePattern As String = ""
        Private _LongTimePattern As String = ""
        Private _ShortDatePattern As String = ""
        Private _ShortTimePattern As String = ""
        Private _SynchronizationContext As Threading.SynchronizationContext = Nothing

        Public Property ComputerName As String = ""
        Public Property CurrentCulture As Globalization.CultureInfo = Nothing
        Public Property DateFormat As String = "yyyy-MM-dd HH:mm:ss"
        Public Property DebugLogging As Boolean = False
        Public Property DebugLoggingDBAccess As Boolean = False
        Public Property Indent As String = "   "
        Public Property ProductName As String = ""
        Public Property ProductVersion As String = ""
        Public Property SQLDateFormat As String = "yyyy-MM-dd HH:mm:ss"
        Public Property StringComparison As StringComparison = StringComparison.OrdinalIgnoreCase
        Public Property UseInvariantCulture As Boolean = False
        Public Property UseUTCDate As Boolean = False
        Public Property UserName As String = ""

        Public Property DefaultScreenName() As String
            Get
                Return Type.ToStringDB([Get]("DefaultScreenName"))
            End Get
            Set(value As String)
                Settings.Set("DefaultScreenName", value)
            End Set
        End Property

        Public ReadOnly Property ShortDatePattern As String
            Get
                Return _ShortDatePattern
            End Get
        End Property

        Public ReadOnly Property ShortTimePattern As String
            Get
                Return _ShortTimePattern
            End Get
        End Property

        Public ReadOnly Property LongDatePattern As String
            Get
                Return _LongDatePattern
            End Get
        End Property

        Public ReadOnly Property LongTimePattern As String
            Get
                Return _LongTimePattern
            End Get
        End Property

        Public ReadOnly Property SynchronizationContext As Threading.SynchronizationContext
            Get
                Return _SynchronizationContext
            End Get
        End Property

        Public Sub Initialise(errorLogLocation As String, loadSettingsCache As Boolean, performanceCounterLocation As String, useInvariantCulture As Boolean)
            Try
                _ErrorDetailLocation = errorLogLocation
                _PerformanceCounterLocation = performanceCounterLocation

                _SynchronizationContext = Threading.SynchronizationContext.Current

                Errors.Handlers.Clear()
                Errors.Handlers.Add(New Errors.LogErrorDetailHandler(AddressOf LogErrorDetail))

                Performance.Handlers.Clear()
                Performance.Handlers.Add(New Performance.LogPerformanceCounterHandler(AddressOf LogPerformanceCounter))

                If loadSettingsCache Then
                    _CacheFileName = "{0}Setting.XML".FormatWith(GetBaseDirectory(Enums.BaseDirectory.AppDataCompanyProduct))

                    LoadCache()
                End If

                ComputerName = GetComputerName()
                ProductName = My.Application.Info.ProductName
                ProductVersion = My.Application.Info.Version.GetVersionString()
                UserName = GetUsername()

                _UseInvariantCulture = useInvariantCulture

                If useInvariantCulture Then
                    _CurrentCulture = Globalization.CultureInfo.InvariantCulture
                Else
                    _CurrentCulture = Globalization.CultureInfo.CurrentCulture
                End If

                _LongDatePattern = _CurrentCulture.DateTimeFormat.LongDatePattern
                _LongTimePattern = _CurrentCulture.DateTimeFormat.LongTimePattern

                _ShortDatePattern = _CurrentCulture.DateTimeFormat.ShortDatePattern
                _ShortTimePattern = _CurrentCulture.DateTimeFormat.ShortTimePattern
            Catch ex As Exception
                ex.ToLog()
            End Try
        End Sub

#Region " Cache "

        Private ReadOnly _Cache As New List(Of Models.Setting)
        Private _CacheFileName As String = ""

        Public Function [Get](name As String) As Object
            Dim Setting As Models.Setting = _Cache.Where(Function(o) o.Name.IsEqualTo(name)).FirstOrDefault()

            If Setting Is Nothing Then
                Return Nothing
            End If

            Return Setting.Value
        End Function

        Public Function [Get](Of T)(name As String) As T
            Return Type.GetValue(Of T)([Get](name))
        End Function

        Public Sub LoadCache()
            Try
                _Cache.Clear()

                If _CacheFileName.IsNotSet() Then
                    Return
                End If

                _Cache.AddRange(Data.Xml.Load(Of Models.Setting)(_CacheFileName))
            Catch ex As Exception
                ex.ToLog()
            End Try
        End Sub

        Public Sub SaveCache()
            Try
                If _CacheFileName.IsNotSet() Then
                    Return
                End If

                Data.Xml.Save(_CacheFileName, _Cache)
            Catch ex As Exception
                ex.ToLog()
            End Try
        End Sub

        Public Sub [Set](name As String, value As Object)
            Dim Setting As Models.Setting = _Cache.Where(Function(o) o.Name.IsEqualTo(name)).FirstOrDefault()

            If Setting Is Nothing Then
                Setting = New Models.Setting(name)
                _Cache.Add(Setting)
            End If

            Setting.Value = value
        End Sub

#End Region

#Region " Error Handler "

        Private _ErrorDetailLocation As String = ""

        Public ReadOnly Property ErrorDetailLocation As String
            Get
                Return _ErrorDetailLocation
            End Get
        End Property

        Private Sub LogErrorDetail(silent As Boolean, ByRef errorDetail As Models.ErrorDetail)
            Try
                Using writer As New IO.StreamWriter(_ErrorDetailLocation, True)
                    writer.WriteLine(errorDetail.Details)
                    writer.WriteLine("")
                End Using
            Catch ex As Exception
            End Try
        End Sub

#End Region

#Region " Performance Handler "

        Private _PerformanceCounterLocation As String = ""

        Public ReadOnly Property PerformanceCounterLocation As String
            Get
                Return _PerformanceCounterLocation
            End Get
        End Property

        Private Sub LogPerformanceCounter(performanceState As Enums.PerformanceState, ByRef performanceCounter As Models.PerformanceCounter)
            Try
                If Not DebugLogging Then
                    Return
                End If

                If performanceState = Enums.PerformanceState.Started OrElse performanceState = Enums.PerformanceState.Unknown Then
                    Return
                End If

                If performanceCounter.HasParent Then
                    Return
                End If

                Dim Message As String = performanceCounter.GetMessage("")

                Using writer As New IO.StreamWriter(_PerformanceCounterLocation, True)
                    writer.WriteLine(Message)
                    writer.WriteLine("")
                End Using
            Catch ex As Exception
            End Try
        End Sub

#End Region

    End Module

End Namespace