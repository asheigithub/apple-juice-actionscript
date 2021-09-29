package 
{
	import system.Byte;
	import system.Char;
	import system.DateTime;
	import system.DateTimeKind;
	import system.DayOfWeek;
	import system.Int64;
	import system.SByte;
	import system.TimeSpan;
	import system.UInt64;
	import system._Object_;
	import system._Array_;
	[Doc]
	/**
	 * ...
	 * @author ...
	 */
	public final class FuncTest 
	{
		
		
		public function FuncTest() 
		{
			
			var lengths = _Array_.createInstance(int, 2);
			lengths.setValue(2, 0);
			lengths.setValue(3, 1);
			
			var arr = _Array_.createInstance_(int, 4,5);
			
			
			trace(arr.rank);
			
			for (var i:int = 0; i < arr.getLength(0); i++) 
			{
				for (var j:int = 0; j < arr.getLength(1); j++) 
				{
					arr.setValue_( (i + 1) * (j + 1), i, j);
					
					trace( (i + 1) + "," + (j + 1) + ":" + arr.getValue_(i, j) );
					
				}
			}
			
			
			
			
			var indices:_Array_ = _Array_.createInstance(int, 2);
			indices.setValue(2, 0);
			indices.setValue(3, 1);
			
			
			arr.setValue__(789, indices);
			
			
			trace(arr.getValue__(indices));
			
		}
		
		
		
		
	}

}
