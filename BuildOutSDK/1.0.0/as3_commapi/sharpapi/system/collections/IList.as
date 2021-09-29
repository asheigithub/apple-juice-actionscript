package system.collections 
{
	
	[link_system_interface(system_collections_ilist_creator_)]
	public interface IList extends ICollection
	{
		[get_this_item];
		[native,system_collections_IList_getThisItem];
		function getThisItem(index:int):*;

		[set_this_item];
		[native,system_collections_IList_setThisItem];
		function setThisItem(value:*,index:int):void;

		[native,system_collections_IList_add]
		function add(value:*):int;

		[native,system_collections_IList_contains]
		function contains(value:*):Boolean;

		[native,system_collections_IList_clear]
		function clear():void;

		[native,system_collections_IList_get_IsReadOnly]
		function get isReadOnly():Boolean;

		[native,system_collections_IList_get_IsFixedSize]
		function get isFixedSize():Boolean;

		[native,system_collections_IList_indexOf]
		function indexOf(value:*):int;

		[native,system_collections_IList_insert]
		function insert(index:int,value:*):void;

		[native,system_collections_IList_remove]
		function remove(value:*):void;

		[native,system_collections_IList_removeAt]
		function removeAt(index:int):void;
	}
	
}