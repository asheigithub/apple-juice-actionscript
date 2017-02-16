Namespace AS3
    Public Class AS3IF
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
        Public Condition As AS3StmtExpressions

        Public TruePass As List(Of IAS3Stmt)

        Public FalsePass As List(Of IAS3Stmt)

        Public Sub Write(tabs As Integer, srcout As ISrcOut) Implements IAS3Stmt.Write

            Condition.Write(tabs, srcout)

            srcout.WriteLn("if (" & Condition.as3exprlist(Condition.as3exprlist.Count - 1).Value.ToString() & ")", tabs)
            srcout.WriteLn("{", tabs)
            For Each s In TruePass
                s.Write(tabs + 1, srcout)
            Next
            srcout.WriteLn("}", tabs)
            If Not FalsePass Is Nothing Then
                srcout.WriteLn("else", tabs)
                srcout.WriteLn("{", tabs)
                For Each s In FalsePass
                    s.Write(tabs + 1, srcout)
                Next
                srcout.WriteLn("}", tabs)
            End If


        End Sub
    End Class
End Namespace

