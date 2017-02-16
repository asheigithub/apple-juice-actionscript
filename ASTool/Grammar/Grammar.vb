''' <summary>
''' 文法
''' </summary>
''' <remarks></remarks>
Public Class Grammar

    Private gnodes As New Dictionary(Of String, GrammarNode)()

    Private termianlnodes As New Dictionary(Of GrammarNode, GrammarNode)()

    Private glines As New List(Of GrammarLine)()

    ''' <summary>
    ''' 预测分析表
    ''' </summary>
    ''' <remarks></remarks>
    Public M As Dictionary(Of GrammarNode, Dictionary(Of GrammarNode, GrammarLine))

    Public hasError As Boolean
    Public hasErrorFF As Boolean

    Public ErrorFFStr As String

    Public Sub New(tokenlist As TokenList)

        hasError = False

        tokenlist.Reset()
        tokenlist.GetNextToken()


        While tokenlist.CurrentToken.Type <> Token.TokenType.eof

            glines.AddRange(ParseLine(tokenlist))

        End While


        '计算FIRST集
        While True

            Dim found As Boolean = False

            For Each line In glines
                For i = 0 To line.Derivation.Count - 1
                    Dim ff = line.Derivation(i).FIRST

                    Dim oldcount = line.Main.FIRST.Count

                    For k = 0 To ff.Count - 1
                        line.Main.FIRST.Add(ff(k))
                    Next

                    If line.Main.FIRST.Count <> oldcount Then
                        found = True
                    End If

                    If Not ff.Contains(GrammarNode.GNodeNull) Then
                        Exit For
                    End If

                Next



            Next

            If Not found Then
                Exit While
            End If

        End While

        '***计算FOLLOW集
        glines(0).Main.FOLLOW.Add(GrammarNode.GNodeEOF)
        While True

            Dim found As Boolean = False

            For Each line In glines
                For i = 0 To line.Derivation.Count - 2
                    If line.Derivation(i).Type = GrammarNodeType.non_terminal Then
                        Dim ff = line.Derivation(i + 1).FIRST
                        Dim oldcount = line.Derivation(i).FOLLOW.Count

                        For Each symbol In ff

                            If symbol.Type <> GrammarNodeType.null Then
                                line.Derivation(i).FOLLOW.Add(symbol)
                            End If

                        Next

                        If line.Derivation(i).FOLLOW.Count > oldcount Then
                            found = True
                        End If

                    End If
                Next

                For i = 0 To line.Derivation.Count - 1
                    If line.Derivation(i).Type = GrammarNodeType.non_terminal Then
                        If (i < line.Derivation.Count - 1 AndAlso line.Derivation(i + 1).FIRST.Contains(GrammarNode.GNodeNull)) Or _
                            i = line.Derivation.Count - 1 Then
                            Dim oldcount = line.Derivation(i).FOLLOW.Count

                            For Each symobl In line.Main.FOLLOW

                                line.Derivation(i).FOLLOW.Add(symobl)
                            Next

                            If line.Derivation(i).FOLLOW.Count > oldcount Then
                                found = True
                            End If

                        End If
                    End If
                Next



            Next
            If Not found Then
                Exit While
            End If


        End While



        Console.WriteLine("非终结符:")
        For Each e In gnodes.Values
            Console.WriteLine(e.Name & vbTab & "FIRST{ " & String.Join(",", e.FIRST.Select(Function(n) IIf(n.Type = GrammarNodeType.terminal, """" & n.Name & """", n.Name)).ToArray()) & " }")
            Console.WriteLine(vbTab & "FOLLOW{ " & String.Join(",", e.FOLLOW.Select(Function(n) IIf(n.Type = GrammarNodeType.terminal, """" & n.Name & """", n.Name)).ToArray()) & " }")
            Console.WriteLine()

        Next


        '***生成预测分析表***
        M = New Dictionary(Of GrammarNode, Dictionary(Of GrammarNode, GrammarLine))()
        For Each nt In gnodes.Values
            Dim ML As New Dictionary(Of GrammarNode, GrammarLine)()
            For Each t In termianlnodes.Values
                If t.Type <> GrammarNodeType.null Then
                    ML.Add(t, Nothing)
                End If
            Next

            ML.Add(GrammarNode.GNodeEOF, Nothing)

            M.Add(nt, ML)
        Next

        For Each line In glines
            Dim first = line.Derivation(0).FIRST

            For Each k In first

                If k.Type <> GrammarNodeType.null Then
                    If Not M(line.Main)(k) Is Nothing AndAlso Not M(line.Main)(k).Equals(line) Then
                        Console.WriteLine("发现二义文法! 行[" & line.Main.Name & "] 输入[" & k.Name & "] 原来是" & M(line.Main)(k).ToString())
                        Console.WriteLine("            " & line.ToString())

                        ErrorFFStr &= "发现二义文法! 行[" & line.Main.Name & "] 输入[" & k.Name & "] 原来是" & M(line.Main)(k).ToString() & vbCrLf
                        ErrorFFStr &= "            " & line.ToString() & vbCrLf

                        'hasError = True
                        hasErrorFF = True
                        'Dim nnode As New GrammarNode()
                        'nnode.Type = GrammarNodeType.conststring
                        'nnode.Name = " " & M(line.Main)(k).ToString()
                        'line.Derivation.Add(nnode)

                        For index = 0 To glines.Count - 1

                            If glines(index).Equals(line) Then
                                M(line.Main)(k) = line
                                ErrorFFStr &= "   新加入的优先级高，替换原处理" & vbCrLf
                                Exit For
                            ElseIf glines(index).Equals(M(line.Main)(k)) Then
                                ErrorFFStr &= "   原优先级高，不处理" & vbCrLf
                                Exit For
                            End If

                        Next



                    Else
                        M(line.Main)(k) = line
                    End If


                End If
                If k.Type = GrammarNodeType.null Then
                    Dim follow = line.Main.FOLLOW

                    For Each b In follow
                        If Not M(line.Main)(b) Is Nothing AndAlso Not M(line.Main)(b).Equals(line) Then
                            Console.WriteLine("发现二义文法! 行[" & line.Main.Name & "] 输入[" & b.Name & "] 原来是" & M(line.Main)(b).ToString())
                            Console.WriteLine("            " & line.ToString())

                            ErrorFFStr &= "发现二义文法! 行[" & line.Main.Name & "] 输入[" & b.Name & "] 原来是" & M(line.Main)(b).ToString() & vbCrLf
                            ErrorFFStr &= "            " & line.ToString() & vbCrLf

                            'hasError = True
                            hasErrorFF = True

                            For index = 0 To glines.Count - 1

                                If glines(index).Equals(line) Then
                                    M(line.Main)(b) = line
                                    ErrorFFStr &= "    新加入的优先级高，替换原处理" & vbCrLf
                                    Exit For
                                ElseIf glines(index).Equals(M(line.Main)(b)) Then
                                    ErrorFFStr &= "    原优先级高，不处理" & vbCrLf
                                    Exit For
                                End If

                            Next


                            'Dim nnode As New GrammarNode()
                            'nnode.Type = GrammarNodeType.conststring
                            'nnode.Name = " " & M(line.Main)(b).ToString()
                            'line.Derivation.Add(nnode)

                            'line.Main.Name = M(line.Main)(b).ToString() & " " & line.Main.Name
                        Else
                            M(line.Main)(b) = line
                        End If



                    Next

                End If

            Next
        Next


        Console.WriteLine("预测分析表:")
        For Each t In termianlnodes.Values
            If t.Type = GrammarNodeType.null Then
                Continue For
            End If
            Console.Write(vbTab)
            Console.Write("|")
            Console.Write(IIf(t.Type = GrammarNodeType.terminal, """" & t.Name & """", t.Name))
        Next
        Console.Write(vbTab)
        Console.Write("|")
        Console.Write(GrammarNode.GNodeEOF.Name)
        Console.WriteLine()

        For Each f In M.Keys
            Console.Write(f.Name)
            For Each t In termianlnodes.Values
                If t.Type = GrammarNodeType.null Then
                    Continue For
                End If
                Console.Write(vbTab)
                Console.Write("|")
                Console.Write(M(f)(t))
            Next
            Console.Write(vbTab)
            Console.Write("|")
            Console.Write(M(f)(GrammarNode.GNodeEOF))
            Console.WriteLine()

        Next



    End Sub

    Private Function ParseLine(words As TokenList) As List(Of GrammarLine)
        Dim tk As Token = words.CurrentToken
        Dim result As New List(Of GrammarLine)()

        Dim grammarline As New GrammarLine()
        result.Add(grammarline)

        If tk.Type = Token.TokenType.other And tk.StringValue = "<" Then
            tk = words.GetNextToken()
            If tk.Type = Token.TokenType.identifier Then
                Dim node As GrammarNode
                If Not gnodes.ContainsKey(tk.StringValue) Then
                    node = New GrammarNode()
                    node.Type = GrammarNodeType.non_terminal
                    node.Name = tk.StringValue
                    gnodes.Add(node.Name, node)

                Else
                    node = gnodes(tk.StringValue)
                End If

                grammarline.Main = node

                tk = words.GetNextToken()
            Else
                Throw New Exception("期望标识符")
            End If
        Else
            Throw New Exception("期望<")
        End If

        If tk.Type = Token.TokenType.other And tk.StringValue = ">" Then
            Match(words.GetNextToken(), Token.TokenType.other, ":")
            Match(words.GetNextToken(), Token.TokenType.other, ":")
            Match(words.GetNextToken(), Token.TokenType.other, "=")

        Else
            Throw New Exception("期望>")
        End If

        tk = words.GetNextToken()

        While Not (tk.Type = Token.TokenType.other And tk.StringValue = ";")
            '***分析导出式***

            If tk.Type = Token.TokenType.other And tk.StringValue = "<" Then
                tk = words.GetNextToken()
                If tk.Type = Token.TokenType.identifier Then
                    Dim node As GrammarNode
                    If Not gnodes.ContainsKey(tk.StringValue) Then
                        node = New GrammarNode()
                        node.Type = GrammarNodeType.non_terminal
                        node.Name = tk.StringValue
                        gnodes.Add(node.Name, node)

                    Else
                        node = gnodes(tk.StringValue)
                    End If

                    grammarline.Derivation.Add(node)

                    
                    Match(words.GetNextToken(), Token.TokenType.other, ">")

                    tk = words.GetNextToken()

                Else
                    Throw New Exception("期望标识符")
                End If
            ElseIf tk.Type = Token.TokenType.identifier Then
                Dim node As New GrammarNode()
                If tk.StringValue = "null" Then
                    node = GrammarNode.GNodeNull
                ElseIf tk.StringValue = "number" Then
                    node = GrammarNode.GNodeNumber
                ElseIf tk.StringValue = "string" Then
                    node = GrammarNode.GNodeString
                ElseIf tk.StringValue = "identifier" Then
                    node = GrammarNode.GNodeIdentifier
                ElseIf tk.StringValue = "S" Then
                    node = GrammarNode.GNodeWhiteSpace
                Else
                    Throw New Exception("错误的符号" & tk.StringValue)
                End If

                If Not termianlnodes.ContainsKey(node) Then
                    node.FIRST.Add(node)
                    termianlnodes.Add(node, node)
                End If

                grammarline.Derivation.Add(termianlnodes(node))

                tk = words.GetNextToken()
            ElseIf tk.Type = Token.TokenType.const_string Then
                Dim node As New GrammarNode()
                node.Type = GrammarNodeType.terminal
                node.Name = tk.StringValue

                If Not termianlnodes.ContainsKey(node) Then
                    node.FIRST.Add(node)
                    termianlnodes.Add(node, node)
                End If

                grammarline.Derivation.Add(termianlnodes(node))



                tk = words.GetNextToken()
            ElseIf tk.Type = Token.TokenType.other And tk.StringValue = "|" Then

                grammarline = New GrammarLine()
                result.Add(grammarline)
                grammarline.Main = result(0).Main

                tk = words.GetNextToken()

            Else

                Throw New Exception("错误的符号" & tk.StringValue)
            End If



        End While

        words.GetNextToken()

        Return result
    End Function


    Private Sub Match(token As Token, type As Token.TokenType, value As String)
        If token.Type <> type Then
            Throw New Exception(token.StringValue & "不能出现在这里")
        End If

        If Not value Is Nothing Then
            If token.StringValue <> value Then
                Throw New Exception("期望" & value)
            End If
        End If

    End Sub


    
    Public Function ParseTree(input As String, definekeywords As IEnumerable(Of String), Optional srcfile As String = "") As GrammerTree

        Dim tree As New GrammerTree()

        tree.Root = New GrammerExpr() With {.GrammerLeftNode = glines(0).Main}
        Dim treenodestack As New Stack(Of GrammerExpr)
        treenodestack.Push(tree.Root)



        Dim words = New Lex(srcfile, definekeywords, True).GetWords(input)

        words.Reset()
        words.GetNextToken()


        Dim stack As New Stack(Of GrammarNode)
        stack.Push(GrammarNode.GNodeEOF)
        stack.Push(glines(0).Main)

        Dim X = stack.Peek()

        Dim matched As String = ""



        While Not X.Equals1(GrammarNode.GNodeEOF)
            Dim selectedline As String = ""

            If MathGNodeAndToken(X, words.CurrentToken) Then

                Dim tnode = treenodestack.Pop()
                tnode.MatchedToken = words.CurrentToken

                stack.Pop()

                matched = matched & words.CurrentToken.StringValue


                'words.GetNextToken()
                words.GetNextTokenWithWhiteBlank()

            ElseIf termianlnodes.ContainsKey(X) Then

                If GetGNode(words.CurrentToken).Type = GrammarNodeType.whitespace Then
                    '***吃掉无用空白***
                    words.GetNextTokenWithWhiteBlank()

                Else

                    'Console.WriteLine("无法匹配" & words.CurrentToken.StringValue)
                    ThrowError("无法匹配", words.CurrentToken)

                    Dim node = treenodestack.Pop()
                    node.InputToken = words.CurrentToken

                    Exit While
                End If
            ElseIf Not M(X).ContainsKey(GetGNode(words.CurrentToken)) Then
                If GetGNode(words.CurrentToken).Type = GrammarNodeType.whitespace Then
                    '***吃掉无用空白***
                    words.GetNextTokenWithWhiteBlank()

                Else
                    ThrowError("不可接受", words.CurrentToken)
                    Dim node = treenodestack.Pop()
                    node.InputToken = words.CurrentToken
                    Exit While
                End If
            ElseIf M(X)(GetGNode(words.CurrentToken)) Is Nothing Then

                If GetGNode(words.CurrentToken).Type = GrammarNodeType.whitespace Then
                    '***吃掉无用空白***
                    words.GetNextTokenWithWhiteBlank()

                Else
                    ThrowError("无法匹配", words.CurrentToken)

                    Dim node = treenodestack.Pop()
                    node.InputToken = words.CurrentToken

                    Exit While
                End If
            Else
                Dim line = M(X)(GetGNode(words.CurrentToken))



                '***输出产生式***

                Dim node = treenodestack.Pop()
                node.MatchedToken = words.CurrentToken
                node.SelectGrammerLine = line

                If Not (line.Derivation.Count = 1 And line.Derivation(0).Type = GrammarNodeType.null) Then

                    For index = 0 To line.Derivation.Count - 1
                        Dim tnode As New GrammerExpr()
                        tnode.GrammerLeftNode = line.Derivation(index)
                        node.Nodes.Add(tnode)
                        tnode.Parent = node
                    Next

                    For index = node.Nodes.Count - 1 To 0 Step -1
                        treenodestack.Push(node.Nodes(index))
                    Next

                End If




                selectedline = line.ToString()
                stack.Pop()




                If Not (line.Derivation.Count = 1 And line.Derivation(0).Type = GrammarNodeType.null) Then
                    For index = line.Derivation.Count - 1 To 0 Step -1
                        stack.Push(line.Derivation(index))
                    Next
                End If
            End If


            X = stack.Peek()

            'Console.Write(matched)
            'Console.Write(vbTab)

            'Console.Write(vbTab)
            'Console.Write(selectedline)

            'Console.WriteLine()

        End While


        Return tree

    End Function

    Private Function GetGNode(token As Token) As GrammarNode
        If token.Type = ASTool.Token.TokenType.eof Then
            Return GrammarNode.GNodeEOF
        End If

        If token.Type = ASTool.Token.TokenType.whitespace Or token.Type = ASTool.Token.TokenType.comments Then
            Return GrammarNode.GNodeWhiteSpace
        End If

        '**定义的关键字优先***
        For Each k In termianlnodes.Values
            If k.Type = GrammarNodeType.terminal Then
                If MathGNodeAndToken(k, token) Then
                    Return k
                End If
            End If
        Next
        '***标识符其次***
        For Each k In termianlnodes.Values
            If k.Type = GrammarNodeType.identifier Then
                If MathGNodeAndToken(k, token) Then
                    Return k
                End If
            End If
        Next

        For Each k In termianlnodes.Values
            If Not k.Type = GrammarNodeType.null Then
                If MathGNodeAndToken(k, token) Then
                    Return k
                End If
            End If
        Next





        Return GrammarNode.GNodeWrong

        'If token.Type = ASTool.Token.TokenType.const_number Then
        '    Return GrammarNode.GNodeNumber
        'End If

        'If token.Type = ASTool.Token.TokenType.const_string Then
        '    Return GrammarNode.GNodeString
        'End If

        'If token.Type = ASTool.Token.TokenType.eof Then
        '    Return GrammarNode.GNodeEOF
        'End If

        'If token.Type = ASTool.Token.TokenType.identifier Then
        '    Return GrammarNode.GNodeIdentifier
        'End If

        'If token.Type = ASTool.Token.TokenType.other Then
        '    Dim node As New GrammarNode() With {.Type = GrammarNodeType.terminal, .Name = token.StringValue}
        '    Return node
        'End If

        'Throw New Exception("不能识别的token" & token.StringValue)

    End Function

    Private Function MathGNodeAndToken(node As GrammarNode, token As Token) As Boolean
        If node.Type = GrammarNodeType.null Then
            Return True
        End If

        If node.Type = GrammarNodeType.whitespace Then
            If token.Type = ASTool.Token.TokenType.eof Or token.Type = ASTool.Token.TokenType.whitespace Or token.Type = ASTool.Token.TokenType.comments Then
                Return True
            End If
        End If

        If node.Type = GrammarNodeType.conststring And token.Type = ASTool.Token.TokenType.const_string Then
            Return True
        ElseIf node.Type = GrammarNodeType.eof And token.Type = ASTool.Token.TokenType.eof Then
            Return True
        End If

        If node.Type = GrammarNodeType.identifier And token.Type = ASTool.Token.TokenType.identifier Then
            Return True
        End If


        If node.Type = GrammarNodeType.number And token.Type = ASTool.Token.TokenType.const_number Then
            Return True
        End If

        If node.Type = GrammarNodeType.terminal And (token.Type = ASTool.Token.TokenType.other Or token.Type = ASTool.Token.TokenType.identifier) _
            And node.Name = token.StringValue Then
            Return True
        End If

        Return False

    End Function


    Private Sub ThrowError(msg As String, token As Token)

        hasError = True

        Console.WriteLine(String.Format("匹配失败!{0} 行{1} 列{2},符号{3} ", msg, token.line + 1, token.ptr, token.StringValue))
    End Sub


    Public Sub ListGNodes()
        Dim sb As New Text.StringBuilder()


        sb.AppendLine("Select Case node.GrammerLeftNode.Name")
        For Each ng In gnodes.Values
            sb.AppendLine(vbTab & "Case """ & ng.Name & """")
            sb.AppendLine(vbTab & vbTab & "_" & ng.Name & "(node)")
        Next
        sb.AppendLine("End Select")

        sb.AppendLine()


        For Each ng In gnodes.Values
            sb.AppendLine("Sub _" & ng.Name & "(node As GrammerExpr)")
            sb.AppendLine()
            sb.AppendLine("End Sub")
        Next


        My.Computer.FileSystem.WriteAllText("D:\ASCRIPT\ASTool\ASTool\GrammarNodes.txt", sb.ToString(), False)

    End Sub




End Class
