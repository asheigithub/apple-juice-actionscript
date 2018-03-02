/*
ADOBE SYSTEMS INCORPORATED
Copyright 2008 Adobe Systems Incorporated. All Rights Reserved.
 
NOTICE:   Adobe permits you to modify and distribute this file only in accordance with
the terms of Adobe AIR SDK license agreement.  You may have received this file from a
source other than Adobe.  Nonetheless, you may modify or distribute this file only in 
accordance with such agreement.
*/

package air.update.core
{
	import air.update.utils.Constants;
	
	import flash.utils.ByteArray;
	
	[ExcludeClass]
	
	public class AIRUnpackager extends UCFUnpackager
	{
		private var _descriptorXML:XML;
		
		public function AIRUnpackager()
		{
			super();
		}
		
		/** The application XML descriptor. */
		public function get descriptorXML():XML {
			return _descriptorXML;
		}
		
		override protected function onDone():void
		{
			if (_descriptorXML == null)
			{
				throw new Error("META-INF/AIR/application.xml must exists in the AIR file", Constants.ERROR_AIR_MISSING_APPLICATION_XML);
			}
		}
		
		override protected function onFile(fileNumber:uint, path:String, data:ByteArray):Boolean
		{
			if (fileNumber == 0) return true;
			if (fileNumber == 1 && path != "META-INF/AIR/application.xml")
				throw new Error( "META-INF/AIR/application.xml must be the second file in an AIR file", Constants.ERROR_AIR_MISSING_APPLICATION_XML);
			_descriptorXML = new XML(data.toString());
			// stop after the second file
			return false;
		}
		
	}
}