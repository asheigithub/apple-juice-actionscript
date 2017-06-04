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