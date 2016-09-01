Namespace AS3


    ''' <summary>
    ''' for(;;)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AS3For
        Implements IAS3Stmt


        ''' <summary>
        ''' for(;{2};{3})的第2部分
        ''' </summary>
        ''' <remarks></remarks>
        Public Part2 As AS3StmtExpressions


        ''' <summary>
        ''' for(;{2};{3})的第3部分
        ''' </summary>
        ''' <remarks></remarks>
        Public Part3 As AS3StmtExpressions

        Public Body As List(Of IAS3Stmt)

        Public Sub Write(tabs As Integer, srcout As ISrcOut) Implements IAS3Stmt.Write

            srcout.WriteLn("for(;", tabs)

            If Not Part2 Is Nothing Then
                Part2.Write(tabs + 1, srcout)
            End If

            srcout.WriteLn("    ;", tabs + 1)

            If Not Part3 Is Nothing Then
                Part3.Write(tabs + 1, srcout)
            End If

            srcout.WriteLn(")", tabs)
            srcout.WriteLn("{", tabs)

            For Each s In Body
                s.Write(tabs + 1, srcout)
            Next

            srcout.WriteLn("}", tabs)
        End Sub
    End Class
End Namespace

