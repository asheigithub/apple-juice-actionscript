package
{
	import flash.display.*;
	import ppp.PPC;
	import ppp.pp2.*;
	
	[Doc]
	/**
	 * ...
	 * @author 
	 */
	public class Main extends Sprite 
	{
		
		public function Main() 
		{
			test();
		}
		
		public override function toString():String
		{
			return "Main tostring";
		}
		
		public function valueOf()
		{
			return 3;
		}
		
		private function test()
		{
			
			//trace(this+1);
			
			//try 
			//{
				//var ff:Function = function() {
					//trace(this);
					//return this;
				//};
				//trace( ff());
				//
			//}
			////catch (e)
			////{
				////
				////trace(e);
			////}
			//finally
			//{
				//trace(this);
			//}
			
			
			
			//trace( new o()  );
			
			//var simpleObj:Simple = new Simple();
			//simpleObj.func = ff;
			//simpleObj.func();
			
			
			var obj:Object = new Object();
			//obj.a = "hahaha";
			//
			//obj.toString = function() 
			//{ 
				//return this.a;
			//}	;
			//
			//var obj2:Object = new Object();
			//
			//obj2.toString = function():*
			//{ 
				//trace("lznb")
				//return null;
			//}	;
			//
			////trace(obj);
			////trace(obj2);
			//
			obj.valueOf = function() { return 123; };
			
			trace(obj + 3)
			
			//trace( simpleObj.func === obj.func );
			//
			//
			//var p:PPC = new PPC(5);
			////trace(p.pf);
			//
			////trace(p.getFunc(7)(5).test);
			//
			//trace(p == null);
			//trace(p);
				
			var p1:* = new PPC(5);
			var p2:PPC = new PPC(6);
			
			var t = 5;
			
			//var p3 = p1 + 3;
			//trace(p1);
			var p3 =(p1--);
			
			trace(p3);
			trace(p1);
			
		}
		
	}
	
}
