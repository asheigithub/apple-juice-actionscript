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
	import mx.containers.FormItem;
	import mx.controls.Label;
	import flash.accessibility.*;

	public class ApplicationUpdaterFormItem extends FormItem
	{
		override protected function commitProperties():void
		{
			accessibilityProperties = new AccessibilityProperties();
			accessibilityProperties.name = label;
			// We merge the accessibilityPropties of the form item 
			// with those from its child label. Otherwise flex gets 
			// the tab order wrong
			if( numChildren > 0 )
			{ 
				try
				{
					var child : Label = getChildAt( 0 ) as Label;
					accessibilityProperties.name += " " + child.text;
					child.accessibilityProperties.silent = true;
				} 
				catch ( e : Error ) {}
			}
			super.commitProperties();
		}
		
	}
}
