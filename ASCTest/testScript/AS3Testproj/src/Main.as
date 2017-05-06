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
	public class Main extends Sprite 
	{
		
		public function get a()
		{
			return 1;
		}
		public function set a(v:int)
		{
			trace(v);
		}
		
		private function bb() { trace("bb") };
		
		public function Main() 
		{
			var obj:Object = {
				'a': 0,
				'b': 1,
				'c': 2
			}

			obj.setPropertyIsEnumerable('a', false)

			trace("object\n")
			for (var op:* in obj)
			{
				trace(op)
			}

			var dict:Dictionary = new Dictionary()
			dict['a'] = 0
			dict['b'] = 1
			dict['c'] = 2

			dict.setPropertyIsEnumerable('a', false)

			trace("dictionary\n")
			for (var dp:* in dict)
			{
				trace(dp)
			}

			

        }

	}
	
}
//var opp=0;
//
//function select( a:Vector.<int> )
//{
	//var i:int; var j:int; var x:int; var tmp:int;
//
	//var len:int = a.length;
	//
	//var count:int = 0;
	//
	//for( i=0; i<len; i++)
	//{
		//
		//var min:int = i;
		//var amin = a[min];
		//for( j=i+1; j<len; j++ )
		//{
			//if ( a[j] < amin )
			//{
				//min = j;
				//amin =a[min];
			//}
			//count++;
		//}
		//
		//tmp = amin;
		//a[min] = a[i];
		//a[i] = tmp;
		//
		//
	//}
	//
	//trace(count);
	//
//}
//
//
//
//var a:Vector.<int> = new Vector.<int>();
//var b:int = 0;
//
//
//
//for(var i:int = 0; i < 5000; ++i )
//{
	//
	//++b;
	//a.unshift(Math.random() * 10000);
//}
//
//var t = new Date().getTime();
//
//select(a);
//
//trace(new Date().getTime()-t);
//
//trace(b);