package 
{
	import flash.display.Sprite;
	
	/**
	 * ...
	 * @author 
	 */
	public class BuildInTest extends Sprite
	{
		
		public function BuildInTest() 
		{
			trace(isNaN(7));
			trace(isNaN(Number.NaN));
			
			trace(isFinite(7));
			trace(isFinite(Number.NEGATIVE_INFINITY));
			
			
			var f=(parseFloat);
			
			trace(f("889.45"));
			trace(parseInt("678.332"))
			
			trace(Object(false));
			
			var j = new Error().getStackTrace;
			try 
			{
				j(6);	
			}
			catch (e)
			{
				trace(e);
			}
			
			var kk = test;
			
			trace(kk);
			
		}
		
		private function test( k:int)
		{
			
			
		}
	}

}