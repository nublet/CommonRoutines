Namespace CommonRoutines.Settings

    Public Module _Shared

        Private _PluralizationService As Entity.Design.PluralizationServices.PluralizationService = Nothing

        Public Property ForceFormBackColor As Boolean = False
        Public Property ForceIcon As Boolean = True
        Public Property Icon As Icon = My.Resources.INFO

        Public Function GetPluralizationService() As Entity.Design.PluralizationServices.PluralizationService
            If _PluralizationService Is Nothing Then
                _PluralizationService = Entity.Design.PluralizationServices.PluralizationService.CreateService(GetCurrentCulture())
            End If

            Return _PluralizationService
        End Function

    End Module

End Namespace