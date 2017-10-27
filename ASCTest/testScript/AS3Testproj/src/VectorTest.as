package 
{
	import flash.display.Sprite;
	
	[Doc]
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
			
			
			
		}
		
		
	}

}