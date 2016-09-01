''' <summary>
''' 语法树
''' </summary>
''' <remarks></remarks>
Public Class GrammerTree
    ''' <summary>
    ''' 根节点
    ''' </summary>
    ''' <remarks></remarks>
    Public Root As GrammerExpr

    Public Function GetTreeString() As String
        Return (Root.GetTreeString(0, vbTab))

    End Function


    ''' <summary>
    ''' 遍历节点
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub VisitNodes()
        VisitNodes(Root)
    End Sub


    Private Sub VisitNodes(node As GrammerExpr)
        If Not VisitCallbacker Is Nothing Then

            VisitCallbacker(node)

            For Each child In node.Nodes
                VisitNodes(child)
            Next

        End If

    End Sub

    Public VisitCallbacker As Action(Of GrammerExpr)



End Class
