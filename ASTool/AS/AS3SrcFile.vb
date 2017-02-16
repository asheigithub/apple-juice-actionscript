Namespace AS3
    Public Class AS3SrcFile



        Public srcFile As String

        Public Package As AS3Package

        ''' <summary>
        ''' 包外程序代码 比如 if trace等
        ''' </summary>
        ''' <remarks></remarks>
        Public OutPackagePrivateScope As AS3MemberListBase

        Public OutPackageImports As New List(Of AS3Import)

        Public Sub Write(tabs As Integer, srcout As ISrcOut)

            If Not Package Is Nothing Then

                For Each m In Package
                    '***包级变量成员***
                    srcout.WriteLn("package " & Package.Name, tabs)
                    srcout.WriteLn("{", tabs)
                    m.Write(1, srcout)
                    srcout.WriteLn("}", tabs)
                Next

                If Not Package.MainClass Is Nothing Then
                    Package.MainClass.Write(tabs, srcout)
                End If
                If Not Package.MainInterface Is Nothing Then
                    Package.MainInterface.Write(tabs, srcout)
                End If




            End If

            '***包外代码***
            For Each oimp In OutPackageImports
                srcout.WriteLn("import " & oimp.Name, tabs)
            Next
            For Each os In OutPackagePrivateScope.StamentsStack.Peek()
                os.Write(0, srcout)
            Next


        End Sub
    End Class
End Namespace

