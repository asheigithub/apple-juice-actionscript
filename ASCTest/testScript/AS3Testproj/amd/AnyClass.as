package 
{
	import autogencodelib.FindMemberResult;
	import autogencodelib.MethodInvokeResult;
	import autogencodelib.MethodInvoker;
	import autogencodelib.ReflectUtil;
	import flash.utils.getQualifiedClassName;
	import system._Array_;
	import system._Object_;
	/**
	 * ...
	 * @author ...
	 */
	public class AnyClass 
	{
		var instance:_Object_;
		public function AnyClass(typeName:String,...args) 
		{
			if (typeName != null)
			{
				var sendargs:_Array_ = null;
				if (args != null)
				{
					sendargs = _Array_.createInstance(_Object_, args.length);
					for (var i:int = 0; i < args.length; i++) 
					{
						sendargs[i] = args[i];
					}
					
				}
				
				instance = ReflectUtil.createInstance(typeName, sendargs);
			}
		}
		
		public function get isExported():Boolean
		{
			return testExported(instance);
		}
		
		private static function testExported(obj:_Object_):Boolean
		{
			return obj !=null && getQualifiedClassName(obj) != "system::_Object_";
		}
		
		[get_this_item];
		public function getValue(key:String):*
		{
			var obj:FindMemberResult =  ReflectUtil.findMember(instance, key);
			if(obj.isMethod)
			{
				 
				var fun:Function =	function(...args):*
					{
						var sendargs:_Array_ = null;
						if (args != null)
						{
							sendargs = _Array_.createInstance(_Object_, args.length);
							for (var i:int = 0; i < args.length; i++) 
							{
								sendargs[i] = args[i];
							}
							
						}
						
						var caller:MethodInvoker = obj.result as MethodInvoker;
						
						var callresult:MethodInvokeResult = caller.invoke(sendargs);
						if (callresult.isVoid)
						{
							return undefined;
						}
						else
						{
							if (callresult.value == null)
							{
								return null;
							}
							else if ( testExported(callresult.value) )
							{
								return callresult.value;
							}
							else
							{
								var ra:AnyClass = new AnyClass(null);
								ra.instance = callresult.value;
								return ra;
							}							
						}									
					};
					
				return fun;
					
			}
			else
			{
				if ( testExported(obj.result) )
				{
					return obj.result;
				}
				else
				{
					var result:AnyClass = new AnyClass(null);
					result.instance = obj.result;
					return result;
				}
			}
		}
		
		[set_this_item];
		public function setValue(value:*, key:String):void
		{
			ReflectUtil.setMember(instance, key, value);
		}
		
		
		public static function invoke(typeName:String, funName:String, ... args):*
		{
			var sendargs:_Array_ = null;
			if (args != null)
			{
				sendargs = _Array_.createInstance(_Object_, args.length);
				for (var i:int = 0; i < args.length; i++) 
				{
					sendargs[i] = args[i];
				}
				
			}
			
			var callresult:MethodInvokeResult = MethodInvoker.invoke(typeName,funName,sendargs);
			if (callresult.isVoid)
			{
				return undefined;
			}
			else
			{
				if (callresult.value == null)
				{
					return null;
				}
				else if ( testExported(callresult.value) )
				{
					return callresult.value;
				}
				else
				{
					var ra:AnyClass = new AnyClass(null);
					ra.instance = callresult.value;
					return ra;
				}							
			}	
			
		}
		
		
		public function valueOf():*
		{
			return instance;
		}	
		
		public function toString():String
		{
			if (instance == null)
			{
				return "null";
			}
			else
			{
				return instance.toString();
			}
		}
		
	}

}