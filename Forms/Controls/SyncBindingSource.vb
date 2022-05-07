Imports System.ComponentModel

Namespace CommonRoutines.Controls

    Public Class SyncBindingSource
        Inherits BindingSource

        Public Sub New()

        End Sub

        Protected Overrides Sub OnListChanged(e As ListChangedEventArgs)
            If Settings.SynchronizationContext Is Nothing Then
                MyBase.OnListChanged(e)
            Else
                Settings.SynchronizationContext.Send(Sub()
                                                         MyBase.OnListChanged(e)
                                                     End Sub, Nothing)
            End If
        End Sub

    End Class

End Namespace