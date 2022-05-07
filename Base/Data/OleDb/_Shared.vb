Imports System.Data.OleDb

Namespace CommonRoutines.Data.OleDb

    Public Module _Shared

        Public Function GetCommand(commandText As String, ByRef conn As OleDbConnection) As OleDbCommand
            Return GetCommand(commandText, Nothing, conn)
        End Function

        Public Function GetCommand(commandText As String, parameters As IDictionary(Of String, Object), ByRef conn As OleDbConnection) As OleDbCommand
            Dim Result As New OleDbCommand(commandText, conn)

            Result.AddParameters(parameters)
            Result.CommandTimeout = 300 ' 5 Minutes

            Return Result
        End Function

    End Module

End Namespace