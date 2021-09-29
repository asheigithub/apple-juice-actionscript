package system 
{
	import system._Object_;
	import system.collections.ICollection;
	import system.collections.IList;
	import system.collections._IEnumerator_;
	
	[no_constructor]
	[link_system]
	/**
	 * 托管数组的基类。可创建和操作托管数组
	 */
	public final class _Array_ extends _Object_ //implements IList, ICollection
	{

		public static function createInstance(elementType:Class, length:int):_Array_{ return null; }
		
		public static function createInstance_(elementType:Class, length1:int, length2:int):_Array_{return null; }
		
		public static function createInstance__(elementType:Class, lengths:_Array_):_Array_{return null; }
		
		public function _Array_(length:int){}
		
		public function get length():int{ return 0; }
		
		
		public function get rank():int{ return 0; }
		
		public function getLength(dimension:int):int{ return 0; }
		
		public function getLowerBound(dimension:int):int{ return 0; }
		
		public function getUpperBound(dimension:int):int{ return 0; }
		
		public function getValue(index:int):*{ return 0; }

		public function setValue(value:*, index:int):void{ }
		
		
		public function getValue_(index1:int,index2:int):*{ return 0; }
		

		public function setValue_(value:*,index1:int,index2:int):void{ }
		
	
		public function getValue__(indices:_Array_):*{ return 0; }
		
		
		public function setValue__(value:*,indices:_Array_):void{ }
		

		
		public function getEnumerator():_IEnumerator_{ return null; }
		
		
		
		public function copyTo(array:_Array_, index:int):void{ }
		
		
		public function get isFixedSize():Boolean{ return false; }
		
		
		public function get isReadOnly():Boolean{ return false; }
		
		
		private function add(value:*):int{ return 0; }
		
		
		private function clear():void{}
		
		
		private function contains(value:*):Boolean{ return false; }
		
		
		private function indexOf(value:*):int{ return 0; }
		
		
		private function insert(index:int, value:*):void{ }
		
		
		private function remove(value:*):void{ }
		
		
		private function removeAt(index:int):void{}
		
		
		
		private function get syncRoot():_Object_{ return null; }
		
		
		
		private function get isSynchronized():Boolean{ return false; }
	}

}