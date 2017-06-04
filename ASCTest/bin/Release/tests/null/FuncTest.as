package {
	[Doc]
	public class FuncTest{
		public function FuncTest() {
		//下例检查索引数组的前六个值，如果没有设置值（如果 value == null），则输出一条消息： 
			var testArray:Array = new Array();
			testArray[0] = "fee";
			testArray[1] = "fi";
			testArray[4] = "foo";

			for (var i = 0; i < 6; i++) {
				if (testArray[i] == null) {
					trace("testArray[" + i + "] == null");
				}
			}

			/* 
			testArray[2] == null
			testArray[3] == null
			testArray[5] == null
			*/
		}
	}
}