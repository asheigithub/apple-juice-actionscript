using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    /// <summary>
    /// 操作符
    /// </summary>
    public enum OpCode : byte
    {
        /// <summary>
        /// 类型转换
        /// </summary>
        cast=0,
        /// <summary>
        /// 赋值 =
        /// </summary>
        assigning=1,

        /// <summary>
        /// 加法+
        /// </summary>
        add =2,
        add_number=3,
        add_string=4,

        /// <summary>
        /// 减法-
        /// </summary>
        sub =5,
        sub_number=6,
        /// <summary>
        /// 乘法 *
        /// </summary>
        multi=7,
        /// <summary>
        /// 除法 /
        /// </summary>
        div=8,
        /// <summary>
        /// 求模 %
        /// </summary>
        mod=9,

        /// <summary>
        /// 取反-
        /// </summary>
        neg =10,
        /// <summary>
        /// 大于 >
        /// 比较两个表达式，确定 expression1 是否大于 expression2；如果是，则结果为 true。
        /// 如果 expression1 小于等于 expression2，则结果为 false。 
        ///如果两个操作数的类型都为 String，则使用字母顺序比较操作数；所有大写字母都排在小写字母的前面。
        ///否则，首先将操作数转换为数字，然后进行比较。
        /// </summary>
        gt_num = 11,
        /// <summary>
        /// 动态 >
        /// </summary>
        gt_void =12,
        /// <summary>
        /// 小于 &lt;
        /// </summary>
        lt_num=13,
        /// <summary>
        /// 动态 &lt;
        /// </summary>
        lt_void =14,
        /// <summary>
        /// 大于等于 >=
        /// </summary>
        ge_num =15,
        /// <summary>
        /// 动态>=
        /// </summary>
        ge_void=16,

        /// <summary>
        /// 小于等于 &lt;=
        /// </summary>
        le_num =17,
        /// <summary>
        /// 动态 &lt;=
        /// </summary>
        le_void=18,
        /// <summary>
        /// 等于=
        /// </summary>
        equality =19,
        /// <summary>
        /// 数字之间比较
        /// </summary>
        equality_num_num=20,
        /// <summary>
        /// 字符串间比较
        /// </summary>
        equality_str_str=21,

        not_equality=22,
        not_equality_num_num=23,
        not_equality_str_str=24,

        /// <summary>
        /// === 测试两个表达式是否相等，但不执行自动数据转换。如果两个表达式（包括它们的数据类型）相等，则结果为 true。 
        /// </summary>
        strict_equality = 25,

        not_strict_equality=26,

        /// <summary>
        /// ! 逻辑取反
        /// </summary>
        logic_not=27,

        /// <summary>
        /// 位与 &
        /// </summary>
        bitAnd =28,
        /// <summary>
        /// 位或 |
        /// </summary>
        bitOr =29,

        /// <summary>
        /// 按位求补 ~
        /// </summary>
        bitNot =30,

        /// <summary>
        /// 位异或 ^
        /// </summary>
        bitXOR =31,
        /// <summary>
        /// 左移位 &lt;&lt;
        /// </summary>
        bitLeftShift=32,
        /// <summary>
        /// 右移位 >>
        /// </summary>
        bitRightShift=33,
        /// <summary>
        /// 结果为无符号32位整数右移位
        /// </summary>
        bitUnsignedRightShift=34,

        /// <summary>
        /// ++ 前缀
        /// </summary>
        increment =35,
        /// <summary>
        /// -- 前缀
        /// </summary>
        decrement =36,

        increment_int=37,
        increment_uint=38,
        increment_number=39,

        decrement_int=40,
        decrement_uint=41,
        decrement_number=42,

        /// <summary>
        /// ++ 后缀
        /// </summary>
        suffix_inc =43,

        suffix_inc_int=44,
        suffix_inc_uint=45,
        suffix_inc_number=46,

        /// <summary>
        /// -- 后缀
        /// </summary>
        suffix_dec =47,

        suffix_dec_int=48,
        suffix_dec_uint=49,
        suffix_dec_number=50,

        /// <summary>
        /// 条件跳转
        /// </summary>
        if_jmp=51,
        /// <summary>
        /// 无条件跳转
        /// </summary>
        jmp=52,

        /// <summary>
        /// 标签行
        /// </summary>
        flag=53,

        

        /// <summary>
        /// 抛出异常
        /// </summary>
        raise_error=54,
        /// <summary>
        /// 捕获异常
        /// </summary>
        catch_error=55,
        /// <summary>
        /// 进入try
        /// </summary>
        enter_try=56,
        /// <summary>
        /// 退出try
        /// </summary>
        quit_try=57,
        /// <summary>
        /// 进入catch
        /// </summary>
        enter_catch=58,
        /// <summary>
        /// 退出catch
        /// </summary>
        quit_catch=59,
        /// <summary>
        /// 进入finally
        /// </summary>
        enter_finally=60,
        /// <summary>
        /// 退出finally
        /// </summary>
        quit_finally=61,


        /// <summary>
        /// 内置trace语句
        /// </summary>
        native_trace = 62,

        /// <summary>
        /// function绑定上下文
        /// </summary>
        bind_scope =63,
        

        /// <summary>
        /// 创建函数参数上下文
        /// </summary>
        make_para_scope =64,
        /// <summary>
        /// 复制参数
        /// </summary>
        push_parameter =65,

        /// <summary>
        /// 调 function
        /// </summary>
        call_function = 66,
        /// <summary>
        /// return
        /// </summary>
        function_return=67,

        /// <summary>
        /// 创建类的实例
        /// </summary>
        new_instance=68,
        /// <summary>
        /// 从class创建类的实例
        /// </summary>
        new_instance_class =69,
        /// <summary>
        /// 初始化静态类对象
        /// </summary>
        init_staticclass=70,
        /// <summary>
        /// 准备构造函数参数
        /// </summary>
        prepare_constructor_argement=71,
        /// <summary>
        /// 准备从class中创建类的构造函数
        /// </summary>
        prepare_constructor_class_argement = 72,
        /// <summary>
        /// 加class构造函数参数
        /// </summary>
        push_parameter_class=73,
        /// <summary>
        /// 访问对象成员
        /// </summary>
        access_dot =74,

        access_dot_byname=75,

        /// <summary>
        /// []访问 需动态检查类型
        /// </summary>
        bracket_access =76,

        /// <summary>
        /// []按名字访问
        /// </summary>
        bracket_byname =77,

        /// <summary>
        /// 访问方法
        /// </summary>
        access_method =78,

        /// <summary>
        /// 如果不是method,则清空this指针
        /// </summary>
        clear_thispointer=79,

        /// <summary>
        /// 如果是一个属性，则尝试从属性中读取值。
        /// </summary>
        try_read_getter=80,

        /// <summary>
        /// 试图往属性中写回值
        /// </summary>
        try_write_setter=81,

        /// <summary>
        /// 删除属性
        /// </summary>
        delete_prop=82,

        /// <summary>
        /// 设置动态属性
        /// </summary>
        set_dynamic_prop=83,

        /// <summary>
        /// 从对象转换到基本类型
        /// </summary>
        cast_primitive=84,


        array_create=85,
        /// <summary>
        /// 往原始数组对象中追加元素
        /// </summary>
        array_push=86,

        ///// <summary>
        ///// 初始化Vector
        ///// </summary>
        //init_vector =85,

        
        /// <summary>
        /// 绑定Vector访问器
        /// </summary>
        vectorAccessor_bind=87,
        /// <summary>
        /// 先转换索引，再绑定Vector访问器
        /// </summary>
        vectorAccessor_convertidx=88,

        /// <summary>
        /// 往vector里追加元素
        /// </summary>
        vector_push=89,
        /// <summary>
        /// 把数组的内容拷贝到Vector
        /// </summary>
        vector_pusharray=90,
        /// <summary>
        /// 把Vector的内容拷贝到Vector
        /// </summary>
        vector_pushvector = 91,

        /// <summary>
        /// 从初始数据中构造Vector
        /// 要么是数组，要么是完全匹配的Vector,要么是T要创建的Vector的T的子类的Vector
        /// </summary>
        vector_initfrmdata=92,

        /// <summary>
        /// 链接到包外成员
        /// </summary>
        link_outpackagevairable=93,

        /// <summary>
        /// 标记准备调父类的构造函数
        /// </summary>
        flag_call_super_constructor =94,


        /// <summary>
        /// 获取forin的enumerator
        /// </summary>
        forin_get_enumerator=95,
        /// <summary>
        /// 获取foreach的enumerator
        /// </summary>
        foreach_get_enumerator=96,
        /// <summary>
        /// 调movenext
        /// </summary>
        enumerator_movenext=97,

        /// <summary>
        /// 调用读取当前值
        /// </summary>
        enumerator_current=98,

        /// <summary>
        /// 关闭枚举器
        /// </summary>
        enumerator_close = 99,

        /// <summary>
        /// is运算符
        /// </summary>
        logic_is=100,
        /// <summary>
        /// instanceof运算符
        /// </summary>
        logic_instanceof=101,

        /// <summary>
        /// as运算符
        /// </summary>
        convert_as=102,

        /// <summary>
        /// in运算符
        /// </summary>
        logic_in =103,

        /// <summary>
        /// typeof运算符
        /// </summary>
        unary_typeof=104,

        /// <summary>
        /// 乘法 2个数字
        /// </summary>
        multi_number =105,

        div_number=106,

        mod_number=107,

        unary_plus=108,

        function_create=109,

        yield_return=110,

        yield_break=111,

        yield_continuetoline=112,



		//重置StackSlot;
		reset_stackslot=113,
		
		call_function_notcheck = 114,
		call_function_notcheck_notreturnobject = 115,
		call_function_notcheck_notreturnobject_notnative = 116,
		call_function_notcheck_notreturnobject_notnative_method =117,

		cast_int_number =118,
		cast_number_int = 119,
		cast_uint_number = 120,
		cast_number_uint = 121,
		cast_int_uint =122,
		cast_uint_int=123,

		push_parameter_skipcheck_storetostack=124,
		push_parameter_skipcheck_storetoheap =125,
		push_parameter_skipcheck_testnative = 126,
		push_parameter_nativeconstpara =127,

		push_parameter_para=128,
		make_para_scope_method=129,
		make_para_scope_withsignature=130,
		make_para_scope_method_notnativeconstpara_allparaonstack =131,
		make_para_scope_withsignature_allparaonstack=132,

		make_para_scope_method_noparameters=133,
		make_para_scope_withsignature_noparameters=134,

		function_return_funvoid =135,
		function_return_nofunction=136,

		if_jmp_notry=137,
		jmp_notry=138,

		function_return_funvoid_notry=139,
		function_return_nofunction_notry=140,

		//sub_number_number=141,


		
		if_equality_num_num_jmp_notry = 142,
		if_equality_num_num_jmp_notry_noreference = 143,

		
		if_not_equality_num_num_jmp_notry = 144,
		if_not_equality_num_num_jmp_notry_noreference=145,

		/// <summary>
		/// 如果 &lt;= 则跳转
		/// </summary>
		if_le_num_jmp_notry = 146,
		if_le_num_jmp_notry_noreference = 147,

		/// <summary>
		/// 如果 &lt; 则跳转
		/// </summary>
		if_lt_num_jmp_notry = 148,
		if_lt_num_jmp_notry_noreference = 149,

		/// <summary>
		/// 如果 >= 则跳转
		/// </summary>
		if_ge_num_jmp_notry=150,
		if_ge_num_jmp_notry_noreference = 151,

		/// <summary>
		/// 如果 > 则跳转
		/// </summary>
		if_gt_num_jmp_notry = 152,
		if_gt_num_jmp_notry_noreference = 153,

		/// <summary>
		/// 当做完前缀后缀++ --后，如果arg1是个Register,则清理链接对象
		/// </summary>
		afterIncDes_clear_v1_link =154,

		/// <summary>
		/// 访问成员并且赋值给临时对象
		/// </summary>
		access_dot_memregister = 155,

		/// <summary>
		/// 两个MemNumber相减
		/// </summary>
		sub_number_memnumber_memnumber=156,
		/// <summary>
		/// 两个MemNumber相除
		/// </summary>
		div_number_memnumber_memnumber = 157,
		/// <summary>
		/// 两个MemNumber相乘
		/// </summary>
		multi_number_memnumber_memnumber = 158
	}
}
