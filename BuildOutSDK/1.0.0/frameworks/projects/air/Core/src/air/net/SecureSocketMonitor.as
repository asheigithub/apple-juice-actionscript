/*
ADOBE SYSTEMS INCORPORATED
Copyright � 2008 Adobe Systems Incorporated. All Rights Reserved.
 
NOTICE:   Adobe permits you to modify and distribute this file only in accordance with
the terms of Adobe AIR SDK license agreement.  You may have received this file from a
source other than Adobe.  Nonetheless, you may modify or distribute this file only in 
accordance with such agreement.
*/

package air.net
{
	import flash.events.TimerEvent;
	import flash.events.Event;
	import flash.events.SecurityErrorEvent;
	import flash.events.IOErrorEvent;
	import flash.net.Socket;
	import flash.net.SecureSocket;
	import flash.errors.IOError;
	
/**
 * A SecureSocketMonitor object monitors availablity of a TCP endpoint over Secure Sockets Layer (SSL)
 * and Transport Layer Security (TLS) protocols. 
 * 
 * <p platform="actionscript">This class is included in the aircore.swc file. 
 * Flash Builder loads this class automatically when you create a project for AIR.
 * The Flex SDK also includes this aircore.swc file, which you should include
 * when compiling the application if you are using Flex SDK.
 * </p>
 * 
 * <p platform="actionscript">In Adobe<sup>&#xAE;</sup> Flash<sup>&#xAE;</sup> CS3 Professional,
 * this class is included in the ServiceMonitorShim.swc file. To use classes in the air.net package , 
 * you must first drag the ServiceMonitorShim component from the Components panel to the 
 * Library and then add the following <code>import</code> statement to your ActionScript 3.0 code:
 * </p>
 * 
 * <listing platform="actionscript">import air.net.~~;</listing>
 *
 * <p platform="actionscript">To use air.net package in Adobe<sup>&#xAE;</sup> Flash<sup>&#xAE;</sup> Professional (CS4 or higher): </p>
 *
 * <ol platform="actionscript">
 *	<li>Select the File &gt; Publish Settings command.</li>
 *	<li>In the Flash panel, click the Settings button for ActionScript 3.0. Select Library Path.</li>
 *	<li>Click the Browse to SWC File button. Browse to Adobe Flash CS<i>n</i>/AIK<i>n.n</i>/frameworks/libs/air/aircore.swc
 		file in the Adobe Flash Professional installation folder.</li>
 *	<li>Click the OK button.</li>
 *	<li>Add the following <code>import</code> statement to your ActionScript 3.0 code: <code>import air.net.~~;</code></li>
 * </ol>
 * 
 * <p platform="javascript">To use this class in JavaScript code, load the aircore.swf 
 * file, as in the following:</p>
 * 
 * <listing platform="javascript">&lt;script src="aircore.swf" type="application/x-shockwave-flash"&gt;</listing>
 * 
 * 
 * @playerversion AIR 2.0
*/
public class SecureSocketMonitor extends SocketMonitor
{
	/**
	 * Creates a SecureSocketMonitor object for a specified TCP endpoint.
	 *
	 * <p>After creating a SecureSocketMonitor object, the caller should call <code>start</code>
	 * to begin monitoring the status of the service.</p>
	 *
	 * <p>As with the Timer object, the caller should maintain a reference to the SecureSocketMonitor
	 * object. Otherwise, the runtime deletes the object and monitoring ends.</p>
	 *
	 * @param host The host to monitor.
	 * @param port The port to monitor.
	 * 
	 * @playerversion AIR 2.0
	 */
	public function SecureSocketMonitor(host:String, port:int)
	{
		super(host,port);
	}
			
	/**
	 * @inheritDoc
	 * 
	 * @playerversion AIR 2.0
	 */
	public override function toString():String
	{
		return '[SecureSocketMonitor host="' + host + '" port="' + port + 
			'" available="' + available  + '"]';
	}

	/**
	* Creates a SecureSocket object.
	* 
	* @return SecureSocket the SecureSocket object to be used by this SocketMonitor or <code>null</code> if
	 * secure sockets are not supported on the current system.
	 *
	 * @playerversion AIR 2.0
	 */
	protected override function createSocket():Socket
	{
		if(!SecureSocket.isSupported)
			return null;
		return new SecureSocket();
	}
}
}
