''' <summary>
''' 文法符号
''' </summary>
''' <remarks></remarks>
Public Class GrammarNode
    Implements IEquatable(Of GrammarNode)



    Public Name As String

    Public Type As GrammarNodeType


    Public Shared GNodeNull As New GrammarNode() With {.Type = GrammarNodeType.null, .Name = "null"}
    Public Shared GNodeNumber As New GrammarNode() With {.Type = GrammarNodeType.number, .Name = "number"}
    Public Shared GNodeString As New GrammarNode() With {.Type = GrammarNodeType.conststring, .Name = "string"}
    Public Shared GNodeIdentifier As New GrammarNode() With {.Type = GrammarNodeType.identifier, .Name = "identifier"}

    Public Shared GNodeWrong As New GrammarNode() With {.Type = GrammarNodeType.wrong, .Name = "wrong"}

    ''' <summary>
    ''' $$
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared GNodeEOF As New GrammarNode() With {.Type = GrammarNodeType.eof, .Name = "$$"}

    Public Shared GNodeWhiteSpace As New GrammarNode() With {.Type = GrammarNodeType.whitespace, .Name = "S"}

    Public Shared GNodeLabel As New GrammarNode() With {.Type = GrammarNodeType.label, .Name = "label"}


    Public FIRST As New HashSet(Of GrammarNode)()
    Public FOLLOW As New HashSet(Of GrammarNode)()


    Public Function Equals1(other As GrammarNode) As Boolean Implements System.IEquatable(Of GrammarNode).Equals
        Return Type = other.Type AndAlso Name = other.Name
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        If TypeOf obj Is GrammarNode Then
            Return Equals1(obj)
        End If

        Return False

    End Function

    Public Overrides Function GetHashCode() As Integer
        Return Type.GetHashCode() Xor Name.GetHashCode()
    End Function

    Public Overrides Function ToString() As String
        Return String.Format("{0}," & vbTab & "type:{1}", Name, Type)
    End Function

End Class
