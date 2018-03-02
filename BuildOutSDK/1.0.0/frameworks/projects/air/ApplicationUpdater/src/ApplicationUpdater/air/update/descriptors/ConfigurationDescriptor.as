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
	import air.update.utils.Constants;
	
	[ExcludeClass]
	public class ConfigurationDescriptor
	{
		public static const NAMESPACE_CONFIGURATION_1_0:Namespace = new Namespace("http://ns.adobe.com/air/framework/update/configuration/1.0");
		
		public static const DIALOG_CHECK_FOR_UPDATE:String = "checkforupdate";
		public static const DIALOG_DOWNLOAD_UPDATE:String = "downloadupdate";
		public static const DIALOG_DOWNLOAD_PROGRESS:String = "downloadprogress";
		public static const DIALOG_INSTALL_UPDATE:String = "installupdate";
		public static const DIALOG_FILE_UPDATE:String = "fileupdate";
		public static const DIALOG_UNEXPECTED_ERROR:String = "unexpectederror";				

		// private
		private var xml:XML;
		private var defaultNS:Namespace;
		
		public function ConfigurationDescriptor(xml:XML)
		{
			this.xml = xml;
			defaultNS = xml.namespace();
		}
		
		/**
		 * Determines if the given namespace refers to the current version of configuration descriptor
		 */
		public static function isThisVersion(ns:Namespace):Boolean
		{
			return ns && ns.uri == NAMESPACE_CONFIGURATION_1_0.uri;
		}
		
		public function get checkInterval():Number
		{
			default xml namespace = defaultNS;
			return convertInterval(xml.delay.toString());
		}
		
		public function get url():String
		{
			default xml namespace = defaultNS;
			return xml.url.toString();
		}
		
		public function get defaultUI():Array
		{
			var dialogs:Array = new Array();
			for each (var elem:XML in xml.defaultUI.*)
			{
				var dlg:Object = {"name": elem.@name, "visible": stringToBoolean_defaultFalse(elem.@visible)};
				dialogs.push(dlg);
			}
			return dialogs;			
		}
		
		public function validate():void
		{
			default xml namespace = defaultNS;
			
			if (!isThisVersion(defaultNS))
			{
				throw new Error("unknown configuration version", Constants.ERROR_CONFIGURATION_UNKNOWN);
			}
			
			if (url == "")
			{
				throw new Error("configuration url must have a non-empty value", Constants.ERROR_URL_MISSING); 
			}
			
			if (!validateInterval(xml.delay.toString())) 
			{
				throw new Error("Illegal value \"" + xml.delay.toString() + "\" for configuration/delay", Constants.ERROR_DELAY_INVALID);
			}
			
			if (!validateDefaultUI(xml.defaultUI))
			{
				throw new Error("Illegal values for configuration/defaultUI", Constants.ERROR_DEFAULTUI_INVALID);
			}
		}
		
		private static function convertInterval(intervalString:String):Number
		{
			var result : Number = -1;
			if (intervalString.length > 0)
			{
				result = Number(intervalString);
			}
			return result;
		}
		
		
		private static function validateInterval(intervalString:String):Boolean
		{
			var result:Boolean = false;
			if (intervalString.length > 0)
			{
				try
				{
					var intervalNumber:Number = Number(intervalString);
					if (intervalNumber >= 0)
					{
						result = true;
					}
				}
				catch(theException:*)
				{
					result = false;
				}
			}
			else
			{
				result = true;
			}
			return result;
		}

		private static function validateDefaultUI( elem:XMLList) : Boolean
		{
			if (elem.length() > 1)
			{
				// XMLList contains more than one element - ie. there is more than one
				// <name> or <description> element. This is invalid.
				return false;
			}

			// Iterate through all children of the element
			var elemChildren:XMLList = elem.*;
			for each (var child:XML in elemChildren)
			{
				if (child.name() == null)
				{
					// If any element doesn't have a name
					return false;
				}
				if (child.name().localName != "dialog")
				{
					// If any element is not <text>, it's not valid
					return false;
				}

				if ([DIALOG_CHECK_FOR_UPDATE, DIALOG_DOWNLOAD_UPDATE, DIALOG_DOWNLOAD_PROGRESS, DIALOG_INSTALL_UPDATE,DIALOG_FILE_UPDATE,DIALOG_UNEXPECTED_ERROR]
						.indexOf(child.@name.toString().toLowerCase()) == -1)
				{
					// If any <dialog> element does not contain "name" attribute, it's not valid
					return false;
				}
				
				if (["true", "false"].indexOf(child.@visible.toString()) == -1)
				{
					// If any <dialog> element does not contain "visible" attribute with "true" or "false", it's not valid
					return false;
				}
				
				if (child.hasComplexContent())
				{
					// If any <dialog> element contains content, it's not valid
					return false;
				}
			}
			return true;
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

	}
}