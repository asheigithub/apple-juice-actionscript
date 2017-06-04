package {
	[Doc]
	public class FuncTest{
		public function FuncTest() {
			var myExt:Extender = new Extender(); 
		}
	}
}
/*
如果使用与同类或超类中的静态属性相同的名称定义实例属性，
则实例属性在作用域链中的优先级比较高。
因此认为实例属性遮蔽了静态属性，这意味着会使用实例属性的值，而不使用静态属性的值。
例如，以下代码显示如果 Extender 类定义名为 test 的实例变量，
trace() 语句将使用实例变量的值，而不使用静态变量的值：
*/
class Base { 
    public static var test:String = "static"; 
} 
 
class Extender extends Base 
{ 
	public var test:String = "instance";
    public function Extender() 
    { 
        trace(test); // output: static 
    } 
     
}






