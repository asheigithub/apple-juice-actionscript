Namespace AS3
    ''' <summary>
    ''' AS3语法元素 比如类，接口，成员
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AS3ProgramElement
        Implements IAS3Stmt

        Public Sub New(token As Token)
            Me.token = token
        End Sub

        Public token As Token

        Public Access As New AS3Access()

        ''' <summary>
        ''' 用[SWF()]这样的东西表示的元属性
        ''' </summary>
        ''' <remarks></remarks>
        Public Meta As List(Of AS3Meta)
        'Public Meta As AS3Meta

        Private ReadOnly Property IAS3Stmt_Token As Token Implements IAS3Stmt.Token
            Get
                Return token
            End Get
        End Property

        Public Overridable Sub Write(tabs As Integer, srcout As ISrcOut) Implements IAS3Stmt.Write

        End Sub



    End Class
End Namespace

