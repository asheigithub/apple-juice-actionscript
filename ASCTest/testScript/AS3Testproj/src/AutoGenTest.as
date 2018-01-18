package 
{
	import autogencodelib.Testobj;
	import autogencodelib.Testobj_TESTHandler_Of_Int64;
	import flash.display.Sprite;
	import system.AsyncCallback;
	import system.Decimal;
	import system.IAsyncResult;
	import system.Int64;
	import system.MulticastDelegate;
	import system.UInt64;
	import system.collections.generic.List_Of_Int32;
	import system.collections.generic.List_Of_Int32_;
	
	
	[Doc]
	/**
	 * ...
	 * @author 
	 */
	public class AutoGenTest extends Sprite 
	{
		
		public function AutoGenTest() 
		{
			
			var g:Testobj = new Testobj();
			trace(g);
			
			//g.FuncTest(genFunc());
			
			trace(g.nc);
			
			
			//function www(v1:String,v2:int,v3:Int64):int
			//{
				//trace("bbb");
				//return v2 + v3;
			//};
			//
			//
			//g.handler = new Testobj_TESTHandler_Of_Int64(www);
			//
			//
			//(function ll()
			//{
				//
				//g.handler = new Testobj_TESTHandler_Of_Int64(www);
			//})();
			
			var fd:Testobj_TESTHandler_Of_Int64 = function():int{
				trace("func1");
				return 36;
			};
			
			
			trace(fd);
			trace(fd.invoke(null, 0, 0));
			
			var f2=function():int
				{
					trace("func2");
					return 48;
				};
				
			fd += f2;
			
			//
			//
			//fd.add(
				//f2			
			//);
			//
			//var f3=function():int
				//{
					//trace("func3");
					//return 54;
				//};
			//
			//fd.add(
				//f3
			//);
			
			g.handler = fd;
			
			
			trace( g.handler.invoke("bb", 3, 4));
			
			//g.handler = Testobj_TESTHandler_Of_Int64.remove(g.handler, f2);
			
			g.handler -=f2;
			
			trace( g.handler.invoke("bb", 3, 4));
			
			
			
			g.handler = null;
			
			
			
			var action = null;
			
			var b=function():int
			{
				trace("调用b");
				
				if (action != null)
				{
					action();					
				}
				
				return 1;
			}
			
			action = function(){ 
				
				trace("执行移除");
				g.handler -= b;
				
				trace(g.handler);
				
			}
			
			g.handler = b;
			
			g.handler.invoke("b", 9, 7);
			
			
			
			//g.handler.remove(f2);
			//
			//
			//trace( g.handler.invoke("bb", 3, 4));
			
			
			g.handler = new Testobj_TESTHandler_Of_Int64( function(v1:String,v2:int,v3:Int64):int{
				
				trace("bbc");
				
				
				return v2+v3;
			} );
			
			
			
			var k = g.handler.beginInvoke("a", 4, 6, 
			new AsyncCallback(
				function (ar:IAsyncResult)
				{
					trace(ar, ar.isCompleted, "result:" + g.handler.endInvoke(ar) );
					
				}
			)
			, null);
			
			
			trace(k,"hahaha");
			
		}
		
		
		private function genFunc()
		{
			var action = null;
			
			var func = function()
			{
				trace("func");
				
				if (action != null)
				{
					action();
					
				}
				
				
				return "bcd";
			}
			
			
			action = function()
			{
				trace("action");
			}
	
			return func;
			
		}
		
	}
	

}