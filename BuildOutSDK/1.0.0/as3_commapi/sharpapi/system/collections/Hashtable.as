package system.collections 
{
	import system.ICloneable;
	import system._Array_;
	import system._Object_;
	
	
	[link_system]
	public class Hashtable extends _Object_	implements IDictionary, ICloneable
	{
		[creator];
		[native, _system_Hashtable_creator_]
		private static function _creator(type:Class):*;
		
		[native, _system_Hashtable_ctor_]
		public function Hashtable();
		
		
		[native,system_collections_ICollection_get_Count]
		public function get count():int;
		
		[native,system_collections_ICollection_copyTo]
		public function copyTo(array:_Array_, index:int):void;
		
		[native, system_collections_IDictionary_getEnumerator_]
		public function getEnumerator():IDictionaryEnumerator;
		
		[native,system_collections_IDictionary_get_IsFixedSize]
		public function get isFixedSize():Boolean;
		
		[native,system_collections_IDictionary_get_IsReadOnly]
		public function get isReadOnly():Boolean;
		
		[get_this_item];
		[native, system_collections_IDictionary_getThisItem];
		public function getThisItem(key:Object):*;
		
		[set_this_item];
		[native, system_collections_IDictionary_setThisItem];
		public function setThisItem(value:*, key:Object):void;
		
		[native,system_collections_IDictionary_get_Keys]
		public function get keys():ICollection;
		
		[native,system_collections_IDictionary_get_Values]
		public function get values():ICollection;
		
		[native,system_collections_IDictionary_add]
		public function add(key:Object, value:*):void;
		
		[native,system_collections_IDictionary_clear]
		public function clear():void;
		
		[native,system_collections_IDictionary_contains]
		public function contains(key:Object):Boolean;
		
		[native,system_collections_IDictionary_remove]
		public function remove(key:Object):void;
		
		[native,system_collections_ICollection_get_SyncRoot]
		private function get syncRoot():_Object_;
		
		
		[native,system_collections_ICollection_get_IsSynchronized]
		private function get isSynchronized():Boolean;
		
		
		[native, system_collections_IEnumerable_getEnumerator]
		private function getEnumerator_():_IEnumerator_;
		
		
		[native,system_ICloneable_clone]
		private function clone():_Object_;
		
	}

}