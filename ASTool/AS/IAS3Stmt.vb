Namespace AS3
    ''' <summary>
    ''' AS3语句代码接口 包括成员，赋值等等
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IAS3Stmt
        Sub Write(tabs As Integer, srcout As ISrcOut)

        ReadOnly Property Token As Token

    End Interface
End Namespace

