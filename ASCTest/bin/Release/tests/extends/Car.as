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