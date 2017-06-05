package system 
{
	[struct]
	[link_system]
	public final class DateTime extends _Object_ 
	{
		[creator];
		[native, _system_DateTime_creator__];
		private static function _creator(type:Class):*;
		
		
		[native,_system_DateTime_static_constructor_]
		public static function constructor_(year:int,month:int,day:int):DateTime;
		
		
		[native,_system_DateTime_ctor]
		public function DateTime();
		
		
		
	}

}