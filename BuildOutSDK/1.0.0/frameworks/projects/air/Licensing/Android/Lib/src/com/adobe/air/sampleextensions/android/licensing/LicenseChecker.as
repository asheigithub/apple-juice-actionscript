/*********************************************************************************************************
* ADOBE SYSTEMS INCORPORATED
* Copyright 2011 Adobe Systems Incorporated
* All Rights Reserved.
*
* NOTICE:  Adobe permits you to use, modify, and distribute this file in accordance with the
* terms of the Adobe license agreement accompanying it.  If you have received this file from a
* source other than Adobe, then your use, modification, or distribution of it requires the prior
* written permission of Adobe.
*
*********************************************************************************************************/


package com.adobe.air.sampleextensions.android.licensing
{
	import flash.events.Event;
	import flash.events.EventDispatcher;
	import flash.events.StatusEvent;
	import flash.external.ExtensionContext;
	import flash.events.ErrorEvent;
	import flash.errors.IllegalOperationError;	

	/*
	 * LicenseChecker checks whether the applicaiton is licensed and returns appropriate event to the AIR application 
	 */
	
	public class LicenseChecker extends EventDispatcher
	{   
		private var extContext:ExtensionContext;
		
		// This value should be same as <extensionID> element in application descriptor
		private var extensionID:String = "com.adobe.air.sampleextensions.android.licensing"; 
		
		// Any string agreed between AS and Native Code. Its value is used to determine which derived class 
		// one wants to return from createContext(). This is not being used in the current implementation. 
		private var contextType:String = null; 
		
		// Add the Public key obtained from Android Market here 
		private static const BASE64_PUBLIC_KEY:String = "";
		
		/*
		*  This function creates an Extension Context and calls native code to start the licensing process.
		*  It also adds listener for status and error events, which are dispatched depending upon the
		*  result received from the native code. 
		*/
			
		public function checkLicense():void
		{
			var errorEvent:Event;
			try {
				extContext = ExtensionContext.createExtensionContext( extensionID, contextType );
				if ( extContext )
				{
					extContext.addEventListener(StatusEvent.STATUS, statusHandler);
					var retValue:int = extContext.call( "checkLicenseNative", BASE64_PUBLIC_KEY )as int;
					if(!retValue)
					{
						//trace(" Failed to call checkLicenseNative ");					
						extContext.removeEventListener(StatusEvent.STATUS, statusHandler);
						errorEvent = new ErrorEvent(ErrorEvent.ERROR, false, false, "Failed to check license", 0);
						dispatchEvent(errorEvent);
					}
				}
				else
				{
					//trace( " Failed to create ExtensionContext " );
					errorEvent = new ErrorEvent(ErrorEvent.ERROR, false, false, "Failed to check license", 0);
					dispatchEvent(errorEvent);
				}
			} catch (errA:ArgumentError) {
				trace(errA.message);
				errorEvent = new ErrorEvent(ErrorEvent.ERROR, false, false, errA.message , 0); 
				dispatchEvent(errorEvent);
			} catch (errIO:IllegalOperationError){
				trace(errIO.message);
				errorEvent = new ErrorEvent(ErrorEvent.ERROR, false, false, errIO.message , 0); 
				dispatchEvent(errorEvent);
			} catch (err:Error) {
				trace(err.message);
				errorEvent = new ErrorEvent(ErrorEvent.ERROR, false, false, err.message , 0); 
				dispatchEvent(errorEvent);
			}
		}
		
		/*
		 * This function creates the LicenseStatusEvent and dispatches it.  
		 * As a result, the handler listening to this event will be called.
		 */
		
		private function statusHandler(evt:StatusEvent):void 
		{
			if( !evt.level.length )
				evt.level = null;

			// Create LicenseStatusEvent and return it to the AIR application.
			var event:Event =new LicenseStatusEvent(LicenseStatusEvent.STATUS, evt.code , evt.level); 
			dispatchEvent(event);	
			extContext.removeEventListener(StatusEvent.STATUS, statusHandler);
		}
	}
}
	
