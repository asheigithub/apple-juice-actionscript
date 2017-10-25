Namespace AS3.Expr
    ''' <summary>
    ''' 表达式求值用寄存器
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AS3Reg
		''' <summary>
		''' 寄存器号
		''' </summary>
		''' <remarks></remarks>
		Public ReadOnly ID As Integer

		''' <summary>
		''' 所属表达式组编号
		''' </summary>
		Public ReadOnly StmtID As Integer

		Public Sub New(id As Integer, groupid As Integer)
			Me.ID = id
			Me.StmtID = groupid
		End Sub

		''' <summary>
		''' 寄存器值
		''' </summary>
		''' <remarks></remarks>
		Public Value As AS3DataValue

    End Class
End Namespace

