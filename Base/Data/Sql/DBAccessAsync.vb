Imports System.Data.SqlClient

Namespace CommonRoutines.Data.Sql

    Public Class DBAccessAsync
        Implements IDBAccessAsync

        Private ReadOnly _ConnectionString As String = ""

        Public Sub New(connectionString As String)
            _ConnectionString = connectionString
        End Sub

#Region " IDBAccessAsync "

        Public ReadOnly Property ConnectionString As String Implements IDBAccessAsync.ConnectionString
            Get
                Return _ConnectionString
            End Get
        End Property

        Public Async Function BulkInsert(dataTable As DataTable, tableName As String) As Task Implements IDBAccessAsync.BulkInsert
            Using Performance.StartDBCounter("CommonRoutines.Data.Sql.DBAccessAsync", "BulkInsert", "TableName: {0}".FormatWith(tableName))
                If _ConnectionString.IsNotSet() Then
                    Return
                End If

                If dataTable Is Nothing OrElse dataTable.Rows.Count <= 0 Then
                    Return
                End If

                If tableName.IsNotSet() Then
                    Return
                End If

                Using conn As New SqlConnection(_ConnectionString)
                    Await conn.OpenAsync()

                    Using BulkCopy As New SqlBulkCopy(conn)
                        BulkCopy.BatchSize = 500
                        BulkCopy.DestinationTableName = tableName
                        Await BulkCopy.WriteToServerAsync(dataTable)
                    End Using
                End Using
            End Using
        End Function

        Public Async Function ExecuteNonQuery(commandText As String) As Task Implements IDBAccessAsync.ExecuteNonQuery
            Await ExecuteNonQuery(commandText, Nothing)
        End Function

        Public Async Function ExecuteNonQuery(commandText As String, parameters As IDictionary(Of String, Object)) As Task Implements IDBAccessAsync.ExecuteNonQuery
            Using Performance.StartDBCounter("CommonRoutines.Data.Sql.DBAccessAsync", "ExecuteNonQuery", parameters.ToDebugString(commandText))
                If _ConnectionString.IsNotSet() Then
                    Return
                End If

                If commandText.IsNotSet() Then
                    Return
                End If

                Using conn As New SqlConnection(_ConnectionString)
                    Await conn.OpenAsync()

                    Using cmd As SqlCommand = GetCommand(commandText, parameters, conn)
                        Await cmd.ExecuteNonQueryAsync()
                    End Using
                End Using
            End Using
        End Function

        Public Async Function ExecuteReader(Of T As New)(commandText As String) As Task(Of IEnumerable(Of T)) Implements IDBAccessAsync.ExecuteReader
            Return Await ExecuteReader(Of T)(commandText, Nothing)
        End Function

        Public Async Function ExecuteReader(Of T As New)(commandText As String, parameters As IDictionary(Of String, Object)) As Task(Of IEnumerable(Of T)) Implements IDBAccessAsync.ExecuteReader
            Using Performance.StartDBCounter("CommonRoutines.Data.Sql.DBAccessAsync", "ExecuteReader", parameters.ToDebugString(commandText))
                If _ConnectionString.IsNotSet() Then
                    Return New List(Of T)
                End If

                If commandText.IsNotSet() Then
                    Return New List(Of T)
                End If

                Using conn As New SqlConnection(_ConnectionString)
                    Await conn.OpenAsync()

                    Using cmd As SqlCommand = GetCommand(commandText, parameters, conn)
                        Using reader As SqlDataReader = Await cmd.ExecuteReaderAsync
                            Return reader.ReadItems(Of T)().ToList()
                        End Using
                    End Using
                End Using
            End Using
        End Function

        Public Async Function ExecuteReaderSingleColumn(Of T)(commandText As String) As Task(Of IEnumerable(Of T)) Implements IDBAccessAsync.ExecuteReaderSingleColumn
            Return Await ExecuteReaderSingleColumn(Of T)(commandText, Nothing)
        End Function

        Public Async Function ExecuteReaderSingleColumn(Of T)(commandText As String, parameters As IDictionary(Of String, Object)) As Task(Of IEnumerable(Of T)) Implements IDBAccessAsync.ExecuteReaderSingleColumn
            Using Performance.StartDBCounter("CommonRoutines.Data.Sql.DBAccessAsync", "ExecuteReaderSingleColumn", parameters.ToDebugString(commandText))
                If _ConnectionString.IsNotSet() Then
                    Return New List(Of T)
                End If

                If commandText.IsNotSet() Then
                    Return New List(Of T)
                End If

                Using conn As New SqlConnection(_ConnectionString)
                    Await conn.OpenAsync()

                    Using cmd As SqlCommand = GetCommand(commandText, parameters, conn)
                        Using reader As SqlDataReader = Await cmd.ExecuteReaderAsync
                            Return reader.ReadItemsSingleColumn(Of T)().ToList()
                        End Using
                    End Using
                End Using
            End Using
        End Function

        Public Async Function ExecuteScalar(commandText As String) As Task(Of Object) Implements IDBAccessAsync.ExecuteScalar
            Return Await ExecuteScalar(commandText, Nothing)
        End Function

        Public Async Function ExecuteScalar(commandText As String, parameters As IDictionary(Of String, Object)) As Task(Of Object) Implements IDBAccessAsync.ExecuteScalar
            Return Await ExecuteScalar(Of Object)(commandText, parameters)
        End Function

        Public Async Function ExecuteScalar(Of T)(commandText As String) As Task(Of T) Implements IDBAccessAsync.ExecuteScalar
            Return Await ExecuteScalar(Of T)(commandText, Nothing)
        End Function

        Public Async Function ExecuteScalar(Of T)(commandText As String, parameters As IDictionary(Of String, Object)) As Task(Of T) Implements IDBAccessAsync.ExecuteScalar
            Using Performance.StartDBCounter("CommonRoutines.Data.Sql.DBAccessAsync", "ExecuteScalar", parameters.ToDebugString(commandText))
                If _ConnectionString.IsNotSet() Then
                    Return Type.GetValue(Of T)(DBNull.Value)
                End If

                If commandText.IsNotSet() Then
                    Return Type.GetValue(Of T)(DBNull.Value)
                End If

                Using conn As New SqlConnection(_ConnectionString)
                    Await conn.OpenAsync()

                    Using cmd As SqlCommand = GetCommand(commandText, parameters, conn)
                        Return Type.GetValue(Of T)(Await cmd.ExecuteScalarAsync())
                    End Using
                End Using
            End Using
        End Function

        Public Async Function GetDataSet(commandText As String) As Task(Of DataSet) Implements IDBAccessAsync.GetDataSet
            Return Await GetDataSet(commandText, Nothing)
        End Function

        Public Async Function GetDataSet(commandText As String, parameters As IDictionary(Of String, Object)) As Task(Of DataSet) Implements IDBAccessAsync.GetDataSet
            Using Performance.StartDBCounter("CommonRoutines.Data.Sql.DBAccessAsync", "GetDataSet", parameters.ToDebugString(commandText))
                If _ConnectionString.IsNotSet() Then
                    Return New DataSet
                End If

                If commandText.IsNotSet() Then
                    Return New DataSet
                End If

                Using conn As New SqlConnection(_ConnectionString)
                    Await conn.OpenAsync()

                    Using cmd As SqlCommand = GetCommand(commandText, parameters, conn)
                        Using reader As SqlDataReader = Await cmd.ExecuteReaderAsync
                            Dim dt As New DataTable
                            dt.Load(reader)

                            Dim ds As New DataSet
                            ds.Tables.Add(dt)

                            Return ds
                        End Using
                    End Using
                End Using
            End Using
        End Function

        Public Async Function GetDataTable(commandText As String) As Task(Of DataTable) Implements IDBAccessAsync.GetDataTable
            Return Await GetDataTable(commandText, Nothing)
        End Function

        Public Async Function GetDataTable(commandText As String, parameters As IDictionary(Of String, Object)) As Task(Of DataTable) Implements IDBAccessAsync.GetDataTable
            Using Performance.StartDBCounter("CommonRoutines.Data.Sql.DBAccessAsync", "GetDataTable", parameters.ToDebugString(commandText))
                If _ConnectionString.IsNotSet() Then
                    Return New DataTable
                End If

                If commandText.IsNotSet() Then
                    Return New DataTable
                End If

                Using conn As New SqlConnection(_ConnectionString)
                    Await conn.OpenAsync()

                    Using cmd As SqlCommand = GetCommand(commandText, parameters, conn)
                        Using reader As SqlDataReader = Await cmd.ExecuteReaderAsync
                            Dim dt As New DataTable
                            dt.Load(reader)

                            Return dt
                        End Using
                    End Using
                End Using
            End Using
        End Function

        Public Async Function GetSchema(tableName As String) As Task(Of Dictionary(Of String, String)) Implements IDBAccessAsync.GetSchema
            Return Await GetSchema("SELECT * FROM [{0}]".FormatWith(tableName), tableName)
        End Function

        Public Async Function GetSchema(selectAllSql As String, tableName As String) As Task(Of Dictionary(Of String, String)) Implements IDBAccessAsync.GetSchema
            Using Performance.StartDBCounter("CommonRoutines.Data.Sql.DBAccessAsync", "GetSchema", "TableName: {0}".FormatWith(tableName))
                If _ConnectionString.IsNotSet() Then
                    Return New Dictionary(Of String, String)
                End If

                If tableName.IsNotSet() Then
                    Return New Dictionary(Of String, String)
                End If

                Using conn As New SqlConnection(_ConnectionString)
                    Await conn.OpenAsync()

                    Using cmd As SqlCommand = GetCommand(selectAllSql, conn)
                        Using reader As SqlDataReader = Await cmd.ExecuteReaderAsync(CommandBehavior.SchemaOnly)
                            Dim Results As New Dictionary(Of String, String)

                            For I As Integer = 0 To reader.FieldCount - 1
                                CheckFieldType(reader.GetName(I), reader.GetDataTypeName(I), Results)
                            Next

                            Return Results
                        End Using
                    End Using
                End Using
            End Using
        End Function

        Public Async Function Merge(createSQL As String, dropSQL As String, dataTable As DataTable, mergeSQL As String, tableName As String) As Task Implements IDBAccessAsync.Merge
            Using Performance.StartDBCounter("CommonRoutines.Data.Sql.DBAccessAsync", "Merge", "TableName: {0}".FormatWith(tableName))
                If _ConnectionString.IsNotSet() Then
                    Return
                End If

                If createSQL.IsNotSet() Then
                    Return
                End If

                If dropSQL.IsNotSet() Then
                    Return
                End If

                If dataTable Is Nothing OrElse dataTable.Rows.Count <= 0 Then
                    Return
                End If

                If mergeSQL.IsNotSet() Then
                    Return
                End If

                If tableName.IsNotSet() Then
                    Return
                End If

                Using conn As New SqlConnection(_ConnectionString)
                    Await conn.OpenAsync()

                    ' Create the Temporary table
                    Using Performance.StartDBCounter("CommonRoutines.Data.Sql.DBAccessAsync", "Merge", "Create Temp: {0}".FormatWith(createSQL))
                        Using cmd As SqlCommand = GetCommand(createSQL, conn)
                            Await cmd.ExecuteNonQueryAsync()
                        End Using
                    End Using

                    ' Bulk insert the records into the temporary table.
                    Using Performance.StartDBCounter("CommonRoutines.Data.Sql.DBAccessAsync", "Merge", "Bulk Copy:- Table: {0}, Records: {1}".FormatWith(tableName, dataTable.Rows.Count))
                        Using BulkCopy As New SqlBulkCopy(conn)
                            BulkCopy.BatchSize = 500
                            BulkCopy.DestinationTableName = "#{0}".FormatWith(tableName)
                            Await BulkCopy.WriteToServerAsync(dataTable)
                        End Using
                    End Using

                    ' Merge the Temporary table with the Main table
                    Using Performance.StartDBCounter("CommonRoutines.Data.Sql.DBAccessAsync", "Merge", "Merge Tables: {0}".FormatWith(mergeSQL))
                        Using cmd As SqlCommand = GetCommand(mergeSQL, conn)
                            Await cmd.ExecuteNonQueryAsync()
                        End Using
                    End Using

                    ' Drop the Temporary table
                    Using Performance.StartDBCounter("CommonRoutines.Data.Sql.DBAccessAsync", "Merge", "Drop Temp: {0}".FormatWith(dropSQL))
                        Using cmd As SqlCommand = GetCommand(dropSQL, conn)
                            Await cmd.ExecuteNonQueryAsync()
                        End Using
                    End Using
                End Using
            End Using
        End Function

#End Region

    End Class

End Namespace