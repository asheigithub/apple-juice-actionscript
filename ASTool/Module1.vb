Imports ASTool.AS3

Module Module1

	Public Function IIf(cond As Boolean, iftrue As String, iffalse As String) As String

		If cond Then
			Return iftrue
		Else
			Return iffalse
		End If

	End Function


	Sub Main()

        'ReadAllUsedImage()

        'Return


        Dim lex As New Lex("D:\ASTool\ASTool\PG1.txt")
        Dim words = lex.GetWords(My.Resources.PG1) 'My.Computer.FileSystem.ReadAllText(lex.File))
        'words.Write(New ConSrcOut(), 0)


        'Dim i As Integer = 1
        'Dim t = i.GetType()


        Dim grammar As New Grammar(words)

        parseAS3(grammar)
        Return


		'Dim teststring As String = "package{} 6/2/3+(3- 4) * (5 +6)+7//;{123{;13+456;;}{}}"

		'Dim teststring As String = "package{} for(var i:int;i<5;i++){ (function($what){alert($what)})(i)} " '"package{ var i=new int(); }"

		'Dim teststring As String = My.Computer.FileSystem.ReadAllText("D:\ASToolTestScript\src\Main.as")

		Dim teststring As String = System.IO.File.ReadAllText("D:\ASToolTestScriptOutput\GYLite(v1.10)\GYLite\component\text\GYText.as")

		'Dim teststring As String = My.Computer.FileSystem.ReadAllText("C:\Program Files (x86)\Adobe Gaming SDK 1.4\Frameworks\Away3D\src\away3d\loaders\parsers\DAEParser.as")
		'Dim teststring As String = My.Computer.FileSystem.ReadAllText("D:\三国2\Sanguo2\trunk\program\client\SG2Mobile\SG2Mobile\src\net\fishluv\sanguo2\client\SceneConsole.as")


		'Dim teststring As String = My.Computer.FileSystem.ReadAllText("D:\flash-x\flashxTools\src\makeMovie\MakeMovieFont.as")

		'Dim teststring As String = My.Computer.FileSystem.ReadAllText("D:\三国2\Sanguo2\trunk\program\client\SG2Mobile\SG2Mobile\src\net\fishluv\sanguo2\client\ui\IForm.as")

		Dim tree = grammar.ParseTree(teststring, AS3LexKeywords.LEXKEYWORDS, AS3LexKeywords.LEXSKIPBLANKWORDS, teststring)








        'Console.WriteLine("表达式树")

        Dim treestr = (tree.GetTreeString())

        'Console.WriteLine(treestr)

        If grammar.hasErrorFF Then
            'Console.WriteLine(grammar.ErrorFFStr)
        End If

        If Not grammar.hasError Then

            'Console.WriteLine(getTreevalue(tree.Root))

            'grammar.ListGNodes()

            'perorderVisit(tree.Root)

            'Console.WriteLine()
            'Console.WriteLine(numberstack.Pop())

            Dim proj As New AS3Proj()

            Dim srcout As New ConSrcOut()

            Dim analyser As New AS3FileGrammarAnalyser(proj, teststring)
			If analyser.Analyse(tree) Then

				For Each p In proj.SrcFiles  'proj.Packages.Values
					p.Write(0, srcout)
				Next
			Else
				Console.WriteLine(analyser.err.ToString())
            End If


        End If



            Console.ReadLine()
    End Sub

    Private Sub parseAS3(grammar As Grammar)
        Dim rootpaths As String() = {"D:\fishluv_svn_repository\NewSG\trunk\program\client\SG2Mobile3\src",
                                     "D:\fishluv_svn_repository\NewSG\trunk\program\client\biz_csharp\sgx.client.biz.common\implement",
                                        "D:\fishluv_svn_repository\NewSG\trunk\program\client\biz_csharp\sgx.client.biz.common\interface",
                                     "D:\fishluv_svn_repository\NewSG\trunk\program\client\biz_csharp\sgx.client.biz.world\implement",
                                     "D:\fishluv_svn_repository\NewSG\trunk\program\client\biz_csharp\sgx.client.biz.world\interface"}

        'Dim rootpaths As String() = {"E:\新建文件夹\Box2DFlashAS3 2.1a\Source"}
        'Dim rootpaths As String() = {"D:\ASToolTestScriptOutput\GYLite(v1.10)\GYLite"}


        'Dim rootpaths As String() = {"D:\ASCRIPT\ascript-master\ascript-master\src"}
        'Dim rootpaths As String() = {" D:\flash-x\game\src"}
        'Dim rootpaths As String() = {"D:\FrameSyncServer\flclient\src"}

        'Dim rootpaths As String() = {"C:\Program Files\Adobe Gaming SDK 1.3\Frameworks\Away3D\src"}
        'Dim rootpaths As String() = {"C:\Program Files (x86)\Adobe Gaming SDK 1.4\Frameworks\DragonBones\src"}
        'Dim rootpaths As String() = {"C:\Program Files\Adobe Gaming SDK 1.3\Frameworks\Feathers"}
        'Dim rootpaths As String() = {"C:\Program Files\Adobe Gaming SDK 1.4\Frameworks\Starling"}
        'Dim rootpaths As String() = {"C:\Program Files\Adobe Gaming SDK 1.3\StarlingDemo\src"}
        'Dim rootpaths As String() = {"C:\Program Files\Adobe Gaming SDK 1.3\Frameworks\Box2DFlashAS3 2.1a"}
        'Dim rootpaths As String() = {"C:\Program Files\Adobe Gaming SDK 1.3\Frameworks\RookieEditor-master\RookieEditor-master\RookieEditor"}
        'Dim rootpaths As String() = {"C:\Program Files\Adobe Gaming SDK 1.3\Frameworks\plane\src"}
        'Dim rootpaths As String() = {"C:\Program Files\Adobe Gaming SDK 1.3\Frameworks\puremvc-as3-standard-framework-master"}
        'Dim rootpaths As String() = {"C:\Program Files\Adobe Gaming SDK 1.3\Frameworks\Flint_4_0_1_src\Flint_4_0_1_src"}
        'Dim rootpaths As String() = {"C:\Program Files\Adobe Gaming SDK 1.3\Frameworks\TweenMaxAS3"}
        'Dim rootpaths As String() = {"C:\Program Files\Adobe Gaming SDK 1.3\Frameworks\nest3d-master\nest3d-master"}
        'Dim rootpaths As String() = {"C:\Program Files\Adobe Gaming SDK 1.3\Frameworks\D5RPG\D5Power-master"}
        'Dim rootpaths As String() = {"D:\flres\rjlwomanrun\view~\GytCoverFlow"}
        'Dim rootpaths As String() = {"D:\flres\rjlwomanrun\Components2(TransitionStyle)"}

        'Dim rootpaths As String() = {"D:\ASToolTestScript\src"}

        'Dim rootpaths As String() = {"E:\layabox\MornUI\MornUI"}


        Dim files As New List(Of String)
        For Each r In rootpaths
			files.AddRange(System.IO.Directory.GetFiles(r, "*.as", IO.SearchOption.AllDirectories))
		Next



        Dim proj As New AS3Proj()
        For Each fs In files

            grammar.hasError = False

			Dim strProg = System.IO.File.ReadAllText(fs)

			If String.IsNullOrEmpty(strProg) Then
                Continue For
            End If

            Dim tree = grammar.ParseTree(strProg, AS3LexKeywords.LEXKEYWORDS, AS3LexKeywords.LEXSKIPBLANKWORDS, fs)
            'Dim tt = tree.GetTreeString()
            If grammar.hasError Then
                Console.ForegroundColor = ConsoleColor.Red
                Console.WriteLine(fs & "有误")
                Console.ResetColor()
                GoTo over
            Else
                Console.ForegroundColor = ConsoleColor.Green
                Console.WriteLine(fs & "分析语法树成功")
                Console.ResetColor()


                Dim analyser As New AS3FileGrammarAnalyser(proj, fs)
				If Not analyser.Analyse(tree) Then
					Console.ForegroundColor = ConsoleColor.Red
					Console.WriteLine(analyser.err.ToString())
					Console.ResetColor()
					GoTo over

				End If

			End If
        Next



        Dim srcout As New ConSrcOut()
        For Each p In proj.SrcFiles
            
            p.Write(0, srcout)

        Next


        proj.Analyse()


over:


        Console.WriteLine("OK")
        Console.ReadLine()
    End Sub


    'Private visited As New HashSet(Of GrammerExpr)
    'Private numberstack As New Stack(Of Double)
    '''' <summary>
    '''' 前序遍历
    '''' </summary>
    '''' <param name="treenode"></param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Private Sub perorderVisit(treenode As GrammerExpr)

    '    If visited.Contains(treenode) Then
    '        Return
    '    Else
    '        visited.Add(treenode)
    '    End If


    '    If treenode.GrammerLeftNode.Name = "Expression" Then
    '        perorderVisit(treenode.Nodes(0))
    '    ElseIf treenode.GrammerLeftNode.Name = "PlusOpt" _
    '        Or treenode.GrammerLeftNode.Name = "MultiplyOpt" Then

    '        If treenode.Nodes.Count > 0 Then
    '            perorderVisit(treenode.Nodes(1))

    '            Dim v2 = numberstack.Pop()
    '            Dim v1 = numberstack.Pop()

    '            Console.Write(treenode.Nodes(0).MatchedToken.StringValue)


    '            Select Case treenode.Nodes(0).MatchedToken.StringValue
    '                Case "+"
    '                    numberstack.Push(v1 + v2)
    '                Case "-"
    '                    numberstack.Push(v1 - v2)
    '                Case "*"
    '                    numberstack.Push(v1 * v2)
    '                Case "/"
    '                    numberstack.Push(v1 / v2)
    '                Case Else

    '            End Select


    '        End If

    '    ElseIf treenode.GrammerLeftNode.Name = "Multiply" Then
    '        perorderVisit(treenode.Nodes(0))
    '    ElseIf treenode.GrammerLeftNode.Name = "Unit" Then
    '        If treenode.Nodes.Count > 1 Then
    '            perorderVisit(treenode.Nodes(1))
    '        Else
    '            numberstack.Push(Double.Parse(treenode.Nodes(0).MatchedToken.StringValue))

    '            Console.Write(treenode.Nodes(0).MatchedToken.StringValue)
    '        End If

    '    End If

    '    For Each node In treenode.Nodes
    '        perorderVisit(node)
    '    Next


    'End Sub



    'Private Function getTreevalue(treenode As GrammerExpr) As String
    '    If treenode.GrammerLeftNode.Type = GrammarNodeType.non_terminal Then
    '        Dim result As String = ""


    '        For i = 0 To treenode.Nodes.Count - 1
    '            Dim temptv = getTreevalue(treenode.Nodes(i))


    '            result = result & temptv
    '        Next




    '        Return result

    '    Else

    '        Return treenode.MatchedToken.StringValue
    '    End If
    'End Function





    'Private Sub ReadAllUsedImage()
    '    Dim rootpaths As String() = {"D:\fishluv_svn_repository\NewSG\trunk\program\client\SG2Mobile3\src"
    '                                 }

    '    Dim files As New List(Of String)
    '    For Each r In rootpaths
    '        files.AddRange(My.Computer.FileSystem.GetFiles(r, FileIO.SearchOption.SearchAllSubDirectories, "*.as"))
    '    Next

    '    Dim str As New HashSet(Of String)

    '    For Each asfile In files
    '        Dim lex As New Lex(asfile)

    '        Dim tokens = lex.GetWords(My.Computer.FileSystem.ReadAllText(asfile))
    '        For i = 0 To tokens.Count - 1
    '            If tokens(i).Type = Token.TokenType.const_string Then
    '                str.Add(tokens(i).StringValue)
    '            End If
    '        Next
    '    Next



    '    Dim pngs = My.Computer.FileSystem.GetFiles("D:\fishluv_svn_repository\NewSG\trunk\program\client\R___UIImages\commimages1", FileIO.SearchOption.SearchAllSubDirectories, "*.png")

    '    Dim gameimages As New List(Of String)

    '    For Each p In pngs
    '        Dim np = p.Replace("D:\fishluv_svn_repository\NewSG\trunk\program\client\R___UIImages\commimages1\", "commimage1/").Replace("\", "/")

    '        If Not str.Contains(np) Then
    '            Console.WriteLine(np)
    '        End If

    '    Next




    '    Console.ReadLine()


    'End Sub

End Module
