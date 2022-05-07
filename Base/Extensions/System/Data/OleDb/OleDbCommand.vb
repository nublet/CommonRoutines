Namespace CommonRoutines.Extensions

    Public Module System_Data_OleDb_OleDbCommand

        <Runtime.CompilerServices.Extension()> Public Sub AddParameters(ByRef cmd As OleDb.OleDbCommand, parameters As IDictionary(Of String, Object))
            If cmd Is Nothing Then
                Return
            End If

            If parameters Is Nothing Then
                Return
            End If

            If parameters.Count <= 0 Then
                Return
            End If

            cmd.Parameters.Clear()
            For Each kvp As KeyValuePair(Of String, Object) In parameters
                cmd.Parameters.AddWithValue(kvp.Key, Data.GetDBParameter(kvp.Value))
            Next
        End Sub

    End Module

End Namespace