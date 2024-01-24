Namespace Performance

    Public Module _Shared

        Public Delegate Sub LogPerformanceCounterHandler(performanceState As Enums.PerformanceState, ByRef performanceCounter As Models.PerformanceCounter)

        Private ReadOnly _Handlers As New List(Of LogPerformanceCounterHandler)

        Public ReadOnly Property Handlers As List(Of LogPerformanceCounterHandler)
            Get
                Return _Handlers
            End Get
        End Property

        Public Function StartCounter(className As String, methodName As String) As Models.PerformanceCounter
            Return StartCounter("{0} -> {1}".FormatWith(className, methodName))
        End Function

        Public Function StartCounter(className As String, methodName As String, message As String) As Models.PerformanceCounter
            Return StartCounter("{0} -> {1}: {2}".FormatWith(className, methodName, message))
        End Function

        Public Function StartCounter(className As String, methodName As String, message As String, performanceType As Enums.PerformanceType) As Models.PerformanceCounter
            Return StartCounter("{0} -> {1}: {2}".FormatWith(className, methodName, message), performanceType)
        End Function

        Public Function StartCounter(className As String, methodName As String, performanceType As Enums.PerformanceType) As Models.PerformanceCounter
            Return StartCounter("{0} -> {1}".FormatWith(className, methodName), performanceType)
        End Function

        Public Function StartCounter(message As String) As Models.PerformanceCounter
            Return StartCounter(message, Enums.PerformanceType.Unknown)
        End Function

        Public Function StartCounter(message As String, performanceType As Enums.PerformanceType) As Models.PerformanceCounter
            If Not Settings.DebugLogging Then
                Return Nothing
            End If

            Return New Models.PerformanceCounter(message, performanceType)
        End Function

        Public Function StartDBCounter(className As String, methodName As String) As Models.PerformanceCounter
            Return StartDBCounter("{0} -> {1}".FormatWith(className, methodName))
        End Function

        Public Function StartDBCounter(className As String, methodName As String, message As String) As Models.PerformanceCounter
            Return StartDBCounter("{0} -> {1}: {2}".FormatWith(className, methodName, message))
        End Function

        Public Function StartDBCounter(message As String) As Models.PerformanceCounter
            If Not Settings.DebugLogging Then
                Return Nothing
            End If

            If Not Settings.DebugLoggingDBAccess Then
                Return Nothing
            End If

            Return New Models.PerformanceCounter(message, Enums.PerformanceType.DatabaseAccess)
        End Function

        Public Sub Log(performanceState As Enums.PerformanceState, ByRef performanceCounter As Models.PerformanceCounter)
            Try
                If Not Settings.DebugLogging Then
                    Return
                End If

                For Each Current As LogPerformanceCounterHandler In _Handlers
                    If Current Is Nothing Then
                        Continue For
                    End If

                    Current(performanceState, performanceCounter)
                Next
            Catch ex As Exception
            End Try
        End Sub

    End Module

End Namespace