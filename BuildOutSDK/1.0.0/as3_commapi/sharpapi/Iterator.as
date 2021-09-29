package
{
	import system._Object_;
	import system.collections._IEnumerator_;


		/**
		*  ASRuntime.nativefuncs.linksystem.Iterator
		*/
	[no_constructor]
	[link_system]
	public final class Iterator extends _Object_ implements _IEnumerator_
	{
		[creator];
		[native,asruntime_nativefuncs_linksystem_Iterator_creator];
		private static function _creator(type:Class):*;

		//*********构造函数*******
		[native,asruntime_nativefuncs_linksystem_Iterator_ctor];
		public function Iterator(v:*);


		//*********公共方法*******
		[native,system_collections_IEnumerator_get_Current]
		public final function get current():*;

		[native,system_collections_IEnumerator_moveNext];
		public final function moveNext():Boolean;

		[native,system_collections_IEnumerator_reset];
		public final function reset():void;


		//*****操作重载*****
		/**
		 * Explicit From ASBinCode.RunTimeValueBase 
		 */
		[native,static_asruntime_nativefuncs_linksystem_Iterator_op_Explicit];
		public static function op_Explicit(v:*):Iterator;

		[operator, "op_explicit"];
		[native,static_asruntime_nativefuncs_linksystem_Iterator_op_Explicit];
		private static function op_Explicit_(v:*):Iterator;
		
		

	}
}
