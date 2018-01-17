package 
{
	import autogencodelib.Testobj;
	import flash.display.Sprite;
	import system.Decimal;
	import system.collections.generic.List_Of_Int32;
	import system.collections.generic.List_Of_Int32_;
	
	
	[Doc]
	/**
	 * ...
	 * @author 
	 */
	public class AutoGenTest extends Sprite 
	{
		
		public function AutoGenTest() 
		{
			
			var g:Testobj = new Testobj();
			trace(g);
			
			g.FuncTest(genFunc());
			
			
			trace("hahaha");
			
		}
		
		
		private function genFunc()
		{
			var action = null;
			
			var func = function()
			{
				trace("func");
				
				if (action != null)
				{
					action();
					
				}
				return "bcd";
			}
			
			
			action = function()
			{
				trace("action");
			}
	
			return func;
			
		}
		
	}
	

}