package system.collections 
{
	
	[link_system_interface(system_collections_idictionary_creator_)]
	public interface IDictionary extends ICollection
	{
		[get_this_item];
		[native, system_collections_IDictionary_getThisItem];
		function getThisItem(key:Object):*;
		
		[set_this_item];
		[native, system_collections_IDictionary_setThisItem];
		function setThisItem(value:*, key:Object):void;
		
		
		[native,system_collections_IDictionary_get_IsFixedSize]
		function get isFixedSize():Boolean;
		
		[native,system_collections_IDictionary_get_IsReadOnly]
		function get isReadOnly():Boolean;
		
		[native,system_collections_IDictionary_get_Keys]
		function get keys():ICollection;
		
		[native,system_collections_IDictionary_get_Values]
		function get values():ICollection;
		
		[native,system_collections_IDictionary_add]
		function add(key:Object, value:*):void;
		
		[native,system_collections_IDictionary_clear]
		function clear():void;
		
		[native,system_collections_IDictionary_contains]
		function contains(key:Object):Boolean;
		
		[native,system_collections_IDictionary_remove]
		function remove(key:Object):void;
		
		
		[native,system_collections_IDictionary_getEnumerator_]
		function getEnumerator_():IDictionaryEnumerator;
		
	}
	
}