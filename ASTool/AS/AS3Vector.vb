Namespace AS3

    Public Class AS3Vector

        Public Overrides Function ToString() As String
            Return "Vector.<" & VectorTypeStr & ">"
        End Function

        ''' <summary>
        ''' 是否构造文法 new &lt;T>[E0, ..., En-1 ,]; 
        ''' </summary>
        Public isInitData As Boolean

        Public VectorTypeStr As String

        Public VectorType As Object

        Public Constructor As AS3.Expr.AS3DataStackElement
    End Class
End Namespace

