package system.collections 
{
	import system._Array_;
	import system._Object_;
	
	[link_system]
	public class ArrayList extends _Object_ implements IList
	{
		[native,_system_collections_ArrayList_static_createInstance]
		public static function createInstance(c:ICollection):ArrayList;
		
		[native,_system_collections_ArrayList_static_createInstance_]
		public static function createInstance_(capacity:int):ArrayList;
		
		
		[creator];
		[native, _system_ArrayList_creator_]
		private static function _creator(type:Class):*;
		
		[native, _system_ArrayList_ctor_]
		public function ArrayList();
		
		
		[get_this_item];
		[native, system_collections_IList_getThisItem];
		public function getThisItem(index:int):*;
		
		[set_this_item];
		[native, system_collections_IList_setThisItem];
		public function setThisItem(value:*, index:int):void;
		
		[native,system_collections_IList_get_IsFixedSize]
		public function get isFixedSize():Boolean;
		
		[native,system_collections_IList_get_IsReadOnly]
		public function get isReadOnly():Boolean;
		
		[native,system_collections_IList_add]
		public function add(value:*):int;
		
		[native,system_collections_IList_clear]
		public function clear():void;
		
		[native,system_collections_IList_contains]
		public function contains(value:*):Boolean;
		
		[native,system_collections_IList_indexOf]
		public function indexOf(value:*):int;
		
		[native,system_collections_IList_insert]
		public function insert(index:int, value:*):void;
		
		[native,system_collections_IList_remove]
		public function remove(value:*):void;
		
		[native,system_collections_IList_removeAt]
		public function removeAt(index:int):void;
		
		[native,system_collections_ICollection_get_Count]
		public function get count():int;
		
		[native,system_collections_arraylist_copyto]
		public function copyTo_(array:_Array_):void;
		
		[native,system_collections_ICollection_copyTo]
		public function copyTo(array:_Array_, index:int):void;
		
		[native,system_collections_arraylist_copyto_]
		public function copyTo__(index:int,array:_Array_,arrayIndex:int,count:int):void;
		
		
		[native,system_collections_IEnumerable_getEnumerator]
		public function getEnumerator():_IEnumerator_;
		
		[native,system_collections_arraylist_capcaity]
		public function get capcaity():int;
		
		[native,system_collections_arraylist_addrange]
		public function addRange(c:ICollection):void;
		
		[native,system_collections_arraylist_reverse]
		public function reverse():void;
		
		[native,system_collections_arraylist_sort]
		public function sort():void;
		
		[native,system_collections_ICollection_get_SyncRoot]
		private function get syncRoot():_Object_;
		
		
		[native,system_collections_ICollection_get_IsSynchronized]
		private function get isSynchronized():Boolean;
	}

}