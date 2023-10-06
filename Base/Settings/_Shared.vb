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

        Public Sub Initialise(loadSettingsCache As Boolean, useInvariantCulture As Boolean)
            Try
                _SynchronizationContext = Threading.SynchronizationContext.Current

                Errors.Handlers.Clear()
                Performance.Handlers.Clear()

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

        Public Sub SetDetails(useInvariantCulture As Boolean)
            Try
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

    End Module

End Namespace