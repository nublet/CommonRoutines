Namespace Extensions

    Public Module System_Date

        <Runtime.CompilerServices.Extension()> Public Function IsEqualTo(d As Date, value As Date) As Boolean
            Return d.Equals(value)
        End Function

        <Runtime.CompilerServices.Extension()> Public Function IsNotEqualTo(d As Date, value As Date) As Boolean
            Return Not d.IsEqualTo(value)
        End Function

        <Runtime.CompilerServices.Extension()> Public Function GetRelative(d As Date) As String
            Dim CurrentDate As Date = GetCurrentDate()

            If d = Date.MinValue Then
                Return ""
            ElseIf CurrentDate.Year = d.Year AndAlso CurrentDate.Month = d.Month AndAlso CurrentDate.Day = d.Day Then
                Return d.GetSQLString("HH:mm")
            ElseIf CurrentDate.Year = d.Year Then
                Return d.GetSQLString("MMM dd")
            Else
                Return d.GetSQLString("dd/MM/yy")
            End If
        End Function

        <Runtime.CompilerServices.Extension()> Public Function GetSQLString(d As Date) As String
            Return d.GetSQLString("")
        End Function

        <Runtime.CompilerServices.Extension()> Public Function GetSQLString(d As Date?) As String
            Return d.GetSQLString("")
        End Function

        <Runtime.CompilerServices.Extension()> Public Function GetSQLString(d As Date, dateFormat As String) As String
            If d.Equals(Date.MinValue) Then
                Return ""
            End If

            If dateFormat.IsNotSet() Then
                dateFormat = Settings.DateFormat
            End If
            If dateFormat.IsEqualTo("Short Date") Then
                dateFormat = GetCurrentCulture().DateTimeFormat.ShortDatePattern
            ElseIf dateFormat.IsEqualTo("Short Date +sme") Then
                dateFormat = "{0} {1}".FormatWith(GetCurrentCulture().DateTimeFormat.ShortDatePattern, GetCurrentCulture().DateTimeFormat.ShortTimePattern)
            ElseIf dateFormat.IsEqualTo("Long Date") Then
                dateFormat = GetCurrentCulture().DateTimeFormat.LongDatePattern
            ElseIf dateFormat.IsEqualTo("Long Date + Time") Then
                dateFormat = "{0} {1}".FormatWith(GetCurrentCulture().DateTimeFormat.LongDatePattern, GetCurrentCulture().DateTimeFormat.LongTimePattern)
            End If

            Return d.ToString(dateFormat, GetCurrentCulture())
        End Function

        <Runtime.CompilerServices.Extension()> Public Function GetSQLString(d As Date?, dateFormat As String) As String
            If d.HasValue Then
                Return d.Value.GetSQLString(dateFormat)
            End If

            Return ""
        End Function

    End Module

End Namespace