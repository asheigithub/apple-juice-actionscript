/*
ADOBE SYSTEMS INCORPORATED
 Copyright 2008 Adobe Systems Incorporated
 All Rights Reserved.

NOTICE:  Adobe permits you to use, modify, and distribute this file in accordance with the 
terms of the Adobe license agreement accompanying it.  If you have received this file from a 
source other than Adobe, then your use, modification, or distribution of it requires the prior 
written permission of Adobe.
*/

package com.adobe.utils
{
	internal final class LocaleParserState
	{
		public static const PRIMARY_LANGUAGE:int = 0;
		public static const EXTENDED_LANGUAGES:int = 1;
		public static const SCRIPT:int = 2;
		public static const REGION:int = 3;
		public static const VARIANTS:int = 5;  
		public static const EXTENSIONS:int = 6;
		public static const PRIVATES:int = 7;

	}
}