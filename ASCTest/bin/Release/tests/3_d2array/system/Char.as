package system 
{
	[struct]
	[link_system]
	public final class Char extends _Object_ 
	{
		[native, _system_Char_MaxValue_getter]
		public static const MaxValue:Char;
		
		[native, _system_Char_MinValue_getter]
		public static const MinValue:Char;
		
		[explicit_from];
		[native, _system_Char_explicit_from_];
		private static function _explicit_from_value(v:int):*;
		
		[creator];
		[native, _system_Char_creator__];
		private static function _creator(type:Class):*;
		
		[native,_system_Char_ctor]
		public function Char(v:int = 0);
		
		
		[native,_system_Char_valueOf]
		public function valueOf():Number;
		
	}

}