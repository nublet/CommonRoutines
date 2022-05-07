Namespace CommonRoutines.Extensions

    Public Module System_Diagnostics_FileVersionInfo

        <Runtime.CompilerServices.Extension()> Public Function GetVersionLong(fileVersionInfo As FileVersionInfo) As Long
            Return fileVersionInfo.GetVersionString().GetVersionLong()
        End Function

        <Runtime.CompilerServices.Extension()> Public Function GetVersionString(fileVersionInfo As FileVersionInfo) As String
            Return "{0}.{1}.{2}.{3}".FormatWith(fileVersionInfo.ProductMajorPart, fileVersionInfo.ProductMinorPart, fileVersionInfo.ProductBuildPart, fileVersionInfo.ProductPrivatePart)
        End Function

    End Module

End Namespace