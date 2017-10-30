package system.collections 
{
	import system._Array_;
	
	[link_system_interface(system_collections_icollectinos_creator_)]
	public interface ICollection extends _IEnumerable_
	{
		[native,system_collections_icollection_count]
		function get count():int;
		
		[native,system_collections_icollection_copyto]
		function copyTo(array:_Array_, index:int):void;
	}
	
}