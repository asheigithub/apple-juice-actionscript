package {
	[Doc]
	public class FuncTest{
		public function FuncTest() {}
	}
}
/*
Error 类包含有关脚本中出现的错误的信息。
可以使用 Error 构造函数来创建 Error 对象。
通常，从 try 代码块内部引发一个新 Error 对象，该对象随后被 catch 代码块捕获。 

以下示例使用 ErrorExample 类说明如何生成自定义错误。这是由以下步骤完成的： 
声明一个 Array 类型的局部变量 nullArray，但是请注意，从未创建新的 Array 对象。
构造函数尝试在错误处理代码段中使用 push() 方法将值加载到未初始化的数组中，该代码段使用 CustomError 类捕获自定义错误，该类扩展 Error。
引发 CustomError 时，构造函数将其捕获并输出一条错误消息（使用 trace() 语句）。 

*/
class CustomError extends Error 
{
    public function CustomError(message:String) 
    {
        super(message);
    }
}
var nullArray:Array;
try 
{
	nullArray.push("item");
}
catch(e:Error) 
{
	trace("catch error.");
	trace(e.getStackTrace());
	trace("throw CustomError");
	
	try
	{
		throw new CustomError("nullArray is null");
	}
	catch(e:CustomError)
	{
		trace("catch CustomError and re throw");
		throw e;
	}
	finally
	{
		trace("finally CustomError");
	}
}
finally
{
	trace("finally");
}

