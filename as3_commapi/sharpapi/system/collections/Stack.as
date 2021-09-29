package system.collections 
{
	import system.ICloneable;
	import system._Array_;
	import system._Object_;
	
	[link_system]
	public class Stack extends _Object_ implements ICollection,ICloneable
	{
		[native,_system_collections_stack_static_createInstance]
		public static function createInstance(c:ICollection):Stack;
		
		[native,_system_collections_stack_static_createInstance_]
		public static function createInstance_(initialCapacity:int):Stack;
		
		
		[creator];
		[native, _system_collections_stack_creator_]
		private static function _creator(type:Class):*;
		
		[native, _system_collections_stack_ctor_]
		public function Stack();
		
		[native,system_collections_ICollection_get_Count]
		public function get count():int;
		
		[native,system_collections_ICollection_copyTo]
		public function copyTo(array:_Array_, index:int):void;
		
		[native,system_collections_IEnumerable_getEnumerator]
		public function getEnumerator():_IEnumerator_;
		
		[native,system_collections_stack_clear]
		public function clear():void;
		
		[native,_system_collections_stack_contains_]
		public function contains(value:*):Boolean;
		
		[native,_system_collections_stack_peek_]
		public function peek():*;
		
		[native,_system_collections_stack_pop_]
		public function pop():*;
		
		[native,_system_collections_stack_push_]
		public function push(obj:*):void;
		
		[native,system_collections_stack_toarray]
		public function toArray():_Array_;
		
		[native,system_collections_ICollection_get_SyncRoot]
		private function get syncRoot():_Object_;
		
		
		[native,system_collections_ICollection_get_IsSynchronized]
		private function get isSynchronized():Boolean;
		
		[native,system_ICloneable_clone]
		function clone():_Object_;
	}

}