package system 
{
	[no_constructor]
	[link_system]
	public class MulticastDelegate extends Delegate
	{
		[creator];
		[native, _system_MulticastDelegate_creator_]
		private static function _creator(type:Class):*;
		
		[native, $$_noctorclass]
		public function MulticastDelegate();
		
		
		[native,system_ICloneable_clone]
		private function clone():_Object_;
	}

}