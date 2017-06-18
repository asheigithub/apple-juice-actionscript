package system 
{
	[struct]
	[link_system]
	public final class UInt64 extends _Object_ implements IComparable
	{
		[native, _system_UInt64_MaxValue_getter]
		public static const MaxValue:UInt64;
		[native, _system_UInt64_MinValue_getter]
		public static const MinValue:UInt64;
		
		[explicit_from];
		[native, _system_UInt64_explicit_from_];
		private static function _explicit_from_value(v:Number):*;
		
		[implicit_from];
		[native, _system_UInt64_implicit_from_];
		private static function _implicit_from_value(v:Number):*;
		
		[native,_system_UInt64_static_parse]
		public static function parse(v:String):UInt64;
		
		[creator];
		[native, _system_UInt64_creator__];
		private static function _creator(type:Class):*;
		
		[native,_system_UInt64_ctor]
		public function UInt64(v:Number=0);
		
		[native,_system_icomparable_compareto_]
		public function compareTo(obj:_Object_):int;
		
		[native,_system_UInt64_valueOf]
		public function valueOf():Number;
		
		[native,_system_UInt64_toString_]
		public function toString_(format:String):String;
		
		
	}

}