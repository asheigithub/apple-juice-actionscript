package
{
	import autogencodelib.Testobj;
	import com.adobe.crypto.SHA1;
	import com.adobe.serialization.json.JSON;
	import com.adobe.images.BitmapData1;
	import com.adobe.images.JPGEncoder;
	import flash.display.Sprite;
	import flash.filesystem.File;
	import flash.filesystem.FileMode;
	import flash.filesystem.FileStream;
	import flash.utils.ByteArray;
	import flash.utils.IDataInput;
	import flash.utils.getDefinitionByName;
	import nochump.util.zip.CRC32;
	import nochump.util.zip.ZipEntry;
	import nochump.util.zip.ZipFile;
	import nochump.util.zip.ZipOutput;
	import system.DateTime;
	import system.Decimal;
	import system.EnvironmentVariableTarget;
	import system.TimeSpan;
	import system._Array_;
	import system._Object_;
	
	[Doc]
	/**
	 * ...
	 * @author
	 */
	public class Main extends Sprite
	{
		
		public function Main()
		{
			
			
			
			//var d:Decimal = new Decimal(5);
			//
			//Decimal.prototype.bbb = function(){ trace(this); };
			//
			//Object( d).bbb();
			
			//var obj:Object = { foo: { foo2: { foo3: { foo4: "bar" } } } };
			//var s:String = com.adobe.serialization.json.JSON.encode( obj );
			//
			//trace(s);
			//var v:Vector.<String> = new Vector.<String>();
			//v[0] = null;
			//trace(  v[0], SHA1.hash(typeof v[0]));
			
			//var v:Vector.<String> = new Vector.<String>();
			//
			//var millionAs:String = new String("");
			//for ( var i:int = 0; i < 10000; i++ ) {
			//
			//v.push("a");
			//
			////millionAs += "a";
			//}
			//
			//millionAs = v.join();
			//
			////trace(v.length,v[0],v[1],v[2],v[10000-1].length);
			////
			//trace(SHA1.hash(millionAs));
			
			
			//var c:Class = getDefinitionByName("Main::et");
			
			var t:* = new et();
			
			t.ATTT(EnvironmentVariableTarget.Process);
			
			var arr1:_Array_ = Testobj.make(5);
			for each (var m:Testobj in arr1)
			{
				trace(m.x);
				
				m.EventTest_addEventListener(function(sender, args)
				{
					trace(sender, args);
				
				});
				
				m.onEvent();
			}
			
			trace(t.geteList(null).count);
			
			t[99] += "bbbc";
			
			trace(t[99]);
			
			
			
			var mt:Testobj = new Testobj(6);
			mt.handler = function(a:String,b:int,c:Number):int
			{
				trace(a, b, c);
				return c;
			};
			
			mt.doHandler("a", 3, 99);
			
			
			//var t:Testobj = new Testobj(3);
			//
			//var st:DateTime = DateTime.now;
			////var m:Number;
			//for (var i:int = 0; i < 200000	; i++) 
			//{
				////m = i;
				////m++;
				////m++;
				//
				//t.roation(1, 2, 3);
				//
				//
				//
				////t.inner.inner.inner.inner.name="123123";
				////m--; m--;
				//
				////this.abc(3, 4,10);
			//}
			//
			//
			//trace( TimeSpan(DateTime.now-st).totalMilliseconds );
			
			
			
		//var stream:FileStream = new FileStream();
				//var file:File = new File('F:/code/Protobuf-as3-ILRuntime-master.zip');//绑定一个文件
				//stream.open(file,FileMode.READ);//读取文件
				//showzip(stream);
				//stream.close();
				
				//var st:DateTime = DateTime.now;
				//var tt:Testobj = new Testobj(3);
				//for (var i:int = 0; i < 200000; i++) 
				//{
					//tt.a = i; tt.b = i; tt.c = i;
					//Testobj.stest();
				//}
				//
				//trace( TimeSpan(DateTime.now-st).totalMilliseconds );
				
				//var tt:Testobj = new Testobj(3);
				//var a:Array = new Array();
				//a[0] = tt;
				//
				//var b = a[0];
				//
				//a[0] = new Testobj(5);
				//
				//trace(b == tt,b.x);
				//
				//trace(tt.x);
				//
				//trace(a[0].x);
				
				//var tt=new Testobj(3);
				//
				//member = tt;
				//
				//var b = member;
				//
				//b.x = 99;
				//
				//trace(b == tt,b.x);
				//
				//member = new Testobj(5);
				//
				//trace(b == member,b.x,member.x,tt.x);
				
				//arr = new Array();
				//
				//var b = new et();
				//
				//arr[0] = b;				
				////member.dialog();
				//arr[1] = null;
				//arr[2] = b;
				////trace(b.x);
				//
				//arr[0] = new Testobj(4);
				//
				//b = arr[0];
				////b.dialog();
				//
				////trace(b.x);
				//
				//
				//arr[1] = arr[0];
				//
				//arr[1] = new Testobj(1);
				//
				//trace(b == arr[1],b.x,arr[1].x);
				//arr[0].x = 99;
				//
				//trace(b == arr[0],b.x,arr[0].x);
				//
				//arr[2].dialog();
		}
		
		//public var arr:Array;
		
		//public var member:Testobj;
		//public var member2:Testobj;
		
		private function abc(x:Number,y:Number,z:Number=7):void
		{
			//trace(z);
		}
		
		public static function saveZip(toread:ByteArray):void
		{
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
			ze2.method = 0;
			ze2.size = pixels.length;
			ze2.crc = crc.getValue();
			
			zipOut.putNextEntry(ze2);
			zipOut.write(pixels);
			
			crc.reset();
			crc.update(fileData);
			
			ze.method = 0;
			ze.size = fileData.length;
			ze.crc = crc.getValue();
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
		
		public static function showzip(input:IDataInput)
		{
			var zip:ZipFile = new ZipFile(input);
			for (var i:int = 0; i < zip.entries.length; i++)
			{
				var entry:ZipEntry = zip.entries[i];
				trace(entry.name, entry.size, entry.compressedSize);
				
				var data:ByteArray = zip.getInput(entry);
				data.position = 0;
				trace(data.length);// , data.readUTFBytes(data.length));
				
			}
		
		}
	
	}

}


import autogencodelib.Testobj;

class et extends Testobj
{
	public function et(v:int=5)
	{
		super(v);
	}
	
	override public function dialog():void
	{
		super.dialog();
		
		trace("override");
	
	}

}

