
package {
	[Doc]
	public class FuncTest{};
}
//下面的示例创建一个名为 Plant 的类。Plant 构造函数采用两个参数。 
class Plant { 
	// Define property names and types 
	private var _leafType:String; 
	private var _bloomSeason:String; 
	// Following line is constructor 
	// because it has the same name as the class 
	public function Plant(param_leafType:String, param_bloomSeason:String) { 
		// Assign passed values to properties when new Plant object is created 
		_leafType = param_leafType; 
		_bloomSeason = param_bloomSeason; 
	} 
	// Create methods to return property values, because best practice 
	// recommends against directly referencing a property of a class 
	public function get leafType():String { 
		return _leafType; 
	} 
	public function get bloomSeason():String { 
		return _bloomSeason; 
	} 
  }

//在脚本中，使用 new 运算符来创建一个 Plant 对象。 
var pineTree:Plant = new Plant("Evergreen", "N/A"); 
// Confirm parameters were passed correctly 
trace(pineTree.leafType); 
trace(pineTree.bloomSeason); 
