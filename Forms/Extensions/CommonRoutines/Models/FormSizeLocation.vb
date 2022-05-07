Namespace CommonRoutines.Extensions

    Public Module CommonRoutines_Models_FormSizeLocation

        <Runtime.CompilerServices.Extension()> Public Function GetRectangle(ByRef f As Models.FormSizeLocation) As Rectangle
            Return New Rectangle(f.Left, f.Top, f.Width, f.Height)
        End Function

    End Module

End Namespace