''' <summary>
''' 文法
''' </summary>
''' <remarks></remarks>
Public Class Grammar

    Private gnodes As New Dictionary(Of String, GrammarNode)()

    Private termianlnodes As New Dictionary(Of GrammarNode, GrammarNode)()

	Private _terminals As New List(Of GrammarNode)
	Private _identifiers As New List(Of GrammarNode)
	Private _other As New List(Of GrammarNode)

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
                        If (i < line.Derivation.Count - 1 AndAlso line.Derivation(i + 1).FIRST.Contains(GrammarNode.GNodeNull)) Or
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



        'Console.WriteLine("非终结符:")
        'For Each e In gnodes.Values
        '    Console.WriteLine(e.Name & vbTab & "FIRST{ " & String.Join(",", e.FIRST._Select(Function(n) IIf(n.Type = GrammarNodeType.terminal, """" & n.Name & """", n.Name))) & " }")
        '    Console.WriteLine(vbTab & "FOLLOW{ " & String.Join(",", e.FOLLOW._Select(Function(n) IIf(n.Type = GrammarNodeType.terminal, """" & n.Name & """", n.Name))) & " }")
        '    Console.WriteLine()

        'Next
        'Console.WriteLine("终结符:")
        'For Each e In termianlnodes.Values
        '    Console.WriteLine(e.Name & vbTab & "FIRST{ " & String.Join(" , ", e.FIRST.Select(Function(n) """" & n.Name & """").ToArray()) & " }")
        'Next

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
                        'Console.WriteLine("发现二义文法! 行[" & line.Main.Name & "] 输入[" & k.Name & "] 原来是" & M(line.Main)(k).ToString())
                        'Console.WriteLine("            " & line.ToString())

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
                            'Console.WriteLine("发现二义文法! 行[" & line.Main.Name & "] 输入[" & b.Name & "] 原来是" & M(line.Main)(b).ToString())
                            'Console.WriteLine("            " & line.ToString())

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

		For Each k In termianlnodes.Values
			If k.Type = GrammarNodeType.terminal Then
				_terminals.Add(k)
			End If
		Next
		For Each k In termianlnodes.Values
			If k.Type = GrammarNodeType.identifier Then
				_identifiers.Add(k)
			End If
		Next
		For Each k In termianlnodes.Values
			If Not k.Type = GrammarNodeType.null Then
				If k.Type <> GrammarNodeType.terminal AndAlso k.Type <> GrammarNodeType.identifier Then
					_other.Add(k)
				End If
			End If
		Next

		'Console.WriteLine("预测分析表:")
		'For Each t In termianlnodes.Values
		'    If t.Type = GrammarNodeType.null Then
		'        Continue For
		'    End If
		'    Console.Write(vbTab)
		'    Console.Write("|")
		'    Console.Write(IIf(t.Type = GrammarNodeType.terminal, """" & t.Name & """", t.Name))
		'Next
		'Console.Write(vbTab)
		'Console.Write("|")
		'Console.Write(GrammarNode.GNodeEOF.Name)
		'Console.WriteLine()

		'For Each f In M.Keys
		'    Console.Write(f.Name)
		'    For Each t In termianlnodes.Values
		'        If t.Type = GrammarNodeType.null Then
		'            Continue For
		'        End If
		'        Console.Write(vbTab)
		'        Console.Write("|")
		'        Console.Write(M(f)(t))
		'    Next
		'    Console.Write(vbTab)
		'    Console.Write("|")
		'    Console.Write(M(f)(GrammarNode.GNodeEOF))
		'    Console.WriteLine()

		'Next



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
					node = New GrammarNode(tk.StringValue, GrammarNodeType.non_terminal)
					'node.Type = GrammarNodeType.non_terminal
					'node.Name = tk.StringValue
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
						node = New GrammarNode(tk.StringValue, GrammarNodeType.non_terminal)
						'node.Type = GrammarNodeType.non_terminal
						'node.Name = tk.StringValue
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
				Dim node As GrammarNode
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
                ElseIf tk.StringValue = "label" Then
                    node = GrammarNode.GNodeLabel
                ElseIf tk.StringValue = "this" Then
                    node = GrammarNode.GNodeThis
                ElseIf tk.StringValue = "super" Then
                    node = GrammarNode.GNodeSuper
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
				Dim node As New GrammarNode(tk.StringValue, GrammarNodeType.terminal)
				'node.Type = GrammarNodeType.terminal
				'node.Name = tk.StringValue

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

    Private Function findinkeywords(definekeywords As IEnumerable(Of String), key As String) As Boolean
        For Each k In definekeywords
            If k = key Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Function ParseTree(input As String, definekeywords As IEnumerable(Of String), defineSkipBlankWords As IEnumerable(Of String), Optional srcfile As String = "") As GrammerTree

        Dim tree As New GrammerTree()

        tree.Root = New GrammerExpr() With {.GrammerLeftNode = glines(0).Main}
        Dim treenodestack As New Stack(Of GrammerExpr)
        treenodestack.Push(tree.Root)

        Dim words As TokenList
        Try
            words = New Lex(srcfile, definekeywords, defineSkipBlankWords, True).GetWords(input)
        Catch ex As LexException

            hasError = True

            Console.WriteLine(ex.Message & " line:" & ex.line & " ptr:" & ex.ptr)


            Return tree
        End Try



        '#Region "检查 this 和 super关键字"
        For index = 0 To words.Count - 1
            Dim token = words(index)
            If token.Type = Token.TokenType.identifier Then
                If token.StringValue = "this" Then
                    token.Type = Token.TokenType.this_pointer
                ElseIf token.StringValue = "super" Then
                    token.Type = Token.TokenType.super_pointer
                End If
            End If
        Next

        '#End Region

        'continue、break、return和throw后跟换行符会添加一个分号
        For index = 0 To words.Count - 1
            Dim token = words(index)
            If token.Type = Token.TokenType.identifier Then
                If token.StringValue = "continue" Or
                    token.StringValue = "break" Or
                    token.StringValue = "return" Or
                    token.StringValue = "throw" Then

                    '***查询后面到换行直接是否全是空白，如果是则插入一个分号

                    For k = index + 1 To words.Count - 1
                        Dim nt = words(k)
                        If nt.Type = Token.TokenType.other And nt.StringValue = ";" Then
                            Exit For
                        ElseIf nt.Type = token.TokenType.whitespace And nt.StringValue = vbLf Then

                            nt.Type = Token.TokenType.other
                            nt.StringValue = ";"
                            Exit For

                        ElseIf nt.Type = Token.TokenType.comments Then

                        Else
                            Exit For
                        End If

                    Next

                End If
            End If
        Next


        '#Region "检测语句label"

        '***检测label  label:for label:while label:do label:switch***
        '这些关键字前出现 identifier ":" 说明是一个label标记
        For index = 2 To words.Count - 1
            Dim token = words(index)
            If token.Type = Token.TokenType.identifier Then
                If token.StringValue = "for" Or
                    token.StringValue = "while" Or
                    token.StringValue = "do" Or
                    token.StringValue = "switch" Or
                    token.StringValue = "if" Or
                    token.StringValue = "try" Or
                    token.StringValue = "with" Then
                    '**前向查找
                    Dim found As Boolean = False
                    Dim fidx = index

                    Dim fstep As Integer = 0
                    Dim stepidx = 0

                    While Not found AndAlso fidx > 0
                        fidx = fidx - 1
                        Dim test = words(fidx)

                        If fstep = 0 Then '查找":"
                            If test.Type = Token.TokenType.whitespace Or test.Type = Token.TokenType.comments Then
                                Continue While
                            ElseIf test.Type = Token.TokenType.other And test.StringValue = ":" Then
                                fstep = 1
                                stepidx = fidx
                                Continue While
                            Else
                                Exit While
                            End If
                        End If

                        If fstep = 1 Then '查找 identifier为 label名
                            If test.Type = Token.TokenType.whitespace Or test.Type = Token.TokenType.comments Then
                                Continue While
                            ElseIf test.Type = Token.TokenType.identifier Then

                                '前一个必须是空或者;
                                If fidx > 0 Then
                                    Dim test2 = words(fidx - 1)
                                    If Not (test2.Type = Token.TokenType.whitespace Or
                                            (test2.Type = Token.TokenType.other AndAlso
                                            test2.StringValue = ";")) Then

                                        Exit While

                                    End If

                                End If


                                found = True
                                test.Type = Token.TokenType.label


                                '**将label:移动到关键字后面去
                                words(fidx) = words(index) '关键字向前移动
                                words(index) = words(stepidx) '冒号后移
                                words(stepidx) = test


                                index = index + 3
                                Exit While
                            Else
                                Exit While
                            End If
                        End If

                    End While
                End If
            End If
        Next
        '#End Region

        '#Region "查找代码块label"
        ' label:{

        For i = 2 To words.Count - 1
            Dim token = words(i)
            If token.Type = Token.TokenType.other Then
                If token.StringValue = "{" Then
                    '**前向查找":"

                    Dim fidx As Integer = i



                    While fidx > 0
                        fidx = fidx - 1
                        Dim test = words(fidx)

                        If test.Type = Token.TokenType.whitespace Or test.Type = Token.TokenType.comments Then
                            Continue While
                        ElseIf test.Type = Token.TokenType.other And test.StringValue = ":" Then
                            Exit While
                        Else
                            Exit While
                        End If
                    End While

                    If Not (words(fidx).Type = Token.TokenType.other And words(fidx).StringValue = ":") Then
                        Continue For
                    End If
                    Dim stepidx As Integer = fidx

                    '***向前查找identifier;
                    While fidx > 0
                        fidx = fidx - 1
                        Dim test = words(fidx)

                        If test.Type = Token.TokenType.whitespace Or test.Type = Token.TokenType.comments Then
                            Continue While
                        ElseIf test.Type = Token.TokenType.identifier Then
                            Exit While
                        Else
                            Exit While
                        End If
                    End While
                    If Not words(fidx).Type = Token.TokenType.identifier Then
                        Continue For
                    End If

                    Dim identifieridx As Integer = fidx

                    If words(identifieridx).StringValue = "default" Then
                        Continue For
                    End If

                    '***向前查找其他排除项***
                    While fidx > 0
                        fidx = fidx - 1
                        Dim test = words(fidx)

                        If test.Type = Token.TokenType.whitespace Or test.Type = Token.TokenType.comments Then
                            Continue While
                        ElseIf test.Type = Token.TokenType.other And (test.StringValue = "." Or test.StringValue = "?" Or test.StringValue = ":" Or findinkeywords(definekeywords, test.StringValue)) Then


                            Continue For
                        ElseIf test.Type = Token.TokenType.identifier And (
                            test.StringValue = "case") Then
                            Continue For
                        Else
                            Exit While
                        End If
                    End While

                    words(identifieridx).Type = Token.TokenType.label

                    Dim temp = words(identifieridx)
                    '**将label:移动到关键字后面去
                    words(identifieridx) = words(i) '关键字向前移动
                    words(i) = words(stepidx) '冒号后移
                    words(stepidx) = temp


                End If
            End If
        Next


        '#End Region




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
		'For Each k In termianlnodes.Values
		'    If k.Type = GrammarNodeType.terminal Then
		'        If MathGNodeAndToken(k, token) Then
		'            Return k
		'        End If
		'    End If
		'Next
		For Each k In _terminals
			If MathGNodeAndToken(k, token) Then
				Return k
			End If
		Next

		'***标识符其次***
		'For Each k In termianlnodes.Values
		'          If k.Type = GrammarNodeType.identifier Then
		'              If MathGNodeAndToken(k, token) Then
		'                  Return k
		'              End If
		'          End If
		'      Next
		For Each k In _identifiers
			If MathGNodeAndToken(k, token) Then
				Return k
			End If
		Next


		'For Each k In termianlnodes.Values
		'          If Not k.Type = GrammarNodeType.null Then
		'              If MathGNodeAndToken(k, token) Then
		'                  Return k
		'              End If
		'          End If
		'      Next
		For Each k In _other
			If MathGNodeAndToken(k, token) Then
				Return k
			End If
		Next




		Return GrammarNode.GNodeWrong

	End Function

    Private Function MathGNodeAndToken(node As GrammarNode, token As Token) As Boolean
		Select Case node.Type
			Case GrammarNodeType.null
				Return True
			Case GrammarNodeType.whitespace
				If token.Type = ASTool.Token.TokenType.eof OrElse token.Type = ASTool.Token.TokenType.whitespace OrElse token.Type = ASTool.Token.TokenType.comments Then
					Return True
				End If
			Case GrammarNodeType.conststring
				If (token.Type = ASTool.Token.TokenType.const_string OrElse token.Type = Token.TokenType.const_regexp) Then
					Return True
				End If
			Case GrammarNodeType.eof
				If token.Type = ASTool.Token.TokenType.eof Then
					Return True
				End If
			Case GrammarNodeType.identifier
				If token.Type = ASTool.Token.TokenType.identifier Then
					Return True
				End If
			Case GrammarNodeType.label
				If token.Type = Token.TokenType.label Then
					Return True
				End If
			Case GrammarNodeType.this
				If token.Type = Token.TokenType.this_pointer Then
					Return True
				End If
			Case GrammarNodeType.super
				If token.Type = Token.TokenType.super_pointer Then
					Return True
				End If
			Case GrammarNodeType.number
				If token.Type = ASTool.Token.TokenType.const_number Then
					Return True
				End If

			Case GrammarNodeType.terminal
				If (token.Type = ASTool.Token.TokenType.other OrElse token.Type = ASTool.Token.TokenType.identifier) _
						And String.Equals(node.Name, token.StringValue, StringComparison.Ordinal) Then 'node.Name = token.StringValue Then
					Return True
				End If

			Case Else

		End Select

		Return False


		'If node.Type = GrammarNodeType.null Then
		'	Return True
		'End If

		'If node.Type = GrammarNodeType.whitespace Then
		'          If token.Type = ASTool.Token.TokenType.eof Or token.Type = ASTool.Token.TokenType.whitespace Or token.Type = ASTool.Token.TokenType.comments Then
		'              Return True
		'          End If
		'      End If

		'If node.Type = GrammarNodeType.conststring And (token.Type = ASTool.Token.TokenType.const_string Or token.Type = Token.TokenType.const_regexp) Then
		'	Return True
		'ElseIf node.Type = GrammarNodeType.eof And token.Type = ASTool.Token.TokenType.eof Then
		'	Return True
		'      End If

		'      If node.Type = GrammarNodeType.identifier And token.Type = ASTool.Token.TokenType.identifier Then
		'          Return True
		'      End If

		'      If node.Type = GrammarNodeType.label And token.Type = Token.TokenType.label Then
		'          Return True
		'      End If

		'      If node.Type = GrammarNodeType.this And token.Type = Token.TokenType.this_pointer Then
		'          Return True
		'      End If

		'      If node.Type = GrammarNodeType.super And token.Type = Token.TokenType.super_pointer Then
		'          Return True
		'      End If

		'      If node.Type = GrammarNodeType.number And token.Type = ASTool.Token.TokenType.const_number Then
		'          Return True
		'      End If

		'If node.Type = GrammarNodeType.terminal And (token.Type = ASTool.Token.TokenType.other Or token.Type = ASTool.Token.TokenType.identifier) _
		'	And String.Equals(node.Name, token.StringValue, StringComparison.Ordinal) Then 'node.Name = token.StringValue Then
		'	Return True
		'End If

		'Return False

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


		System.IO.File.WriteAllText("D:\ASCRIPT\ASTool\ASTool\GrammarNodes.txt", sb.ToString())

	End Sub




End Class
