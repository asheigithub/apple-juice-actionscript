/*
ADOBE SYSTEMS INCORPORATED
Copyright 2008 Adobe Systems Incorporated. All Rights Reserved.
 
NOTICE:   Adobe permits you to modify and distribute this file only in accordance with
the terms of Adobe AIR SDK license agreement.  You may have received this file from a
source other than Adobe.  Nonetheless, you may modify or distribute this file only in 
accordance with such agreement.
*/

package air.update.descriptors
{
	import air.update.logging.Logger;
	import air.update.utils.Constants;
	
	import flash.filesystem.File;
	
	[ExcludeClass]
	public class StateDescriptor
	{
		private static var logger:Logger = Logger.getLogger("air.update.descriptors.StateDescriptor");
		
		public static const NAMESPACE_STATE_1_0:Namespace = new Namespace("http://ns.adobe.com/air/framework/update/state/1.0");
		
		// private
		private var xml:XML;
		private var defaultNS:Namespace;
				
		public function StateDescriptor(xml:XML)
		{
			this.xml = xml;
			defaultNS = xml.namespace();
		}
		
		/**
		 * Determines if the given namespace refers to the current version of state descriptor
		 */
		public static function isThisVersion(ns:Namespace):Boolean
		{
			return ns && ns.uri == NAMESPACE_STATE_1_0.uri;
		}
		
		/**
		 * Creates the default XML file
		 */
		public static function defaultState():StateDescriptor
		{
			default xml namespace = StateDescriptor.NAMESPACE_STATE_1_0;
			var initialXML:XML = 
				<state>
					<lastCheck>{new Date()}</lastCheck>
				</state>;
			return new StateDescriptor(initialXML);
		}
		
		public function getXML():XML
		{
			return xml;
		}
		

		public function get lastCheckDate():Date
		{	
			return stringToDate_defaultNull(xml.lastCheck.toString());
		}

		public function set lastCheckDate(value:Date):void
		{
			xml.lastCheck = value.toString();
		}
		
		public function get currentVersion():String
		{
			return xml.currentVersion.toString();			
		}
		
		public function set currentVersion(value:String):void
		{
			xml.currentVersion = value;
		}
		
		public function get previousVersion():String
		{
			return xml.previousVersion.toString();			
		}
		
		public function set previousVersion(value:String):void
		{
			xml.previousVersion = value;
		}
		
		public function get storage():File
		{
			return stringToFile_defaultNull(xml.storage.toString());			
		}
		
		public function set storage(value:File):void
		{
			xml.storage = fileToString_defaultEmpty(value);
		}
		
		public function get updaterLaunched():Boolean
		{
			return stringToBoolean_defaultFalse(xml.updaterLaunched.toString());			
		}
		
		public function set updaterLaunched(value:Boolean):void
		{
			xml.updaterLaunched = value.toString();
		}
		
		public function get failedUpdates():Array
		{
			var updates:Array = new Array();
			for each (var version:XML in xml.failed.*)
			{
				updates.push(version);
			}
			return updates;
		}	
		
		public function addFailedUpdate(value:String):void
		{
			if (xml.failed.length() == 0)
			{
				xml.failed = <failed/>
			}
			xml.failed.appendChild(<version>{value}</version>);
		}
		
		public function removeAllFailedUpdates():void
		{
			xml.failed = <failed />
		}
		
		public function validate():void
		{
			default xml namespace = defaultNS;
			
			if (!isThisVersion(defaultNS))
			{
				throw new Error("unknown state version", Constants.ERROR_STATE_UNKNOWN);
			}
			
			if (xml.lastCheck.toString() == "") 
			{
				throw new Error("lastCheck must have a non-empty value", Constants.ERROR_LAST_CHECK_MISSING);
			}
			
			if (!validateDate(xml.lastCheck.toString())) 
			{
				throw new Error("Invalid date format for state/lastCheck", Constants.ERROR_LAST_CHECK_INVALID);
			}
			
			if (xml.previousVersion.toString() != "" && !validateText(xml.previousVersion))
			{
				throw new Error("Illegal value for state/previousVersion", Constants.ERROR_PREV_VERSION_INVALID);
			}
			
			if (xml.currentVersion.toString() != "" && !validateText(xml.currentVersion))
			{
				throw new Error("Illegal value for state/currentVersion", Constants.ERROR_CURRENT_VERSION_INVALID);
			}

			if (xml.storage.toString() != "" && (!validateText(xml.storage) || !validateFile(xml.storage.toString())))
			{
				throw new Error("Illegal value for state/storage", Constants.ERROR_STORAGE_INVALID);
			}
			
			if ([ "", "true", "false"].indexOf(xml.updaterLaunched.toString()) == -1 ) 
			{
				throw new Error("Illegal value \"" + xml.updaterLaunched.toString() + "\" for state/updaterLaunched.", Constants.ERROR_LAUNCHED_INVALID);
			}
			
			if (!validateFailed(xml.failed))
			{
				throw new Error("Illegal values for state/failed", Constants.ERROR_FAILED_INVALID);
			}
			
			// check if all the update data is in place
			var count:int = 0;
			if (previousVersion != "") count ++;
			if (currentVersion != "") count ++;
			if (storage) count ++;
			
			if (count > 0 &&  count != 3)
			{
				throw new Error("All state/previousVersion, state/currentVersion, state/storage, state/updaterLaunched  must be set", Constants.ERROR_VERSIONS_INVALID);
			}
			
		}
		
		private function validateDate(dateString:String):Boolean
		{
			var result:Boolean = false;
			try
			{
				var n:Number = Date.parse(dateString);
				if (!isNaN(n))
				{
					result = true;
				}
			}catch(err:Error)
			{
				result = false;
			}
			return result;
		}
		
		private function validateFile(fileString:String):Boolean
		{
			var result:Boolean = false;
			try
			{
				var file:File = new File(fileString);
				result = true;
			}catch(err:Error)
			{
				result = false;
			}
			return result;
		}		
		
		private function validateText(elem:XMLList):Boolean
		{
			// See if element contains simple content
			if (!elem.hasSimpleContent())
			{
				return false;
			}			
			if (elem.length() > 1)
			{
				// XMLList contains more than one element - ie. there is more than one
				// <name> or <description> element. This is invalid.
				return false;
			}

			return true;
		}
		
		private function validateFailed(elem:XMLList):Boolean
		{
			if (elem.length() > 1)
			{
				// XMLList contains more than one element - ie. there is more than one
				// <name> or <description> element. This is invalid.
				return false;
			}
			var elemChildren:XMLList = elem.*;
			for each (var child:XML in elemChildren)
			{
				if (child.name() == null)
				{
					// If any element doesn't have a name
					return false;
				}				
				if (child.name().localName != "version")
				{
					// If any element is not <version>, it's not valid
					return false;
				}

				if (!child.hasSimpleContent())
				{
					// If any <version> element contains more than simple content, it's not valid
					return false;
				}
			}
			return true;	
		}
		
		private function stringToDate_defaultNull(dateString:String):Date
		{
			var date:Date = null;
			if (dateString)
			{
				date = new Date(dateString);
			}
			return date;
		}

		private function stringToBoolean_defaultFalse(str:String):Boolean
		{
			switch (str) {
				case "true":
				case "1":
					return true;

					
       			case "":        
				case "false":
				case "0":
					return false;
			}
			return false;
		}
		
		private function stringToFile_defaultNull(str:String):File
		{
			if (!str) 
			{
				return null;
			}
			return new File(str);
		}
		
		private function fileToString_defaultEmpty(file:File):String
		{
			if (file && file.nativePath) 
			{
				return file.nativePath;
			}
			return "";
		}

	}
}