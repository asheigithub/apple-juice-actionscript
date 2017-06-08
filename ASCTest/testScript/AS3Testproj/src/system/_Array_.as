package system 
{
	import system._Object_;
	
	[no_constructor]
	[link_system]
	public class _Array_ extends _Object_ 
	{
		
		[creator];
		[native, _system_Array_creator_]
		private static function _creator(type:Class):*;
		
		[native,_system_Array_static_createInstance]
		public static function createInstance(elemetType:Class,length:int);
		
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
		
	}

}