package system.collections 
{
	import system._Array_;
	import system._Object_;
	
	[link_system]
	public class Queue extends _Object_ implements ICollection
	{
		[native,_system_collections_queue_static_createInstance]
		public static function createInstance(c:ICollection):Queue;
		
		[native,_system_collections_queue_static_createInstance_]
		public static function createInstance_(capacity:int):Queue;
		
		
		[creator];
		[native, _system_collections_queue_creator_]
		private static function _creator(type:Class):*;
		
		[native, _system_collections_queue_ctor_]
		public function Queue();
		
		[native,system_collections_icollection_count]
		public function get count():int;
		
		[native,system_collections_icollection_copyto]
		public function copyTo(array:_Array_, index:int):void;
		
		[native,system_collections_ienumerable_getenumerator_]
		public function getEnumerator():_IEnumerator_;
		
		[native,system_collections_queue_clear]
		public function clear():void;
		
		[native,_system_collections_queue_contains_]
		public function contains(value:*):Boolean;
		
		[native,_system_collections_queue_dequeue_]
		public function dequeue():*;
		
		[native,_system_collections_queue_enqueue_]
		public function enqueue(obj:*):void;
		
		[native,_system_collections_queue_peek_]
		public function peek():*;
		
		[native,system_collections_queue_toarray]
		public function toArray():_Array_;
		
		[native,system_collections_queue_trimtosize]
		public function trimToSize():void;
		
	}

}