package system 
{
	[struct]
	[link_system]
	public final class SByte extends _Object_ 
	{
		[native, _system_SByte_MaxValue_getter]
		public static const MaxValue:SByte;
		
		[native, _system_SByte_MinValue_getter]
		public static const MinValue:SByte;
		
		[explicit_from];
		[native, _system_SByte_explicit_from_];
		private static function _explicit_from_value(v:int):*;
		
		[implicit_from];
		[native, _system_SByte_implicit_from_];
		private static function _implicit_from_value(v:int):*;
		
		[creator];
		[native, _system_SByte_creator__];
		private static function _creator(type:Class):*;
		
		[native,_system_SByte_ctor]
		public function SByte(v:int = 0);
		
		[native,_system_SByte_static_parse]
		public static function parse(v:String):SByte;
		
		[native,_system_SByte_valueOf]
		public function valueOf():Number;
		
		[native,_system_SByte_toString_]
		public function toString_(format:String):String;
	}

}