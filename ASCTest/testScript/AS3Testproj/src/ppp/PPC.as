package ppp
{
	
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
		
	}

}

trace("PPC outscope");

