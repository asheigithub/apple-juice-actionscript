package as3runtime
{
	import system._Object_;


	/**
	*  保存当调用带ref或者out的 .net函数时，ref或out返回的值。
	*/
	public class RefOutStore extends _Object_
	{
		
		public function RefOutStore();


		//*********公共方法*******
		
		/**
		 * 获取返回值
		 * @param	parameterName 要获取值的形式参数名
		 * @return  ref或者out的返回值
		 */
		public function getValue(parameterName:String):_Object_{ return null; }


	}
}
