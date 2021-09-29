package as3runtime
{
	import system._Object_;


		/**
		*  保存当调用带ref或者out的 .net函数时，ref或out返回的值。
		*/
	[link_system]
	public class RefOutStore extends _Object_
	{
		[creator];
		[native,as3runtime_RefOutStore_creator];
		private static function _creator(type:Class):*;

		//*********构造函数*******
		[native,as3runtime_RefOutStore_ctor];
		public function RefOutStore();


		//*********公共方法*******
		
		/**
		* as3runtime.RefOutStore.GetValue
		*parameters:
		*  parameterName : System.String
		*return:
		*   System.Object
		*/
		[native,as3runtime_RefOutStore_getValue];
		public function getValue(parameterName:String):_Object_;


	}
}
