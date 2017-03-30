package
{
	import flash.display.Sprite;
	
	[Doc]
	/**
	 * ...
	 * @author 
	 */
	public class Main extends Sprite 
	{
		
		public function Main() 
		{
			var t:* = new Test();
			//var j;
			//var k=j=t.i = 5;
			trace(t.i);
			//trace(k);
			//
			t.i += "gg";
			
			//trace( t.b == true);
			//
			////
			////
			////
			////var f = t.testOut;
			////
			////trace( f() );
			trace(t.i++);

			trace(++t.i);
			
			
			
		}
		
		
		[to]
		public override function toString():String
		{
			return "Main tostring";
		}
		
		public function valueOf()
		{
			return 3;
		}
		
		private function test()
		{
			
		}
		
	}
	
}

