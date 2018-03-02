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
	import air.update.descriptors.StateDescriptor;
	import air.update.logging.Logger;
	import air.update.utils.FileUtils;
	import air.update.utils.Constants;
	import air.update.utils.FileUtils;
	import flash.filesystem.*;
	
	import flash.filesystem.File;
	
	[ExcludeClass]
	public class UpdaterState
	{
		private static var logger:Logger = Logger.getLogger("air.update.core.UpdaterState");
		
		private var _descriptor:StateDescriptor;
		
		public function UpdaterState()
		{
		}
		
		public function get descriptor():StateDescriptor
		{
			return _descriptor;
		}
		
		public function resetUpdateData():void
		{
			descriptor.currentVersion = "";
			descriptor.previousVersion = "";
			descriptor.storage = null;
			FileUtils.deleteFile(FileUtils.getLocalUpdateFile());
			FileUtils.deleteFile(FileUtils.getLocalDescriptorFile());			
			descriptor.updaterLaunched = false;
		}
		
		public function removePreviousStorageData(previousStorage:File):void
		{
			if (!previousStorage || !previousStorage.exists)
			{
				return;
			}
			var file:File = previousStorage.resolvePath(Constants.UPDATER_FOLDER + "/" + Constants.STATE_FILE);
			FileUtils.deleteFile(file);
			file = previousStorage.resolvePath(Constants.UPDATER_FOLDER + "/" + Constants.UPDATE_LOCAL_FILE);
			FileUtils.deleteFile(file);
			file = previousStorage.resolvePath(Constants.UPDATER_FOLDER + "/" + Constants.DESCRIPTOR_LOCAL_FILE);
			FileUtils.deleteFile(file);
			file = previousStorage.resolvePath(Constants.UPDATER_FOLDER);
			FileUtils.deleteFolder(file);
		}
		
		public function addFailedUpdate(version:String):void
		{
			descriptor.addFailedUpdate(version);
		}
		
		public function removeAllFailedUpdates():void
		{
			descriptor.removeAllFailedUpdates();
		}
		
		public function saveToDocuments():void
		{
			var documentsFile:File = FileUtils.getDocumentsStateFile();
			FileUtils.saveXMLToFile(_descriptor.getXML(), documentsFile);
		}
		
		public function saveToStorage():void
		{
			var storageFile:File = FileUtils.getStorageStateFile();
			FileUtils.saveXMLToFile(_descriptor.getXML(), storageFile);
		}
		
		public function load():void
		{
			var xml:XML;
			//
			var storageFile:File = FileUtils.getStorageStateFile();
			var documentsFile:File = FileUtils.getDocumentsStateFile();
			 
			if (!storageFile.exists)
			{
				// try documents folder
				// file doesn't exists in documents
				if (!documentsFile.exists)
				{
					// create a default state
					_descriptor = StateDescriptor.defaultState();
					saveToStorage();
				} else
				{
					// read the state from the documents folder
					try {
						xml = FileUtils.readXMLFromFile(documentsFile);
						_descriptor = new StateDescriptor(xml);
						_descriptor.validate();
						// 
						saveToStorage();
					}catch(e:Error) 
					{
						logger.fine("Invalid state: " + e);
						_descriptor = StateDescriptor.defaultState();
						saveToStorage();
					}
				}
			} else {
				// try to read from storage
				try {
					xml = FileUtils.readXMLFromFile(storageFile);
					_descriptor = new StateDescriptor(xml);
					_descriptor.validate();
				}catch(e:Error) 
				{
					logger.fine("Invalid state: " + e);
					_descriptor = StateDescriptor.defaultState();
					saveToStorage();
				}
			}
			// if the update file is missing
			// someone tamperred. 
			var updateFile:File = FileUtils.getLocalUpdateFile();
			if (descriptor.currentVersion && !updateFile.exists && !descriptor.updaterLaunched) 
			{
				logger.fine("Missing update file");
				_descriptor = StateDescriptor.defaultState();
				saveToStorage();
			}
			FileUtils.deleteFile(documentsFile);			
		}
	}
}