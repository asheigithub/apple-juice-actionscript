package
{
	
	public dynamic class Error
	{
		
		public var message : String;

		public var name : String;

		
		public function get errorID () : int
		{
			return 0;
		}

		public function Error (message:String="", id:int=0)
		{
			
		}

		//public static function getErrorMessage (index:int) : String;
		public function getStackTrace () : String
		{
			return null;
		}

		public function toString():String
		{
			return null;
		}


	}
}
