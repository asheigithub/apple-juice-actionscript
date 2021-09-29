package system.collections
{
	
	public interface IEnumerator
    {
        function get current():*;

        function moveNext():Boolean;

        function reset():void;
    }
}
