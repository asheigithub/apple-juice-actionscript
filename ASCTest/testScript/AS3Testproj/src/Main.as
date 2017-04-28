package
{
	import adobe.utils.CustomActions;
	import flash.accessibility.Accessibility;
	import flash.display.Loader;
	import flash.display.Sprite;
	import flash.events.Event;
	import flash.events.IOErrorEvent;
	import flash.net.URLRequest;
	import flash.sampler.NewObjectSample;
	import flash.utils.Dictionary;
	import ppp.IPPP;
	import ppp.it;
	import ppp.pp2.CP;
	
	[Doc]
	/**
	 * ...
	 * @author 
	 */
	public class Main extends Sprite implements IPPP
	{
		var loader = "loader";
		
		public static const TP:String = "TTP";
		
		public static var JJ:Function = function() { trace(this,"JJ"); };
		
		/* INTERFACE ppp.IPPP */
		
		public function get p() 
		{
			return 6;
		}
		
		public function set p(v):void 
		{
			trace("invoke set ", v);
		}
		
		public function kkk(k:Vector.<IPPP>) 
		{
			
		}
		
		
		
		/* INTERFACE ppp.IPPP */
		
		
		
		
		
		public function jk() 
		{
			trace("impl JK");
		}
		
		
		
		
		private var JF;
		
		public function Main() 
		{
			var dict = new Dictionary();
			 var obj = new Object();
			 var key:Object = new Object();
			 key.toString = function() { return "key" }
			 //
			 dict[key] = "Letters";
			 obj["key"] = "Letters";
			 //
			trace( dict[key] == "Letters"); // true
			trace( obj["key"] == "Letters"); // true
			trace( obj[key] == "Letters"); // true because key == "key" is true b/c key.toString == "key"
			trace( dict["key"] == "Letters"); // false because "key" === key is false
			
			trace(dict[key]);
			
			//delete dict[key]; //removes the key
				
			trace(dict[key]);
			
			dict[this] = this;
			
			for (var m in dict)
			{
				trace("d",m);
				
			}
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
interface iip
{
	
}
