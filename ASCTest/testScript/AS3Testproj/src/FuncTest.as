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
	import system.collections.ICollection;
	import system.collections.IEnumerable;
	import system.collections.IList;
	import system.collections._IEnumerable_;
	import system.collections._IEnumerator_;
	[Doc]
	/**
	 * ...
	 * @author ...
	 */
	public final class FuncTest
	{
		
		
		
		public function FuncTest() 
		{
			//var k:IEnumerable;
			//var o:Object;
			//
			//o = k;
			//trace(o);
			
			
			
			
			
			var arr = _Array_.createInstance(_Object_, 5);
			
			arr[3] = function(j){ trace(j, j)};
			
			//arr[3] =[1,2,3];
			//arr[3][0] =  int(9999);
			//trace(arr[3]--);
			
			trace(arr[3](66));
			
		}
		
		
	}

}