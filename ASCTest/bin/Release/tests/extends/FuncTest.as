
package {
	[Doc]
	public class FuncTest{};
}
/*
在下面的示例中，Car 类扩展 Vehicle 类，以继承其所有方法、属性和函数。
如果您的脚本对 Car 对象进行实例化，则来自 Car 类的方法和来自 Vehicle 类的方法都可以使用。 
下面的示例显示名为 Vehicle.as 的文件（它定义 Vehicle 类）的内容： 

package {
	class Vehicle { 
	    var numDoors:Number; 
	    var color:String; 
	    public function Vehicle(param_numDoors:Number = 2, param_color:String = null) { 
	        numDoors = param_numDoors; 
	        color = param_color; 
	    } 
	    public function start():void { 
	        trace("[Vehicle] start"); 
	    } 
	    public function stop():void { 
	        trace("[Vehicle] stop"); 
	    } 
	    public function reverse():void { 
	        trace("[Vehicle] reverse"); 
	    } 
	}	
}

下面的示例显示同一目录下名为 Car.as 的第二个 ActionScript 文件。
此类扩展 Vehicle 类，通过三种方式修改它。第一种是 Car 类添加变量 fullSizeSpare 以跟踪 car 对象是否具有标准尺寸的备用轮胎。第二种是它添加特定于汽车的新方法 activateCarAlarm()，
该方法用于激活汽车的防盗警报。
第三种是它覆盖 stop() 函数以添加 Car 类使用防抱死制动系统来停车的事实。 

package {

	public class Car extends Vehicle { 
	    var fullSizeSpare:Boolean; 
	    public function Car(param_numDoors:Number, param_color:String, param_fullSizeSpare:Boolean) { 
	        numDoors = param_numDoors; 
	        color = param_color; 
	        fullSizeSpare = param_fullSizeSpare; 
	    } 
	    public function activateCarAlarm():void { 
	        trace("[Car] activateCarAlarm"); 
	    } 
	    public override function stop():void { 
	        trace("[Car] stop with antilock brakes"); 
	    } 
	}
}

以下示例对 Car 对象进行实例化，调用在 Vehicle 类中定义的方法 (start())，
然后调用由 Car 类覆盖的方法 (stop())，
最后从 Car 类调用一个方法 (activateCarAlarm())： 
*/

var myNewCar:Car = new Car(2, "Red", true); 
myNewCar.start(); // [Vehicle] start 
myNewCar.stop(); // [Car] stop with anti-lock brakes 
myNewCar.activateCarAlarm(); // [Car] activateCarAlarm

/*
使用 super 语句也可以编写 Vehicle 类的子类，此子类可以使用该语句访问超类的构造函数。
下面的示例显示第三个 ActionScript 文件，该文件名为 Truck.as，
也在同一目录中。在构造函数和覆盖的 reverse() 方法中，Truck 类使用 super。 

package {
	class Truck extends Vehicle {
		var numWheels:Number;
		public function Truck(param_numDoors:Number, param_color:String, param_numWheels:Number) { 
			super(param_numDoors, param_color); 
			numWheels = param_numWheels; 
		} 
		public override function reverse():void { 
			beep();
			super.reverse();
		} 
		public function beep():void { 
			trace("[Truck] make beeping sound"); 
		} 
	}
}
以下示例对 Truck 对象进行实例化，调用由 Truck 类覆盖的方法 (reverse())，
然后调用在 Vehicle 类中定义的方法 (stop())： 

*/

var myTruck:Truck = new Truck(2, "White", 18); 
myTruck.reverse(); // [Truck] make beeping sound [Vehicle] reverse 
myTruck.stop(); // [Vehicle] stop



