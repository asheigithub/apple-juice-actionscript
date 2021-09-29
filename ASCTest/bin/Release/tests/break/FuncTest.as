package {
	[Doc]
	public class FuncTest{
		public function FuncTest() {
			var i:int = 0;
			while (true) { 
				trace(i); 
				if (i >= 10) { 
					break; // this will terminate/exit the loop 
				} 
				i++; 
			} 
			/*
			0 
			1 
			2 
			3 
			4 
			5 
			6 
			7 
			8 
			9 
			10*/
		}
	}
}
