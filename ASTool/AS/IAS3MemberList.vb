Namespace AS3
    Public Interface IAS3MemberList
        Inherits IList(Of AS3Member)

        Function NextRegId() As Integer

        Function LastRegId() As Integer


		ReadOnly Property ExprDataStack As Stack(Of AS3.Expr.AS3DataStackElement)

        ReadOnly Property StamentsStack As Stack(Of List(Of IAS3Stmt))

    End Interface
End Namespace



