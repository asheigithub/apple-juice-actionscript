package {
	[Doc]
	public class FuncTest{
		public function FuncTest() ;
	}
}
/*
类是唯一可实现接口的 ActionScript 3.0 语言元素。在类声明中使用 implements 关键字可实现一个或多个接口。
下面的示例定义两个接口 IAlpha 和 IBeta 以及实现这两个接口的类 Alpha：
*/
interface IAlpha 
{ 
    function foo(str:String):String; 
} 
 
interface IBeta 
{ 
    function bar():void; 
} 
 
class Alpha implements IAlpha, IBeta 
{ 
    public function foo(param:String):String {  trace("foo", param); return null; } 
    public function bar():void { trace("bar");} 
}

var a=new Alpha();

var alpha:IAlpha = a;
var beta:IBeta = IBeta(alpha);

alpha.foo("call foo");
beta.bar();


