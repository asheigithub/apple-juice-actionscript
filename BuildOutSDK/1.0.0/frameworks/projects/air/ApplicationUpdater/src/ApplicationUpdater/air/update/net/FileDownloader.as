/*
ADOBE SYSTEMS INCORPORATED
Copyright 2008 Adobe Systems Incorporated. All Rights Reserved.
 
NOTICE:   Adobe permits you to modify and distribute this file only in accordance with
the terms of Adobe AIR SDK license agreement.  You may have received this file from a
source other than Adobe.  Nonetheless, you may modify or distribute this file only in 
accordance with such agreement.
*/

package air.update.net
{
	import air.update.events.DownloadErrorEvent;
	import air.update.events.UpdateEvent;
	import air.update.logging.Logger;
	import air.update.utils.Constants;
	import air.update.utils.NetUtils;
	
	import flash.errors.EOFError;
	import flash.errors.IOError;
	import flash.events.ErrorEvent;
	import flash.events.Event;
	import flash.events.EventDispatcher;
	import flash.events.HTTPStatusEvent;
	import flash.events.IOErrorEvent;
	import flash.events.ProgressEvent;
	import flash.events.SecurityErrorEvent;
	import flash.filesystem.File;
	import flash.filesystem.FileMode;
	import flash.filesystem.FileStream;
	import flash.net.URLRequest;
	import flash.net.URLStream;
	import flash.utils.ByteArray;


	/**
	 * Dispatched in case of error
	 * 
	 * @eventType air.update.events.DownloadErrorEvent
	 */
	[Event(name="downloadError", type="air.update.events.DownloadErrorEvent")]
	/**
	 * Dispatched during downloading 
	 * 
	 * @eventType flash.events.ProgressEvent.PROGRESS
	 */
	[Event(name="progress", type="flash.events.ProgressEvent")]
	/**
	 * Dispatched when download has finished. Typical usage is to close the downloading window
	 * 
	 * @eventType air.update.events.UpdateEvent.DOWNLOAD_COMPLETE
	 */
	[Event(name="downloadComplete", type="air.update.events.UpdateEvent")]	

	[Event(name="downloadStart", type="air.update.events.UpdateEvent")]

	[ExcludeClass]
	public class FileDownloader extends EventDispatcher
	{
		private static var logger:Logger = Logger.getLogger("air.update.net.FileDownloader");
		
		private var fileURL:URLRequest;
		private var downloadedFile:File;
		//
		private var urlStream:URLStream;
		private var fileStream:FileStream;
		private var isInError:Boolean;
		
		public function FileDownloader(url:URLRequest, file:File):void
		{
			fileURL = url;
			fileURL.useCache = false;
			downloadedFile = file;
			logger.finer("url: " + url.url + " file: " + file.nativePath);
			//
			urlStream = new URLStream();
			urlStream.addEventListener(IOErrorEvent.IO_ERROR, onDownloadError);
			urlStream.addEventListener(SecurityErrorEvent.SECURITY_ERROR, onDownloadError);
			urlStream.addEventListener(ProgressEvent.PROGRESS, onDownloadProgress);
			urlStream.addEventListener(Event.OPEN, onDownloadOpen);
			urlStream.addEventListener(Event.COMPLETE, onDownloadComplete);
			urlStream.addEventListener(HTTPStatusEvent.HTTP_RESPONSE_STATUS, onDownloadResponseStatus);			
		}
		
		public function download():void
		{
			urlStream.load(fileURL);
		}
		
		public function cancel():void
		{
			try
			{
				if (urlStream && urlStream.connected)
				{
					urlStream.close();
				}
				if (fileStream)
				{
					fileStream.close();
					fileStream = null;
				}
				if (downloadedFile && downloadedFile.exists) {
					downloadedFile.deleteFile();
				}
			}catch(e:Error) {
				logger.fine("Error during canceling the download: " + e); 
			}
		}
		
		public function inProgress():Boolean
		{
			return urlStream.connected; 
		}
		
		private function onDownloadResponseStatus(event:HTTPStatusEvent):void
		{
			logger.fine("DownloadStatus: " + event.status);
			if (!NetUtils.isHTTPStatusAcceptable(event.status))
			{
				dispatchErrorEvent("Invalid HTTP status code: " + event.status, Constants.ERROR_INVALID_HTTP_STATUS, event.status);
			}
		}
		
		private function dispatchErrorEvent(eventText:String, errorID:int = 0, subErrorID:int = 0):void
		{
			isInError = true;
			if (urlStream && urlStream.connected)
			{
				urlStream.close();
				urlStream = null;
			}
			dispatchEvent(new DownloadErrorEvent(DownloadErrorEvent.DOWNLOAD_ERROR, false, true, eventText, errorID, subErrorID));	
		}
		
		private function onDownloadError(event:ErrorEvent):void
		{
			if (event is IOErrorEvent)
			{
				dispatchErrorEvent(event.text, Constants.ERROR_IO_DOWNLOAD, event.errorID);	
			} else if (event is SecurityErrorEvent)
			{
				dispatchErrorEvent(event.text, Constants.ERROR_SECURITY, event.errorID);
			}
		}
		
		private function onDownloadOpen(event:Event):void
		{
			fileStream = new FileStream();
			try
			{			
				fileStream.open(downloadedFile, FileMode.WRITE);
			}catch(e:Error)
			{
				logger.fine("Error opening file on disk: " + e);
				isInError = true;
				dispatchErrorEvent(e.message, Constants.ERROR_IO_FILE, e.errorID);
				return;
			}
			dispatchEvent(new UpdateEvent(UpdateEvent.DOWNLOAD_START, false, false));
		}
		
		private function saveBytes():void	
		{
			if (!fileStream || !urlStream || !urlStream.connected)
				return;
			
			try
			{
				var bytes:ByteArray = new ByteArray();
				urlStream.readBytes(bytes, 0, urlStream.bytesAvailable);
				fileStream.writeBytes(bytes);
			}catch(error:EOFError) {
				isInError = true;
				logger.fine("EOFError: " + error);
				dispatchErrorEvent(error.message, Constants.ERROR_EOF_DOWNLOAD, error.errorID);
			}
			catch(err:IOError) {
				isInError = true;
				logger.fine("IOError: " + err);
				dispatchErrorEvent(err.message, Constants.ERROR_IO_FILE, err.errorID);
			}			
		}
		
		private function onDownloadProgress(event:ProgressEvent):void
		{
			if (!isInError)
			{
				saveBytes();
				dispatchEvent(event);
			}
		}
		
		private function onDownloadComplete(event:Event):void
		{
			// empty the buffer
			while (urlStream && urlStream.bytesAvailable)
			{
				saveBytes();
			}
			if (urlStream && urlStream.connected)
			{
				urlStream.close();
				urlStream = null;
			}
			fileStream.close();
			fileStream = null;
			
			if (!isInError) dispatchEvent(new UpdateEvent(UpdateEvent.DOWNLOAD_COMPLETE));
		}
		
		
	}
}