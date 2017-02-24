Namespace AS3
    Public Class AS3Interface
        Inherits AS3MemberListBase
        Implements IMemberDataType


        Public Name As String
        'Public Import As New List(Of AS3Import)
        Public Package As AS3Package

        Public ExtendsNames As New List(Of String)

        Public innerClass As New List(Of AS3Class)()
        Public innerInterface As New List(Of AS3Interface)()


        ''' <summary>
        ''' 是否是包外类
        ''' </summary>
        ''' <remarks></remarks>
        Public IsOutPackage As Boolean

        Public Sub New(token As Token)
            MyBase.New(token)
            Me.Access.IsInternal = True
        End Sub

        Public Overrides Sub Write(tabs As Integer, srcout As ISrcOut)

            srcout.WriteLn("package " & Package.Name, tabs)
            srcout.WriteLn("{", tabs)

            For Each s In Package.StamentsStack.Peek()
                s.Write(tabs + 1, srcout)
            Next

            WriteBody(tabs + 1, srcout)


            srcout.WriteLn("}", tabs)

            For Each c In innerClass
                c.WriteBody(tabs, srcout)
            Next
            For Each i In innerClass
                i.WriteBody(tabs, srcout)
            Next

        End Sub

        Friend Sub WriteBody(tabs As Integer, srcout As ISrcOut)

            If Not IsOutPackage Then
                For Each im In Package.Import
                    srcout.WriteLn("import " & im.Name & ";", tabs)
                Next
            Else
                For Each im In Package.AS3File.OutPackageImports
                    srcout.WriteLn("import " & im.Name & ";", tabs)
                Next
            End If


            If Not Meta Is Nothing Then
                Meta.Write(tabs, srcout)
            End If

            Dim cline As String = Access.ToString() & "interface " & Name
            If ExtendsNames.Count > 0 Then
                cline &= " extends " & String.Join(",", ExtendsNames.ToArray())
            End If

            srcout.WriteLn(cline, tabs)
            srcout.WriteLn("{", tabs)

            For Each im In StamentsStack.Peek()
                im.Write(tabs + 1, srcout)
            Next

            srcout.WriteLn("}", tabs)
        End Sub

        

        
    End Class
End Namespace

