package system.collections
{
	import system._Array_;
	import system._Object_;


	[link_system_interface(system_collections_ICollection_creator)]
	public interface ICollection extends _IEnumerable_
	{
		[native,system_collections_ICollection_copyTo]
		function copyTo(array:_Array_,index:int):void;

		[native,system_collections_ICollection_get_Count]
		function get count():int;

		[native,system_collections_ICollection_get_SyncRoot]
		function get syncRoot():_Object_;

		[native,system_collections_ICollection_get_IsSynchronized]
		function get isSynchronized():Boolean;

	}
}
