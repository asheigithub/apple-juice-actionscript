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
	 * A StatusUpdateErrorEvent is dispatched when a call to the <code>checkForUpdate()</code> method of a ApplicationUpdater object encounters an error
	 * while downloading or parsing the update descriptor
	 */	
	public class StatusUpdateErrorEvent extends ErrorEvent
	{
		/**
		 * The <code>StatusUpdateErrorEvent.UPDATE_ERROR</code> constant defines the value of the  
		 * <code>type</code> property of the event object for a <code>statusUpdateError</code> event.
		 */			
		public static const UPDATE_ERROR:String = "updateError";
		
		public function StatusUpdateErrorEvent(type:String, bubbles:Boolean=false, cancelable:Boolean=false, text:String = "", id:int = 0, subErrorID:int = 0 )
		{
			super(type, bubbles, cancelable, text, id);
			this.subErrorID = subErrorID;
		}
		
		/**
	 	 * @inheritDoc
	 	 */
		override public function clone():Event
		{
			return new StatusUpdateErrorEvent(type, bubbles, cancelable, text, errorID, subErrorID);
		}
		
		/**
	 	 * @inheritDoc
	 	 */
		override public function toString():String
		{
			return "[StatusUpdateErrorEvent (type=" + type + " text=" + text + " id=" + errorID + " + subErrorID=" + subErrorID + ")]";	
		}	
		
		public var subErrorID:int = 0;

	}
}