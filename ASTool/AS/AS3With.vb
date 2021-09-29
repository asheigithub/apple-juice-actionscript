Namespace AS3

    Public Class AS3With
        Implements IAS3Stmt

        Public label As String

        Private matchtoken As Token
        Public Sub New(token As Token)
            Me.matchtoken = token
        End Sub

        Public ReadOnly Property Token As Token Implements IAS3Stmt.Token
            Get
                Return matchtoken
            End Get
        End Property

        Public withObject As AS3StmtExpressions

        Public Body As List(Of IAS3Stmt)


        Public Sub Write(tabs As Integer, srcout As ISrcOut) Implements IAS3Stmt.Write
            withObject.Write(tabs, srcout)

            srcout.WriteLn(IIf(label Is Nothing, "", label & ":") & "with (" & withObject.as3exprlist(withObject.as3exprlist.Count - 1).Value.ToString() & ")", tabs)
            srcout.WriteLn("{", tabs)
            For Each s In Body

                s.Write(tabs + 1, srcout)
            Next

            withObject.Write(tabs + 1, srcout)

            srcout.WriteLn("}", tabs)
        End Sub
    End Class
End Namespace
