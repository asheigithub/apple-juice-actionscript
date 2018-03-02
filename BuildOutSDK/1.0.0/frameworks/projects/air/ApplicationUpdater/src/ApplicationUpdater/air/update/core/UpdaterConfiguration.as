/*
ADOBE SYSTEMS INCORPORATED
Copyright 2008 Adobe Systems Incorporated. All Rights Reserved.
 
NOTICE:   Adobe permits you to modify and distribute this file only in accordance with
the terms of Adobe AIR SDK license agreement.  You may have received this file from a
source other than Adobe.  Nonetheless, you may modify or distribute this file only in 
accordance with such agreement.
*/

package air.update.core
{
	import air.update.descriptors.ConfigurationDescriptor;
	import air.update.utils.Constants;
	import air.update.utils.FileUtils;
	import air.update.utils.VersionUtils;
	import air.update.logging.Logger;
	
	import flash.filesystem.File;
	
	[ExcludeClass]
	public class UpdaterConfiguration
	{
		private static var logger:Logger = Logger.getLogger("air.update.core.UpdaterConfiguration");
		
		private var _updateURL:String;
		private var _delay:Number;
		private var configurationDescriptor:ConfigurationDescriptor;
		private var _isNewerVersionFunction:Function;
		private var _configurationFile:File;
		private var _isCheckForUpdateVisible:int;
		private var _isDownloadUpdateVisible:int;
		private var _isDownloadProgressVisible:int;
		private var _isInstallUpdateVisible:int;
		private var _isFileUpdateVisible:int;
		private var _isUnexpectedErrorVisible:int;				
		
		public function UpdaterConfiguration()
		{
			_delay = -1;
			_isCheckForUpdateVisible = -1;
			_isDownloadUpdateVisible = -1;
			_isDownloadProgressVisible = -1;
			_isInstallUpdateVisible = -1;
			_isFileUpdateVisible = -1;
			_isUnexpectedErrorVisible = -1;
			_isNewerVersionFunction = VersionUtils.isNewerVersion;
		}

		public function get updateURL():String
		{
			return _updateURL ? _updateURL : configurationDescriptor.url;
		}
		
		public function set updateURL(value:String):void
		{
			_updateURL = value;
		}		
		
		public function get delay():Number
		{
			if (_delay < 0)
			{
				if (configurationDescriptor && configurationDescriptor.checkInterval >= 0)
				{
					return configurationDescriptor.checkInterval;
				}
				return 0;
			} 
			return _delay;
		}
		
		public function get delayAsMilliseconds():Number
		{
			return delay * Constants.DAY_IN_MILLISECONDS;
		}
		
		public function set delay(value:Number):void
		{
			_delay = value;
		}
		
		public function get configurationFile():File
		{
			return _configurationFile;
		}
		
		public function set configurationFile(value:File):void
		{
			_configurationFile = value;
		}

		public function get isNewerVersionFunction():Function
		{
			return _isNewerVersionFunction;
		}
		
		public function set isNewerVersionFunction(value:Function):void
		{
			_isNewerVersionFunction = value;
		}
		
		// UI only
		public function get isCheckForUpdateVisible():Boolean
		{
			// setter was used ?
			if (_isCheckForUpdateVisible >= 0)
			{
				return _isCheckForUpdateVisible == 1;
			}
			var config:int = dialogVisibilityInConfiguration(ConfigurationDescriptor.DIALOG_CHECK_FOR_UPDATE);
			if (config >= 0)
			{
				return config == 1; 
			}
			// default is visible
			return true;
		}
		
		public function set isCheckForUpdateVisible(value:Boolean):void
		{
			_isCheckForUpdateVisible = value ? 1 : 0;
		}
		
		//
		public function get isDownloadUpdateVisible():Boolean
		{
			// setter was used ?
			if (_isDownloadUpdateVisible >= 0)
			{
				return _isDownloadUpdateVisible == 1;
			}
			var config:int = dialogVisibilityInConfiguration(ConfigurationDescriptor.DIALOG_DOWNLOAD_UPDATE);
			if (config >= 0)
			{
				return config == 1; 
			}
			// default is visible
			return true;
		}
		
		public function set isDownloadUpdateVisible(value:Boolean):void
		{
			_isDownloadUpdateVisible = value ? 1 : 0;
		}
		
		//
		public function get isDownloadProgressVisible():Boolean
		{
			// setter was used ?
			if (_isDownloadProgressVisible >= 0)
			{
				return _isDownloadProgressVisible == 1;
			}
			var config:int = dialogVisibilityInConfiguration(ConfigurationDescriptor.DIALOG_DOWNLOAD_PROGRESS);
			if (config >= 0)
			{
				return config == 1; 
			}
			// default is visible
			return true;
		}
		
		public function set isDownloadProgressVisible(value:Boolean):void
		{
			_isDownloadProgressVisible = value ? 1 : 0;
		}
		
		//
		public function get isInstallUpdateVisible():Boolean
		{
			// setter was used ?
			if (_isInstallUpdateVisible >= 0)
			{
				return _isInstallUpdateVisible == 1;
			}
			var config:int = dialogVisibilityInConfiguration(ConfigurationDescriptor.DIALOG_INSTALL_UPDATE);
			if (config >= 0)
			{
				return config == 1; 
			}
			// default is visible
			return true;
		}
		
		public function set isInstallUpdateVisible(value:Boolean):void
		{
			_isInstallUpdateVisible = value ? 1 : 0;
		}		
		
		//
		public function get isFileUpdateVisible():Boolean
		{
			// setter was used ?
			if (_isFileUpdateVisible >= 0)
			{
				return _isFileUpdateVisible == 1;
			}
			var config:int = dialogVisibilityInConfiguration(ConfigurationDescriptor.DIALOG_FILE_UPDATE);
			if (config >= 0)
			{
				return config == 1; 
			}
			// default is visible
			return true;
		}
		
		public function set isFileUpdateVisible(value:Boolean):void
		{
			_isFileUpdateVisible = value ? 1 : 0;
		}		
		
		//
		public function get isUnexpectedErrorVisible():Boolean
		{
			// setter was used ?
			if (_isUnexpectedErrorVisible >= 0)
			{
				return _isUnexpectedErrorVisible == 1;
			}
			var config:int = dialogVisibilityInConfiguration(ConfigurationDescriptor.DIALOG_UNEXPECTED_ERROR);
			if (config >= 0)
			{
				return config == 1; 
			}
			// default is visible
			return true;
		}
		
		public function set isUnexpectedErrorVisible(value:Boolean):void
		{
			_isUnexpectedErrorVisible = value ? 1 : 0;
		}		

		public function validate():void
		{
			// check file
			if (configurationFile)
			{
				if (!configurationFile.exists)
				{
					throw new Error("Configuration file \"" + configurationFile.nativePath + "\" does not exists on disk", Constants.ERROR_CONFIGURATION_FILE_MISSING);
				}
				var xml:XML = FileUtils.readXMLFromFile(configurationFile);
				configurationDescriptor = new ConfigurationDescriptor(xml);
				// this validate may throw
				configurationDescriptor.validate();
			}
			//
			if (!_updateURL && !configurationDescriptor)
			{
				throw new Error("Update URL not set", Constants.ERROR_UPDATE_URL_MISSING);
			}
		}
		
		/**
		 *  Return -1 if not in configuration file
		 *  1 if visible, 0 if not
		 */
		private function dialogVisibilityInConfiguration(name:String):int
		{
			if (!configurationDescriptor)
				return -1;
			var dialogs:Array = configurationDescriptor.defaultUI;
			
			for(var i:int = 0; i < dialogs.length; i++)
			{
				var dlg:Object = dialogs[i];
				if (name.toLowerCase() == dlg.name.toLowerCase())
				{
					return dlg.visible ? 1 : 0;
				}
			}
			//
			return -1;
		}
		
	}
}