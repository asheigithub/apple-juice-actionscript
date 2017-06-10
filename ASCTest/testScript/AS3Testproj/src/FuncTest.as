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
			var arr:IList = _Array_.createInstance(_IEnumerable_, 10);
			
			_Array_(arr).setValue( _Array_.createInstance(int,4)  , 0);
			
			_Array_(arr).getValue(0).setValue(9, 3);
			
			//trace(_Array_(arr).getValue(0).getValue(0));
			
			var b:_IEnumerator_ = _Array_(arr).getValue(0).getEnumerator();
			b.reset();
			while (b.moveNext()) 
			{
				trace(b.current);
			}
			
			trace( _Object_.equals( b, arr.getEnumerator() )  ); 
			
		}
		
		
		
		
	}

}