package system.collections 
{
	
	[link_system_interface(system_collections_idictionary_creator_)]
	public interface IDictionary extends ICollection
	{
		[get_this_item];
		[native, _system_collections_idictionary_getThisItem_];
		function getThisItem(key:Object):*;
		
		[set_this_item];
		[native, _system_collections_idictionary_setThisItem_];
		function setThisItem(value:*, key:Object):void;
		
		
		[native,_system_collections_idictionary_isFixedSize_]
		function get isFixedSize():Boolean;
		
		[native,_system_collections_idictionary_isReadOnly_]
		function get isReadOnly():Boolean;
		
		[native,_system_collections_idictionary_keys_]
		function get keys():ICollection;
		
		[native,_system_collections_idictionary_values_]
		function get values():ICollection;
		
		[native,_system_collections_idictionary_add_]
		function add(key:Object, value:*):void;
		
		[native,_system_collections_idictionary_clear_]
		function clear():void;
		
		[native,_system_collections_idictionary_contains_]
		function contains(key:Object):Boolean;
		
		[native,_system_collections_idictionary_remove_]
		function remove(key:Object):void;
		
		
		[native,system_collections_idictionary_getenumerator_]
		function getEnumerator():_IEnumerator_;
		
	}
	
}