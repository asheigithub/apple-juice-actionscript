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
			
		}
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
//var b:int = 0;
for(var i:int = 0; i < 5000; ++i )
{
	//++b;
	a[i] =Math.random() * 10000;
}



select(a);

//trace(b);
