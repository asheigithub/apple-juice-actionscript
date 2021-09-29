Namespace AS3
    Public Class AS3Block

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

        Public CodeList As List(Of IAS3Stmt)

        Public Sub Write(tabs As Integer, srcout As ISrcOut) Implements IAS3Stmt.Write


            srcout.WriteLn(IIf(label Is Nothing, "", label & ":"), tabs)
            srcout.WriteLn("{", tabs)
            For Each s In CodeList
                s.Write(tabs + 1, srcout)
            Next
            srcout.WriteLn("}", tabs)

        End Sub
    End Class
End Namespace
