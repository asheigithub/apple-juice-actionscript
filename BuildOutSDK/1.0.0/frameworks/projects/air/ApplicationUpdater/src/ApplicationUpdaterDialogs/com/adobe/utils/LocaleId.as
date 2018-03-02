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
	import flash.utils.Dictionary;
	
	internal final class LocaleId
	{
		
		private var lang:String = '';
		private var script:String = '';
		private var region:String = '';
		private var extended_langs:Array = [];
		private var variants:Array = [];
		private var extensions:Object = {};
		private var privates:Array = [];
		private var privateLangs:Boolean = false; 
	
		public function LocaleId()
		{
		}
		
		
		public static function fromString(str:String):LocaleId{
			var localeId:LocaleId = new LocaleId();
		
			var state:int = LocaleParserState.PRIMARY_LANGUAGE;
			var subtags:Array = str.replace(/-/g, '_').split('_');
		    
			var last_extension:Array;
		
			for(var i:int=0, l:int=subtags.length; i<l ;i++){
				var subtag:String = subtags[i].toLowerCase();
				
				if(state==LocaleParserState.PRIMARY_LANGUAGE){
					if(subtag=='x'){
						localeId.privateLangs = true; //not used in our implementation, but makes the tag private
					}else if(subtag=='i'){
						localeId.lang += 'i-';	//and wait the next subtag to complete the language name
					}else{
						localeId.lang += subtag;
						state ++;
					}
				}else{
					//looging for an extended language 	- 3 chars
					//			   a script 			- 4 chars
					//			   a region				- 2-3 chars
					//			   a variant			- alpha with at least 5 chars or numeric with at least 4 chars
					//			  an extension/private singleton - 1 char
					
					var subtag_length:int = subtag.length; //store it for faster use later
					if(subtag_length==0) continue; //skip zero-lengthed subtags
					var firstChar:String = subtag.charAt(0).toLowerCase();
					
					if(state<=LocaleParserState.EXTENDED_LANGUAGES && subtag_length==3){
					    localeId.extended_langs.push(subtag);
						if(localeId.extended_langs.length==3){ //allow a maximum of 3 extended langs
							state = LocaleParserState.SCRIPT;
						}
					}else if ( state <= LocaleParserState.SCRIPT && subtag_length==4 ){
						localeId.script = subtag;
						state = LocaleParserState.REGION;
					}else if( state <= LocaleParserState.REGION && (subtag_length==2 || subtag_length==3) ){
						localeId.region = subtag;
						state = LocaleParserState.VARIANTS;
					}else if ( state <= LocaleParserState.VARIANTS && 
							( 
								( firstChar>='a' && firstChar<='z' && subtag_length>=5 ) 
														|| 
								( firstChar>='0' && firstChar<='9' && subtag_length>=4 ) 
							)
					  ){
						//variant
						localeId.variants.push(subtag);
						state = LocaleParserState.VARIANTS;
					}else if ( state < LocaleParserState.PRIVATES && subtag_length==1 ){ //singleton
						if(subtag == 'x'){
							state = LocaleParserState.PRIVATES;
							last_extension = localeId.privates;
						} else { 
							state = LocaleParserState.EXTENSIONS;
							last_extension = localeId.extensions[subtag] || [];
							localeId.extensions[subtag] = last_extension;
						}
					}else if(state >= LocaleParserState.EXTENSIONS){
						last_extension.push(subtag);
					}
				}
			}
			localeId.canonicalize();
			return localeId; 
		}
		
		public function canonicalize():void{
			for(var i:String in this.extensions){
				if(this.extensions.hasOwnProperty(i)){
					//also clear zero length extensions
					if(this.extensions[i].length==0) delete this.extensions[i];
					else this.extensions[i] = this.extensions[i].sort();
				}
			}
			this.extended_langs = this.extended_langs.sort();
			this.variants = this.variants.sort();
			this.privates = this.privates.sort();
			if(this.script == ''){
				this.script = LocaleRegistry.getScriptByLang(this.lang);
			}
			//still no script, check the region
			if(this.script == '' && this.region!=''){
				this.script = LocaleRegistry.getScriptByLangAndRegion(this.lang, this.region);
			}
			
			if(this.region=='' && this.script!=''){
				this.region = LocaleRegistry.getDefaultRegionForLangAndScript(this.lang, this.script);
			}
		}
		
		public function toString():String{
			var stack:Array = [ this.lang ];
			appendElements(stack, this.extended_langs);
			if(this.script!='') stack.push(this.script);
			if(this.region!='') stack.push(this.region);
			appendElements(stack, this.variants);
			for(var i:String in this.extensions){
				if(this.extensions.hasOwnProperty(i)){
					stack.push(i);
					appendElements(stack, this.extensions[i]);
				}
			}
			if(this.privates.length>0){
				stack.push('x');
				appendElements(stack, this.privates);
			}
			return stack.join('_');
		} 
		
		public function equals(locale:LocaleId):Boolean{
			return this.toString() == locale.toString();
		}
		
		public function isSiblingOf(other:LocaleId):Boolean{
			return (this.lang==other.lang&&this.script==other.script);
		}
		
		public function transformToParent():Boolean{
			if(this.privates.length>0){
				this.privates.splice(this.privates.length-1, 1);
				return true;
			}
			
			var lastExtensionName:String = null;
			for(var i:String in this.extensions){
				if(this.extensions.hasOwnProperty(i)){
					lastExtensionName = i;
				}
			}
			if(lastExtensionName){
				var lastExtension:Array = this.extensions[lastExtensionName];
				if(lastExtension.length==1){
					delete this.extensions[ lastExtensionName ];
					return true;
				}
				lastExtension.splice(lastExtension.length-1, 1);
				return true;
			}
			
			if(this.variants.length>0){
				this.variants.splice(this.variants.length-1, 1);
				return true;
			}
	
			if(this.script!=''){
				//check if we can surpress the script
			    if(LocaleRegistry.getScriptByLang(this.lang)!=''){
					this.script='';
					return true;
			    }else if(this.region==''){
					//maybe the default region can surpress the script
					var region:String = LocaleRegistry.getDefaultRegionForLangAndScript(this.lang, this.script);
					if(region!=''){
						this.region = region;
						this.script = '';
						return true;
					}
				}
			}
			
			if(this.region!=''){
				if(!(this.script=='' && LocaleRegistry.getScriptByLang(this.lang) == '')){
					this.region='';
					return true;
				}
			}
			
			
			if(this.extended_langs.length>0){
				this.extended_langs.splice(this.extended_langs.length-1, 1);
				return true;
			}
			
			return false;
		}

		/**
		 *  @private
		*/
		private static function appendElements(dest:Array, src:Array):void
		{
			for (var i:uint=0, argc:uint=src.length; i < argc; i++)
				dest.push(src[i]);
		}


	}
}