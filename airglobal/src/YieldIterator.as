package
{
	import system.collections.IEnumerator
	
	public final class YieldIterator implements IEnumerator
	{
		
		public function get current():*{return null; }

		
        public function moveNext():Boolean{return false; }

        public function reset():void{}
		
	}
}