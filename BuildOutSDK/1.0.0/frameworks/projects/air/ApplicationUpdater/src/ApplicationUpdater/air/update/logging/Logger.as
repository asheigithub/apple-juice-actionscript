/*
ADOBE SYSTEMS INCORPORATED
Copyright 2008 Adobe Systems Incorporated. All Rights Reserved.
 
NOTICE:   Adobe permits you to modify and distribute this file only in accordance with
the terms of Adobe AIR SDK license agreement.  You may have received this file from a
source other than Adobe.  Nonetheless, you may modify or distribute this file only in 
accordance with such agreement.
*/

package air.update.logging
{
	import flash.filesystem.File;
	import flash.filesystem.FileMode;
	import flash.filesystem.FileStream;
	import flash.utils.Dictionary;
	
	[ExcludeClass]
	public class Logger
	{
		
		private static var loggers:Dictionary = new Dictionary();
		
		private static var _level:int = Level.OFF;
		private var name:String = "";
		
		public function Logger(name:String):void
		{
			this.name = name;
		}
		
		static public function getLogger(name:String):Logger
		{
			if (!loggers[name]) {
				return new Logger(name);
			}
			return loggers[name];
		}
		
		public function log(logLevel:int, ...arguments):void
		{
			if (!isLoggable(logLevel))
			{
				return;
			}
			var s:String = format(logLevel, arguments); 
			trace(s);
			try
			{
				var file:File = new File(File.documentsDirectory.nativePath +   "/../.airappupdater.log");
				if (file.exists)
				{
					var fs:FileStream = new FileStream();
					fs.open(file, FileMode.APPEND);
					fs.writeUTFBytes(s + "\n");
					fs.close();
				}
			}catch(e:Error){}			
		}
		
		public function finest(...arguments):void
		{
			log(Level.FINEST, arguments);
		}

		public function finer(...arguments):void
		{
			log(Level.FINER, arguments);
		}
		
		public function fine(...arguments):void
		{
			log(Level.FINE, arguments);			
		}

		public function info(...arguments):void
		{
			log(Level.INFO, arguments);
		}
		
		public function config(...arguments):void
		{
			log(Level.CONFIG, arguments);
		}
		
		public function warning(...arguments):void
		{
			log(Level.WARNING, arguments);
		}
		
		public function severe(...arguments):void
		{
			log(Level.SEVERE, arguments);
		}
		
		public static function get level():int
		{
			return _level;
		}

		public static function set level(value:int):void
		{
			_level = value;
		}
		
		public function isLoggable(logLevel:int):Boolean
		{
			return logLevel >= level;   
		}
		
		private function format(logLevel:int, ...arguments):String
		{
			var date:Date = new Date();
			var dateString:String = 
					date.fullYear + "/" + date.month + "/" + date.day + " " + 
					date.hours + ":" + date.minutes + ":" + date.seconds + "." + date.milliseconds;	
			var output:String = date + " | " + name + " | [" + Level.getName(logLevel) + "] ";
			if (arguments == null) return output;
			for(var i:int = 0; i < arguments.length; i ++)
			{
				output += (i > 0 ? "," : "") + (arguments[i] != null ? arguments[i].toString() : "null");
			} 
			return output;
		}
		
	}
}