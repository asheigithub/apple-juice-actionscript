/*
ADOBE SYSTEMS INCORPORATED
Copyright 2008 Adobe Systems Incorporated. All Rights Reserved.
 
NOTICE:   Adobe permits you to modify and distribute this file only in accordance with
the terms of Adobe AIR SDK license agreement.  You may have received this file from a
source other than Adobe.  Nonetheless, you may modify or distribute this file only in 
accordance with such agreement.
*/

package air.update
{
	import air.update.events.DownloadErrorEvent;
	import air.update.events.StatusFileUpdateErrorEvent;
	import air.update.events.StatusFileUpdateEvent;
	import air.update.events.StatusUpdateErrorEvent;
	import air.update.events.StatusUpdateEvent;
	import air.update.events.UpdateEvent;
	import air.update.logging.Logger;
	import air.update.states.UpdateState;
	import air.update.ui.UpdaterUI;
	
	import flash.events.ErrorEvent;
	import flash.events.Event;
	import flash.events.EventDispatcher;
	import flash.events.ProgressEvent;
	import flash.filesystem.File;

	/** Dispatched after the initialization is complete **/
	[Event(name="initialized", type="air.update.events.UpdateEvent")]
	/** Dispatched just before the update process begins **/
	[Event(name="checkForUpdate", type="air.update.events.UpdateEvent")]
	/** Dispatched when the download of the update file begins **/
	[Event(name="downloadStart", type="air.update.events.UpdateEvent")]
	/** Dispatched when the download of the update file is complete **/
	[Event(name="downloadComplete", type="air.update.events.UpdateEvent")]
	/** Dispatched just before installing the update (cancellable) **/
	[Event(name="beforeInstall", type="air.update.events.UpdateEvent")]
	/** Dispatched when status information is available after parsing the update descriptor (cancellable if an update is available)**/
	[Event(name="updateStatus", type="air.update.events.StatusUpdateEvent")]
	/** Dispatched when an error occured while trying to download or parse the update descriptor **/
	[Event(name="updateError", type="air.update.events.StatusUpdateErrorEvent")]
	/** Dispatched when an error occured while downloading the update file **/
	[Event(name="downloadError", type="air.update.events.DownloadErrorEvent")]
	/** Dispatched when status information is available after parsing the AIR file from installFromAIRFile call (cancellable if an update is available)**/
	[Event(name="fileUpdateStatus", type="air.update.events.StatusFileUpdateEvent")]
	/** Dispatched when an error occured while trying to parse the AIR file from installFromAIRFile call **/
	[Event(name="fileUpdateError", type="air.update.events.StatusFileUpdateErrorEvent")]
	/** Dispatched after the initialization is complete **/
	[Event(name="progress", type="flash.events.ProgressEvent")]
	/** Dispatched if something goes wrong with our knowledge **/
	[Event(name="error", type="flash.events.ErrorEvent")]	

	public class ApplicationUpdaterUI extends EventDispatcher
	{
		private static var logger:Logger = Logger.getLogger("air.update.ApplicationUpdaterUI");
		
		private var applicationUpdater:UpdaterUI;
		private var isUpdaterUIInitialized:Boolean;
		
		public function ApplicationUpdaterUI():void
		{
			applicationUpdater = new UpdaterUI();
			// initialized
			applicationUpdater.addEventListener(UpdateEvent.INITIALIZED, dispatchProxy);
			applicationUpdater.addEventListener(ErrorEvent.ERROR, dispatchError);
			// adding all other events
			applicationUpdater.addEventListener(UpdateEvent.CHECK_FOR_UPDATE, dispatchProxy);
			applicationUpdater.addEventListener(StatusUpdateEvent.UPDATE_STATUS, dispatchProxy);
			applicationUpdater.addEventListener(UpdateEvent.DOWNLOAD_START, dispatchProxy);
			applicationUpdater.addEventListener(ProgressEvent.PROGRESS, dispatchProxy);
			applicationUpdater.addEventListener(UpdateEvent.DOWNLOAD_COMPLETE, dispatchProxy);
			applicationUpdater.addEventListener(UpdateEvent.BEFORE_INSTALL, dispatchProxy);
			applicationUpdater.addEventListener(StatusUpdateErrorEvent.UPDATE_ERROR, dispatchProxy);
			applicationUpdater.addEventListener(DownloadErrorEvent.DOWNLOAD_ERROR, dispatchProxy);
			applicationUpdater.addEventListener(StatusFileUpdateEvent.FILE_UPDATE_STATUS, dispatchProxy);
			applicationUpdater.addEventListener(StatusFileUpdateErrorEvent.FILE_UPDATE_ERROR, dispatchProxy);			
		}
		
		protected function dispatchError(event:ErrorEvent):void
		{
			// dispatch ErrorEvent only if it wasn't initialized
			if (!isUpdaterUIInitialized)
			{
				dispatchEvent(event);
			}
		}
		
		protected function dispatchProxy(event:Event):void
		{
			if (event.type == UpdateEvent.INITIALIZED)
			{ 
				isUpdaterUIInitialized = true;
			}
			if (event is ErrorEvent)
			{
				// dispatch error events only if there is a listener for them
				// we don't want the debug window
				if (hasEventListener(event.type))
				{
					dispatchEvent(event);
				}
			} else {
				dispatchEvent(event);
			} 
		}		
		
		//---- Properties ------------
		public function get updateURL():String
		{
			return applicationUpdater.updateURL;
		}
		
		public function set updateURL(value:String):void
		{
			applicationUpdater.updateURL = value;
		}		
		
		public function get delay():Number
		{
			return applicationUpdater.delay;
		}
		
		public function set delay(value:Number):void
		{
			applicationUpdater.delay = value;
		}
		
		public function get configurationFile():File
		{
			return applicationUpdater.configurationFile;
		}
		
		public function set configurationFile(value:File):void
		{
			applicationUpdater.configurationFile = value;
		}
		
		public function get isNewerVersionFunction():Function
		{
			return applicationUpdater.isNewerVersionFunction;
		}
		
		public function set isNewerVersionFunction(value:Function):void
		{
			applicationUpdater.isNewerVersionFunction = value;
		}
		
		// properties
		public function get isFirstRun():Boolean
		{
			return applicationUpdater.isFirstRun;	
		}
		
		public function get wasPendingUpdate():Boolean
		{
			return applicationUpdater.wasPendingUpdate;
		}
		
		public function get currentVersion():String
		{
			return applicationUpdater.currentVersion;
		}
		
		public function get previousVersion():String
		{
			return applicationUpdater.previousVersion;
		}
		
		public function get isUpdateInProgress():Boolean
		{
			return applicationUpdater.currentState != UpdateState.getStateName(UpdateState.READY);
		}
		
		public function get updateDescriptor():XML
		{
			return applicationUpdater.updateDescriptor;
		}
		
		/**
		 * Set to the storage before installing the update
		 * After a certificate migration the storage will be different
		 * This is NULL if it is the same storage
		 */
		public function get previousApplicationStorageDirectory():File
		{
			return applicationUpdater.previousApplicationStorageDirectory;
		}	
		
		public function get isCheckForUpdateVisible():Boolean
		{
			return  applicationUpdater.isCheckForUpdateVisible;
		}
		
		public function set isCheckForUpdateVisible(value:Boolean):void
		{
			applicationUpdater.isCheckForUpdateVisible = value;
		}
		
		public function get isDownloadUpdateVisible():Boolean
		{
			return applicationUpdater.isDownloadUpdateVisible;
		}
		
		public function set isDownloadUpdateVisible(value:Boolean):void
		{
			applicationUpdater.isDownloadUpdateVisible = value;
		}
		
		public function get isDownloadProgressVisible():Boolean
		{
			return applicationUpdater.isDownloadProgressVisible;
		}
		
		public function set isDownloadProgressVisible(value:Boolean):void
		{
			applicationUpdater.isDownloadProgressVisible = value;
		}
		
		public function get isInstallUpdateVisible():Boolean
		{
			return applicationUpdater.isInstallUpdateVisible;
		}
		
		public function set isInstallUpdateVisible(value:Boolean):void
		{
			applicationUpdater.isInstallUpdateVisible = value;
		}
		
		public function get isFileUpdateVisible():Boolean
		{
			return applicationUpdater.isFileUpdateVisible;
		}
		
		public function set isFileUpdateVisible(value:Boolean):void
		{
			applicationUpdater.isFileUpdateVisible = value;
		}
		
		public function get isUnexpectedErrorVisible():Boolean
		{
			return applicationUpdater.isUnexpectedErrorVisible;
		}
		
		public function set isUnexpectedErrorVisible(value:Boolean):void
		{
			applicationUpdater.isUnexpectedErrorVisible = value;
		}				
		
		public function get localeChain():Array
		{
			return applicationUpdater.localeChain;
		}
		
		public function set localeChain(value:Array):void
		{
			applicationUpdater.localeChain = value;
		}
		
		public function addResources(lang:String, res:Object):void
		{
			applicationUpdater.addResources(lang, res);
		}
		
		//-----Public functions ------
		/**
		 * 
		 */
		public function initialize():void
		{
			applicationUpdater.initialize();
		}
		
		public function checkNow():void
		{
			applicationUpdater.checkNow();
		}
		
		public function installFromAIRFile(file:File):void
		{
			applicationUpdater.installFromAIRFile(file);
		}
		
		public function cancelUpdate():void
		{
			applicationUpdater.cancelUpdate();	
		}
		
		/** TEST-ONLY for generating UI screens */
		/*
		public function _setCurrentState(state:String):Boolean{
			var dialogs:Object = applicationUpdater._applicationDialogs;
			if(dialogs!=null)
			{
				return dialogs._setCurrentState(state);
			}
			
			return false;
		}
		*/
		
		
	}
}