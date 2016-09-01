Namespace AS3.Expr
    Public Enum FF1DataValueType
        dynamicobj
        e4xxml
        identifier
        const_number
        const_string
        as3_function
        as3_array
        as3_vector
        as3_callargements

        as3_expressionlist

        compiler_const

    End Enum


    ''' <summary>
    ''' AS3表达式值
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AS3DataValue

        Public FF1Type As FF1DataValueType

        Public Value As Object



    End Class
End Namespace

