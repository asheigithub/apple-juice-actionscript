Namespace AS3
    Public MustInherit Class AS3ClassInterfaceBase
        Inherits AS3MemberListBase
        Implements IMemberDataType

        Public Name As String
        'Public Import As New List(Of AS3Import)
        Public Package As AS3Package

        Public ExtendsNames As New List(Of String)

        Public innerClass As New List(Of AS3Class)()
        Public innerInterface As New List(Of AS3Interface)()

		Public ReadOnly as3SrcFile As AS3SrcFile

		''' <summary>
		''' 是否是包外类
		''' </summary>
		''' <remarks></remarks>
		Public IsOutPackage As Boolean

		Public Sub New(token As Token, as3SrcFile As AS3SrcFile)
			MyBase.New(token)
			Me.as3SrcFile = as3SrcFile
		End Sub


	End Class
End Namespace
