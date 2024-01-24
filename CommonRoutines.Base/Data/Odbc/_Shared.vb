Imports System.Data.Odbc

Namespace Data.Odbc

    Public Module _Shared

        Public Function GetCommand(commandText As String, ByRef conn As OdbcConnection) As OdbcCommand
            Return GetCommand(commandText, Nothing, conn)
        End Function

        Public Function GetCommand(commandText As String, parameters As IDictionary(Of String, Object), ByRef conn As OdbcConnection) As OdbcCommand
            Dim Result As New OdbcCommand(commandText, conn)

            Result.AddParameters(parameters)
            Result.CommandTimeout = 300 ' 5 Minutes

            Return Result
        End Function

    End Module

End Namespace