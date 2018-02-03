package system
{


	[link_system_interface(system_ICloneable_creator)]
	public interface ICloneable
	{
		[native,system_ICloneable_clone]
		function clone():_Object_;

	}
}
