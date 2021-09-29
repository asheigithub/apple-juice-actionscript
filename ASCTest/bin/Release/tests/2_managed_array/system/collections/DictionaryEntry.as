package system.collections 
{
	import system._Object_;
	
	[struct]
	[link_system]
	public final class DictionaryEntry extends _Object_ 
	{
		
		[creator];
		[native, _system_DictionaryEntry_creator__];
		private static function _creator(type:Class):*;
		
		[native,_system_DictionaryEntry_ctor_]
		public function DictionaryEntry(key:Object,value:*);
		
		[native,_system_DictionaryEntry_getkey_]
		public function get key():*;
		
		[native,_system_DictionaryEntry_setkey_]
		public function set key(value:Object):void;
		
		
		[native,_system_DictionaryEntry_getvalue_]
		public function get value():*;
		
		[native,_system_DictionaryEntry_setvalue_]
		public function set value(value:*):void
		
	}

}