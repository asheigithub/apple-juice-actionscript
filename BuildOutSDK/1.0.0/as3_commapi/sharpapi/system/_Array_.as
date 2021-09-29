package system 
{
	import system._Object_;
	import system.collections.ICollection;
	import system.collections.IList;
	import system.collections._IEnumerator_;
	
	[no_constructor]
	[link_system]
	/**
	 * 托管数组的基类。可创建和操作托管数组
	 */
	public final class _Array_ extends _Object_ implements IList, ICollection
	{
		
		[creator];
		[native, _system_Array_creator_]
		private static function _creator(type:Class):*;
		
		[native,_system_Array_static_createInstance]
		public static function createInstance(elementType:Class,length:int):_Array_;
		
		[native,_system_Array_static_createInstance_]
		public static function createInstance_(elementType:Class,length1:int,length2:int):_Array_;
		
		[native,_system_Array_static_createInstance__]
		public static function createInstance__(elementType:Class, lengths:_Array_):_Array_;
		
		[native, _system_Array_ctor_]
		public function _Array_(length:int);
		
		[native, system_collections_ICollection_get_Count]
		public function get length():int;
		
		[native, _system_Array_rank_]
		public function get rank():int;
		
		[native, _system_Array_getLength_]
		public function getLength(dimension:int):int;
		
		[native, _system_Array_getLowerBound_]
		public function getLowerBound(dimension:int):int; 
		
		[native, _system_Array_getUpperBound_]
		public function getUpperBound(dimension:int):int; 
		
		[get_this_item];
		[native, system_collections_IList_getThisItem];
		public function getValue(index:int):*;
		
		[set_this_item];
		[native, system_collections_IList_setThisItem];
		public function setValue(value:*, index:int):void;
		
		
		[native, _system_Array_getValue__]
		public function getValue_(index1:int,index2:int):*;
		
		[native, _system_Array_setValue__]
		public function setValue_(value:*,index1:int,index2:int):void;
		
		[native, _system_Array_getValue___]
		public function getValue__(indices:_Array_):*;
		
		[native, _system_Array_setValue___]
		public function setValue__(value:*,indices:_Array_):void;
		

		[native, system_collections_IEnumerable_getEnumerator]
		public function getEnumerator():_IEnumerator_;
		
		
		[native,system_collections_ICollection_copyTo]
		public function copyTo(array:_Array_, index:int):void;
		
		[native,system_collections_IList_get_IsFixedSize]
		public function get isFixedSize():Boolean;
		
		[native,system_collections_IList_get_IsReadOnly]
		public function get isReadOnly():Boolean;
		
		[native,system_collections_IList_add]
		private function add(value:*):int;
		
		[native,system_collections_IList_clear]
		private function clear():void;
		
		[native,system_collections_IList_contains]
		private function contains(value:*):Boolean;
		
		[native,system_collections_IList_indexOf]
		private function indexOf(value:*):int;
		
		[native,system_collections_IList_insert]
		private function insert(index:int, value:*):void;
		
		[native,system_collections_IList_remove]
		private function remove(value:*):void;
		
		[native,system_collections_IList_removeAt]
		private function removeAt(index:int):void;
		
		
		[native,system_collections_ICollection_get_SyncRoot]
		private function get syncRoot():_Object_;
		
		
		[native,system_collections_ICollection_get_IsSynchronized]
		private function get isSynchronized():Boolean;
	}

}