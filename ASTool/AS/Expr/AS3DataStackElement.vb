Namespace AS3.Expr
    Public Class AS3DataStackElement
        ''' <summary>
        ''' 是否是寄存器
        ''' </summary>
        ''' <remarks></remarks>
        Public IsReg As Boolean

        Public Data As AS3DataValue

        Public Reg As AS3Reg



        Public Overrides Function ToString() As String
           
            If IsReg Then

                Return "<V" & Reg.ID & ">"
            Else

                If Not Data.Value Is Nothing Then

                    If TypeOf Data.Value Is List(Of AS3DataStackElement) Then

                        Dim result As String = "["

                        For Each k In CType(Data.Value, List(Of AS3DataStackElement))

                            result &= k.ToString() & ","

                        Next

                        If result.Length > 1 Then
                            result = result.Substring(0, result.Length - 1)
                        End If

                        result &= "]"

                        If Data.FF1Type = FF1DataValueType.as3_callargements Then
                            result = "args:" & result
                        ElseIf Data.FF1Type = FF1DataValueType.as3_expressionlist Then

                            result = "expressionlist:(" & result.Substring(1, result.Length - 2) & ")"

                        End If

                        Return result
                    ElseIf TypeOf Data.Value Is Hashtable Then '动态对象
                        Dim hastable = CType(Data.Value, Hashtable)

                        Dim result As String = "{"

                        For Each k In hastable.Keys
                            result &= CType(k, Token).StringValue & ":" & CType(hastable(k), AS3.Expr.AS3DataStackElement).ToString()
                            result &= ","
                        Next
                        If result.Length > 1 Then
                            result = result.Substring(0, result.Length - 1)
                        End If

                        result &= "}"

                        Return result
                    ElseIf TypeOf Data.Value Is AS3Vector Then

                        Dim result As String = "Vector.<" & CType(Data.Value, AS3Vector).VectorTypeStr & ">["

                        If Not CType(Data.Value, AS3Vector).Constructor Is Nothing Then

                            result = result & CType(Data.Value, AS3Vector).Constructor.ToString()
                        End If

                        result &= "]"

                        Return result

                    ElseIf Data.FF1Type = FF1DataValueType.const_string Then

                        Return """" & Data.Value.ToString().Replace("\", "\\").Replace(vbBack, "\b").Replace(vbFormFeed, "\f").Replace(vbLf, "\n").Replace(vbCr, "\r").Replace(vbTab, "\t").Replace("""", "\""") & """"
                    ElseIf Data.FF1Type = FF1DataValueType.compiler_const Then
                        Return "CONFIG::" & Data.Value.ToString()

                    ElseIf TypeOf Data.Value Is AS3Function Then
                        Return IIf(CType(Data.Value, AS3Function).IsMethod, "", "closure") & " function @funid=" & Data.Value.GetHashCode().ToString()
                    Else

                        Return Data.Value.ToString()
                    End If


                Else
                    Return "null"

                End If

            End If

        End Function


        Public Shared Function MakeReg(regno As Integer) As AS3DataStackElement

            Dim r As New AS3DataStackElement()
            r.IsReg = True
            r.Reg = New AS3.Expr.AS3Reg()
            r.Reg.ID = regno

            Return r
        End Function


    End Class

End Namespace
