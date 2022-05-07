Namespace CommonRoutines.Models

    <Xml.Serialization.XmlInclude(GetType(FormSizeLocation))>
    <Xml.Serialization.XmlInclude(GetType(List(Of String)))>
    <Serializable()> Public Class Setting

        Public Property Name As String = ""
        Public Property Value As Object = Nothing

        Public Sub New()

        End Sub

        Public Sub New(name As String)
            Me.Name = name
        End Sub

        Public Sub New(name As String, value As Object)
            Me.Name = name
            Me.Value = value
        End Sub

    End Class

End Namespace