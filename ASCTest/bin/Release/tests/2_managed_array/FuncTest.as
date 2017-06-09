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
			
			var u:UInt64 = new UInt64( 6);
			
			var bb:UInt64 = u;
			
			bb = 9999;
			
			trace(bb,u);
			trace(UInt64(100));
			trace(UInt64.parse("5678").valueOf() - UInt64.parse("5000").valueOf());
			
			
			var values:_Array_ = _Array_.createInstance(SByte, 3);
			values.setValue(SByte( -124), 0);
			values.setValue(SByte( 0), 1);
			values.setValue(SByte( 118), 2);
			
			var specifiers:Array = [ "G", "C", "D3", "E2", "e3", "F", 
                              "N", "P", "X", "00.0", "#.0", 
                              "000;(0);**Zero**"];
			var sps = _Array_.createInstance(String, specifiers.length);
			for (var jj:int = 0; jj < specifiers.length	; jj++) 
			{
				sps.setValue( specifiers[jj], jj );
			}
			
			
			
			_Array_.prototype.dofunc = function(arr1,arr2)
			{
				for (var i:int = 0; i < values.length	; i++) 
				{
					var value = values.getValue(i);
					for (var k:int = 0; k < sps.length	; k++) 
					{
						var ss = [ sps.getValue(k) , value.toString_(sps.getValue(k))].join(":");
						trace(ss);
					}
				}
			};
			
			
			
			
			var c:testcls = new testcls();
			
			var ar = c.array; //getArray() //as _Array_;
			
			var eq = ar.equals(c.array);
			
			trace(eq);
			
			trace(ar , _Array_( ar.getValue(0)).getValue(0));
			
			ar.setValue(null, 0);
			
			ar.setValue(_Array_.createInstance(Number, 3), 1);
			
			ar.getValue(1).setValue(456, 1);
			
			trace( ar.equals(c.array));
			
			trace(ar,  ar.getValue(1) , c.array, c.array.getValue(1).getValue(1) );
			
			var k = new Array(ar);
			
			trace(k[0]===c.array);
			
			//return;
			
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
			
			
			
			
			
		}
		
		private function readformats(d:DateTime)
		{
			return d.getDateTimeFormats();
		}
		
	}

}
import system._Array_;

class testcls
{
	var ar:_Array_;
	
	public function testcls()
	{
		ar=_Array_.createInstance(_Array_, 100);
		
		ar.setValue( _Array_.createInstance(String,5),0 );
		
		ar.getValue(0).setValue("array string", 0);
		
	}
	
	public function get array():_Array_
	{
		return ar;
	}
	
	
}

var t:FuncTest = new FuncTest();
_Array_.prototype.dofunc.call();