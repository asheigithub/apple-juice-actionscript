Namespace AS3
    Public Class AS3YieldReturn
        Implements IAS3Stmt

        Private matchtoken As Token
        Public Sub New(token As Token)
            Me.matchtoken = token
        End Sub

        Public ReadOnly Property Token As Token Implements IAS3Stmt.Token
            Get
                Return matchtoken
            End Get
        End Property

        Public ReturnValue As AS3StmtExpressions

        Public Sub Write(tabs As Integer, srcout As ISrcOut) Implements IAS3Stmt.Write
            If Not ReturnValue Is Nothing Then
                ReturnValue.Write(tabs, srcout)
                srcout.WriteLn("yield return " & ReturnValue.as3exprlist(ReturnValue.as3exprlist.Count - 1).Value.ToString() & ";", tabs)
            Else
                srcout.WriteLn("yield return;", tabs)
            End If
        End Sub
    End Class
End Namespace

