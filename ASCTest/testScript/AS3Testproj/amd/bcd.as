package 
{
	import autogencodelib.RefTool;
	import autogencodelib.ReflectUtil;
	import flash.utils.getDefinitionByName;
	import flash.utils.getQualifiedClassName;
	import system.Int64;
	import system._Object_;
	
	[Doc]
	public class bcd 
	{
		
		public function bcd() 
		{
			var data:AnyClass = new AnyClass("System.DateTime",2019,2,22);
			
			var obj:AnyClass = new AnyClass("AutoGenCodeLib.RefB","abcd",data.instance);
			
			trace(obj["LLLL"](null,33)["ToString"]());
			
			
			trace( AnyClass.invoke("AutoGenCodeLib.RefB", "MMM", 3));
			
		}
		
	}

}


