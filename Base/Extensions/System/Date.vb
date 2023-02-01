Namespace CommonRoutines.Extensions

    Public Module System_Date

        <Runtime.CompilerServices.Extension()> Public Function IsEqualTo(d As Date, value As Date) As Boolean
            Return d.Equals(value)
        End Function

        <Runtime.CompilerServices.Extension()> Public Function IsNotEqualTo(d As Date, value As Date) As Boolean
            Return Not d.IsEqualTo(value)
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