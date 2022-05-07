Namespace CommonRoutines.Extensions

    Public Module System_Collections_Generic_IDictionary

        <Runtime.CompilerServices.Extension()> Public Function ToDebugString(d As IDictionary(Of String, Object), commandText As String) As String
            If Not Settings.DebugLogging Then
                Return ""
            End If

            If Not Settings.DebugLoggingDBAccess Then
                Return ""
            End If

            If d Is Nothing OrElse d.Count <= 0 Then
                Return "Command Text: {0}".FormatWith(commandText)
            End If

            Dim CommandTextLower As String = commandText.ToLower()

            Return "{0}Command Text: {1}{0}Parameters: {2}".FormatWith(Environment.NewLine, commandText, String.Join(", ", d.Where(Function(o) CommandTextLower.Contains("@{0}".FormatWith(o.Key.ToLower()))).Select(Function(o) "{0} -> {1}.".FormatWith(o.Key, Type.ToStringDB(o.Value)))))
        End Function

    End Module

End Namespace