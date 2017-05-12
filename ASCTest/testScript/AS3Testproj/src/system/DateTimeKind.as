package system 
{
	/**
	 * ...
	 * @author 
	 */
	[no_constructor]
	[hosted]
	public final class DateTimeKind 
	{
		public static const Unspecified:DateTimeKind = create(0,"Unspecified",DateTimeKind);
		public static const Utc:DateTimeKind = create(1,"Utc",DateTimeKind);
		public static const Local:DateTimeKind = create(2,"Local",DateTimeKind);
		
		[explicit_from]
		private static function _explicit_from_value(value:int):Object
		{
			if (value == 1)
			{
				return Utc;
			}
			else if (value == 2)
			{
				return Local;
			}
			else
			{
				return Unspecified;
			}
		}
		
		[native,_enumitem_create_]
		private static function create(value:int, str:String, type:*):*;
		
		[native,_enumitem_tostring_]
		public function toString():String;
	
		[native,_enumitem_valueof_]
		public function valueOf():int;
		
	}

}