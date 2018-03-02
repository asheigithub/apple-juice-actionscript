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
import mx.effects.easing.Back;
import mx.controls.scrollClasses.ScrollBar;

/**
 *  The skin for all the states of the arrow in a scrollbar.
 *       
 */
public class SimpleScrollArrowSkin extends Border
{

	//private var thumbIcon : DisplayObject;
	
	//--------------------------------------------------------------------------
	//
	//  Constructor
	//
	//--------------------------------------------------------------------------

	/**
	 *  Constructor.
	 */
	public function SimpleScrollArrowSkin()
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
     */    
    override public function get measuredWidth():Number
    {
        return ScrollBar.THICKNESS;
    }
    
    //----------------------------------
	//  measuredHeight
    //----------------------------------
    
    /**
     *  @private
     */        
    override public function get measuredHeight():Number
    {
        return ScrollBar.THICKNESS;
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
		var borderColor:uint = getStyle("arrowBorderColor");
		var fillColor:uint = getStyle("arrowFillColor");
		var overFillColor:uint = getStyle("arrowOverFillColor");
		var arrowColor:uint = getStyle("arrowIconColor");
		var upArrow:Boolean = (name.charAt(0) == 'u');
		
		graphics.clear();
		
		switch (name)
		{
			case "upArrowUpSkin":
			case "downArrowUpSkin":
			{
				drawRoundRect( 1, 1, w - 1, h - 1, 0, borderColor, 1 );
				drawRoundRect( 2, 2, w - 3, h - 3, 0, fillColor, 1 );
				
				break;
			}
			
			case "upArrowOverSkin":
			case "upArrowDownSkin":
			case "downArrowDownSkin":
			case "downArrowOverSkin":
			{
				drawRoundRect( 1, 1, w - 1, h - 1, 0, borderColor, 1 );
				drawRoundRect( 2, 2, w - 3, h - 3, 0, overFillColor, 1 );
				break;
			}
			
			default:
			{
				drawRoundRect(
					0, 0, w, h, 0,
					fillColor, 0);
				
				return;
				break;
			}
		}
	
		// Draw up or down arrow
		graphics.beginFill(arrowColor);
		if (upArrow)
		{
			graphics.moveTo(w / 2, 6);
			graphics.lineTo(w - 5, h - 6);
			graphics.lineTo(5, h - 6);
			graphics.lineTo(w / 2, 6);
		}
		else
		{
			graphics.moveTo(w / 2, h - 6);
			graphics.lineTo(w - 5, 6);
			graphics.lineTo(5, 6);
			graphics.lineTo(w / 2, h - 6);
		}
		graphics.endFill();
	}
}
}