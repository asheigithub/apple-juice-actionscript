Namespace AS3
    Public Class AS3Continue
        Implements IAS3Stmt

        Public Sub Write(tabs As Integer, srcout As ISrcOut) Implements IAS3Stmt.Write
            srcout.WriteLn("continue;", tabs)
        End Sub
    End Class
End Namespace

