package
{
	[Doc]
	public class abc
	{
		//public function abc()
		//{
			//var t:Number = (new Date()).getTime();
			//
			//
			//for (var i:int = 0;  i<500000 ; i++) 
			//{
				//
					//b += i;
				////blank(i);
			//}
			//trace("action script: it", (new Date()).getTime() - t );
//
			//trace(b);
						//
		//}
		//var b:int;
		//function blank(p:int):void
		//{
			//++b;
		//}
		
		
		
	}
}



class fib
{
	
function fibonacci_recursion( n:Number ):Number
{
	
    if( n <= 2 )
        return 1;
	
    return fibonacci_recursion(n - 1) +fibonacci_recursion(n-2);
}

function fib()
{

var t:Number = (new Date()).getTime();


var f:int = fibonacci_recursion(30);



trace("actionscript3:", ((new Date()).getTime() - t ));
trace(f);

}
}

var f = new fib();
var ff = fib(f);
trace(ff);

