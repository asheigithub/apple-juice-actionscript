package system.collections 
{
	
	[link_system_interface(system_collections_ilist_creator_)]
	public interface IList extends ICollection
	{
		[get_this_item];
		[native, _system_collections_ilist_getThisItem_];
		function getThisItem(index:int):*;
		
		[set_this_item];
		[native, _system_collections_ilist_setThisItem_];
		function setThisItem(value:*, index:int):void;
		
		[native,_system_collections_ilist_isFixedSize_]
		function get isFixedSize():Boolean;
		
		[native,_system_collections_ilist_isReadOnly_]
		function get isReadOnly():Boolean;
		
		[native,_system_collections_ilist_add_]
		function add(value:*):int;
		
		[native,_system_collections_ilist_clear_]
		function clear():void;
		
		[native,_system_collections_ilist_contains_]
		function contains(value:*):Boolean;
		
		[native,_system_collections_ilist_indexOf_]
		function indexOf(value:*):int;
		
		[native,_system_collections_ilist_insert_]
		function insert(index:int, value:*):void;
		
		[native,_system_collections_ilist_remove_]
		function remove(value:*):void;
		
		[native,_system_collections_ilist_removeAt_]
		function removeAt(index:int):void
	}
	
}