package {
	[Doc]
	public class FuncTest{
		public function FuncTest() {}
	}
}
/*下面的示例说明如何使用 static 关键字创建一个计数器，
该计数器跟踪已创建类的实例的数量。
由于 numInstances 变量是静态的，
因此它只对整个类创建一次，而不是对每个单独实例都创建一次。*/
class Users { 
	private static var numInstances:Number = 0; 
	function Users() { 
		numInstances++; 
	} 
	static function get instances():Number { 
		return numInstances; 
	} 
}
//在脚本中输入下面的代码： 
trace(Users.instances); 
var user1:Users = new Users(); 
trace(Users.instances); 
var user2:Users = new Users(); 
trace(Users.instances); 
//下例扩展了 Users 类，以说明尽管未继承静态变量和方法，但是可以在子类中引用它们。 
class PowerUsers extends Users{
    function PowerUsers() {
        
    }
}
var pu=new PowerUsers();
//trace(PowerUsers.instances); // error, cannot access static property using PowerUsers class
trace(Users.instances); 






