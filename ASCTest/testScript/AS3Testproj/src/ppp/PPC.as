package ppp
{
	import ppp.pp2.TC;
	
	/**
	 * ...
	 * @author 
	 */
	public class PPC 
	{
		
		public static const pp2:int = ppi +6;
		public static const ppi:int = 1000;
		
		public var pf:int = 1234;
		
		public function PPC(pf:int) 
		{
			
			this.pf = pf;
			
		}
		
		public function getFunc(v:int)
		{
			pf = v;
			
			return function(j:String) {
				//	this.lmn = 88;
				//trace(this.lmn);
				trace(this);
				return this;
			}
			
		}
		
		public function toString():*
		{
			var tc:TC = new TC(pf);
			
			
			return "5" + ppi + tc.f() ;
			//trace("invoke toString");
			//return null;// "J ";
		}
		
		public function valueOf()
		{
			trace("invoke valueof");
			//return pf;
			//trace("invoke valueof");
			//return pf;
			return new ippc();
		}
		
	}

}

class ippc
{
	public function valueOf()
	{
		return new lll();
	}
}

class lll
{
	public function valueOf()
	{
		return 4;
	}
	
}

trace("PPC outscope");

