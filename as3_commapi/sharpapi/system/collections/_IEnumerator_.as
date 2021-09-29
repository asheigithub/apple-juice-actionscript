package system.collections
{
	

	[link_system_interface(system_collections_IEnumerator_creator)]
	public interface _IEnumerator_
	{
		[native,system_collections_IEnumerator_moveNext]
		function moveNext():Boolean;

		[native,system_collections_IEnumerator_get_Current]
		function get current():*;

		[native,system_collections_IEnumerator_reset]
		function reset():void;

	}
}
