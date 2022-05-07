Imports System.ComponentModel

Namespace CommonRoutines.Models

    <Runtime.Serialization.DataContract> Public MustInherit Class GenericChanged
        Implements INotifyPropertyChanged

        Protected Friend Sub SetProperty(Of T)(ByRef field As T, value As T, name As String)
            SetProperty(field, value, name, {})
        End Sub

        Protected Friend Sub SetProperty(Of T)(ByRef field As T, value As T, name As String, otherProperties() As String)
            If Not EqualityComparer(Of T).Default.Equals(field, value) Then
                field = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
                For Each PropertyName As String In otherProperties
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(PropertyName))
                Next
            End If
        End Sub

        Protected Friend Sub RaisePropertyChanged(name As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
        End Sub

#Region " INotifyPropertyChanged "

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#End Region

    End Class

End Namespace