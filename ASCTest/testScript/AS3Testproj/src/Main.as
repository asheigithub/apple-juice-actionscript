package 
{
	import autogencodelib.Testobj;
	import com.adobe.crypto.SHA1;
	import flash.display.Sprite;
	import flash.utils.getDefinitionByName;
	import system.EnvironmentVariableTarget;
	import system._Array_;
	[Doc]
	/**
	 * ...
	 * @author 
	 */
	public class Main  extends Sprite
	{
		
		public function Main() 
		{
			//var v:Vector.<String> = new Vector.<String>();
			//
			//
			//var millionAs:String = new String("");
			//for ( var i:int = 0; i < 10000; i++ ) {
				//
				//v.push("a");
				//
				////millionAs += "a";
			//}
			//
			//millionAs = v.join();
			//
			////trace(v.length,v[0],v[1],v[2],v[10000-1].length);
			//
			//trace(SHA1.hash(millionAs));
			//
			
			//var c:Class = getDefinitionByName("Main::et");
			
			var t:* = new et();
			
			t.ATTT(EnvironmentVariableTarget.Process);
			
			var arr:_Array_ = Testobj.make(5);
			for each (var m:Testobj in arr) 
			{
				trace(m.x);
				
				m.EventTest_addEventListener( function(sender, args)
				{
					trace( sender, args );
					
				}  );
				
				m.onEvent();
			}
			
			trace( t.geteList(null).count );
			
			t[99] += "bbbc";
			
			trace( t[99] );
			
			
			
			
			
		}
		
	}

}
import autogencodelib.Testobj;

class et extends Testobj
{
	public function et()
	{
		super(5);
	}
	
	override public function dialog():void 
	{
		super.dialog();
		
		trace("override");
		
	}
}
