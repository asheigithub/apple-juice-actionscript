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
		[native, _system_collections_ilist_getThisItem_];
		public function getThisItem(index:int):*;
		
		[set_this_item];
		[native, _system_collections_ilist_setThisItem_];
		public function setThisItem(value:*, index:int):void;
		
		[native,_system_collections_ilist_isFixedSize_]
		public function get isFixedSize():Boolean;
		
		[native,_system_collections_ilist_isReadOnly_]
		public function get isReadOnly():Boolean;
		
		[native,_system_collections_ilist_add_]
		public function add(value:*):int;
		
		[native,_system_collections_ilist_clear_]
		public function clear():void;
		
		[native,_system_collections_ilist_contains_]
		public function contains(value:*):Boolean;
		
		[native,_system_collections_ilist_indexOf_]
		public function indexOf(value:*):int;
		
		[native,_system_collections_ilist_insert_]
		public function insert(index:int, value:*):void;
		
		[native,_system_collections_ilist_remove_]
		public function remove(value:*):void;
		
		[native,_system_collections_ilist_removeAt_]
		public function removeAt(index:int):void;
		
		[native,system_collections_icollection_count]
		public function get count():int;
		
		[native,system_collections_icollection_copyto]
		public function copyTo(array:_Array_, index:int):void;
		
		[native,system_collections_ienumerable_getenumerator_]
		public function getEnumerator():_IEnumerator_;
		
		[native,system_collections_arraylist_capcaity]
		public function get capcaity():int;
		
		[native,system_collections_arraylist_addrange]
		public function addRange(c:ICollection):void;
	}

}