Imports ASTool.AS3
Public Class AS3FileGrammarAnalyser

    Private proj As AS3Proj


    Public srcFile As String

    Public Sub New(proj As AS3Proj, srcFile As String)
        Me.proj = proj
        'proj.AS3Files.Add(Me)

        Me.srcFile = srcFile


    End Sub

    ''' <summary>
    ''' 当前在解析的包
    ''' </summary>
    ''' <remarks></remarks>
    Private currentPackage As AS3Package


    ''' <summary>
    ''' 当前文件的包外代码
    ''' </summary>
    ''' <remarks></remarks>
    Public outpackageprivatescope As AS3MemberListBase


    Private inpackimports As List(Of AS3Import)
    Private outpackimports As List(Of AS3Import)

    ''' <summary>
    ''' 当前主类或主接口
    ''' </summary>
    ''' <remarks></remarks>
    Public currentMain As Object
    ''' <summary>
    ''' 当前包外类包外接口
    ''' </summary>
    ''' <remarks></remarks>
    Private currentInner As Object

    Private currentImpllist As List(Of String)

    'Private classaccessStack As Stack(Of String)

    Private memberaccessStack As Stack(Of String)

    Private dynamticObjStack As Stack(Of Hashtable)

    ''' <summary>
    ''' 当前的成员符号表
    ''' </summary>
    ''' <remarks></remarks>
    Private MemberScopeStack As Stack(Of IAS3MemberList)

    Private ExprStack As Stack(Of AS3Expression)



    Private metapropertystack As Stack(Of AS3Expression)

    Private currentparsefuncstack As Stack(Of AS3Function)

    Private currentparseExprListStack As Stack(Of List(Of AS3Expression))

    Private currentSwitchStack As Stack(Of AS3Switch)

    Private currentTryStack As Stack(Of AS3Try)


    Private currentLabelStack As Stack(Of String)

    Private current_expression_canfunctioninvoke As Stack(Of Boolean)

    Private current_new_operator As Stack(Of GrammerExpr)
    Private current_visiting_expression As Stack(Of GrammerExpr)

    Private as3file As New AS3SrcFile()

    Public err As GrammarException

    Public Function Analyse(grammer As Grammar, tree As GrammerTree) As Boolean

        If Not currentPackage Is Nothing Then
            Throw New Exception("不能重复使用")
        End If

        currentPackage = Nothing
        inpackimports = New List(Of AS3Import)()
        outpackimports = New List(Of AS3Import)()

        'classaccessStack = New Stack(Of String)()
        memberaccessStack = New Stack(Of String)()

        MemberScopeStack = New Stack(Of IAS3MemberList)()

        outpackageprivatescope = New AS3MemberListBase(Nothing)

        MemberScopeStack.Push(outpackageprivatescope)

        ExprStack = New Stack(Of AS3Expression)()

        dynamticObjStack = New Stack(Of Hashtable)()

        metapropertystack = New Stack(Of AS3Expression)()

        currentparsefuncstack = New Stack(Of AS3Function)()

        currentparseExprListStack = New Stack(Of List(Of AS3Expression))()

        currentSwitchStack = New Stack(Of AS3Switch)

        currentTryStack = New Stack(Of AS3Try)

        currentLabelStack = New Stack(Of String)

        current_expression_canfunctioninvoke = New Stack(Of Boolean)

        current_new_operator = New Stack(Of GrammerExpr)()
        current_visiting_expression = New Stack(Of GrammerExpr)()

        currentMain = Nothing
        currentImpllist = Nothing

        Try
            VisitNodes(tree.Root)
        Catch ex As GrammarException
            err = ex
            Return False
        End Try

        'If outpackageprivatescope.Count > 0 Then
        '    If TypeOf currentMain Is AS3Class Then
        '        CType(currentMain, AS3Class).outpackageinnermembers.AddRange(outpackageprivatescope)
        '    End If
        'End If
        If currentparseExprListStack.Count > 0 Then
            Throw New Exception()
        End If

        as3file.srcFile = srcFile
        as3file.OutPackageImports.AddRange(outpackimports)

        proj.SrcFiles.Add(as3file)

        as3file.OutPackagePrivateScope = outpackageprivatescope

        Return True
    End Function




#Region "遍历节点"

    Private nodevisited As New Dictionary(Of GrammerExpr, Action(Of GrammerExpr))

    Private visitednodes As New HashSet(Of GrammerExpr)()

    Private Sub VisitNodes(node As GrammerExpr)

        If visitednodes.Contains(node) Then
            Return
        End If

        Dim name = node.GrammerLeftNode.Name
        If name.StartsWith("F_") Or name.StartsWith("K_") Then
            name = name.Substring(2)
        End If

        Select Case name
            Case "PACKAGE"
                _PACKAGE(node)
            Case "PACKAGE_NAME"
                _PACKAGE_NAME(node)
            Case "PACKAGEBODY"
                _PACKAGEBODY(node)
            Case "OUT_PACKAGE"
                _OUT_PACKAGE(node)
            Case "PACKAGE_STMTS"
                _PACKAGE_STMTS(node)
            Case "ClassPath"
                _ClassPath(node)
            Case "PACKAGE_STMT"
                _PACKAGE_STMT(node)
            Case "PACKAGE_STMTS1"
                _PACKAGE_STMTS1(node)
            Case "PACKAGE_BLOCK"
                _PACKAGE_BLOCK(node)
            Case "PACKAGE_BLOCK_2"
                _PACKAGE_BLOCK_2(node)
            Case "PACKAGE_EXPR"
                _PACKAGE_EXPR(node)
            Case "ACCESS_DEF"
                _ACCESS_DEF(node)
            Case "Syntax"
                _Syntax(node)
            Case "Import"
                _Import(node)
            Case "Import_ClassPath"
                _Import_ClassPath(node)
            Case "Import_ClassName"
                _Import_ClassName(node)
            Case "Import_ClassPath_1"
                _Import_ClassPath_1(node)
            Case "Use"
                _Use(node)
            Case "ClassName"
                _ClassName(node)
            Case "ClassPath_1"
                _ClassPath_1(node)
            Case "ClassMetaProperty"
                _ClassMetaProperty(node)
            Case "Access"
                _Access(node)
            Case "NSAccess"
                _NSAccess(node)
            Case "NameSpace"
                _NameSpace(node)
            Case "NameSpaceDefaultValue"
                _NameSpaceDefaultValue(node)
            Case "Expression"
                _Expression(node)
            Case "DefClass"
                _DefClass(node)
            Case "Extends"
                _Extends(node)
            Case "Implements"
                _Implements(node)
            Case "ClassBody"
                _ClassBody(node)
            Case "ImplList"
                _ImplList(node)
            Case "ImplList1"
                _ImplList1(node)
            Case "DefInterface"
                _DefInterface(node)
            Case "InterfaceName"
                _InterfaceName(node)
            Case "ACCESS_KEYWORD"
                _ACCESS_KEYWORD(node)
            Case "Function"
                _Function(node)
            Case "Const"
                _Const(node)
            Case "Variable"
                _Variable(node)
            Case "Stmts"
                _Stmts(node)
            Case "Stmt"
                _Stmt(node)
            Case "StmtList"
                _StmtList(node)
            Case "BLOCK"
                _BLOCK(node)
            Case "BP2"
                _BP2(node)
            Case "ACCESS_MEMBER"
                _ACCESS_MEMBER(node)
            Case "EatAnSemicolon"
                _EatAnSemicolon(node)
            Case "IF"
                _IF(node)
            Case "FOR_STMT"
                _FOR_STMT(node)
            Case "WHILE"
                _WHILE(node)
            Case "WITH"
                _WITH(node)
            Case "DO"
                _DO(node)
            Case "TRY"
                _TRY(node)
            Case "THROW"
                _THROW(node)
            Case "SWITCH"
                _SWITCH(node)
            Case "Return"
                _Return(node)
            Case "Break"
                _Break(node)
            Case "Continue"
                _Continue(node)
            Case "ACCESS_MEMBER_KEYWORD"
                _ACCESS_MEMBER_KEYWORD(node)
            Case "ExpressionList"
                _ExpressionList(node)
            Case "AExprList"
                _AExprList(node)
            Case "ID_EABLED_KEYWORD"
                _ID_EABLED_KEYWORD(node)
            Case "DefineType"
                _DefineType(node)
            Case "DefineTypeVector"
                _DefineTypeVector(node)
            Case "VariableDefineList"
                _VariableDefineList(node)
            Case "VariableDefine"
                _VariableDefine(node)
            Case "VariableDefineList1"
                _VariableDefineList1(node)
            Case "VariableName"
                _VariableName(node)
            Case "VariableDEFType"
                _VariableDEFType(node)
            Case "VariableDefaultValue"
                _VariableDefaultValue(node)
            Case "VariableType"
                _VariableType(node)
            Case "ConstDefineList"
                _ConstDefineList(node)
            Case "ConstDefine"
                _ConstDefine(node)
            Case "ConstDefineList1"
                _ConstDefineList1(node)
            Case "ConstName"
                _ConstName(node)
            Case "ConstDEFType"
                _ConstDEFType(node)
            Case "ConstDefaultValue"
                _ConstDefaultValue(node)
            Case "ConstType"
                _ConstType(node)
            Case "FunctionProperty"
                _FunctionProperty(node)
            Case "Parameters"
                _Parameters(node)
            Case "FunctionReturnType"
                _FunctionReturnType(node)
            Case "FunctionCode"
                _FunctionCode(node)
            Case "FunctionBody"
                _FunctionBody(node)
            Case "FunctionName"
                _FunctionName(node)
            Case "FunctionType"
                _FunctionType(node)
            Case "Parameter_list"
                _Parameter_list(node)
            Case "Parameter_Array"
                _Parameter_Array(node)
            Case "Parameter"
                _Parameter(node)
            Case "Parameter_list1"
                _Parameter_list1(node)
            Case "ParameterDEFType"
                _ParameterDEFType(node)
            Case "ParameterDefaultValue"
                _ParameterDefaultValue(node)
            Case "ParameterName"
                _ParameterName(node)
            Case "ParameterType"
                _ParameterType(node)
            Case "ReturnValue"
                _ReturnValue(node)
            Case "IFElse"
                _IFElse(node)
            Case "FORTYPE"
                _FORTYPE(node)
            Case "Each"
                _Each(node)
            Case "FOR_FORIN"
                _FOR_FORIN(node)
            Case "Each_TEMP1"
                _Each_TEMP1(node)
            Case "ForVar"
                _ForVar(node)
            Case "Each_TEMP2"
                _Each_TEMP2(node)
            Case "FOR_TEMP1"
                _FOR_TEMP1(node)
            Case "FOR_TEMP2"
                _FOR_TEMP2(node)
            Case "FOR"
                _FOR(node)
            Case "FORIN"
                _FORIN(node)
            Case "FORPART2"
                _FORPART2(node)
            Case "FORPART3"
                _FORPART3(node)
            Case "FORPART1"
                _FORPART1(node)
            Case "DO_CONDITION"
                _DO_CONDITION(node)
            Case "TRYBLOCK"
                _TRYBLOCK(node)
            Case "CATCHLIST"
                _CATCHLIST(node)
            Case "FINALLY"
                _FINALLY(node)
            Case "THROWEXCEPTION"
                _THROWEXCEPTION(node)
            Case "CATCH"
                _CATCH(node)
            Case "CATCHLIST1"
                _CATCHLIST1(node)
            Case "CATCHBLOCK"
                _CATCHBLOCK(node)
            Case "FINALLYBLOCK"
                _FINALLYBLOCK(node)
            Case "CASEBODY"
                _CASEBODY(node)
            Case "SWITCH_CASE"
                _SWITCH_CASE(node)
            Case "CASESTMT"
                _CASESTMT(node)
            Case "CASELIST"
                _CASELIST(node)
            Case "CASELIST1"
                _CASELIST1(node)
            Case "Assigning"
                _Assigning(node)
            Case "Ternary"
                _Ternary(node)
            Case "AssigningOpt"
                _AssigningOpt(node)
            Case "LogicOr"
                _LogicOr(node)
            Case "TernaryOpt"
                _TernaryOpt(node)
            Case "LogicAnd"
                _LogicAnd(node)
            Case "LogicOrOpt"
                _LogicOrOpt(node)
            Case "BitOr"
                _BitOr(node)
            Case "LogicAndOpt"
                _LogicAndOpt(node)
            Case "BitXor"
                _BitXor(node)
            Case "BitOrOpt"
                _BitOrOpt(node)
            Case "BitAnd"
                _BitAnd(node)
            Case "BitXorOpt"
                _BitXorOpt(node)
            Case "LogicEQ"
                _LogicEQ(node)
            Case "BitAndOpt"
                _BitAndOpt(node)
            Case "Logic"
                _Logic(node)
            Case "LogicEQOpt"
                _LogicEQOpt(node)
            Case "BitShift"
                _BitShift(node)
            Case "LogicOpt"
                _LogicOpt(node)
            Case "Plus"
                _Plus(node)
            Case "BitShiftOpt"
                _BitShiftOpt(node)
            Case "Multiply"
                _Multiply(node)
            Case "PlusOpt"
                _PlusOpt(node)
            Case "Unary"
                _Unary(node)
            Case "MultiplyOpt"
                _MultiplyOpt(node)
            Case "Unit"
                _Unit(node)
            Case "AccessOpt"
                _AccessOpt(node)
            Case "NSAccessOpt"
                _NSAccessOpt(node)
            Case "UnitSuffix"
                _UnitSuffix(node)
            Case "Call"
                _Call(node)
            Case "Object"
                _Object(node)
            Case "E4XAccess"
                _E4XAccess(node)
            Case "Array"
                _Array(node)
            Case "Vector"
                _Vector(node)
            Case "Argements"
                _Argements(node)
            Case "ArrayElements"
                _ArrayElements(node)
            Case "ArrayExpressionList"
                _ArrayExpressionList(node)
            Case "Expression_1"
                _Expression_1(node)
            Case "ArrayCommaOpt"
                _ArrayCommaOpt(node)
            Case "CommaOpt"
                _CommaOpt(node)
            Case "CommaOpt_1"
                _CommaOpt_1(node)
            Case "ACommaOpt"
                _ACommaOpt(node)
            Case "ACommaOpt_1"
                _ACommaOpt_1(node)
            Case "VectorConstructor"
                _VectorConstructor(node)
            Case "ObjectBody"
                _ObjectBody(node)
            Case "ObjMembers"
                _ObjMembers(node)
            Case "ObjMember"
                _ObjMember(node)
            Case "ObjMembers1"
                _ObjMembers1(node)
            Case "CaseDefultOrXMLUse_1"
                _CaseDefultOrXMLUse_1(node)
            Case "E4XAccessOpt"
                _E4XAccessOpt(node)
            Case "E4XFilter"
                _E4XFilter(node)
            Case "E4XNSAPPEND"
                _E4XNSAPPEND(node)
            Case "New"
                _New(node)
            Case "NewClassOrVector"
                _NewClassOrVector(node)
            Case "NewOpt"
                _NewOpt(node)
            Case "ThisSuper"
                _ThisSuper(node)
            Case "A_FC"
                _A_FC(node)
        End Select

        visitednodes.Add(node)

        For Each c In node.Nodes
            VisitNodes(c)
        Next

        If nodevisited.ContainsKey(node) Then
            nodevisited(node)(node)
            nodevisited.Remove(node)
        End If

    End Sub

#End Region


    Sub _PACKAGE(node As GrammerExpr)

        'If node.Nodes.Count = 0 Then Return

        Dim packagename As String = GrammerExpr.getNodeValue(node.Nodes(1))

        'If Not proj.Packages.ContainsKey(packagename) Then
        currentPackage = New AS3Package(node.MatchedToken)
        currentPackage.Name = packagename


        If String.IsNullOrEmpty(currentPackage.Name) Then
            currentPackage.IsTopLevel = True
        End If

        'proj.Packages.Add(packagename, currentPackage)
        'Else
        'currentPackage = proj.Packages(packagename)
        'End If


        MemberScopeStack.Push(currentPackage)

        as3file.Package = currentPackage
        currentPackage.AS3File = as3file

    End Sub
    Sub _PACKAGE_NAME(node As GrammerExpr)
        Return
    End Sub
    Sub _PACKAGEBODY(node As GrammerExpr)
        Return
    End Sub
    Sub _OUT_PACKAGE(node As GrammerExpr)

        'If TypeOf currentMain Is AS3Class Then
        '    CType(currentMain, AS3Class).Import.AddRange(inpackimports)
        'ElseIf TypeOf currentMain Is AS3Interface Then
        '    CType(currentMain, AS3Interface).Import.AddRange(inpackimports)
        'End If

        currentPackage.Import.AddRange(inpackimports)

        '进入包外。'''
        currentPackage = Nothing
        If TypeOf MemberScopeStack.Peek() Is AS3Package Then
            MemberScopeStack.Pop()
        Else
            Throw New Exception("程序域嵌套错误啦")
        End If


    End Sub
    Sub _PACKAGE_STMTS(node As GrammerExpr)

    End Sub
    Sub _ClassPath(node As GrammerExpr)

    End Sub
    Sub _PACKAGE_STMT(node As GrammerExpr)

    End Sub
    Sub _PACKAGE_STMTS1(node As GrammerExpr)

        If node.Nodes.Count = 0 Then

            'If TypeOf currentMain Is AS3Class Then
            '    For Each i In CType(currentMain, AS3Class).innerClass
            '        i.Import.AddRange(outpackimports)
            '    Next
            '    For Each i In CType(currentMain, AS3Class).innerInterface
            '        i.Import.AddRange(outpackimports)
            '    Next
            'ElseIf TypeOf currentMain Is AS3Interface Then
            '    For Each i In CType(currentMain, AS3Interface).innerClass
            '        i.Import.AddRange(outpackimports)
            '    Next
            '    For Each i In CType(currentMain, AS3Interface).innerInterface
            '        i.Import.AddRange(outpackimports)
            '    Next
            'End If
        End If

    End Sub
    Sub _PACKAGE_BLOCK(node As GrammerExpr)

        ':<PACKAGE_BLOCK> ::="{"<Label> <PACKAGE_BLOCK_2> | <PACKAGE_EXPR> ;

        If node.Nodes.Count = 3 Then

            Dim lbl As String = Nothing
            If node.Nodes(1).Nodes.Count = 2 Then
                lbl = node.Nodes(1).Nodes(0).MatchedToken.StringValue
            End If

            Dim blockstmts As New List(Of IAS3Stmt)
            MemberScopeStack.Peek().StamentsStack.Push(blockstmts)

            VisitNodes(node.Nodes(2))

            blockstmts = MemberScopeStack.Peek().StamentsStack.Pop()

            Dim block As New AS3Block(node.MatchedToken)
            block.CodeList = blockstmts
            block.label = lbl

            MemberScopeStack.Peek().StamentsStack.Peek().Add(block)

        End If

    End Sub
    Sub _PACKAGE_BLOCK_2(node As GrammerExpr)

    End Sub
    Sub _PACKAGE_EXPR(node As GrammerExpr)
        '<ACCESS_DEF>|<Syntax>|<ExpressionList>;
        If node.Nodes(0).GrammerLeftNode.Name = "ExpressionList" Then
            VisitNodes(node.Nodes(0))
            Dim stmt As New AS3StmtExpressions(node.MatchedToken)
            stmt.as3exprlist = currentparseExprListStack.Pop()
            outpackageprivatescope.StamentsStack.Peek().Add(stmt)
        End If

    End Sub
    Sub _ACCESS_DEF(node As GrammerExpr)

    End Sub
    Sub _Syntax(node As GrammerExpr)

    End Sub



    Sub _Import(node As GrammerExpr)

        Dim import As New AS3Import()
        import.Name = GrammerExpr.getNodeValue(node.Nodes(1))

        If currentPackage Is Nothing Then
            '包外
            outpackimports.Add(import)
        Else
            '包内
            inpackimports.Add(import)
        End If

    End Sub
    Sub _Import_ClassPath(node As GrammerExpr)

    End Sub
    Sub _Import_ClassName(node As GrammerExpr)

    End Sub
    Sub _Import_ClassPath_1(node As GrammerExpr)

    End Sub
    Sub _Use(node As GrammerExpr)
        '"use" "namespace"  <ClassPath>;//| "default" "xml" "namespace" "=" <Expression>; 

        Dim use As New AS3Use(node.MatchedToken)

        'If node.Nodes.Count = 3 Then
        use.NameSpaceStr = GrammerExpr.getNodeValue(node.Nodes(2))


        'Else

        '    Dim as3expr As New AS3Expression
        '    VisitNodes(node.Nodes(4))
        '    as3expr.exprStepList = node.Nodes(4).exprsteplist
        '    as3expr.Value = MemberScopeStack.Peek().ExprDataStack.Pop()

        '    use.IsDefaultXMLNameSpace = True
        '    use.xmlnsexpr = as3expr

        'End If
        MemberScopeStack.Peek().StamentsStack.Peek().Add(use)


    End Sub

    Sub _CaseDefultOrXMLUse_1(node As GrammerExpr)
        '<CaseDefultOrXMLUse_1> ::= "xml" "namespace" "=" <Expression> |
        ' ":" <CASESTMT> ; 



        If node.Nodes.Count = 4 Then
            Dim use As New AS3Use(node.MatchedToken)
            Dim as3expr As New AS3Expression(node.Nodes(3).MatchedToken)
            VisitNodes(node.Nodes(3))
            as3expr.exprStepList = node.Nodes(3).exprsteplist
            as3expr.Value = MemberScopeStack.Peek().ExprDataStack.Pop()

            use.IsDefaultXMLNameSpace = True
            use.xmlnsexpr = as3expr
            MemberScopeStack.Peek().StamentsStack.Peek().Add(use)

            'Else
            '    '**case
            '    Dim sw = currentSwitchStack.Peek()

            '    Dim as3case As New AS3SwitchCase()
            '    as3case.IsDefault = True

            '    Dim body As New List(Of IAS3Stmt)
            '    MemberScopeStack.Peek().StamentsStack.Push(body)

            '    VisitNodes(node.Nodes(1))

            '    as3case.Body = MemberScopeStack.Peek().StamentsStack.Pop()

            '    sw.CaseList.Add(as3case)
        End If



    End Sub

    Sub _ClassName(node As GrammerExpr)

    End Sub
    Sub _ClassPath_1(node As GrammerExpr)

    End Sub
    Sub _ClassMetaProperty(node As GrammerExpr)
        '::="[" <Access> "]";
        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        VisitNodes(node.Nodes(1))
        node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)
    End Sub

    Sub _NameSpace(node As GrammerExpr)

        '<NameSpace>         ::="namespace" <ClassPath><NameSpaceDefaultValue>;
        '<NameSpaceDefaultValue> ::="="<Expression>|null;

        Dim as3namespace As New AS3NameSpace(node.MatchedToken)
        as3namespace.Name = GrammerExpr.getNodeValue(node.Nodes(1))
        as3namespace.Package = currentPackage

        'If classaccessStack.Count > 0 Then
        '    as3namespace.Access.SetValue(classaccessStack)
        'End If

        If memberaccessStack.Count > 0 Then
            as3namespace.Access.SetValue(memberaccessStack)
        End If

        If node.Nodes(2).Nodes.Count > 0 Then
            as3namespace.URI = GrammerExpr.getNodeValue(node.Nodes(2))

        End If

        MemberScopeStack.Peek().Add(as3namespace)


        'currentPackage.NameSpaces.Add(as3namespace)

    End Sub
    Sub _NameSpaceDefaultValue(node As GrammerExpr)

    End Sub


    Sub _Expression(node As GrammerExpr)

        current_visiting_expression.Push(node)

        VisitNodes(node.Nodes(0))

        node.exprsteplist = node.Nodes(0).exprsteplist

        current_visiting_expression.Pop()

    End Sub

    Sub _Access(node As GrammerExpr)
        '<NSAccess><AccessOpt> | <Function><A_FC>;     

        node.exprsteplist = New AS3.Expr.AS3ExprStepList()

        If node.Nodes.Count = 3 Then

            VisitNodes(node.Nodes(0))
            VisitNodes(node.Nodes(1))
            VisitNodes(node.Nodes(2))

            node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)
            node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)
            node.exprsteplist.AddRange(node.Nodes(2).exprsteplist)
        Else
            VisitNodes(node.Nodes(0))
            VisitNodes(node.Nodes(1))

            node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)
            node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)

        End If

    End Sub

    Sub _ThisSuper(node As GrammerExpr)
        '<ThisSuper> ::=this <AccessOpt> |super <AccessOpt> ;
        node.exprsteplist = New Expr.AS3ExprStepList()
        If node.Nodes(0).MatchedToken.Type = Token.TokenType.this_pointer Then
            Dim v As New AS3.Expr.AS3DataStackElement()
            v.Data = New AS3.Expr.AS3DataValue()
            v.Data.FF1Type = Expr.FF1DataValueType.this_pointer
            v.Data.Value = node.Nodes(0).MatchedToken.StringValue
            MemberScopeStack.Peek().ExprDataStack.Push(v)
        ElseIf node.Nodes(0).MatchedToken.Type = Token.TokenType.super_pointer Then
            Dim v As New AS3.Expr.AS3DataStackElement()
            v.Data = New AS3.Expr.AS3DataValue()
            v.Data.FF1Type = Expr.FF1DataValueType.super_pointer
            v.Data.Value = node.Nodes(0).MatchedToken.StringValue
            MemberScopeStack.Peek().ExprDataStack.Push(v)

        End If

        VisitNodes(node.Nodes(1))
        node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)
    End Sub

    Sub _NSAccess(node As GrammerExpr)
        '<ThisSuper> ::=this <AccessOpt> |super <AccessOpt> ;
        '<Unit><NSAccessOpt>|<ThisSuper>;

        node.exprsteplist = New AS3.Expr.AS3ExprStepList()

        If node.Nodes.Count = 2 Then
            VisitNodes(node.Nodes(0))
            VisitNodes(node.Nodes(1))
            '

            node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)
            node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)
            '
        ElseIf node.Nodes.Count = 1 Then
            '<E4XFilter>

            VisitNodes(node.Nodes(0))
            node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)

        End If





    End Sub

    Sub _DefClass(node As GrammerExpr)
        Dim as3class As New AS3Class(node.MatchedToken)
        as3class.Name = GrammerExpr.getNodeValue(node.Nodes(1))
        as3class.Package = as3file.Package

        MemberScopeStack.Push(as3class)

        'If classaccessStack.Count > 0 Then
        '    as3class.Access.SetValue(classaccessStack)
        'End If

        If memberaccessStack.Count > 0 Then
            as3class.Access.SetValue(memberaccessStack)
        End If

        If metapropertystack.Count > 0 Then
            'as3class.Meta = metapropertystack.Pop()
            as3class.Meta = New List(Of AS3Meta)
            While metapropertystack.Count > 0
                as3class.Meta.Add(metapropertystack.Pop())
            End While

        End If

        If Not currentPackage Is Nothing Then
            currentPackage.MainClass = (as3class)
            currentMain = as3class
        Else

            If Not currentMain Is Nothing Then
                If TypeOf currentMain Is AS3Class Then
                    CType(currentMain, AS3Class).innerClass.Add(as3class)
                ElseIf TypeOf currentMain Is AS3Interface Then
                    CType(currentMain, AS3Interface).innerClass.Add(as3class)
                End If
            End If

            as3class.IsOutPackage = True
            currentInner = as3class
        End If



    End Sub
    Sub _Extends(node As GrammerExpr)

        If TypeOf MemberScopeStack.Peek() Is AS3Class Then
            currentImpllist = CType(MemberScopeStack.Peek(), AS3Class).ExtendsNames
        End If

        If TypeOf MemberScopeStack.Peek() Is AS3Interface Then
            currentImpllist = CType(MemberScopeStack.Peek(), AS3Interface).ExtendsNames
        End If

    End Sub
    Sub _Implements(node As GrammerExpr)
        If TypeOf MemberScopeStack.Peek() Is AS3Class Then
            currentImpllist = CType(MemberScopeStack.Peek(), AS3Class).ImplementsNames
        ElseIf TypeOf currentInner Is AS3Class Then
            currentImpllist = CType(MemberScopeStack.Peek(), AS3Class).ImplementsNames
        End If
    End Sub
    Sub _ClassBody(node As GrammerExpr)
        currentImpllist = Nothing

        nodevisited.Add(node, AddressOf _ClassBodyVisited)



    End Sub
    Sub _ClassBodyVisited(node As GrammerExpr)
        MemberScopeStack.Pop()
    End Sub



    Sub _ImplList(node As GrammerExpr)
        currentImpllist.Add(GrammerExpr.getNodeValue(node.Nodes(0)))
    End Sub
    Sub _ImplList1(node As GrammerExpr)

    End Sub
    Sub _DefInterface(node As GrammerExpr)
        Dim as3inteface As New AS3Interface(node.MatchedToken)
        as3inteface.Name = GrammerExpr.getNodeValue(node.Nodes(1))
        as3inteface.Package = as3file.Package

        MemberScopeStack.Push(as3inteface)

        'If classaccessStack.Count > 0 Then
        '    as3inteface.Access.SetValue(classaccessStack)
        'End If

        If memberaccessStack.Count > 0 Then
            as3inteface.Access.SetValue(memberaccessStack)
        End If

        If metapropertystack.Count > 0 Then
            'as3inteface.Meta = metapropertystack.Pop()
            as3inteface.Meta = New List(Of AS3Meta)
            While metapropertystack.Count > 0
                as3inteface.Meta.Add(metapropertystack.Pop())
            End While
        End If

        If Not currentPackage Is Nothing Then
            currentPackage.MainInterface = (as3inteface)
            currentMain = as3inteface
        Else
            If Not currentMain Is Nothing Then
                If TypeOf currentMain Is AS3Class Then
                    CType(currentMain, AS3Class).innerInterface.Add(as3inteface)
                ElseIf TypeOf currentMain Is AS3Interface Then
                    CType(currentMain, AS3Interface).innerInterface.Add(as3inteface)
                End If
            End If
            currentInner = as3inteface

            as3inteface.IsOutPackage = True

        End If
    End Sub
    Sub _InterfaceName(node As GrammerExpr)

    End Sub
    Sub _ACCESS_KEYWORD(node As GrammerExpr)

        If node.Nodes(0).GrammerLeftNode.Type <> GrammarNodeType.non_terminal Then
            'classaccessStack.Push(node.Nodes(0).MatchedToken.StringValue)
            memberaccessStack.Push(node.Nodes(0).MatchedToken.StringValue)
        Else

            'Throw New Exception("<ClassMetaProperty>未实现")

            Dim meta As New AS3Meta(node.MatchedToken)
            VisitNodes(node.Nodes(0))
            meta.exprStepList = node.Nodes(0).exprsteplist



            meta.Value = MemberScopeStack.Peek().ExprDataStack.Pop()

            metapropertystack.Push(meta)

        End If


    End Sub

    Sub _ACCESS_MEMBER_KEYWORD(node As GrammerExpr)
        memberaccessStack.Push(node.Nodes(0).MatchedToken.StringValue)
    End Sub

    Sub _ACCESS_MEMBER(node As GrammerExpr)

        If TypeOf MemberScopeStack.Peek() Is AS3Class Or TypeOf MemberScopeStack.Peek() Is AS3Interface Then
            If node.Nodes(0).GrammerLeftNode.Name = "ExpressionList" Then
                If node.MatchedToken.Type = Token.TokenType.identifier And node.MatchedToken.StringValue = "function" Then
                    '***方法定义***
                    VisitNodes(node.Nodes(0))

                    MemberScopeStack.Peek().ExprDataStack.Pop()

                    currentparseExprListStack.Pop()

                Else
                    '**元数据定义***

                    VisitNodes(node.Nodes(0))

                    Dim expr As New AS3Meta(node.MatchedToken)
                    expr.exprStepList = node.Nodes(0).exprsteplist
                    expr.Value = MemberScopeStack.Peek().ExprDataStack.Pop()

                    If expr.Value.IsReg Then
                        Throw New GrammarException(node.MatchedToken, "多个meta数据用;分开。")
                    End If

                    metapropertystack.Push(expr)

                    currentparseExprListStack.Pop()
                End If
            End If

        Else


            If node.Nodes(0).GrammerLeftNode.Name = "ExpressionList" Then

                If node.MatchedToken.Type = Token.TokenType.identifier And node.MatchedToken.StringValue = "function" Then
                    VisitNodes(node.Nodes(0))

                    MemberScopeStack.Peek().ExprDataStack.Pop()

                    Dim func As AS3Function = MemberScopeStack.Peek()
                    Dim stmt As New AS3StmtExpressions(node.MatchedToken)
                    stmt.as3exprlist = currentparseExprListStack.Pop()
                    func.StamentsStack.Peek().Add(stmt)


                Else

                    '*****表达式语句****
                    VisitNodes(node.Nodes(0))

                    Dim exprlist As New AS3StmtExpressions(node.MatchedToken)

                    'exprlist.grammerExpr = node.Nodes(0)

                    exprlist.as3exprlist = currentparseExprListStack.Pop()

                    MemberScopeStack.Peek().StamentsStack.Peek().Add(exprlist)


                End If


            End If

        End If

    End Sub

    Sub _Function(node As GrammerExpr)



        Dim func As New AS3Function(node.MatchedToken)

        If metapropertystack.Count > 0 Then
            'func.Meta = metapropertystack.Pop()
            func.Meta = New List(Of AS3Meta)
            While metapropertystack.Count > 0
                func.Meta.Add(metapropertystack.Pop())
            End While
        End If

        If memberaccessStack.Count > 0 Then
            Dim temp = memberaccessStack.ToArray()
            func.Access.SetValue(memberaccessStack)

            If TypeOf MemberScopeStack.Peek() Is AS3Class Or TypeOf MemberScopeStack.Peek() Is AS3Interface Then
                func.IsMethod = True
            Else

                func.IsMethod = False

                For Each t In temp
                    memberaccessStack.Push(t)
                Next
            End If
        Else
            If Not (TypeOf MemberScopeStack.Peek() Is AS3Function) Then
                func.Access.IsInternal = True
            End If
            If TypeOf MemberScopeStack.Peek() Is AS3Class Or TypeOf MemberScopeStack.Peek() Is AS3Interface Then
                func.IsMethod = True
            End If
        End If

        Dim funcprop = node.Nodes(1)
        If funcprop.Nodes.Count = 1 Then
            func.Name = GrammerExpr.getNodeValue(funcprop.Nodes(0))

            If (funcprop.Nodes(0).Nodes.Count = 0) Then
                func.IsAnonymous = True
            End If

        Else
            If funcprop.Nodes(1).Nodes.Count = 0 Then
                func.Name = funcprop.Nodes(0).MatchedToken.StringValue

                If (funcprop.Nodes(0).Nodes.Count = 0) Then
                    func.IsAnonymous = True
                End If

            Else
                If funcprop.Nodes(0).MatchedToken.StringValue = "get" Then
                    func.IsGet = True
                ElseIf funcprop.Nodes(0).MatchedToken.StringValue = "set" Then
                    func.IsSet = True
                End If

                func.Name = GrammerExpr.getNodeValue(funcprop.Nodes(1))

                If (funcprop.Nodes(1).Nodes.Count = 0) Then
                    func.IsAnonymous = True
                End If

            End If

        End If

        If TypeOf MemberScopeStack.Peek() Is AS3Class Then
            If CType(MemberScopeStack.Peek(), AS3Class).Name = func.Name Then
                func.IsConstructor = True
            End If

        End If


        func.TypeStr = getFuncTypeStr(node.Nodes(5))

        func.FunctionBody = node.Nodes(6)

        func.ParentScope = MemberScopeStack.Peek()


        currentparsefuncstack.Push(func)
        VisitNodes(node.Nodes(3))
        currentparsefuncstack.Pop()

        MemberScopeStack.Peek().Add(func)
        MemberScopeStack.Push(func)

        VisitNodes(node.Nodes(6))



        MemberScopeStack.Pop()

        '***Unit部分

        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        Dim data As New AS3.Expr.AS3DataStackElement()
        data.Data = New AS3.Expr.AS3DataValue()
        data.Data.FF1Type = Expr.FF1DataValueType.as3_function
        data.Data.Value = func

        MemberScopeStack.Peek().ExprDataStack.Push(data)



        '****

    End Sub
    Sub _Const(node As GrammerExpr)

    End Sub
    Sub _Variable(node As GrammerExpr)

    End Sub

    Sub _Stmts(node As GrammerExpr)

    End Sub
    Sub _Stmt(node As GrammerExpr)

    End Sub
    Sub _StmtList(node As GrammerExpr)

    End Sub
    Sub _BLOCK(node As GrammerExpr)
        '<BLOCK> ::="{"<Label> <BP2>     ;

        Dim lbl As String = Nothing
        If node.Nodes(1).Nodes.Count = 2 Then
            lbl = node.Nodes(1).Nodes(0).MatchedToken.StringValue
        End If


        Dim blockstmts As New List(Of IAS3Stmt)
        MemberScopeStack.Peek().StamentsStack.Push(blockstmts)

        VisitNodes(node.Nodes(2))

        blockstmts = MemberScopeStack.Peek().StamentsStack.Pop()

        Dim block As New AS3Block(node.MatchedToken)
        block.CodeList = blockstmts
        block.label = lbl

        MemberScopeStack.Peek().StamentsStack.Peek().Add(block)


    End Sub
    Sub _BP2(node As GrammerExpr)

    End Sub


    Sub _EatAnSemicolon(node As GrammerExpr)


        memberaccessStack.Clear()

    End Sub
    Sub _IF(node As GrammerExpr)

        '<IF>   ::="if" <Label> "(" <ExpressionList> ")" <Stmt><IFElse>;
        '<IFElse> ::="else" <Stmt>|null;

        Dim as3if As New AS3IF(node.MatchedToken)

        Dim lbl As String = Nothing
        If node.Nodes(1).Nodes.Count = 2 Then
            lbl = node.Nodes(1).Nodes(0).MatchedToken.StringValue
        End If

        as3if.label = lbl

        Dim condition As New AS3StmtExpressions(node.MatchedToken)

        VisitNodes(node.Nodes(3))

        condition.as3exprlist = currentparseExprListStack.Pop()

        as3if.Condition = condition

        Dim truepass As New List(Of IAS3Stmt)
        MemberScopeStack.Peek().StamentsStack.Push(truepass)

        VisitNodes(node.Nodes(5))

        as3if.TruePass = MemberScopeStack.Peek().StamentsStack.Pop()

        If node.Nodes(6).Nodes.Count > 0 Then

            Dim elsepass As New List(Of IAS3Stmt)
            MemberScopeStack.Peek().StamentsStack.Push(elsepass)

            VisitNodes(node.Nodes(6).Nodes(1))

            as3if.FalsePass = MemberScopeStack.Peek().StamentsStack.Pop()

        End If

        MemberScopeStack.Peek().StamentsStack.Peek().Add(as3if)


    End Sub
    Sub _FOR_STMT(node As GrammerExpr)
        '<FOR_STMT>  ::="for"<Label><FORTYPE>;

        Dim lbl As String = Nothing
        If node.Nodes(1).Nodes.Count = 2 Then

            lbl = node.Nodes(1).Nodes(0).MatchedToken.StringValue

        End If

        currentLabelStack.Push(lbl)


    End Sub

    Sub _WITH(node As GrammerExpr)
        '<WITH>		::="with"  <Label> "(" <ExpressionList> ")" "{" <Stmts> "}";
        Dim lbl As String = Nothing
        If node.Nodes(1).Nodes.Count = 2 Then
            lbl = node.Nodes(1).Nodes(0).MatchedToken.StringValue
        End If

        Dim as3with As New AS3.AS3With(node.MatchedToken)
        as3with.label = lbl

        Dim withObj As New AS3StmtExpressions(node.MatchedToken)

        VisitNodes(node.Nodes(3))

        withObj.as3exprlist = currentparseExprListStack.Pop()

        as3with.withObject = withObj


        Dim body As New List(Of IAS3Stmt)

        MemberScopeStack.Peek().StamentsStack.Push(body)
        VisitNodes(node.Nodes(6))

        as3with.Body = MemberScopeStack.Peek().StamentsStack.Pop()


        MemberScopeStack.Peek().StamentsStack.Peek().Add(as3with)

    End Sub

    Sub _WHILE(node As GrammerExpr)
        '::="while"<Label> "(" <ExpressionList> ")" <Stmt>;

        Dim lbl As String = Nothing
        If node.Nodes(1).Nodes.Count = 2 Then
            lbl = node.Nodes(1).Nodes(0).MatchedToken.StringValue
        End If

        Dim as3while As New AS3While(node.MatchedToken)
        as3while.label = lbl

        Dim conditon As New AS3StmtExpressions(node.MatchedToken)

        VisitNodes(node.Nodes(3))

        conditon.as3exprlist = currentparseExprListStack.Pop()

        as3while.Condition = conditon

        Dim body As New List(Of IAS3Stmt)

        MemberScopeStack.Peek().StamentsStack.Push(body)
        VisitNodes(node.Nodes(5))

        as3while.Body = MemberScopeStack.Peek().StamentsStack.Pop()


        MemberScopeStack.Peek().StamentsStack.Peek().Add(as3while)

    End Sub
    Sub _DO(node As GrammerExpr)
        '<DO>        ::= "do"<Label> <Stmt><DO_CONDITION>;
        '<DO_CONDITION> ::="while" "(" <ExpressionList> ")";


        Dim lbl As String = Nothing
        If node.Nodes(1).Nodes.Count = 2 Then
            lbl = node.Nodes(1).Nodes(0).MatchedToken.StringValue
        End If


        Dim as3do As New AS3DoWhile(node.MatchedToken)
        as3do.label = lbl


        Dim body As New List(Of IAS3Stmt)

        MemberScopeStack.Peek().StamentsStack.Push(body)
        VisitNodes(node.Nodes(2))

        as3do.Body = MemberScopeStack.Peek().StamentsStack.Pop()


        Dim conditon As New AS3StmtExpressions(node.MatchedToken)

        VisitNodes(node.Nodes(3).Nodes(2))

        conditon.as3exprlist = currentparseExprListStack.Pop()

        as3do.Condition = conditon



        MemberScopeStack.Peek().StamentsStack.Peek().Add(as3do)

    End Sub
    Sub _TRY(node As GrammerExpr)
        '"try" <Label> "{" <TRYBLOCK> "}" <CATCHLIST> <FINALLY>;
        Dim lbl As String = Nothing
        If node.Nodes(1).Nodes.Count = 2 Then
            lbl = node.Nodes(1).Nodes(0).MatchedToken.StringValue
        End If
        Dim as3try As New AS3Try(node.MatchedToken)
        as3try.label = lbl

        currentTryStack.Push(as3try)

        Dim tryblock As New List(Of IAS3Stmt)
        MemberScopeStack.Peek().StamentsStack.Push(tryblock)
        VisitNodes(node.Nodes(3))
        as3try.TryBlock = MemberScopeStack.Peek().StamentsStack.Pop()


        VisitNodes(node.Nodes(5))


        Dim finallyblock As New List(Of IAS3Stmt)
        MemberScopeStack.Peek().StamentsStack.Push(finallyblock)
        VisitNodes(node.Nodes(6))

        If node.Nodes(6).MatchedToken.StringValue = "finally" Then

            as3try.FinallyBlock = MemberScopeStack.Peek().StamentsStack.Pop()


        End If



        MemberScopeStack.Peek().StamentsStack.Peek().Add(currentTryStack.Pop())
    End Sub
    Sub _THROW(node As GrammerExpr)
        '"throw" <THROWEXCEPTION>;
        '<THROWEXCEPTION> ::= <Expression> |null; 

        Dim as3throw As New AS3Throw(node.MatchedToken)

        If node.Nodes(1).Nodes.Count > 0 Then
            Dim as3expr As New AS3Expression(node.Nodes(1).MatchedToken)

            VisitNodes(node.Nodes(1).Nodes(0))


            as3expr.exprStepList = node.Nodes(1).Nodes(0).exprsteplist
            as3expr.Value = MemberScopeStack.Peek().ExprDataStack.Pop()

            as3throw.Exception = as3expr



        End If

        MemberScopeStack.Peek().StamentsStack.Peek().Add(as3throw)

    End Sub
    Sub _SWITCH(node As GrammerExpr)
        '"switch" <Label> "(" <Expression> ")" "{" <CASEBODY> ;
        Dim sw As New AS3Switch(node.MatchedToken)

        Dim lbl As String = Nothing
        If node.Nodes(1).Nodes.Count = 2 Then
            lbl = node.Nodes(1).Nodes(0).MatchedToken.StringValue
        End If
        sw.label = lbl

        VisitNodes(node.Nodes(3))

        Dim expr As New AS3Expression(node.Nodes(3).MatchedToken)
        expr.exprStepList = node.Nodes(3).exprsteplist
        expr.Value = MemberScopeStack.Peek().ExprDataStack.Pop()
        sw.Expr = expr

        currentSwitchStack.Push(sw)

        VisitNodes(node.Nodes(6))

        MemberScopeStack.Peek().StamentsStack.Peek().Add(currentSwitchStack.Pop())

    End Sub

    Sub _SWITCH_CASE(node As GrammerExpr)
        '<SWITCH_CASE> ::="case" <Expression> ":"<CASESTMT> | "default:" <CASESTMT> ;

        Dim sw = currentSwitchStack.Peek()

        If node.Nodes.Count = 4 Then
            Dim as3case As New AS3SwitchCase(node.MatchedToken)

            Dim expr As New AS3Expression(node.Nodes(1).MatchedToken)

            VisitNodes(node.Nodes(1))

            expr.exprStepList = node.Nodes(1).exprsteplist
            expr.Value = MemberScopeStack.Peek().ExprDataStack.Pop()

            as3case.Condition = expr

            Dim body As New List(Of IAS3Stmt)
            MemberScopeStack.Peek().StamentsStack.Push(body)

            VisitNodes(node.Nodes(3))

            as3case.Body = MemberScopeStack.Peek().StamentsStack.Pop()

            Dim reg = AS3.Expr.AS3DataStackElement.MakeReg(MemberScopeStack.Peek().NextRegId())
            as3case.holdreg = reg

            sw.CaseList.Add(as3case)
        Else
            'VisitNodes(node.Nodes(0))

            Dim as3case As New AS3SwitchCase(node.MatchedToken)
            as3case.IsDefault = True

            Dim body As New List(Of IAS3Stmt)
            MemberScopeStack.Peek().StamentsStack.Push(body)

            VisitNodes(node.Nodes(1))

            as3case.Body = MemberScopeStack.Peek().StamentsStack.Pop()

            Dim reg = AS3.Expr.AS3DataStackElement.MakeReg(MemberScopeStack.Peek().NextRegId())
            as3case.holdreg = reg

            sw.CaseList.Add(as3case)
        End If


    End Sub

    Sub _Return(node As GrammerExpr)
        '<Return>  ::="return"<ReturnValue>;
        '<ReturnValue> ::=<ExpressionList>|null;

        Dim as3return As New AS3Return(node.MatchedToken)

        If node.Nodes(1).Nodes.Count > 0 Then
            Dim as3stmt = New AS3StmtExpressions(node.MatchedToken)

            VisitNodes(node.Nodes(1).Nodes(0))

            as3stmt.as3exprlist = currentparseExprListStack.Pop()

            as3return.ReturnValue = as3stmt

        End If

        MemberScopeStack.Peek().StamentsStack.Peek().Add(as3return)
    End Sub
    Sub _Break(node As GrammerExpr)

        Dim nodeflag As String = Nothing
        If node.Nodes(1).Nodes.Count > 0 Then
            nodeflag = node.Nodes(1).Nodes(0).MatchedToken.StringValue
        End If

        MemberScopeStack.Peek().StamentsStack.Peek().Add(New AS3Break(node.MatchedToken) With {.breakFlag = nodeflag})
    End Sub
    Sub _Continue(node As GrammerExpr)

        Dim nodeflag As String = Nothing
        If node.Nodes(1).Nodes.Count > 0 Then
            nodeflag = node.Nodes(1).Nodes(0).MatchedToken.StringValue
        End If


        MemberScopeStack.Peek().StamentsStack.Peek().Add(New AS3Continue(node.MatchedToken) With {.continueFlag = nodeflag})
    End Sub

    Sub _AExprList(node As GrammerExpr)
        '<Expression><CommaOpt> ;

        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        VisitNodes(node.Nodes(0))

        '**访问CommaOpt***
        VisitNodes(node.Nodes(1))

        node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)
        node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)




    End Sub

    Sub _ExpressionList(node As GrammerExpr)
        '::=<Expression><CommaOpt> ;

        Dim as3exprs As New List(Of AS3Expression)()

        currentparseExprListStack.Push(as3exprs)


        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        VisitNodes(node.Nodes(0))

        Dim as3expr As New AS3Expression(node.Nodes(0).MatchedToken)
        as3expr.exprStepList = node.Nodes(0).exprsteplist
        as3expr.Value = MemberScopeStack.Peek().ExprDataStack.Peek()

        as3exprs.Add(as3expr)


        '**访问CommaOpt***
        VisitNodes(node.Nodes(1))

        node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)
        node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)

    End Sub
    Sub _ID_EABLED_KEYWORD(node As GrammerExpr)

    End Sub
    Sub _DefineType(node As GrammerExpr)

    End Sub
    Sub _DefineTypeVector(node As GrammerExpr)

    End Sub
    Sub _VariableDefineList(node As GrammerExpr)

    End Sub
    Sub _VariableDefine(node As GrammerExpr)

        Dim variable As New AS3Variable(node.MatchedToken)

        If metapropertystack.Count > 0 Then
            'variable.Meta = metapropertystack.Pop()
        End If

        If memberaccessStack.Count > 0 Then
            Dim temp = memberaccessStack.ToArray()
            variable.Access.SetValue(memberaccessStack)

            For Each t In temp
                memberaccessStack.Push(t)
            Next
        Else
            If Not (TypeOf MemberScopeStack.Peek() Is AS3Function) Then
                variable.Access.IsInternal = True
            End If
        End If

        variable.Name = GrammerExpr.getNodeValue(node.Nodes(0))

        variable.TypeStr = getDefTypeStr(node.Nodes(1))

        If node.Nodes(2).Nodes.Count > 0 Then
            Dim expr As New AS3Expression(node.Nodes(2).Nodes(1).MatchedToken)

            VisitNodes(node.Nodes(2).Nodes(1))
            expr.exprStepList = node.Nodes(2).Nodes(1).exprsteplist

            expr.Value = MemberScopeStack.Peek().ExprDataStack.Pop()

            If Not expr.Value.IsReg AndAlso expr.Value.Data.FF1Type = AS3.Expr.FF1DataValueType.as3_function Then
                MemberScopeStack.Peek().Remove(expr.Value.Data.Value)
            End If

            variable.ValueExpr = expr
        End If

        MemberScopeStack.Peek().Add(variable)

    End Sub

    ''' <summary>
    ''' [VariableDEFType] ::= ":" [VariableType]|":*"|null;
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getDefTypeStr(node As GrammerExpr) As String
        If node.Nodes.Count = 0 Then
            Return "*"
        ElseIf node.Nodes.Count = 2 Then
            Return GrammerExpr.getNodeValue(node.Nodes(1))
        Else
            Return "*"
        End If
    End Function

    ''' <summary>
    ''' [FunctionReturnType] ::=":"[FunctionType]|":*"|null;
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getFuncTypeStr(node As GrammerExpr) As String
        '":"<FunctionType>|":*"|":void"|null;
        If node.Nodes.Count = 0 Then
            Return "*"
        ElseIf node.Nodes.Count = 2 Then
            Return GrammerExpr.getNodeValue(node.Nodes(1))
        Else
            Return node.MatchedToken.StringValue.Substring(1)
        End If
    End Function


    Sub _VariableDefineList1(node As GrammerExpr)
        If node.Nodes.Count = 0 Then
            memberaccessStack.Clear()
        End If
    End Sub
    Sub _VariableName(node As GrammerExpr)

    End Sub
    Sub _VariableDEFType(node As GrammerExpr)

    End Sub
    Sub _VariableDefaultValue(node As GrammerExpr)

    End Sub
    Sub _VariableType(node As GrammerExpr)

    End Sub
    Sub _ConstDefineList(node As GrammerExpr)

    End Sub
    Sub _ConstDefine(node As GrammerExpr)

        Dim as3const As New AS3Const(node.MatchedToken)
        If metapropertystack.Count > 0 Then
            'as3const.Meta = metapropertystack.Pop()
            as3const.Meta = New List(Of AS3Meta)
            While metapropertystack.Count > 0
                as3const.Meta.Add(metapropertystack.Pop())
            End While
        End If
        If memberaccessStack.Count > 0 Then
            Dim temp = memberaccessStack.ToArray()
            as3const.Access.SetValue(memberaccessStack)
            For Each t In temp
                memberaccessStack.Push(t)
            Next
        Else
            If Not (TypeOf MemberScopeStack.Peek() Is AS3Function) Then
                as3const.Access.IsInternal = True
            End If

        End If


        as3const.Name = GrammerExpr.getNodeValue(node.Nodes(0))

        as3const.TypeStr = getDefTypeStr(node.Nodes(1))

        If node.Nodes(2).Nodes.Count > 0 Then
            Dim expr As New AS3Expression(node.Nodes(2).Nodes(1).MatchedToken)

            VisitNodes(node.Nodes(2).Nodes(1))
            expr.exprStepList = node.Nodes(2).Nodes(1).exprsteplist

            expr.Value = MemberScopeStack.Peek().ExprDataStack.Pop()

            as3const.ValueExpr = expr
        End If

        MemberScopeStack.Peek().Add(as3const)

    End Sub
    Sub _ConstDefineList1(node As GrammerExpr)
        If node.Nodes.Count = 0 Then
            memberaccessStack.Clear()
        End If
    End Sub
    Sub _ConstName(node As GrammerExpr)

    End Sub
    Sub _ConstDEFType(node As GrammerExpr)

    End Sub
    Sub _ConstDefaultValue(node As GrammerExpr)

    End Sub
    Sub _ConstType(node As GrammerExpr)

    End Sub
    Sub _FunctionProperty(node As GrammerExpr)

    End Sub
    Sub _Parameters(node As GrammerExpr)

    End Sub
    Sub _FunctionReturnType(node As GrammerExpr)

    End Sub
    Sub _FunctionCode(node As GrammerExpr)
        '**当functionCode运行结束时出栈
        'nodevisited.Add(node, AddressOf _FunctionCodeVisited)

        If node.Nodes.Count <> 3 Then

            If node.MatchedToken.Type <> Token.TokenType.identifier And node.MatchedToken.StringValue <> ";" _
                And node.MatchedToken.StringValue <> "}" Then

                Throw New GrammarException(node.MatchedToken, "Syntax error: '" & node.MatchedToken.StringValue & "' is not allowed here")


            End If


        End If

    End Sub
    Private Sub _FunctionCodeVisited(node As GrammerExpr)
        'MemberScopeStack.Pop()
    End Sub

    Sub _FunctionBody(node As GrammerExpr)

    End Sub
    Sub _FunctionName(node As GrammerExpr)

    End Sub
    Sub _FunctionType(node As GrammerExpr)

    End Sub
    Sub _Parameter_list(node As GrammerExpr)

    End Sub
    Sub _Parameter_Array(node As GrammerExpr)

    End Sub
    Sub _Parameter(node As GrammerExpr)
        ' identifier <ParameterDEFType><ParameterDefaultValue>|<Parameter_Array>;

        Dim func As AS3Function = currentparsefuncstack.Peek()

        Dim para As New AS3Parameter(node.MatchedToken)

        If node.Nodes.Count = 1 Then
            para.IsArrPara = True

            para.Name = GrammerExpr.getNodeValue(node.Nodes(0).Nodes(1))
            para.TypeStr = getDefTypeStr(node.Nodes(0).Nodes(2))
        Else
            para.Name = GrammerExpr.getNodeValue(node.Nodes(0))
            para.TypeStr = getDefTypeStr(node.Nodes(1))

            If node.Nodes(2).Nodes.Count = 2 Then
                Dim expr As New AS3Expression(node.Nodes(2).Nodes(1).MatchedToken)

                VisitNodes(node.Nodes(2).Nodes(1))
                expr.exprStepList = node.Nodes(2).Nodes(1).exprsteplist

                expr.Value = MemberScopeStack.Peek().ExprDataStack.Pop()

                para.ValueExpr = expr
            End If

        End If


        func.Parameters.Add(para)

    End Sub
    Sub _Parameter_list1(node As GrammerExpr)

    End Sub
    Sub _ParameterDEFType(node As GrammerExpr)

    End Sub
    Sub _ParameterDefaultValue(node As GrammerExpr)

    End Sub
    Sub _ParameterName(node As GrammerExpr)

    End Sub
    Sub _ParameterType(node As GrammerExpr)

    End Sub
    Sub _ReturnValue(node As GrammerExpr)

    End Sub
    Sub _IFElse(node As GrammerExpr)

    End Sub
    Sub _FORTYPE(node As GrammerExpr)

    End Sub
    Sub _Each(node As GrammerExpr)

    End Sub
    Sub _FOR_FORIN(node As GrammerExpr)

    End Sub
    Sub _Each_TEMP1(node As GrammerExpr)


        Dim nodeforeach = node.Nodes(1)
        ''"in" <F_Expression> ")" <Stmt> 

        Dim foreachArg As IAS3Stmt


        If node.Nodes(0).Nodes(0).GrammerLeftNode.Name = "F_Variable" Then
            Dim tempscope As New AS3MemberListBase(Nothing)
            Dim currentno = MemberScopeStack.Peek().LastRegId()
            While tempscope.LastRegId() <currentno
                tempscope.NextRegId()
            End While

            MemberScopeStack.Push(tempscope)
            VisitNodes(node.Nodes(0))
            MemberScopeStack.Pop()

            For index = 0 To tempscope.Count - 1
                MemberScopeStack.Peek().Add(tempscope(index))
                MemberScopeStack.Peek().StamentsStack.Peek().Remove(tempscope(index))
            Next

            MemberScopeStack.Peek().StamentsStack.Peek().AddRange(tempscope.StamentsStack.Peek())

            While MemberScopeStack.Peek().LastRegId() < tempscope.LastRegId()
                MemberScopeStack.Peek().NextRegId()
            End While

            foreachArg = tempscope(tempscope.Count - 1)
        Else
            '**F_ExpressionList**
            Dim exprs As New AS3StmtExpressions(node.MatchedToken)
            VisitNodes(node.Nodes(0))
            exprs.as3exprlist = currentparseExprListStack.Pop()
            foreachArg = exprs
        End If



        VisitNodes(nodeforeach.Nodes(1))
        Dim forinexpr = New AS3Expression(nodeforeach.Nodes(1).MatchedToken)
        forinexpr.exprStepList = nodeforeach.Nodes(1).exprsteplist
        forinexpr.Value = MemberScopeStack.Peek().ExprDataStack.Pop()


        MemberScopeStack.Peek().StamentsStack.Push(New List(Of IAS3Stmt))
        VisitNodes(nodeforeach.Nodes(3))

        Dim foreachinbody = MemberScopeStack.Peek().StamentsStack.Pop()

        Dim as3foreach As New AS3ForEach(node.MatchedToken)
        as3foreach.ForArg = foreachArg
        as3foreach.ForExpr = forinexpr
        as3foreach.Body = foreachinbody
        as3foreach.label = currentLabelStack.Pop()
        MemberScopeStack.Peek().StamentsStack.Peek().Add(as3foreach)

    End Sub
    Sub _ForVar(node As GrammerExpr)

    End Sub
    Sub _Each_TEMP2(node As GrammerExpr)

    End Sub
    Sub _FOR_TEMP1(node As GrammerExpr)
        '<FOR_TEMP1>  ::=<ForVar> <FOR_TEMP2>|<FOR>;
        '<FOR_TEMP2> ::=<FORIN>|<FOR>;

        '<FORIN>     ::="in" <Expression> ")" <Stmt> | ")" <Stmt> ;
        '<FOR>       ::=";"<FORPART2>";"<FORPART3>")"<Stmt>;
        '<ForVar> ::=<F_Variable>|<F_ExpressionList>;
        '<FORPART1>  ::=<ForVar>;
        '<FORPART2>  ::=<ExpressionList>|null;
        '<FORPART3>  ::=<ExpressionList>|null;

        If node.Nodes.Count = 2 Then
            If node.Nodes(1).Nodes(0).GrammerLeftNode.Name = "FOR" Then

                VisitNodes(node.Nodes(0))
                If node.Nodes(0).Nodes(0).GrammerLeftNode.Name = "F_ExpressionList" Then

                    Dim exprs As New AS3StmtExpressions(node.Nodes(0).MatchedToken)
                    exprs.as3exprlist = currentparseExprListStack.Pop()

                    MemberScopeStack.Peek().StamentsStack.Peek().Add(exprs)

                End If
                VisitNodes(node.Nodes(1))

            Else


                Dim nodeforin = node.Nodes(1).Nodes(0)
                ''"in" <Expression> ")" <Stmt> | ")" <Stmt> ;

                Dim forinArg As IAS3Stmt

                If nodeforin.Nodes.Count = 4 Then

                    If node.Nodes(0).Nodes(0).GrammerLeftNode.Name = "F_Variable" Then
                        Dim tempscope As New AS3MemberListBase(Nothing)
                        Dim currentno = MemberScopeStack.Peek().LastRegId()
                        While tempscope.LastRegId() < currentno
                            tempscope.NextRegId()
                        End While

                        MemberScopeStack.Push(tempscope)
                        VisitNodes(node.Nodes(0))
                        MemberScopeStack.Pop()

                        For index = 0 To tempscope.Count - 1
                            MemberScopeStack.Peek().Add(tempscope(index))
                            MemberScopeStack.Peek().StamentsStack.Peek().Remove(tempscope(index))
                        Next

                        MemberScopeStack.Peek().StamentsStack.Peek().AddRange(tempscope.StamentsStack.Peek())

                        While MemberScopeStack.Peek().LastRegId() < tempscope.LastRegId()
                            MemberScopeStack.Peek().NextRegId()
                        End While

                        forinArg = tempscope(tempscope.Count - 1)
                    Else
                        '**F_ExpressionList**
                        Dim exprs As New AS3StmtExpressions(node.MatchedToken)
                        VisitNodes(node.Nodes(0))
                        exprs.as3exprlist = currentparseExprListStack.Pop()
                        forinArg = exprs
                    End If



                    VisitNodes(nodeforin.Nodes(1))
                    Dim forinexpr = New AS3Expression(nodeforin.Nodes(1).MatchedToken)
                    forinexpr.exprStepList = nodeforin.Nodes(1).exprsteplist
                    forinexpr.Value = MemberScopeStack.Peek().ExprDataStack.Pop()


                    MemberScopeStack.Peek().StamentsStack.Push(New List(Of IAS3Stmt))
                    VisitNodes(nodeforin.Nodes(3))

                    Dim forinbody = MemberScopeStack.Peek().StamentsStack.Pop()

                    Dim as3forin As New AS3ForIn(node.MatchedToken)
                    as3forin.ForArg = forinArg
                    as3forin.ForExpr = forinexpr
                    as3forin.Body = forinbody
                    as3forin.label = currentLabelStack.Pop()

                    MemberScopeStack.Peek().StamentsStack.Peek().Add(as3forin)


                End If



            End If
        Else
            'for (;)
            VisitNodes(node.Nodes(0))
        End If



    End Sub
    Sub _FOR_TEMP2(node As GrammerExpr)

    End Sub
    Sub _FOR(node As GrammerExpr)

        'Dim forpart1 As AS3StmtExpressions = Nothing
        Dim forpart2 As AS3StmtExpressions = Nothing

        'If currentparseExprListStack.Count > 0 Then
        '    forpart1 = New AS3StmtExpressions(node.MatchedToken)
        '    forpart1.as3exprlist = currentparseExprListStack.Peek()

        'End If


        If node.Nodes(1).Nodes.Count > 0 Then
            forpart2 = New AS3StmtExpressions(node.MatchedToken)
            VisitNodes(node.Nodes(1))
            'forpart2.grammerExpr = node.Nodes(1)
            forpart2.as3exprlist = currentparseExprListStack.Pop()


        End If

        Dim forpart3 As AS3StmtExpressions = Nothing
        If node.Nodes(3).Nodes.Count > 0 Then

            forpart3 = New AS3StmtExpressions(node.MatchedToken)
            VisitNodes(node.Nodes(3))
            'forpart3.grammerExpr = node.Nodes(3)
            forpart3.as3exprlist = currentparseExprListStack.Pop()

        End If

        '***for体内代码进入新循环***
        MemberScopeStack.Peek().StamentsStack.Push(New List(Of IAS3Stmt))
        VisitNodes(node.Nodes(5))

        Dim forbody As List(Of IAS3Stmt) = MemberScopeStack.Peek().StamentsStack.Pop()

        Dim as3for As New AS3For(node.MatchedToken)
        'as3for.Part1 = forpart1
        as3for.Part2 = forpart2
        as3for.Part3 = forpart3
        as3for.Body = forbody

        as3for.label = currentLabelStack.Pop()

        MemberScopeStack.Peek().StamentsStack.Peek().Add(as3for)

    End Sub
    Sub _FORIN(node As GrammerExpr)

    End Sub
    Sub _FORPART2(node As GrammerExpr)
        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        If node.Nodes.Count > 0 Then
            VisitNodes(node.Nodes(0))
            node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)

        End If

    End Sub
    Sub _FORPART3(node As GrammerExpr)
        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        If node.Nodes.Count > 0 Then
            VisitNodes(node.Nodes(0))
            node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)

        End If
    End Sub
    Sub _FORPART1(node As GrammerExpr)

    End Sub
    Sub _DO_CONDITION(node As GrammerExpr)

    End Sub
    Sub _TRYBLOCK(node As GrammerExpr)

    End Sub
    Sub _CATCHLIST(node As GrammerExpr)

    End Sub
    Sub _FINALLY(node As GrammerExpr)

    End Sub
    Sub _THROWEXCEPTION(node As GrammerExpr)

    End Sub
    Sub _CATCH(node As GrammerExpr)
        '"catch" "(" <VariableDefine> ")""{"<CATCHBLOCK>"}";

        Dim tempscope As New AS3MemberListBase(node.MatchedToken)
        Dim currentno = MemberScopeStack.Peek().LastRegId()
        While tempscope.LastRegId() < currentno
            tempscope.NextRegId()
        End While

        MemberScopeStack.Push(tempscope)
        VisitNodes(node.Nodes(2))
        MemberScopeStack.Pop()

        For index = 0 To tempscope.Count - 1
            'MemberScopeStack.Peek().Add(tempscope(index))
            MemberScopeStack.Peek().StamentsStack.Peek().Remove(tempscope(index))
        Next

        'MemberScopeStack.Peek().StamentsStack.Peek().AddRange(tempscope.StamentsStack.Peek())

        While MemberScopeStack.Peek().LastRegId() < tempscope.LastRegId()
            MemberScopeStack.Peek().NextRegId()
        End While

        Dim trystmt = currentTryStack.Peek()

        Dim catchblock As New AS3Catch()
        catchblock.CatchVariable = tempscope(tempscope.Count - 1)

        Dim catchbody As New List(Of IAS3Stmt)
        MemberScopeStack.Peek().StamentsStack.Push(catchbody)
        VisitNodes(node.Nodes(5))

        catchblock.CatchBlock = MemberScopeStack.Peek().StamentsStack.Pop()

        trystmt.CatchList.Add(catchblock)

    End Sub
    Sub _CATCHLIST1(node As GrammerExpr)

    End Sub
    Sub _CATCHBLOCK(node As GrammerExpr)

    End Sub
    Sub _FINALLYBLOCK(node As GrammerExpr)

    End Sub
    Sub _CASEBODY(node As GrammerExpr)

    End Sub

    Sub _CASESTMT(node As GrammerExpr)

    End Sub
    Sub _CASELIST(node As GrammerExpr)

    End Sub
    Sub _CASELIST1(node As GrammerExpr)

    End Sub
    Sub _Assigning(node As GrammerExpr)
        '///'::=<Ternary><AssigningOpt> | <Function>;         
        'If node.Nodes.Count = 2 Then
        '    VisitNodes(node.Nodes(0))

        '    VisitNodes(node.Nodes(1))

        '    node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        '    node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)
        '    node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)

        'Else
        '    VisitNodes(node.Nodes(0))
        '    node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        '    node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)
        'End If

        '::=<Ternary><AssigningOpt> ;


        If node.MatchedToken.StringValue = "function" Then
            If current_expression_canfunctioninvoke.Count > 0 Then
                current_expression_canfunctioninvoke.Push(current_expression_canfunctioninvoke.Peek())
            Else
                current_expression_canfunctioninvoke.Push(False)
            End If

        Else
            current_expression_canfunctioninvoke.Push(True)
        End If


        VisitNodes(node.Nodes(0))
        VisitNodes(node.Nodes(1))

        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)
        node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)

        current_expression_canfunctioninvoke.Pop()

    End Sub
    Sub _A_FC(node As GrammerExpr)
        '::=<Call>|null;
        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        If node.Nodes.Count = 2 Then

            'VisitNodes(node.Nodes(0))

            'If TypeOf MemberScopeStack.Peek() Is AS3Class Or TypeOf MemberScopeStack.Peek() Is AS3Interface Then
            '    Throw New GrammarExpression(node.MatchedToken, "不能出现在这里")
            'End If

            'Dim argnode = node.Nodes(0).Nodes(1)
            'If argnode.Nodes.Count = 0 Then
            '    If Not current_expression_canfunctioninvoke.Peek() Then
            '        Throw New GrammarExpression(node.MatchedToken, "不能出现在这里")
            '    End If


            'End If



            'node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)


            'VisitNodes(node.Nodes(1).Nodes(0))
            'node.exprsteplist.AddRange(node.Nodes(1).Nodes(0).exprsteplist)


        ElseIf node.Nodes.Count = 1 Then
            '<Call>|null

            If TypeOf MemberScopeStack.Peek() Is AS3Class Or TypeOf MemberScopeStack.Peek() Is AS3Interface Then
                Throw New GrammarException(node.MatchedToken, "不能出现在这里")
            End If

            Dim argnode = node.Nodes(0).Nodes(1)
            If argnode.Nodes.Count = 0 Then
                If Not current_expression_canfunctioninvoke.Peek() Then
                    Throw New GrammarException(node.MatchedToken, "不能出现在这里")
                End If

            End If

            VisitNodes(node.Nodes(0))
            node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)

        End If


    End Sub


    Sub _Ternary(node As GrammerExpr)
        '::=<LogicOr><TernaryOpt>
        VisitNodes(node.Nodes(0))

        VisitNodes(node.Nodes(1))

        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)
        node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)

    End Sub
    Sub _AssigningOpt(node As GrammerExpr)
        ' <AssigningOpt> ::= 
        '"="<Assigning> |                        //改为右结合
        '"*="<Assigning> |
        '"/="<Assigning> |
        '"%="<Assigning> |
        '"+="<Assigning> |
        '"-="<Assigning> |
        '"<<="<Assigning> |
        '">>="<Assigning> |
        '">>>="<Assigning> |
        '"&="<Assigning> |
        '"^="<Assigning> |
        '"|="<Assigning> |
        '"||="<Assigning> |
        '"&&="<Assigning>|
        ' null

        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        If node.Nodes.Count = 2 Then
            VisitNodes(node.Nodes(1))


            Dim arg2 = MemberScopeStack.Peek().ExprDataStack.Pop()
            Dim arg1 = MemberScopeStack.Peek().ExprDataStack.Pop()


            Dim code As String = node.Nodes(0).MatchedToken.StringValue


            If code = "||=" Then
                '***短路拆分***
                Dim lv_true_flag As New AS3.Expr.AS3ExprStep(node.MatchedToken)
                lv_true_flag.Type = Expr.OpType.Flag
                lv_true_flag.OpCode = "logicOr_leftvalue_true_" & AS3.Expr.AS3ExprStep.GetFlagId().ToString()

                Dim opifgoto_lvtrue As New AS3.Expr.AS3ExprStep(node.MatchedToken)    '短路操作
                opifgoto_lvtrue.Arg1 = arg1
                opifgoto_lvtrue.OpCode = lv_true_flag.OpCode
                opifgoto_lvtrue.Type = Expr.OpType.IF_GotoFlag

                node.exprsteplist.Add(opifgoto_lvtrue)

                node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)

                Dim op As New AS3.Expr.AS3ExprStep(node.MatchedToken)
                op.Type = Expr.OpType.Assigning
                op.OpCode = "="
                op.Arg1 = arg1
                op.Arg2 = arg2

                node.exprsteplist.Add(op)

                node.exprsteplist.Add(lv_true_flag)

                MemberScopeStack.Peek().ExprDataStack.Push(arg2)

            ElseIf code <> "=" Then
                '操作拆分  v<temp> = arg1+arg2
                '         arg1 = v<temp>
                node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)

                Dim rcode = code.Substring(0, code.Length - 1)

                Dim optype As Expr.OpType

                If rcode = "+" Or rcode = "-" Then
                    optype = Expr.OpType.Plus
                ElseIf rcode = "*" Or rcode = "/" Or rcode = "%" Then
                    optype = Expr.OpType.Multiply
                ElseIf rcode = "<<" Or rcode = ">>" Or rcode = ">>>" Then
                    optype = Expr.OpType.BitShift
                ElseIf rcode = "&" Then
                    optype = Expr.OpType.BitAnd
                ElseIf rcode = "|" Then
                    optype = Expr.OpType.BitOr
                ElseIf rcode = "^" Then
                    optype = Expr.OpType.BitXor
                End If

                Dim reg = Expr.AS3DataStackElement.MakeReg(MemberScopeStack.Peek().NextRegId())
                Dim op As New AS3.Expr.AS3ExprStep(node.Nodes(0).MatchedToken)
                op.Type = optype
                op.OpCode = rcode
                op.Arg1 = reg
                op.Arg2 = arg1
                op.Arg3 = arg2

                node.exprsteplist.Add(op)


                op = New AS3.Expr.AS3ExprStep(node.Nodes(0).MatchedToken)
                op.Type = Expr.OpType.Assigning
                op.OpCode = "="
                op.Arg1 = arg1
                op.Arg2 = reg

                node.exprsteplist.Add(op)

                MemberScopeStack.Peek().ExprDataStack.Push(reg)

            Else
                node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)

                Dim op As New AS3.Expr.AS3ExprStep(node.Nodes(0).MatchedToken)
                op.Type = Expr.OpType.Assigning
                op.OpCode = node.Nodes(0).MatchedToken.StringValue
                op.Arg1 = arg1
                op.Arg2 = arg2

                node.exprsteplist.Add(op)

                MemberScopeStack.Peek().ExprDataStack.Push(arg2)

            End If


        End If


    End Sub
    Sub _LogicOr(node As GrammerExpr)
        '::=<LogicAnd><LogicOrOpt>;
        VisitNodes(node.Nodes(0))
        VisitNodes(node.Nodes(1))

        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)
        node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)
    End Sub
    Sub _TernaryOpt(node As GrammerExpr)
        '::="?" <Expression> ":" <Expression> | null;

        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        'v0= v1?v2:v3

        'if v1 goto flag1
        'v3expr
        'v0 = v3

        'GoTo flag2

        'flag1
        'v2expr
        'v0 = v2

        'flag2
        If node.Nodes.Count > 0 Then
            VisitNodes(node.Nodes(1))
            VisitNodes(node.Nodes(3))

            Dim arg4 = MemberScopeStack.Peek().ExprDataStack.Pop()
            Dim arg3 = MemberScopeStack.Peek().ExprDataStack.Pop()
            Dim arg2 = MemberScopeStack.Peek().ExprDataStack.Pop()

            Dim arg1 = AS3.Expr.AS3DataStackElement.MakeReg(MemberScopeStack.Peek().NextRegId())

            MemberScopeStack.Peek().ExprDataStack.Push(arg1)

            Dim flag1 As New AS3.Expr.AS3ExprStep(node.MatchedToken)
            flag1.Type = Expr.OpType.Flag
            flag1.OpCode = "flag_?_true_" & Expr.AS3ExprStep.GetFlagId().ToString()


            Dim opifgoto As New AS3.Expr.AS3ExprStep(node.MatchedToken)
            opifgoto.Type = Expr.OpType.IF_GotoFlag
            opifgoto.OpCode = flag1.OpCode
            opifgoto.Arg1 = arg2

            node.exprsteplist.Add(opifgoto)

            node.exprsteplist.AddRange(node.Nodes(3).exprsteplist)

            Dim op1 As New Expr.AS3ExprStep(node.MatchedToken)
            op1.Type = Expr.OpType.Assigning
            op1.OpCode = "="
            op1.Arg1 = arg1
            op1.Arg2 = arg4

            node.exprsteplist.Add(op1)

            Dim flag2 As New AS3.Expr.AS3ExprStep(node.MatchedToken)
            flag2.Type = Expr.OpType.Flag
            flag2.OpCode = "flag_?_false_" & Expr.AS3ExprStep.GetFlagId().ToString()

            Dim opgoto As New AS3.Expr.AS3ExprStep(node.MatchedToken)
            opgoto.Type = Expr.OpType.GotoFlag
            opgoto.OpCode = flag2.OpCode

            node.exprsteplist.Add(opgoto)
            node.exprsteplist.Add(flag1)

            node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)

            Dim op2 As New Expr.AS3ExprStep(node.MatchedToken)
            op2.Type = Expr.OpType.Assigning
            op2.OpCode = "="
            op2.Arg1 = arg1
            op2.Arg2 = arg3

            node.exprsteplist.Add(op2)

            node.exprsteplist.Add(flag2)

            'Dim op As New AS3.Expr.AS3ExprStep()
            'op.Arg1 = arg1
            'op.Arg2 = arg2
            'op.Arg3 = arg3
            'op.Arg4 = arg4
            'op.OpCode = "?"
            'op.Type = Expr.OpType.Ternary

            'MemberScopeStack.Peek().ExprDataStack.Push(arg1)

            'node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)
            'node.exprsteplist.AddRange(node.Nodes(3).exprsteplist)
            'node.exprsteplist.Add(op)

        End If

    End Sub
    Sub _LogicAnd(node As GrammerExpr)
        '   ::=<BitOr><LogicAndOpt>;
        VisitNodes(node.Nodes(0))
        VisitNodes(node.Nodes(1))

        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)
        node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)

    End Sub
    Sub _LogicOrOpt(node As GrammerExpr)
        '"||"<LogicAnd><LogicOrOpt>|null;

        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        If node.Nodes.Count > 0 Then

            VisitNodes(node.Nodes(1))

            Dim arg3 = MemberScopeStack.Peek().ExprDataStack.Pop()
            Dim arg2 = MemberScopeStack.Peek().ExprDataStack.Pop()
            Dim arg1 = AS3.Expr.AS3DataStackElement.MakeReg(MemberScopeStack.Peek().NextRegId())

            'Dim op As New AS3.Expr.AS3ExprStep()
            'op.Arg1 = arg1
            'op.Arg2 = arg2
            'op.Arg3 = arg3
            'op.OpCode = "||"
            'op.Type = Expr.OpType.LogicOr

            MemberScopeStack.Peek().ExprDataStack.Push(arg1)

            Dim lv_true_flag As New AS3.Expr.AS3ExprStep(node.MatchedToken)
            lv_true_flag.Type = Expr.OpType.Flag
            lv_true_flag.OpCode = "logicOr_leftvalue_true_" & AS3.Expr.AS3ExprStep.GetFlagId().ToString()

            Dim lv_false_flag As New AS3.Expr.AS3ExprStep(node.MatchedToken)
            lv_false_flag.Type = Expr.OpType.Flag
            lv_false_flag.OpCode = "logicOr_leftvalue_false_" & AS3.Expr.AS3ExprStep.GetFlagId().ToString()


            Dim opifgoto_lvtrue As New AS3.Expr.AS3ExprStep(node.MatchedToken)    '短路操作
            opifgoto_lvtrue.Arg1 = arg2
            opifgoto_lvtrue.OpCode = lv_true_flag.OpCode
            opifgoto_lvtrue.Type = Expr.OpType.IF_GotoFlag

            node.exprsteplist.Add(opifgoto_lvtrue)


            node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)

            Dim op1 As New AS3.Expr.AS3ExprStep(node.MatchedToken)
            op1.Type = Expr.OpType.Assigning
            op1.OpCode = "="
            op1.Arg1 = arg1
            op1.Arg2 = arg3

            node.exprsteplist.Add(op1)

            Dim opgoto As New AS3.Expr.AS3ExprStep(node.MatchedToken)
            opgoto.Type = Expr.OpType.GotoFlag
            opgoto.OpCode = lv_false_flag.OpCode

            node.exprsteplist.Add(opgoto)

            node.exprsteplist.Add(lv_true_flag)

            Dim op2 As New AS3.Expr.AS3ExprStep(node.MatchedToken)
            op2.Type = Expr.OpType.Assigning
            op2.OpCode = "="
            op2.Arg1 = arg1
            op2.Arg2 = arg2

            node.exprsteplist.Add(op2)

            node.exprsteplist.Add(lv_false_flag)


            'node.exprsteplist.Add(op)

            VisitNodes(node.Nodes(2))
            node.exprsteplist.AddRange(node.Nodes(2).exprsteplist)

        End If


    End Sub
    Sub _BitOr(node As GrammerExpr)
        '::=<BitXor><BitOrOpt>;           //位或       
        VisitNodes(node.Nodes(0))
        VisitNodes(node.Nodes(1))

        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)
        node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)

    End Sub
    Sub _LogicAndOpt(node As GrammerExpr)
        '"&&"<BitOr><LogicAndOpt>|null;
        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        If node.Nodes.Count > 0 Then

            VisitNodes(node.Nodes(1))

            Dim arg3 = MemberScopeStack.Peek().ExprDataStack.Pop()
            Dim arg2 = MemberScopeStack.Peek().ExprDataStack.Pop()
            Dim arg1 = AS3.Expr.AS3DataStackElement.MakeReg(MemberScopeStack.Peek().NextRegId())

            'Dim op As New AS3.Expr.AS3ExprStep(node.MatchedToken)
            'op.Arg1 = arg1
            'op.Arg2 = arg2
            'op.Arg3 = arg3
            'op.OpCode = "&&"
            'op.Type = Expr.OpType.LogicAnd

            MemberScopeStack.Peek().ExprDataStack.Push(arg1)

            'node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)
            'node.exprsteplist.Add(op)

            '**如果arg2为真，则继续求arg3的值，否则直接返回arg2的值
            Dim lv_true_flag As New AS3.Expr.AS3ExprStep(node.MatchedToken)
            lv_true_flag.Type = Expr.OpType.Flag
            lv_true_flag.OpCode = "logicAnd_leftvalue_true_" & AS3.Expr.AS3ExprStep.GetFlagId().ToString()

            Dim lv_false_flag As New AS3.Expr.AS3ExprStep(node.MatchedToken)
            lv_false_flag.Type = Expr.OpType.Flag
            lv_false_flag.OpCode = "logicAnd_leftvalue_false_" & AS3.Expr.AS3ExprStep.GetFlagId().ToString()

            Dim opifgoto_lvtrue As New AS3.Expr.AS3ExprStep(node.MatchedToken)
            opifgoto_lvtrue.Arg1 = arg2
            opifgoto_lvtrue.OpCode = lv_true_flag.OpCode
            opifgoto_lvtrue.Type = Expr.OpType.IF_GotoFlag


            node.exprsteplist.Add(opifgoto_lvtrue)

            Dim op1 As New AS3.Expr.AS3ExprStep(node.MatchedToken)
            op1.Type = Expr.OpType.Assigning
            op1.OpCode = "="
            op1.Arg1 = arg1
            op1.Arg2 = arg2

            node.exprsteplist.Add(op1)
            Dim opgoto As New AS3.Expr.AS3ExprStep(node.MatchedToken)
            opgoto.Type = Expr.OpType.GotoFlag
            opgoto.OpCode = lv_false_flag.OpCode

            node.exprsteplist.Add(opgoto)

            node.exprsteplist.Add(lv_true_flag)

            node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)

            Dim op2 As New AS3.Expr.AS3ExprStep(node.MatchedToken)
            op2.Type = Expr.OpType.Assigning
            op2.OpCode = "="
            op2.Arg1 = arg1
            op2.Arg2 = arg3
            node.exprsteplist.Add(op2)
            node.exprsteplist.Add(lv_false_flag)






            VisitNodes(node.Nodes(2))
            node.exprsteplist.AddRange(node.Nodes(2).exprsteplist)

        End If

    End Sub
    Sub _BitXor(node As GrammerExpr)
        '::=<BitAnd><BitXorOpt>;           //位异或           
        VisitNodes(node.Nodes(0))
        VisitNodes(node.Nodes(1))

        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)
        node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)

    End Sub
    Sub _BitOrOpt(node As GrammerExpr)
        '::="|"<BitXor><BitOrOpt> | null;
        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        If node.Nodes.Count > 0 Then

            VisitNodes(node.Nodes(1))

            Dim arg3 = MemberScopeStack.Peek().ExprDataStack.Pop()
            Dim arg2 = MemberScopeStack.Peek().ExprDataStack.Pop()
            Dim arg1 = AS3.Expr.AS3DataStackElement.MakeReg(MemberScopeStack.Peek().NextRegId())

            Dim op As New AS3.Expr.AS3ExprStep(node.MatchedToken)
            op.Arg1 = arg1
            op.Arg2 = arg2
            op.Arg3 = arg3
            op.OpCode = "|"
            op.Type = Expr.OpType.BitOr

            MemberScopeStack.Peek().ExprDataStack.Push(arg1)

            node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)
            node.exprsteplist.Add(op)

            VisitNodes(node.Nodes(2))
            node.exprsteplist.AddRange(node.Nodes(2).exprsteplist)

        End If


    End Sub
    Sub _BitAnd(node As GrammerExpr)
        '::=<LogicEQ><BitAndOpt>;           //位与                 
        VisitNodes(node.Nodes(0))
        VisitNodes(node.Nodes(1))

        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)
        node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)

    End Sub
    Sub _BitXorOpt(node As GrammerExpr)
        '::="^"<BitAnd><BitXorOpt> | null;
        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        If node.Nodes.Count > 0 Then

            VisitNodes(node.Nodes(1))

            Dim arg3 = MemberScopeStack.Peek().ExprDataStack.Pop()
            Dim arg2 = MemberScopeStack.Peek().ExprDataStack.Pop()
            Dim arg1 = AS3.Expr.AS3DataStackElement.MakeReg(MemberScopeStack.Peek().NextRegId())

            Dim op As New AS3.Expr.AS3ExprStep(node.MatchedToken)
            op.Arg1 = arg1
            op.Arg2 = arg2
            op.Arg3 = arg3
            op.OpCode = "^"
            op.Type = Expr.OpType.BitXor

            MemberScopeStack.Peek().ExprDataStack.Push(arg1)

            node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)
            node.exprsteplist.Add(op)

            VisitNodes(node.Nodes(2))
            node.exprsteplist.AddRange(node.Nodes(2).exprsteplist)

        End If

    End Sub
    Sub _LogicEQ(node As GrammerExpr)
        '::=<Logic><LogicEQOpt>;  
        VisitNodes(node.Nodes(0))
        VisitNodes(node.Nodes(1))

        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)
        node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)
    End Sub
    Sub _BitAndOpt(node As GrammerExpr)
        '"&"<LogicEQ><BitAndOpt> | null;
        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        If node.Nodes.Count > 0 Then

            VisitNodes(node.Nodes(1))

            Dim arg3 = MemberScopeStack.Peek().ExprDataStack.Pop()
            Dim arg2 = MemberScopeStack.Peek().ExprDataStack.Pop()
            Dim arg1 = AS3.Expr.AS3DataStackElement.MakeReg(MemberScopeStack.Peek().NextRegId())

            Dim op As New AS3.Expr.AS3ExprStep(node.MatchedToken)
            op.Arg1 = arg1
            op.Arg2 = arg2
            op.Arg3 = arg3
            op.OpCode = "&"
            op.Type = Expr.OpType.BitAnd

            MemberScopeStack.Peek().ExprDataStack.Push(arg1)

            node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)
            node.exprsteplist.Add(op)

            VisitNodes(node.Nodes(2))
            node.exprsteplist.AddRange(node.Nodes(2).exprsteplist)

        End If
    End Sub
    Sub _Logic(node As GrammerExpr)
        '::=<BitShift><LogicOpt>;  
        VisitNodes(node.Nodes(0))
        VisitNodes(node.Nodes(1))

        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)
        node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)

    End Sub
    Sub _LogicEQOpt(node As GrammerExpr)
        '    "=="<Logic><LogicEQOpt> | "!="<Logic><LogicEQOpt> 
        '|"==="<Logic><LogicEQOpt>|"!=="<Logic><LogicEQOpt> |null; 

        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        If node.Nodes.Count > 0 Then

            VisitNodes(node.Nodes(1))

            Dim arg3 = MemberScopeStack.Peek().ExprDataStack.Pop()
            Dim arg2 = MemberScopeStack.Peek().ExprDataStack.Pop()
            Dim arg1 = AS3.Expr.AS3DataStackElement.MakeReg(MemberScopeStack.Peek().NextRegId())

            Dim op As New AS3.Expr.AS3ExprStep(node.MatchedToken)
            op.Arg1 = arg1
            op.Arg2 = arg2
            op.Arg3 = arg3
            op.OpCode = node.Nodes(0).MatchedToken.StringValue
            op.Type = Expr.OpType.LogicEQ

            MemberScopeStack.Peek().ExprDataStack.Push(arg1)

            node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)
            node.exprsteplist.Add(op)

            VisitNodes(node.Nodes(2))
            node.exprsteplist.AddRange(node.Nodes(2).exprsteplist)

        End If



    End Sub
    Sub _BitShift(node As GrammerExpr)
        '::= <Plus> <BitShiftOpt>;
        VisitNodes(node.Nodes(0))
        VisitNodes(node.Nodes(1))

        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)
        node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)


    End Sub
    Sub _LogicOpt(node As GrammerExpr)
        '  "<" <BitShift><LogicOpt> | ">" <BitShift><LogicOpt> |
        '"<=" <BitShift><LogicOpt> | ">=" <BitShift><LogicOpt>	|
        '"as" <BitShift><LogicOpt>	| "in" <BitShift><LogicOpt> |
        '"instanceof" <BitShift><LogicOpt> |"is" <BitShift><LogicOpt> |null;

        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        If node.Nodes.Count > 0 Then

            VisitNodes(node.Nodes(1))

            Dim arg3 = MemberScopeStack.Peek().ExprDataStack.Pop()
            Dim arg2 = MemberScopeStack.Peek().ExprDataStack.Pop()
            Dim arg1 = AS3.Expr.AS3DataStackElement.MakeReg(MemberScopeStack.Peek().NextRegId())

            Dim op As New AS3.Expr.AS3ExprStep(node.MatchedToken)
            op.Arg1 = arg1
            op.Arg2 = arg2
            op.Arg3 = arg3
            op.OpCode = node.Nodes(0).MatchedToken.StringValue
            op.Type = Expr.OpType.Logic

            MemberScopeStack.Peek().ExprDataStack.Push(arg1)

            node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)
            node.exprsteplist.Add(op)

            VisitNodes(node.Nodes(2))
            node.exprsteplist.AddRange(node.Nodes(2).exprsteplist)

        End If
    End Sub
    Sub _Plus(node As GrammerExpr)
        '<Multiply><PlusOpt>;
        VisitNodes(node.Nodes(0))
        VisitNodes(node.Nodes(1))

        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)
        node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)

    End Sub
    Sub _BitShiftOpt(node As GrammerExpr)
        '::= "<<" <Plus><BitShiftOpt> | ">>" <Plus><BitShiftOpt>| ">>>" <Plus><BitShiftOpt> |  null;          //移位 << >> >>>
        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        If node.Nodes.Count > 0 Then

            VisitNodes(node.Nodes(1))

            Dim arg3 = MemberScopeStack.Peek().ExprDataStack.Pop()
            Dim arg2 = MemberScopeStack.Peek().ExprDataStack.Pop()
            Dim arg1 = AS3.Expr.AS3DataStackElement.MakeReg(MemberScopeStack.Peek().NextRegId())

            Dim op As New AS3.Expr.AS3ExprStep(node.MatchedToken)
            op.Arg1 = arg1
            op.Arg2 = arg2
            op.Arg3 = arg3
            op.OpCode = node.Nodes(0).MatchedToken.StringValue
            op.Type = Expr.OpType.BitShift

            MemberScopeStack.Peek().ExprDataStack.Push(arg1)

            node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)
            node.exprsteplist.Add(op)

            VisitNodes(node.Nodes(2))
            node.exprsteplist.AddRange(node.Nodes(2).exprsteplist)

        End If

    End Sub
    Sub _Multiply(node As GrammerExpr)
        '<Unary> <MultiplyOpt>;               
        VisitNodes(node.Nodes(0))
        VisitNodes(node.Nodes(1))

        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)
        node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)


    End Sub
    Sub _PlusOpt(node As GrammerExpr)
        '"+" <Multiply><PlusOpt> | "-" <Multiply><PlusOpt> |  null;        
        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        If node.Nodes.Count > 0 Then

            VisitNodes(node.Nodes(1))

            Dim arg3 = MemberScopeStack.Peek().ExprDataStack.Pop()
            Dim arg2 = MemberScopeStack.Peek().ExprDataStack.Pop()
            Dim arg1 = AS3.Expr.AS3DataStackElement.MakeReg(MemberScopeStack.Peek().NextRegId())

            Dim op As New AS3.Expr.AS3ExprStep(node.MatchedToken)
            op.Arg1 = arg1
            op.Arg2 = arg2
            op.Arg3 = arg3
            op.OpCode = node.Nodes(0).MatchedToken.StringValue
            op.Type = Expr.OpType.Plus

            MemberScopeStack.Peek().ExprDataStack.Push(arg1)

            node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)
            node.exprsteplist.Add(op)

            VisitNodes(node.Nodes(2))
            node.exprsteplist.AddRange(node.Nodes(2).exprsteplist)

        End If


    End Sub
    Sub _Unary(node As GrammerExpr)
        '"new" <Access>  | "+" <Access> | "-" <Access> | "~" <Unary>| "!" <Unary>| "delete"<Access> | "typeof"<Unary> /*| "void" <Access> void会和返回值冲突.. */ //单目运算
        '        | "++" <Access> | "--" <Access>|<Access>  ;     

        '前缀，一元运算

        node.exprsteplist = New AS3.Expr.AS3ExprStepList()

        If node.Nodes.Count = 2 Then
            VisitNodes(node.Nodes(1))
            node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)

            Dim arg2 = MemberScopeStack.Peek().ExprDataStack.Pop()


            Dim op As New AS3.Expr.AS3ExprStep(node.MatchedToken)
            op.OpCode = node.Nodes(0).MatchedToken.StringValue


            'If op.OpCode <> "++" And op.OpCode <> "--" Then
            Dim arg1 = AS3.Expr.AS3DataStackElement.MakeReg(MemberScopeStack.Peek().NextRegId())
                op.Arg1 = arg1
                op.Arg2 = arg2

            'Else
            '    op.Arg1 = arg2
            'End If




            op.Type = Expr.OpType.Unary

            MemberScopeStack.Peek().ExprDataStack.Push(op.Arg1)

            node.exprsteplist.Add(op)


        Else
            VisitNodes(node.Nodes(0))
            node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)
        End If


    End Sub
    Sub _MultiplyOpt(node As GrammerExpr)
        '"*" <Unary><MultiplyOpt> | "/" <Unary><MultiplyOpt> | "%" <Unary><MultiplyOpt> | null;    
        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        If node.Nodes.Count > 0 Then

            VisitNodes(node.Nodes(1))

            Dim arg3 = MemberScopeStack.Peek().ExprDataStack.Pop()
            Dim arg2 = MemberScopeStack.Peek().ExprDataStack.Pop()
            Dim arg1 = AS3.Expr.AS3DataStackElement.MakeReg(MemberScopeStack.Peek().NextRegId())

            Dim op As New AS3.Expr.AS3ExprStep(node.MatchedToken)
            op.Arg1 = arg1
            op.Arg2 = arg2
            op.Arg3 = arg3
            op.OpCode = node.Nodes(0).MatchedToken.StringValue
            op.Type = Expr.OpType.Multiply

            MemberScopeStack.Peek().ExprDataStack.Push(arg1)

            node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)
            node.exprsteplist.Add(op)

            VisitNodes(node.Nodes(2))
            node.exprsteplist.AddRange(node.Nodes(2).exprsteplist)

        End If
    End Sub
    Sub _Unit(node As GrammerExpr)
        '<New>|<Object>|<E4XAccess>|<ID_EABLED_KEYWORD>|identifier |number | string  |"(" <Expression> ")" | <Function>| <Array> |<Vector>|"CONFIG::" identifier ;
        node.exprsteplist = New AS3.Expr.AS3ExprStepList()

        If node.Nodes(0).GrammerLeftNode.Type = GrammarNodeType.identifier _
            OrElse node.Nodes(0).GrammerLeftNode.Name = "ID_EABLED_KEYWORD" Then


            Dim v As New AS3.Expr.AS3DataStackElement()
            v.Data = New AS3.Expr.AS3DataValue()
            v.Data.FF1Type = Expr.FF1DataValueType.identifier
            v.Data.Value = node.Nodes(0).MatchedToken.StringValue
            MemberScopeStack.Peek().ExprDataStack.Push(v)
        ElseIf node.Nodes(0).GrammerLeftNode.Type = GrammarNodeType.number Then
            Dim v As New AS3.Expr.AS3DataStackElement()
            v.Data = New AS3.Expr.AS3DataValue()
            v.Data.FF1Type = Expr.FF1DataValueType.const_number
            v.Data.Value = node.Nodes(0).MatchedToken.StringValue
            MemberScopeStack.Peek().ExprDataStack.Push(v)
        ElseIf node.Nodes(0).GrammerLeftNode.Type = GrammarNodeType.conststring Then
            Dim v As New AS3.Expr.AS3DataStackElement()
            v.Data = New AS3.Expr.AS3DataValue()
            v.Data.FF1Type = Expr.FF1DataValueType.const_string
            v.Data.Value = node.Nodes(0).MatchedToken.StringValue
            MemberScopeStack.Peek().ExprDataStack.Push(v)

        ElseIf node.Nodes(0).GrammerLeftNode.Name = "Object" Then

            VisitNodes(node.Nodes(0))
            node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)
        ElseIf node.Nodes(0).GrammerLeftNode.Name = "New" Then
            VisitNodes(node.Nodes(0))
            node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)
        ElseIf node.Nodes(0).GrammerLeftNode.Name = "E4XAccess" Then

            VisitNodes(node.Nodes(0))
            node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)

        ElseIf node.Nodes(0).GrammerLeftNode.Name = "(" Then

            'VisitNodes(node.Nodes(1))
            'node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)

            'Dim data = MemberScopeStack.Peek().ExprDataStack.Pop()

            'MemberScopeStack.Peek().ExprDataStack.Push(data)



            '***改为表达式列表。表达式列表的返回值是最后一个值***

            Dim exprs As New List(Of AS3.Expr.AS3DataStackElement)

            Dim oc = MemberScopeStack.Peek().ExprDataStack.Count

            VisitNodes(node.Nodes(1))

            node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)

            While MemberScopeStack.Peek().ExprDataStack.Count > oc

                exprs.Add(MemberScopeStack.Peek().ExprDataStack.Pop())

            End While

            exprs.Reverse()

            If exprs.Count > 1 Then
                Dim argsdata As New AS3.Expr.AS3DataStackElement()
                argsdata.IsReg = False
                argsdata.Data = New AS3.Expr.AS3DataValue
                argsdata.Data.FF1Type = Expr.FF1DataValueType.as3_expressionlist
                argsdata.Data.Value = exprs

                MemberScopeStack.Peek().ExprDataStack.Push(argsdata)

            Else
                Dim data = exprs(0)

                MemberScopeStack.Peek().ExprDataStack.Push(data)

            End If






        ElseIf node.Nodes(0).GrammerLeftNode.Name = "Function" Then

            VisitNodes(node.Nodes(0))
            node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)

            VisitNodes(node.Nodes(1))
            node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)

        ElseIf node.Nodes(0).GrammerLeftNode.Name = "Array" Then

            VisitNodes(node.Nodes(0))
            node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)


        ElseIf node.Nodes(0).GrammerLeftNode.Name = "Vector" Then

            VisitNodes(node.Nodes(0))
            node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)
        ElseIf node.Nodes(0).GrammerLeftNode.Type = GrammarNodeType.terminal And node.Nodes(0).GrammerLeftNode.Name = "CONFIG::" Then

            '"CONFIG::" identifier

            Dim v As New AS3.Expr.AS3DataStackElement()
            v.Data = New AS3.Expr.AS3DataValue()
            v.Data.FF1Type = Expr.FF1DataValueType.compiler_const
            v.Data.Value = node.Nodes(1).MatchedToken.StringValue
            MemberScopeStack.Peek().ExprDataStack.Push(v)
        ElseIf node.Nodes(0).GrammerLeftNode.Name = "new" Then
            '"new" <Expression>
            current_new_operator.Push(node.Nodes(1))
            Dim count = current_new_operator.Count


            VisitNodes(node.Nodes(1))
            node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)

            If current_new_operator.Count = count Then
                current_new_operator.Pop()
                '***new 不带构造参数***

                Dim op As New Expr.AS3ExprStep(node.MatchedToken)
                op.Type = Expr.OpType.Constructor
                op.OpCode = "new"

                op.Arg1 = Expr.AS3DataStackElement.MakeReg(MemberScopeStack.Peek().NextRegId())
                op.Arg2 = MemberScopeStack.Peek().ExprDataStack.Pop()

                MemberScopeStack.Peek().ExprDataStack.Push(op.Arg1)
                node.exprsteplist.Add(op)

            ElseIf current_new_operator.Count > count Then
                Throw New Exception
            End If


        End If

    End Sub
    Sub _AccessOpt(node As GrammerExpr)
        '    "." <NSAccess><AccessOpt> | "[" <Expression> "]" <AccessOpt>|<Call><AccessOpt> 
        '   | ".."<NSAccess><AccessOpt> | null;

        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        If node.Nodes.Count = 3 AndAlso node.Nodes(0).GrammerLeftNode.Name <> "[" Then

            If node.Nodes(1).Nodes(0).GrammerLeftNode.Name = "E4XFilter" Or node.Nodes(1).Nodes(0).Nodes(0).GrammerLeftNode.Name = "E4XAccess" Then
                '****如果是 E4XAccess ,则忽略 "."符号***
                VisitNodes(node.Nodes(1))
                VisitNodes(node.Nodes(2))

                node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)
                node.exprsteplist.AddRange(node.Nodes(2).exprsteplist)



            Else



                VisitNodes(node.Nodes(1))

                Dim arg3 = MemberScopeStack.Peek().ExprDataStack.Pop()
                Dim arg2 = MemberScopeStack.Peek().ExprDataStack.Pop()
                Dim arg1 = AS3.Expr.AS3DataStackElement.MakeReg(MemberScopeStack.Peek().NextRegId())

                Dim op As New AS3.Expr.AS3ExprStep(node.MatchedToken)
                op.Arg1 = arg1
                op.Arg2 = arg2
                op.Arg3 = arg3
                op.OpCode = node.Nodes(0).MatchedToken.StringValue
                op.Type = Expr.OpType.Access

                MemberScopeStack.Peek().ExprDataStack.Push(arg1)

                node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)
                node.exprsteplist.Add(op)

                VisitNodes(node.Nodes(2))
                node.exprsteplist.AddRange(node.Nodes(2).exprsteplist)

            End If


        ElseIf node.Nodes.Count = 4 AndAlso node.Nodes(0).GrammerLeftNode.Name = "[" Then

            VisitNodes(node.Nodes(1))

            Dim arg3 = MemberScopeStack.Peek().ExprDataStack.Pop()
            Dim arg2 = MemberScopeStack.Peek().ExprDataStack.Pop()
            Dim arg1 = AS3.Expr.AS3DataStackElement.MakeReg(MemberScopeStack.Peek().NextRegId())

            Dim op As New AS3.Expr.AS3ExprStep(node.MatchedToken)
            op.Arg1 = arg1
            op.Arg2 = arg2
            op.Arg3 = arg3
            op.OpCode = node.Nodes(0).MatchedToken.StringValue
            op.Type = Expr.OpType.Access

            MemberScopeStack.Peek().ExprDataStack.Push(arg1)

            node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)
            node.exprsteplist.Add(op)

            VisitNodes(node.Nodes(3))
            node.exprsteplist.AddRange(node.Nodes(3).exprsteplist)
        ElseIf node.Nodes.Count = 2 Then

            VisitNodes(node.Nodes(0))
            VisitNodes(node.Nodes(1))

            node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)
            node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)

        End If

    End Sub

    Sub _NSAccessOpt(node As GrammerExpr)
        '    "::" <Unit><NSAccessOpt>|null;

        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        If node.Nodes.Count > 0 Then
            VisitNodes(node.Nodes(1))

            Dim arg3 = MemberScopeStack.Peek().ExprDataStack.Pop()
            Dim arg2 = MemberScopeStack.Peek().ExprDataStack.Pop()
            Dim arg1 = AS3.Expr.AS3DataStackElement.MakeReg(MemberScopeStack.Peek().NextRegId())

            Dim op As New AS3.Expr.AS3ExprStep(node.MatchedToken)
            op.Arg1 = arg1
            op.Arg2 = arg2
            op.Arg3 = arg3
            op.OpCode = node.Nodes(0).MatchedToken.StringValue
            op.Type = Expr.OpType.NameSpaceAccess

            MemberScopeStack.Peek().ExprDataStack.Push(arg1)

            node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)
            node.exprsteplist.Add(op)

            VisitNodes(node.Nodes(2))
            node.exprsteplist.AddRange(node.Nodes(2).exprsteplist)
        End If

    End Sub

    Sub _UnitSuffix(node As GrammerExpr)
        '    ::="++" | "--"| null ;    //后缀

        node.exprsteplist = New AS3.Expr.AS3ExprStepList()

        If node.Nodes.Count = 1 Then
            Dim arg1 = MemberScopeStack.Peek().ExprDataStack.Pop()

            Dim op As New AS3.Expr.AS3ExprStep(node.MatchedToken)
            op.OpCode = node.Nodes(0).MatchedToken.StringValue
            'op.Arg1 = arg1

            Dim arg2 = AS3.Expr.AS3DataStackElement.MakeReg(MemberScopeStack.Peek().NextRegId())
            op.Arg1 = arg2
            op.Arg2 = arg1

            op.Type = Expr.OpType.Suffix

            node.exprsteplist.Add(op)

            MemberScopeStack.Peek().ExprDataStack.Push(arg2)
        End If




    End Sub
    Sub _Call(node As GrammerExpr)
        '"("<Argements>")";
        node.exprsteplist = New AS3.Expr.AS3ExprStepList()

        VisitNodes(node.Nodes(1))

        If current_new_operator.Count > 0 AndAlso current_new_operator.Peek().Equals(current_visiting_expression.Peek()) Then
            'Dim op As New Expr.AS3ExprStep(node.MatchedToken)
            'op.Type = Expr.OpType.Constructor
            'op.OpCode = "new"

            'op.Arg1 = Expr.AS3DataStackElement.MakeReg(MemberScopeStack.Peek().NextRegId())
            'op.Arg2 = MemberScopeStack.Peek().ExprDataStack.Pop()

            'MemberScopeStack.Peek().ExprDataStack.Push(op.Arg1)
            'node.exprsteplist.Add(op)

            current_new_operator.Pop()

            '***提取new的主体
            Dim arg3 = MemberScopeStack.Peek().ExprDataStack.Pop()    '参数数组
            Dim arg2 = MemberScopeStack.Peek().ExprDataStack.Pop()    '方法名


            Dim op As New Expr.AS3ExprStep(node.MatchedToken)
            op.Type = Expr.OpType.Constructor
            op.OpCode = "new"

            op.Arg2 = arg2
            op.Arg3 = arg3


            op.Arg1 = Expr.AS3DataStackElement.MakeReg(MemberScopeStack.Peek().NextRegId())

            MemberScopeStack.Peek().ExprDataStack.Push(op.Arg1)
            node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)
            node.exprsteplist.Add(op)

        Else

            Dim arg3 = MemberScopeStack.Peek().ExprDataStack.Pop()    '参数数组
            Dim arg2 = MemberScopeStack.Peek().ExprDataStack.Pop()    '方法名
            Dim arg1 = AS3.Expr.AS3DataStackElement.MakeReg(MemberScopeStack.Peek().NextRegId())

            Dim op As New AS3.Expr.AS3ExprStep(node.MatchedToken)
            op.Arg1 = arg1
            op.Arg2 = arg2
            op.Arg3 = arg3
            op.OpCode = node.Nodes(0).MatchedToken.StringValue
            op.Type = Expr.OpType.CallFunc

            MemberScopeStack.Peek().ExprDataStack.Push(arg1)

            node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)
            node.exprsteplist.Add(op)

        End If


    End Sub


    Sub _Object(node As GrammerExpr)

        node.exprsteplist = New AS3.Expr.AS3ExprStepList()

        Dim dobj As New Hashtable()

        dynamticObjStack.Push(dobj)

        VisitNodes(node.Nodes(1))

        node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)

        dynamticObjStack.Pop()

        Dim obj As New AS3.Expr.AS3DataStackElement
        obj.Data = New AS3.Expr.AS3DataValue
        obj.Data.FF1Type = Expr.FF1DataValueType.dynamicobj
        obj.Data.Value = dobj

        MemberScopeStack.Peek().ExprDataStack.Push(obj)

    End Sub
    Sub _E4XAccess(node As GrammerExpr)
        '"@"<E4XAccess_1>;    <E4XAccess_1>  ::=identifier|"*";
        '<F_E4XAccess>  ::=".."<NSAccess>|".*";

        node.exprsteplist = New AS3.Expr.AS3ExprStepList()

        If node.Nodes(0).GrammerLeftNode.Name = ".." Then



            'If node.MatchedToken.StringValue = ".." Then
            Dim arg2 = MemberScopeStack.Peek().ExprDataStack.Pop()
            Dim arg1 = AS3.Expr.AS3DataStackElement.MakeReg(MemberScopeStack.Peek().NextRegId())




            If node.Nodes(1).Nodes.Count > 0 Then


                VisitNodes(node.Nodes(1))
                node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)



                Dim op As New AS3.Expr.AS3ExprStep(node.MatchedToken)
                op.Arg1 = arg1
                op.Arg2 = arg2
                op.Arg3 = MemberScopeStack.Peek().ExprDataStack.Pop()
                op.OpCode = node.MatchedToken.StringValue
                op.Type = Expr.OpType.E4XAccess


                MemberScopeStack.Peek().ExprDataStack.Push(arg1)
                node.exprsteplist.Add(op)

            Else
                Dim op As New AS3.Expr.AS3ExprStep(node.MatchedToken)
                op.Arg1 = arg1
                op.Arg2 = arg2

                op.OpCode = node.MatchedToken.StringValue
                op.Type = Expr.OpType.E4XAccess


                MemberScopeStack.Peek().ExprDataStack.Push(arg1)
                node.exprsteplist.Add(op)
            End If








            'Else

            '    Dim arg3 As New AS3.Expr.AS3DataStackElement()
            '    arg3.Data = New AS3.Expr.AS3DataValue()
            '    arg3.Data.FF1Type = Expr.FF1DataValueType.e4xxml
            '    arg3.Data.Value = "*"

            '    MemberScopeStack.Peek().ExprDataStack.Push(arg3)

            'End If
        ElseIf node.Nodes(0).GrammerLeftNode.Name = ".*" Then
            Dim arg2 = MemberScopeStack.Peek().ExprDataStack.Peek()
            Dim arg1 = AS3.Expr.AS3DataStackElement.MakeReg(MemberScopeStack.Peek().NextRegId())

            Dim op As New AS3.Expr.AS3ExprStep(node.MatchedToken)
            op.Arg1 = arg1
            op.Arg2 = arg2
            op.OpCode = "*"
            op.Type = Expr.OpType.E4XAccess



            'If op.OpCode = ".." Then
            MemberScopeStack.Peek().ExprDataStack.Pop()
            'End If



            MemberScopeStack.Peek().ExprDataStack.Push(arg1)
            node.exprsteplist.Add(op)


        ElseIf node.Nodes(0).GrammerLeftNode.Name = "*" Then
            'If node.MatchedToken.StringValue = ".." Then
            Dim arg2 = MemberScopeStack.Peek().ExprDataStack.Peek()
            Dim arg1 = AS3.Expr.AS3DataStackElement.MakeReg(MemberScopeStack.Peek().NextRegId())

            Dim op As New AS3.Expr.AS3ExprStep(node.MatchedToken)
            op.Arg1 = arg1
            op.Arg2 = arg2
            op.OpCode = node.MatchedToken.StringValue
            op.Type = Expr.OpType.E4XAccess



            'If op.OpCode = ".." Then
            MemberScopeStack.Peek().ExprDataStack.Pop()
            'End If



            MemberScopeStack.Peek().ExprDataStack.Push(arg1)
            node.exprsteplist.Add(op)



        Else

            'VisitNodes(node.Nodes(1))

            'Dim arg3 = MemberScopeStack.Peek().ExprDataStack.Pop()    '成员名

            Dim arg3 As AS3.Expr.AS3DataStackElement
            If node.Nodes(1).Nodes.Count = 1 Then

                arg3 = New AS3.Expr.AS3DataStackElement
                arg3.Data = New AS3.Expr.AS3DataValue()
                arg3.Data.FF1Type = Expr.FF1DataValueType.identifier
                arg3.Data.Value = GrammerExpr.getNodeValue(node.Nodes(1))

            Else

                '"["<ExpressionList>"]";

                Dim scope As New AS3.AS3MemberListBase(Nothing)

                While scope.LastRegId() < MemberScopeStack.Peek().LastRegId()
                    scope.NextRegId()
                End While

                MemberScopeStack.Push(scope)

                VisitNodes(node.Nodes(1).Nodes(1))

                MemberScopeStack.Pop()

                While MemberScopeStack.Peek().LastRegId() < scope.LastRegId()
                    MemberScopeStack.Peek().NextRegId()
                End While


                Dim exprs = currentparseExprListStack.Pop()

                arg3 = scope.ExprDataStack.Pop()

                For Each e In exprs
                    node.exprsteplist.AddRange(e.exprStepList)
                Next


            End If




            Dim arg2 = MemberScopeStack.Peek().ExprDataStack.Pop()    'xml对象
            Dim arg1 = AS3.Expr.AS3DataStackElement.MakeReg(MemberScopeStack.Peek().NextRegId())

            Dim op As New AS3.Expr.AS3ExprStep(node.MatchedToken)
            op.Arg1 = arg1
            op.Arg2 = arg2
            op.Arg3 = arg3
            op.OpCode = "@"
            op.Type = Expr.OpType.E4XAccess

            MemberScopeStack.Peek().ExprDataStack.Push(arg1)

            'node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)
            node.exprsteplist.Add(op)

        End If


    End Sub

    Sub _E4XAccessOpt(node As GrammerExpr)
        '    <E4XAccessOpt> ::= 
        'identifier<AccessOpt>
        '| <E4XAccess><AccessOpt>
        ' |<AccessOpt>
        '| null;     

        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        If node.Nodes.Count > 0 Then

            If node.Nodes(0).GrammerLeftNode.Type = GrammarNodeType.identifier Then
                Dim arg3 As AS3.Expr.AS3DataStackElement
                node.Nodes(0).exprsteplist = New AS3.Expr.AS3ExprStepList()

                arg3 = New AS3.Expr.AS3DataStackElement()
                arg3.Data = New AS3.Expr.AS3DataValue()
                arg3.Data.FF1Type = Expr.FF1DataValueType.identifier
                arg3.Data.Value = GrammerExpr.getNodeValue(node.Nodes(0))

                Dim arg2 = MemberScopeStack.Peek().ExprDataStack.Pop()
                Dim arg1 = AS3.Expr.AS3DataStackElement.MakeReg(MemberScopeStack.Peek().NextRegId())

                Dim op As New AS3.Expr.AS3ExprStep(node.MatchedToken)
                op.Arg1 = arg1
                op.Arg2 = arg2
                op.Arg3 = arg3
                op.OpCode = node.Nodes(0).MatchedToken.StringValue
                op.Type = Expr.OpType.E4XAccess

                MemberScopeStack.Peek().ExprDataStack.Push(arg1)

                node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)
                node.exprsteplist.Add(op)

                VisitNodes(node.Nodes(1))
                node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)



            ElseIf node.Nodes.Count = 2 Then
                VisitNodes(node.Nodes(0))
                VisitNodes(node.Nodes(1))

                node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)
                node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)
            Else
                VisitNodes(node.Nodes(0))

                node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)

            End If





        End If


    End Sub


    Sub _E4XFilter(node As GrammerExpr)
        '<E4XFilter>    ::="(" <AExprList> ")";
        '输入是一个XML  。。或者还是个XMLList 输出是一个 过滤后的Xmllist
        node.exprsteplist = New AS3.Expr.AS3ExprStepList()

        Dim inputxml = MemberScopeStack.Peek().ExprDataStack.Pop()

        Dim scope As New AS3MemberListBase(Nothing)
        scope.ExprDataStack.Push(inputxml)

        While scope.LastRegId() < MemberScopeStack.Peek().LastRegId()
            scope.NextRegId()
        End While
        MemberScopeStack.Push(scope)

        VisitNodes(node.Nodes(1))


        MemberScopeStack.Pop()

        While MemberScopeStack.Peek().LastRegId() < scope.LastRegId()
            MemberScopeStack.Peek().NextRegId()
        End While


        Dim outxml = AS3.Expr.AS3DataStackElement.MakeReg(MemberScopeStack.Peek().NextRegId())



        Dim filter As New AS3.AS3E4XFilter(node.MatchedToken)
        filter.FilterExprList = node.Nodes(1).exprsteplist
        filter.InputXml = inputxml
        filter.OutPutXml = outxml
        filter.FilterId = AS3.Expr.AS3DataStackElement.MakeReg(MemberScopeStack.Peek().NextRegId())


        MemberScopeStack.Peek().StamentsStack.Peek().Add(filter)


        Dim op As New AS3.Expr.AS3ExprStep(node.MatchedToken)
        op.Type = Expr.OpType.E4XFilter
        op.OpCode = "Filter"
        op.Arg1 = outxml
        op.Arg2 = inputxml
        op.Arg3 = filter.FilterId

        node.exprsteplist.Add(op)

        MemberScopeStack.Peek().ExprDataStack.Push(outxml)

    End Sub

    Sub _E4XNSAPPEND(node As GrammerExpr)
        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        If node.Nodes.Count > 0 Then
            VisitNodes(node.Nodes(0))
            VisitNodes(node.Nodes(1))
            node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)
            node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)
        End If
    End Sub

    Sub _Array(node As GrammerExpr)
        '::="[" <ArrayElements>;
        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        VisitNodes(node.Nodes(1))
        node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)

    End Sub
    Sub _Vector(node As GrammerExpr)
        '::=<DefineTypeVector><VectorConstructor>| "<"<ClassPath>">"<Array> ;
        node.exprsteplist = New AS3.Expr.AS3ExprStepList()

        Dim vecotr As New AS3Vector()

        If node.Nodes.Count = 2 Then
            '<DefineTypeVector>     ::="Vector.<"<DefineType>">";
            vecotr.VectorTypeStr = GrammerExpr.getNodeValue(node.Nodes(0).Nodes(1))

            If node.Nodes(1).Nodes.Count > 0 Then


                VisitNodes(node.Nodes(1))
                node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)

                vecotr.Constructor = MemberScopeStack.Peek().ExprDataStack.Pop()

            End If

            Dim data As New AS3.Expr.AS3DataStackElement()
            data.Data = New AS3.Expr.AS3DataValue()
            data.Data.FF1Type = Expr.FF1DataValueType.as3_vector
            data.Data.Value = vecotr
            MemberScopeStack.Peek().ExprDataStack.Push(data)

        ElseIf node.Nodes.Count = 4 Then
            vecotr.VectorTypeStr = GrammerExpr.getNodeValue(node.Nodes(1))



            VisitNodes(node.Nodes(3))
            node.exprsteplist.AddRange(node.Nodes(3).exprsteplist)
            vecotr.Constructor = MemberScopeStack.Peek().ExprDataStack.Pop()


            Dim data As New AS3.Expr.AS3DataStackElement()
            data.Data = New AS3.Expr.AS3DataValue()
            data.Data.FF1Type = Expr.FF1DataValueType.as3_vector
            data.Data.Value = vecotr
            MemberScopeStack.Peek().ExprDataStack.Push(data)


        End If


    End Sub
    Sub _VectorConstructor(node As GrammerExpr)
        '="("<Argements>")"|null;

        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        If node.Nodes.Count > 0 Then
            VisitNodes(node.Nodes(1))
            node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)
        End If

    End Sub
    Sub _Argements(node As GrammerExpr)
        '::= <AExprList>|null;

        node.exprsteplist = New AS3.Expr.AS3ExprStepList()

        Dim args As New List(Of AS3.Expr.AS3DataStackElement)

        If node.Nodes.Count > 0 Then
            Dim oc = MemberScopeStack.Peek().ExprDataStack.Count

            VisitNodes(node.Nodes(0))


            node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)

            While MemberScopeStack.Peek().ExprDataStack.Count > oc

                args.Add(MemberScopeStack.Peek().ExprDataStack.Pop())

            End While

            args.Reverse()

        End If

        Dim argsdata As New AS3.Expr.AS3DataStackElement()
        argsdata.IsReg = False
        argsdata.Data = New AS3.Expr.AS3DataValue
        argsdata.Data.FF1Type = Expr.FF1DataValueType.as3_callargements
        argsdata.Data.Value = args

        MemberScopeStack.Peek().ExprDataStack.Push(argsdata)

    End Sub
    Sub _ArrayElements(node As GrammerExpr)
        ' ::=<AExprList> "]" |"]" ;

        node.exprsteplist = New AS3.Expr.AS3ExprStepList()

        Dim elements As New List(Of AS3.Expr.AS3DataStackElement)

        If node.Nodes.Count > 1 Then
            Dim oc = MemberScopeStack.Peek().ExprDataStack.Count

            VisitNodes(node.Nodes(0))

            node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)

            While MemberScopeStack.Peek().ExprDataStack.Count > oc

                elements.Add(MemberScopeStack.Peek().ExprDataStack.Pop())

            End While

            elements.Reverse()

        End If

        Dim arraydata As New AS3.Expr.AS3DataStackElement()
        arraydata.IsReg = False
        arraydata.Data = New AS3.Expr.AS3DataValue
        arraydata.Data.FF1Type = Expr.FF1DataValueType.as3_array
        arraydata.Data.Value = elements

        MemberScopeStack.Peek().ExprDataStack.Push(arraydata)


    End Sub
    Sub _ArrayExpressionList(node As GrammerExpr)

    End Sub
    Sub _Expression_1(node As GrammerExpr)

    End Sub
    Sub _ArrayCommaOpt(node As GrammerExpr)

    End Sub

    Sub _ACommaOpt(node As GrammerExpr)
        '","<ACommaOpt_1> |null;
        node.exprsteplist = New AS3.Expr.AS3ExprStepList()

        If node.Nodes.Count > 0 Then
            VisitNodes(node.Nodes(1))
            node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)

        End If

    End Sub
    Sub _ACommaOpt_1(node As GrammerExpr)
        '::=<Expression><ACommaOpt>|null;
        node.exprsteplist = New AS3.Expr.AS3ExprStepList()

        If node.Nodes.Count > 0 Then
            VisitNodes(node.Nodes(0))
            VisitNodes(node.Nodes(1))

            node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)
            node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)
        End If




    End Sub


    Sub _CommaOpt(node As GrammerExpr)
        '","<CommaOpt_1> |null;
        node.exprsteplist = New AS3.Expr.AS3ExprStepList()

        If node.Nodes.Count > 0 Then
            VisitNodes(node.Nodes(1))
            node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)

        End If




    End Sub
    Sub _CommaOpt_1(node As GrammerExpr)
        '::=<Expression><CommaOpt>|null;
        node.exprsteplist = New AS3.Expr.AS3ExprStepList()

        If node.Nodes.Count > 0 Then
            VisitNodes(node.Nodes(0))

            Dim expr As New AS3Expression(node.Nodes(0).MatchedToken)
            expr.exprStepList = node.Nodes(0).exprsteplist
            expr.Value = MemberScopeStack.Peek().ExprDataStack.Peek()

            currentparseExprListStack.Peek().Add(expr)

            VisitNodes(node.Nodes(1))

            node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)
            node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)
        End If




    End Sub


    Sub _ObjectBody(node As GrammerExpr)
        '::=<ObjMembers> "}" | "}";

        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        If node.Nodes.Count > 1 Then
            VisitNodes(node.Nodes(0))
            node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)
        End If

    End Sub
    Sub _ObjMembers(node As GrammerExpr)
        '<ObjMember><ObjMembers1>;
        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        VisitNodes(node.Nodes(0))
        VisitNodes(node.Nodes(1))
        node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)
        node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)

    End Sub
    Sub _ObjMember(node As GrammerExpr)
        '<ObjMember> ::=identifier":"<Expression>|number":"<Expression>|string":"<Expression>|"{"label":"<ObjectBody>;

        node.exprsteplist = New AS3.Expr.AS3ExprStepList()

        If node.Nodes.Count = 3 Then

            Dim obj = dynamticObjStack.Peek()
            Dim vn = node.Nodes(0).MatchedToken

            Dim expr As New AS3Expression(node.Nodes(2).MatchedToken)
            VisitNodes(node.Nodes(2))
            expr.exprStepList = node.Nodes(2).exprsteplist

            node.exprsteplist.AddRange(node.Nodes(2).exprsteplist)
            expr.Value = MemberScopeStack.Peek().ExprDataStack.Pop()

            obj.Add(vn, expr.Value)

        Else

            '***新建Object路线***
            Dim dobj As New Hashtable()

            dynamticObjStack.Push(dobj)

            VisitNodes(node.Nodes(3))

            node.exprsteplist.AddRange(node.Nodes(3).exprsteplist)

            dynamticObjStack.Pop()

            Dim obj As New AS3.Expr.AS3DataStackElement
            obj.Data = New AS3.Expr.AS3DataValue
            obj.Data.FF1Type = AS3.Expr.FF1DataValueType.dynamicobj
            obj.Data.Value = dobj
            MemberScopeStack.Peek().ExprDataStack.Push(obj)


            Dim tobj = dynamticObjStack.Peek()
            Dim vn = node.Nodes(1).MatchedToken



            tobj.Add(vn, MemberScopeStack.Peek().ExprDataStack.Pop())


        End If

    End Sub
    Sub _ObjMembers1(node As GrammerExpr)
        '","<ObjMember><ObjMembers1>|null;

        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        If node.Nodes.Count > 0 Then
            VisitNodes(node.Nodes(1))
            VisitNodes(node.Nodes(2))
            node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)
            node.exprsteplist.AddRange(node.Nodes(2).exprsteplist)
        End If

    End Sub

    Sub _New(node As GrammerExpr)
        node.exprsteplist = New Expr.AS3ExprStepList()

        VisitNodes(node.Nodes(1))
        node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)

        '<New>      ::=  "new" <NewClassOrVector> ;
        '<NewClassOrVector> ::=<ClassPath> <NewOpt>| <Vector>;
        '<NewOpt>   ::=  "("<Argements>")"|null; 
    End Sub
    Sub _NewClassOrVector(node As GrammerExpr)

        '<NewClassOrVector> ::=<ClassPath> <NewOpt>| <Vector> |<ThisSuper> ;

        node.exprsteplist = New Expr.AS3ExprStepList()
        If (node.Nodes.Count = 2) Then


            Dim op As New Expr.AS3ExprStep(node.MatchedToken)
            op.Type = Expr.OpType.Constructor
            op.OpCode = node.Parent.Nodes(0).MatchedToken.StringValue

            Dim classname As New Expr.AS3DataStackElement()
            classname.IsReg = False
            classname.Data = New Expr.AS3DataValue()
            classname.Data.FF1Type = Expr.FF1DataValueType.identifier
            classname.Data.Value = GrammerExpr.getNodeValue(node.Nodes(0))

            op.Arg2 = classname

            If node.Nodes(1).Nodes.Count > 0 Then
                VisitNodes(node.Nodes(1))

                node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)

                op.Arg3 = MemberScopeStack.Peek().ExprDataStack.Pop()

            End If



            op.Arg1 = Expr.AS3DataStackElement.MakeReg(MemberScopeStack.Peek().NextRegId())

            MemberScopeStack.Peek().ExprDataStack.Push(op.Arg1)
            node.exprsteplist.Add(op)

        Else

            Dim op As New Expr.AS3ExprStep(node.MatchedToken)
            op.Type = Expr.OpType.Constructor
            op.OpCode = node.Parent.Nodes(0).MatchedToken.StringValue

            VisitNodes(node.Nodes(0))
            node.exprsteplist.AddRange(node.Nodes(0).exprsteplist)

            op.Arg1 = Expr.AS3DataStackElement.MakeReg(MemberScopeStack.Peek().NextRegId())
            op.Arg2 = MemberScopeStack.Peek().ExprDataStack.Pop()



            MemberScopeStack.Peek().ExprDataStack.Push(op.Arg1)
            node.exprsteplist.Add(op)


        End If


    End Sub
    Sub _NewOpt(node As GrammerExpr)
        '"("<Argements>")"|null; 
        node.exprsteplist = New AS3.Expr.AS3ExprStepList()
        If node.Nodes.Count > 0 Then
            VisitNodes(node.Nodes(1))
            node.exprsteplist.AddRange(node.Nodes(1).exprsteplist)
        End If

    End Sub

End Class
