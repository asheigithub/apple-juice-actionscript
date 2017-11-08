package 
{
	import flash.display.Sprite;
	
	
	/**
	 * ...
	 * @author 
	 */
	public class VectorTest extends Sprite
	{
		
		public function VectorTest() 
		{
			var names:Vector.<String> = new Vector.<String>(); 
			trace(names.length); // output: 0
			
			names.fixed = true;
			trace(names.fixed);
			
			try 
			{
				names.length = 5;
				trace(names.length)
			}
			catch (e)
			{
				
				trace(e);
				trace(names.length)
				names.fixed = false;
				names.length = 5;
				trace(names.length)
			}
			
			trace(names.toString());
			
			var v2:* = names.concat(Vector.<String>(["Bob", "Larry", "Sarah"]));
			trace(v2);
			
			
			names.fixed = true;
			try 
			{
				names.insertAt(1, "bb");
			}
			catch(e:Error)
			{
				trace(e.getStackTrace());
				names.fixed = false;
				names.insertAt(1, "bbb");
			}
			finally
			{
				trace(names);				
			}
			
			trace(names.join("--"));
			
			trace(names.pop());
			
			trace(names.push("b", "c", 4, 5)," ",names);
			
			names.removeAt(2);
			
			trace(names);
			
			trace(names.reverse());
			
			var iv:Vector.<Boolean> = new Vector.<Boolean>();
			
			trace(iv.shift());
			
			trace( names.slice(3,8));
			
			
			var names:Vector.<String> = Vector.<String>(["John Q. Smith", "Jane Doe", "Mike Jones"]); 
			function orderLastName(a, b):int 
			{ 
				var lastName:RegExp = /\b\S+$/; 
				var name1 = a.match(lastName); 
				var name2 = b.match(lastName); 
				if (name1 < name2) 
				{ 
					return -1; 
				} 
				else if (name1 > name2) 
				{ 
					return 1; 
				} 
				else 
				{ 
					//throw new Error("抛个异常玩");
					return 0; 
				} 
			} 
			trace(names); // output: John Q. Smith,Jane Doe,Mike Jones 
			names.sort(orderLastName); 
			trace(names); // output: Jane Doe,Mike Jones,John Q. Smith
			
			var str1:String = "abc12 def34";
			var pattern:RegExp = /([a-z]+)([0-9]+)/;
			//var str2:String = str1.replace(pattern, replFN);
			trace(str1.match(pattern));
		}
		
		
	}

}