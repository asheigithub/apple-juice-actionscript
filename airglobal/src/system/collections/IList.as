package system.collections 
{
	
	
	public interface IList extends ICollection
	{
		
		function getThisItem(index:int):*;

		
		function setThisItem(value:*,index:int):void;

		
		function add(value:*):int;

		
		function contains(value:*):Boolean;

		
		function clear():void;

		
		function get isReadOnly():Boolean;

		
		function get isFixedSize():Boolean;

		
		function indexOf(value:*):int;

		
		function insert(index:int,value:*):void;

		
		function remove(value:*):void;

		
		function removeAt(index:int):void;
	}
	
}