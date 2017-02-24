Namespace AS3
    Public Class AS3Continue
        Implements IAS3Stmt

        Public continueFlag As String

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

            If continueFlag Is Nothing Then
                srcout.WriteLn("continue;", tabs)
            Else
                srcout.WriteLn("continue " & continueFlag & ";", tabs)
            End If

        End Sub
    End Class
End Namespace

