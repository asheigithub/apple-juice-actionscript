Namespace AS3
    Public Class AS3Access
        '"public" |"private"   | "internal" |"final" |"dynamic"
        '"public" |"private"| "internal" |"final"|"dynamic" |"static" |"override" |"protected"

        Public IsPublic As Boolean
        Public IsPrivate As Boolean
        Public IsInternal As Boolean
        Public IsFinal As Boolean
        Public IsDynamic As Boolean
        Public IsStatic As Boolean
        Public IsOverride As Boolean
        Public IsProtected As Boolean

        Public Overrides Function ToString() As String
            Dim result As String = ""

            If IsPublic Then
                result = result & "public "
            ElseIf IsPrivate Then
                result = result & "private "
            ElseIf IsInternal Then
                result = result & "internal "
            End If

            If IsFinal Then
                result = result & "final "
            End If
            If IsDynamic Then
                result = result & "dynamic "
            End If
            If IsStatic Then
                result = result & "static "
            End If
            If IsOverride Then
                result = result & "override "
            End If
            If IsProtected Then
                result = result & "protected "
            End If

            Return result
        End Function

        Public Sub SetValue(accessStack As Stack(Of String))
            IsPublic = False
            IsPrivate = False
            IsInternal = False
            IsFinal = False
            IsDynamic = False
            IsStatic = False
            IsProtected = False
            IsStatic = False

            While accessStack.Count > 0
                Dim a = accessStack.Pop()

                If a = "public" Then ' |"private"   | "internal" |"final" |"dynamic"
                    IsPublic = True
                ElseIf a = "private" Then ' |"private"   | "internal" |"final" |"dynamic"
                    IsPrivate = True
                ElseIf a = "internal" Then ' |"private"   | "internal" |"final" |"dynamic"
                    IsInternal = True
                ElseIf a = "final" Then ' |"private"   | "internal" |"final" |"dynamic"
                    IsFinal = True
                ElseIf a = "dynamic" Then ' |"private"   | "internal" |"final" |"dynamic"
                    IsDynamic = True
                ElseIf a = "static" Then ' |"static" |"override" |"protected"
                    IsStatic = True
                ElseIf a = "override" Then ' |"static" |"override" |"protected"
                    IsOverride = True
                ElseIf a = "protected" Then ' |"static" |"override" |"protected"
                    IsProtected = True
                End If

            End While
        End Sub

    End Class
End Namespace

