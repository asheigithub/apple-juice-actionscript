''' <summary>
''' 语法树节点
''' </summary>
''' <remarks></remarks>
Public Class GrammerExpr

    Public Parent As GrammerExpr

    Public Nodes As New List(Of GrammerExpr)

    Public GrammerLeftNode As GrammarNode

    Public SelectGrammerLine As GrammarLine

    Public MatchedToken As Token

    Public InputToken As Token

    Friend exprsteplist As AS3.Expr.AS3ExprStepList


	Public Function getMd5Key() As String

		Dim md5p As New System.Security.Cryptography.MD5CryptoServiceProvider()

		Dim sb As New Text.StringBuilder()

		visMD5Token(Me, sb)

		Dim buff = System.Text.Encoding.UTF8.GetBytes(sb.ToString())

		Dim md5out = md5p.ComputeHash(buff)

		Return System.Convert.ToBase64String(md5out)
	End Function

	Private Sub visMD5Token(node As GrammerExpr, sb As Text.StringBuilder)
		sb.Append(node.MatchedToken.Type.ToString())
		sb.Append(node.MatchedToken.StringValue)

		For Each child In node.Nodes
			visMD5Token(child, sb)
		Next



	End Sub



	Public Function GetTreeString(tabs As Integer) As String
        Return GetTreeString(tabs, vbTab)
    End Function

    Public Function GetTreeString(tabs As Integer, tabstring As String) As String
        Dim sb As New System.Text.StringBuilder()

        sb.Append(tabstring, tabs)
        If GrammerLeftNode.Type = GrammarNodeType.non_terminal Then
            If Not SelectGrammerLine Is Nothing Then
                sb.AppendLine(SelectGrammerLine.ToString() & tabstring & " [input """ & MatchedToken.StringValue & """]")
            Else
                If Not InputToken Is Nothing Then
                    sb.AppendLine(GrammerLeftNode.Name & " ->***nochoose" & tabstring & " [input """ & InputToken.StringValue & """]")
                Else
                    sb.AppendLine(GrammerLeftNode.Name & " ***wait input")
                End If

            End If
        Else
            If Not MatchedToken Is Nothing Then
                sb.AppendLine(GrammerLeftNode.Name & tabstring & " [matched """ + MatchedToken.StringValue + """]")
            Else
                If Not InputToken Is Nothing Then
                    sb.AppendLine(GrammerLeftNode.Name & " ***notmatched" & tabstring & " [input """ & InputToken.StringValue & """]")
                Else
                    sb.AppendLine(GrammerLeftNode.Name & " ***wait input")
                End If

            End If

        End If





        If Nodes.Count > 0 Then


            For Each n In Nodes

                sb.Append(n.GetTreeString(tabs + 1, tabstring))

            Next
        End If





        Return sb.ToString()

    End Function



    Public Shared Function getNodeValue(node As GrammerExpr) As String



        If node.GrammerLeftNode.Type = GrammarNodeType.non_terminal Then

            Dim result As String = ""
            For i = 0 To node.Nodes.Count - 1
                Dim temptv = getNodeValue(node.Nodes(i))
                result = result & temptv
            Next
            Return result

        Else
            Return node.MatchedToken.StringValue
        End If


        Return ""

    End Function

End Class
