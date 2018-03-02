/*
ADOBE SYSTEMS INCORPORATED
Copyright 2008 Adobe Systems Incorporated. All Rights Reserved.
 
NOTICE:   Adobe permits you to modify and distribute this file only in accordance with
the terms of Adobe AIR SDK license agreement.  You may have received this file from a
source other than Adobe.  Nonetheless, you may modify or distribute this file only in 
accordance with such agreement.
*/

package air.update.utils
{
	[ExcludeClass]
	public class NetUtils
	{
		public static const ACCEPTABLE_STATUSES:Array = [
		    0,
      		200,  // 200 OK
      		//202,  // 203 Accepted
      		//204,  // 204 No content
      		//205,  // 205 Reset Content -- eh, why not?
      		//206,  // 206 Partial Content (in response to req with Range header)

      		// NOT:
      		//   201 Created -- seems not to be repeatable by definition
      		//   203 Non-Authoritative Information -- returned by intermediate proxy
      		//   3xx redirection -- Because I don't want to ignore SHOULD clauses of RFC 2616, section 10
      		//   4xx client errors -- Ditto and also to keep server admins from going crazy
	      	//   5xx server errors -- Not particularly indicative of server health
   		];
		
		
		public static function isHTTPStatusAcceptable(httpStatus:int):Boolean
		{
			// not in the acceptable status array
			if (ACCEPTABLE_STATUSES.indexOf(httpStatus) == -1)
			{
				return false;
			}
			return true;
		}
	}
}