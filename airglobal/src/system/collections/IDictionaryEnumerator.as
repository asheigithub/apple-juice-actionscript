package system.collections 
{
	
	//[link_system_interface(system_collections_idictionaryenumerator_creator_)]
	//public interface IDictionaryEnumerator extends _IEnumerator_
	//{
		//[native,system_collections_idictionaryenumerator_entry]
		//function get entry():*;
	//}
	
	public interface IDictionaryEnumerator extends _IEnumerator_
	{
		
		function get key():Object;

		
		function get value():*;

		
		function get entry():DictionaryEntry;

	}
}