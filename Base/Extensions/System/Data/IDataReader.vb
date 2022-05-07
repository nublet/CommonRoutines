Namespace CommonRoutines.Extensions

    Public Module System_Data_IDataReader

        <Runtime.CompilerServices.Extension()> Public Function ReadItems(Of T As New)(r As IDataReader) As IEnumerable(Of T)
            Dim Results As New List(Of T)

            Dim Fields As Dictionary(Of Integer, Reflection.IPropertyAccessor) = Reflection.GetFieldList(Of T)(r)

            While r.Read
                Dim Entity As New T()

                For Each kvp As KeyValuePair(Of Integer, Reflection.IPropertyAccessor) In Fields
                    If r.IsDBNull(kvp.Key) Then
                        kvp.Value.SetValue(Entity, Type.GetValue(Nothing, kvp.Value.PropertyInfo.PropertyType.Name))
                    Else
                        kvp.Value.SetValue(Entity, Type.GetValue(r(kvp.Key), kvp.Value.PropertyInfo.PropertyType.Name))
                    End If
                Next

                Results.Add(Entity)
            End While

            Return Results
        End Function

        <Runtime.CompilerServices.Extension()> Public Function ReadItemsSingleColumn(Of T)(r As IDataReader) As IEnumerable(Of T)
            Dim Results As New List(Of T)

            While r.Read
                If r.IsDBNull(0) Then
                    Results.Add(Type.GetValue(Of T)(Nothing))
                Else
                    Results.Add(Type.GetValue(Of T)(r(0)))
                End If
            End While

            Return Results
        End Function

    End Module

End Namespace