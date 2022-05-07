Namespace CommonRoutines.Data

    Public Module _Shared

        Public Function GetDBParameter(obj As Object) As Object
            If TypeOf obj Is DateTime OrElse TypeOf obj Is Date Then
                Dim oDate As DateTime = DirectCast(obj, DateTime)
                If oDate.ToOADate = 0.0! Then
                    Return DBNull.Value
                ElseIf Type.IsNullOrDefault(obj) Then
                    Return DBNull.Value
                Else
                    Return obj
                End If
            ElseIf TypeOf obj Is String AndAlso obj.ToString().IsNotSet() Then
                Return ""
            ElseIf Type.IsNull(obj) Then
                Return DBNull.Value
            Else
                Return obj
            End If
        End Function

        Friend Sub CheckFieldType(field As String, fieldType As String, ByRef results As Dictionary(Of String, String))
            If field.IsNotSet() Then
                Return
            End If

            Select Case fieldType.ToLower
                Case "boolean", "bit"
                    fieldType = "Boolean"
                Case "date", "datetime"
                    fieldType = "DateTime"
                Case "integer", "int"
                    fieldType = "Integer"
                Case "string", "varchar", "nvarchar"
                    fieldType = "String"
                Case Else
                    System_String.ToLog("Unhandled Field Data Type: {0}".FormatWith(fieldType.ToLower), True)
            End Select

            results.Add(field, fieldType)
        End Sub

    End Module

End Namespace