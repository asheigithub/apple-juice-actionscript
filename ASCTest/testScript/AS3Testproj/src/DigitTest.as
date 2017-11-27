package 
{
	import flash.display.Sprite;
	[Doc]
	/**
	 * ...
	 * @author 
	 */
	public class DigitTest extends Sprite
	{
		
		public function DigitTest() 
		{
			var num:int = 315003;
			trace(num.toExponential(2)); 
			trace(num.toPrecision(3)); 
			trace(num.toFixed(2)); 

			trace(num.toString());
			
			var u:uint = 4567;
			trace(u.toExponential(10));
			trace(u.toFixed(4));
			trace(u.toString(3));
			
			
			var n:Number = 456.2323;
			trace(n.toPrecision(3));
			trace(n.toExponential(5));
			trace(n.toFixed(3));
			trace(n.toString(3),n.toString(3).length);
			
			
			
		}
		
	}

}


function fibonacci_recursion( n:int ):int
{
	
    if( n <= 2 )
        return 1;

    return fibonacci_recursion(n-1) + fibonacci_recursion(n-2);
}

var t:Number = (new Date()).getTime();


var f:int = fibonacci_recursion(30);



trace("actionscript3:", ((new Date()).getTime() - t ));
trace(f);
//var array:Vector.<int> = new Vector.<int>();
//
			//for (var i:int = 0; i < 1024; i++) 
			//{
				//array[i] = i;
			//}
//
//
			//var total:int = 0;
			//var t:Number = (new Date()).getTime();
			//for (var j:int = 0; j < 1000	; j++) 
			//{
				//for (var k:int = 0; k < 1024; k++) 
				//{
					//total = total + array[k];
				//}
			//}
			//trace("action script:", (new Date()).getTime() - t );
			//trace(total);
			
		