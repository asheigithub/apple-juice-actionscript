package system.collections 
{
	import system._Array_;
	import system._Object_;
	
	
	[link_system]
	public class Hashtable extends _Object_	implements IDictionary
	{
		[creator];
		[native, _system_Hashtable_creator_]
		private static function _creator(type:Class):*;
		
		[native, _system_Hashtable_ctor_]
		public function Hashtable();
		
		
		[native,system_collections_icollection_count]
		public function get count():int;
		
		[native,system_collections_icollection_copyto]
		public function copyTo(array:_Array_, index:int):void;
		
		[native, system_collections_idictionary_getenumerator_]
		public function getEnumerator():_IEnumerator_;
		
		[native,_system_collections_idictionary_isFixedSize_]
		public function get isFixedSize():Boolean;
		
		[native,_system_collections_idictionary_isReadOnly_]
		public function get isReadOnly():Boolean;
		
		[get_this_item];
		[native, _system_collections_idictionary_getThisItem_];
		public function getThisItem(key:Object):*;
		
		[set_this_item];
		[native, _system_collections_idictionary_setThisItem_];
		public function setThisItem(value:*, key:Object):void;
		
		[native,_system_collections_idictionary_keys_]
		public function get keys():ICollection;
		
		[native,_system_collections_idictionary_values_]
		public function get values():ICollection;
		
		[native,_system_collections_idictionary_add_]
		public function add(key:Object, value:*):void;
		
		[native,_system_collections_idictionary_clear_]
		public function clear():void;
		
		[native,_system_collections_idictionary_contains_]
		public function contains(key:Object):Boolean;
		
		[native,_system_collections_idictionary_remove_]
		public function remove(key:Object):void;
		
		
	}

}