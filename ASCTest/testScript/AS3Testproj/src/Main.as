package
{
	import autogencodelib.Testobj;
	import com.adobe.crypto.SHA1;
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
	import system.EnvironmentVariableTarget;
	import system._Array_;
	
	[Doc]
	/**
	 * ...
	 * @author
	 */
	public class Main extends Sprite
	{
		
		public function Main()
		{
			
			
			var v:Vector.<String> = new Vector.<String>();
			
			
			var millionAs:String = new String("");
			for ( var i:int = 0; i < 10000; i++ ) {
			
			v.push("a");
			
			//millionAs += "a";
			}
			
			millionAs = v.join();
			
			//trace(v.length,v[0],v[1],v[2],v[10000-1].length);
			
			trace(SHA1.hash(millionAs));
			
			
			//var c:Class = getDefinitionByName("Main::et");
			
			//var t:* = new et();
			//
			//t.ATTT(EnvironmentVariableTarget.Process);
			//
			//var arr:_Array_ = Testobj.make(5);
			//for each (var m:Testobj in arr)
			//{
				//trace(m.x);
				//
				//m.EventTest_addEventListener(function(sender, args)
				//{
					//trace(sender, args);
				//
				//});
				//
				//m.onEvent();
			//}
			//
			//trace(t.geteList(null).count);
			//
			//t[99] += "bbbc";
			//
			//trace(t[99]);
		//var stream:FileStream = new FileStream();
				//var file:File = new File('F:/code/Protobuf-as3-ILRuntime-master.zip');//绑定一个文件
				//stream.open(file,FileMode.READ);//读取文件
				//showzip(stream);
				//stream.close();
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

//import autogencodelib.Testobj;
//
//class et extends Testobj
//{
	//public function et()
	//{
		//super(5);
	//}
	//
	//override public function dialog():void
	//{
		//super.dialog();
		//
		//trace("override");
	//
	//}
//}
