Namespace AS3
    ''' <summary>
    ''' 命名空间
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AS3NameSpace
        Inherits AS3Member

        
        Public Sub New(token As Token)
            MyBase.New(token)
            TypeStr = "namespace"
        End Sub

        Public Package As AS3Package

        Public URI As String = Guid.NewGuid().ToString()

        Public Overrides Sub Write(tabs As Integer, srcout As ISrcOut)
            
            srcout.WriteLn(Access.ToString() & "namespace " & Name & "=""" & URI.Replace("""", """""") & """;", tabs)

        End Sub

       
    End Class
End Namespace

