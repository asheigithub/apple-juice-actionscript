package system.collections 
{
	
	
	public interface IDictionary extends ICollection
	{
		
		function getThisItem(key:Object):*;
		
		
		function setThisItem(value:*, key:Object):void;
		
		
		
		function get isFixedSize():Boolean;
		
		
		function get isReadOnly():Boolean;
		
		
		function get keys():ICollection;
		
		
		function get values():ICollection;
		
		
		function add(key:Object, value:*):void;
		
		
		function clear():void;
		
		
		function contains(key:Object):Boolean;
		
		
		function remove(key:Object):void;
		
		
		
		function getEnumerator_():IDictionaryEnumerator;
		
	}
	
}