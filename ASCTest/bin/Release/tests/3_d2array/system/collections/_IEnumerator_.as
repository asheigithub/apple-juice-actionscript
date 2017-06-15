package system.collections 
{
	
	[link_system_interface(system_collections_ienumerator_creator_)]
	public interface _IEnumerator_ 
	{
		[native,system_collections_ienumerator_current]
		function get current():*;
		
		[native,system_collections_ienumerator_movenext]
        function moveNext():Boolean;
		
		[native,system_collections_ienumerator_reset]
        function reset():void;
	}
	
}