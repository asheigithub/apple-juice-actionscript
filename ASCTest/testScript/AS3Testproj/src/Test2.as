package 
{
	import flash.display.Sprite;
	
	/**
	 * ...
	 * @author 
	 */
	public class Test2 extends Sprite
	{
		
		public function Test2() 
		{
			trace(Test.KV);
			
			var t = new Test(null);
			t.m ++;
			t.getfunc()();
			
			
			
			function ttttabc(a:int=6,b:int=7)
			{
				trace("a", a, "b", b);
				
			}
			ttttabc();
			
		}
		
		
		
	}

}