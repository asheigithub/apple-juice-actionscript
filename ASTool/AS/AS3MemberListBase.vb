Namespace AS3
    Public Class AS3MemberListBase
        Inherits AS3ProgramElement
        Implements IAS3MemberList

        Public Sub New(token As Token)
            MyBase.New(token)

        End Sub

        Public Overrides Sub Write(tabs As Integer, srcout As ISrcOut)
            For Each m In Me
                m.Write(tabs, srcout)
            Next
        End Sub


        Private list As New List(Of AS3Member)

        Public Sub Add(item As AS3Member) Implements System.Collections.Generic.ICollection(Of AS3Member).Add
            list.Add(item)
            _stamentsStack.Peek().Add(item)

        End Sub

        Public Sub Clear() Implements System.Collections.Generic.ICollection(Of AS3Member).Clear
            Throw New NotSupportedException()
        End Sub

        Public Function Contains(item As AS3Member) As Boolean Implements System.Collections.Generic.ICollection(Of AS3Member).Contains
            Return list.Contains(item)
        End Function

        Public Sub CopyTo(array() As AS3Member, arrayIndex As Integer) Implements System.Collections.Generic.ICollection(Of AS3Member).CopyTo
            list.CopyTo(array, arrayIndex)
        End Sub

        Public ReadOnly Property Count As Integer Implements System.Collections.Generic.ICollection(Of AS3Member).Count
            Get
                Return list.Count
            End Get
        End Property

        Public ReadOnly Property IsReadOnly As Boolean Implements System.Collections.Generic.ICollection(Of AS3Member).IsReadOnly
            Get
                Return False
            End Get
        End Property

        Public Function Remove(item As AS3Member) As Boolean Implements System.Collections.Generic.ICollection(Of AS3Member).Remove
            _stamentsStack.Peek().Remove(item)
            Return list.Remove(item)
        End Function

        Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of AS3Member) Implements System.Collections.Generic.IEnumerable(Of AS3Member).GetEnumerator
            Return list.GetEnumerator()
        End Function

        Public Function IndexOf(item As AS3Member) As Integer Implements System.Collections.Generic.IList(Of AS3Member).IndexOf
            Return list.IndexOf(item)
        End Function

        Public Sub Insert(index As Integer, item As AS3Member) Implements System.Collections.Generic.IList(Of AS3Member).Insert
            Throw New NotSupportedException()
        End Sub

        Default Public Property Item(index As Integer) As AS3Member Implements System.Collections.Generic.IList(Of AS3Member).Item
            Get
                Return list(index)
            End Get
            Set(value As AS3Member)
                list(index) = value
            End Set
        End Property

        Public Sub RemoveAt(index As Integer) Implements System.Collections.Generic.IList(Of AS3Member).RemoveAt
            Throw New NotSupportedException()
        End Sub

        Public Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return list.GetEnumerator()
        End Function
        Private _exprdatastack As New Stack(Of AS3.Expr.AS3DataStackElement)
        Public ReadOnly Property ExprDataStack As System.Collections.Generic.Stack(Of Expr.AS3DataStackElement) Implements IAS3MemberList.ExprDataStack
            Get
                Return _exprdatastack
            End Get
        End Property
        Private _regid As Integer
        Public Function NextRegId() As Integer Implements IAS3MemberList.NextRegId
            _regid += 1
            Return _regid
        End Function
        Public Function LastRegId() As Integer Implements IAS3MemberList.LastRegId
            Return _regid
        End Function



		Private _stamentsStack As New Stack(Of List(Of IAS3Stmt))({New List(Of IAS3Stmt)})

		Public ReadOnly Property StamentsStack As Stack(Of System.Collections.Generic.List(Of IAS3Stmt)) Implements IAS3MemberList.StamentsStack
            Get
                Return _stamentsStack
            End Get
        End Property


    End Class
End Namespace

