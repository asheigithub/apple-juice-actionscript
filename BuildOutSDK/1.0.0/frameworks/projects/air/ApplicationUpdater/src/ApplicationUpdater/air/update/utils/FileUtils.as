/*
ADOBE SYSTEMS INCORPORATED
Copyright 2008 Adobe Systems Incorporated. All Rights Reserved.
 
NOTICE:   Adobe permits you to modify and distribute this file only in accordance with
the terms of Adobe AIR SDK license agreement.  You may have received this file from a
source other than Adobe.  Nonetheless, you may modify or distribute this file only in 
accordance with such agreement.
*/


package air.update.utils
{
	import air.update.logging.Logger;
	
	import flash.filesystem.File;
	import flash.filesystem.FileMode;
	import flash.filesystem.FileStream;
	import flash.utils.ByteArray;
	
	[ExcludeClass]
	public final class FileUtils
	{
		private static var logger:Logger = Logger.getLogger("air.update.utils.FileUtils");
		
		public static function readUTFBytesFromFile(file:File):String 
		{
			var stream:FileStream = new FileStream();
			stream.open(file, FileMode.READ);

			var result:String = stream.readUTFBytes(stream.bytesAvailable);
			stream.close();

			return result;
		}
		
		public static function readByteArrayFromFile(file:File):ByteArray
		{
			var stream:FileStream = new FileStream();
			stream.open(file, FileMode.READ);

			var result:ByteArray = new ByteArray();
			stream.readBytes(result, 0, file.size);
			stream.close();
			
			return result;
		}
		
		public static function readXMLFromFile(file:File):XML 
		{
			// FIXME: This isn't really correct because only an XML parser can properly
			// figure out the actual encoding of the XML file being read--that information
			// is embeded in the XML declaration itself. However, the limitations of the XML
			// API are currently such that that can't be done. So, this for now.
			
			return new XML(readByteArrayFromFile(file).toString());
		}
		
		public static function saveXMLToFile(xml:XML, file:File):void
		{
			var stream:FileStream = new FileStream();
			stream.open(file, FileMode.WRITE);
			stream.writeUTFBytes(xml.toXMLString());
			stream.close();
		}
		
		public static function getFilenameFromURL(url:String):String
		{
			var idx:int = url.lastIndexOf("/");
			if (idx == -1)
			{
				logger.finest("Cannot get filename from \"" + url + "\"");
				return "";
			}
			return url.substr(idx + 1);
		}
		
		public static function deleteFile(file:File):void
		{
			if (!file.exists)
				return;
			if (file.isDirectory)
				return;
			try
			{
				file.deleteFile();					
			}catch(e:Error) {}
		}
		
		public static function deleteFolder(file:File):void
		{
			if (!file.exists)
				return;
			if (!file.isDirectory)
				return;
			try
			{
				file.deleteDirectory(false);					
			}catch(e:Error) {}
		}		
		
		public static function getDocumentsStateFile():File
		{
			return new File(File.applicationStorageDirectory.nativePath +  "/../../" + VersionUtils.getApplicationID() + "_" + Constants.STATE_FILE);
		}
		
		public static function getStorageStateFile():File
		{
			return File.applicationStorageDirectory.resolvePath(Constants.UPDATER_FOLDER + "/" + Constants.STATE_FILE);
		}
		
		public static function getLocalDescriptorFile():File
		{
			return File.applicationStorageDirectory.resolvePath(Constants.UPDATER_FOLDER + "/" + Constants.DESCRIPTOR_LOCAL_FILE);
		}
		
		public static function getLocalUpdateFile():File
		{
			return File.applicationStorageDirectory.resolvePath(Constants.UPDATER_FOLDER + "/" + Constants.UPDATE_LOCAL_FILE);
		}
		
	}
}