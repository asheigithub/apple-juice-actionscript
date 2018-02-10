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
	import flash.utils.Endian;
	import flash.utils.IDataInput;
	import flash.utils.getQualifiedClassName;
	import nochump.util.zip.CRC32;
	import nochump.util.zip.ZipEntry;
	import nochump.util.zip.ZipFile;
	import nochump.util.zip.ZipOutput;
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
			//var v:Vector.<String> = new Vector.<String>();
			//v[0] = null;
			//trace(  v[0], SHA1.hash(typeof v[0]));
			
			
			//o = com.adobe.serialization.json.JSON.decode( str );
			//trace(o,typeof o);
			
			//var e:JPGEncoder = new JPGEncoder();
			//var bytes:ByteArray =e.encode( new BitmapData1(256, 256) );
			//
			//trace( SHA1.hashBytes( bytes));
			
			
			//trace(MD5.hash("abcdefghijklmnopqrstuvwxyz"));
			
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
			
			
			
			saveZip(new ByteArray());
			
			
		}
		
		public static function saveZip(toread:ByteArray):void {
			 var fileName:String = "helloworld.txt";
			 var fileName2:String = "image.jpg";
			 var fileData:ByteArray = new ByteArray();
			 fileData.writeUTFBytes("Hello World!中文");
			 var zipOut:ZipOutput = new ZipOutput();
			// Add entry to zip
			 var ze:ZipEntry = new ZipEntry(fileName);//这是一个文件
			 var ze2:ZipEntry = new ZipEntry(fileName2);//这是一个文件
			 
			 
			  var crc:CRC32 = new CRC32();
			 
			 var jpg:BitmapData1 = new BitmapData1(256, 256);
			 var je:JPGEncoder = new JPGEncoder();
			 var pixels:ByteArray = je.encode(jpg);
			 
			 crc.update(pixels);
			 ze2.method = 0; ze2.size = pixels.length; ze2.crc =crc.getValue();
			 
			 zipOut.putNextEntry(ze2);
			 zipOut.write(pixels);
			
			 crc.reset();
			 crc.update(fileData);
			 
			 ze.method = 0; ze.size = fileData.length; ze.crc =crc.getValue();
			 zipOut.putNextEntry(ze);
			 zipOut.write(fileData);
			 
			 
			 zipOut.closeEntry();
			// end the zip
			 zipOut.finish();
			// access the zip data
			 var zipData:ByteArray = zipOut.byteArray;
			trace(zipData.position, zipData.length);
		   //trace(MD5.hashBytes(zipData));

		   //trace(SHA1.hashBytes(zipData));
		   //return zipData;
		   zipData.readBytes(toread);
		}

		
		
		//public static function showzip(input:IDataInput)
		//{
			//var zip:ZipFile = new ZipFile(input);			
			//for (var i:int = 0; i < zip.entries.length; i++) 
			//{
				//var entry:ZipEntry = zip.entries[i];
				//trace(entry.name,entry.size,entry.compressedSize);
				//
				//
				//var data:ByteArray = zip.getInput(entry);
				//data.position = 0;
				//trace(data.length,data.readUTFBytes(data.length));
				//
			//}
			//
		//}
		
		

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

