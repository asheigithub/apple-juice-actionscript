/*
ADOBE SYSTEMS INCORPORATED
Copyright 2010 Adobe Systems Incorporated. All Rights Reserved.

NOTICE:   Adobe permits you to modify and distribute this file only in accordance with
the terms of Adobe AIR SDK license agreement.  You may have received this file from a
source other than Adobe.  Nonetheless, you may modify or distribute this file only in 
accordance with such agreement.
*/

package air.update.ui
{
	import air.update.logging.Logger;
	
	import flash.events.Event;
	import flash.events.EventDispatcher;
	import flash.system.ApplicationDomain;

	public class ResourceManagerReflection extends EventDispatcher
	{
		private static var logger:Logger = Logger.getLogger("air.update.utils.ResourceManagerReflection");
		
		private static const RESOURCE_MANAGER_CLASS_NAME : String = "mx.resources.ResourceManager";
		
		private var _resourceManager : Object = null;

		private static var _instance:ResourceManagerReflection = null;
		
		public function ResourceManagerReflection(classLock : Class)
		{
			if (classLock != SingletonLock)
				return;
			
			var currentDomain : ApplicationDomain = ApplicationDomain.currentDomain;
			if (currentDomain.hasDefinition(RESOURCE_MANAGER_CLASS_NAME)) { 
				var ResourceManagerClass : Object = currentDomain.getDefinition(RESOURCE_MANAGER_CLASS_NAME);
				
				if (ResourceManagerClass && ResourceManagerClass is Class) {
					var rmGetInstance : Function = getFunction("getInstance", ResourceManagerClass);
					_resourceManager = rmGetInstance();
					
					addListeners();
				} else {
					logger.warning("ResourceManager class definition is null or not a class");
				}
			} else {
				logger.warning("Could not find definition: " + RESOURCE_MANAGER_CLASS_NAME);
			}
		}
		
		private function addListeners() : void {
			var rmAddEventListener : Function = getResourceManagerFunction("addEventListener");
			
			rmAddEventListener(Event.CHANGE, dispatchEvent);
		}
		
		public static function getInstance() : ResourceManagerReflection {
			if (!_instance)
				_instance = new ResourceManagerReflection(SingletonLock);

			return _instance;
		}
		
		public function hasResourceManager() : Boolean {
			// Since this is an instance function, the constructor has been called
			// If _resourceManager is still null, the ResourceManager class was not found 
			var result : Boolean = Boolean(_resourceManager);
			logger.info("hasResourceManager: " + result);
			
			return result;
		}
		
		// If the selfObject does not have a property with the name functionName
		// or this property is not a Function object, return a function that does nothing
		// selfObject must not be null
		private static function getFunction(functionName : String, selfObject : Object) : Function {
			var resultFunction : Function = function(...args) : * {
				logger.info(functionName + " not found. Empty (stub) function called.");
			};
			
			if (selfObject && selfObject.hasOwnProperty(functionName) && selfObject[functionName] is Function)
				resultFunction = selfObject[functionName];
			else
				logger.warning("Could not find member function - " + functionName + " - on object - " + selfObject);

			return resultFunction;
		}
		
		// Returns a method of the ResourceManager.
		// Not meant for external use, but extreme cases may appear
		private function getResourceManagerFunction(functionName : String) : Function {
			return getFunction(functionName, _resourceManager);
		}
		
		private static function getProperty(propertyName : String, selfObject : Object) : * {
			var resultProperty : * = null;
			
			if (selfObject && selfObject.hasOwnProperty(propertyName))
				resultProperty = selfObject[propertyName];
			else
				logger.warning("Could not find property - " + propertyName + " on object - " + selfObject);
			
			return resultProperty;
		}
		
		// Returns a property of the ResourceManager.
		// Not meant for external use, but extreme cases may appear
		private function getResourceManagerProperty(propertyName : String) : * {
			return getProperty(propertyName, _resourceManager);
		}
		
		public function getLocales() : Array {
			var rmGetLocales : Function = getResourceManagerFunction("getLocales");
			return rmGetLocales();
		}
		
		// Original: getResourceBundle(locale:String, bundleName:String):IResourceBundle
		private function getResourceBundle(locale:String, bundleName:String) : Object {
			var rmGetResourceBundle : Function = getResourceManagerFunction("getResourceBundle");
			return rmGetResourceBundle(locale, bundleName);
		}
		
		public function getResourceBundleContent(locale:String, bundleName:String) : Object {
			var content : Object = null;

			var resourceBundle : Object = getResourceBundle(locale, bundleName);
			if (resourceBundle)
				content = getProperty("content", resourceBundle);
			
			return content;
		}
		
		public function get localeChain() : Array {
			var localeChain : Array = getResourceManagerProperty("localeChain");
			if (!localeChain)
				localeChain = [];
			
			return localeChain;
		}
	}
}

class SingletonLock { }