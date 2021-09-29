package system.collections 
{
	
	/**
	 * ...
	 * @author 
	 */
	[_IEnumerator_]
	public interface IEnumerator
    {
        function get current():*;

        function moveNext():Boolean;

        function reset():void;
    }
	
}