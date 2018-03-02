/*
ADOBE SYSTEMS INCORPORATED
Copyright 2008 Adobe Systems Incorporated. All Rights Reserved.
 
NOTICE:   Adobe permits you to modify and distribute this file only in accordance with
the terms of Adobe AIR SDK license agreement.  You may have received this file from a
source other than Adobe.  Nonetheless, you may modify or distribute this file only in 
accordance with such agreement.
*/

package air.update.ui
{
	import air.update.ApplicationUpdater;
	import air.update.events.UpdateEvent;
	
	import flash.events.Event;
	import flash.events.IOErrorEvent;
	import flash.filesystem.File;

	[ExcludeClass]
	public class UpdaterUI extends ApplicationUpdater
	{
		private var uiLoader:EmbeddedUILoader;
		
		public function UpdaterUI()
		{
			super();
		}
		
		override protected function onInitializationComplete():void
		{
			// load the UI
			uiLoader = new EmbeddedUILoader();
			uiLoader.applicationUpdater = this;
			uiLoader.addEventListener(IOErrorEvent.IO_ERROR, function (e:Event):void {
				throw new Error("Cannot load UI");
			});
			uiLoader.addEventListener(Event.COMPLETE, onUILoadComplete);
			uiLoader.load();
		}
		
		private function onUILoadComplete(event:Event):void
		{
			dispatch(new UpdateEvent(UpdateEvent.INITIALIZED));
		}
		
		public function get isCheckForUpdateVisible():Boolean
		{
			return configuration.isCheckForUpdateVisible;
		}
		
		public function set isCheckForUpdateVisible(value:Boolean):void
		{
			configuration.isCheckForUpdateVisible = value;
		}
		
		public function get isDownloadUpdateVisible():Boolean
		{
			return configuration.isDownloadUpdateVisible;
		}
		
		public function set isDownloadUpdateVisible(value:Boolean):void
		{
			configuration.isDownloadUpdateVisible = value;
		}
		
		public function get isDownloadProgressVisible():Boolean
		{
			return configuration.isDownloadProgressVisible;
		}
		
		public function set isDownloadProgressVisible(value:Boolean):void
		{
			configuration.isDownloadProgressVisible = value;
		}
		
		public function get isInstallUpdateVisible():Boolean
		{
			return configuration.isInstallUpdateVisible;
		}		
		
		public function set isInstallUpdateVisible(value:Boolean):void
		{
			configuration.isInstallUpdateVisible = value;
		}
		
		public function get isFileUpdateVisible():Boolean
		{
			return configuration.isFileUpdateVisible;
		}		
		
		public function set isFileUpdateVisible(value:Boolean):void
		{
			configuration.isFileUpdateVisible = value;
		}			

		public function get isUnexpectedErrorVisible():Boolean
		{
			return configuration.isUnexpectedErrorVisible;
		}		
		
		public function set isUnexpectedErrorVisible(value:Boolean):void
		{
			configuration.isUnexpectedErrorVisible = value;
		}			
					
		
		public function get localeChain():Array
		{
			if (uiLoader && uiLoader.initialized)
			{
				return uiLoader.getLocaleChain();
			}
			return [];
		}
		
		public function set localeChain(value:Array):void
		{
			if (uiLoader && uiLoader.initialized)
			{
				uiLoader.setLocaleChain(value);
			}
		}
		
		public function addResources(lang:String, res:Object):void
		{
			if (uiLoader && uiLoader.initialized)
			{
				uiLoader.addResources(lang, res);
			}
		}
		
		override public function checkNow():void
		{
			showUI();
			super.checkNow();
		}
		
		override public function checkForUpdate():void
		{
			hideUI();
			super.checkForUpdate();
		}		
		
		override public function downloadUpdate():void
		{
			hideUI();
			super.downloadUpdate();
		}
		
		override public function installUpdate():void
		{
			hideUI();
			super.installUpdate();
		}
		
		override public function cancelUpdate():void
		{
			hideUI();
			super.cancelUpdate();
		}
		
		override public function installFromAIRFile(file:File):void
		{
			showUI();
			super.installFromAIRFile(file);
		}
	
		// 
		private function hideUI():void
		{
			if (uiLoader && uiLoader.initialized) uiLoader.hideWindow();
		} 
		
		private function showUI():void
		{
			if (uiLoader && uiLoader.initialized) uiLoader.showWindow();
		}	
	
		/** TEST-ONLY for generating UI screens */
		/*
		public function get _applicationDialogs():Object{
			if (uiLoader && uiLoader.initialized){
				return uiLoader._applicationDialogs;
			}
			return null;
		}
		*/	
	
	}
}