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

import flash.display.GradientType;
import mx.skins.Border;
import mx.styles.StyleManager;
import mx.utils.ColorUtil;
import flash.display.DisplayObject;
import mx.core.UIComponent;

/**
 *  The skin for all the states of the thumb in a ScrollBar.
 * 
 * NOTE: In order to match the visual design, we 
 *       
 */
public class SimpleScrollThumbSkin extends UIComponent
{

	private var thumbIcon : DisplayObject;
	
	//--------------------------------------------------------------------------
	//
	//  Constructor
	//
	//--------------------------------------------------------------------------

	/**
	 *  Constructor.
	 */
	public function SimpleScrollThumbSkin()
	{
		super();
	}

	//--------------------------------------------------------------------------
	//
	//  Overridden properties
	//
	//--------------------------------------------------------------------------

	 /**
     *  @private
     *  Create child objects.
     */
    override protected function createChildren():void
    {
        // Create the thumb icon
        if (!thumbIcon)
        {
            var thumbIconClass:Class = getStyle("scrollThumbIcon");
            thumbIcon = new thumbIconClass();
            addChild(thumbIcon);
        }
    }


	//----------------------------------
	//  measuredWidth
	//----------------------------------
	
	/**
	 *  @private
	 */
	override public function get measuredWidth():Number
	{
		return 16;
	}
	
	//----------------------------------
	//  measuredHeight
	//----------------------------------

	/**
	 *  @private
	 */
	override public function get measuredHeight():Number
	{
		return 10;
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
		
		// User-defined styles.
		var borderColor:uint = getStyle("thumbBorderColor");
		var fillColor:uint = getStyle("thumbFillColor");
		var overFillColor:uint = getStyle("thumbOverFillColor");
		
		
		graphics.clear();
		
		switch (name)
		{
			default:
			case "thumbUpSkin":
			{
				drawRoundRect( 1, 1, w - 1, h - 1, 0, borderColor, 1 );
				drawRoundRect( 2, 2, w - 3, h - 3, 0, fillColor, 1 );
				
				break;
			}
			
			case "thumbOverSkin":
			case "thumbDownSkin":
			{
				drawRoundRect( 1, 1, w - 1, h - 1, 0, borderColor, 1 );
				drawRoundRect( 2, 2, w - 3, h - 3, 0, overFillColor, 1 );
				break;
			}
			
		}
	
		thumbIcon.x = ( w - thumbIcon.width ) / 2;
		thumbIcon.y = ( h - thumbIcon.height ) / 2;
	}
}
}