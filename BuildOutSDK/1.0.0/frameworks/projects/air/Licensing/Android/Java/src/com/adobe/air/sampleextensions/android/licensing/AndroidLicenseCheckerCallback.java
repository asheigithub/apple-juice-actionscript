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
import com.google.android.vending.licensing.LicenseCheckerCallback;


/* 
 * LVL after checking with the licensing server and conferring with the policy makes callbacks to communicate  
 * result with the application using callbacks i.e AndroidLicenseCheckerCallback in this case.
 * AndroidLicenseCheckerCallback then dipatches StatusEventAsync event to communicate the result obtained from LVL
 * with the ActionScript library.
 */

public class AndroidLicenseCheckerCallback implements LicenseCheckerCallback{

	private static final String EMPTY_STRING = ""; 
	private static final String LICENSED = "licensed"; 
	private static final String NOT_LICENSED = "notLicensed";
	private static final String CHECK_IN_PROGRESS = "checkInProgress";
	private static final String INVALID_PACKAGE_NAME = "invalidPackageName";
	private static final String INVALID_PUBLIC_KEY = "invalidPublicKey";
	private static final String MISSING_PERMISSION = "missingPermission";
	private static final String NON_MATCHING_UID = "nonMatchingUID";
	private static final String NOT_MARKET_MANAGED = "notMarketManaged";
	
	private FREContext mFREContext;
	

	public AndroidLicenseCheckerCallback(FREContext freContext) {
		mFREContext = freContext;
	}

	@Override
	public void allow(int reason) {
		mFREContext.dispatchStatusEventAsync(LICENSED, EMPTY_STRING);
	}

    @Override
	public void dontAllow(int reason) {
		mFREContext.dispatchStatusEventAsync(NOT_LICENSED, EMPTY_STRING);
	}

	/*
 	 * This function maps the ApplicationErrorCode obtained from LVL to the LicenseStatusReason of ActionScript library.
 	 */ 

	@Override
	public void applicationError(int errorCode) {

		String errorMessage = EMPTY_STRING;

		switch(errorCode)
		{
			case ERROR_CHECK_IN_PROGRESS :
				errorMessage = CHECK_IN_PROGRESS;
				break;
			case ERROR_INVALID_PACKAGE_NAME :	
				errorMessage = INVALID_PACKAGE_NAME;
				break;
			case ERROR_INVALID_PUBLIC_KEY :
				errorMessage = INVALID_PUBLIC_KEY;
				break;
			case ERROR_MISSING_PERMISSION :
				errorMessage = MISSING_PERMISSION;
				break;
			case ERROR_NON_MATCHING_UID :
				errorMessage = NON_MATCHING_UID;
				break;
			case ERROR_NOT_MARKET_MANAGED :
				errorMessage = NOT_MARKET_MANAGED;
				break;
		}
		mFREContext.dispatchStatusEventAsync(NOT_LICENSED, errorMessage);
	}
}
