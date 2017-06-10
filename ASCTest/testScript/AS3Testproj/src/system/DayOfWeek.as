package system 
{
	/**
	 * ...
	 * @author ...
	 */
	[no_constructor]
	[link_system]
	public final class DayOfWeek 
	{
		
		[native, _system_DayOfWeek_Friday_getter]
		public static const Friday:DayOfWeek;
		
		[native, _system_DayOfWeek_Monday_getter]
		public static const Monday:DayOfWeek;
		
		[native, _system_DayOfWeek_Saturday_getter]
		public static const Saturday:DayOfWeek;
		
		[native, _system_DayOfWeek_Sunday_getter]
		public static const Sunday:DayOfWeek;
		
		[native, _system_DayOfWeek_Thursday_getter]
		public static const Thursday:DayOfWeek;
		
		[native, _system_DayOfWeek_Tuesday_getter]
		public static const Tuesday:DayOfWeek;
		
		[native, _system_DayOfWeek_Wednesday_getter]
		public static const Wednesday:DayOfWeek;
		
		
		
		[creator];
		[native, _system_DayOfWeek_creator__];
		private static function _creator(type:Class):*;
		
		[native,_system_DayOfWeek_ctor]
		public function DayOfWeek();
		
		[native, _system_Enum_valueOf];
		public function valueOf():int;
		
		
		[operator,"|"];
		[native, _system_DayOfWeek_operator_bitOr];
		private static function bitOr(t1:DayOfWeek,t2:DayOfWeek):DayOfWeek;
		
		
	}

}