package {
	[Doc]
	public class FuncTest{
		public function FuncTest() {}
	}
}
//this
//对方法的包含对象的引用。执行脚本时，this 关键字引用包含该脚本的对象。在方法体的内部，this 关键字引用包含调用方法的类实例。
//若要调用在动态类中定义的函数，则必须使用 this 调用适当作用域内的函数：


// incorrect version of Simple.as
/*
dynamic class Simple {
    function callfunc() {
        func();
    }
}
*/
// correct version of Simple.as
dynamic class Simple {
    function callfunc() {
        this.func();
    }
}
//将下面的代码添加到您的脚本中： 
var simpleObj:Simple = new Simple();
simpleObj.func = function() {
	trace("hello there");
}
simpleObj.callfunc();
//当您在 callfunc() 方法中使用 this 时，以上代码生效。不过，如果您使用了不正确的 Simple.as 版本，将出现语法错误（在上例中已被注释掉）。 

