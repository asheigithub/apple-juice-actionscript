Public Enum GrammarNodeType
    ''' <summary>
    ''' 非终结符 
    ''' </summary>
    ''' <remarks></remarks>
    non_terminal

    ''' <summary>
    ''' 终结符 如"+" "("
    ''' </summary>
    ''' <remarks></remarks>
    terminal

    ''' <summary>
    ''' 检测出的label
    ''' </summary>
    label

    ''' <summary>
    ''' 关键字this
    ''' </summary>
    this

    ''' <summary>
    ''' 关键字super
    ''' </summary>
    super

    ''' <summary>
    ''' 终结符-数字 number
    ''' </summary>
    ''' <remarks></remarks>
    number

    ''' <summary>
    ''' 终结符-标识符 identifier
    ''' </summary>
    ''' <remarks></remarks>
    identifier

    ''' <summary>
    ''' 终结符-字符串 string
    ''' </summary>
    ''' <remarks></remarks>
    conststring

    ''' <summary>
    ''' 终结符-空匹配 null
    ''' </summary>
    ''' <remarks></remarks>
    null

    ''' <summary>
    ''' 空白符号S 
    ''' </summary>
    ''' <remarks></remarks>
    whitespace


    ''' <summary>
    ''' 右端输入结束符 $$
    ''' </summary>
    ''' <remarks></remarks>
    eof

    ''' <summary>
    ''' 错误 wrong
    ''' </summary>
    ''' <remarks></remarks>
    wrong


End Enum
    
