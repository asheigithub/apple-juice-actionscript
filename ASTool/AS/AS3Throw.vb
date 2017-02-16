Namespace AS3
    Public Class AS3Throw
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

        Public Exception As AS3Expression

        Public Sub Write(tabs As Integer, srcout As ISrcOut) Implements IAS3Stmt.Write
            If Not Exception Is Nothing Then

                Exception.Write(tabs, srcout)

                srcout.WriteLn("throw " & Exception.Value.ToString(), tabs)

            Else

                srcout.WriteLn("throw;", tabs)

            End If

        End Sub
    End Class
End Namespace

