Imports System.Data
Imports System.Data.OleDb

Namespace Data.OleDb

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

        Public Function BulkInsert(dataTable As DataTable, tableName As String) As Task Implements IDBAccessAsync.BulkInsert
            Throw New NotImplementedException
        End Function

        Public Async Function ExecuteNonQuery(commandText As String) As Task Implements IDBAccessAsync.ExecuteNonQuery
            Await ExecuteNonQuery(commandText, Nothing)
        End Function

        Public Async Function ExecuteNonQuery(commandText As String, parameters As IDictionary(Of String, Object)) As Task Implements IDBAccessAsync.ExecuteNonQuery
            Using Performance.StartDBCounter("CommonRoutines.Data.OleDb.DBAccessAsync", "ExecuteNonQuery", parameters.ToDebugString(commandText))
                If _ConnectionString.IsNotSet() Then
                    Return
                End If

                If commandText.IsNotSet() Then
                    Return
                End If

                Using conn As New OleDbConnection(_ConnectionString)
                    Await conn.OpenAsync()

                    Using cmd As OleDbCommand = GetCommand(commandText, parameters, conn)
                        Await cmd.ExecuteNonQueryAsync()
                    End Using
                End Using
            End Using
        End Function

        Public Async Function ExecuteReader(Of T As New)(commandText As String) As Task(Of IEnumerable(Of T)) Implements IDBAccessAsync.ExecuteReader
            Return Await ExecuteReader(Of T)(commandText, Nothing)
        End Function

        Public Async Function ExecuteReader(Of T As New)(commandText As String, parameters As IDictionary(Of String, Object)) As Task(Of IEnumerable(Of T)) Implements IDBAccessAsync.ExecuteReader
            Using Performance.StartDBCounter("CommonRoutines.Data.OleDb.DBAccessAsync", "ExecuteReader", parameters.ToDebugString(commandText))
                If _ConnectionString.IsNotSet() Then
                    Return New List(Of T)
                End If

                If commandText.IsNotSet() Then
                    Return New List(Of T)
                End If

                Using conn As New OleDbConnection(_ConnectionString)
                    Await conn.OpenAsync()

                    Using cmd As OleDbCommand = GetCommand(commandText, parameters, conn)
                        Using reader As Common.DbDataReader = Await cmd.ExecuteReaderAsync
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
            Using Performance.StartDBCounter("CommonRoutines.Data.OleDb.DBAccessAsync", "ExecuteReaderSingleColumn", parameters.ToDebugString(commandText))
                If _ConnectionString.IsNotSet() Then
                    Return New List(Of T)
                End If

                If commandText.IsNotSet() Then
                    Return New List(Of T)
                End If

                Using conn As New OleDbConnection(_ConnectionString)
                    Await conn.OpenAsync()

                    Using cmd As OleDbCommand = GetCommand(commandText, parameters, conn)
                        Using reader As Common.DbDataReader = Await cmd.ExecuteReaderAsync
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
            Using Performance.StartDBCounter("CommonRoutines.Data.OleDb.DBAccessAsync", "ExecuteScalar", parameters.ToDebugString(commandText))
                If _ConnectionString.IsNotSet() Then
                    Return Type.GetValue(Of T)(DBNull.Value)
                End If

                If commandText.IsNotSet() Then
                    Return Type.GetValue(Of T)(DBNull.Value)
                End If

                Using conn As New OleDbConnection(_ConnectionString)
                    Await conn.OpenAsync()

                    Using cmd As OleDbCommand = GetCommand(commandText, parameters, conn)
                        Return Type.GetValue(Of T)(Await cmd.ExecuteScalarAsync())
                    End Using
                End Using
            End Using
        End Function

        Public Async Function GetDataSet(commandText As String) As Task(Of DataSet) Implements IDBAccessAsync.GetDataSet
            Return Await GetDataSet(commandText, Nothing)
        End Function

        Public Async Function GetDataSet(commandText As String, parameters As IDictionary(Of String, Object)) As Task(Of DataSet) Implements IDBAccessAsync.GetDataSet
            Using Performance.StartDBCounter("CommonRoutines.Data.OleDb.DBAccessAsync", "GetDataSet", parameters.ToDebugString(commandText))
                If _ConnectionString.IsNotSet() Then
                    Return New DataSet
                End If

                If commandText.IsNotSet() Then
                    Return New DataSet
                End If

                Using conn As New OleDbConnection(_ConnectionString)
                    Await conn.OpenAsync()

                    Using cmd As OleDbCommand = GetCommand(commandText, parameters, conn)
                        Using reader As Common.DbDataReader = Await cmd.ExecuteReaderAsync
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
            Using Performance.StartDBCounter("CommonRoutines.Data.OleDb.DBAccessAsync", "GetDataTable", parameters.ToDebugString(commandText))
                If _ConnectionString.IsNotSet() Then
                    Return New DataTable
                End If

                If commandText.IsNotSet() Then
                    Return New DataTable
                End If

                Using conn As New OleDbConnection(_ConnectionString)
                    Await conn.OpenAsync()

                    Using cmd As OleDbCommand = GetCommand(commandText, parameters, conn)
                        Using reader As Common.DbDataReader = Await cmd.ExecuteReaderAsync
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
            Using Performance.StartDBCounter("CommonRoutines.Data.OleDb.DBAccessAsync", "GetSchema", "TableName: {0}".FormatWith(tableName))
                If _ConnectionString.IsNotSet() Then
                    Return New Dictionary(Of String, String)
                End If

                If tableName.IsNotSet() Then
                    Return New Dictionary(Of String, String)
                End If

                Using conn As New OleDbConnection(_ConnectionString)
                    Await conn.OpenAsync()

                    Using cmd As OleDbCommand = GetCommand(selectAllSql, conn)
                        Using reader As Common.DbDataReader = Await cmd.ExecuteReaderAsync(CommandBehavior.SchemaOnly)
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

        Public Function Merge(createSQL As String, dropSQL As String, dataTable As DataTable, mergeSQL As String, tableName As String) As Task Implements IDBAccessAsync.Merge
            Throw New NotImplementedException
        End Function

#End Region

    End Class

End Namespace