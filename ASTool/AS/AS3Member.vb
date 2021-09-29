Namespace AS3
    ''' <summary>
    ''' AS3成员
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AS3Member
        Inherits AS3ProgramElement
        Implements IMemberDataType


        Public Name As String

        Public TypeStr As String

        ''' <summary>
        ''' 类型定义
        ''' </summary>
        ''' <remarks></remarks>
        Public TypeDefine As IMemberDataType

        Public ValueExpr As AS3Expression

        Public Sub New(token As Token)
            MyBase.New(token)
        End Sub
    End Class
End Namespace

