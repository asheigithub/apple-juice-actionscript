Namespace AS3
    Public Class AS3Package
        Inherits AS3MemberListBase

        Public Name As String
        Public IsTopLevel As Boolean

        Public Import As New List(Of AS3Import)

        Public AS3File As AS3SrcFile

        Public MainClass As AS3Class
        Public MainInterface As AS3Interface

        Public Sub New(token As Token)
            MyBase.New(token)


        End Sub
        'Public ClassList As New List(Of AS3Class)()
        'Public InterfaceList As New List(Of AS3Interface)()

    End Class
End Namespace
