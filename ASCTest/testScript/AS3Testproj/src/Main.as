package
{
	import flash.display.Sprite;
	import flash.sampler.NewObjectSample;
	
	[Doc]
	/**
	 * ...
	 * @author 
	 */
	public class Main extends Sprite 
	{
		
		public function Main() 
		{
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
			test();
			//
			//test2();
			
			
			
		}
		
		
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
			innerObj : { p1:this-"3", p2:"p2" } };
			
			
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