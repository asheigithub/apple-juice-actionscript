Imports ASTool.Token
''' <summary>
''' 分词
''' </summary>
''' <remarks></remarks>
Public Class Lex

    Public File As String
    Public defineWords As List(Of String)

    Public defineSkipBlankWords As List(Of String)

    Private specBrackets As New Stack(Of Token)

    Public parseXML As Boolean

    Public Sub New(file As String)
        Me.File = file
        defineWords = New List(Of String)

        defineSkipBlankWords = New List(Of String)

    End Sub

    Public Sub New(file As String, definewords As IEnumerable(Of String), defineSkipBlankWords As IEnumerable(Of String), parseXML As Boolean)
        Me.New(file)
        If Not definewords Is Nothing Then
            'Me.defineWords.AddRange(definewords.OrderByDescending(Of Integer)(Function(i) i.Length).ToArray())
            Dim temp As New List(Of String)
            temp.AddRange(definewords)

            temp.Sort(New Comparison(Of String)(Function(s1, s2) s2.Length - s1.Length))

            Me.defineWords.AddRange(temp)
        End If

        If Not defineSkipBlankWords Is Nothing Then
            'Me.defineSkipBlankWords.AddRange(defineSkipBlankWords.OrderByDescending(Of Integer)(Function(i) i.Length).ToArray())

            Dim temp As New List(Of String)
            temp.AddRange(defineSkipBlankWords)

            temp.Sort(New Comparison(Of String)(Function(s1, s2) s2.Length - s1.Length))

            Me.defineSkipBlankWords.AddRange(temp)

        End If

        Me.parseXML = parseXML
    End Sub

    Public Function GetWords(input As String) As TokenList

        cline = 0

        input = input.Replace(vbCrLf, vbLf)
        input = input.Replace(vbCr, vbLf)

        Dim ptr As Integer = 0

        Dim word As Token



        Dim words = New TokenList()
        words.FileName = File

        Do

            word = getNextWord(input, ptr)
            If Not word Is Nothing Then

                words.Add(word)
            End If



        Loop While Not word Is Nothing

        cline = 0


        Return words

    End Function

    Private lastword As Token

    Private cline As Int16

    Private linepos As Int16

    Private Function getNextWord(input As String, ByRef ptr As Integer) As Token

        Dim xmllen As Integer = 0

        Dim currentptr As Integer = ptr

        If (currentptr >= input.Length) Then
            Return Nothing
        End If

        Dim result As Token = New Token()
        result.sourceFile = File

        Dim ch As String = Nothing

        Dim tempwhitespace As String = ""
        Dim templf As Boolean = False
        While currentptr < input.Length
            ch = input(currentptr)

            If isWhiteSpace(ch) Then

                If ch = vbLf Then
                    templf = True
                End If

                getNextChar(input, currentptr)
                tempwhitespace += " "
            Else

                If tempwhitespace.Length > 0 Then

                    result.line = cline
                    result.ptr = linepos
                    result.Type = TokenType.whitespace
                    result.StringValue = " "
                    If templf Then
                        result.StringValue = vbLf
                    End If
                    ptr = currentptr

                    Return result
                End If


                Exit While
            End If


        End While

        If ch = "/" Then




            Dim nextchar As String = seeNextChar(input, currentptr)
            If nextchar = "/" Then  '读注释
                getNextChar(input, currentptr)

                result.line = cline
                result.ptr = linepos
                result.Type = Token.TokenType.comments
                result.StringValue += "//"

                Do
                    Dim nn = getNextChar(input, currentptr)
                    If nn = vbLf Or nn Is Nothing Then

                        Exit Do
                    Else

                        result.StringValue += nn
                    End If

                Loop



            ElseIf nextchar = "*" Then  '读注释
                getNextChar(input, currentptr)

                result.line = cline
                result.ptr = linepos
                result.Type = Token.TokenType.comments
                result.StringValue += "/*"

                Do
                    Dim n1 As String = seeNextChar(input, currentptr)
                    getNextChar(input, currentptr)
                    Dim n2 As String = seeNextChar(input, currentptr)

                    If n1 = "*" And n2 = "/" Then

                        result.StringValue += n1 + n2

                        getNextChar(input, currentptr)
                        getNextChar(input, currentptr)

                        Exit Do
                    End If

                    result.StringValue += n1

                Loop
            ElseIf lastword Is Nothing OrElse (lastword.Type = TokenType.other And lastword.StringValue = "=") _
                                        OrElse (lastword.Type = TokenType.other And lastword.StringValue = ",") _
                                        OrElse (lastword.Type = TokenType.other And lastword.StringValue = ";") _
                                        OrElse (lastword.Type = TokenType.other And lastword.StringValue = "(") Then
                result.line = cline
                result.ptr = linepos
                result.Type = TokenType.const_string
                result.StringValue = "/"

                '**读正则模式
                Dim s1 As Int16 = 1
                While s1 > 0
                    Dim nc = getNextChar(input, currentptr)

                    If nc = "/" Then
                        s1 -= 1
                        result.StringValue &= nc
                        If s1 = 0 Then
                            Dim gc = getNextChar(input, currentptr)
                            While Char.IsLetter(gc)
                                result.StringValue &= gc

                                gc = getNextChar(input, currentptr)
                            End While

                            Console.WriteLine("代码中发现内嵌正则表达式" & result.StringValue)


                            Exit While
                        End If
                    ElseIf nc = "\" Then
                        result.StringValue &= nc
                        Dim s2 = getNextChar(input, currentptr)

                        If s2 = "/" Then
                            result.StringValue &= s2
                            'getNextChar(input, currentptr)
                        End If

                    Else
                        result.StringValue &= nc
                        'getNextChar(input, currentptr)
                    End If

                End While



            ElseIf findIndefinewords(ch, input, currentptr) Then
                GoTo flagdefwords
            Else

                result.line = cline
                result.ptr = linepos
                result.Type = Token.TokenType.other
                result.StringValue = ch
                getNextChar(input, currentptr)

            End If

        ElseIf ch = """" Or ch = "'" Then
            '***读字符串

            result.line = cline
            result.ptr = linepos
            result.Type = Token.TokenType.const_string

            Do
                Dim nn As String = getNextChar(input, currentptr)

                If nn = vbCr Or nn Is Nothing Then
                    'Throw New Exception("字符串读取错误")
                    Exit Do
                End If

                If nn = ch Then
                    getNextChar(input, currentptr)
                    Exit Do
                ElseIf nn = "\" Then
                    Dim n2 = getNextChar(input, currentptr)

                    If n2 = ch Then
                        result.StringValue += ch
                    ElseIf n2 = "\" Then
                        result.StringValue += "\"
                    ElseIf n2 = "b" Then
                        result.StringValue += vbBack
                    ElseIf n2 = "f" Then
                        result.StringValue += vbFormFeed
                    ElseIf n2 = "n" Then
                        result.StringValue += vbLf
                    ElseIf n2 = "r" Then
                        result.StringValue += vbCr
                    ElseIf n2 = "t" Then
                        result.StringValue += vbTab
                    Else
                        result.StringValue += n2
                    End If

                Else
                    result.StringValue += nn
                End If



            Loop
        ElseIf findIndefinewords(ch, input, currentptr) Then   '读设定字符
flagdefwords:
            Do
                For Each w In defineWords
                    Dim test As String = ch

                    For k = 0 To w.Length - 2
                        test = test & seeNextChar(input, currentptr + k)
                    Next

                    If w = test Then
                        result.line = cline
                        result.ptr = linepos
                        result.Type = TokenType.other
                        result.StringValue = w

                        getNextChar(input, currentptr)
                        For k = 0 To w.Length - 2
                            getNextChar(input, currentptr)
                        Next

                        '****要处理.Vector<  >=这样的情况， 遇到.Vector<,需要脱掉一个">"
                        If result.StringValue = "Vector.<" Then
                            specBrackets.Push(result)
                        End If


                        Exit Do

                    End If
                Next

                For Each w In defineSkipBlankWords
                    Dim test As String = ch

                    Dim real As String = ch

                    Dim k As Integer = 0

                    While w.StartsWith(test)
                        test = test & seeNextChar(input, currentptr + k)
                        real = real & seeNextChar(input, currentptr + k)
                        k = k + 1

                        test = test.TrimEnd()

                        If w = test Then
                            result.line = cline
                            result.ptr = linepos
                            result.Type = TokenType.other
                            result.StringValue = w

                            getNextChar(input, currentptr)
                            For k = 0 To real.Length - 2
                                getNextChar(input, currentptr)
                            Next


                            Exit Do

                        End If

                    End While





                Next





            Loop
        ElseIf isIdStChar(ch) Then
            '***读取标识符
            result.Type = Token.TokenType.identifier
            result.line = cline
            result.ptr = linepos
            result.StringValue += ch


            Do
                Dim nn = getNextChar(input, currentptr)

                If nn Is Nothing OrElse (Not (isIdStChar(nn) Or Char.IsDigit(nn(0)))) Then
                    Exit Do
                Else

                    result.StringValue += nn

                End If


            Loop


            'ElseIf ch = "0" Then
        ElseIf ch = "<" AndAlso findxml(ch, input, currentptr, xmllen) > 0 Then
            result.line = cline
            result.ptr = linepos
            result.Type = Token.TokenType.const_string

            result.StringValue = "<"
            For index = 1 To xmllen - 1
                result.StringValue &= getNextChar(input, currentptr)
            Next
            getNextChar(input, currentptr)

        ElseIf ch = "." AndAlso Not seeNextChar(input, currentptr) Is Nothing AndAlso Char.IsDigit(seeNextChar(input, currentptr)) Then
            GoTo readnumber
        ElseIf ch = "0" AndAlso ("x" = seeNextChar(input, currentptr) Or "X" = seeNextChar(input, currentptr)) Then
            result.Type = TokenType.const_number
            result.line = cline
            result.ptr = linepos
            result.StringValue += ch

            result.StringValue += getNextChar(input, currentptr)

            Do
                Dim nn = getNextChar(input, currentptr)

                If Char.IsDigit(nn(0)) Or nn.ToLower() = "a" Or nn.ToLower() = "b" Or nn.ToLower() = "c" Or nn.ToLower() = "d" Or nn.ToLower() = "e" Or nn.ToLower() = "f" Then
                    result.StringValue += nn
                ElseIf isIdStChar(nn) Then
                    Throw New Exception("解析数值错误")

                Else

                    Exit Do
                End If


            Loop


        ElseIf Char.IsDigit(ch(0)) Then

readnumber:

            result.Type = TokenType.const_number
            result.line = cline
            result.ptr = linepos
            result.StringValue += ch

            result.StringValue += getNumberSerial(input, currentptr)

            Dim csymobl = getNextChar(input, currentptr)
            If csymobl = "." Then

                If result.StringValue(0) = "." Then
                    Throw New Exception("解析数值错误")
                End If

                result.StringValue += csymobl

                result.StringValue += getNumberSerial(input, currentptr)


                csymobl = getNextChar(input, currentptr)

            End If

            If Not csymobl Is Nothing AndAlso csymobl.ToLower() = "e" Then
                Dim en1 = seeNextChar(input, currentptr)

                If Not en1 Is Nothing AndAlso (en1 = "+" Or en1 = "-") Then
                    Dim en2 = seeNextChar(input, currentptr + 1)

                    If Not en2 Is Nothing AndAlso Char.IsDigit(en2(0)) Then
                        If en1 = "+" Or en1 = "-" Then
                            result.StringValue += csymobl
                            result.StringValue += getNextChar(input, currentptr)

                            result.StringValue += getNumberSerial(input, currentptr)

                            getNextChar(input, currentptr)

                        End If
                    End If
                ElseIf Not en1 Is Nothing AndAlso Char.IsDigit(en1(0)) Then
                    result.StringValue += csymobl
                    result.StringValue += getNumberSerial(input, currentptr)
                    getNextChar(input, currentptr)

                End If



            End If


            'ElseIf ch = "+" AndAlso seeNextChar(input, currentptr) = "+" Then
            '    result.line = cline
            '    result.Type = TokenType.other
            '    result.StringValue = "++"

            '    getNextChar(input, currentptr)
            '    getNextChar(input, currentptr)


        Else



            result.line = cline
            result.ptr = linepos
            result.Type = TokenType.other

            If isWhiteSpace(ch) Then
                result.Type = TokenType.whitespace
                If templf Then
                    result.StringValue = vbLf
                End If
            End If

            result.StringValue = ch
            getNextChar(input, currentptr)

        End If



        lastword = result

        ptr = currentptr
        Return result
    End Function


    Dim partern As String = "<(?<HtmlTag>[\w]+)[^>]*>.*?</\k<HtmlTag>>" &
         "|" &
         "<!\[CDATA\[.*?]]>" &
         "|" &
         "<[\w]+\s*/>"
    Private expr As New Text.RegularExpressions.Regex(partern, Text.RegularExpressions.RegexOptions.Compiled Or Text.RegularExpressions.RegexOptions.Singleline)
    Private Function findxml(ch As String, input As String, currentptr As Integer, ByRef len As Integer) As Integer

        If Not parseXML Then
            Return 0
        End If

        If Not Char.IsLetterOrDigit(seeNextChar(input, currentptr)) And "!" <> seeNextChar(input, currentptr) Then
            Return 0
        End If

        Dim expr As New Text.RegularExpressions.Regex(partern, Text.RegularExpressions.RegexOptions.Singleline)

        Dim m = expr.Match(input, currentptr)

        If m.Success AndAlso currentptr = m.Index Then

            Console.WriteLine("代码中发现内嵌XML" & m.Value)

            len = m.Length
            Return m.Length
        End If


        Return 0

    End Function


    Private Function findIndefinewords(ch As String, input As String, currentptr As Integer) As Boolean
        If ch = ">" Then
            If specBrackets.Count > 0 AndAlso specBrackets.Peek().StringValue = "Vector.<" Then
                specBrackets.Pop()

                Return False
            End If

        End If


        If defineWords.Count > 0 Then

            For Each w In defineWords
                Dim test As String = ch

                For k = 0 To w.Length - 2
                    test = test & seeNextChar(input, currentptr + k)
                Next

                If w = test Then
                    Return True

                End If
            Next


        End If


        If defineSkipBlankWords.Count > 0 Then

            For Each w In defineSkipBlankWords
                Dim test As String = ch

                Dim real As String = ch

                Dim k As Integer = 0

                While w.StartsWith(test)
                    test = test & seeNextChar(input, currentptr + k)
                    real = real & seeNextChar(input, currentptr + k)
                    k = k + 1

                    test = test.TrimEnd()

                    If w = test Then
                        Return True

                    End If

                End While





            Next


        End If

        Return False
    End Function

    Private Function getNumberSerial(input As String, ByRef currentptr As Integer) As String
        Dim r As String = ""
        Do
            Dim nc = seeNextChar(input, currentptr)

            If nc Is Nothing Then
                Return r
            End If

            If Char.IsDigit(nc(0)) Then
                r = r + getNextChar(input, currentptr)
            ElseIf isIdStChar(nc) And nc.ToLower() <> "e" Then
                Throw New Exception("解析数值错误")
            Else

                Return r
            End If

        Loop


    End Function


    Private Function getNextChar(input As String, ByRef currentptr As Integer) As String



        currentptr += 1

        If currentptr < input.Length Then

            If input(currentptr - 1) = vbLf Then
                cline += 1
                linepos = 0
            End If

            If input(currentptr - 1) = vbTab Then
                linepos += 4
            Else
                linepos += 1
            End If



            Return input(currentptr)
        Else
            Return Nothing
        End If

    End Function


    Private Function seeNextChar(input As String, currentptr As Integer) As String
        If currentptr + 1 < input.Length Then
            Return input(currentptr + 1)
        Else
            Return Nothing
        End If

    End Function

    Private Function isWhiteSpace(ch As String) As Boolean
        Return (ch = " " Or ch = "　" Or ch = " " Or ch = vbTab Or ch = vbCr Or ch = vbLf)
    End Function

    ''' <summary>
    ''' 是否可用作标识符起始的字符
    ''' </summary>
    ''' <param name="ch"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function isIdStChar(ch As String) As Boolean

        Dim b As Byte() = System.Text.Encoding.Unicode.GetBytes(ch)

        Dim value = b(0) * 256 + b(1)

        If Char.IsLetter(ch(0)) Or ch = "_" Or ch = "$" Then
            Return True
        End If

        Return False

        'If (value < 91 And value > 64) Or (value > 96 And value < 123) Or ch = "_" Then
        '    Return True
        'End If

        'If (value >= &H4E00 And value <= &H9FA5) Then
        '    Return True
        'End If

        'Return False

    End Function



End Class
