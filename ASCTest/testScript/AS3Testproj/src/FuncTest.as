package 
{
	import system.DateTime;
	import system.DateTimeKind;
	[Doc]
	/**
	 * ...
	 * @author ...
	 */
	public final class FuncTest 
	{
		
		public function FuncTest() 
		{
			var d:DateTime = new DateTime();
			
			trace(d);
			
			d = DateTime.constructor_(2017, 6, 65);
			trace(d);
			
		}
		
	}

}