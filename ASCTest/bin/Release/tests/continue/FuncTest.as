package {
	[Doc]
	public class FuncTest{
		public function FuncTest()
		{
		//在下面的 while 循环中，continue 语句用于遇到 3 的整数倍时跳过循环体的其余部分，并跳转到循环的顶端（在该处进行条件测试）： 
			var i:int = 0; 
			while (i < 10) { 
				if (i % 3 == 0) { 
					i++; 
					continue; 
				} 
				trace(i); 
				i++; 
			}
		}
	};
}