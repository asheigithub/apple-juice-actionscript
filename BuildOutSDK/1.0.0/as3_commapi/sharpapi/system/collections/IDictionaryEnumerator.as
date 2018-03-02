package system.collections 
{
	
	//[link_system_interface(system_collections_idictionaryenumerator_creator_)]
	//public interface IDictionaryEnumerator extends _IEnumerator_
	//{
		//[native,system_collections_idictionaryenumerator_entry]
		//function get entry():*;
	//}
	[link_system_interface(system_collections_IDictionaryEnumerator_creator)]
	public interface IDictionaryEnumerator extends _IEnumerator_
	{
		[native,system_collections_IDictionaryEnumerator_get_Key]
		function get key():Object;

		[native,system_collections_IDictionaryEnumerator_get_Value]
		function get value():*;

		[native,system_collections_IDictionaryEnumerator_get_Entry]
		function get entry():DictionaryEntry;

	}
}