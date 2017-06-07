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
	import system.arrays.Array_Of_Array;
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
			
			var u:UInt64 = new UInt64( 6);
			
			var bb:UInt64 = u;
			
			bb = 9999;
			
			trace(bb,u);
			trace(UInt64(100));
			trace(UInt64.parse("5678").valueOf() - UInt64.parse("5000").valueOf());
			
			
			var values = [ SByte(-124), SByte(0), SByte(118) ];

			var specifiers = [ "G", "C", "D3", "E2", "e3", "F", 
                              "N", "P", "X", "00.0", "#.0", 
                              "000;(0);**Zero**"];
			
			for each (var value in values) 
			{
				for each (var s in specifiers) 
				{
					var ss = [s, value.toString_(s)].join(":");
					trace( ss);
				}
			}	
			
			
			return;
			
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
			trace(sr.getLength(0),sr.getLowerBound(0),sr.getUpperBound(0));
			
			
			
			
			
		}
		
		private function readformats(d:DateTime)
		{
			return d.getDateTimeFormats();
		}
		
	}

}