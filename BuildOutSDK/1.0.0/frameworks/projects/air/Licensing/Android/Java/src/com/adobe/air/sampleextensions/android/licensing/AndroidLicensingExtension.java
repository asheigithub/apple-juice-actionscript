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


package com.adobe.air.sampleextensions.android.licensing;

import com.adobe.fre.FREContext;
import com.adobe.fre.FREExtension;

/*
 * Initialization and finalization class of native extension.
 */

public class AndroidLicensingExtension implements FREExtension
{
	/*
 	 * Extension initialization.
 	 */  
	public void initialize( )
	{

		//System.out.println("**** InitializeExtension:initialize");
	}
	public void finalize( )
	{
		//System.out.println("**** InitializeExtension:finalize");
	}
	public FREContext createContext( String extId )
	{
	
	/*
	 * Creates a new instance of AndroidLicensingExtensionContext when the context is created from the actionscript code
	 */
		
		//System.out.println("**** InitializeExtension:createContext");
		
		return new AndroidLicensingExtensionContext();
	}
	@Override
	public void dispose() {

	/*
	* Called if the extension is unloaded from the process. Extensions
	* are not guaranteed to be unloaded; the runtime process may exit without
	* doing so.
	*/

		//System.out.println("**** InitializeExtension:dispose");
		
	}
}
