/*
ADOBE SYSTEMS INCORPORATED
 Copyright 2008 Adobe Systems Incorporated
 All Rights Reserved.

NOTICE:  Adobe permits you to use, modify, and distribute this file in accordance with the 
terms of the Adobe license agreement accompanying it.  If you have received this file from a 
source other than Adobe, then your use, modification, or distribution of it requires the prior 
written permission of Adobe.
*/

package components
{
	import flash.display.DisplayObject;
	import flash.system.Capabilities;
	
	import mx.core.mx_internal;
	
	use namespace mx_internal;

	/**
	 * This class extends the HBox to conditionally layout its 
	 * components in reverse order depending on operating system. 
	 * 
	 * This allows for dialog action buttons (e.g. 'OK', 'Cancel')
	 * to be reversed. 
	 * 
	 * NOTE: The specified layout is assumed to be Windows-based,
	 * so it will be reversed for OSX. 
	 */ 
	public class OSButtonBarHBox extends ApplicationUpdaterHBox
	{
       override public function getChildAt(index:int):DisplayObject
       {
       	 	if ( Capabilities.os.indexOf("Mac") >= 0 )
       	 	{
       	 		return super.getChildAt( numChildren - (index + 1) );
       	 	}
       	 	else
       	 	{
       	 		return super.getChildAt( index );
       	 	}
       }
 	}
}		
