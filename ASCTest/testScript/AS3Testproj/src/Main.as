package
{
	import flash.accessibility.Accessibility;
	import flash.display.Loader;
	import flash.display.Sprite;
	import flash.events.Event;
	import flash.events.IOErrorEvent;
	import flash.net.URLRequest;
	import flash.sampler.NewObjectSample;
	import ppp.pp2.CP;
	
	[Doc]
	/**
	 * ...
	 * @author 
	 */
	public class Main extends Sprite 
	{
		var loader = "loader";
		
		public static const TP:String = "TTP";
		
		public static var JJ:Function = function() { trace(this,"JJ"); };
		
		
		
		private var JF;
		
		public function Main() 
		{
			var ot:* = Main;
			
			var p = ot(this);
			
			trace(p);
			
			ot.prototype.a="a";
			
			trace(p["a"]);
			
			trace(ot.prototype.toString);
		}
		
		
		private function testVector()
		{
			
		}


		
		[ff];
		[to]
		//public override function toString():String
		//{
			//return "Main tostring";
		//}
		
		private var _value:int = 2;
		public function valueOf()
		{
			return this._value;
		}
		
		private function test()
		{
			
		}
		
		private function test2()
		{
			//trace("动态[]运算符");
			//
			//var obj:Object = new Object();
			//obj.prop1 = "foo";
			//obj.prop2 = "bar";
//
			//for (var i:int = 1;i < 3;i++) {
				//trace(obj["prop"+i]);
			//}
		}
		
	}
	
}
