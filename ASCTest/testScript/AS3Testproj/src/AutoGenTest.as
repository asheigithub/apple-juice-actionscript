package 
{
	
	import flash.display.Sprite;
	import system.Byte;
	import system._Array_;
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
		
		public function AutoGenTest() 
		{
			var md5:MD5CryptoServiceProvider = new MD5CryptoServiceProvider();
			
			
			
			var bytes:_Array_ = Encoding.UTF8.getBytes___("md5testttt");
			
			var ms:MemoryStream = MemoryStream.constructor__(bytes); 
			
			var md5bytes= md5.computeHash(ms);
			
			for each (var c:Byte in md5bytes) 
			{
				trace( c.toString___("X2") );
			}
			
			ms.close();
		}
		
	}
	

}