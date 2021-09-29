package system.collections 
{
	import system._Array_;
	import system._Object_;
	
	
	public class Queue extends _Object_ implements ICollection
	{
		
		public static function createInstance(c:ICollection):Queue{ return null; }
		
		
		public static function createInstance_(capacity:int):Queue{ return null; }
		
		
		
		public function Queue(){}
		
		
		public function get count():int{ return 0; }
		
		
		public function copyTo(array:_Array_, index:int):void{}
		
		
		public function getEnumerator():_IEnumerator_{ return null; }
		
		
		public function clear():void{}
		
		
		public function contains(value:*):Boolean{ return false; }
		
		
		public function dequeue():*{ return null; }
		
		
		public function enqueue(obj:*):void{}
		
		
		public function peek():*{ return null; }
		
		
		public function toArray():_Array_{ return null; }
		
		
		public function trimToSize():void{}
		
		
	}

}