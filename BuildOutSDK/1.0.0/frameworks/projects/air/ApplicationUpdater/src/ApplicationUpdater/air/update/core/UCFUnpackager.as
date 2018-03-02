/*
ADOBE SYSTEMS INCORPORATED
Copyright 2008 Adobe Systems Incorporated. All Rights Reserved.
 
NOTICE:   Adobe permits you to modify and distribute this file only in accordance with
the terms of Adobe AIR SDK license agreement.  You may have received this file from a
source other than Adobe.  Nonetheless, you may modify or distribute this file only in 
accordance with such agreement.
*/

package air.update.core {

	import air.update.states.HSM;
	import air.update.states.HSMEvent;
	import air.update.utils.Constants;
	
	import flash.events.*;
	import flash.filesystem.*;
	import flash.net.*;
	import flash.utils.*;
	
	/** Dispatched as the UCF file is processed. */
	[Event(name="progress", type="flash.events.ProgressEvent")]

	/** Dispatched when the UCF file is completely expanded. */
	[Event(name="complete", type="flash.events.Event")]

	/** 
	 * Dispatched if a validation error occurs while reading the UCF file.
	 * The following errors may be dispatched:
	 *
	 * - If the UCF file does not begin with the correct
	 *   4-byte signature (magic).
	 *
	 * - If the UCF file uses a feature, such as encryption,
	 *   not supported by this implementation.
	 *
	 * - If the UCF file is signed but the signature is not valid.
	 *   (Note: It is not an error if the signature is valid but the
	 *   the signer is not trusted.)
	 */	 
	[Event(name="error", type="flash.events.ErrorEvent")]
	
	/** 
	 * Dispatch if an IO error occurs while reading the UCF file
	 * or writing an expanded file to disk.
	 */
	[Event(name="ioError", type="flash.events.IOErrorEvent")]
	
	/**
	 * Dispatched if unpackaging from an http or https URL. See 
	 * URLStream for further definition.
	 */
	 
	[Event(name="httpStatus", type="flash.events.HTTPStatusEvent")]

	/**
	 * See URLStream for further definition.
	 */
	 
	[Event(name="securityError", type="flash.events.SecurityErrorEvent")]
	
	/**
	 * UCFUnpackager can be used to unpackage the contents of a Univeral
	 * Container Format (UCF) file.
	 */
	[ExcludeClass]
	public class UCFUnpackager extends HSM {
		
		public function UCFUnpackager() {
			super( initialized );
		}
		
		/**
		 * Start unpackaging of the specified file.
		 */
		 
		public function unpackageAsync( url:String ):void {
			identifier = url;
			transitionAsync( unpackaging );
		}
		
		public function get isComplete():Boolean {
			return m_isComplete;
		}
		
		/** 
		 * If set, the directory in which the contents of the UCF file are written.
		 * If not set, the contents are not written to disk.
		 *
		 * If a directory is specified it must already exist. It need not be empty,
		 * but unpackaging will file if overwriting a file in the directory fails.
		 */
		 
		public function set outputDirectory( dir:File ):void {
			m_dir = dir;
		}

		public function get outputDirectory():File {
			return m_dir;
		}
		
		/**
		 * Enable/disable signature validation. Defaults to true. If set,
		 * the UCF file must be signed.
		 */
		 
		public function set enableSignatureValidation( enable:Boolean ):void {
			m_enableSignatureValidation = enable;
		}
		
		/**
		 * Cancel any further processing. It is assumed the client will no longer access
		 * this data after calling cancel().
		 */
		
		public function cancel():void {
			if (source && source.connected) {
				source.close();
				m_ucfParseState = AT_ABORTED;
			}
		}
		
		// Methods for subclasses to override follow
		
		/**
		 * Called during unpackaging each time a new file is processed. Subclasses may override
		 * this in order to take action on each file during unpackaging. If outputDirectory
		 * is set, the file will be written to outputDirectory *before* this method is called.
		 *
		 * If the method returns false, then the unpackager enters the AT_ABORTED state and
		 * further processing stops immediately. To continue processing, return true.
		 */
		 
		protected function onFile( fileNumber:uint, path:String, data:ByteArray ):Boolean {
			return true;
		}
		
		/**
		 * Called during unpackagin each time a new directory is processed. Sublcasses may
		 * override this in order to take action on each directory. If outputDirectory is set,
		 * the directory will have been created *before* this method is called.
		 */
		 
		protected function onDirectory( path:String ):void {
			// nop
		}
		
		/**
		 * Called when all processing is complete and the file has been successfully unpackaged.
		 */
		 
		protected function onDone():void {
			// nop
		}
		
		// Types and methods for subclass-only access follow
		
		// Current state with respect to the UCF file.

		protected static const AT_START       : uint = 0;
		protected static const AT_HEADER      : uint = 1;
		protected static const AT_FILENAME    : uint = 2;
		protected static const AT_EXTRA_FIELD : uint = 3;
		protected static const AT_DATA        : uint = 4;
		protected static const AT_END         : uint = 5;
		protected static const AT_ERROR       : uint = 6;
		protected static const AT_COMPLETE    : uint = 12;
		protected static const AT_ABORTED     : uint = 13;
		
		// states for reading the central directory.
		
		protected static const AT_CDHEADER	  : uint = 7;
		protected static const AT_CDHEADERMAGIC : uint = 8;
		protected static const AT_CDFILENAME    : uint = 9;
		protected static const AT_CDEXTRA_FIELD : uint = 10;
		protected static const AT_CDCOMMENT	  : uint = 11;
		
		protected function get ucfParseState():uint {
			return m_ucfParseState;
		}
		
		// States
		
		private function initialized( event:Event ):void {
			// nop
		}
		
		private function unpackaging( event:Event ):void {
			switch( event.type ) {
				case HSMEvent.ENTER:
					source = new URLStream();
					source.endian = Endian.LITTLE_ENDIAN;
					source.addEventListener( ProgressEvent.PROGRESS, dispatch );
					source.addEventListener( HTTPStatusEvent.HTTP_STATUS, dispatch );
					source.addEventListener( IOErrorEvent.IO_ERROR, dispatch );
					source.addEventListener( SecurityErrorEvent.SECURITY_ERROR, dispatch );
					source.addEventListener( Event.COMPLETE, dispatch );
					source.load( new URLRequest( identifier ));
					break;
					
				case ProgressEvent.PROGRESS:
					onData( event as ProgressEvent );
					break;
					
				case HTTPStatusEvent.HTTP_STATUS:
					dispatchEvent( event.clone());
					break;
					
				case IOErrorEvent.IO_ERROR:
				case SecurityErrorEvent.SECURITY_ERROR:
					m_ucfParseState = AT_ERROR;
					dispatchEvent( event.clone());
					break;
					
				case Event.COMPLETE:
					onComplete( event );
					break;
			}
		}

		private function complete( event:Event ):void {
			switch( event.type ) {
				case HSMEvent.ENTER:
					m_isComplete = true;
					dispatchEvent( new Event( Event.COMPLETE ));
					break;
			}
		}
		
		private function errored( event:Event ):void {
			// nop
		}
		
		// TODO: Break into additional states. Code is old and pre-dates HSM.
		
		private function onData( event:ProgressEvent ):void {
			// The underlying progress from the byte stream is the best estimate
			// we've got of work accomplished and remaining.

			// FIXME: This code needs to be hardened.
			
			dispatchEvent( event.clone());
			
			try {
				const HEADER_SIZE_BYTES : uint = 30;
				const ZIP_LFH_MAGIC : uint = 0x04034b50;
				
				const CDHEADER_SIZE_BYTES : uint = 46;
				const ZIP_CDH_MAGIC : uint = 0x02014b50;
				
				const ZIP_CDSIG_MAGIC : uint = 0x06054b50;

				// When a data event is received we must process as much data as possible because it's possible
				// that this is the only or last data event we'll receive. However, we don't have to read every last
				// byte: if the stream stops in, say, the middle of a header, we can reasonably expect to receive
				// another data event. This would only not happen if the data stream is malformed; in that case
				// we'll know that when we get the complete event.

				var magic : uint;
				var filename : ByteArray;
				while( true ) {
					switch( m_ucfParseState ) {
						case AT_START:
						case AT_HEADER:
							if( source.bytesAvailable < HEADER_SIZE_BYTES ) return;
							
							_currentLFH = new ByteArray();
							_currentLFH.endian = Endian.LITTLE_ENDIAN;						
							
							// Read the magic identifier first to determine where we are. 
							source.readBytes(_currentLFH, 0, 4);
																					
							magic = _currentLFH.readUnsignedInt();
							if( ZIP_LFH_MAGIC != magic ) {
								if( m_ucfParseState == AT_START ) throw new Error( "not an AIR file", Constants.ERROR_UCF_INVALID_AIR_FILE );

								// Everything after the local file header we skip for now.
								// FIXME: Should be more rigorous; fail on unallowed sections.
								
								if (ZIP_CDH_MAGIC == magic) {
									m_ucfParseState = AT_CDHEADERMAGIC;
									break;
								}							
								m_ucfParseState = AT_END;
								return;
							}
							
							source.readBytes(_currentLFH, _currentLFH.length, HEADER_SIZE_BYTES - 4);											
							
							// TODO: Check "version need to extract" field
							var versionNeededToExtract : uint = _currentLFH.readUnsignedShort();
													
							// If bit 3 is set, some header values are in the data
							// descriptor following the file instead of in the file.
							_generalPurposeBitFlags = _currentLFH.readUnsignedShort();
							if(( _generalPurposeBitFlags & 0xFFF9 ) != 0 ) throw new Error( "file uses unsupported encryption or streaming features", Constants.ERROR_UCF_INVALID_FLAGS );

							_compressionMethod = _currentLFH.readUnsignedShort();
							var lastModTime : uint = _currentLFH.readUnsignedShort();
							var lastModDate : uint = _currentLFH.readUnsignedShort();
							
							var crc32 : uint = _currentLFH.readUnsignedInt();													
							_compressedSize = _currentLFH.readUnsignedInt();
							_uncompressedSize = _currentLFH.readUnsignedInt();
							_filenameLength = _currentLFH.readUnsignedShort();
							_extraFieldLength = _currentLFH.readUnsignedShort();
						
							if( _filenameLength == 0 ) throw new Error( "one of the files has an empty (zero-length) name", Constants.ERROR_UCF_INVALID_FILENAME );

							// Fall through
							m_ucfParseState = AT_FILENAME;
							
						case AT_FILENAME:
						
							if( source.bytesAvailable < _filenameLength ) return;
							
							source.readBytes(_currentLFH, _currentLFH.length, _filenameLength);
							
							filename = new ByteArray();
							_currentLFH.readBytes( filename, 0, _filenameLength );

							_path = filename.toString();

							// Now that we have a file name, check some error conditions.

                            // First, make sure files are in a specific order
                            
							if( m_fileCount == 0 && _path != "mimetype" )
							    throw new Error( "mimetype must be the first file", Constants.ERROR_UCF_NO_MIMETYPE );
							
							var DATA_DESCRIPTOR_FLAG : uint = 0x80;
							if( _generalPurposeBitFlags & DATA_DESCRIPTOR_FLAG ) throw new Error( "file " + _path + " uses a data descriptor field", Constants.ERROR_UCF_INVALID_FLAGS );
						
							var COMPRESSION_NONE : uint = 0;
							var COMPRESSION_DEFLATE : uint = 8;
							if( _compressionMethod != COMPRESSION_DEFLATE && _compressionMethod != COMPRESSION_NONE ) {
								throw new Error( "file " + _path + " uses an illegal compression method " + _compressionMethod, Constants.ERROR_UCF_UNKNOWN_COMPRESSION );
							}

							// A directory is defined to be an entry with a name ending in /. Once we
							// know that, however, we strip of the the last / before doing a split.
						
							isDirectory = ( _path.charAt( _path.length - 1 ) == "/" );
							if( isDirectory ) {
								_path = _path.slice( 0, _path.length - 1 );
							}
						
							var elements : Array = _path.split( "/" );
							if( elements.length == 0 ) throw new Error( "it contains a file with an empty name", Constants.ERROR_UCF_INVALID_FILENAME );

							elements.filter( function( item : *, index : int, array : Array ):Boolean {
								if( item == "." ) throw new Error( "filename " + _path + " contains a component of '.'", Constants.ERROR_UCF_INVALID_FILENAME );
								if( item == ".." ) throw new Error( "filename " + _path + " contains a component of '..'", Constants.ERROR_UCF_INVALID_FILENAME );
								if( item == "" ) throw new Error( "filename " + _path + " contains an empty component", Constants.ERROR_UCF_INVALID_FILENAME );
								return true;
							});				
						
							// The name looks valid. Now figure out the list of parent directories for this file, if any.
							// Then notify the listener of any new directories in this path.

							var numParentDirs : int = ( isDirectory ? elements.length : elements.length - 1 );
							var parent : Object = _root;
							var currentPath : Array = new Array();
							
							for( var i : uint = 0; i < numParentDirs; i++ ) {
								var element : String = elements[i];
								currentPath.push( element );
								
								if( parent[element] == null ) {
									parent[element] = new Object();
									onDirectory( currentPath.join( "/" ));
								}

								parent = parent[element];
							}

							// Fall through, as before
							m_ucfParseState = AT_EXTRA_FIELD;
						
						case AT_EXTRA_FIELD:
		
							if( source.bytesAvailable < _extraFieldLength ) return;
						
							if( _extraFieldLength > 0 ) {
							
								// The extra field is discarded, but we still need to hash it. 							
								source.readBytes(_currentLFH, _currentLFH.length, _extraFieldLength);															
							}
							
							// Fall through, as before
							m_ucfParseState = AT_DATA;

						case AT_DATA:
							var sizeToRead : uint = ( _compressionMethod == 8 ? _compressedSize : _uncompressedSize );
							if( source.bytesAvailable < sizeToRead ) return;

							// Note that directory events are dispatched in the AT_FILENAME state, above.
							
							if( isDirectory ) {
								if( _uncompressedSize != 0 ) throw new Error( "directory entry " + _path + " has associated data", Constants.ERROR_UCF_INVALID_FILENAME );
								
								if( m_dir ) {
									m_dir.resolvePath( _path ).createDirectory();
								}

							} else {
								_data = new ByteArray();
								if( sizeToRead > 0 ) {
									source.readBytes( _data, 0, sizeToRead );
									if( _compressionMethod == 8 ) _data.uncompress(CompressionAlgorithm.DEFLATE);
								}
								
								if( m_dir ) {
									_data.position = 0;
									var fs:FileStream = new FileStream();
									fs.open( m_dir.resolvePath( _path ), FileMode.WRITE );
									fs.writeBytes( _data );
									fs.close();
								}

                                if( m_enableSignatureValidation ) {
                                    if( _path == "META-INF/signatures.xml" ) {
                                        m_validator.signatures = _data;
                                    } else {
                                        m_validator.addFile( _path, _data );
                                    }
                                }
								
								var shouldContinue:Boolean = onFile( m_fileCount, _path, _data );
								if( !shouldContinue ) {
									m_ucfParseState = AT_ABORTED;
									break;
								}
							}
							
							// Back to the beginning
							m_fileCount++;
							m_ucfParseState = AT_HEADER;
							break;
						
						case AT_CDHEADER:						
							if( source.bytesAvailable < 4 ) return;
																		
							
							_currentLFH = new ByteArray();
							_currentLFH.endian = Endian.LITTLE_ENDIAN;						
							
							source.readBytes(_currentLFH, 0, 4);
														
							magic = _currentLFH.readUnsignedInt();
							if( ZIP_CDH_MAGIC != magic ) {
								//passed the last CD entry.
								m_ucfParseState = AT_END;
								return;
							}
							m_ucfParseState = AT_CDHEADERMAGIC;						
							
						case AT_CDHEADERMAGIC:
							// at this point, we have read 4 bytes of the CD header. 
							if (source.bytesAvailable < CDHEADER_SIZE_BYTES - 4)
								return;
														
							source.readBytes(_currentLFH, _currentLFH.length, CDHEADER_SIZE_BYTES - 4);
							
							// Don't really care about all the bytes after the CDH magic and 
							// before "file name length" section, so skip those bytes. 
							_currentLFH.position = _currentLFH.position + 24;
													
							_filenameLength = _currentLFH.readUnsignedShort();
							_extraFieldLength = _currentLFH.readUnsignedShort();
							_fileCommentLength = _currentLFH.readUnsignedShort();
							
							// Don't care about the bytes between fileCommentLength and relative offset
							// of local header, so skip those. 
							
							_currentLFH.position = _currentLFH.position + 8;
							_fileRelativeOffset	= _currentLFH.readUnsignedInt();
							
							m_ucfParseState = AT_CDFILENAME;
							
						case AT_CDFILENAME:
							// Same processing as in AT_FILENAME.
							if( source.bytesAvailable < _filenameLength ) return;
							
							source.readBytes(_currentLFH, _currentLFH.length, _filenameLength);
							
							filename = new ByteArray();
							_currentLFH.readBytes( filename, 0, _filenameLength );

							_path = filename.toString();
							
							//TODO: do same error checks as in AT_FILENAME? skipping them for now.
							m_ucfParseState = AT_CDEXTRA_FIELD;
							
						case AT_CDEXTRA_FIELD:
							if( source.bytesAvailable < _extraFieldLength ) return;
						
							if( _extraFieldLength > 0 ) {
							
								// The extra field is discarded, but we still need to hash it. 							
								source.readBytes(_currentLFH, _currentLFH.length, _extraFieldLength);															
							}
							m_ucfParseState = AT_CDCOMMENT;
							
						case AT_CDCOMMENT:
							if( source.bytesAvailable < _fileCommentLength ) return;
						
							if( _fileCommentLength > 0 ) {
							
								// The extra field is discarded, but we still need to hash it. 							
								source.readBytes(_currentLFH, _currentLFH.length, _fileCommentLength);															
							}
														
							// Ready to read the next CD entry.
							m_ucfParseState = AT_CDHEADER;
							break;														
						
						case AT_END:
							// We've passed all of the files but are still receiving data events. Ignore them.
							return;
							
						case AT_ABORTED:
							// Ignore everything until the read completes
							return;
							
						case AT_ERROR:
							// Something has already gone wrong, but we might receive more progress events
							// while waiting for the end. Ignore them.
							return;
					}
				}
			} catch( e:Error ) {
				dispatchError( e );
			}
		}
		
		private function onComplete( event : Event ) : void {
		    try {
    			switch( m_ucfParseState ) {
    				case AT_END:
    					// All is well    					
						onDone();
						m_ucfParseState = AT_COMPLETE;

                        if( m_enableSignatureValidation && m_validator.packageSignatureStatus != 0 ) {
                            dispatchEvent( new ErrorEvent( ErrorEvent.ERROR, false, false, "signature is not valid" ));
                            transition( errored );                        
                        } else {
                            transition( complete );
                        }
    					break;
						
					case AT_ABORTED:
						m_isComplete = true;
						dispatchEvent( new Event( Event.COMPLETE ));
						break;
						
    				case AT_ERROR:
    					// We've already reported an error event
    					break;
						
    				default:
    					// We're in the middle of the stream but out of data. This is an error.
    				    throw new Error( "truncated or corrupt", Constants.ERROR_UCF_CORRUPT_AIR );
    			}
			} catch( e:Error ) {
				dispatchError( e );
			}
		}

		private function dispatchError( error:Error ):void {
			m_ucfParseState = AT_ERROR;
			dispatchEvent( new ErrorEvent( 
				ErrorEvent.ERROR, false, false, error.message, error.errorID
			));
		}
	
		private var m_ucfParseState:uint = AT_START;		
		private var identifier:String;
		private var source:URLStream;
		
        // data about the file/Central Dir entry currently being read

		private var _generalPurposeBitFlags : uint;
        private var _compressionMethod : uint;
		private var _extraFieldLength : uint;
		private var _compressedSize : uint;
		private var _uncompressedSize : uint;
		private var _filenameLength : uint;
		private var _data : ByteArray;
        private var _path : String;
        private var _currentLFH : ByteArray;
		private var isDirectory:Boolean;		
		private var m_fileCount:uint = 0;
        
        // data specific to central dir entries only.
        private var _fileCommentLength : uint;
        private var _fileRelativeOffset : uint;

		// Used to maintain a tree of directories seen.
		private var _root : Object = new Object();
		
		private var m_dir:File;
		private var m_validator:Object = new Object();
		private var m_enableSignatureValidation:Boolean = false;
		private var m_isComplete:Boolean = false;
	}
}
