package
{
	import flash.display.*;
	import ppp.PPC;
	import ppp.pp2.*;
	
	[Doc]
	/**
	 * ...
	 * @author 
	 */
	public class Main extends Sprite 
	{
		
		public function Main() 
		{	
			test();
		}
		
		public override function toString():String
		{
			return "tostring";
		}
		
		private function test()
		{
			try 
			{
				var ff:Function = function() {
					trace(this.Book);
					return this.Book;
				};
				trace( ff());
				
			}
			catch (e)
			{
				//trace(e);
			}
			finally
			{
				trace(this);
			}
			
			//trace( new o()  );
			
			var simpleObj:Simple = new Simple();
			simpleObj.func = ff;
			simpleObj.func();
			
			
			var obj:Object = new Object();
			obj.fg = ff;
			obj.fg();
			
			
			trace( simpleObj.func === obj.func );
			
			
			var p:PPC = new PPC(5);
			trace(p.pf);
			
			trace(p.getFunc(7)(5).test);
			
		}
		
	}
	
}
