package
{
	public dynamic class ArgumentError extends Error
	{
		public function ArgumentError (message:String="", id:int=0)
		{
			super(message, id);
		}
	}
}