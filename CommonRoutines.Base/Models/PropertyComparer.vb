Imports System.ComponentModel

Namespace Models

    Public Class PropertyComparer(Of T)
        Implements IComparer(Of T)

        Private ReadOnly _PropertyDescriptor As PropertyDescriptor = Nothing
        Private ReadOnly _Direction As ListSortDirection = ListSortDirection.Ascending

        Public Sub New(propertyDescriptor As PropertyDescriptor, direction As ListSortDirection)
            _PropertyDescriptor = propertyDescriptor
            _Direction = direction
        End Sub

        Private Function CompareAscending(x As Object, y As Object) As Integer
            If x Is Nothing Then
                If y Is Nothing Then
                    Return 0
                Else
                    Return -1
                End If
            ElseIf y Is Nothing Then
                Return 1
            End If

            If TypeOf x Is IComparable Then
                Return CType(x, IComparable).CompareTo(y)
            ElseIf TypeOf y Is IComparable Then
                Return CType(y, IComparable).CompareTo(x)
            ElseIf x.Equals(y) Then
                Return 0
            Else
                Return x.ToString().CompareTo(y.ToString())
            End If
        End Function

        Private Function CompareDescending(x As Object, y As Object) As Integer
            Return (CompareAscending(x, y) * -1)
        End Function

#Region " IComparer "

        Public Function Compare(x As T, y As T) As Integer Implements IComparer(Of T).Compare
            Dim XValue As Object = _PropertyDescriptor.GetValue(x)
            Dim YValue As Object = _PropertyDescriptor.GetValue(y)

            If _Direction = ListSortDirection.Ascending Then
                Return CompareAscending(XValue, YValue)
            Else
                Return CompareDescending(XValue, YValue)
            End If
        End Function

        Public Overloads Function Equals(x As T, y As T) As Boolean
            Return x.Equals(y)
        End Function

        Public Overloads Function GetHashCode(item As T) As Integer
            Return item.GetHashCode()
        End Function

#End Region

    End Class

End Namespace