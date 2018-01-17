package 
{
	import autogencodelib.Testobj;
	import flash.display.Sprite;
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
			trace(g.listtest);
			
			var i:List_Of_Int32 = new List_Of_Int32();
			
			
			try 
			{
				i[5] = 9;
			}
			finally
			{
				trace("hahaha");
				
			}
			
			
			
			trace(g.geteList(i));
			
			
		}
		
	}

}