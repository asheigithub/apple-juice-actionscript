Namespace AS3
    ''' <summary>
    ''' 表达式列表
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AS3StmtExpressions
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

        'Public grammerExpr As GrammerExpr

        Public as3exprlist As List(Of AS3Expression)

        Public Sub Write(tabs As Integer, srcout As ISrcOut) Implements IAS3Stmt.Write
            For Each k In as3exprlist
                k.Write(tabs, srcout)
            Next
        End Sub
    End Class
End Namespace

