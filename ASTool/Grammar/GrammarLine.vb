''' <summary>
''' 一行文法 
''' </summary>
''' <remarks></remarks>
Public Class GrammarLine
    ''' <summary>
    ''' 左边定义
    ''' </summary>
    ''' <remarks></remarks>
    Public Main As GrammarNode

    ''' <summary>
    ''' 导出式
    ''' </summary>
    ''' <remarks></remarks>
    Public Derivation As New List(Of GrammarNode)

    Public Overrides Function ToString() As String
        Dim names As New List(Of String)
        For index = 0 To Derivation.Count - 1
            names.Add(Derivation(index).Name)
        Next

        Return Main.Name & "->" & String.Join(" ", names.ToArray()) 'Derivation.Select(Function(o) o.Name))
    End Function

End Class
