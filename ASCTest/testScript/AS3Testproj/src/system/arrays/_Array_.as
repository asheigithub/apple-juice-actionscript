package system.arrays 
{
	import system._Object_;
	
	[no_constructor]
	[link_system]
	public class _Array_ extends _Object_ 
	{
		
		[creator];
		[native, _system_Array_creator_];
		private static function _creator(type:Class):*;
		
		
		[native, _system_ArrayOfObject_ctor_]
		public function _Array_(length:int);
		
		[native, _system_Array_length_]
		public function get length():int;
		
		[native, _system_Array_rank_]
		public function get rank():int;
		
	}

}