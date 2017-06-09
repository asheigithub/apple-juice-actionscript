package system 
{
	import system._Object_;
	
	[no_constructor]
	[link_system]
	/**
	 * 托管数组的基类。可创建和操作托管数组
	 */
	public class _Array_ extends _Object_ 
	{
		
		[creator];
		[native, _system_Array_creator_]
		private static function _creator(type:Class):*;
		
		[native,_system_Array_static_createInstance]
		public static function createInstance(elementType:Class,length:int);
		
		[native,_system_Array_static_createInstance_]
		public static function createInstance_(elementType:Class,length1:int,length2:int);
		
		[native,_system_Array_static_createInstance__]
		public static function createInstance__(elementType:Class, lengths:_Array_);
		
		[native, _system_Array_ctor_]
		public function _Array_(length:int);
		
		[native, _system_Array_length_]
		public function get length():int;
		
		[native, _system_Array_rank_]
		public function get rank():int;
		
		[native, _system_Array_getLength_]
		public function getLength(dimension:int):int;
		
		[native, _system_Array_getLowerBound_]
		public function getLowerBound(dimension:int):int; 
		
		[native, _system_Array_getUpperBound_]
		public function getUpperBound(dimension:int):int; 
		
		
		[native, _system_Array_getValue_]
		public function getValue(index:int):*;
		
		[native, _system_Array_setValue_]
		public function setValue(value:*, index:int):void;
		
		
		[native, _system_Array_getValue__]
		public function getValue_(index1:int,index2:int):*;
		
		[native, _system_Array_setValue__]
		public function setValue_(value:*,index1:int,index2:int):void;
		
		[native, _system_Array_getValue___]
		public function getValue__(indices:_Array_):*;
		
		[native, _system_Array_setValue___]
		public function setValue__(value:*,indices:_Array_):void;
	}

}