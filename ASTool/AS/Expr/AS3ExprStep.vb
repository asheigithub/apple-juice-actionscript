Namespace AS3.Expr
    ''' <summary>
    ''' 操作类型
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum OpType
        ''' <summary>
        ''' 赋值=
        ''' </summary>
        ''' <remarks></remarks>
        Assigning

        ' ''' <summary>
        ' ''' 三元运算符?   已被短路操作取代
        ' ''' </summary>
        ' ''' <remarks></remarks>
        'Ternary

        ''''逻辑或已被短路操作取代
        '''' <summary>
        '''' ||逻辑或
        '''' </summary>
        '''' <remarks></remarks>
        ''LogicOr

        ''' <summary>
        ''' 逻辑与
        ''' </summary>
        ''' <remarks></remarks>
        LogicAnd
        ''' <summary>
        ''' 位或|
        ''' </summary>
        ''' <remarks></remarks>
        BitOr

        ''' <summary>
        ''' 位异或~
        ''' </summary>
        ''' <remarks></remarks>
        BitXor
        ''' <summary>
        ''' 位与
        ''' </summary>
        ''' <remarks></remarks>
        BitAnd

        ''' <summary>
        ''' 逻辑等 ==, ===, !=,!==
        ''' </summary>
        ''' <remarks></remarks>
        LogicEQ

        ''' <summary>
        ''' 逻辑大于等于之类 >= ,>,...
        ''' </summary>
        ''' <remarks></remarks>
        Logic

        ''' <summary>
        ''' 位移
        ''' </summary>
        ''' <remarks></remarks>
        BitShift

        ''' <summary>
        ''' 加减 + -
        ''' </summary>
        ''' <remarks></remarks>
        Plus

        ''' <summary>
        ''' 乘除
        ''' </summary>
        ''' <remarks></remarks>
        Multiply

        ''' <summary>
        ''' 前置运算符 ++ -- new等
        ''' </summary>
        ''' <remarks></remarks>
        Unary

        ''' <summary>
        ''' 构造类的实例 new
        ''' </summary>
        ''' <remarks></remarks>
        Constructor


        ''' <summary>
        ''' 成员访问 . , [] 
        ''' </summary>
        ''' <remarks></remarks>
        Access

        ''' <summary>
        ''' e4x 访问
        ''' </summary>
        ''' <remarks></remarks>
        E4XAccess
        ''' <summary>
        ''' e4x过滤 (@id==)之类
        ''' </summary>
        ''' <remarks></remarks>
        E4XFilter

        ''' <summary>
        ''' 命名空间访问 ::
        ''' </summary>
        ''' <remarks></remarks>
        NameSpaceAccess

        ''' <summary>
        ''' 函数调用 (,,,)
        ''' </summary>
        ''' <remarks></remarks>
        CallFunc
        ''' <summary>
        ''' 后缀 ++,--
        ''' </summary>
        ''' <remarks></remarks>
        Suffix

        ''' <summary>
        ''' 行标记
        ''' </summary>
        ''' <remarks></remarks>
        Flag

        ''' <summary>
        ''' 条件跳转
        ''' </summary>
        ''' <remarks></remarks>
        IF_GotoFlag

        ''' <summary>
        ''' 无条件跳转
        ''' </summary>
        ''' <remarks></remarks>
        GotoFlag


    End Enum



    ''' <summary>
    ''' 表达式求值之类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AS3ExprStep

        Public Type As OpType

        Public OpCode As String

        Public Arg1 As AS3DataStackElement

        Public Arg2 As AS3DataStackElement

        Public Arg3 As AS3DataStackElement

        Public Arg4 As AS3DataStackElement


        Private Shared flagseed As Integer
        Public Shared Function GetFlagId() As Integer
            flagseed += 1
            Return flagseed
        End Function

        Public Overrides Function ToString() As String

            If Type = OpType.GotoFlag Then 'Or Type = OpType.Flag Then

                Return ("[" & Type.ToString() & "]").PadRight(15) & "Goto " & OpCode & ";"
            ElseIf Type = OpType.Flag Then
                Return ("[" & Type.ToString() & "]").PadRight(15) & OpCode & ":"
            ElseIf Type = OpType.IF_GotoFlag Then

                Return ("[" & Type.ToString() & "]").PadRight(15) & "IF " & Arg1.ToString() & " Goto " & OpCode & ";"
            End If



            Dim result As String = ("[" & Type.ToString() & "]").PadRight(15) & OpCode & vbTab & Arg1.ToString()

            If Not Arg2 Is Nothing Then
                result = result & vbTab & Arg2.ToString()
            End If

            If Not Arg3 Is Nothing Then
                result = result & vbTab & Arg3.ToString()
            End If

            If Not Arg4 Is Nothing Then
                result = result & vbTab & Arg4.ToString()
            End If


            Return result

        End Function

    End Class
End Namespace

