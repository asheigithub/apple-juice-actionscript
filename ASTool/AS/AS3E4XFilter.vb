Namespace AS3
    ''' <summary>
    ''' XML的过滤 .(@id==XXX)之类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AS3E4XFilter
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

        Public InputXml As Expr.AS3DataStackElement
        Public OutPutXml As Expr.AS3DataStackElement

        Public FilterExprList As Expr.AS3ExprStepList

        Public FilterId As Expr.AS3DataStackElement

        Public Sub Write(tabs As Integer, srcout As ISrcOut) Implements IAS3Stmt.Write




            srcout.WriteLn("E4XFilter Id=" & FilterId.ToString() & " (input:" & InputXml.ToString() & ",output:" & OutPutXml.ToString() & ")", tabs)
            srcout.WriteLn("{", tabs)

            For Each e In FilterExprList
                srcout.WriteLn(e.ToString(), tabs + 1)
            Next

            
            srcout.WriteLn("}", tabs)



        End Sub
    End Class
End Namespace

