Public Class Token
    Public Enum TokenType
        ''' <summary>
        ''' 标识符
        ''' </summary>
        ''' <remarks></remarks>
        identifier

        ''' <summary>
        ''' 检查出是一个label
        ''' </summary>
        label
        ''' <summary>
        ''' 检查出是一个this
        ''' </summary>
        this_pointer
        ''' <summary>
        ''' 检查出是一个super
        ''' </summary>
        super_pointer

		''' <summary>
		''' 字符串常量
		''' </summary>
		''' <remarks></remarks>
		const_string
		''' <summary>
		''' 内嵌正则表达式
		''' </summary>
		const_regexp

		''' <summary>
		''' 数值常量
		''' </summary>
		''' <remarks></remarks>
		const_number

        ''' <summary>
        ''' 注释
        ''' </summary>
        ''' <remarks></remarks>
        comments

        ''' <summary>
        ''' 空白
        ''' </summary>
        ''' <remarks></remarks>
        whitespace

        ''' <summary>
        ''' 文件尾
        ''' </summary>
        ''' <remarks></remarks>
        eof


        ''' <summary>
        ''' 其他
        ''' </summary>
        ''' <remarks></remarks>
        other


    End Enum


	Public Sub New()
        Type = TokenType.other
        StringValue = String.Empty
    End Sub

    Public preToken As Token
    Public nextToken As Token


    Public Type As TokenType

    Public line As Integer

    Public ptr As Integer

    ''' <summary>
    ''' 字符串值
    ''' </summary>
    ''' <remarks></remarks>
    Public StringValue As String

    ''' <summary>
    ''' 源文件
    ''' </summary>
    ''' <remarks></remarks>
    Public sourceFile As String

	''' <summary>
	''' 源文件全路径
	''' </summary>
	Public sourceFileFullPath As String

	Public Overrides Function ToString() As String
		Return Type.ToString() & " " & StringValue

	End Function


End Class
