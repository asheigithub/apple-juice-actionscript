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