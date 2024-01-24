Namespace Extensions

    Public Module System_Type

        <Runtime.CompilerServices.Extension()> Public Function GetDefault(t As System.Type) As Object
            If t Is Nothing OrElse Not t.IsValueType OrElse t.Equals(GetType(Void)) Then
                Return Nothing
            End If

            If t.ContainsGenericParameters Then
                Return Nothing
            End If

            If t.IsPrimitive OrElse Not t.IsNotPublic Then
                Try
                    Return Activator.CreateInstance(t)
                Catch ex As Exception
                    Return Nothing
                End Try
            End If

            Return Nothing
        End Function

        <Runtime.CompilerServices.Extension()> Public Function IsDefault(t As System.Type, value As Object) As Boolean
            If t Is Nothing Then
                If value Is Nothing Then
                    Return True
                End If

                t = value.GetType()
            End If

            Dim DefaultValue As Object = t.GetDefault

            If value IsNot Nothing Then
                Return value.Equals(DefaultValue)
            End If

            Return (DefaultValue Is Nothing)
        End Function

    End Module

End Namespace