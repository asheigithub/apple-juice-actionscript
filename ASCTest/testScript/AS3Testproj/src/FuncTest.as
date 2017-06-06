package 
{
	import system.DateTime;
	import system.DateTimeKind;
	import system.DayOfWeek;
	import system.Int64;
	import system.TimeSpan;
	import system.arrays.Array_Of_Int;
	import system.arrays.Array_Of_String;
	import system.arrays._Array_;
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
			
			var j = DateTimeKind.Local;
			
			
			d = DateTime.constructor_____(2017, 6, 5,16,35,4, j,j);
			trace(d.kind);
			
			
			var d2:DateTime = DateTime.constructor__(2017, 6, 6,6,7,8);
			trace(d2.month);
			
			trace(DateTime.utcNow.dayOfWeek);
			
			var d3 = DateTime.constructor_______(Int64(123456789),j);
			trace(d3);
			
			trace(DateTime.MinValue.add(TimeSpan.fromDays(10)).addDays(15).addHours(120).addYears(1000));
			
			DateTime.prototype.kk = function(d:DateTime){
				
				
					trace(this.year);
				
				
			};
			
			d3.kk.apply(d3);
			
			trace(DateTime.compare(d3, d2));
			
			trace(d2.compareTo_DateTime(d3));
			
			trace("subtract",d2.subtract_TimeSpan(TimeSpan.fromDays(365)));
			
			
			var July = 7;
			var Feb = 2;

			var daysInJuly = DateTime.daysInMonth(2001, July);
			trace(daysInJuly);
			
			var formats = readformats(d2);
			for (var i:int = 0; i < formats.length	; i++) 
			{
				trace(formats.getValue(i));
			}
			trace(d.getHashCode());
			
			var ar:Array_Of_Int = new Array_Of_Int(10);
			trace(ar, ar.length);
			
			var b = ar;
			//ar = null;
			trace(b.length);
			trace(b.rank);
			trace(ar == b);
			
			ar.setValue(5, 3);
			
			trace( b.getValue(3)  );
			
			trace( ar.getValue(3)  );
			
			var sr:Array_Of_String = new Array_Of_String(3);
			
			
			
			trace( sr.setValue("sss",0) ,sr.getValue(0) );
			
			trace( sr.equals(ar) );
		}
		
		private function readformats(d:DateTime)
		{
			return d.getDateTimeFormats();
		}
		
	}

}