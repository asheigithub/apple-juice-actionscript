/*
ADOBE SYSTEMS INCORPORATED
 Copyright 2008 Adobe Systems Incorporated
 All Rights Reserved.

NOTICE:  Adobe permits you to use, modify, and distribute this file in accordance with the 
terms of the Adobe license agreement accompanying it.  If you have received this file from a 
source other than Adobe, then your use, modification, or distribution of it requires the prior 
written permission of Adobe.
*/

package skins
{
	import mx.skins.Border;
	import mx.styles.StyleManager;

	public class SimpleProgressTrackSkin extends Border
	{
		public function SimpleProgressTrackSkin()
		{
			super();
		}
		
		//--------------------------------------------------------------------------
		//
		//  Overridden properties
		//
		//--------------------------------------------------------------------------
	
		//----------------------------------
		//  measuredWidth
		//----------------------------------
		
		/**
		 *  @private
		 */
		override public function get measuredWidth():Number
		{
			return 200;
		}
		
		//----------------------------------
		//  measuredHeight
		//----------------------------------
	
		/**
		 *  @private
		 */
		override public function get measuredHeight():Number
		{
			return 6;
		}
	
	    //--------------------------------------------------------------------------
	    //
	    //  Overridden methods
	    //
	    //--------------------------------------------------------------------------
			
		/**
		 *  @private
		 */
		override protected function updateDisplayList(w:Number, h:Number):void
		{
			super.updateDisplayList(w, h);
	
			// User-defined styles
			var borderColor:uint = getStyle("borderColor");
			var fillColors:Array = getStyle("trackColors") as Array;
			StyleManager.getColorNames(fillColors);
			
			// ProgressTrack-unique styles
			var borderColorDrk1:Number = borderColor;
				//ColorUtil.adjustBrightness2(borderColor, -30);
					
			graphics.clear();
			
			drawRoundRect(
				0, 0, w, h, 0, 
				[ borderColorDrk1, borderColor ], 1,
				verticalGradientMatrix(0, 0, w, h));
	
			drawRoundRect(
				1, 1, w - 2, h - 2, 0,
				fillColors, 1,
				verticalGradientMatrix(1, 1, w - 2, h - 2));
		}
	}		
}