Imports System.Data
Imports System.Data.SqlClient

Namespace Data.Sql

    Public Class DBAccess
        Implements IDBAccess

        Private ReadOnly _ConnectionString As String = ""

        Public Sub New(connectionString As String)
            _ConnectionString = connectionString
        End Sub

#Region " IDBAccess "

        Public ReadOnly Property ConnectionString As String Implements IDBAccess.ConnectionString
            Get
                Return _ConnectionString
            End Get
        End Property

        Public Function ExecuteReader(Of T As New)(commandText As String) As IEnumerable(Of T) Implements IDBAccess.ExecuteReader
            Return ExecuteReader(Of T)(commandText, Nothing)
        End Function

        Public Function ExecuteReader(Of T As New)(commandText As String, parameters As IDictionary(Of String, Object)) As IEnumerable(Of T) Implements IDBAccess.ExecuteReader
            Using Performance.StartDBCounter("CommonRoutines.Data.Sql.DBAccess", "ExecuteReader", parameters.ToDebugString(commandText))
                If _ConnectionString.IsNotSet() Then
                    Return New List(Of T)
                End If

                If commandText.IsNotSet() Then
                    Return New List(Of T)
                End If

                Using conn As New SqlConnection(_ConnectionString)
                    conn.Open()

                    Using cmd As SqlCommand = GetCommand(commandText, parameters, conn)
                        Using reader As SqlDataReader = cmd.ExecuteReader
                            Return reader.ReadItems(Of T)().ToList()
                        End Using
                    End Using
                End Using
            End Using
        End Function

        Public Function ExecuteReaderSingleColumn(Of T)(commandText As String) As IEnumerable(Of T) Implements IDBAccess.ExecuteReaderSingleColumn
            Return ExecuteReaderSingleColumn(Of T)(commandText, Nothing)
        End Function

        Public Function ExecuteReaderSingleColumn(Of T)(commandText As String, parameters As IDictionary(Of String, Object)) As IEnumerable(Of T) Implements IDBAccess.ExecuteReaderSingleColumn
            Using Performance.StartDBCounter("CommonRoutines.Data.Sql.DBAccess", "ExecuteReaderSingleColumn", parameters.ToDebugString(commandText))
                If _ConnectionString.IsNotSet() Then
                    Return New List(Of T)
                End If

                If commandText.IsNotSet() Then
                    Return New List(Of T)
                End If

                Using conn As New SqlConnection(_ConnectionString)
                    conn.Open()

                    Using cmd As SqlCommand = GetCommand(commandText, parameters, conn)
                        Using reader As SqlDataReader = cmd.ExecuteReader
                            Return reader.ReadItemsSingleColumn(Of T)().ToList()
                        End Using
                    End Using
                End Using
            End Using
        End Function

        Public Function ExecuteScalar(commandText As String) As Object Implements IDBAccess.ExecuteScalar
            Return ExecuteScalar(commandText, Nothing)
        End Function

        Public Function ExecuteScalar(commandText As String, parameters As IDictionary(Of String, Object)) As Object Implements IDBAccess.ExecuteScalar
            Return ExecuteScalar(Of Object)(commandText, parameters)
        End Function

        Public Function ExecuteScalar(Of T)(commandText As String) As T Implements IDBAccess.ExecuteScalar
            Return ExecuteScalar(Of T)(commandText, Nothing)
        End Function

        Public Function ExecuteScalar(Of T)(commandText As String, parameters As IDictionary(Of String, Object)) As T Implements IDBAccess.ExecuteScalar
            Using Performance.StartDBCounter("CommonRoutines.Data.Sql.DBAccess", "ExecuteScalar", parameters.ToDebugString(commandText))
                If _ConnectionString.IsNotSet() Then
                    Return Type.GetValue(Of T)(DBNull.Value)
                End If

                If commandText.IsNotSet() Then
                    Return Type.GetValue(Of T)(DBNull.Value)
                End If

                Using conn As New SqlConnection(_ConnectionString)
                    conn.Open()

                    Using cmd As SqlCommand = GetCommand(commandText, parameters, conn)
                        Return Type.GetValue(Of T)(cmd.ExecuteScalar())
                    End Using
                End Using
            End Using
        End Function

        Public Function GetDataSet(commandText As String) As DataSet Implements IDBAccess.GetDataSet
            Return GetDataSet(commandText, Nothing)
        End Function

        Public Function GetDataSet(commandText As String, parameters As IDictionary(Of String, Object)) As DataSet Implements IDBAccess.GetDataSet
            Using Performance.StartDBCounter("CommonRoutines.Data.Sql.DBAccess", "GetDataSet", parameters.ToDebugString(commandText))
                If _ConnectionString.IsNotSet() Then
                    Return New DataSet()
                End If

                If commandText.IsNotSet() Then
                    Return New DataSet()
                End If

                Dim dt As DataTable = GetDataTable(commandText, parameters)

                Dim ds As New DataSet()
                ds.Tables.Add(dt)

                Return ds
            End Using
        End Function

        Public Function GetDataTable(commandText As String) As DataTable Implements IDBAccess.GetDataTable
            Return GetDataTable(commandText, Nothing)
        End Function

        Public Function GetDataTable(commandText As String, parameters As IDictionary(Of String, Object)) As DataTable Implements IDBAccess.GetDataTable
            Using Performance.StartDBCounter("CommonRoutines.Data.Sql.DBAccess", "GetDataTable", parameters.ToDebugString(commandText))
                If _ConnectionString.IsNotSet() Then
                    Return New DataTable()
                End If

                If commandText.IsNotSet() Then
                    Return New DataTable()
                End If

                Using conn As New SqlConnection(_ConnectionString)
                    conn.Open()

                    Using cmd As SqlCommand = GetCommand(commandText, parameters, conn)
                        Using reader As SqlDataReader = cmd.ExecuteReader
                            Dim dt As New DataTable()
                            dt.Load(reader)

                            Return dt
                        End Using
                    End Using
                End Using
            End Using
        End Function

        Public Function GetSchema(tableName As String) As Dictionary(Of String, String) Implements IDBAccess.GetSchema
            Return GetSchema("SELECT * FROM [{0}]".FormatWith(tableName), tableName)
        End Function

        Public Function GetSchema(selectAllSql As String, tableName As String) As Dictionary(Of String, String) Implements IDBAccess.GetSchema
            Using Performance.StartDBCounter("CommonRoutines.Data.Sql.DBAccess", "GetSchema", "TableName: {0}".FormatWith(tableName))
                If _ConnectionString.IsNotSet() Then
                    Return New Dictionary(Of String, String)
                End If

                If tableName.IsNotSet() Then
                    Return New Dictionary(Of String, String)
                End If

                Using conn As New SqlConnection(_ConnectionString)
                    conn.Open()

                    Using cmd As SqlCommand = GetCommand(selectAllSql, Nothing, conn)
                        Using reader As SqlDataReader = cmd.ExecuteReader(CommandBehavior.SchemaOnly)
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

        Public Sub BulkInsert(dataTable As DataTable, tableName As String) Implements IDBAccess.BulkInsert
            Using Performance.StartDBCounter("CommonRoutines.Data.Sql.DBAccess", "BulkInsert", "TableName: {0}".FormatWith(tableName))
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
                    conn.Open()

                    Using BulkCopy As New SqlBulkCopy(conn)
                        BulkCopy.BatchSize = 500
                        BulkCopy.DestinationTableName = tableName
                        BulkCopy.WriteToServer(dataTable)
                    End Using
                End Using
            End Using
        End Sub

        Public Sub ExecuteNonQuery(commandText As String) Implements IDBAccess.ExecuteNonQuery
            ExecuteNonQuery(commandText, Nothing)
        End Sub

        Public Sub ExecuteNonQuery(commandText As String, parameters As IDictionary(Of String, Object)) Implements IDBAccess.ExecuteNonQuery
            Using Performance.StartDBCounter("CommonRoutines.Data.Sql.DBAccess", "ExecuteNonQuery", parameters.ToDebugString(commandText))
                If _ConnectionString.IsNotSet() Then
                    Return
                End If

                If commandText.IsNotSet() Then
                    Return
                End If

                Using conn As New SqlConnection(_ConnectionString)
                    conn.Open()

                    Using cmd As SqlCommand = GetCommand(commandText, parameters, conn)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
            End Using
        End Sub

        Public Sub Merge(createSQL As String, dropSQL As String, dataTable As DataTable, mergeSQL As String, tableName As String) Implements IDBAccess.Merge
            Using Performance.StartDBCounter("CommonRoutines.Data.Sql.DBAccess", "Merge", "TableName: {0}".FormatWith(tableName))
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
                    conn.Open()

                    ' Create the Temporary table
                    Using Performance.StartDBCounter("CommonRoutines.Data.Sql.DBAccess", "Merge", "Create Temp: {0}".FormatWith(createSQL))
                        Using cmd As SqlCommand = GetCommand(createSQL, conn)
                            cmd.ExecuteNonQuery()
                        End Using
                    End Using

                    ' Bulk insert the records into the temporary table.
                    Using Performance.StartDBCounter("CommonRoutines.Data.Sql.DBAccess", "Merge", "Bulk Copy:- Table: {0}, Records: {1}".FormatWith(tableName, dataTable.Rows.Count))
                        Using BulkCopy As New SqlBulkCopy(conn)
                            BulkCopy.BatchSize = 500
                            BulkCopy.DestinationTableName = "#{0}".FormatWith(tableName)
                            BulkCopy.WriteToServer(dataTable)
                        End Using
                    End Using

                    ' Merge the Temporary table with the Main table
                    Using Performance.StartDBCounter("CommonRoutines.Data.Sql.DBAccess", "Merge", "Merge Tables: {0}".FormatWith(mergeSQL))
                        Using cmd As SqlCommand = GetCommand(mergeSQL, conn)
                            cmd.ExecuteNonQuery()
                        End Using
                    End Using

                    ' Drop the Temporary table
                    Using Performance.StartDBCounter("CommonRoutines.Data.Sql.DBAccess", "Merge", "Drop Temp: {0}".FormatWith(dropSQL))
                        Using cmd As SqlCommand = GetCommand(dropSQL, conn)
                            cmd.ExecuteNonQuery()
                        End Using
                    End Using
                End Using
            End Using
        End Sub

#End Region

    End Class

End Namespace