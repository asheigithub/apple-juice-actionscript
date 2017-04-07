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
		var loader="loader";
		public function Main() 
		{
			
			var names:Array =  Array(false, "Jane", "David"); 
			trace(names);
			
			//var oddNumbers:Array = [1, 3, 5, 7, 9, 11]; 
			//var len:uint = oddNumbers.length; 
			//for (var i:uint = 0; i < len; i++) 
			//{ 
				//trace("oddNumbers[" + i.toString() + "] = " + oddNumbers[i].toString()); 
			//}
			//
			//oddNumbers[100] = 6;
			//trace(oddNumbers.length);
			//
			//
			//trace( oddNumbers.indexOf(7) );
			
			var f:Function = function(...args)
			{
				//trace("f argements length:" + args.length);
				//trace(args.length);
				
				
				var a:* = (args);
				//trace(a[0].length);
				
				//args.length = 0;
				
				//trace(a[0].length);
				
				trace(a == args);
			}
			
			
			f(1, 2, [3, 4, 5]);
			
			
			names.insertAt(-100, "gg");
			
			trace(names.join());
			
			var letters:Array = new Array("a");
			var count:uint = letters.push("b", "c");

			trace(letters); // a,b,c
			trace(count);   // 3
			
			trace( letters.removeAt( 5));
			trace(letters.length);
			
			//loader=new Loader();
			////Debug/GDebug.swf为GDebug.swf所在的目录，请替换成GDebug.swf的真正的路径
			//loader.load(new URLRequest("GDebug.swf?v="+Math.random()));
			//loader.contentLoaderInfo.addEventListener(Event.COMPLETE, complete);
			//loader.contentLoaderInfo.addEventListener(IOErrorEvent.IO_ERROR, ioError);

			//var l =new int(6)-6.56;
			////trace(Number.max(1,2,1,this));
			//
			//trace( Math.sin(l) * Math.PI );
			//
			//
			//var t = uint(l);
			//
			//trace(t.toFixed(3));
			
			//var p = CP(5);
			
			//trace(p);
			
			//trace(t + 5);
			
			
			//var t:Test = new Test(null);
			//
			//
			//trace(t.i++);
			//trace(++t.i);
			//
			//var t2:Test = new Test(null);
			//t2._i = 3;
			//trace( t2.testOut());
			//
			//
			//var f1 = t.testOut;			
			//var f2 = t2.testOut;
			//
			//trace(f1());
			//trace(f2());
			//
			//test();
			
			//test2();
			
			//f = null;
			
			//f.prototype = { a:"a" };
			
			
			f.p1 = "p1";
			
			trace(f.p1);
			
			
			var fo = new f();
			
			
			f.apply(5, [3,4,"tt"]);
			
		}
		
		
		[ff];
		[to]
		//public override function toString():String
		//{
			//return "Main tostring";
		//}
		
		public function valueOf()
		{
			return 3;
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