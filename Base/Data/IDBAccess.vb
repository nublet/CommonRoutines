Namespace CommonRoutines.Data

    Public Interface IDBAccess

        ReadOnly Property ConnectionString As String

        Function ExecuteReader(Of T As New)(commandText As String) As IEnumerable(Of T)
        Function ExecuteReader(Of T As New)(commandText As String, parameters As IDictionary(Of String, Object)) As IEnumerable(Of T)

        Function ExecuteReaderSingleColumn(Of T)(commandText As String) As IEnumerable(Of T)
        Function ExecuteReaderSingleColumn(Of T)(commandText As String, parameters As IDictionary(Of String, Object)) As IEnumerable(Of T)

        Function ExecuteScalar(commandText As String) As Object
        Function ExecuteScalar(commandText As String, parameters As IDictionary(Of String, Object)) As Object

        Function ExecuteScalar(Of T)(commandText As String) As T
        Function ExecuteScalar(Of T)(commandText As String, parameters As IDictionary(Of String, Object)) As T

        Function GetDataSet(commandText As String) As DataSet
        Function GetDataSet(commandText As String, parameters As IDictionary(Of String, Object)) As DataSet

        Function GetDataTable(commandText As String) As DataTable
        Function GetDataTable(commandText As String, parameters As IDictionary(Of String, Object)) As DataTable

        Function GetSchema(tableName As String) As Dictionary(Of String, String)
        Function GetSchema(selectAllSql As String, tableName As String) As Dictionary(Of String, String)

        Sub BulkInsert(dataTable As DataTable, tableName As String)

        Sub ExecuteNonQuery(commandText As String)
        Sub ExecuteNonQuery(commandText As String, parameters As IDictionary(Of String, Object))

        Sub Merge(createSQL As String, dropSQL As String, dataTable As DataTable, mergeSQL As String, tableName As String)

    End Interface

End Namespace