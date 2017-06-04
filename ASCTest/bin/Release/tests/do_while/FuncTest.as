package {
	[Doc]
	public class FuncTest{
		public function FuncTest()
		{
		//以下示例使用 do..while 循环来计算条件是否为 true，并跟踪 myVar，直到 myVar 大于等于 5 为止。myVar 大于等于 5 时，循环结束。 
			var myVar:Number = 0; 
			do { 
				trace(myVar); 
				myVar++; 
			} 
			while (myVar < 5); 
			/*
			0 
			1 
			2 
			3 
			4
			*/
		}
	};
}
