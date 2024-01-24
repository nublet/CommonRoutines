Namespace Extensions

    Public Module System_Version

        <Runtime.CompilerServices.Extension()> Public Function GetVersionLong(v As Version) As Long
            Return v.GetVersionString().GetVersionLong()
        End Function

        <Runtime.CompilerServices.Extension()> Public Function GetVersionString(v As Version) As String
            Return "{0}.{1}.{2}.{3}".FormatWith(v.Major, v.Minor, v.Build, v.Revision)
        End Function

    End Module

End Namespace