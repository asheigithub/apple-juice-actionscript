package {
	[Doc]
	public class FuncTest{
		public function FuncTest() {
			var myExt:Extender = new Extender(); 
		}
	}
}
/*
虽然并不继承静态属性，但是静态属性在定义它们的类或该类的任何子类的作用域链中。同样，可以认为静态属性在定义它们的类和任何子类的作用域 中。
这意味着在定义静态属性的类体及该类的任何子类中可直接访问静态属性。 
以说明 Base 类中定义的 test 静态变量在 Extender 类的作用域中。
换句话说， Extender 类可以访问 test 静态变量，而不必用定义 test 的类名作为变量的前缀。
*/
class Base { 
    public static var test:String = "static"; 
} 
 
class Extender extends Base 
{ 
    public function Extender() 
    { 
        trace(test); // output: static 
    } 
     
}






