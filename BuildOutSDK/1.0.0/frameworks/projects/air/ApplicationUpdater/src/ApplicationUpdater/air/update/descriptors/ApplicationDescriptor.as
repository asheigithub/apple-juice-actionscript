/*
ADOBE SYSTEMS INCORPORATED
Copyright 2008 Adobe Systems Incorporated. All Rights Reserved.
 
NOTICE:   Adobe permits you to modify and distribute this file only in accordance with
the terms of Adobe AIR SDK license agreement.  You may have received this file from a
source other than Adobe.  Nonetheless, you may modify or distribute this file only in 
accordance with such agreement.
*/

package air.update.descriptors {
	import flash.geom.Point;
    import flash.utils.ByteArray;
    import flash.display.NativeWindowSystemChrome;

	/**
	 * Class for accessing the contents of an application descriptor. This
	 * class provides access to the entire descriptor but only for the this
	 * version of the runtime; if you want to access descriptors for other
	 * versions, see InstalledApplication.
	 *
	 * After instantiating, call the validate methods to ensure you have a 
	 * valid file. Results of accessor methods/properties are undefined if 
	 * validate() fails.
	 */
	[ExcludeClass]
	public class ApplicationDescriptor {

		public function ApplicationDescriptor( xml:XML) {
		    m_xml = xml;
			m_defaultNs = m_xml.namespace();
		}

        public function get namespace():Namespace
        {
			default xml namespace = m_defaultNs;
            return m_xml.namespace();
        }
           
		public function get minimumPatchLevel():int 
		{
			default xml namespace = m_defaultNs;
			return m_xml.@minimumPatchLevel;
		}
		
		public function get id():String
		{
			default xml namespace = m_defaultNs;
			return m_xml.id.toString();
		}
		
		public function get version():String
		{
			default xml namespace = m_defaultNs;
			
			// until 2.5 we had version, after 2.5 we had versionNumber
			if (m_xml.version == undefined && m_xml.versionNumber == undefined)
				throw new Error( "cannot get version (backwards incompatible application namespace change?)" );

			if (m_xml.version == undefined) 
				return m_xml.versionNumber.toString();
			
			return m_xml.version.toString();
		}

		public function get versionLabel():String
		{
			//default xml namespace = m_defaultNs;
			// until 2.5 we had version, after 2.5 we had versionNumber
			if (m_xml.nsversion == undefined && m_xml.versionNumber == undefined)
				throw new Error( "cannot get version (backwards incompatible application namespace change?)" );

			if (m_xml.version != undefined)
				return m_xml.version.toString();
				
			return (m_xml.versionLabel == undefined) ?  m_xml.versionNumber.toString() : m_xml.versionLabel.toString();
		}

		public function get filename():String
		{
			default xml namespace = m_defaultNs;
			return m_xml.filename.toString();
		}

		/** Name defaults to filename if not specified */
		public function get name():String
		{
			default xml namespace = m_defaultNs;
			return (m_name == "" ? filename : m_name);
		}

		public function get description():String
		{
			return m_description;
		}
		
		public function get copyright():String
		{
			default xml namespace = m_defaultNs;
			return m_xml.copyright.toString();
		}

		public function get initialWindowContent():String 
		{
			default xml namespace = m_defaultNs;
			return m_xml.initialWindow.content;
		}

		/** Defaults to name of application */
		public function get initialWindowTitle():String
		{
			default xml namespace = m_defaultNs;
		    var result:String = m_xml.initialWindow.title.toString();
			if( result == "" ) result = name;
			return result;
		}

		/** Defaults to STANDARD */
		public function get initialWindowSystemChrome():String 
		{
			default xml namespace = m_defaultNs;
			var systemChromeString:String = m_xml.initialWindow.systemChrome.toString(); 
			var result:String = NativeWindowSystemChrome.STANDARD;

			switch( systemChromeString )
			{
				// accept valid entries
			case NativeWindowSystemChrome.STANDARD:
			case NativeWindowSystemChrome.NONE:
				result = systemChromeString;
			}

			return result;
		}

		private function stringToBoolean_defaultTrue( str:String ):Boolean
		{
			switch( str ) {
       			case "":        
				case "true":
				case "1":
					return true;
		
				case "false":
				case "0":
					return false;
			}
			return true;
		}

		private function stringToBoolean_defaultFalse( str:String ):Boolean
		{
			switch( str ) {
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


		public function get initialWindowTransparent():Boolean
		{
			default xml namespace = m_defaultNs;
			return stringToBoolean_defaultFalse( m_xml.initialWindow.transparent.toString() );
		}

		public function get initialWindowVisible():Boolean
		{
			default xml namespace = m_defaultNs;
			return stringToBoolean_defaultFalse( m_xml.initialWindow.visible.toString() );
		}

		public function get initialWindowMinimizable():Boolean
		{
			default xml namespace = m_defaultNs;
			return stringToBoolean_defaultTrue( m_xml.initialWindow.minimizable.toString() );
		}

		public function get initialWindowMaximizable():Boolean
		{
			default xml namespace = m_defaultNs;
			return stringToBoolean_defaultTrue( m_xml.initialWindow.maximizable.toString() );
		}
			
		public function get initialWindowResizable():Boolean
		{
			default xml namespace = m_defaultNs;
			return stringToBoolean_defaultTrue( m_xml.initialWindow.resizable.toString() );
		}
		public function get initialWindowCloseable():Boolean
		{
			default xml namespace = m_defaultNs;
			return stringToBoolean_defaultTrue( m_xml.initialWindow.closeable.toString() );
		}

		public function get initialWindowWidth():Number
		{
			default xml namespace = m_defaultNs;
			return convertDimension( m_xml.initialWindow.width.toString() );
		}
		
		public function get initialWindowHeight():Number
		{
			default xml namespace = m_defaultNs;
			return convertDimension( m_xml.initialWindow.height.toString() );
		}

		public function get initialWindowX():Number
		{
			default xml namespace = m_defaultNs;
			return convertLocation( m_xml.initialWindow.x.toString() );
		}
		
		public function get initialWindowY():Number
		{
			default xml namespace = m_defaultNs;
			return convertLocation( m_xml.initialWindow.y.toString() );
		}

		public function get initialWindowMinSize(): Point
		{
			default xml namespace = m_defaultNs;
			return convertDimensionPoint( m_xml.initialWindow.minSize.toString() );
		}

		public function get initialWindowMaxSize():Point
		{
			default xml namespace = m_defaultNs;
			return convertDimensionPoint( m_xml.initialWindow.maxSize.toString() );
		}

        public function get installFolder():String
        {
			default xml namespace = m_defaultNs;
            return m_xml.installFolder.toString();
        }

        public function get programMenuFolder():String
        {
			default xml namespace = m_defaultNs;
            return m_xml.programMenuFolder.toString();
        }


		/** 
		 * Iterate property names to get the corresponding names of the app
		 * descriptor elements. Associated property values are the width and
		 * height of the corresponding image.
		 */
		 
		public static const ICON_IMAGES:Object = { 
		    image16x16: 16, 
		    image32x32: 32, 
		    image48x48: 48, 
		    image128x128: 128
		};
		
		/**
		 * Returns the package path to the 16x16 icon image, if any. The icon paths are 
		 * the values inside the <icon> block:
		 *   ...
		 *   <icon>
		 *     <image16x16>path/to/16x16.gif</image16x16>
		 *     <image32x32>path/to/32x32.png</image32x32>
		 *     ...
		 *   </icon>
		 *   ...
		 */
		 
		public function getIcon( size:String ):String
		{
			default xml namespace = m_defaultNs;
		    return m_xml.icon.elements( new QName( m_defaultNs, size )).toString();
		}

        public function hasCustomUpdateUI():Boolean {
			default xml namespace = m_defaultNs;
			return stringToBoolean_defaultFalse( m_xml.customUpdateUI.toString());
        }

        public function get fileTypes():XMLList
		{
			default xml namespace = m_defaultNs;
            return m_xml.fileTypes.elements();
		}

		public function validate():void
		{
			default xml namespace = m_defaultNs;

			if( filename == "" ) {
				throw new Error( "application filename must have a non-empty value." );
            }
			// check for the 2.5 change (versionNumber instead of version)
			if (m_xml.versionNumber != undefined) {
				if( version == "") {
					throw new Error( "versionNumber must have a non-empty value." );
				}
				// version has to be of format <0-999>.<0-999>.<0-999>
				if( !(/^[0-9]{1,3}(\.[0-9]{1,3}){0,2}$/.test( version ) ) ) {
					throw new Error( "versionNumber contains an invalid value." );
				}
			}
			else {
				if( version == "") {
					throw new Error( "version must have a non-empty value." );
				}
			}

            // The application cannot begin with a ' ' (space), have any of these characters: *"/:<>?\|, and end with a . (dot) or ' ' (space).
    		if( !( /^([^\*\"\/:<>\?\\\|\. ]|[^\*\"\/:<>\?\\\| ][^\*\"\/:<>\?\\\|]*[^\*\"\/:<>\?\\\|\. ])$/.test( filename ))) {
				throw new Error( "invalid application filename" );
			}

			if( m_xml.initialWindow.content.toString() == "" ) {
				throw new Error( "initialWindow/content must have a non-empty value." );
			}

            // The install and program menu folders cannot begin with a / (forward-slash) or a ' ' (space),
            // have any of these characters: *":<>?\|, and end with a . (dot) or ' ' (space)

            if( ( installFolder != "" ) &&
                !( /^([^\*\"\/:<>\?\\\|\. ]|[^\*\"\/:<>\?\\\| ][^\*\":<>\?\\\|]*[^\*\":<>\?\\\|\. ])$/.test( installFolder))) {
				throw new Error( "invalid install folder" );
			}

            if( ( programMenuFolder != "" ) &&
                !( /^([^\*\"\/:<>\?\\\|\. ]|[^\*\"\/:<>\?\\\| ][^\*\":<>\?\\\|]*[^\*\":<>\?\\\|\. ])$/.test( programMenuFolder))) {
				throw new Error( "invalid program menu folder" );
			}

			if( [ "", NativeWindowSystemChrome.NONE, NativeWindowSystemChrome.STANDARD ].indexOf( m_xml.initialWindow.systemChrome.toString()) == -1 ) {
				throw new Error( "Illegal value \"" + m_xml.initialWindow.systemChrome.toString() + "\" for application/initialWindow/systemChrome." );
			}
			if( [ "", "true", "false", "1", "0" ].indexOf( m_xml.initialWindow.transparent.toString()) == -1 ) {
				throw new Error( "Illegal value \"" + m_xml.initialWindow.transparent.toString() + "\" for application/initialWindow/transparent." );
			}
			if( [ "", "true", "false", "1", "0" ].indexOf( m_xml.initialWindow.visible.toString()) == -1 ) {
				throw new Error( "Illegal value \"" + m_xml.initialWindow.visible.toString() + "\" for application/initialWindow/visible." );
			}
			if( [ "", "true", "false", "1", "0" ].indexOf( m_xml.initialWindow.minimizable.toString()) == -1 ) {
				throw new Error( "Illegal value \"" + m_xml.initialWindow.minimizable.toString() + "\" for application/initialWindow/minimizable." );
			}
			if( [ "", "true", "false", "1", "0" ].indexOf( m_xml.initialWindow.maximizable.toString()) == -1 ) {
				throw new Error( "Illegal value \"" + m_xml.initialWindow.maximizable.toString() + "\" for application/initialWindow/maximizable." );
			}
			if( [ "", "true", "false", "1", "0" ].indexOf( m_xml.initialWindow.resizable.toString()) == -1 ) {
				throw new Error( "Illegal value \"" + m_xml.initialWindow.resizable.toString() + "\" for application/initialWindow/resizeable." );
			}
			if( [ "", "true", "false", "1", "0" ].indexOf( m_xml.initialWindow.closeable.toString()) == -1 ) {
				throw new Error( "Illegal value \"" + m_xml.initialWindow.closeable.toString() + "\" for application/initialWindow/closeable." );
			}

			if ( initialWindowTransparent && (initialWindowSystemChrome != NativeWindowSystemChrome.NONE) )
			{
				throw new Error("Illegal window settings.  Transparent windows are only supported when systemChrome is set to \"none\".");
			}
			
			if ( ! validateDimension( m_xml.initialWindow.width.toString() ) )
			{
				throw new Error( "Illegal value \"" + m_xml.initialWindow.width.toString() + "\" for application/initialWindow/width." );
			}
			
			if ( ! validateDimension( m_xml.initialWindow.height.toString() ) )
			{
				throw new Error( "Illegal value \"" + m_xml.initialWindow.height.toString() + "\" for application/initialWindow/height." );
			}

			if ( ! validateLocation( m_xml.initialWindow.x.toString() ) )
			{
				throw new Error( "Illegal value \"" + m_xml.initialWindow.x.toString() + "\" for application/initialWindow/x." );
			}
			
			if ( ! validateLocation( m_xml.initialWindow.y.toString() ) )
			{
				throw new Error( "Illegal value \"" + m_xml.initialWindow.y.toString() + "\" for application/initialWindow/y." );
			}
			
			if ( ! validateDimensionPair( m_xml.initialWindow.minSize.toString() ) )
			{
				throw new Error( "Illegal value \"" + m_xml.initialWindow.minSize.toString() + "\" for application/initialWindow/minSize." );
			}
			
			if ( ! validateDimensionPair( m_xml.initialWindow.maxSize.toString() ) )
			{
				throw new Error( "Illegal value \"" + m_xml.initialWindow.maxSize.toString() + "\" for application/initialWindow/maxSize." );
			}
			
			if ( ! validateLocalizedText( m_xml.name, m_defaultNs ) )
			{
				throw new Error( "Illegal values for application/name." );
			}
			
			if ( ! validateLocalizedText( m_xml.description, m_defaultNs ) )
			{
				throw new Error( "Illegal values for application/description." );
			}

			// This pattern matches the set of characters permitted in Uniform Type Identifiers, as
			// defined in the Carbon Data Management Guide (see http://developer.apple.com). It is
			// additionally limited to 212 characters so that it can be concatenated with the pub id
			// and used as a filename.

			if( !( /^[A-Za-z0-9\-\.]{1,212}$/.test( id ))) {
				throw new Error( "invalid application identifier" );
			}
		}

		private static function convertDimension( dimensionString : String ) : Number
		{
			var result : Number = -1;
			if ( dimensionString.length > 0 )
			{
				var dimensionUINT : uint = uint( dimensionString );
				result = Number( dimensionUINT );
			}
			return result;
		}

		private static function convertLocation( locationString : String ) : Number
		{
			var result : Number = -1;
			if ( locationString.length > 0 )
			{
				var locationINT : int = int( locationString );
				result = Number( locationINT );
			}
			return result;
		}

		private static function convertDimensionPoint( dimensionString : String ) : Point
		{
			var result : Point = null;
			if ( dimensionString.length > 0 )
			{
				try {
					var list : Array = dimensionString.split(/ +/);
					if ( list.length == 2 )
					{
						var x : Number = convertDimension( String( list[0] ) );
						var y : Number = convertDimension( String( list[1] ) );
						var pt : Point = new Point();
						pt.x = x;
						pt.y = y;
						
						result = pt;
					}
				}
				catch( e : Error )
				{
					// feh. error.  conversion failed
					result = null;
				}

			}
			return result;
		}

		private static function validateDimension( dimensionString : String ) : Boolean
		{
			var result : Boolean = false;
			if ( dimensionString.length > 0 )
			{
				try
				{
					var dimensionNumber : Number = Number( dimensionString );
					if ( dimensionNumber >= 0 )
					{
						result = true;
					}
				}
				catch( theException : * )
				{
					result = false;dimensionString
				}
			}
			else
			{
				result = true;
			}
			return result;
		}

		private static function validateDimensionPair( inputString : String ) : Boolean
		{
			var result : Boolean = false;
			if ( inputString.length > 0 )
			{
				var pt : Point = convertDimensionPoint( inputString );
				if ( (pt != null) &&
					 (pt.x != -1) && (pt.y != -1 ))
				{
					result = true;
				}
			}
			else
			{
				result = true;
			}
			return result;
		}

		private static function validateLocation ( inputString : String ) : Boolean
		{
			var result : Boolean = false;
			if ( inputString.length > 0 )
			{
				try
				{
					var dimensionNumber : Number = Number( inputString );
					if ( !isNaN( dimensionNumber ) )
					{
						result = true;
					}
				}
				catch( theException : * )
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
		
		private static function validateLocalizedText( elem:XMLList, ns:Namespace ) : Boolean
		{
			var xmlNS:Namespace = new Namespace("http://www.w3.org/XML/1998/namespace");
			
			// See if element contains simple content
			if ( elem.hasSimpleContent() )
				return true;

			if ( elem.length() > 1 )
			{
				// XMLList contains more than one element - ie. there is more than one
				// <name> or <description> element. This is invalid.
				return false;
			}

			// Iterate through all children of the element
			var elemChildren:XMLList = elem.*;
			for each (var child:XML in elemChildren)
			{
				if ( child.name() == null || child.name().localName != "text" )
				{
					// If any element is not <text>, it's not valid
					return false;
				}

				if ( (child.@xmlNS::lang).length() == 0 )
				{
					// If any <text> element does not contain "xml:lang" attribute, it's not valid
					return false;
				}
				
				if ( !child.hasSimpleContent() )
				{
					// If any <text> element contains more than simple content, it's not valid
					return false;
				}
			}
			
			return true;
		}
		
		private var m_xml:XML;
		private var m_defaultNs:Namespace;
		private var m_name:String;
		private var m_description:String;
	}
}
	
