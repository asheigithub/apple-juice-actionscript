Namespace AS3
    ''' <summary>
    ''' AS3变量
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AS3Variable
        Inherits AS3Member

        Public Sub New(token As Token)
            MyBase.New(token)
        End Sub

        Public Overrides Sub Write(tabs As Integer, srcout As ISrcOut)

            If Not Meta Is Nothing Then
                'Meta.Write(tabs, srcout)
                For Each m In Meta
                    m.Write(tabs, srcout)
                Next
            End If

            Dim defalutvalue As String = ""
            'If Not ValueExpr Is Nothing Then
            '    defalutvalue = " =" & ValueExpr.ToString()
            'End If

            If Not ValueExpr Is Nothing Then
                ValueExpr.Write(tabs, srcout)

                If Not (Not ValueExpr.Value.IsReg AndAlso ValueExpr.Value.Data.FF1Type = Expr.FF1DataValueType.as3_function) Then
                    defalutvalue = " =" & ValueExpr.Value.ToString()
                End If


            End If

            If Not ValueExpr Is Nothing AndAlso _
                (Not ValueExpr.Value.IsReg AndAlso ValueExpr.Value.Data.FF1Type = Expr.FF1DataValueType.as3_function) Then


                srcout.WriteLn(Access.ToString() & "var " & Name & ":" & TypeStr & "=", tabs)

                CType(ValueExpr.Value.Data.Value, AS3Function).Write(tabs, srcout)

                srcout.WriteLn(";", tabs)

            Else
                srcout.WriteLn(Access.ToString() & "var " & Name & ":" & TypeStr & defalutvalue & ";", tabs)
            End If





        End Sub

    End Class
End Namespace

