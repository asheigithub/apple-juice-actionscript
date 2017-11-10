''' <summary>
''' 文法符号
''' </summary>
''' <remarks></remarks>
Public Class GrammarNode
    Implements IEquatable(Of GrammarNode)


	Private hashcode As Integer
	Public ReadOnly Name As String

	Public ReadOnly Type As GrammarNodeType

	Public Sub New(name As String, type As GrammarNodeType)
		Me.Name = name
		Me.Type = type

		hashcode = type.GetHashCode() Xor name.GetHashCode()

	End Sub


	Public Shared GNodeNull As New GrammarNode("null", GrammarNodeType.null) 'With {.Type = GrammarNodeType.null, .Name = "null"}
	Public Shared GNodeNumber As New GrammarNode("number", GrammarNodeType.number) 'With {.Type = GrammarNodeType.number, .Name = "number"}
	Public Shared GNodeString As New GrammarNode("string", GrammarNodeType.conststring) 'With {.Type = GrammarNodeType.conststring, .Name = "string"}
	Public Shared GNodeIdentifier As New GrammarNode("identifier", GrammarNodeType.identifier) 'With {.Type = GrammarNodeType.identifier, .Name = "identifier"}

	Public Shared GNodeWrong As New GrammarNode("wrong", GrammarNodeType.wrong) 'With {.Type = GrammarNodeType.wrong, .Name = "wrong"}

	''' <summary>
	''' $$
	''' </summary>
	''' <remarks></remarks>
	Public Shared GNodeEOF As New GrammarNode("$$", GrammarNodeType.eof) 'With {.Type = GrammarNodeType.eof, .Name = "$$"}

	Public Shared GNodeWhiteSpace As New GrammarNode("S", GrammarNodeType.whitespace) 'With {.Type = GrammarNodeType.whitespace, .Name = "S"}

	Public Shared GNodeLabel As New GrammarNode("label", GrammarNodeType.label) 'With {.Type = GrammarNodeType.label, .Name = "label"}


	Public Shared GNodeThis As New GrammarNode("this", GrammarNodeType.this) 'With {.Type = GrammarNodeType.this, .Name = "this"}

	Public Shared GNodeSuper As New GrammarNode("super", GrammarNodeType.super) 'With {.Type = GrammarNodeType.super, .Name = "super"}


	Public FIRST As New HashSet(Of GrammarNode)()
    Public FOLLOW As New HashSet(Of GrammarNode)()


    Public Function Equals1(other As GrammarNode) As Boolean Implements System.IEquatable(Of GrammarNode).Equals
		Return Type = other.Type AndAlso String.Equals(Name, other.Name, StringComparison.Ordinal)  'Name = other.Name
	End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        If TypeOf obj Is GrammarNode Then
            Return Equals1(obj)
        End If

        Return False

    End Function

    Public Overrides Function GetHashCode() As Integer
		'Return Type.GetHashCode() Xor Name.GetHashCode()
		Return hashcode
	End Function

    Public Overrides Function ToString() As String
        Return String.Format("{0}," & vbTab & "type:{1}", Name, Type)
    End Function

End Class
