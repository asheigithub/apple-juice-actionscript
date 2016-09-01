Namespace AS3
    ''' <summary>
    ''' for each( in )
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AS3ForEach
        Implements IAS3Stmt

        ''' <summary>
        ''' for each  变量部分 可能是AS3Variable 或者 AS3StmtExpressions
        ''' </summary>
        ''' <remarks></remarks>
        Public ForArg As IAS3Stmt
        ''' <summary>
        ''' for each  表达式值部分
        ''' </summary>
        ''' <remarks></remarks>
        Public ForExpr As AS3Expression

        Public Body As List(Of IAS3Stmt)

        Public Sub Write(tabs As Integer, srcout As ISrcOut) Implements IAS3Stmt.Write

            If TypeOf ForArg Is AS3StmtExpressions Then
                CType(ForArg, AS3StmtExpressions).Write(tabs, srcout)
            End If

            ForExpr.Write(tabs, srcout)

            srcout.WriteLn("for each( ", tabs)
            If TypeOf ForArg Is AS3Variable Then
                srcout.WriteLn(CType(ForArg, AS3Variable).Name, tabs + 1)
            Else
                Dim tt = CType(ForArg, AS3StmtExpressions)
                srcout.WriteLn(tt.as3exprlist(tt.as3exprlist.Count - 1).Value.ToString(), tabs + 1)
            End If

            srcout.WriteLn("    in", tabs)

            srcout.WriteLn(ForExpr.Value.ToString(), tabs + 1)


            srcout.WriteLn(")", tabs)
            srcout.WriteLn("{", tabs)

            For Each s In Body
                s.Write(tabs + 1, srcout)
            Next

            srcout.WriteLn("}", tabs)

        End Sub
    End Class
End Namespace

