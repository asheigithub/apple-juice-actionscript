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
			//
			//
			//for (var i:int = 0;  i<1664079 ; i++) 
			//{
				//blank(i);
			//}
			//trace("action script: it", (new Date()).getTime() - t );
//
			//trace(b);
						//
		//}
		//var b:int;
		//function blank(n:int):void
		//{
			//++b; 
			//if (n <= 2)
				//return;
			//n = n + 1;
		//}
	}
}


class fib
{

function fibonacci_recursion( n:int ):int
{
	
    if( n <= 2 )
        return 1;

    return fibonacci_recursion(n-1) + fibonacci_recursion(n-2);
}

function fib()
{

var t:Number = (new Date()).getTime();


var f:int = fibonacci_recursion(25);



trace("actionscript3:", ((new Date()).getTime() - t ));
trace(f);
}
}

var f = new fib();
var ff = fib(f);
trace(ff);
