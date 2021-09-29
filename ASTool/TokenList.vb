
Public Class TokenList
    Inherits List(Of Token)

    ''' <summary>
    ''' 指针回零
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Reset()
        currentindx = -1

        For index = 1 To Count - 1
            Dim token = Me(index)

            token.preToken = Me(index - 1)
            token.preToken.nextToken = token

        Next

    End Sub

    

    Public FileName As String

    Private currentindx As Integer
    ''' <summary>
    ''' 获取当前TOKEN
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CurrentToken() As Token
        Get
            If currentindx < 0 Then
                Throw New Exception("先调用NextToken")
            End If


            If currentindx < Count Then
                Return Me(currentindx)
            Else
				'Return Token.TokenEOF

				Dim eof As New Token()
				eof.Type = Token.TokenType.eof
				eof.sourceFile = FileName
				If Count > 0 Then
					eof.line = Me(Count - 1).line
					eof.ptr = Me(Count - 1).ptr
				End If

				Return eof

			End If

        End Get

    End Property
    ''' <summary>
    ''' 移动到下一个非空白非注释的TOKEN并返回
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNextToken() As Token
        
        Do
            currentindx += 1

            If CurrentToken.Type = Token.TokenType.eof Then
				'Return Token.TokenEOF
				'Return New Token() With {.Type = Token.TokenType.eof}
				Return CurrentToken
			End If

            If CurrentToken.Type <> Token.TokenType.comments And CurrentToken.Type <> Token.TokenType.whitespace Then
                Return CurrentToken
            End If

        Loop


       

    End Function


    ''' <summary>
    ''' 返回下一个符号包括空白
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNextTokenWithWhiteBlank() As Token

        Do
            currentindx += 1
            

            Return CurrentToken

        Loop




    End Function



    ''' <summary>
    ''' 观察后面第M个非空非注释TOKEN
    ''' </summary>
    ''' <param name="addptr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SeeToken(addptr As UInt32) As Token
        Dim i As UInt32 = 0

        Dim k As Integer = currentindx

        While k < Count


            Dim temp = Me(k)
            If temp.Type <> Token.TokenType.comments And temp.Type <> Token.TokenType.whitespace Then
                If i = addptr Then
                    Return temp
                End If

                i = i + 1
            End If
            k = k + 1

        End While


		Dim eof As New Token()
		eof.Type = Token.TokenType.eof
		eof.sourceFile = FileName
		If Count > 0 Then
			eof.line = Me(Count - 1).line
			eof.ptr = Me(Count - 1).ptr
		End If

		Return eof

		'Return Token.TokenEOF
	End Function


    'Public Function SkipLPR(index As Integer) As Integer

    '    Do
    '        If Me(index).Type = Token.TokenType.other And Me(index).StringValue = "(" Then

    '            index += 1
    '            index = SkipLPR(index)


    '        ElseIf Me(index).Type = Token.TokenType.other And Me(index).StringValue = ")" Then


    '            Return index + 1
    '        Else
    '            index += 1
    '        End If


    '    Loop
    'End Function

    Public Sub Write(srcout As ISrcOut, tab As Integer)

        Dim line As Int16 = 0
        Dim lastline As Integer = 0

        Dim toline = 0

        Dim strline As String = ""

        For index = 0 To Count - 1

            If Me(index).Type <> Token.TokenType.comments Then

                If Me(index).line > lastline Then
                    lastline = Me(index).line
                    toline += 1
                End If

                While line < toline
                    line += 1

                    srcout.WriteLn(strline, tab)
                    strline = ""
                End While

                'If (Me(index).Type = Token.TokenType.identifier And Me(index).StringValue = "trace") Then
                '    Do

                '        index += 1
                '        If Me(index).Type = Token.TokenType.other And Me(index).StringValue = "(" Then
                '            Exit Do
                '        End If
                '    Loop
                '    index += 1
                '    index = SkipLPR(index)
                'End If

                strline += StringValueToString(Me(index))

            End If
        Next

        If strline.Length > 0 Then
            srcout.WriteLn(strline, tab)
        End If


    End Sub



    Private Function StringValueToString(token As Token) As String
        If token.Type = ASTool.Token.TokenType.const_string Then

            Dim r As String = """"

            For index = 0 To token.StringValue.Length - 1

                If token.StringValue(index) = """" Then
                    r = r + "\"""
                ElseIf token.StringValue(index) = vbCr Then
                    r = r + "\r"
                ElseIf token.StringValue(index) = vbLf Then
                    r = r + "\n"
                ElseIf token.StringValue(index) = vbFormFeed Then
                    r = r + "\f"
                ElseIf token.StringValue(index) = vbBack Then
                    r = r + "\b"
                ElseIf token.StringValue(index) = vbTab Then
                    r = r + "\t"
                ElseIf token.StringValue(index) = "\" Then
                    r = r + "\\"
                Else
                    r = r + token.StringValue(index)
                End If

            Next


            Return r + """"

        Else
            Return token.StringValue
        End If


    End Function

End Class
