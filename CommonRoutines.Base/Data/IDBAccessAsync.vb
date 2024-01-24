Imports System.Data

Namespace Data

    Public Interface IDBAccessAsync

        ReadOnly Property ConnectionString As String

        Function BulkInsert(dataTable As DataTable, tableName As String) As Task

        Function ExecuteNonQuery(commandText As String) As Task
        Function ExecuteNonQuery(commandText As String, parameters As IDictionary(Of String, Object)) As Task

        Function ExecuteReader(Of T As New)(commandText As String) As Task(Of IEnumerable(Of T))
        Function ExecuteReader(Of T As New)(commandText As String, parameters As IDictionary(Of String, Object)) As Task(Of IEnumerable(Of T))

        Function ExecuteReaderSingleColumn(Of T)(commandText As String) As Task(Of IEnumerable(Of T))
        Function ExecuteReaderSingleColumn(Of T)(commandText As String, parameters As IDictionary(Of String, Object)) As Task(Of IEnumerable(Of T))

        Function ExecuteScalar(commandText As String) As Task(Of Object)
        Function ExecuteScalar(commandText As String, parameters As IDictionary(Of String, Object)) As Task(Of Object)

        Function ExecuteScalar(Of T)(commandText As String) As Task(Of T)
        Function ExecuteScalar(Of T)(commandText As String, parameters As IDictionary(Of String, Object)) As Task(Of T)

        Function GetDataSet(commandText As String) As Task(Of DataSet)
        Function GetDataSet(commandText As String, parameters As IDictionary(Of String, Object)) As Task(Of DataSet)

        Function GetDataTable(commandText As String) As Task(Of DataTable)
        Function GetDataTable(commandText As String, parameters As IDictionary(Of String, Object)) As Task(Of DataTable)

        Function GetSchema(tableName As String) As Task(Of Dictionary(Of String, String))
        Function GetSchema(selectAllSql As String, tableName As String) As Task(Of Dictionary(Of String, String))

        Function Merge(createSQL As String, dropSQL As String, dataTable As DataTable, mergeSQL As String, tableName As String) As Task

    End Interface

End Namespace