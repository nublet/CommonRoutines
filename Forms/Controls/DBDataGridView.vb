Namespace CommonRoutines.Controls

    Public Class DBDataGridView
        Inherits DataGridView

        Public Sub New()
            MyBase.New()
            DoubleBuffered = True
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            e.Graphics.Clear(BackgroundColor)
            MyBase.OnPaint(e)
        End Sub

    End Class

End Namespace