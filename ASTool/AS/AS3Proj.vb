Namespace AS3
    ''' <summary>
    ''' 解析出的AS3项目
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AS3Proj

        Public SrcFiles As New List(Of AS3SrcFile)

        'Public AS3Files As New List(Of AS3FileGrammarAnalyser)

        ''' <summary>
        ''' 综合分析对象类型等
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Analyse()
            For Each s In SrcFiles
                If Not s.Package Is Nothing Then
                    For Each t In s.Package      '包成员
                        FindMemberType(t, s, s.Package.Import)
                        If TypeOf t Is IAS3MemberList Then
                            VisitFunctionMemberType(t, s, s.Package.Import)
                        End If

                    Next

                    If Not s.Package.MainClass Is Nothing Then
                        For Each m In s.Package.MainClass
                            FindMemberType(m, s, s.Package.Import)
                            If TypeOf m Is IAS3MemberList Then
                                VisitFunctionMemberType(m, s, s.Package.Import)
                            End If
                        Next
                    End If

                    If Not s.Package.MainInterface Is Nothing Then
                        For Each m In s.Package.MainInterface
                            FindMemberType(m, s, s.Package.Import)
                            If TypeOf m Is IAS3MemberList Then
                                VisitFunctionMemberType(m, s, s.Package.Import)
                            End If
                        Next
                    End If

                    For Each o In s.OutPackagePrivateScope

                        FindMemberType(o, s, s.OutPackageImports)
                        If TypeOf o Is IAS3MemberList Then
                            VisitFunctionMemberType(o, s, s.OutPackageImports)
                        End If

                    Next

                End If
            Next

            Console.WriteLine("====缺少对象=====")
            Console.ForegroundColor = ConsoleColor.DarkYellow

            For Each t In notfoundTypeStrs
                Console.WriteLine(t)

            Next

            Console.ResetColor()


        End Sub
        Private Sub VisitFunctionMemberType(f As IAS3MemberList, srcfile As AS3SrcFile, import As List(Of AS3Import))
            For Each m In f
                FindMemberType(m, srcfile, import)
                If TypeOf m Is AS3Function Then
                    VisitFunctionMemberType(m, srcfile, import)
                End If
            Next
        End Sub

        Private notfoundTypeStrs As New HashSet(Of String)

        ''' <summary>
        ''' 分析对象类型
        ''' </summary>
        ''' <param name="member"></param>
        ''' <remarks></remarks>
        Private Sub FindMemberType(member As AS3Member, srcfile As AS3SrcFile, import As List(Of AS3Import))


            'Console.WriteLine(member.token.sourceFile & " 行" & member.token.line + 1 & " 列" & member.token.ptr & " " & member.TypeStr)

            Dim str = member.TypeStr
            Dim rank As Integer = 0
            While str.StartsWith("Vector.<")
                rank += 1
                str = str.Substring(8, str.Length - 9)
            End While

            If rank = 0 Then
                member.TypeDefine = FindTypeByTypeStr(str, member.token, srcfile, import)
            Else

                Dim v As New AS3.AS3MemberDateType()
                v.DateType = MemberDataTypes.Vector

                Dim root = v

                rank -= 1
                While rank > 0
                    Dim b As New AS3.AS3MemberDateType()
                    b.DateType = MemberDataTypes.Vector

                    v.TypeValue = b

                    v = b
                    rank -= 1
                End While


                v.TypeValue = FindTypeByTypeStr(str, member.token, srcfile, import)

                If Not v.TypeValue Is Nothing Then
                    member.TypeDefine = root
                End If

            End If


            If member.TypeDefine Is Nothing Then
                notfoundTypeStrs.Add(member.TypeStr)

            End If




        End Sub

        Private Function FindTypeByTypeStr(typestr As String, token As Token, srcfile As AS3SrcFile, import As List(Of AS3Import)) As IMemberDataType

            Select Case typestr
                Case "*"
                    Return AS3.AS3MemberDateType.ANY
                Case "void"
                    Return AS3.AS3MemberDateType.VOID
                Case "String"
                    Return AS3.AS3MemberDateType._STRING
                Case "int"
                    Return AS3.AS3MemberDateType.INT
                Case "uint"
                    Return AS3.AS3MemberDateType.UINT
                Case "Number"
                    Return AS3.AS3MemberDateType.NUMBER
                Case "Array"
                    Return AS3.AS3MemberDateType.ARRAY
                Case "namespace"
                    Return AS3.AS3MemberDateType._NAMESPACE
                Case "Boolean"
                    Return AS3.AS3MemberDateType._BOOLEAN
                Case "Object"
                    Return AS3.AS3MemberDateType._OBJECT
                Case "Function"
                    Return AS3.AS3MemberDateType._FUNCTION
                Case "Class"
                    Return AS3.AS3MemberDateType.CLASSDEF
                Case Else
            End Select


            Dim importtypes As New HashSet(Of AS3ProgramElement)
            If Not srcfile.Package.MainClass Is Nothing Then
                importtypes.Add(srcfile.Package.MainClass)

                For Each c In srcfile.Package.MainClass.innerClass
                    importtypes.Add(c)
                Next
                For Each i In srcfile.Package.MainClass.innerInterface
                    importtypes.Add(i)
                Next

            End If
            If Not srcfile.Package.MainInterface Is Nothing Then
                importtypes.Add(srcfile.Package.MainInterface)

                For Each c In srcfile.Package.MainInterface.innerClass
                    importtypes.Add(c)
                Next
                For Each i In srcfile.Package.MainInterface.innerInterface
                    importtypes.Add(i)
                Next

            End If

            '***导入顶级包和本级包****
            For Each p In SrcFiles
                If Not p.Package Is Nothing Then

                    If Not p.Package.MainClass Is Nothing AndAlso (p.Package.Name = "" Or (Not srcfile.Package Is Nothing AndAlso p.Package.Name = srcfile.Package.Name)) Then
                        importtypes.Add(p.Package.MainClass)
                    End If
                    If Not p.Package.MainInterface Is Nothing AndAlso (p.Package.Name = "" Or (Not srcfile.Package Is Nothing AndAlso p.Package.Name = srcfile.Package.Name)) Then
                        importtypes.Add(p.Package.MainInterface)
                    End If

                End If
            Next

            For Each imp In import
                If imp.Name.EndsWith(".*") Then
                    Dim testname As String = imp.Name.Substring(0, imp.Name.Length - 2)
                    For Each p In SrcFiles
                        If Not p.Package Is Nothing Then
                            If p.Package.Name = testname Then
                                If Not p.Package.MainClass Is Nothing Then
                                    importtypes.Add(p.Package.MainClass)
                                End If
                                If Not p.Package.MainInterface Is Nothing Then
                                    importtypes.Add(p.Package.MainInterface)
                                End If
                            End If
                        End If
                    Next

                Else
                    Dim testname As String = imp.Name
                    For Each p In SrcFiles
                        If Not p.Package Is Nothing Then

                            If Not p.Package.MainClass Is Nothing AndAlso p.Package.Name & "." & p.Package.MainClass.Name = testname Then
                                importtypes.Add(p.Package.MainClass)
                            End If
                            If Not p.Package.MainInterface Is Nothing AndAlso p.Package.Name & "." & p.Package.MainInterface.Name = testname Then
                                importtypes.Add(p.Package.MainInterface)
                            End If

                        End If
                    Next



                End If


            Next

            Dim fclass As New HashSet(Of IMemberDataType)

            If typestr.IndexOf(".") >= 0 Then
                '***完全限定***
                For Each c In importtypes
                    If TypeOf c Is AS3Class Then
                        If typestr = CType(c, AS3Class).Package.Name & "." & CType(c, AS3Class).Name Then
                            fclass.Add(c)
                            Exit For
                        End If
                    ElseIf TypeOf c Is AS3Interface Then
                        If typestr = CType(c, AS3Interface).Package.Name & "." & CType(c, AS3Interface).Name Then
                            fclass.Add(c)
                            Exit For
                        End If

                    End If

                Next

            Else

                For Each c In importtypes
                    If TypeOf c Is AS3Class Then
                        If typestr = CType(c, AS3Class).Name Then
                            fclass.Add(c)

                        End If
                    End If
                    If TypeOf c Is AS3Interface Then
                        If typestr = CType(c, AS3Interface).Name Then
                            fclass.Add(c)

                        End If

                    End If

                Next

            End If

            If fclass.Count > 1 Then
                Console.WriteLine(String.Format("命名冲突{0} 行{1} 列{2} 类型:{3}", token.sourceFile, token.line, token.ptr, typestr))
                Return Nothing
            ElseIf fclass.Count = 0 Then
                Console.WriteLine(String.Format("类型未找到...{0} 行{1} 列{2} 类型:{3}", token.sourceFile, token.line, token.ptr, typestr))
                Return Nothing
            Else

                Dim arr(0) As IMemberDataType

                fclass.CopyTo(arr)

                Return arr(0)
            End If




        End Function


    End Class

End Namespace

