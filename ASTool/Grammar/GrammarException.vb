Public Class GrammarException
    Inherits Exception

    Public token As Token

    Public Sub New(token As Token, msg As String)
        MyBase.New(msg)

        Me.token = token

    End Sub

    Public Overrides Function ToString() As String
        Return token.sourceFile & vbCrLf & "line:" & token.line & " ptr:" & token.ptr & " " & token.StringValue & " " & Message
    End Function

End Class
