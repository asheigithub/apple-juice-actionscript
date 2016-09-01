Namespace AS3
    ''' <summary>
    ''' function参数
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AS3Parameter
        Inherits AS3Variable

        ''' <summary>
        ''' 是否是 ...参数
        ''' </summary>
        ''' <remarks></remarks>
        Public IsArrPara As Boolean

       

        Public Sub New(token As Token)
            MyBase.New(token)
            Access.IsPrivate = True
            Access.IsInternal = False
        End Sub

    End Class
End Namespace

