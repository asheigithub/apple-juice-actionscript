package air.desktop {
	
import flash.events.EventDispatcher;
import flash.events.Event;
import flash.events.ProgressEvent;
import flash.events.IOErrorEvent;
import flash.events.SecurityErrorEvent;
import flash.events.ErrorEvent;
import flash.events.HTTPStatusEvent;
import flash.net.URLRequest;
import flash.net.URLStream;
import flash.utils.IDataInput;
import flash.desktop.IFilePromise;

/**
* Dispatched when the underlying URLStream connection is opened. 
* 
* <p><b>Note:</b> The AIR runtime uses this event to manage the asynchronous data retrieval process.
* Typically, there is no need for your application to take any action in response to this event.</p>
* 
* @eventType flash.events.Event.OPEN
* @playerversion AIR 2.0
* @langversion 3.0
*/
[Event(name="open", type="flash.events.Event")]
/**
* Dispatched when the data for the file has been fully downloaded. 
* 
* <p><b>Note:</b> The AIR runtime uses this event to manage the asynchronous data retrieval process.
* Typically, there is no need for your application to take any action in response to this event.</p>
* 
* @eventType flash.events.Event.COMPLETE
* @playerversion AIR 2.0
* @langversion 3.0
*/
[Event(name="complete",  type="flash.events.Event")]
/**
* Dispatched when a block of data is available to be read from the underlying URLStream. 
* 
* <p><b>Note:</b> The AIR runtime uses this event to manage the asynchronous data retrieval process.
* Typically, there is no need for your application to take any action in response to this event.</p>
* 
* @eventType flash.events.ProgressEvent.PROGRESS
* @playerversion AIR 2.0
* @langversion 3.0
*/
[Event(name="progress", type="flash.events.ProgressEvent")] 
/**
* Dispatched when an IOError prevents the file download. 
* 
* @eventType flash.events.IOErrorEvent.IO_ERROR
* @playerversion AIR 2.0
* @langversion 3.0
*/
[Event(name="ioError", type="flash.events.IOErrorEvent")]
/**
* Dispatched for HTTP requests to report the response headers. 
* 
* @eventType flash.events.HTTPStatusEvent.HTTP_RESPONSE_STATUS
* @playerversion AIR 2.0
* @langversion 3.0
*/
[Event(name="httpResponseStatus", type="flash.events.HTTPStatusEvent")]
/**
* Dispatched for HTTP requests to report the request status code.
* 
* @eventType flash.events.HTTPStatusEvent.HTTP_STATUS
* @playerversion AIR 2.0
* @langversion 3.0
*/
[Event(name="httpStatus", type="flash.events.HTTPStatusEvent")]
/**
* Dispatched when a security error prevents the file download. 
* 
* @eventType flash.events.SecurityErrorEvent.SECURITY_ERROR
* @playerversion AIR 2.0
* @langversion 3.0
*/
[Event(name="securityError", type="flash.events.SecurityErrorEvent")]
[Version("air2.0")]
/**
 * The URLFilePromise class allows resources accessible at a URL to be dragged out of an AIR application as a file promise.
 * 
 * <p>The URLFilePromise class implements the IFilePromise interface using URLStream and URLRequest objects as the data source.
 * The implementation provides drag and drop support for files that can be retrieved using HTTP or the other protocols supported 
 * by the URLStream class.</p>
 * 
 * <p>To create a URL file promise:</p>
 * <ol>
 * <li>Construct and initialize one or more URLFilePromise objects.</li>
 * <li>Add the URLFilePromise objects to an array.</li>
 * <li>Add the array to a new Clipboard object using the ClipboardFormat, <code>FILE_PROMISE_LIST</code>.</li>
 * <li>In response to a user gesture, call the NativeDragManager <code>startDrag()</code> method, passing in the
 * Clipboard object containing the array of file promises.</li>
 * </ol>
 * 
 * <p>When the user completes the drag operation, the runtime downloads the data for each file promise. The data is
 * accessed at the URL specified by the <code>request</code> property of the URLFilePromise object and saved to the file 
 * specified in the <code>relativePath</code> property. The file is saved relative to the drop location. Thus, if the 
 * relative path is <code>foo/bar.txt</code>, and the file promise is dropped into a directory called <code>home</code>, then the location of the
 * created file is: <code>home/foo/bar.txt</code>. When an error occurs, the file is not created.</p>
 * 
 * <p>To support data sources that are not accessible through the URLStream class, implement the IFilePromise interface.</p>
 * 
 * <p><b>Note:</b> The AIR runtime calls the IFilePromise methods, <code>open()</code>, <code>close()</code>, and <code>reportError()</code> 
 *  automatically. These methods should never be called by your application logic. Likewise, the <code>open</code>, <code>progress</code>,
 *  <code>complete</code>, and <code>close</code> events dispatched by this URLFilePromise object are provided primarily for debugging
 * purposes. Your application does not need to respond to these events.</p>
 *
 * <p platform="actionscript">This class is included in the aircore.swc file. 
 * Adobe<sup>&#xAE;</sup> Flash<sup>&#8482;</sup> Builder loads this class automatically when you create a project for 
 * Adobe<sup>&#xAE;</sup> AIR<sup>&#8482;</sup>. The Adobe<sup>&#xAE;</sup> Flex<sup>&#8482;</sup> SDK also 
 * includes this aircore.swc file, which you should include when compiling the application if you are using 
 * the Flex SDK.
 * </p>
 * 
 * <p platform="actionscript">To use air.desktop package in Adobe<sup>&#xAE;</sup> Flash<sup>&#xAE;</sup> Professional (CS4 or higher): </p>
 *
 * <ol platform="actionscript">
 *	<li>Select the File &gt; Publish Settings command.</li>
 *	<li>In the Flash panel, click the Settings button for ActionScript 3.0. Select Library Path.</li>
 *	<li>Click the Browse to SWC File button. Browse to Adobe Flash CS<i>n</i>/AIK<i>n.n</i>/frameworks/libs/air/aircore.swc
 		file in the Adobe Flash Professional installation folder.</li>
 *	<li>Click the OK button.</li>
 *	<li>Add the following <code>import</code> statement to your ActionScript 3.0 code: <code>import air.desktop.~~;</code></li>
 * </ol>
 * 
 * <p platform="javascript">To use this class in JavaScript code, load the aircore.swf 
 * file, as in the following:</p>
 * 
 * <listing platform="javascript">&lt;script src="aircore.swf" type="application/x-shockwave-flash"&gt;</listing>
 * 
 * @see flash.desktop.IFilePromise IFilePromise interface
 * @see flash.desktop.Clipboard Clipboard class
 * @see flash.desktop.ClipboardFormats ClipboardFormats class
 * @see flash.desktop.NativeDragManager NativeDragManager class
 * @see flash.net.URLStream URLStream class
 * @see flash.net.URLRequest URLRequest class
 * 
 * @playerversion AIR 2.0
 * @langversion 3.0
 */
public class URLFilePromise extends EventDispatcher implements IFilePromise
{
	private var _request:URLRequest;
	private var _stream:URLStream;
    private var _relativePath:String;
    
    /**
    * Creates a URLFilePromise object.
    * 
    * <p>You must set the <code>request</code> and <code>relativePath</code> properties before
    * using this URLFilePromise object.</p>
    * 
 	* @playerversion AIR 2.0
	* @langversion 3.0
    */
    public function URLFilePromise()
    {
    }
    
    /** 
    * The URLRequest identifying the resource to be copied as the result of the drag-and-drop operation.
    * 
    * @param value A URLRequest object
    * 
  	* @playerversion AIR 2.0
 	* @langversion 3.0
   */
    public function set request(value:URLRequest):void
    {
        _request = value;    
    }
    
    /**
    *  @private -- docs on setter
    *  Gets the URLRequest associated with this object
    */
    public function get request():URLRequest
    {
    	return _request;
    }

    /**
    * Indicates whether the resource data is available asynchronously.
    *  
    * <p>The isAsync property of a URLFilePrmise object is always <code>true</code> since URL streams are inherently asynchronous.</p>
    * 
 	* @playerversion AIR 2.0
 	* @langversion 3.0
    */      
    public function get isAsync():Boolean
    {
    	return true;
    }

    /** 
    * The path and file name of the created file, relative to the drop destination.
    * 
    * <p>The path can include subdirectories, which are resolved based on the drop location. The subdirectories are created,
    * if needed. When including subdirectories, use the <code>File.separator</code> constant to insert the
    * proper path separator character for the current operating system. Using the .. shortcut to navigate to a parent directory 
    * is not allowed.</p>
    * 
    * <p>The file name does not need to be the same as the file name of the remote resource.</p>
    * 
 	* @playerversion AIR 2.0
 	* @langversion 3.0
    */      
    public function set relativePath(value:String):void
    {
        _relativePath = value;    
    }
     
    /** 
    * @private -- docs on setter
    * The relativePath of the file that will be created in the drop destination.
    */ 
    public function get relativePath():String
    {
        return _relativePath;    
    }

    /**
    * Allows the AIR runtime to open the data source at the appropriate time during the drag-and-drop operation.
    * 
    * <p>Do not call this function in your application logic.</p>
    * 
 	* @playerversion AIR 2.0
 	* @langversion 3.0
    */       
    public function open():IDataInput
    {
        _stream = new URLStream();
        registerEventHandlers(); // to reflect URLStream events
        _stream.load(_request);
        return _stream;
    }
     
    /**
    * Allows the AIR runtime to close the data source at the appropriate time during the drag-and-drop operation.
    * 
    * <p>Do not call this function in your application logic.</p>
    * 
 	* @playerversion AIR 2.0
 	* @langversion 3.0
    */     
    public function close():void
    {
        if ( _stream ) _stream.close();
        unregisterEventHandlers();
    }
     
    /**
    * Allows the AIR runtime to report errors that occur during the drag-and-drop operation.
    * 
    * <p>The URLFilePromise object redispatches any error events reported. Do not call this 
    * function in your application logic.</p>
    * 
 	* @playerversion AIR 2.0
 	* @langversion 3.0
    */     
    public function reportError(e:ErrorEvent):void
    {
		dispatchEvent(e);	
    }
    
    private function registerEventHandlers():void
    {
        if ( _stream )
        {
            _stream.addEventListener(flash.events.Event.OPEN, reflectURLStreamEvent);
            _stream.addEventListener(flash.events.Event.COMPLETE, reflectURLStreamEvent);
            _stream.addEventListener(flash.events.HTTPStatusEvent.HTTP_STATUS, reflectURLStreamEvent);
            _stream.addEventListener(flash.events.HTTPStatusEvent.HTTP_RESPONSE_STATUS, reflectURLStreamEvent);            
            _stream.addEventListener(flash.events.ProgressEvent.PROGRESS, reflectURLStreamEvent);
            _stream.addEventListener(flash.events.IOErrorEvent.IO_ERROR, reflectURLStreamEvent);
            _stream.addEventListener(flash.events.SecurityErrorEvent.SECURITY_ERROR, reflectURLStreamEvent);
        }
    }
    
    private function unregisterEventHandlers():void
    {
        if ( _stream ) 
        {
            _stream.removeEventListener(flash.events.Event.OPEN, reflectURLStreamEvent);
            _stream.removeEventListener(flash.events.Event.COMPLETE, reflectURLStreamEvent);
            _stream.removeEventListener(flash.events.HTTPStatusEvent.HTTP_STATUS, reflectURLStreamEvent);
            _stream.removeEventListener(flash.events.HTTPStatusEvent.HTTP_RESPONSE_STATUS, reflectURLStreamEvent);
            _stream.removeEventListener(flash.events.ProgressEvent.PROGRESS, reflectURLStreamEvent);            
            _stream.removeEventListener(flash.events.IOErrorEvent.IO_ERROR, reflectURLStreamEvent);
            _stream.removeEventListener(flash.events.SecurityErrorEvent.SECURITY_ERROR, reflectURLStreamEvent);
        }
    }

    private function reflectURLStreamEvent(e:Event):void
    {
    	if ( !(e is IOErrorEvent) && !(e is SecurityErrorEvent) )
    	{
    		// Other events will get reported back from runtime via reportError
        	dispatchEvent(e);
        }
    }            
}
    
} // package