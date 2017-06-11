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
		
		var temp;
		
		public function FuncTest() 
		{
			//var k:IEnumerable;
			//var o:Object;
			//
			//o = k;
			//trace(o);
			
			var arr = _Array_.createInstance(Object,3);
			arr[0] = this;
			arr[1] = undefined;
			arr[2] = 1;
			
			temp = arr;

			var enum:_IEnumerator_ = _Array_(arr).getEnumerator();
			
			enum.reset();
			while (enum.moveNext()) 
			{
				trace(enum.current);
			}
			
			
		}
		
		public function makfunc()
		{
			var temp = this.temp;
			function ff(f)
			{
				
				temp[1] = f;
				trace(temp[0], temp[1],temp[2] );
			}
			
			return ff;
		}
	}

}
