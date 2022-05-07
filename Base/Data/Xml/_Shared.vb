Namespace CommonRoutines.Data.Xml

    Public Module _Shared

        Public Function Load(Of TValue)(fileName As String) As List(Of TValue)
            Try
                If Not fileName.FileExists() Then
                    Return New List(Of TValue)
                End If

                Dim Serializer As New System.Xml.Serialization.XmlSerializer(GetType(List(Of TValue)))

                Using sr As New IO.StreamReader(fileName, Text.Encoding.UTF8)
                    Dim Value As Object = Serializer.Deserialize(sr)

                    If Value IsNot Nothing Then
                        Return DirectCast(Value, List(Of TValue))
                    End If
                End Using
            Catch ex As Exception
                ex.ToLog(True)
            End Try

            Return New List(Of TValue)
        End Function

        Public Function LoadSingle(Of TValue)(fileName As String) As TValue
            Try
                If Not fileName.FileExists() Then
                    Return Nothing
                End If

                Dim Serializer As New System.Xml.Serialization.XmlSerializer(GetType(TValue))

                Using sr As New IO.StreamReader(fileName, Text.Encoding.UTF8)
                    Dim Value As Object = Serializer.Deserialize(sr)
                    If Value IsNot Nothing Then
                        Return DirectCast(Value, TValue)
                    End If
                End Using
            Catch ex As Exception
                ex.ToLog(True)
            End Try

            Return Nothing
        End Function

        Public Sub Save(Of TValue)(fileName As String, ByRef values As List(Of TValue))
            Try
                Dim Serializer As New System.Xml.Serialization.XmlSerializer(GetType(List(Of TValue)))

                Using sw As New IO.StreamWriter(fileName, False, System.Text.Encoding.UTF8)
                    Serializer.Serialize(sw, values)
                    sw.Flush()
                End Using
            Catch ex As Exception
                ex.ToLog(True)
            End Try
        End Sub

        Public Sub SaveSingle(Of TValue)(fileName As String, ByRef values As List(Of TValue))
            Try
                If fileName.FileExists() Then
                    IO.File.Delete(fileName)
                End If

                Dim Serializer As New System.Xml.Serialization.XmlSerializer(GetType(List(Of TValue)))

                Using sw As New IO.StreamWriter(fileName, False, System.Text.Encoding.UTF8)
                    Serializer.Serialize(sw, values)
                    sw.Flush()
                End Using
            Catch ex As Exception
                ex.ToLog(True)
            End Try
        End Sub

    End Module

End Namespace