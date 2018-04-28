package system.collections 
{
	import system.ICloneable;
	import system._Array_;
	import system._Object_;
	
	
	public class Hashtable extends _Object_	//implements IDictionary, ICloneable
	{
	
		public function Hashtable(){}
		
		
		
		public function get count():int{return 0; }
		
		
		public function copyTo(array:_Array_, index:int):void{}
		
		
		public function getEnumerator():IDictionaryEnumerator{ return null; }
		
		
		public function get isFixedSize():Boolean{ return false; }
		
		
		public function get isReadOnly():Boolean{ return false; }
		
		
		public function getThisItem(key:Object):*{ return null; }
		
		
		public function setThisItem(value:*, key:Object):void{}
		
		
		public function get keys():ICollection{ return null; }
		
		
		public function get values():ICollection{ return null; }
		
		
		public function add(key:Object, value:*):void{}
		
	
		public function clear():void{}
		
		
		public function contains(key:Object):Boolean{ return false; }
		
		
		public function remove(key:Object):void{}
		
		
		
		
		
	}

}