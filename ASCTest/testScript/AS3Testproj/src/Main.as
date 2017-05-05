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
		
		public function Main() 
		{
			//var d = new Date("Tue Feb 1 00:00:00 GMT-0800 2005");
			
			//var millisecondsPerDay:int = 1000 * 60 * 60 * 24; 
			// gets a Date one day after the start date of 1/1/1970 
			//var startTime:Date = new Date(millisecondsPerDay);
			
			//var d:Date = new Date("Wed Apr 12 15:30:17 GMT-0700 2006");
			//
			//trace(d.toDateString());
			//trace(d.toLocaleDateString());
			
			
			//var dateParsed:String = "Sat Nov 30 1974";
//
			//var milliseconds:Number = Date.parse(dateParsed);
			//trace(milliseconds); // 154972800000

			
		}
		
		
		
		
		//public function println(str:String):void {
            //trace(str);
        //}

	}
	
}


function select( a:Vector.<int> )
{
	var i:int; var j:int; var x:int; var tmp:int;

	var len:int = a.length;
	
	var count:int = 0;
	
	for( i=0; i<len; i++)
	{
		var min:int = i;
		for( j=i+1; j<len; j++ )
		{
			if( a[j] < a[min] )
			min = j;
			
			count++;
		}

		tmp = a[min];
		a[min] = a[i];
		a[i] = tmp;
		
	}
	
	trace(count);
	
}



var a:Vector.<int> = new Vector.<int>();
var b:int = 0;



for(var i:int = 0; i < 1000; ++i )
{
	
	++b;
	a[i] =Math.random() * 10000;
}

var t = new Date().getTime();

select(a);

trace(new Date().getTime()-t);

//trace(b);
