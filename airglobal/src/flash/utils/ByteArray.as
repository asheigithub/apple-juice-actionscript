//
// C:\Users\Manju-pc\AppData\Local\FlashDevelop\Apps\ascsdk\27.0.0\frameworks\libs\air\airglobal.swc\flash\utils\ByteArray
//
package flash.utils
{
	import flash.utils.ByteArray;
	import flash.errors.IllegalOperationError;
	/**
	 * ByteArray 类提供用于优化读取、写入以及处理二进制数据的方法和属性。
	 * 
	 *   <p class="- topic/p "><i class="+ topic/ph hi-d/i ">注意：</i>ByteArray 类适用于需要在字节层访问数据的高级 开发人员。</p><p class="- topic/p ">内存中的数据是一个压缩字节数组（数据类型的最紧凑表示形式），但可以使用标准 <codeph class="+ topic/ph pr-d/codeph ">[]</codeph>（数组访问）运算符来操作 ByteArray 类的实例。也可以使用与 URLStream 和 Socket 类中的方法相类似的方法将它作为内存中的文件进行读取和写入。</p><p class="- topic/p ">此外，还支持 zlib 压缩和解压缩，以及 Action Message Format (AMF) 对象序列化。</p><p class="- topic/p ">ByteArray 类可能的用途包括：
	 * 
	 *   <ul class="- topic/ul "><li class="- topic/li ">创建用以连接到服务器的自定义协议。</li><li class="- topic/li ">编写自己的 URLEncoder/URLDecoder。</li><li class="- topic/li ">编写自己的 AMF/Remoting 包。</li><li class="- topic/li ">通过使用数据类型优化数据的大小。</li><li class="- topic/li ">在 Adobe<sup class="+ topic/ph hi-d/sup ">®</sup> AIR<sup class="+ topic/ph hi-d/sup ">®</sup> 中处理从文件加载的二进制数据。</li></ul></p>
	 * 
	 *   EXAMPLE:
	 * 
	 *   以下示例使用 <codeph class="+ topic/ph pr-d/codeph ">ByteArrayExample</codeph> 类将布尔值和 pi 的双精度浮点表示形式写入字节数组。这是使用以下步骤完成的：
	 * <ol class="- topic/ol "><li class="- topic/li ">声明新的 ByteArray 对象实例 <codeph class="+ topic/ph pr-d/codeph ">byteArr</codeph>。</li><li class="- topic/li ">写入布尔值 <codeph class="+ topic/ph pr-d/codeph ">false</codeph> 的字节等效值，然后检查长度并重新读取。</li><li class="- topic/li ">写入数学值 pi 的双精度浮点等效值。</li><li class="- topic/li ">重新读取写入字节数组的九个字节中的每一个字节。</li></ol><p class="- topic/p "><b class="+ topic/ph hi-d/b ">注意：</b>在字节上调用 <codeph class="+ topic/ph pr-d/codeph ">trace()</codeph> 时，它将输出存储于字节数组中的字节的十进制等效值。</p><p class="- topic/p ">注意如何在末尾添加一段代码以检查文件结尾错误，确保读取的字节流没有超出文件结尾。</p><codeblock xml:space="preserve" class="+ topic/pre pr-d/codeblock ">
	 * package {
	 * import flash.display.Sprite;
	 * import flash.utils.ByteArray;
	 * import flash.errors.EOFError;
	 * 
	 *   public class ByteArrayExample extends Sprite {        
	 * public function ByteArrayExample() {
	 * var byteArr:ByteArray = new ByteArray();
	 * 
	 *   byteArr.writeBoolean(false);
	 * trace(byteArr.length);            // 1
	 * trace(byteArr[0]);            // 0
	 * 
	 *   byteArr.writeDouble(Math.PI);
	 * trace(byteArr.length);            // 9
	 * trace(byteArr[0]);            // 0
	 * trace(byteArr[1]);            // 64
	 * trace(byteArr[2]);            // 9
	 * trace(byteArr[3]);            // 33
	 * trace(byteArr[4]);            // 251
	 * trace(byteArr[5]);            // 84
	 * trace(byteArr[6]);            // 68
	 * trace(byteArr[7]);            // 45
	 * trace(byteArr[8]);            // 24
	 * 
	 *   byteArr.position = 0;
	 * 
	 *   try {
	 * trace(byteArr.readBoolean() == false); // true
	 * } 
	 * catch(e:EOFError) {
	 * trace(e);           // EOFError: Error #2030: End of file was encountered.
	 * }
	 * 
	 *   try {
	 * trace(byteArr.readDouble());        // 3.141592653589793
	 * }
	 * catch(e:EOFError) {
	 * trace(e);           // EOFError: Error #2030: End of file was encountered.
	 * }
	 * 
	 *   try {
	 * trace(byteArr.readDouble());
	 * } 
	 * catch(e:EOFError) {
	 * trace(e);            // EOFError: Error #2030: End of file was encountered.
	 * }
	 * }
	 * }
	 * }
	 * </codeblock>
	 * @langversion	3.0
	 * @playerversion	Flash 9
	 * @playerversion	Lite 4
	 */
	public class ByteArray implements IDataInput2, IDataOutput2
	{
		
		public function ByteArray(){}
		

		/**
		 * 可从字节数组的当前位置到数组末尾读取的数据的字节数。
		 * 
		 *   每次访问 ByteArray 对象时，将 bytesAvailable 属性与读取方法结合使用，以确保读取有效的数据。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public function get bytesAvailable () : uint{ return 0; }
		

		/**
		 * 表示用于新 ByteArray 实例的 ByteArray 类的默认对象编码。在创建新的 ByteArray 实例时，该实例上的编码以 defaultObjectEncoding 的值开头。defaultObjectEncoding 属性被初始化为 ObjectEncoding.AMF3。
		 * 
		 *   将对象写入二进制数据或从中读取对象时，将使用 objectEncoding 值来确定应使用 ActionScript 3.0、ActionScript2.0 还是 ActionScript 1.0 格式。该值为 ObjectEncoding 类中的常数。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public static function get defaultObjectEncoding () : uint
		{
			return 0;
		}

		public static function set defaultObjectEncoding (version:uint) : void{  }

		
		/**
		 * 更改或读取数据的字节顺序；Endian.BIG_ENDIAN 或 Endian.LITTLE_ENDIAN。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public function get endian () : String
		{
			return null;
		}
		public function set endian (type:String) : void{}

		/**
		 * ByteArray 对象的长度（以字节为单位）。
		 * 
		 *   如果将长度设置为大于当前长度的值，则用零填充字节数组的右侧。如果将长度设置为小于当前长度的值，将会截断该字节数组。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public function get length () : uint{ return 0; }
		public function set length (value:uint) : void{ }

		/**
		 * 用于确定在写入或读取 ByteArray 实例时应使用 ActionScript 3.0、ActionScript 2.0 还是 ActionScript 1.0 格式。该值为 ObjectEncoding 类中的常数。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public function get objectEncoding () : uint
		{
			return 0;
		}
		public function set objectEncoding (version:uint) : void{}

		/**
		 * 将文件指针的当前位置（以字节为单位）移动或返回到 ByteArray 对象中。下一次调用读取方法时将在此位置开始读取，或者下一次调用写入方法时将在此位置开始写入。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public function get position () : uint{ return 0; }
		public function set position (offset:uint) : void{}

		public function get shareable () : Boolean
		{
			return false;
		}
		public function set shareable (newValue:Boolean) : void{}

		

		/**
		 * 清除字节数组的内容，并将 length 和 position 属性重置为 0。明确调用此方法将释放 ByteArray 实例占用的内存。
		 * @langversion	3.0
		 * @playerversion	Flash 10
		 * @playerversion	AIR 1.5
		 * @playerversion	Lite 4
		 */
		public function clear () : void{}

		/**
		 * 未实现
		 * 
		 *   
		 */
		public function compress (algorithm:String="zlib") : void{}

		/**
		 * 未实现
		 */
		public function deflate () : void{}

		/**
		 * 未实现
		 */
		public function inflate () : void{}

		/**
		 * 从字节流中读取布尔值。读取单个字节，如果字节非零，则返回 true，否则返回 false。
		 * @return	如果字节不为零，则返回 true，否则返回 false。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 * @throws	EOFError 没有足够的数据可供读取。
		 */
		public function readBoolean () : Boolean{ return false; }
		

		/**
		 * 从字节流中读取带符号的字节。
		 * 返回值的范围是从 -128 到 127。
		 * @return	介于 -128 和 127 之间的整数。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 * @throws	EOFError 没有足够的数据可供读取。
		 */
		public function readByte () : int{ return 0; }
		

		/**
		 * 从字节流中读取 length 参数指定的数据字节数。从 offset 指定的位置开始，将字节读入 bytes 参数指定的 ByteArray 对象中，并将字节写入目标 ByteArray 中。
		 * @param	bytes	要将数据读入的 ByteArray 对象。
		 * @param	offset	bytes 中的偏移（位置），应从该位置写入读取的数据。
		 * @param	length	要读取的字节数。默认值 0 导致读取所有可用的数据。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 * @throws	EOFError 没有足够的数据可供读取。
		 * @throws	RangeError 所提供的位移和长度的组合值大于单元的最大值。
		 */
		public function readBytes (bytes:ByteArray, offset:uint=0, length:uint=0) : void{}

		/**
		 * 从字节流中读取一个 IEEE 754 双精度（64 位）浮点数。
		 * @return	双精度（64 位）浮点数。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 * @throws	EOFError 没有足够的数据可供读取。
		 */
		public function readDouble () : Number{ return 0; }
		

		/**
		 * 从字节流中读取一个 IEEE 754 单精度（32 位）浮点数。
		 * @return	单精度（32 位）浮点数。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 * @throws	EOFError 没有足够的数据可供读取。
		 */
		public function readFloat () : Number{ return 0; }
		

		/**
		 * 从字节流中读取一个带符号的 32 位整数。
		 * 
		 *   返回值的范围是从 -2147483648 到 2147483647。
		 * @return	介于 -2147483648 和 2147483647 之间的 32 位带符号整数。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 * @throws	EOFError 没有足够的数据可供读取。
		 */
		public function readInt () : int{ return 0; }
		

		/**
		 * 使用指定的字符集从字节流中读取指定长度的多字节字符串。
		 * @param	length	要从字节流中读取的字节数。
		 * @param	charSet	表示用于解释字节的字符集的字符串。可能的字符集字符串包括 "shift-jis"、"cn-gb"、"iso-8859-1"”等。有关完整列表，请参阅支持的字符集。
		 *   注意：如果当前系统无法识别 charSet 参数的值，则应用程序将使用系统的默认代码页作为字符集。例如，charSet 参数的值（如在使用 01 而不是 1 的 myTest.readMultiByte(22, "iso-8859-01") 中）可能在您的开发系统上起作用，但在其他系统上可能不起作用。在其他系统上，应用程序将使用系统的默认代码页。
		 * @return	UTF-8 编码的字符串。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 * @throws	EOFError 没有足够的数据可供读取。
		 */
		public function readMultiByte (length:uint, charSet:String) : String{ return null; }
		

		/**
		 * 从字节数组中读取一个以 AMF 序列化格式进行编码的对象。
		 * @return	反序列化的对象。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 * @throws	EOFError 没有足够的数据可供读取。
		 */
		public function readObject () : *
		{
			return null;
		}

		/**
		 * 从字节流中读取一个带符号的 16 位整数。
		 * 
		 *   返回值的范围是从 -32768 到 32767。
		 * @return	介于 -32768 和 32767 之间的 16 位带符号整数。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 * @throws	EOFError 没有足够的数据可供读取。
		 */
		public function readShort () : int{ return 0; }
		

		/**
		 * 从字节流中读取无符号的字节。
		 * 
		 *   返回值的范围是从 0 到 255。
		 * @return	介于 0 和 255 之间的 32 位无符号整数。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 * @throws	EOFError 没有足够的数据可供读取。
		 */
		public function readUnsignedByte () : uint{ return 0; }
		

		/**
		 * 从字节流中读取一个无符号的 32 位整数。
		 * 
		 *   返回值的范围是从 0 到 4294967295。
		 * @return	介于 0 和 4294967295 之间的 32 位无符号整数。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 * @throws	EOFError 没有足够的数据可供读取。
		 */
		public function readUnsignedInt () : uint{ return 0; }
		

		/**
		 * 从字节流中读取一个无符号的 16 位整数。
		 * 
		 *   返回值的范围是从 0 到 65535。
		 * @return	介于 0 和 65535 之间的 16 位无符号整数。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 * @throws	EOFError 没有足够的数据可供读取。
		 */
		public function readUnsignedShort () : uint{ return 0; }
		

		/**
		 * 从字节流中读取一个 UTF-8 字符串。假定字符串的前缀是无符号的短整型（以字节表示长度）。
		 * @return	UTF-8 编码的字符串。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 * @throws	EOFError 没有足够的数据可供读取。
		 */
		public function readUTF () : String{ return null; }
		

		/**
		 * 从字节流中读取一个由 length 参数指定的 UTF-8 字节序列，并返回一个字符串。
		 * @param	length	指明 UTF-8 字节长度的无符号短整型数。
		 * @return	由指定长度的 UTF-8 字节组成的字符串。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 * @throws	EOFError 没有足够的数据可供读取。
		 */
		public function readUTFBytes (length:uint) : String{ return null; }
		

		/**
		 * 将字节数组转换为字符串。如果数组中的数据以 Unicode 字节顺序标记开头，应用程序在将其转换为字符串时将保持该标记。如果 System.useCodePage 设置为 true，应用程序在转换时会将数组中的数据视为处于当前系统代码页中。
		 * @return	字节数组的字符串表示形式。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public function toString () : String{ return null; }
		

		/**
		 * 未实现
		 */
		public function uncompress (algorithm:String="zlib") : void{}

		/**
		 * 写入布尔值。根据 value 参数写入单个字节。如果为 true，则写入 1，如果为 false，则写入 0。
		 * @param	value	确定写入哪个字节的布尔值。如果该参数为 true，则该方法写入 1；如果该参数为 false，则该方法写入 0。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public function writeBoolean (value:Boolean) : void{}

		/**
		 * 在字节流中写入一个字节。
		 * 使用参数的低 8 位。忽略高 24 位。
		 * @param	value	一个 32 位整数。低 8 位将被写入字节流。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public function writeByte (value:int) : void{}

		/**
		 * 将指定字节数组 bytes（起始偏移量为 offset，从零开始的索引）中包含 length 个字节的字节序列写入字节流。
		 * 
		 *   如果省略 length 参数，则使用默认长度 0；该方法将从 offset 开始写入整个缓冲区。如果还省略了 offset 参数，则写入整个缓冲区。 如果 offset 或 length 超出范围，它们将被锁定到 bytes 数组的开头和结尾。
		 * @param	bytes	ByteArray 对象。
		 * @param	offset	从 0 开始的索引，表示在数组中开始写入的位置。
		 * @param	length	一个无符号整数，表示在缓冲区中的写入范围。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public function writeBytes (bytes:ByteArray, offset:uint=0, length:uint=0) : void{}

		/**
		 * 在字节流中写入一个 IEEE 754 双精度（64 位）浮点数。
		 * @param	value	双精度（64 位）浮点数。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public function writeDouble (value:Number) : void{}

		/**
		 * 在字节流中写入一个 IEEE 754 单精度（32 位）浮点数。
		 * @param	value	单精度（32 位）浮点数。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public function writeFloat (value:Number) : void{}

		/**
		 * 在字节流中写入一个带符号的 32 位整数。
		 * @param	value	要写入字节流的整数。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public function writeInt (value:int) : void{}

		/**
		 * 使用指定的字符集将多字节字符串写入字节流。
		 * @param	value	要写入的字符串值。
		 * @param	charSet	表示要使用的字符集的字符串。可能的字符集字符串包括 "shift-jis"、"cn-gb"、"iso-8859-1"”等。有关完整列表，请参阅支持的字符集。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public function writeMultiByte (value:String, charSet:String) : void{}

		/**
		 * 未实现
		 */
		public function writeObject (object:*) : void { throw new flash.errors.IllegalOperationError(); }

		/**
		 * 在字节流中写入一个 16 位整数。使用参数的低 16 位。忽略高 16 位。
		 * @param	value	32 位整数，该整数的低 16 位将被写入字节流。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public function writeShort (value:int) : void{}

		/**
		 * 在字节流中写入一个无符号的 32 位整数。
		 * @param	value	要写入字节流的无符号整数。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public function writeUnsignedInt (value:uint) : void{}

		/**
		 * 将 UTF-8 字符串写入字节流。先写入以字节表示的 UTF-8 字符串长度（作为 16 位整数），然后写入表示字符串字符的字节。
		 * @param	value	要写入的字符串值。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 * @throws	RangeError 如果长度大于 65535。
		 */
		public function writeUTF (value:String) : void{}

		/**
		 * 将 UTF-8 字符串写入字节流。类似于 writeUTF() 方法，但 writeUTFBytes() 不使用 16 位长度的词为字符串添加前缀。
		 * @param	value	要写入的字符串值。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public function writeUTFBytes (value:String) : void{}
		


		public function getThisItem(key:Number):Number{ return 0; }
		
		public function setThisItem(value:Number, key:Number):void{}

	}
}
