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
	import air.update.core.UpdaterConfiguration;
	import air.update.core.UpdaterHSM;
	import air.update.core.UpdaterState;
	import air.update.events.DownloadErrorEvent;
	import air.update.events.StatusFileUpdateErrorEvent;
	import air.update.events.StatusFileUpdateEvent;
	import air.update.events.StatusUpdateErrorEvent;
	import air.update.events.StatusUpdateEvent;
	import air.update.events.UpdateEvent;
	import air.update.logging.Logger;
	import air.update.states.HSM;
	import air.update.states.HSMEvent;
	import air.update.states.UpdateState;
	import air.update.utils.Constants;
	import air.update.utils.FileUtils;
	import air.update.utils.VersionUtils;
	
	import flash.desktop.Updater;
	import flash.events.ErrorEvent;
	import flash.events.Event;
	import flash.events.ProgressEvent;
	import flash.events.TimerEvent;
	import flash.filesystem.File;
	import flash.utils.Timer;

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
	
	public class ApplicationUpdater extends HSM
	{
		private static var logger:Logger = Logger.getLogger("air.update.ApplicationUpdater");
		private static const EVENT_INITIALIZE:String = "initialize";
		private static const EVENT_CHECK_URL:String = "check.url";
		private static const EVENT_CHECK_FILE:String = "check.file";
		
		protected var configuration:UpdaterConfiguration;
		protected var state:UpdaterState;
		protected var updaterHSM:UpdaterHSM;
		//
		private var _wasPendingUpdate:Boolean = false;
		private var _isFirstRun:Boolean = false;
		private var _previousVersion:String = "";
		private var _previousStorage:File = null;
		//
		private var installFile:File = null;
		//
		private var isInitialized:Boolean = false;
		private var timer:Timer;
		
		public function ApplicationUpdater()
		{
			super(stateUninitialized);
			init();
			configuration = new UpdaterConfiguration();
			state = new UpdaterState();
			updaterHSM = new UpdaterHSM();
			updaterHSM.configuration = configuration;
			updaterHSM.addEventListener(UpdateEvent.CHECK_FOR_UPDATE, dispatch);
			updaterHSM.addEventListener(StatusUpdateEvent.UPDATE_STATUS, dispatch);
			updaterHSM.addEventListener(UpdateEvent.DOWNLOAD_START, dispatch);
			updaterHSM.addEventListener(ProgressEvent.PROGRESS, dispatch);
			updaterHSM.addEventListener(UpdateEvent.DOWNLOAD_COMPLETE, dispatch);
			updaterHSM.addEventListener(UpdateEvent.BEFORE_INSTALL, dispatch);
			updaterHSM.addEventListener(StatusUpdateErrorEvent.UPDATE_ERROR, dispatch);
			updaterHSM.addEventListener(DownloadErrorEvent.DOWNLOAD_ERROR, dispatch);
			
			updaterHSM.addEventListener(UpdaterHSM.EVENT_INSTALL_TRIGGER, dispatch);
			updaterHSM.addEventListener(UpdaterHSM.EVENT_FILE_INSTALL_TRIGGER, dispatch);
			updaterHSM.addEventListener(UpdaterHSM.EVENT_STATE_CLEAR_TRIGGER, onStateClear);
			// this may happen
			updaterHSM.addEventListener(ErrorEvent.ERROR, dispatch);
			//
			updaterHSM.addEventListener(StatusFileUpdateEvent.FILE_UPDATE_STATUS, dispatch);
			updaterHSM.addEventListener(StatusFileUpdateErrorEvent.FILE_UPDATE_ERROR, dispatch);
			// 
			timer = new Timer(0);
			timer.addEventListener(TimerEvent.TIMER, onTimer);
		}
		
		protected function stateUninitialized(event:Event):void
		{
			logger.finest("stateUninitialized: " + event.type);
			switch(event.type)
			{
				case HSMEvent.ENTER:
					isInitialized = false;
					break;
				case EVENT_INITIALIZE:
					transition(stateInitializing);
					break;
			}			
		}
		
		protected function stateInitializing(event:Event):void
		{
			logger.finest("stateInitializing: " + event.type);
			switch(event.type)
			{
				case HSMEvent.ENTER:
						onInitialize();
						break;
				case UpdateEvent.INITIALIZED:
						isInitialized = true;
						transition(stateReady);
						dispatchEvent(event);
						handlePeriodicalCheck();
						break;
				case ErrorEvent.ERROR:
						transition(stateUninitialized);						
						dispatchEvent(event.clone());
						break;
			}		
		}
		
		protected function stateReady(event:Event):void
		{
			logger.finest("stateReady: " + event.type);			
			switch(event.type)
			{
				case HSMEvent.ENTER:
					break;
				case EVENT_CHECK_URL:
					transition(stateRunning);
					dispatch(event);
					break;
				case EVENT_CHECK_FILE:
					transition(stateRunning);
					dispatch(event);
					break;
				case ErrorEvent.ERROR:
					dispatchEvent(event);
					break;
			}
		}
		
		protected function stateRunning(event:Event):void
		{
			logger.finest("stateRunning: " + event.type);
			switch(event.type)
			{
				case HSMEvent.ENTER:
					break;
				//
				case EVENT_CHECK_URL:
					//
					state.descriptor.lastCheckDate = new Date();
					state.saveToStorage();
					//
					handlePeriodicalCheck();
					//
					updaterHSM.checkAsync(configuration.updateURL);			
					break;
				case EVENT_CHECK_FILE:
					updaterHSM.installFile(installFile);
					break;
				//
				case UpdaterHSM.EVENT_CHECK:
				case UpdaterHSM.EVENT_DOWNLOAD:
				case UpdaterHSM.EVENT_INSTALL:
					updaterHSM.dispatch(event);
					break;
				case UpdateEvent.CHECK_FOR_UPDATE:
				case StatusUpdateEvent.UPDATE_STATUS:
				case UpdateEvent.DOWNLOAD_START:
				case ProgressEvent.PROGRESS:
				case UpdateEvent.BEFORE_INSTALL:
					dispatchProxy(event);
					break;
				case StatusUpdateErrorEvent.UPDATE_ERROR:
				case DownloadErrorEvent.DOWNLOAD_ERROR:
				case StatusFileUpdateErrorEvent.FILE_UPDATE_ERROR:
				case ErrorEvent.ERROR:
					dispatchProxy(event);								
					transition(stateReady);
					break;
					
				case StatusFileUpdateEvent.FILE_UPDATE_STATUS:
					onFileStatus(event as StatusFileUpdateEvent);
					break;
				
				case UpdateEvent.DOWNLOAD_COMPLETE:
					onDownloadComplete(event as UpdateEvent);
					break;
					
				
				case UpdaterHSM.EVENT_INSTALL_TRIGGER:
					onInstall();
					break;
					
				case UpdaterHSM.EVENT_FILE_INSTALL_TRIGGER:
					onFileInstall();
					break;
					
			}			
		}
		
		protected function stateCancelled(event:Event):void
		{
			logger.finest("stateCancelled: " + event.type);
			switch(event.type)
			{
				case HSMEvent.ENTER:
					updaterHSM.cancel();
					transition(stateReady);
					break;
			}		
		}

		//---- Properties ------------
		public function get updateURL():String
		{
			return configuration.updateURL;
		}
		
		public function set updateURL(value:String):void
		{
			configuration.updateURL = value;
		}		
		
		public function get delay():Number
		{
			return configuration.delay;
		}
		
		public function set delay(value:Number):void
		{
			configuration.delay = value;
			if (isInitialized) handlePeriodicalCheck();
		}
		
		public function get configurationFile():File
		{
			return configuration.configurationFile;
		}
		
		public function set configurationFile(value:File):void
		{
			configuration.configurationFile = value;
		}
		
		public function get updateDescriptor():XML
		{
			if (updaterHSM.descriptor)
				return updaterHSM.descriptor.getXML();
			return null;
		}

		public function get isNewerVersionFunction():Function
		{
			return configuration.isNewerVersionFunction;
		}
		
		public function set isNewerVersionFunction(value:Function):void
		{
			configuration.isNewerVersionFunction = value;
		}
		
		public function get currentState():String
		{
			if (!isInitialized) return UpdateState.getStateName(UpdateState.UNINITIALIZED);
			return UpdateState.getStateName(updaterHSM.getUpdateState());
		}
		
		public function get isFirstRun():Boolean
		{
			return _isFirstRun;	
		}
		
		public function get wasPendingUpdate():Boolean
		{
			return _wasPendingUpdate;
		}
		
		public function get currentVersion():String
		{
			return VersionUtils.getApplicationVersion();
		}
		
		public function get previousVersion():String
		{
			return _previousVersion;
		}
		
		/**
		 * Set to the storage before installing the update
		 * After a certificate migration the storage will be different
		 * This is NULL if it is the same storage
		 */
		public function get previousApplicationStorageDirectory():File
		{
			return _previousStorage;
		}		
		
		//-----Public functions ------
		/**
		 * 
		 */
		public function initialize():void
		{
			dispatch(new Event(EVENT_INITIALIZE));
		}
		
		protected function onInitialize():void
		{
			configuration.validate();
			state.load();
			// return false when updater is called
			// workaround for a update bug
			if (handleFirstRun())
			{
				onInitializationComplete();
			}		
		}
		
		protected function onInitializationComplete():void
		{
			dispatch(new UpdateEvent(UpdateEvent.INITIALIZED));			
		}
		
		public function checkForUpdate():void
		{
			dispatch(new Event(UpdaterHSM.EVENT_CHECK));
		}
		
		public function downloadUpdate():void
		{
			dispatch(new Event(UpdaterHSM.EVENT_DOWNLOAD));
		}
		
		public function installUpdate():void
		{
			dispatch(new Event(UpdaterHSM.EVENT_INSTALL));
		}
		
		public function cancelUpdate():void
		{
			transition(stateCancelled);
		}
		
		public function checkNow():void
		{
			dispatch(new Event(EVENT_CHECK_URL));
		}
		
		public function installFromAIRFile(file:File):void
		{
			installFile = file;
			dispatch(new Event(EVENT_CHECK_FILE));
			updaterHSM.installFile(file);
		}
		
//		public function set trace(value:Boolean):void
//		{
//			Logger.level = value ? Level.CONFIG : Level.OFF; 
//		}
//----------------------------------------------------------

		protected function handleFirstRun():Boolean
		{
			var result:Boolean = true;
			// if currentVersion is set means that there is an update
			if (!state.descriptor.currentVersion)
			{
				return true;
			}
			if (state.descriptor.updaterLaunched)
			{
				_wasPendingUpdate = true;
				// update installed OK
				if (state.descriptor.currentVersion == VersionUtils.getApplicationVersion())
				{
					_isFirstRun = true;
					_previousVersion = state.descriptor.previousVersion;
					if (state.descriptor.storage.nativePath != File.applicationStorageDirectory.nativePath)
					{
						_previousStorage = state.descriptor.storage;
					}
					state.removeAllFailedUpdates();
					state.resetUpdateData();
					state.removePreviousStorageData(_previousStorage);
					state.saveToStorage();
				}  
				// failed update 
				else if (state.descriptor.previousVersion == VersionUtils.getApplicationVersion())
				{
					// add version from state to failed
					state.addFailedUpdate(state.descriptor.currentVersion);
					state.resetUpdateData();
					state.saveToStorage();
				}
				// maybe installed a different version ? Reset state 
				else 
				{
					_wasPendingUpdate = false;
					state.removeAllFailedUpdates();
					state.resetUpdateData();
					state.saveToStorage();
				}
			} else {
				// postponed update
				if (state.descriptor.previousVersion == VersionUtils.getApplicationVersion())
				{
					var updateFile:File = FileUtils.getLocalUpdateFile();
					if (!updateFile.exists)
					{
						state.resetUpdateData();
						return true;
					}
					try
					{
						state.descriptor.updaterLaunched = true;
						state.saveToStorage();
						state.saveToDocuments();
						var updater:Updater = new Updater();
						updater.update(updateFile, state.descriptor.currentVersion);
						result = false;
					}catch(e:Error)
					{
						logger.warning("The application cannot be updated when is launched from ADL." + e.message);
						state.resetUpdateData();
						state.saveToStorage();						
					}
				} 
				// possible when user postponed an update and the manually installed the new version
				else if (state.descriptor.currentVersion == VersionUtils.getApplicationVersion())
				{
					state.removeAllFailedUpdates();
					state.resetUpdateData();
					state.saveToStorage();								
				}
				// postponed and manually installed a different version ?
				else 
				{
					state.removeAllFailedUpdates();
					state.resetUpdateData();
					state.saveToStorage();				
				}
			}
			return result;
		}
		
		protected function handlePeriodicalCheck():void
		{
			logger.finest("PeriodicalCheck: " + configuration.delay);
			if (configuration.delay == 0)
			{
				
				// no periodical check
				return;
			}
			//
			timer.reset();
			timer.repeatCount = 1;
			//
			var difference:Number = (new Date()).time - state.descriptor.lastCheckDate.time;
			logger.finest("Difference: " + difference + " > " + configuration.delayAsMilliseconds + "(" + state.descriptor.lastCheckDate + ")");
			if (difference > configuration.delayAsMilliseconds)
			{
				// start now
				timer.delay = 1;
			} else
			{
				// because setting a delay > MAX_INT will trigger the timer continuously
				// make the timer max delay = 1 day
				var millisecondsToCheck:Number = configuration.delayAsMilliseconds - difference;
				// We add 1 because if want to timer to run at least once (setting repeatCount to 0 means run indefinitely)
				var daysToComplete:Number = Math.floor(millisecondsToCheck / Constants.DAY_IN_MILLISECONDS) + 1;
				if (millisecondsToCheck > Constants.DAY_IN_MILLISECONDS)
				{
					millisecondsToCheck = Constants.DAY_IN_MILLISECONDS;
				}
				timer.delay = millisecondsToCheck;
				timer.repeatCount = daysToComplete; 
			}
			timer.start();
			logger.finest("PeriodicalCheck: started with delay: " + timer.delay  + " and count: " + timer.repeatCount);		
		}
		
		protected function dispatchProxy(event:Event):void
		{
			if (event.type != ProgressEvent.PROGRESS) logger.info("Dispatching event ", event); 
			if (!dispatchEvent(event)) event.preventDefault(); 
		}
		
		protected function onTimer(event:TimerEvent):void
		{
			// check must be done if the timer run for the repeatCount times
			var isEventDispatched:Boolean = timer.currentCount == timer.repeatCount; 
			handlePeriodicalCheck();
			if (isEventDispatched) dispatch(new Event(EVENT_CHECK_URL));
		}
		
		protected function onDownloadComplete(event:UpdateEvent):void
		{
			state.descriptor.previousVersion = VersionUtils.getApplicationVersion();
			state.descriptor.currentVersion = updaterHSM.descriptor.version;
			state.descriptor.storage = File.applicationStorageDirectory;
			state.saveToStorage();
			dispatchProxy(event);
		}
		
		protected function onFileStatus(event:StatusFileUpdateEvent):void
		{
			if (event.available)
			{
				state.descriptor.previousVersion = VersionUtils.getApplicationVersion();
				state.descriptor.currentVersion = event.version;
				state.descriptor.storage = File.applicationStorageDirectory;
				state.saveToStorage();
			}
			dispatchProxy(event);
		}
		
		protected function onStateClear(event:Event):void
		{
			state.resetUpdateData();
			try {
				state.saveToStorage();
			} catch(e : Error) {
				logger.warning("The application cannot be updated (state file)." + e.message);
			}
		}
		
		protected function onInstall():void
		{
			var updateFile:File = FileUtils.getLocalUpdateFile();
			if (!updateFile.exists)
			{
				// Update file
				logger.finest("Update file doesn't exist at update");
				state.resetUpdateData();
				state.saveToStorage();
				updaterHSM.cancel();
				throw new Error("Missing update file  at install time", Constants.ERROR_APPLICATION_UPDATE_NO_FILE);
				return;
			}
			try
			{
				state.descriptor.updaterLaunched = true;
				state.saveToStorage();
				state.saveToDocuments();
				var updater:Updater = new Updater();
				updater.update(updateFile, updaterHSM.descriptor.version);
			}catch(e:Error)
			{
				logger.warning("The application cannot be updated (url)." + e.message);
				state.resetUpdateData();
				state.saveToStorage();
				updaterHSM.cancel();
				throw new Error("Cannot update (from remote)", Constants.ERROR_APPLICATION_UPDATE);
			}
		}
		
		protected function onFileInstall():void
		{
			var updateFile:File = updaterHSM.airFile;
			if (!updateFile.exists)
			{
				logger.finest("Update file doesn't exist at update");
				state.resetUpdateData();
				state.saveToStorage();
				updaterHSM.cancel();
				throw new Error("Missing update file at install time", Constants.ERROR_APPLICATION_UPDATE_NO_FILE);				
				return;
			}
			try
			{
				state.descriptor.updaterLaunched = true;
				state.saveToStorage();
				state.saveToDocuments();
				
				var updater:Updater = new Updater();
				updater.update(updateFile, updaterHSM.applicationDescriptor.version);
			}catch(e:Error)
			{
				logger.warning("The application cannot be updated (file)." + e.message);
				state.resetUpdateData();
				state.saveToStorage();
				updaterHSM.cancel();
				throw new Error("Cannot update (from file)", Constants.ERROR_APPLICATION_UPDATE); 
			}
		}
		
		 
	}
}