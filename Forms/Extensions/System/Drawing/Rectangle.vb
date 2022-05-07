Namespace CommonRoutines.Extensions

    Public Module System_Drawing_Rectangle

        <Runtime.CompilerServices.Extension()> Public Function IsEqualTo(i As Rectangle, value As Rectangle) As Boolean
            Return i.Equals(value)
        End Function

        <Runtime.CompilerServices.Extension()> Public Function IsOnScreen(rectangle As Rectangle) As Boolean
            Return Screen.AllScreens.Any(Function(o) o.WorkingArea.IntersectsWith(rectangle))
        End Function

    End Module

End Namespace