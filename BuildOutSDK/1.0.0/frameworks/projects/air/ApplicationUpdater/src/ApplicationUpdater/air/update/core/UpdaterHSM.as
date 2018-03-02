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
	import air.update.descriptors.ApplicationDescriptor;
	import air.update.descriptors.UpdateDescriptor;
	import air.update.events.DownloadErrorEvent;
	import air.update.events.StatusFileUpdateErrorEvent;
	import air.update.events.StatusFileUpdateEvent;
	import air.update.events.StatusUpdateErrorEvent;
	import air.update.events.StatusUpdateEvent;
	import air.update.events.UpdateEvent;
	import air.update.logging.Logger;
	import air.update.net.FileDownloader;
	import air.update.states.HSM;
	import air.update.states.HSMEvent;
	import air.update.states.UpdateState;
	import air.update.utils.Constants;
	import air.update.utils.FileUtils;
	import air.update.utils.VersionUtils;
	
	import flash.events.ErrorEvent;
	import flash.events.Event;
	import flash.events.IOErrorEvent;
	import flash.events.ProgressEvent;
	import flash.filesystem.File;
	import flash.net.URLRequest;


	[Event(name="checkForUpdate", type="air.update.events.UpdateEvent")]
	
	[Event(name="updateStatus", type="air.update.events.StatusUpdateEvent")]
	
	[Event(name="updateError", type="air.update.events.StatusUpdateErrorEvent")]
	
	[Event(name="downloadStart", type="air.update.events.UpdateEvent")]	

	/**
	 * Dispatched during downloading 
	 * 
	 * @eventType flash.events.ProgressEvent.PROGRESS
	 */
	[Event(name="progress", type="flash.events.ProgressEvent")]
	
	/**
	 * Dispatched in case of error
	 * 
	 * @eventType air.update.events.DownloadErrorEvent
	 */
	[Event(name="downloadError", type="air.update.events.DownloadErrorEvent")]

	/**
	 * Dispatched when download has finished. Typical usage is to close the downloading window
	 * 
	 * @eventType air.update.events.UpdateEvent.DOWNLOAD_COMPLETE
	 */
	[Event(name="downloadComplete", type="air.update.events.UpdateEvent")]
	
	[Event(name="beforeInstall", type="air.update.events.UpdateEvent")]
	
	[ExcludeClass]
	public class UpdaterHSM extends HSM
	{
		private static var logger:Logger = Logger.getLogger("air.update.core.UpdaterHSM");
		
		public static const EVENT_CHECK:String = "updater.check";
		public static const EVENT_DOWNLOAD:String = "updater.download";
		public static const EVENT_INSTALL:String = "updater.install";
		//
		public static const EVENT_INSTALL_TRIGGER:String = "install.trigger";
		public static const EVENT_FILE_INSTALL_TRIGGER:String = "file_install.trigger";
		public static const EVENT_STATE_CLEAR_TRIGGER:String = "state_clear.trigger";
		//
		public static const EVENT_ASYNC:String = "check.async";
		public static const EVENT_FILE:String = "check.file";
		public static const EVENT_VERIFIED:String = "check.verified";
		
		private var downloader:FileDownloader;
		//
		private var descriptorURL:URLRequest; 
		private var descriptorFile:File;
		//
		private var updateURL:URLRequest; 
		private var updateFile:File;
		//
		private var _descriptor:UpdateDescriptor;
		private var _applicationDescriptor:ApplicationDescriptor;
		//
		private var requestedURL:String;
		//
		private var requestedFile:File;
		private var unpackager:AIRUnpackager;
		//
		private var _configuration:UpdaterConfiguration;
		//
		private var lastErrorEvent:ErrorEvent;

		
		public function UpdaterHSM()
		{
			super(stateReady);
		}
		
		public function get configuration():UpdaterConfiguration
		{
			return _configuration;
		}
		
		public function set configuration(value:UpdaterConfiguration):void
		{
			_configuration = value;
		}
		
		private function isNewerVersion(oldVersion:String, newVersion:String):Boolean
		{
			if (configuration) return configuration.isNewerVersionFunction(oldVersion, newVersion);
			return VersionUtils.isNewerVersion(oldVersion, newVersion);
		}
		
		public function checkAsync(url:String):void
		{
			requestedURL = url;
			dispatch(new Event(EVENT_ASYNC));
		}
		
		public function installFile(file:File):void
		{
			requestedFile = file;
			dispatch(new Event(EVENT_FILE));
		}
		
		public function cancel():void
		{
			transition(stateCancelled);
		}
		
		public function getUpdateState():int
		{
			var updateState:int = -1;
			switch(stateHSM)
			{
				case stateInitialized:
					updateState = UpdateState.READY;
					break;
				case stateBeforeChecking:
					updateState = UpdateState.BEFORE_CHECKING;
					break;
				case stateChecking:
					updateState = UpdateState.CHECKING;
					break;
				case stateAvailable:
				case stateAvailableFile:
					updateState = UpdateState.AVAILABLE;
					break;
				case stateDownloading:
					updateState = UpdateState.DOWNLOADING;
					break;
				case stateDownloaded:
					updateState = UpdateState.DOWNLOADED;
					break;
				case stateInstalling:
					updateState = UpdateState.INSTALLING;
					break;
				case statePendingInstall:
					updateState = UpdateState.PENDING_INSTALLING;
					break;
				case stateReady:
					updateState = UpdateState.READY;
					break;
			}
			return updateState;
		}

		public function get descriptor():UpdateDescriptor
		{
			return _descriptor;
		}
		
		public function get applicationDescriptor():ApplicationDescriptor
		{
			return _applicationDescriptor;
		}
		
		public function get airFile():File
		{
			return requestedFile;
		}
		
		protected function stateInitialized(event:Event):void
		{
			logger.finest("stateInitialized: " + event.type);
			switch(event.type)
			{
				case HSMEvent.ENTER:
					transition(stateReady);
					break;
			}
		}
		
		
		protected function stateBeforeChecking(event:Event):void
		{
			logger.finest("stateBeforeChecking: " + event.type);
			switch(event.type)
			{
				case HSMEvent.ENTER:
					//
					var e:UpdateEvent = new UpdateEvent(UpdateEvent.CHECK_FOR_UPDATE, false, true);
					dispatchEvent(e);
					if (!e.isDefaultPrevented())
					{
						// if the event wasn't cancelled, start downloading
						transition(stateChecking);
						return;
					}
					logger.finer("CheckForUpdate cancelled");
					// event was cancelled, wait for the next event
					break;
				case EVENT_CHECK:
					transition(stateChecking);
					break;
			}
		}
		
		protected function stateChecking(event:Event):void
		{
			logger.finest("stateChecking: " + event.type);
			switch(event.type)
			{
				case HSMEvent.ENTER:
					downloader = new FileDownloader(updateURL, updateFile);
					downloader.addEventListener(DownloadErrorEvent.DOWNLOAD_ERROR, dispatch);
					downloader.addEventListener(ProgressEvent.PROGRESS, dispatch);
					downloader.addEventListener(UpdateEvent.DOWNLOAD_COMPLETE, dispatch);
					downloader.download();
					break;
				case UpdateEvent.DOWNLOAD_START:
					// Not interested in download start, when downloading descriptor
					break;					
				case ProgressEvent.PROGRESS:
					// Not interested in progress events, while downloading descriptor
					break;
				case DownloadErrorEvent.DOWNLOAD_ERROR:
					//
					lastErrorEvent = new StatusUpdateErrorEvent(StatusUpdateErrorEvent.UPDATE_ERROR, false, true, DownloadErrorEvent(event).text, DownloadErrorEvent(event).errorID, DownloadErrorEvent(event).subErrorID);
					transition(stateErrored);
					break;
				case UpdateEvent.DOWNLOAD_COMPLETE:
					downloader = null;
					// update descriptor is downloaded
					// read it
					descriptorDownloaded();
					break;
			}
		}
		
		protected function stateAvailable(event:Event):void
		{
			logger.finest("stateAvailable: " + event.type);
			switch (event.type)
			{
				case HSMEvent.ENTER:
					if (dispatchEvent(new StatusUpdateEvent(StatusUpdateEvent.UPDATE_STATUS, false, true, true, descriptor.version, descriptor.description, descriptor.versionLabel)))
					{
						// if the event wasn't cancelled, start downloading
						transition(stateDownloading);
						return;
					}
					// event was cancelled, wait for the next event
					break;
				case EVENT_DOWNLOAD:
					transition(stateDownloading);
					break;					
			}				
		}
		
		protected function stateDownloading(event:Event):void
		{
			if (event.type != ProgressEvent.PROGRESS) logger.finest("stateDownloading: " + event.type);
			switch (event.type)
			{
				case HSMEvent.ENTER:
					downloader = new FileDownloader(updateURL, updateFile);
					downloader.addEventListener(DownloadErrorEvent.DOWNLOAD_ERROR, dispatch);
					downloader.addEventListener(UpdateEvent.DOWNLOAD_START, dispatch);
					downloader.addEventListener(ProgressEvent.PROGRESS, dispatch);
					downloader.addEventListener(UpdateEvent.DOWNLOAD_COMPLETE, dispatch);
					downloader.download();
					break;
				case UpdateEvent.DOWNLOAD_START:
					dispatchEvent(event.clone());
					break;				
				case ProgressEvent.PROGRESS:
					dispatchEvent(event.clone());
					break;
				case DownloadErrorEvent.DOWNLOAD_ERROR:
					lastErrorEvent = ErrorEvent(event.clone());
					transition(stateErrored);
					break;
				case UpdateEvent.DOWNLOAD_COMPLETE:
					downloader = null;
					transition(stateDownloaded);
					break;					
			}		
		}
		
		protected function stateDownloaded(event:Event):void
		{
			logger.finest("stateDownloaded: " + event.type);
			switch (event.type)
			{
				case HSMEvent.ENTER:
					unpackager = new AIRUnpackager();
					//unpackager.addEventListener(ProgressEvent.PROGRESS, dispatch);
					unpackager.addEventListener(Event.COMPLETE, dispatch);
					unpackager.addEventListener(ErrorEvent.ERROR, dispatch);
					unpackager.addEventListener(IOErrorEvent.IO_ERROR, dispatch);
					unpackager.unpackageAsync(updateFile.url);
					break;
					
				case ErrorEvent.ERROR:
				case IOErrorEvent.IO_ERROR:
					unpackager.cancel();
					unpackager = null;
					lastErrorEvent = new DownloadErrorEvent(DownloadErrorEvent.DOWNLOAD_ERROR, false, true, "", Constants.ERROR_AIR_UNPACKAGING, ErrorEvent(event).errorID);
					transition(stateErrored);
					break;
					
				case Event.COMPLETE:
					unpackager.cancel();
					var descriptor:ApplicationDescriptor = new ApplicationDescriptor(unpackager.descriptorXML);
					try
					{
						descriptor.validate();
					}catch(e:Error)
					{
						unpackager = null;
						lastErrorEvent = new DownloadErrorEvent(DownloadErrorEvent.DOWNLOAD_ERROR, false, true, e.message, Constants.ERROR_VALIDATE, e.errorID);
						transition(stateErrored);
						return;
					}
					if (descriptor.id != VersionUtils.getApplicationID())
					{
						lastErrorEvent = new DownloadErrorEvent(DownloadErrorEvent.DOWNLOAD_ERROR, false, true, "Different applicationID", Constants.ERROR_VALIDATE,Constants.ERROR_DIFFERENT_APPLICATION_ID);
						transition(stateErrored);
						return;
					}
					if (_descriptor.version != descriptor.version)
					{
						lastErrorEvent = new DownloadErrorEvent(DownloadErrorEvent.DOWNLOAD_ERROR, false, true, "Version mismatch", Constants.ERROR_VALIDATE, Constants.ERROR_VERSION_MISMATCH);
						transition(stateErrored);
						return;
					}
					if (!isNewerVersion(VersionUtils.getApplicationVersion(), descriptor.version))
					{
						lastErrorEvent = new DownloadErrorEvent(DownloadErrorEvent.DOWNLOAD_ERROR, false, true, "Not a newer version", Constants.ERROR_VALIDATE, Constants.ERROR_NOT_NEW_VERSION);
						transition(stateErrored);
						return;
					}
					dispatch(new Event(EVENT_VERIFIED));
					break;
					
				case EVENT_VERIFIED:
					if (dispatchEvent(new UpdateEvent(UpdateEvent.DOWNLOAD_COMPLETE, false, true)))
					{
						// if the event wasn't cancelled, start downloading
						transition(stateInstalling);
						return;
					}
					// event was cancelled, wait for the next event
					break;
				case EVENT_INSTALL:
					transition(stateInstalling);
					break;					
			}			
		}
		
		
		protected function stateInstalling(event:Event):void
		{
			logger.finest("stateInstalling: " + event.type);
			switch (event.type)
			{
				case HSMEvent.ENTER:
					if (!dispatchEvent(new UpdateEvent(UpdateEvent.BEFORE_INSTALL, false, true)))
					{
						// if the event was  cancelled, wait restart
						transition(statePendingInstall);
						return;
					}
					installUpdate();
					break;
			}
		}
		
		/**
		 *  Terminal state
		 */
		protected function statePendingInstall(event:Event):void
		{
			logger.finest("statePendingInstall: " + event.type);
			switch (event.type)
			{
				case HSMEvent.ENTER:
					return;
			}
		}
		
		protected function stateUnpackaging(event:Event):void
		{
			logger.finest("stateUnpackaging: " + event.type);
			switch (event.type)
			{
				case HSMEvent.ENTER:
					unpackager = new AIRUnpackager();
					//unpackager.addEventListener(ProgressEvent.PROGRESS, dispatch);
					unpackager.addEventListener(Event.COMPLETE, dispatch);
					unpackager.addEventListener(ErrorEvent.ERROR, dispatch);
					unpackager.addEventListener(IOErrorEvent.IO_ERROR, dispatch);
					unpackager.unpackageAsync(requestedFile.url);
					break;
				case Event.COMPLETE:
					unpackager.cancel();				
					fileUnpackaged();
					unpackager = null;
					break;
				case ErrorEvent.ERROR:
				case IOErrorEvent.IO_ERROR:
					unpackager.cancel();
					unpackager = null;				
				// TODO: make a new event type?
					lastErrorEvent = new StatusFileUpdateErrorEvent(StatusFileUpdateErrorEvent.FILE_UPDATE_ERROR, false, true, "", (ErrorEvent(event).errorID));
					transition(stateErrored);
					break;
			}			
		}
		
		protected function stateAvailableFile(event:Event):void
		{
			logger.finest("stateAvailableFile: " + event.type + " file: " + requestedFile.url);
			switch (event.type)
			{
				case HSMEvent.ENTER:
					if (dispatchEvent(new StatusFileUpdateEvent(StatusFileUpdateEvent.FILE_UPDATE_STATUS, false, true, true, _applicationDescriptor.version, requestedFile.nativePath, _applicationDescriptor.versionLabel)))
					{
						// if the event wasn't cancelled, start downloading
						transition(stateInstallingFile);
						return;
					}
					// event was cancelled, wait for the next event
					break;
				case EVENT_INSTALL:
					transition(stateInstallingFile);
					break;			
			}	
		}	
		
		protected function stateInstallingFile(event:Event):void
		{
			logger.finest("stateInstallingFile: " + event.type);
			switch (event.type)
			{
				case HSMEvent.ENTER:
					installFileUpdate();
					break;
			}
		}
		
		protected function stateReady(event:Event):void
		{
			logger.finest("stateReady: " + event.type);
			switch (event.type)
			{
				case HSMEvent.ENTER:
					break;
				case EVENT_ASYNC:
					updateURL = new URLRequest(requestedURL);
					updateFile = FileUtils.getLocalDescriptorFile();
					transitionAsync(stateBeforeChecking);
					break;
				case EVENT_FILE:
					transitionAsync(stateUnpackaging);
					break;
					
			}
		}
		
		protected function stateCancelled(event:Event):void
		{
			logger.finest("stateCancelled: " + event.type);
			switch (event.type)
			{
				case HSMEvent.ENTER:
					dispatchEvent(new Event(EVENT_STATE_CLEAR_TRIGGER));
					// clear everything and transition to ready
					if (downloader) 
					{
						downloader.cancel();
						downloader = null;	
					}
					transition(stateReady);
					break;
			}
		}
		
		protected function stateErrored(event:Event):void
		{
			logger.finest("stateErrored: " + event.type + " lastErrorEvent: " + lastErrorEvent);
			switch (event.type)
			{
				case HSMEvent.ENTER:
					var isDialogHidden:Boolean = false;
					if (lastErrorEvent)
					{
						isDialogHidden = dispatchEvent(lastErrorEvent);
						lastErrorEvent = null;
					}
					
					dispatchEvent(new Event(EVENT_STATE_CLEAR_TRIGGER));
					// clear everything and transition to ready
					if (downloader) 
					{
						downloader.cancel();
						downloader = null;
					}

					if (isDialogHidden)
					{
						transition(stateReady);	
					}

					break;
			}
		}
		
//---------------------------------------------------

		private function fileUnpackaged():void
		{
			logger.finer("Parsing file descriptor");
			try
			{
				var xml:XML = unpackager.descriptorXML;
				_applicationDescriptor = new ApplicationDescriptor(xml);
				_applicationDescriptor.validate();
				// diferent applicationID
				if (VersionUtils.getApplicationID() != _applicationDescriptor.id)
				{
					throw new Error("Different applicationID", Constants.ERROR_DIFFERENT_APPLICATION_ID);
				}
				if (!isNewerVersion(VersionUtils.getApplicationVersion(), _applicationDescriptor.version))
				{
					if(dispatchEvent(new StatusFileUpdateEvent(StatusFileUpdateEvent.FILE_UPDATE_STATUS, false, true, false, _applicationDescriptor.version, requestedFile.nativePath, _applicationDescriptor.versionLabel)))
					{
						transition(stateReady);
					}

					return;
				}
				transition(stateAvailableFile);
			}
			catch(e:Error)
			{
				logger.fine("Error validating file descriptor: " + e);
				lastErrorEvent = new StatusFileUpdateErrorEvent(StatusFileUpdateErrorEvent.FILE_UPDATE_ERROR, false, true, e.message, e.errorID);
				transition(stateErrored);
			}
		}

		private function descriptorDownloaded():void
		{
			logger.finer("Parsing descriptor");
			try 
			{
				var xml:XML = FileUtils.readXMLFromFile(updateFile);
				_descriptor = new UpdateDescriptor(xml);
				_descriptor.validate();
				if(!_descriptor.isCompatibleWithApplicationDescriptor(VersionUtils.applicationHasVersionNumber()))
					throw new Error("Application namespace and update descriptor namespace are not compatible", Constants.ERROR_INCOMPATIBLE_NAMESPACE);
				//
				if (!isNewerVersion(VersionUtils.getApplicationVersion(), _descriptor.version))
				{
					if (dispatchEvent(new StatusUpdateEvent(StatusUpdateEvent.UPDATE_STATUS, false, true)))
					{
						transition(stateReady);
					}

					return;
				}
				// update is available
				updateFile = FileUtils.getLocalUpdateFile();
				updateURL = new URLRequest(descriptor.url);
				//
				transition(stateAvailable);
			}catch(e:Error)
			{
				logger.fine("Error loading/validating downloaded descriptor: " + e);
				lastErrorEvent = new StatusUpdateErrorEvent(StatusUpdateErrorEvent.UPDATE_ERROR, false, false, e.message, e.errorID);
				transition(stateErrored);
			}
		}
		
		private function installUpdate():void
		{
			logger.finest("Installing update");
			dispatchEvent(new Event(EVENT_INSTALL_TRIGGER));
		}
		
		private function installFileUpdate():void
		{
			logger.finest("Installing file update");
			dispatchEvent(new Event(EVENT_FILE_INSTALL_TRIGGER));
		}
		
		
		private function startTimer(delay:Number = -1):void
		{
			
		}
	}
}
