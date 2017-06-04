package {
	[Doc]
	public class FuncTest{
		public function FuncTest() {
			/*
			yield 是从.net2.0语法中移植过来，可自动生成一个可枚举对象的语法。每次yield return都可以返回一个值
			yield break可停止枚举。
			*/
			var yieldtest=function(a):*
			{
				for (var i:int = 0; i < 100; i++ )
				{
					if(i>=a)
					{
						trace("exit yield");
						//**输出调用堆栈。
						trace(new Error().getStackTrace());
						yield break;
					}
					trace("current output:",i);
					yield return i;
				}
			}
			
			for (var k in yieldtest(4))
			{
				
				
				trace("receive:",k);
				//**输出调用堆栈。
				trace(new Error().getStackTrace());
			}
		}
	}
}
