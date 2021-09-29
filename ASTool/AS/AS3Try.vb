Namespace AS3
    Public Class AS3Try
        Implements IAS3Stmt
        ''' <summary>
        ''' 编译时用，保持的tryid
        ''' </summary>
        Public holdTryId As Integer

        Public label As String

        Public TryBlock As List(Of IAS3Stmt)

        Public CatchList As New List(Of AS3Catch)

        Public FinallyBlock As List(Of IAS3Stmt)

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

            srcout.WriteLn(IIf(label Is Nothing, "", label & ":") & "try", tabs)
            srcout.WriteLn("{", tabs)

            For Each b In TryBlock
                b.Write(tabs + 1, srcout)
            Next

            srcout.WriteLn("}", tabs)

            For Each c In CatchList
                srcout.WriteLn("catch (" & c.CatchVariable.Name & ":" & c.CatchVariable.TypeStr & ")", tabs)
                srcout.WriteLn("{", tabs)

                For Each cb In c.CatchBlock
                    cb.Write(tabs + 1, srcout)
                Next

                srcout.WriteLn("}", tabs)
            Next

            If Not FinallyBlock Is Nothing Then

                srcout.WriteLn("finally", tabs)
                srcout.WriteLn("{", tabs)

                For Each b In FinallyBlock
                    b.Write(tabs + 1, srcout)
                Next
                srcout.WriteLn("}", tabs)
            End If

        End Sub
    End Class

    Public Class AS3Catch
        Public CatchVariable As AS3Variable

        Public CatchBlock As List(Of IAS3Stmt)

    End Class



End Namespace

