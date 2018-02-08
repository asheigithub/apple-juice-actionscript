Public Class AS3LexKeywords
	''' <summary>
	''' "CONFIG::", "...", "..", "++", "--"....等等
	''' </summary>
	''' <remarks></remarks>
	Public Shared LEXKEYWORDS As String() = {"CONFIG::", "...", "..", "..*", "++", "--", "||", "||=", ":*", "::" _
															, "&&", "&&=", "<<", ">>", ">>>", "<=", ">=" _
															, "==", "!=", "===", "!==", "Vector.<" _
															, "+=", "-=", "*=", "/=", "%=", ">>=", "<<=", ">>>=", "&=", "^=", "|="}


	Public Shared LEXSKIPBLANKWORDS As String() = {".*", "default:", ":void"}

End Class
