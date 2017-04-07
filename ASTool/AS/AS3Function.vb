Namespace AS3
    Public Class AS3Function
        Inherits AS3Member
        Implements IAS3MemberList


        Public Sub New(token As Token)
            MyBase.New(token)
        End Sub

        ''' <summary>
        ''' 是否是绑定于类的方法
        ''' </summary>
        ''' <remarks></remarks>
        Public IsMethod As Boolean

        ''' <summary>
        ''' 是否是get方法
        ''' </summary>
        ''' <remarks></remarks>
        Public IsGet As Boolean
        ''' <summary>
        ''' 是否是set方法
        ''' </summary>
        ''' <remarks></remarks>
        Public IsSet As Boolean


        ''' <summary>
        ''' 形参
        ''' </summary>
        ''' <remarks></remarks>
        Public Parameters As New List(Of AS3Parameter)

        ''' <summary>
        ''' 是否是匿名函数
        ''' </summary>
        ''' <remarks></remarks>
        Public IsAnonymous As Boolean

        ''' <summary>
        ''' 是否是构造函数
        ''' </summary>
        ''' <remarks></remarks>
        Public IsConstructor As Boolean



        Public FunctionBody As GrammerExpr

        Public ParentScope As IAS3MemberList



        Public Overrides Sub Write(tabs As Integer, srcout As ISrcOut)

            If Not Meta Is Nothing Then
                'Meta.Write(tabs, srcout)
                For Each m In Meta
                    m.Write(tabs, srcout)
                Next
            End If

            Dim paras As String = ""


            For i = 0 To Parameters.Count - 1
                Dim p = Parameters(i)

                If Not p.ValueExpr Is Nothing Then

                    p.ValueExpr.Write(tabs, srcout)

                End If

            Next

            For i = 0 To Parameters.Count - 1
                Dim p = Parameters(i)
                paras &= p.Name & ":" & p.TypeStr

                If Not p.ValueExpr Is Nothing Then
                    paras &= "=" & p.ValueExpr.ToString()
                End If

                If i < Parameters.Count - 1 Then
                    paras &= ","
                End If

            Next

            If Not IsMethod Then
                srcout.WriteLn("// closure @funid=" & GetHashCode(), tabs)
            End If

            srcout.WriteLn(Access.ToString() & "function " & IIf(IsGet, "get ", IIf(IsSet, "set ", "")) & Name & "(" & paras & ")" & IIf(IsConstructor, "", ":" & TypeStr), tabs)
            srcout.WriteLn("{", tabs)

            WriteBody(tabs + 1, srcout)

            srcout.WriteLn("}", tabs)


        End Sub

        Friend Sub WriteBody(tabs As Integer, srcout As ISrcOut)
            For Each im In StamentsStack.Peek()
                im.Write(tabs, srcout)
            Next
        End Sub


#Region "MemberList"

        Private list As New List(Of AS3Member)

        Public Sub Add(item As AS3Member) Implements System.Collections.Generic.ICollection(Of AS3Member).Add
            list.Add(item)
            _stamentsStack.Peek().Add(item)
        End Sub

        Public Sub Clear() Implements System.Collections.Generic.ICollection(Of AS3Member).Clear
            'list.Clear()
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
            'Throw New NotSupportedException()

            _stamentsStack.Peek().Remove(item)
            Return list.Remove(item)
        End Function

        Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of AS3Member) Implements System.Collections.Generic.IEnumerable(Of AS3Member).GetEnumerator
            Return list.GetEnumerator()
        End Function

        Public Function IndexOf(item As AS3Member) As Integer Implements System.Collections.Generic.IList(Of AS3Member).IndexOf
            Throw New NotSupportedException()
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

        Private _stamentsStack As New Stack(Of List(Of IAS3Stmt))({New List(Of IAS3Stmt)})
        Public ReadOnly Property StamentsStack As Stack(Of System.Collections.Generic.List(Of IAS3Stmt)) Implements IAS3MemberList.StamentsStack
            Get
                Return _stamentsStack
            End Get
        End Property
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
#End Region

        
    End Class
End Namespace


