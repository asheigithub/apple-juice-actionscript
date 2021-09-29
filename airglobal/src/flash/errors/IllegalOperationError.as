package flash.errors 
{
	/**
	 * ...
	 * @author 
	 */
	public dynamic final class IllegalOperationError extends Error 
	{
		
		public function IllegalOperationError(message:String="", id:*=0) 
		{
			super(message, id);
		}
		
	}

}