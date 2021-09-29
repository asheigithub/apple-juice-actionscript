Public Class GrammarException
    Inherits Exception

    Public token As Token

    Public Sub New(token As Token, msg As String)
        MyBase.New(msg)

        Me.token = token

    End Sub

    Public Overrides Function ToString() As String
		'Return token.sourceFile & vbCrLf & "line:" & token.line & " ptr:" & token.ptr & " " & token.StringValue & " " & Message


		'Console.WriteLine(token.sourceFile & ":" & token.line + 1 & ":Error:" & msg)
		'Console.WriteLine(lines(token.line))
		'Console.WriteLine("^".PadLeft(token.ptr))

		Dim err As String = token.sourceFileFullPath & ":" & token.line + 1 & ":Error: " & token.StringValue & Message

		Return err

	End Function

End Class
