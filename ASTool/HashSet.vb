Public Class HashSet(Of T)
    Implements ICollection(Of T), IList(Of T)

    Private ReadOnly dict As New Dictionary(Of T, Object)
    Private ReadOnly list As New List(Of T)


    Public ReadOnly Property Count As Integer Implements ICollection(Of T).Count
        Get
            Return list.Count
        End Get
    End Property

    Public ReadOnly Property IsReadOnly As Boolean Implements ICollection(Of T).IsReadOnly
        Get
            Return False
        End Get
    End Property

    Default Public Property Item(index As Integer) As T Implements IList(Of T).Item
        Get
            Return list(index)
        End Get
        Set(value As T)

            Throw New NotImplementedException()

        End Set
    End Property

    Public Delegate Function selectFunc(n As T) As String

    Public Function _Select(f As selectFunc) As String()
        Dim s As New List(Of String)

        For index = 0 To list.Count - 1

            s.Add(f(list(index)))
        Next

        Return s.ToArray()
    End Function

    Public Sub CopyTo(array As T())
        For index = 0 To array.Length - 1
            If list.Count > index Then
                array(index) = list(index)
            End If

        Next
    End Sub

    Public Sub Add(item As T) Implements ICollection(Of T).Add
        If dict.ContainsKey(item) Then
            Return
        End If
        list.Add(item)
        dict.Add(item, Nothing)
    End Sub

    Public Sub Clear() Implements ICollection(Of T).Clear
        list.Clear()
        dict.Clear()
    End Sub

    Public Function Contains(item As T) As Boolean Implements ICollection(Of T).Contains
        Return dict.ContainsKey(item)
    End Function

    Public Sub CopyTo(array() As T, arrayIndex As Integer) Implements ICollection(Of T).CopyTo
        Throw New NotImplementedException()
    End Sub

    Public Function Remove(item As T) As Boolean Implements ICollection(Of T).Remove
        Throw New NotImplementedException()
    End Function

    Public Function GetEnumerator() As IEnumerator(Of T) Implements IEnumerable(Of T).GetEnumerator
        Return list.GetEnumerator()
    End Function

    Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return list.GetEnumerator()
    End Function

    Public Function IndexOf(item As T) As Integer Implements IList(Of T).IndexOf
        Throw New NotImplementedException()
    End Function

    Public Sub Insert(index As Integer, item As T) Implements IList(Of T).Insert
        Throw New NotImplementedException()
    End Sub

    Public Sub RemoveAt(index As Integer) Implements IList(Of T).RemoveAt
        Throw New NotImplementedException()
    End Sub
End Class
