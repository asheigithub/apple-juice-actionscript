/*
ADOBE SYSTEMS INCORPORATED
 Copyright 2008 Adobe Systems Incorporated
 All Rights Reserved.

NOTICE:  Adobe permits you to use, modify, and distribute this file in accordance with the 
terms of the Adobe license agreement accompanying it.  If you have received this file from a 
source other than Adobe, then your use, modification, or distribution of it requires the prior 
written permission of Adobe.
*/

package com.adobe.utils
{
	/**
	* 	Class that contains static locale sorting function
	* 
	* 	@langversion ActionScript 3.0
	*	@playerversion Flash 9.0
	*	@tiptext
	*
	*/
	public final class LocaleUtil
	{
		/**
		*	Sorts a list of locales using the order specified by the user preferences.
		* 
		* 	@param appLocales A list of locales supported by the application.
		* 	@param systemPreferences The locale chain of user preffered locales.
		* 	@param fallbackLocale The ultimate fallback locale that will be used when no locale from systemPreference matches 
		* 								  a locale from application supported locale list.
		* 	@param keepAll When true, adds all the non-matching locales at the end of the result list preserving the given order.
		*
		*	@return A locale chain that matches user preferences order. 
		*
		* 	@langversion ActionScript 3.0
		*	@playerversion Flash 9.0
		*	@tiptext
		*/
		public static function sortLanguagesByPreference(appLocales:Array, systemPreferences:Array, fallbackLocale:String=null, keepAll:Boolean=false):Array{
			var result:Array = [];
			var haveLocale:Object = {};
			
			var i:int, j:int, l:int, k:int;
			var locale:String;
		
			
			function prepare(list:Array):Array{
				var resultList:Array = []; 
				for(var i:int=0,l:int=list.length;i<l;i++) {
					resultList.push (list[i].toLowerCase().replace(/-/g,'_'));
				}
				return resultList;
			}
			
			var locales:Array = prepare(appLocales);
			var preferenceLocales:Array = prepare(systemPreferences);
		
			if(fallbackLocale&&fallbackLocale!=''){
				fallbackLocale = fallbackLocale.toLowerCase().replace(/-/g,'_');
				var found:Boolean = false;
				for(i=0, l=preferenceLocales.length; i<l; i++){
					if(preferenceLocales[i]==fallbackLocale){
						found = true;
					}
				}
				if(!found){
					preferenceLocales.push(fallbackLocale);
				}
			}
			
			for(j=0, k=locales.length; j<k; j++){
				haveLocale[ locales[j] ] = j;
			}
			
			function promote(locale:String):void{
				if(typeof haveLocale[locale]!='undefined'){
					result.push( appLocales[ haveLocale[locale] ] );
					delete haveLocale[locale];
				}
			}
			
			
			for(i=0, l=preferenceLocales.length; i<l; i++){
				var plocale:LocaleId = LocaleId.fromString( preferenceLocales[i] );
				
				// step 1: promote the perfect match
				promote(preferenceLocales[i]);
		
				promote(plocale.toString());
		       
				// step 2: promote the parent chain
				while(plocale.transformToParent()){
					promote(plocale.toString());
				}
				
				//parse it again
			    plocale = LocaleId.fromString( preferenceLocales[i] );
			    // step 3: promote the order of siblings from preferenceLocales
				for(j=0; j<l; j++){
					locale = preferenceLocales[j];
					if( plocale.isSiblingOf ( LocaleId.fromString( locale ) ) ){
						promote(locale);
					}
				}				
				// step 4: promote all remaining siblings (aka not in preferenceLocales)
				for(j=0, k=locales.length; j<k; j++){
					locale = locales[j];
					if( plocale.isSiblingOf( LocaleId.fromString( locale ) ) ){
						promote( locale );
					}
				}
				
			}
			if(keepAll){
				// check what locales are not already loaded and push them. 
				// using the "for" because we want to preserve order
				for(j=0, k=locales.length; j<k; j++){
					promote(locales[j]);
				}
			}
			return result;
		} 

	}
}