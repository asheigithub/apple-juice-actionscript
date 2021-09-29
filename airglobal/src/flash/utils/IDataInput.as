//
// C:\Users\Manju-pc\AppData\Local\FlashDevelop\Apps\ascsdk\27.0.0\frameworks\libs\air\airglobal.swc\flash\utils\IDataInput
//
package flash.utils
{
	import flash.utils.ByteArray;

	/**
	 * IDataInput 接口提供一组用于读取二进制数据的方法。此接口是写入二进制数据的 IDataOutput 接口的 I/O 对应接口。
	 * <p class="- topic/p ">默认情况下，所有 IDataInput 和 IDataOutput 操作均为“bigEndian”（序列中的最高有效字节存储在最低或第一个存储地址），而且都不分块。如果可用数据不足，则会引发 <codeph class="+ topic/ph pr-d/codeph ">EOFError</codeph> 异常。使用 <codeph class="+ topic/ph pr-d/codeph ">IDataInput.bytesAvailable</codeph> 属性来确定可供读取的数据有多少。</p><p class="- topic/p ">符号扩展名仅在读取数据时有效，写入数据时无效。因此，无需单独的写入方法就可以使用 <codeph class="+ topic/ph pr-d/codeph ">IDataInput.readUnsignedByte()</codeph> 和 <codeph class="+ topic/ph pr-d/codeph ">IDataInput.readUnsignedShort()</codeph>。换言之：</p><ul class="- topic/ul "><li class="- topic/li ">将 <codeph class="+ topic/ph pr-d/codeph ">IDataOutput.writeByte()</codeph> 与 <codeph class="+ topic/ph pr-d/codeph ">IDataInput.readUnsignedByte()</codeph> 和 <codeph class="+ topic/ph pr-d/codeph ">IDataInput.readByte()</codeph> 一起使用。</li><li class="- topic/li ">将 <codeph class="+ topic/ph pr-d/codeph ">IDataOutput.writeShort()</codeph> 与 <codeph class="+ topic/ph pr-d/codeph ">IDataInput.readUnsignedShort()</codeph> 和 <codeph class="+ topic/ph pr-d/codeph ">IDataInput.readShort()</codeph> 一起使用。</li></ul>
	 * 
	 *   EXAMPLE:
	 * 
	 *   以下示例使用 <codeph class="+ topic/ph pr-d/codeph ">DataInputExample</codeph> 类将布尔值和 pi 的双精度浮点表示形式写入字节数组。这是使用以下步骤完成的：
	 * <ol class="- topic/ol "><li class="- topic/li ">声明新的 ByteArray 对象实例 <codeph class="+ topic/ph pr-d/codeph ">byteArr</codeph>。</li><li class="- topic/li ">写入布尔值 <codeph class="+ topic/ph pr-d/codeph ">false</codeph> 的字节等效值和数学值 pi 的双精度浮点等效值。</li><li class="- topic/li ">重新读取布尔值和双精度浮点数。</li></ol><p class="- topic/p ">注意如何在末尾添加一段代码以检查文件结尾错误，确保读取的字节流没有超出文件结尾。</p><codeblock xml:space="preserve" class="+ topic/pre pr-d/codeblock ">
	 * package {
	 * import flash.display.Sprite;
	 * import flash.utils.ByteArray;
	 * import flash.errors.EOFError;
	 * 
	 *   public class DataInputExample extends Sprite {        
	 * public function DataInputExample() {
	 * var byteArr:ByteArray = new ByteArray();
	 * 
	 *   byteArr.writeBoolean(false);
	 * byteArr.writeDouble(Math.PI);
	 * 
	 *   byteArr.position = 0;
	 * 
	 *   try {
	 * trace(byteArr.readBoolean()); // false
	 * } 
	 * catch(e:EOFError) {
	 * trace(e);           // EOFError: Error #2030: End of file was encountered.
	 * }
	 * 
	 *   try {
	 * trace(byteArr.readDouble());    // 3.141592653589793
	 * } 
	 * catch(e:EOFError) {
	 * trace(e);           // EOFError: Error #2030: End of file was encountered.
	 * }
	 * 
	 *   try {
	 * trace(byteArr.readDouble());
	 * } 
	 * catch(e:EOFError) {
	 * trace(e);        // EOFError: Error #2030: End of file was encountered.
	 * }
	 * }
	 * }
	 * }
	 * </codeblock>
	 * @langversion	3.0
	 * @playerversion	Flash 9
	 * @playerversion	Lite 4
	 */
	public interface IDataInput
	{
		/**
		 * 返回可在输入缓冲区中读取的数据的字节数。在尝试使用某一种读取方法读取数据之前，用户代码必须调用 bytesAvailable 以确保有足够的数据可用。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		function get bytesAvailable () : uint;

		/**
		 * 数据的字节顺序：为 Endian 类中的 BIG_ENDIAN 或 LITTLE_ENDIAN 常量。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		function get endian () : String;
		function set endian (type:String) : void;

		/**
		 * 用于确定在使用 readObject() 方法写入或读取二进制数据时是使用 AMF3 格式还是 AMF0 格式。该值为 ObjectEncoding 类中的常数。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		function get objectEncoding () : uint;
		function set objectEncoding (version:uint) : void;

		/**
		 * 从文件流、字节流或字节数组中读取布尔值。读取单个字节，如果字节非零，则返回 true，否则返回 false。
		 * @return	一个布尔值，如果字节不为零，则为 true，否则为 false。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 * @throws	EOFError 没有足够的数据可供读取。
		 */
		function readBoolean () : Boolean;

		/**
		 * 从文件流、字节流或字节数组中读取带符号的字节。
		 * @return	返回值的范围是从 -128 到 127。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 * @throws	EOFError 没有足够的数据可供读取。
		 */
		function readByte () : int;

		/**
		 * 从文件流、字节流或字节数组中读取 length 参数指定的数据字节数。将从 offset 指定的位置开始，将字节读入 bytes 参数指定的 ByteArray 对象。
		 * @param	bytes	要将数据读入的 ByteArray 对象。
		 * @param	offset	bytes 参数中的偏移，应从该位置开始读取数据。
		 * @param	length	要读取的字节数。默认值 0 导致读取所有可用的数据。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 * @throws	EOFError 没有足够的数据可供读取。
		 */
		function readBytes (bytes:ByteArray, offset:uint=0, length:uint=0) : void;

		/**
		 * 从文件流、字节流或字节数组中读取 IEEE 754 双精度浮点数。
		 * @return	一个 IEEE 754 双精度浮点数。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 * @throws	EOFError 没有足够的数据可供读取。
		 */
		function readDouble () : Number;

		/**
		 * 从文件流、字节流或字节数组中读取 IEEE 754 单精度浮点数。
		 * @return	一个 IEEE 754 单精度浮点数。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 * @throws	EOFError 没有足够的数据可供读取。
		 */
		function readFloat () : Number;

		/**
		 * 从文件流、字节流或字节数组中读取带符号的 32 位整数。
		 * @return	返回值的范围是从 -2147483648 到 2147483647。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 * @throws	EOFError 没有足够的数据可供读取。
		 */
		function readInt () : int;

		/**
		 * 使用指定的字符集从文件流、字节流或字节数组中读取指定长度的多字节字符串。
		 * @param	length	要从字节流中读取的字节数。
		 * @param	charSet	表示用于解释字节的字符集的字符串。可能的字符集字符串包括 "shift-jis"、"cn-gb"、"iso-8859-1"”等。有关完整列表，请参阅支持的字符集。
		 *   
		 *     注意：如果当前系统无法识别 charSet 参数的值，则 Adobe® Flash® Player 或 Adobe® AIR® 将采用系统的默认代码页作为字符集。例如，myTest.readMultiByte(22, "iso-8859-01") 中使用 01 而不是 1 的 charSet 参数值可能在开发系统而不是另一个系统中起作用。在其他系统上，Flash Player 或 AIR 运行时将使用系统的默认代码页。
		 * @return	UTF-8 编码的字符串。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 * @throws	EOFError 没有足够的数据可供读取。
		 */
		function readMultiByte (length:uint, charSet:String) : String;

		/**
		 * 从文件流、字节流或字节数组中读取以 AMF 序列化格式编码的对象。
		 * @return	反序列化的对象
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 * @throws	EOFError 没有足够的数据可供读取。
		 */
		function readObject () : *;

		/**
		 * 从文件流、字节流或字节数组中读取带符号的 16 位整数。
		 * @return	返回值的范围是从 -32768 到 32767。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 * @throws	EOFError 没有足够的数据可供读取。
		 */
		function readShort () : int;

		/**
		 * 从文件流、字节流或字节数组中读取无符号的字节。
		 * @return	返回值的范围是从 0 到 255。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 * @throws	EOFError 没有足够的数据可供读取。
		 */
		function readUnsignedByte () : uint;

		/**
		 * 从文件流、字节流或字节数组中读取无符号的 32 位整数。
		 * @return	返回值的范围是从 0 到 4294967295。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 * @throws	EOFError 没有足够的数据可供读取。
		 */
		function readUnsignedInt () : uint;

		/**
		 * 从文件流、字节流或字节数组中读取无符号的 16 位整数。
		 * @return	返回值的范围是从 0 到 65535。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 * @throws	EOFError 没有足够的数据可供读取。
		 */
		function readUnsignedShort () : uint;

		/**
		 * 从文件流、字节流或字节数组中读取 UTF-8 字符串。假定字符串的前缀是无符号的短整型（以字节表示长度）。
		 * 
		 *   此方法类似于 Java® IDataInput 接口中的 readUTF() 方法。
		 * @return	由字符的字节表示形式生成的 UTF-8 字符串。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 * @throws	EOFError 没有足够的数据可供读取。
		 */
		function readUTF () : String;

		/**
		 * 从字节流或字节数组中读取 UTF-8 字节序列，并返回一个字符串。
		 * @param	length	要读取的字节数。
		 * @return	由指定长度字符的字节表示形式生成的 UTF-8 字符串。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 * @throws	EOFError 没有足够的数据可供读取。
		 */
		function readUTFBytes (length:uint) : String;
	}
}
