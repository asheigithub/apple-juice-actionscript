package 
{
	/**
	 * ...
	 * @author 
	 */
	public class Test 
	{
		
		public function Test()
		{
			trace(--i);
		}
		
		
		public function testOut():int
		{
			return i;
			
		}
		
		public function get b():Boolean
		{
			return true;
		}
		
		
		public  function get i()
		{
			return _i;
		}
		public  function set i(v)
		{
			trace(v);
			
			_i =  i + 5;
			trace("after set")
			
		}
		
		 var _i:int = 0;
	}

}
