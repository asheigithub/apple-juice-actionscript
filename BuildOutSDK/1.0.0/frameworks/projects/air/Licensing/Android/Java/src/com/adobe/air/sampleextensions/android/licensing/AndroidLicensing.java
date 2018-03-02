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
import com.adobe.fre.FREFunction;
import com.adobe.fre.FREInvalidObjectException;
import com.adobe.fre.FREObject;
import com.adobe.fre.FRETypeMismatchException;
import com.adobe.fre.FREWrongThreadException;

import com.google.android.vending.licensing.AESObfuscator;
import com.google.android.vending.licensing.LicenseChecker;
import com.google.android.vending.licensing.LicenseCheckerCallback;
import com.google.android.vending.licensing.ServerManagedPolicy;

import android.app.Activity;
import android.content.Context;
import android.provider.Settings.Secure;




public class AndroidLicensing implements FREFunction  {
	
	/* 
	 *  This function is called from ActionsScript class LicenseChecker after a call to "checkLicenseNative" is made in checkLicense() function. 
	 *  The mapping between function called by ActionScript and the native code is specified in class AndroidLicensingExtensionContext.  
	 */  

	public FREObject call(FREContext ctx, FREObject[] passedArgs) {

		FREObject result = null;
		String BASE64_PUBLIC_KEY = null;
		Activity act;

		try{
			BASE64_PUBLIC_KEY = passedArgs[0].getAsString();
			act = ctx.getActivity();
			checkLicense(ctx, act, BASE64_PUBLIC_KEY);
			result = FREObject.newObject(1);

		} catch (FRETypeMismatchException e) {
			System.out.println("##### AndroidLicensing - caught FRETypeMismatchException");
			e.printStackTrace();
		} catch (FREInvalidObjectException e) {
			System.out.println("##### AndroidLicensing - caught FREInvalidObjectException");
			e.printStackTrace();
		} catch (FREWrongThreadException e) {
			System.out.println("##### AndroidLicensing - caught FREWrongThreadException");
			e.printStackTrace();
		}
		return result;
	}

	// Generate your own 20 random bytes, and put them here. This is required by License Verification Library (LVL) for obfuscation.
	private static final byte[] SALT = new byte[] {
		0, 0, 0, 0
	};

	private LicenseCheckerCallback mLicenseCheckerCallback;
	private LicenseChecker mChecker;

	/* 
	 * @param freContext  FREContext created during initialization of the native extension.
	 * @param context     Context of the running AIR Application activity.
	 * @param publicKey   Public key obtained from Android Market for licensing purposes.
	 */
	public void checkLicense( FREContext freContext, Context context, String publicKey )
	{
		// This sample code uses Secure.ANDROID_ID only for device identification. Strenthen this string by using as many device specific
		// string so as to make it as unique as possible as this is used for obfuscating the server response.
			
		// ANDROID_ID is a 64-bit number (as a hex string) that is randomly generated on the device's first boot and should remain 
		// constant for the lifetime of the device. This value may change if a factory reset is performed on the device.  
		String deviceId = Secure.getString(context.getContentResolver(), Secure.ANDROID_ID);

		// Library calls this callback when it's done verifying with Android licensing server
		mLicenseCheckerCallback =  new AndroidLicenseCheckerCallback(freContext);
		
		// Construct the LicenseChecker object with a policy and call checkAccess(). In case a developer wants to change the policy to
		// be used he needs to replace  the "ServerManagedPolicy" with the policy name with any other policy, if required. 
		// ServerManagedPolicy is defined in License Verification Library (LVL) provided by android. 

		ServerManagedPolicy policy = new ServerManagedPolicy(context, new AESObfuscator(SALT, context.getPackageName(), deviceId));
		
		mChecker = new LicenseChecker( context, policy, publicKey);
		mChecker.checkAccess(mLicenseCheckerCallback);
	}
}
