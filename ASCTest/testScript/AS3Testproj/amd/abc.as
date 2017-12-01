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
				////if(i * 2==0)
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
		//
		//public function get mm():int
		//{
			//return 1;
		//}
		//public function set mm(v:int):void
		//{
			//
		//}

		public function abc()
		{
			
			
			
			//local total = 0
			//local t = os.clock()
//
			//for i = 0, 1000000, 1 do
				//total = total + i - (i/2) * (i + 3) / (i + 5)
			//end
//
			//return os.clock() - t   
			
			//var t:Number = (new Date()).getTime();
			//var total:int = 0;
			//for (var i:int = 0; i < 1000000; i++) 
			//{
				//total = total + i - (i / 2) * (i + 3) / (i + 5);
				//
			//}
			//
			//trace("action script:", (new Date()).getTime() - t );
			//trace(total);
			//
			//total = total + total * 1 + total * 2;
			//trace(total);
			
			
			//var array:Vector.<int> = new Vector.<int>();
//
			//for (var i:int = 0; i < 1024; i++) 
			//{
				//array[i] = i;
			//}
//
			//var total:int = 0;
			//var t:Number = (new Date()).getTime();
			//for (var j:int = 0; j < 100000	; j++) 
			//{
				//for (var k:int = 0; k < 1024; k++) 
				//{
					//total = total + array[k];
				//}
			//}
			//trace("action script:", (new Date()).getTime() - t );
			//trace(total);
			
			//kkk(1);
			//kkk(2);
			//kkk(4);
			//kkk(5);
			
		}
		
		//private function kkk(i:int):void
		//{
			//
			//switch (i) 
			//{
				//case 1:
				//case 2:
					//kkk(i + 2);
				//case 3:
					//trace("case",i);
				//break;
			//case 4:
				//trace(i + 2);
			//default:
				//
				//trace("default", i);
				//i++;
				//trace("d2", i);
			//}
			//
		//}
		
	}
}



class fib
{
	
function fibonacci_recursion( n:int ):int
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

