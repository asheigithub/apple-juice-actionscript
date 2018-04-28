package system.collections 
{
	import system.ICloneable;
	import system._Array_;
	import system._Object_;
	
	
	public class Stack extends _Object_ implements ICollection,ICloneable
	{
		
		public static function createInstance(c:ICollection):Stack{return null; }
		
		
		public static function createInstance_(initialCapacity:int):Stack{ return null; }
		
		
		
		public function Stack(){}
		
		
		public function get count():int{ return 0; }
		
		
		public function copyTo(array:_Array_, index:int):void{}
		
		
		public function getEnumerator():_IEnumerator_{ return null; }
		
		
		public function clear():void{}
		
		
		public function contains(value:*):Boolean{ return false; }
	
		
		public function peek():*{return null; }
		
		
		public function pop():*{return null; }
		
		
		public function push(obj:*):void{}
		
		
		public function toArray():_Array_{return null; }
		
		
		/* INTERFACE system.ICloneable */
		
		public function clone():_Object_ 
		{
			return null;
		}
		
	}

}