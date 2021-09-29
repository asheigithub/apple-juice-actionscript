Namespace AS3
    Public Class AS3YieldBreak
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
        Public Sub Write(tabs As Integer, srcout As ISrcOut) Implements IAS3Stmt.Write

            srcout.WriteLn("yield break;", tabs)

        End Sub
    End Class
End Namespace
