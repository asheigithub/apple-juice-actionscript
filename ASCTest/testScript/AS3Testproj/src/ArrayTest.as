package 
{
	import flash.display.Sprite;
	
	/**
	 * ...
	 * @author 
	 */
	public class ArrayTest extends Sprite
	{
		
		public function ArrayTest() 
		{
			var arr:Array = new Array();
            arr[0] = "one";
            arr[1] = "two";
            arr[3] = "four";
            var isUndef:Boolean = arr.some(isUndefined);
            if (isUndef) {
                trace("array contains undefined values: " + arr);
            } else {
                trace("array contains no undefined values.");
            }

			
			var b:Array = [1,2,3];
			b.removeAt(0);
			
			trace(b);
			
			trace(b.reverse());
			
			var letters:Array = new Array("a", "b", "c");
			var firstLetter:String = letters.shift();
			trace(letters);     // b,c
			trace(firstLetter); // a
			
			var letters:Array = new Array("a", "b", "c", "d", "e", "f");
			var someLetters:Array = letters.slice(1,3);

			trace(letters);     // a,b,c,d,e,f
			trace(someLetters); // b,c
			
			var letters:Array = new Array("a", "b", "c");
			trace(letters); // a,b,c
			var letter:String = letters.pop();
			trace(letters); // a,b
			trace(letter);     // c
			
			
			var names:Array = new Array("Bill");
			names.push("Kyle");
			trace(names.length); // 2

			names.push("Jeff");
			trace(names.length); // 3

			names.shift();
			names.shift();
			trace(names.length); // 1

			names.length = 10;
			trace(names.length); // 10
			
			names.insertAt(5, "adsf");
			trace(names.length, " ", names);
			

		}
		private function isUndefined(element:*, index:int, arr:Array):Boolean {
            return (element == undefined);
        }


	}

}