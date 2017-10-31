package system
{
	[no_constructor]
	[link_system]
	public final class StringSplitOptions
	{
		[creator];
		[native,system_StringSplitOptions_creator];
		private static function _creator(type:Class):*;

		[native,system_StringSplitOptions_ctor];
		public function StringSplitOptions();

		/**
		 *None = 0
		 */
		[native,system_StringSplitOptions_None_getter];
		public static const None:StringSplitOptions;

		/**
		 *RemoveEmptyEntries = 1
		 */
		[native,system_StringSplitOptions_RemoveEmptyEntries_getter];
		public static const RemoveEmptyEntries:StringSplitOptions;

		[native, _system_Enum_valueOf];
		public function valueOf():int;

		[operator,"|"];
		[native,system_StringSplitOptions_operator_bitOr];
		private static function bitOr(t1:StringSplitOptions,t2:StringSplitOptions):StringSplitOptions;
	}
}
