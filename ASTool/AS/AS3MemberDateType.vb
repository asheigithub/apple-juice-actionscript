Namespace AS3

    Public Enum MemberDataTypes
        ''' <summary>
        ''' *
        ''' </summary>
        ''' <remarks></remarks>
        Any
        ''' <summary>
        ''' void
        ''' </summary>
        ''' <remarks></remarks>
        void
        ''' <summary>
        ''' String
        ''' </summary>
        ''' <remarks></remarks>
        _string
        ''' <summary>
        ''' uint
        ''' </summary>
        ''' <remarks></remarks>
        _uint
        ''' <summary>
        ''' int
        ''' </summary>
        ''' <remarks></remarks>
        int

        ''' <summary>
        ''' Number
        ''' </summary>
        ''' <remarks></remarks>
        Number
        ''' <summary>
        ''' Array
        ''' </summary>
        ''' <remarks></remarks>
        Array
        ''' <summary>
        ''' namespace
        ''' </summary>
        ''' <remarks></remarks>
        _namespace
        ''' <summary>
        ''' Boolean
        ''' </summary>
        ''' <remarks></remarks>
        _Boolean
        ''' <summary>
        ''' Object
        ''' </summary>
        ''' <remarks></remarks>
        _Object
        ''' <summary>
        ''' Function
        ''' </summary>
        ''' <remarks></remarks>
        _Function
        ''' <summary>
        ''' Class 的定义
        ''' </summary>
        ''' <remarks></remarks>
        ClassDef

        ''' <summary>
        ''' Vector.
        ''' </summary>
        ''' <remarks></remarks>
        Vector

        ''' <summary>
        ''' 类
        ''' </summary>
        ''' <remarks></remarks>
        _Class

        ''' <summary>
        ''' 接口
        ''' </summary>
        ''' <remarks></remarks>
        _Interface
    End Enum

    ''' <summary>
    ''' 标记可以作为类型定义
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IMemberDataType

    End Interface

    ''' <summary>
    ''' 成员数据类型 var i:int 中的int
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AS3MemberDateType
        Implements IMemberDataType

        Public Shared ANY As AS3MemberDateType = New AS3MemberDateType() With {.DateType = MemberDataTypes.Any}
        Public Shared VOID As AS3MemberDateType = New AS3MemberDateType() With {.DateType = MemberDataTypes.void}
        Public Shared _STRING As AS3MemberDateType = New AS3MemberDateType() With {.DateType = MemberDataTypes._string}
        Public Shared UINT As AS3MemberDateType = New AS3MemberDateType() With {.DateType = MemberDataTypes._uint}
        Public Shared INT As AS3MemberDateType = New AS3MemberDateType() With {.DateType = MemberDataTypes.int}
        Public Shared NUMBER As AS3MemberDateType = New AS3MemberDateType() With {.DateType = MemberDataTypes.Number}
        Public Shared ARRAY As AS3MemberDateType = New AS3MemberDateType() With {.DateType = MemberDataTypes.Array}
        Public Shared _NAMESPACE As AS3MemberDateType = New AS3MemberDateType() With {.DateType = MemberDataTypes._namespace}
        Public Shared _BOOLEAN As AS3MemberDateType = New AS3MemberDateType() With {.DateType = MemberDataTypes._Boolean}
        Public Shared _OBJECT As AS3MemberDateType = New AS3MemberDateType() With {.DateType = MemberDataTypes._Object}
        Public Shared _FUNCTION As AS3MemberDateType = New AS3MemberDateType() With {.DateType = MemberDataTypes._Function}
        Public Shared CLASSDEF As AS3MemberDateType = New AS3MemberDateType() With {.DateType = MemberDataTypes.ClassDef}



        Public DateType As MemberDataTypes = MemberDataTypes.Any

        Public TypeValue As IMemberDataType

    End Class




End Namespace

