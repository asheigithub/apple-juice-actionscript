/*
ADOBE SYSTEMS INCORPORATED
Copyright 2008 Adobe Systems Incorporated. All Rights Reserved.
 
NOTICE:   Adobe permits you to modify and distribute this file only in accordance with
the terms of Adobe AIR SDK license agreement.  You may have received this file from a
source other than Adobe.  Nonetheless, you may modify or distribute this file only in 
accordance with such agreement.
*/

package air.update.events
{
	import flash.events.ErrorEvent;
	import flash.events.Event;

	/**
	 * A DownloadErrorEvent is dispatched when an error happens while downloading
	 * the update file.
	 */
	public class DownloadErrorEvent extends ErrorEvent
	{
		/**
		 * The <code>DownloadErrorEvent.DOWNLOAD_ERROR</code> constant defines the value of the  
		 * <code>type</code> property of the event object for a <code>downloadError</code> event.
		 */		
		public static const DOWNLOAD_ERROR:String="downloadError";
		
		/**
		 * Creates an DownloadError object to pass to event listeners 
		 */
		public function DownloadErrorEvent(type:String, bubbles:Boolean = false, cancelable:Boolean = false, text:String = "", id:int = 0, subErrorID:int = 0)
		{
			super(type, bubbles, cancelable, text, id);
			this.subErrorID = subErrorID;
		}
		
		/**
	 	 * @inheritDoc
	 	 */
		override public function clone():Event
		{
			return new DownloadErrorEvent(type, bubbles, cancelable, text, errorID, subErrorID);
		}
		
		/**
	 	 * @inheritDoc
	 	 */
		override public function toString():String
		{
			return "[DownloadErrorEvent (type=" + type + " text=" + text + " id=" + errorID + " subErrorID=" + subErrorID +  ")]";	
		}
		
		public var subErrorID:int = 0;
	}
}