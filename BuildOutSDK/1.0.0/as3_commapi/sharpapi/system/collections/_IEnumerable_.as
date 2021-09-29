package system.collections
{


	[link_system_interface(system_collections_IEnumerable_creator)]
	public interface _IEnumerable_
	{
		[native,system_collections_IEnumerable_getEnumerator]
		function getEnumerator():_IEnumerator_;

	}
}
