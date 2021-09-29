package {
	import flash.utils.Dictionary;
	[Doc]
	public class FuncTest{
		public function FuncTest()
		{
		/*Dictionary 类用于创建属性的动态集合，该集合使用 strict equality (===) 运算符进行键比较。将对象用作键时，
		会使用对象的标识来查找对象，而不是使用在对象上调用 toString() 所返回的值。 */
		
			var dict = new Dictionary();
			 var obj = new Object();
			 var key:Object = new Object();
			 key.toString = function() { return "key" }
			 
			 dict[key] = "Letters";
			 obj["key"] = "Letters";
			 
			trace( dict[key] == "Letters"); // true
			trace( obj["key"] == "Letters"); // true
			trace( obj[key] == "Letters"); // true because key == "key" is true b/c key.toString == "key"
			trace( dict["key"] == "Letters"); // false because "key" === key is false
			
			trace(dict[key]);
			
			delete dict[key]; //removes the key
				
			trace(dict[key]);
			
			dict[1] = "4";
			
			trace(dict["1"]);
		}
	}
}
