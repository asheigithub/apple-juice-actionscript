package 
{
	import ppp.pp2.CP;
	/**
	 * ...
	 * @author 
	 */
	public  class ExtCP extends CP
	{
		//public static var TP:Number = 999.9;
		
		var k:Number = 99;
		
		internal var i:Number=k ;
		
		public  function ExtCP() 
		{
			//super();
			
			//testf();
			trace("ExtCP GZ");
			
		}
		
		public  function CP() 
		{
			
			//testf();
			trace("CP");
		}
		
		override public function testf() 
		{
			trace("extcp");
			
			trace( super["testf"]());
			//super["testf"]();
			
		}
		
		
		
		override public function get gi():*
		{
			//return super.gi;
			
			trace(TP);
			trace("ov")
			return super["gi"];
			
			//
			//return "overrided kkk";
		}
		
		override public function set gi(value:*)
		{
			super.gi += value+8;
			
			trace("has set super prop");
		}
	}

}

var TP:int = 99;

class S2CP extends ExtCP
{
	override public function testf() 
	{
		trace( "FK");
		
		trace(super["testf"]());
		
	}
}

var s2c = new S2CP();
s2c.testf();
