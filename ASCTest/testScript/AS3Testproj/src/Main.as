package
{
	import flash.display.
	*;
	
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


try 
{
	var l:int;
	throw "ggg";
	l = 1;
}
catch(e:int)
{
	trace(e);
	
	try 
	{
		e = null;
		trace(e);
		throw "catch 中抛出";
	}
	catch (e)
	{
		trace(e);
	}
	trace("incatch");
}
catch(e:String)
{
	
}
finally 
{
	//***222***
	trace(l);
}
