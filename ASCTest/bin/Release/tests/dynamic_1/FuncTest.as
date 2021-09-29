package {
	[Doc]
	public class FuncTest{
		public function FuncTest()
		{
		}
	};
}
//下例创建了两个类，一个是命名为 Expando 的动态类，另一个是命名为 Sealed 的密封类，将在随后的示例中使用它们。 
dynamic class Expando  {
	}
	
class Sealed {
	}
//以下代码创建 Expando 类的实例，并说明可以向该实例添加属性。 
var myExpando:Expando = new Expando();
myExpando.prop1 = "new";
trace(myExpando.prop1); // new

//以下代码创建 Sealed 类的实例，并说明尝试添加属性将导致错误。 
var mySealed:* = new Sealed();
mySealed.prop1 = "newer"; // error

