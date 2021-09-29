Namespace AS3
    Public Class AS3Switch
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

        Public Expr As AS3Expression

        Public CaseList As New List(Of AS3SwitchCase)

        Public Sub Write(tabs As Integer, srcout As ISrcOut) Implements IAS3Stmt.Write

            Expr.Write(tabs, srcout)
            For Each c In CaseList
                If Not c.IsDefault Then
                    c.Condition.Write(tabs, srcout)
                End If

            Next

            srcout.WriteLn(IIf(label Is Nothing, "", label & ":") & "switch (" & Expr.Value.ToString() & ")", tabs)
            srcout.WriteLn("{", tabs)

            For Each c In CaseList
                If Not c.IsDefault Then
                    srcout.WriteLn("case " & c.Condition.Value.ToString() & ":", tabs + 1)
                    For Each b In c.Body
                        b.Write(tabs + 2, srcout)
                    Next
                Else
                    srcout.WriteLn("default:", tabs + 1)
                    For Each b In c.Body
                        b.Write(tabs + 2, srcout)
                    Next
                End If
            Next


            srcout.WriteLn("}", tabs)



        End Sub
    End Class

    Public Class AS3SwitchCase
        Public Condition As AS3Expression

        Public token As Token

        Public holdreg As Expr.AS3DataStackElement

        Public Sub New(token As Token)
            Me.token = token
        End Sub

        Public Body As List(Of IAS3Stmt)

        Public IsDefault As Boolean

    End Class

End Namespace

