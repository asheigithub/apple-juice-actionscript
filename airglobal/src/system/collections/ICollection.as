package system.collections
{
	import system._Array_;
	import system._Object_;


	
	public interface ICollection extends _IEnumerable_
	{
		
		function copyTo(array:_Array_,index:int):void;

		
		function get count():int;

		
		//function get syncRoot():_Object_;

		
		//function get isSynchronized():Boolean;

	}
}
