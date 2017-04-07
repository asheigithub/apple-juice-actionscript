Public Class LexException
    Inherits Exception

    Public ReadOnly line As Integer
    Public ReadOnly ptr As Integer
    Public Sub New(msg As String, line As Integer, ptr As Integer)
        MyBase.New(msg)

        Me.line = line
        Me.ptr = ptr

    End Sub
End Class
