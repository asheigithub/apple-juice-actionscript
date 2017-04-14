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
		
		
		
		public function Main() 
		{
			var v:Vector.<Vector.<int>> = Vector.<Vector.<int>>([
			
			Vector.<int>([1,2,3]),
			Vector.<int>([4,5,6])

			]);
			
			var nv= v[0].concat( new <int>[7,8] , new <int>[8]);
			trace(nv);
			
			trace(v[0]);
			
			//v[0] = v[1];
			
			//trace(v[0]);
			//v[1].length = 0;
			//trace(v[0]);
			
			//trace(v[0] == v[1]);
			
			trace( v.some( 
			function( item:Vector.<int>, idx:int, v:Vector.<Vector.<int>> ):Boolean
			{
				trace("item: ", item);
				trace("idx ", idx);
				trace("vec: ", v);
				
				return item[0] > 1;
				
			} 
			
			) )
			//v.fixed = true;
			//v.insertAt(4, null);
			
			//trace(v.pop());
			
			trace(v.push( new <int>[-1] , new <int>[-2,-3]));
			trace(v.unshift( new <int>[9] , new <int>[10,14]));
			
			trace(v);
			
			//var f = Main(this);
			var vv=(v.removeAt(-3));
			
			trace(vv.reverse());
			trace(vv);
			
			trace(vv.shift());
			
			trace(v);
			
			trace(v.splice(1,4,null,null,null));
			trace(v);
			
			//var v:Vector.<int> = new Vector.<int>(1);
			//trace( v.concat([3, 4, 5]));
			
			
			
			
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
			trace("动态对象加载");
			
			var myAssocArray:Object = { fname:"John", lname:"Public", ff:valueOf , 
			innerObj : { p1:this+"3", p2:"p2" } };
			
			
			 trace(myAssocArray.fname);     // John
			 trace(myAssocArray["lname"]);  // Public
			 myAssocArray.initial = "Q";
			 
			 trace( myAssocArray.innerObj.p1 );
			 
			 trace(myAssocArray.initial);   // Q
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
