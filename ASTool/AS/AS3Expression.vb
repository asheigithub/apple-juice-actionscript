Namespace AS3
    ''' <summary>
    ''' AS3表达式
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AS3Expression
        Public exprStepList As AS3.Expr.AS3ExprStepList

        Public Value As AS3.Expr.AS3DataStackElement

        Public token As Token

        Public Sub New(token As Token)
            Me.token = token
        End Sub


        Public Overridable Sub Write(tabs As Integer, srcout As ISrcOut)
            If exprStepList.Count > 0 Then
                For Each k In exprStepList
                    srcout.WriteLn(k.ToString(), tabs)
                Next
            Else
                'srcout.WriteLn(ToString(), tabs)
            End If
        End Sub

        Public Overrides Function ToString() As String

            If Not Value Is Nothing Then
                Return Value.ToString()
            End If

            Return ""

        End Function

    End Class
End Namespace

