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
	public class UpdateDescriptor
	{
		public static const NAMESPACE_UPDATE_1_0:Namespace = new Namespace("http://ns.adobe.com/air/framework/update/description/1.0");
		public static const NAMESPACE_UPDATE_2_5:Namespace = new Namespace("http://ns.adobe.com/air/framework/update/description/2.5");
		
		// private
		private var xml:XML;
		private var defaultNS:Namespace;
		
		public function UpdateDescriptor(xml:XML)
		{
			this.xml = xml;
			defaultNS = xml.namespace();
		}
		
		/**
		 * Determines if the given namespace refers to the current version of update descriptor
		 */
		public static function isKnownVersion(ns:Namespace):Boolean
		{
			if (!ns)
				return false;
			switch( ns.uri ) {
				case NAMESPACE_UPDATE_1_0.uri:
				case NAMESPACE_UPDATE_2_5.uri:
					return true;
				default:
					return false;
			}
			return false;
		}
		
		public function isCompatibleWithApplicationDescriptor(appHasVersionNumber:Boolean):Boolean
		{
			default xml namespace = defaultNS;
			var updateHasVersionNumber:Boolean = xml.versionNumber != undefined;
			
			return updateHasVersionNumber == appHasVersionNumber;
		}

		public function get version():String
		{
			default xml namespace = defaultNS;
			return (xml.version != undefined) ? xml.version.toString() : xml.versionNumber.toString();
		}

		public function get versionLabel():String
		{
			default xml namespace = defaultNS;
			if (xml.version != undefined)
				return xml.version.toString();

			return (xml.versionLabel != undefined) ?  xml.versionLabel.toString() : xml.versionNumber.toString();
		}

		public function get url():String
		{
			default xml namespace = defaultNS;
			return xml.url.toString();
		}
		
		public function get description():Array
		{
			default xml namespace = defaultNS;
			return UpdateDescriptor.getLocalizedText(xml.description, defaultNS);
		}
		
		public function getXML():XML
		{
			return xml;
		}
		
		public function validate():void
		{
			default xml namespace = defaultNS;
			
			if (!isKnownVersion(defaultNS))
			{
				throw new Error("unknown update descriptor namespace", Constants.ERROR_UPDATE_UNKNOWN);
			}
			
			if(xml.versionNumber != undefined) {
				if (version == "")
				{
					throw new Error("update versionNumber must have a non-empty value", Constants.ERROR_VERSION_MISSING); 
				}
				if( !(/^[0-9]{1,3}(\.[0-9]{1,3}){0,2}$/.test( version ) ) ) 
				{
					throw new Error( "update versionNumber contains an invalid value", Constants.ERROR_VERSION_INVALID );
				}
			}
			else
			{
				if (version == "")
				{
					throw new Error("update version must have a non-empty value", Constants.ERROR_VERSION_MISSING); 
				}
			}
			// An URL must not start or end with space
//			if (!(/^([^ ]|[^ ].*[^ ])$/.test(version)))
//			{
//				throw new Error("invalid update version", ERROR_VERSION_INVALID);
//			}
			
			if (url == "")
			{
				throw new Error("update url must have a non-empty value", Constants.ERROR_URL_MISSING); 
			}
			
			// An URL must not start or end with space
//			if (!(/^([^ ]|[^ ].*[^ ])$/.test(url)))
//			{
//				throw new Error("invalid update url", ERROR_URL_INVALID);
//			}
			
			if (!validateLocalizedText(xml.description, defaultNS))
			{
				throw new Error("Illegal values for update/description", Constants.ERROR_DESCRIPTION_INVALID);
			}		

		}
		
		/**
		 * Retrieve the localized text from the given XML element (passed in as an
		 * XMLList object). Returns appropriate text child based on current system
		 * language. Returns first text child if no text language matches system
		 * language. 
		 * Assumes xml element is of the following form:
		 *	<xmlelement>
		 *		<text xml:lang="xx">XXXX</text>
		 *		...
		 *	</xmlelement>
		 */
		
		public static function getLocalizedText( elem:XMLList, ns:Namespace ):Array
		{
			var xmlNS:Namespace = new Namespace("http://www.w3.org/XML/1998/namespace");
			var result:Array = []; 
			
			// See if element contains simple content
			if (elem.hasSimpleContent())
			{
				result = [["", elem.toString()]];
			}
			else
			{
				// Gather all the languages from the text children
				var elemChildren:XMLList = elem.ns::text;
				for each (var child:XML in elemChildren)
				{
					result.push([child.@xmlNS::lang.toString(), child[0].toString()]);
				}
			}
			return result;
		}
		
		private static function validateLocalizedText( elem:XMLList, ns:Namespace ) : Boolean
		{
			var xmlNS:Namespace = new Namespace("http://www.w3.org/XML/1998/namespace");
			
			// See if element contains simple content
			if (elem.hasSimpleContent())
				return true;

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
				if (child.name().localName != "text")
				{
					// If any element is not <text>, it's not valid
					return false;
				}

				if ((child.@xmlNS::lang).length() == 0)
				{
					// If any <text> element does not contain "xml:lang" attribute, it's not valid
					return false;
				}
				
				if (!child.hasSimpleContent())
				{
					// If any <text> element contains more than simple content, it's not valid
					return false;
				}
			}
			
			return true;
		}
		

		
	}
}
