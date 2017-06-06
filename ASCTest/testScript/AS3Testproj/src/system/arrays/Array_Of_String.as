package system.arrays 
{
	import system._Object_;
	
	[link_system]
	public final class Array_Of_String extends _Array_ 
	{
		
		[creator];
		[native, _system_Array_creator_];
		private static function _creator(type:Class):*;
		
		
		[native, _system_ArrayOfString_ctor_]
		public function Array_Of_String(length:int);
		
		[native, _system_ArrayOfString_getValue_]
		public function getValue(index:int):String;
		
		[native, _system_ArrayOfString_setValue_]
		public function setValue(value:String,index:int):void;
	}

}