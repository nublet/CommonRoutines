Namespace CommonRoutines.Data.Sql

    Public Module _Shared

        Public Function GetCommand(commandText As String, ByRef conn As SqlClient.SqlConnection) As SqlClient.SqlCommand
            Return GetCommand(commandText, Nothing, conn)
        End Function

        Public Function GetCommand(commandText As String, parameters As IDictionary(Of String, Object), ByRef conn As SqlClient.SqlConnection) As SqlClient.SqlCommand
            Dim Result As New SqlClient.SqlCommand(commandText, conn)

            Result.AddParameters(parameters)
            Result.CommandTimeout = 300 ' 5 Minutes

            Return Result
        End Function

        Public Function GetConnectionString(database As String, server As String) As String
            Return GetConnectionString(database, server, "", "")
        End Function

        Public Function GetConnectionString(database As String, server As String, userName As String, password As String) As String
            Dim CSBuilder As New SqlClient.SqlConnectionStringBuilder With {
                .AsynchronousProcessing = True,
                .MultipleActiveResultSets = True,
                .Pooling = True,
                .DataSource = server,
                .InitialCatalog = database
            }

            If userName.IsNotSet() OrElse password.IsNotSet() Then
                CSBuilder.IntegratedSecurity = True
            Else
                CSBuilder.IntegratedSecurity = False
                CSBuilder.UserID = userName
                CSBuilder.Password = password
            End If

            Return CSBuilder.ToString()
        End Function

    End Module

End Namespace