package system 
{
	
	[struct]
	[link_system]
	/**
	 * ...
	 * @author 
	 */
	public final class Int64 extends _Object_ implements IComparable
	{
		[native, _system_Int64_MaxValue_getter]// , _system_Int64_MaxValue_setter]
		public static const MaxValue:Int64;
		[native, _system_Int64_MinValue_getter];// , _system_Int64_MaxValue_setter]
		public static const MinValue:Int64;
		
		[creator];
		[native, _system_Int64_creator__];
		private static function _creator(type:Class):*;
		
		
		[explicit_from];
		[native, _system_Int64_explicit_from_];
		private static function _explicit_from_value(v:Number):*;
		
		
		[implicit_from];
		[native, _system_Int64_implicit_from_];
		private static function _implicit_from_value(v:Number):*;
		
		[native,_system_Int64_static_parse]
		public static function parse(v:String):Int64;
		
		
		[native,_system_Int64_ctor]
		public function Int64(v:Number=0);
		
		[native,_system_icomparable_compareto_]
		public function compareTo(obj:_Object_):int;
		
		[native,_system_Int64_valueOf]
		public function valueOf():Number;
		
		[native,_system_Int64_toString_]
		public function toString_(format:String):String;
		
		
	}

}