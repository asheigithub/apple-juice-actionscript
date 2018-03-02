/*
ADOBE SYSTEMS INCORPORATED
 Copyright 2008 Adobe Systems Incorporated
 All Rights Reserved.

NOTICE:  Adobe permits you to use, modify, and distribute this file in accordance with the 
terms of the Adobe license agreement accompanying it.  If you have received this file from a 
source other than Adobe, then your use, modification, or distribution of it requires the prior 
written permission of Adobe.
*/

package air.update.locale
{
	import com.adobe.utils.LocaleUtil;
	
	import flash.events.Event;
	import flash.events.EventDispatcher;
	
	import mx.resources.IResourceManager;
	import mx.resources.ResourceManager;
	import mx.utils.StringUtil;
	
	public class UpdaterResourceManager extends EventDispatcher
	{
		private var _localeChain:Array = [];
		private var _applicationLocaleChain:Array = []; 
		private var _initialized:Boolean = false;
		private var _ultimateFallbackLocale:String = "en_US";
		private var _resourceBundleName:String = "ApplicationUpdaterDialogs";
		
		public function UpdaterResourceManager()
		{
			_applicationLocaleChain = ResourceManager.getInstance().localeChain;
			ResourceManager.getInstance().addEventListener(Event.CHANGE, onChange);
		}
		
		private function onChange(ev:Event):void{
			_applicationLocaleChain = ResourceManager.getInstance().localeChain;
			initialize();
		}
		
		public function set localeChain(chain:Array):void{
			_applicationLocaleChain = chain;
			initialize();
		}
		public function get localeChain():Array{
			if(!_initialized){
				initialize();
			}
			return _localeChain;
		}
		
		public function initialize():void{
			_localeChain = LocaleUtil.sortLanguagesByPreference(getUpdaterBundles(), _applicationLocaleChain, _ultimateFallbackLocale, false);
			_initialized = true;
			dispatchEvent(new Event(Event.CHANGE));
		}
		
		private function getUpdaterBundles():Array{
			var resourceManager:IResourceManager = ResourceManager.getInstance(); 
			var locales:Array = resourceManager.getLocales();
			var result:Array = [];
			for(var i:int=0; i < locales.length; i++){
				if( resourceManager.getResourceBundle(locales[i], _resourceBundleName) ){
					result.push(locales[i]);
				}
			}
			return result;
		}
		
		[Bindable("change")]
		public function getString(resourceName:String, parameters:Array = null):String{
			if(!_initialized){
				initialize();
			}
			var resourceManager:IResourceManager = ResourceManager.getInstance();
			for(var i:int=0; i < _localeChain.length; i++){
				var localizedObject:Object = resourceManager.getObject(_resourceBundleName, resourceName, _localeChain[i]);
				if(localizedObject!=null){
					var localizedString:String = String(localizedObject);
					if(parameters){
						localizedString = StringUtil.substitute(localizedString, parameters);
					}	
					return localizedString;
				}
			}
			return null;
		}
		
		public function set ultimateFallbackLocale(locale:String):void{
			_ultimateFallbackLocale = locale;
			initialize();
		}
		
		public function get ultimateFallbackLocale():String{
			return _ultimateFallbackLocale;
		}
		
		public function set resourceBundleName(bundleName:String):void{
			_resourceBundleName = bundleName;
		}
		
		public function get resourceBundleName():String{
			return _resourceBundleName;
		}
		
	}
}