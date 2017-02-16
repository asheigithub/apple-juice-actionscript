Namespace AS3
    Public Class AS3Use
        Implements IAS3Stmt

        Public NameSpaceStr As String

        ''' <summary>
        ''' 是否是 default xml namespace = XXXX
        ''' </summary>
        ''' <remarks></remarks>
        Public IsDefaultXMLNameSpace As Boolean

        Public xmlnsexpr As AS3Expression

        Private matchtoken As Token
        Public Sub New(token As Token)
            Me.matchtoken = token
        End Sub

        Public ReadOnly Property Token As Token Implements IAS3Stmt.Token
            Get
                Return matchtoken
            End Get
        End Property


        Public Sub Write(tabs As Integer, srcout As ISrcOut) Implements IAS3Stmt.Write
            If Not IsDefaultXMLNameSpace Then
                srcout.WriteLn("use namespace " & NameSpaceStr & ";", tabs)
            Else
                For Each c In xmlnsexpr.exprStepList
                    srcout.WriteLn(c.ToString(), tabs)
                Next
                srcout.WriteLn("default xml namespace = " & xmlnsexpr.Value.ToString(), tabs)

            End If

        End Sub
    End Class
End Namespace

