package system 
{
	[struct]
	[link_system]
	/**
	 * ...
	 * @author 
	 */
	public final class Int64
	{
		[native, _system_Int64_MaxValue_getter];// , _system_Int64_MaxValue_setter]
		public static const MaxValue:Int64;
		[native, _system_Int64_MinValue_getter];// , _system_Int64_MaxValue_setter]
		public static const MinValue:Int64;
		
		[creator];
		[native, _system_Int64_creator__];
		private static function _creator(type:Class):Object;
		
		
		[explicit_from];
		[native, _system_Int64_explicit_from_];
		private static function _explicit_from_value(v:Number):Object;
		
		[native,_system_Int64_ctor]
		public function Int64(v:Number=0);
		
		
		[native,_system_Int64_valueOf]
		public function valueOf():Number;
		

		[native,_system_Int64_toString]
		public function toString():String;
		
	}

}