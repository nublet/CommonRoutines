Namespace CommonRoutines.Extensions

    Public Module System_String

        <Runtime.CompilerServices.Extension()> Public Function IsPlural(s As String) As Boolean
            Return Settings.GetPluralizationService().IsPlural(s)
        End Function

        <Runtime.CompilerServices.Extension()> Public Function IsSingular(s As String) As Boolean
            Return Settings.GetPluralizationService().IsSingular(s)
        End Function

        <Runtime.CompilerServices.Extension()> Public Function Pluralize(s As String) As String
            Return Settings.GetPluralizationService().Pluralize(s)
        End Function

        <Runtime.CompilerServices.Extension()> Public Function Singularize(s As String) As String
            Return Settings.GetPluralizationService().Singularize(s)
        End Function

    End Module

End Namespace