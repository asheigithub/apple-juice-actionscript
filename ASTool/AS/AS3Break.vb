Namespace AS3
    Public Class AS3Break
        Implements IAS3Stmt

        Public Sub Write(tabs As Integer, srcout As ISrcOut) Implements IAS3Stmt.Write
            srcout.WriteLn("break;", tabs)
        End Sub
    End Class
End Namespace

