Imports System.Data

Namespace Reflection

    Public Module _Shared

        Private ReadOnly _Cache As New List(Of System.Type)
        Private ReadOnly _Fields As New Dictionary(Of String, List(Of IFieldAccessor))
        Private ReadOnly _Properties As New Dictionary(Of String, List(Of IPropertyAccessor))

        Public Function CreateAccessor(ByRef fieldInfo As System.Reflection.FieldInfo) As IFieldAccessor
            Return DirectCast(Activator.CreateInstance(GetType(FieldWrapper(Of ,)).MakeGenericType(fieldInfo.DeclaringType, fieldInfo.FieldType), fieldInfo), IFieldAccessor)
        End Function

        Public Function CreateAccessor(ByRef propertyInfo As System.Reflection.PropertyInfo) As IPropertyAccessor
            Return DirectCast(Activator.CreateInstance(GetType(PropertyWrapper(Of ,)).MakeGenericType(propertyInfo.DeclaringType, propertyInfo.PropertyType), propertyInfo), IPropertyAccessor)
        End Function

        Public Function GetFieldList(reader As IDataReader, obj As Object) As Dictionary(Of Integer, IPropertyAccessor)
            Return GetFieldList(reader, obj.GetType())
        End Function

        Public Function GetFieldList(Of T)(reader As IDataReader) As Dictionary(Of Integer, IPropertyAccessor)
            Return GetFieldList(reader, GetType(T))
        End Function

        Public Function GetFieldList(reader As IDataReader, type As System.Type) As Dictionary(Of Integer, IPropertyAccessor)
            Dim Properties = GetProperties(True, True, type)
            Dim Results As New Dictionary(Of Integer, IPropertyAccessor)

            For I As Integer = 0 To reader.FieldCount - 1
                Dim FieldName As String = reader.GetName(I).ReplaceInvalidCharacters("_")

                Dim PropertyAccessor As IPropertyAccessor = Properties.Where(Function(o) o.Name.IsEqualTo(FieldName)).FirstOrDefault()
                If PropertyAccessor Is Nothing Then
                    Continue For
                End If

                Results.Add(I, PropertyAccessor)
            Next

            Return Results
        End Function

        Public Function GetFields(obj As Object) As IEnumerable(Of IFieldAccessor)
            Return GetFields(obj.GetType())
        End Function

        Public Function GetFields(Of T)() As IEnumerable(Of IFieldAccessor)
            Return GetFields(GetType(T))
        End Function

        Public Function GetFields(type As System.Type) As IEnumerable(Of IFieldAccessor)
            Dim Key As String = type.FullName.ToLower()
            Dim Results As List(Of IFieldAccessor) = Nothing

            Try
                If Not _Fields.TryGetValue(Key, Results) Then
                    Results = type.GetFields(System.Reflection.BindingFlags.Public Or System.Reflection.BindingFlags.NonPublic Or System.Reflection.BindingFlags.Instance).OrderBy(Function(o) o.Name).Select(Function(o) CreateAccessor(o)).ToList()

                    _Fields.Add(Key, Results)
                End If
            Catch ex As Exception
            End Try

            Return Results
        End Function

        Public Function GetInstances(Of T)() As IEnumerable(Of T)
            If _Cache.Count <= 0 Then
                Dim AssemblyMatches As IEnumerable(Of System.Reflection.Assembly) = AppDomain.CurrentDomain.GetAssemblies()

                AssemblyMatches = AssemblyMatches.Where(Function(o) Not o.IsDynamic)
                AssemblyMatches = AssemblyMatches.Where(Function(o) Not o.Location.StartsWith("file://"))

                Dim TypeMatches As IEnumerable(Of System.Type) = AssemblyMatches.SelectMany(Function(o) o.GetTypes())

                TypeMatches = TypeMatches.Where(Function(o) o.IsClass)
                TypeMatches = TypeMatches.Where(Function(o) Not o.IsAbstract)

                _Cache.AddRange(TypeMatches)
            End If

            Dim RequestType As System.Type = GetType(T)

            Return _Cache.Where(Function(o) RequestType.IsAssignableFrom(o)).Select(Function(o) CType(Activator.CreateInstance(o), T))
        End Function

        Public Function GetProperties(canRead As Boolean, canWrite As Boolean, obj As Object) As IEnumerable(Of IPropertyAccessor)
            Return GetProperties(canRead, canWrite, obj.GetType())
        End Function

        Public Function GetProperties(Of T)(canRead As Boolean, canWrite As Boolean) As IEnumerable(Of IPropertyAccessor)
            Return GetProperties(canRead, canWrite, GetType(T))
        End Function

        Public Function GetProperties(canRead As Boolean, canWrite As Boolean, type As System.Type) As IEnumerable(Of IPropertyAccessor)
            If type Is Nothing Then
                Return New List(Of IPropertyAccessor)
            End If

            Dim Key As String = type.FullName.ToLower()
            Dim CachedValues As List(Of IPropertyAccessor) = Nothing

            If Not _Properties.TryGetValue(Key, CachedValues) Then
                CachedValues = New List(Of IPropertyAccessor)

                Try
                    Dim PropertyInfos As IEnumerable(Of System.Reflection.PropertyInfo) = type.GetProperties(System.Reflection.BindingFlags.Public Or System.Reflection.BindingFlags.NonPublic Or System.Reflection.BindingFlags.Instance)

                    If PropertyInfos?.Any() Then
                        Dim Matches = PropertyInfos.Where(Function(o) o.GetIndexParameters().Length <= 0).OrderBy(Function(o) o.Name)

                        CachedValues.AddRange(Matches.Select(Function(o) CreateAccessor(o)).Where(Function(o) o IsNot Nothing))
                    End If
                Catch ex As Exception
                    ex.ToLog(True)
                End Try

                _Properties.Add(Key, CachedValues)
            End If

            Dim Results As IEnumerable(Of IPropertyAccessor) = CachedValues

            If canRead Then
                Results = Results.Where(Function(o) o.CanRead)
            End If

            If canWrite Then
                Results = Results.Where(Function(o) o.CanWrite)
            End If

            Return Results
        End Function

        Public Function GetValue(propertyName As String, obj As Object) As Object
            Dim Properties = GetProperties(True, True, obj)

            Dim PropertyAccessor As IPropertyAccessor = Properties.Where(Function(o) o.Name.IsEqualTo(propertyName)).FirstOrDefault()
            If PropertyAccessor Is Nothing Then
                Return Nothing
            End If

            Return PropertyAccessor.GetValue(obj)
        End Function

        Public Function GetValue(Of T)(propertyName As String, obj As Object) As Object
            Dim Properties = GetProperties(Of T)(True, False)

            Dim PropertyAccessor As IPropertyAccessor = Properties.Where(Function(o) o.Name.IsEqualTo(propertyName)).FirstOrDefault()
            If PropertyAccessor Is Nothing Then
                Return Nothing
            End If

            Return PropertyAccessor.GetValue(obj)
        End Function

        Public Sub CopyObject(Of T)(source As Object, destination As T)
            If source Is Nothing Then
                Return
            End If

            If destination Is Nothing Then
                Return
            End If

            Dim DestinationProperties = GetProperties(Of T)(True, True)
            Dim SourceProperties = GetProperties(True, False, source)

            For Each Current As IPropertyAccessor In DestinationProperties
                Dim SourceProperty As IPropertyAccessor = SourceProperties.Where(Function(o) o.Name.IsEqualTo(Current.Name)).FirstOrDefault()

                If SourceProperty Is Nothing Then
                    Continue For
                End If

                Current.SetValue(destination, SourceProperty.GetValue(source))
            Next
        End Sub

        Public Sub CopyReader(Of T)(source As IDataReader, destination As T)
            If source Is Nothing Then
                Return
            End If

            If destination Is Nothing Then
                Return
            End If

            For Each kvp As KeyValuePair(Of Integer, IPropertyAccessor) In GetFieldList(source, destination)
                kvp.Value.SetValue(destination, Type.GetValue(source(kvp.Key), kvp.Value.PropertyInfo.PropertyType.Name))
            Next
        End Sub

        Public Sub SetValue(propertyName As String, obj As Object, value As Object)
            Dim Properties = GetProperties(True, True, obj)

            Dim PropertyAccessor As IPropertyAccessor = Properties.Where(Function(o) o.Name.IsEqualTo(propertyName)).FirstOrDefault()
            If PropertyAccessor Is Nothing Then
                Return
            End If

            PropertyAccessor.SetValue(obj, value)
        End Sub

        Public Sub SetValue(Of T)(propertyName As String, obj As Object, value As Object)
            Dim Properties = GetProperties(Of T)(True, False)

            Dim PropertyAccessor As IPropertyAccessor = Properties.Where(Function(o) o.Name.IsEqualTo(propertyName)).FirstOrDefault()
            If PropertyAccessor Is Nothing Then
                Return
            End If

            PropertyAccessor.SetValue(obj, value)
        End Sub

    End Module

End Namespace