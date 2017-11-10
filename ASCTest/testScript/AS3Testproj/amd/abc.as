package
{
	[Doc]
	public class abc
	{
		public function abc()
		{
			var t:Number = (new Date()).getTime();
			

			
			
			for (var i:int = 0;  i<2000000 ; i++) 
			{
				blank();
			}
			trace("action script:", (new Date()).getTime() - t );

			trace(b);
						
		}
		var b:int;
		function blank():void{++b; }
	}
}



