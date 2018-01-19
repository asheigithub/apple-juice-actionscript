package 
{
	import autogencodelib.Testobj;
	import autogencodelib.Testobj_TESTHandler_Of_Int64;
	import flash.display.Sprite;
	import system.AsyncCallback;
	import system.Char;
	import system.Decimal;
	import system.IAsyncResult;
	import system.Int64;
	import system.MulticastDelegate;
	import system.UInt64;
	import system.Uri;
	import system._Array_;
	import system.collections.generic.List_Of_Int32;
	import system.collections.generic.List_Of_Int32_;
	import system.net.DownloadStringCompletedEventArgs;
	import system.net.DownloadStringCompletedEventHandler;
	import system.net.WebClient;
	
	
	[Doc]
	/**
	 * ...
	 * @author 
	 */
	public class AutoGenTest extends Sprite 
	{
		
		public function AutoGenTest() 
		{
			var client:WebClient = new WebClient();
			//trace( client.downloadString("http://www.baidu.com4"));
			
			client.DownloadStringCompleted_addEventListener(downloadcomplet);
			client.downloadStringAsync( new Uri( "http://www.baidu.com"));
			
			client.cancelAsync();
			
			
			var arr:_Array_ = _Array_.createInstance(int, 30);
			arr[0] = 5;
			arr[3] = 6;
			
			for each (var i in arr) 
			{
				trace(i);
			}
			
			
			
			client.dispose();
			
		}
		
		private function downloadcomplet(sender,args:DownloadStringCompletedEventArgs)
		{
			WebClient(sender).DownloadStringCompleted_removeEventListener(downloadcomplet);
			
			
			trace(args.cancelled);
			if(!args.cancelled)
				trace(args.result);
			WebClient(sender).dispose();
		}
	}
	

}