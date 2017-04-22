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
class A {
     var x = 1
	 public function A()
	 {
     A.prototype.px = 2
	 }
 }
 dynamic class B extends A {
     var y = 3
	 public function B()
	 {
     B.prototype.py = 4
	 }
 }
  
 var b = new B()
 trace(b.x) // 1 via class inheritance
  trace(b.px) // 2 via prototype inheritance from A.prototype
  trace(b.y) // 3
  trace(b.py) // 4 via prototype inheritance from B.prototype
  
 B.prototype.px = 5
  trace(b.px) // now 5 because B.prototype hides A.prototype
  
 b.px = 6
  trace(b.px) // now 6 because b hides B.prototype