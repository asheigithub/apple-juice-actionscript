package 
{
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
			var dt:DateTimeKind = DateTimeKind.Local;
			
			trace( dt  | DateTimeKind.Utc );
			
		}
		
	}

}