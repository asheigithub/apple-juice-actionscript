/*
ADOBE SYSTEMS INCORPORATED
Copyright 2008 Adobe Systems Incorporated. All Rights Reserved.
 
NOTICE:   Adobe permits you to modify and distribute this file only in accordance with
the terms of Adobe AIR SDK license agreement.  You may have received this file from a
source other than Adobe.  Nonetheless, you may modify or distribute this file only in 
accordance with such agreement.
*/

package air.update.utils
{
	[ExcludeClass]
	public class StringUtils
	{
		public static function isDigit(ch:String):Boolean
		{
			return ch >= '0' && ch <= '9';
		}
		
	}
}