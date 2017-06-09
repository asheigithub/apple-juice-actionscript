package system.collections 
{
	
	[link_system_interface(system_collections_ienumerable_creator_)]
	public interface _IEnumerable_ 
	{
		[native,system_collections_ienumerable_getenumerator_]
		function getEnumerator():_IEnumerator_;
	}
	
}