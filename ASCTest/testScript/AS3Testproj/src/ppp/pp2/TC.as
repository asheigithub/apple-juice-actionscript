package ppp.pp2 
{
	/**
	 * ...
	 * @author 
	 */
	public class TC 
	{
		internal var count:int;
		public function TC(count:int) 
		{
			var o:Object = new Object();
			
			o.ggg;
			
			delete o.hhh;
			
			trace(o.ggg);
			
			
			
			this.count = count;
			
		}
		
		public function f()
		{
			return _f(count);
		}
		
		private function _f(v:int)
		{
			if (v < 1)
			{
				return 0;
			}
			else if (v == 1)
			{
				return 1;
			}
			else
			{
				return _f(v-1) + _f(v - 2);
			}
		}
		
	}

}
