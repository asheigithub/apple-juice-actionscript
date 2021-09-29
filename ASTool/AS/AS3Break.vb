Namespace AS3
    Public Class AS3Break
        Implements IAS3Stmt

        Public breakFlag As String

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
            If breakFlag Is Nothing Then
                srcout.WriteLn("break;", tabs)
            Else
                srcout.WriteLn("break " & breakFlag & ";", tabs)
            End If

        End Sub
    End Class
End Namespace

