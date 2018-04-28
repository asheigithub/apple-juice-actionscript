package
{
	import system._Object_;
	import system.collections._IEnumerator_;


		/**
		*  ASRuntime.nativefuncs.linksystem.Iterator
		*/
	
	public final class Iterator extends _Object_ implements _IEnumerator_
	{
		
		//*********构造函数*******
		public function Iterator(v:*){}


		//*********公共方法*******
		public final function get current():*{return null; }

		public final function moveNext():Boolean{ return false; }

		public final function reset():void{}


		//*****操作重载*****
		/**
		 * Explicit From ASBinCode.RunTimeValueBase 
		 */
		public static function op_Explicit(v:*):Iterator{return null; }

		
	}
}
