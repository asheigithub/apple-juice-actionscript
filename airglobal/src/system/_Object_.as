package system 
{
	
	/**
	 * .net对象的基类。因为直接用Object会导致IDE不正常，所以改名为_Object_
	 * @author 
	 */
	public class _Object_
	{

		public function _Object_(){}
		
		/**
		 * 将任意对象封装成.net Object对象。
		 * 同强制类型转换 _Object_(v:*)
		 */
		public static function boxAnyToObject(v:*):_Object_{ return null; }
		

		public static function equals(objA:_Object_, objB:_Object_):Boolean{ return false; }
		
		public static function referenceEquals(objA:_Object_, objB:_Object_):Boolean{ return false; }
		
		public function toString():String{ return null; }
		
		public function getHashCode():int{ return 1; }
		
		public function equals(obj:_Object_):Boolean{ return false; }
		
		
	}

}