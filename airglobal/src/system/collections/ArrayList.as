package system.collections 
{
	import system._Array_;
	import system._Object_;
	
	
	public class ArrayList extends _Object_ implements IList
	{
		
		public static function createInstance(c:ICollection):ArrayList{return null; }
		
		
		public static function createInstance_(capacity:int):ArrayList{return null; }
		
		public function ArrayList(){}
		
		
		
		public function getThisItem(index:int):*{return null; }
		
		
		public function setThisItem(value:*, index:int):void{}
		
		
		public function get isFixedSize():Boolean{ return false; }
		
		
		public function get isReadOnly():Boolean{ return false; }
		
		
		public function add(value:*):int{ return 0; }
		
		
		public function clear():void{}
		
		
		public function contains(value:*):Boolean{ return false; }
		
		
		public function indexOf(value:*):int{ return 0; }
		
		
		public function insert(index:int, value:*):void{}
		
		
		public function remove(value:*):void{}
		
		
		public function removeAt(index:int):void{}
		
		
		public function get count():int{ return 0; }
		
		
		public function copyTo_(array:_Array_):void{}
		
		
		public function copyTo(array:_Array_, index:int):void{}
		
		
		public function copyTo__(index:int,array:_Array_,arrayIndex:int,count:int):void{}
		
		
		
		public function getEnumerator():_IEnumerator_{return null; }
		
		
		public function get capcaity():int{ return 0; }
		
		
		public function addRange(c:ICollection):void{}
		
		
		public function reverse():void{}
		
		
		public function sort():void{}
		
		
	}

}