Namespace AS3
    ''' <summary>
    ''' AS3表达式
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AS3Expression
        Public exprStepList As AS3.Expr.AS3ExprStepList

        Public Value As AS3.Expr.AS3DataStackElement


        Public Overridable Sub Write(tabs As Integer, srcout As ISrcOut)

            For Each k In exprStepList
                srcout.WriteLn(k.ToString(), tabs)
            Next

        End Sub

        Public Overrides Function ToString() As String

            If Not Value Is Nothing Then
                Return Value.ToString()
            End If

            Return ""

        End Function

    End Class
End Namespace

