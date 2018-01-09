package 
{
	import autogencodelib.Testobj;
	import flash.display.Sprite;
	import linkcodegen.Testobj;
	import system.Decimal;
	import system.Int64;
	import system.UInt64;
	
	import system.TimeSpan;
	
	[Doc]
	/**
	 * ...
	 * @author 
	 */
	public class AutoGenTest extends Sprite 
	{
		
		public function AutoGenTest() 
		{
			
			
			var t:Testobj = new Testobj(321);
			
			trace(Testobj.longvalue);
			Testobj.longvalue = 9876;
			
			Testobj.Ins = null; //Testobj.constructor__();
			
			t.x = 100;
			t.y = 3.456;
			
			trace( t.x,t.y);
			trace(Testobj.op_ExclusiveOr(t,Testobj.Ins));
			trace(Testobj.op_Addition_(t, "asdfasf"));
			
			
			trace(Testobj.op_Increment(t).x);
			trace(Testobj.op_Decrement(t).x);
			
			trace(Testobj.op_OnesComplement(t));
			
			trace(Testobj.op_BitwiseAnd(t,Testobj.op_Increment(t)).toUpperCase());
			
			var t2 = Testobj.kKK(t, 4);
			trace(t2, t===t2 );
			
			trace(Testobj.op_UnaryNegation(t2));
			
			trace(Testobj.op_LeftShift(t2, Testobj.op_Addition_(t, "asdfasf") ));
			trace(Testobj.op_RightShift(t2, Testobj.op_Addition_(t, "098") ));
			
			trace(Testobj.longvalue);
			
			
		}
		
	}

}