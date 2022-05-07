Imports System.ComponentModel

Namespace CommonRoutines.Data

    Public Class SortableSearchableBindingList(Of T)
        Inherits BindingList(Of T)
        Implements IBindingListView
        Implements IRaiseItemChangedEvents

        Public Sub AddRange(entities As IEnumerable(Of T))
            For Each Current As T In entities
                Add(Current)
            Next
        End Sub

#Region " Searching "

        Protected Overrides ReadOnly Property SupportsSearchingCore As Boolean
            Get
                Return True
            End Get
        End Property

        Protected Overrides Function FindCore(propertyDescriptor As PropertyDescriptor, value As Object) As Integer
            Return FindCore(0, propertyDescriptor, value)
        End Function

        Protected Overloads Function FindCore(startIndex As Integer, propertyDescriptor As PropertyDescriptor, value As Object) As Integer
            If propertyDescriptor Is Nothing Then
                Return -1
            End If

            If Type.IsNull(value) Then
                Return -1
            End If

            For I As Integer = startIndex To Count - 1
                Dim CurrentItem As T = Items(I)

                If propertyDescriptor.GetValue(CurrentItem).Equals(value) Then
                    Return I
                End If
            Next

            Return -1
        End Function

        Public Function Find(propertyName As String, value As Object) As Integer
            Return Find(0, propertyName, value)
        End Function

        Public Function Find(startIndex As Integer, propertyName As String, value As Object) As Integer
            If propertyName.IsNotSet() Then
                Return -1
            End If

            If Type.IsNull(value) Then
                Return -1
            End If

            Dim PD As PropertyDescriptor = TypeDescriptor.GetProperties(GetType(T)).Find(propertyName, True)

            If PD Is Nothing Then
                Return -1
            End If

            If startIndex <= 0 Then
                Return FindCore(PD, value)
            Else
                Return FindCore(startIndex, PD, value)
            End If
        End Function

#End Region

#Region " Sorting "

        Private _IsSorted As Boolean = False
        Private _SortDescriptions As ListSortDescriptionCollection
        Private _SortDirection As ListSortDirection = ListSortDirection.Ascending
        Private _SortProperty As PropertyDescriptor = Nothing

        Private ReadOnly _Comparers As New List(Of Models.PropertyComparer(Of T))

        Protected Overrides ReadOnly Property IsSortedCore As Boolean
            Get
                Return _IsSorted
            End Get
        End Property

        Protected Overrides ReadOnly Property SortDirectionCore As ListSortDirection
            Get
                Return _SortDirection
            End Get
        End Property

        Protected Overrides ReadOnly Property SortPropertyCore As PropertyDescriptor
            Get
                Return _SortProperty
            End Get
        End Property

        Protected Overrides ReadOnly Property SupportsSortingCore As Boolean
            Get
                Return True
            End Get
        End Property

        Private Function CompareValuesByProperties(x As T, y As T) As Integer
            If x Is Nothing Then
                If y Is Nothing Then
                    Return 0
                Else
                    Return -1
                End If
            ElseIf y Is Nothing Then
                Return 1
            End If

            For Each Current As Models.PropertyComparer(Of T) In _Comparers
                Dim Result As Integer = Current.Compare(x, y)

                If Result.IsNotEqualTo(0) Then
                    Return Result
                End If
            Next

            Return 0
        End Function

        Private Sub SortMe()
            RaiseListChangedEvents = False

            Dim ListRef As List(Of T) = TryCast(Items, List(Of T))
            If ListRef Is Nothing Then
                Return
            End If

            ListRef.Sort(AddressOf CompareValuesByProperties)

            _IsSorted = True

            RaiseListChangedEvents = True
            OnListChanged(New ListChangedEventArgs(ListChangedType.Reset, -1))
        End Sub

        Protected Overrides Sub ApplySortCore(propertyDescriptor As PropertyDescriptor, direction As ListSortDirection)
            _SortDirection = direction
            _SortProperty = propertyDescriptor

            _Comparers.Clear()
            _Comparers.Add(New Models.PropertyComparer(Of T)(propertyDescriptor, direction))

            SortMe()
        End Sub

        Protected Overrides Sub RemoveSortCore()
            If Not _IsSorted Then
                Return
            End If

            _IsSorted = False
            _SortDescriptions = Nothing
            _SortProperty = Nothing

            OnListChanged(New ListChangedEventArgs(ListChangedType.Reset, -1))
        End Sub

        Public Sub ApplySort()
            If Not _IsSorted Then
                Return
            End If

            If _SortDescriptions Is Nothing OrElse _SortDescriptions.Count <= 0 Then
                If _SortProperty Is Nothing Then
                    Return
                End If

                ApplySortCore(_SortProperty, _SortDirection)
            Else
                ApplySort(_SortDescriptions)
            End If
        End Sub

        Public Sub ApplySort(propertyName As String, sortDirection As ListSortDirection)
            If propertyName.IsNotSet() Then
                Return
            End If

            Dim PD As PropertyDescriptor = TypeDescriptor.GetProperties(GetType(T)).Find(propertyName, True)
            If PD Is Nothing Then
                Return
            End If

            ApplySortCore(PD, sortDirection)
        End Sub

        Public Sub RemoveSort()
            RemoveSortCore()
        End Sub

#End Region

#Region " IBindingListView "

        Public Property Filter As String Implements IBindingListView.Filter
            Get
                Throw New NotImplementedException()
            End Get
            Set(value As String)
                Throw New NotImplementedException()
            End Set
        End Property

        Public ReadOnly Property SortDescriptions As ListSortDescriptionCollection Implements IBindingListView.SortDescriptions
            Get
                Return _SortDescriptions
            End Get
        End Property

        Public ReadOnly Property SupportsAdvancedSorting As Boolean Implements IBindingListView.SupportsAdvancedSorting
            Get
                Return True
            End Get
        End Property

        Public ReadOnly Property SupportsFiltering As Boolean Implements IBindingListView.SupportsFiltering
            Get
                Return True
            End Get
        End Property

        Public Sub ApplySort(listSortDescriptions As ListSortDescriptionCollection) Implements IBindingListView.ApplySort
            _SortProperty = Nothing
            _SortDescriptions = listSortDescriptions

            _Comparers.Clear()

            For Each Current As ListSortDescription In _SortDescriptions
                _Comparers.Add(New Models.PropertyComparer(Of T)(Current.PropertyDescriptor, Current.SortDirection))
            Next

            SortMe()
        End Sub

        Public Sub RemoveFilter() Implements IBindingListView.RemoveFilter
            Throw New NotImplementedException()
        End Sub

#End Region

    End Class

End Namespace