package {
	[Doc]
	public class FuncTest1{
	/*
	下例说明如何使用嵌套循环的标签来跳出整个系列的循环。
	代码使用嵌套循环以生成从 0 到 99 的数字列表。
	在数字即将达到 80 前产生 break 语句。
	如果 break 语句未使用 outerLoop 标签，则代码将仅跳过下一循环的其余部分，
	并且代码将继续输出从 90 到 99 的数字。
	然而，因为使用了 outerLoop 标签，
	break 语句将跳过整个系列循环的其余部分，最后输出的数字是 79。 
	*/
		public function FuncTest1() {
			outerLoop: for (var i:int = 0; i < 10; i++) {
			for (var j:int = 0; j < 10; j++) {
				if ( (i == 8) && (j == 0)) {
					break outerLoop;
				}
				trace(10 * i + j);
			}
		}
		/*
		1
		2
		...
		79
		*/
		}
	}
}