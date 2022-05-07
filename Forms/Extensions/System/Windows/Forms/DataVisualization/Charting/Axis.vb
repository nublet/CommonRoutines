Namespace CommonRoutines.Extensions

    Public Module System_Windows_Forms_DataVisualization_Charting_Axis

        <Runtime.CompilerServices.Extension()> Public Sub SetTheme(ByRef a As DataVisualization.Charting.Axis)
            If a.Enabled = DataVisualization.Charting.AxisEnabled.True Then
                a.LabelStyle.ForeColor = UITheme.ForeColor
                a.LineColor = UITheme.LineColor
                a.MajorGrid.LineColor = UITheme.LineColor
                a.MajorTickMark.LineColor = UITheme.LineColor
            End If
        End Sub

    End Module

End Namespace