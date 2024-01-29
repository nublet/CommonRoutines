Namespace Extensions

    Public Module CommonRoutines_UserControls_ComboBox

        <Runtime.CompilerServices.Extension()> Public Function GetSelectedComboItemValue(ByRef c As UserControls.ComboBox) As String
            If c.Items.Count <= 0 Then
                If c.Text.IsSet() Then
                    Return c.Text
                Else
                    Return ""
                End If
            End If

            If c.SelectedItem Is Nothing Then
                If c.Text.IsSet() Then
                    Return c.Text
                Else
                    Return ""
                End If
            End If

            If Not (TypeOf c.Items(0) Is Models.ComboItem) Then
                Return ""
            End If

            Return DirectCast(c.SelectedItem, Models.ComboItem).Value
        End Function

        <Runtime.CompilerServices.Extension()> Private Sub PopulateComboItems(ByRef c As UserControls.ComboBox, items As IEnumerable(Of Models.ComboItem))
            Dim CurrentValue As String = c.GetSelectedComboItemValue()

            c.DisplayMember = "Display"
            c.ValueMember = "Value"
            c.Sorted = False
            c.SelectedItem = Nothing
            c.BeginUpdate()
            c.Items.Clear()
            c.Items.AddRange(items.ToArray())
            If CurrentValue.IsSet() Then
                Dim CI As Models.ComboItem = items.Where(Function(o) o.Display.IsEqualTo(CurrentValue) OrElse o.Value.IsEqualTo(CurrentValue)).FirstOrDefault()
                If CI IsNot Nothing Then
                    c.SelectedItem = CI
                End If
            End If
            c.EndUpdate()
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub PopulateComboItems(ByRef c As UserControls.ComboBox, items As IEnumerable(Of String))
            c.PopulateComboItems(Nothing, items)
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub PopulateComboItems(ByRef c As UserControls.ComboBox, fixedItems As IEnumerable(Of String), items As IEnumerable(Of String))
            Dim ComboItems As New List(Of Models.ComboItem)

            If fixedItems IsNot Nothing AndAlso fixedItems.Any() Then
                For Each item As String In fixedItems
                    ComboItems.Add(New Models.ComboItem(item))
                Next
            End If

            If items IsNot Nothing AndAlso items.Any() Then
                For Each item As String In items.Distinct()
                    ComboItems.Add(New Models.ComboItem(item))
                Next
            End If

            c.PopulateComboItems(ComboItems)
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub PopulateComboItems(ByRef c As UserControls.ComboBox, items As IDictionary(Of String, String))
            c.PopulateComboItems(Nothing, items)
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub PopulateComboItems(ByRef c As UserControls.ComboBox, fixedItems As IDictionary(Of String, String), items As IDictionary(Of String, String))
            Dim ComboItems As New List(Of Models.ComboItem)
            If fixedItems IsNot Nothing AndAlso fixedItems.Count > 0 Then
                For Each kvp As KeyValuePair(Of String, String) In fixedItems
                    ComboItems.Add(New Models.ComboItem(kvp.Key, kvp.Value))
                Next
            End If
            If items IsNot Nothing AndAlso items.Count > 0 Then
                For Each kvp As KeyValuePair(Of String, String) In items.Distinct()
                    ComboItems.Add(New Models.ComboItem(kvp.Key, kvp.Value))
                Next
            End If
            c.PopulateComboItems(ComboItems)
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub SelectComboItem(ByRef c As UserControls.ComboBox, item As String)
            c.SelectComboItem(item, True)
        End Sub

        <Runtime.CompilerServices.Extension()> Public Sub SelectComboItem(ByRef c As UserControls.ComboBox, item As String, autoAdd As Boolean)
            c.SelectedItem = Nothing

            If item.IsNotSet() Then
                Return
            End If

            If c.Items.Count > 0 Then
                If Not (TypeOf c.Items(0) Is Models.ComboItem) Then
                    Return
                End If

                For Each ComboItem As Models.ComboItem In c.Items
                    If ComboItem.Value.IsEqualTo(item) Then
                        c.SelectedItem = ComboItem
                        Exit For
                    End If
                    If ComboItem.Display.IsEqualTo(item) Then
                        c.SelectedItem = ComboItem
                        Exit For
                    End If
                Next
            End If

            If c.SelectedItem Is Nothing AndAlso autoAdd Then
                c.DisplayMember = "Display"
                c.ValueMember = "Value"

                Dim ComboItem As New Models.ComboItem(item)
                c.BeginUpdate()
                c.Items.Add(ComboItem)
                c.SelectedItem = ComboItem
                c.EndUpdate()
            End If
        End Sub

    End Module

End Namespace