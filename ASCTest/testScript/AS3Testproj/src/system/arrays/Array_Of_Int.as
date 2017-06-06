package system.arrays 
{
	import system._Object_;
	
	[link_system]
	public final class Array_Of_Int extends _Array_ 
	{
		[creator];
		[native, _system_Array_creator_];
		private static function _creator(type:Class):*;
		
		[native, _system_ArrayOfInt_ctor_]
		public function Array_Of_Int(length:int);
		
		[native, _system_ArrayOfInt_getValue_]
		public function getValue(index:int):int;
		
		[native, _system_ArrayOfInt_setValue_]
		public function setValue(value:int,index:int):void;
		
	}

}