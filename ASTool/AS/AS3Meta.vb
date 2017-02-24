Namespace AS3
    ''' <summary>
    ''' 用[]修饰的东西,是一个表达式
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AS3Meta
        Inherits AS3Expression

        Public Sub New(token As Token)
            MyBase.New(token)
        End Sub

        Public Overrides Sub Write(tabs As Integer, srcout As ISrcOut)
            MyBase.Write(tabs, srcout)

            srcout.WriteLn(Value.ToString(), tabs)

        End Sub

    End Class
End Namespace

