package 
{
	
	import as3runtime.RefOutStore;
	import autogencodelib.TestObjExtends;
	import autogencodelib.Testobj;
	import autogencodelib.Testobj_TESTHandler_Of_Int64;
	import flash.display.Sprite;
	import system.Byte;
	import system.Decimal;
	import system.EventArgs;
	import system.Int64;
	import system.UInt64;
	import system._Array_;
	import system._Object_;
	import system.collections.IEnumerable;
	import system.collections.IEnumerator;
	import system.collections._IEnumerator_;
	import system.io.MemoryStream;
	import system.security.cryptography.MD5;
	import system.security.cryptography.MD5CryptoServiceProvider;
	import system.text.Encoding;
	
	[Doc]
	/**
	 * ...
	 * @author 
	 */
	public class AutoGenTest extends Sprite
	{
		public var b;
		
		public function AutoGenTest() 
		{
			//var o = _Object_(5);			
			//trace(o);
			
			var arr:_Array_= _Array_.createInstance(_Object_, 5);
			
			 b = new CrossExt2(99);
			
			 
			 
			 
			 
			trace(b.b, b.i);
			 
			//b.testType(CrossExt2);
			
			arr[0] = b;
			
			trace(arr[0].i,arr[0].testType(Decimal) );
			
			var c:Testobj = Testobj.createTestObj(CrossTest);
			
			c.EventTest_addEventListener(function(a,b){
				trace("oneventhandler", a==c, b==EventArgs.Empty);
			});
			
			trace(CrossTest(c).onEvent());
			
			
			//b.doHandler("678", 1, 2);
			
			
			//var md5:MD5CryptoServiceProvider = new MD5CryptoServiceProvider();
			//
			//
			//
			//var bytes:_Array_ = Encoding.UTF8.getBytes___("md5testttt");
			//
			//var ms:MemoryStream = MemoryStream.constructor__(bytes); 
			//
			//var md5bytes= md5.computeHash(ms);
			//
			//for each (var c:Byte in md5bytes) 
			//{
				//trace( c.toString___("X2") );
			//}
			//
			//ms.close();
			//var t:Number = (new Date()).getTime();
//
			//var count:Number = 0;
//
			//var a:Number = 3;
//
			//var b:Number = 3434;
//
			//var c:Number = 232323;
//
			//for (var i:int = 0; i <= 100000;i++ )
			//{
				//count = a*4+c*444+b/3+i
			//}
//
			//trace("actionscript3:", ((new Date()).getTime() - t ));
			//
			//trace( count);
			
			
		}
		
		
		public static function test():Number
		{
			
			var count:Number = 0;

			var a:Number = 3;

			var b:Number = 3434;

			var c:Number = 232323;

			for (var i:int = 0; i <= 100000;i++ )
			{
				count = a*4+c*444+b/3+i
			}
			
			return count;
			
		}
		
		
		
		
		
	}
	
	

}

