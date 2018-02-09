package 
{
	
	//import as3runtime.RefOutStore;
	import com.adobe.crypto.MD5;
	import com.adobe.crypto.SHA1;
	import com.adobe.crypto.SHA224;
	import com.adobe.crypto.SHA256;
	import com.adobe.images.BitmapData1;
	import com.adobe.images.JPGEncoder;
	import com.adobe.images.PNGEncoder;
	import com.adobe.serialization.json.JSON;
	import flash.display.Sprite;
	import flash.utils.ByteArray;
	import flash.utils.getQualifiedClassName;
	//import system.Byte;
	//import system.Decimal;
	//import system.EventArgs;
	//import system.Int64;
	//import system.UInt64;
	//import system._Array_;
	//import system._Object_;
	//import system.collections.IEnumerable;
	//import system.collections.IEnumerator;
	//import system.collections._IEnumerator_;
	//import system.io.MemoryStream;
	//import system.text.Encoding;
	
	[Doc]
	/**
	 * ...
	 * @author 
	 */
	public class AutoGenTest extends Sprite
	{
		//public var b;
		
		public function AutoGenTest() 
		{
			//var o = _Object_(5);			
			//trace(o);
			
			//var arr:_Array_= _Array_.createInstance(_Object_, 5);
			//
			 //b = new CrossExt2(99);
			//
			 //
			 //
			 //
			 //
			//trace(b.b, b.i);
			 //
			////b.testType(CrossExt2);
			//
			//arr[0] = b;
			//
			//trace(arr[0].i,arr[0].testType(Decimal) );
			//
			//var c:Testobj = Testobj.createTestObj(CrossTest);
			//
			//c.EventTest_addEventListener(function(a,b){
				//trace("oneventhandler", a==c, b==EventArgs.Empty);
			//});
			//
			//trace(CrossTest(c).onEvent());
			
			
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
			
			//trace(getQualifiedClassName(MD5));
			
			// com.adobe.serialization.json.
			
			//var b = [1,2,3];
			//
			//trace(com.adobe.serialization.json.JSON.encode(b));
			//
			//var str ="this \"is\" \t a \/ string \b \f \r \h \\ \n with \' ch\\u0061rs that should be { } http://escaped.com/";
			//
			//var e = com.adobe.serialization.json.JSON.encode(str);
			//trace(e);
			//
			//var de= com.adobe.serialization.json.JSON.decode(e);
			//trace( MD5.hash( String(de)));
			//
			//var o:* = com.adobe.serialization.json.JSON.decode( "{\"p1\":true,\"p2\":false}" ) as Object;
			//
			//trace(o);
			//trace(o.p1);
			//
			//trace(o.p2);
			//
			//var obj:Object = { foo: { foo2: { foo3: { foo4: "bar" } } } };
			//var s:String = com.adobe.serialization.json.JSON.encode( obj );
			//
			//trace(s);
			var v:Vector.<String> = new Vector.<String>();
			v[0] = null;
			trace(  v[0], SHA1.hash(typeof v[0]));
			
			
			//o = com.adobe.serialization.json.JSON.decode( str );
			//trace(o,typeof o);
			
			var e:JPGEncoder = new JPGEncoder();
			var bytes:ByteArray =e.encode( new BitmapData1(256, 256) );
			
			trace( SHA256.hashBytes( bytes));
			
			
			//trace(SHA256.hash(""));
			
			//var t:Number = (new Date()).getTime();
			//var millionAs:String = new String("");
			//for ( var i:int = 0; i < 100000; i++ ) {
				//millionAs += "a";
//
			//}
			//
			//trace("makestring:", ((new Date()).getTime() - t ));
			//t = (new Date()).getTime();
			//
			//trace(SHA224.hash(millionAs));
			//trace("gethash:", ((new Date()).getTime() - t ));
		}
		
		
		
		private var _a:int = 5;
		public function get abc():int 
		{
			return _a;
		}
		public function set abc(i:int):void
		{
			_a = i;
			
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

