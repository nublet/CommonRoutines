Namespace Extensions

    Public Module CommonRoutines_Models_FormSizeLocation

        <Runtime.CompilerServices.Extension()> Public Function GetRectangle(ByRef f As Models.FormSizeLocation) As Drawing.Rectangle
            Return New Drawing.Rectangle(f.Left, f.Top, f.Width, f.Height)
        End Function

    End Module

End Namespace