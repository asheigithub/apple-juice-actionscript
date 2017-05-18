package
{
	import adobe.utils.CustomActions;
	import flash.accessibility.Accessibility;
	import flash.display.Loader;
	import flash.display.Sprite;
	import flash.events.Event;
	import flash.events.IOErrorEvent;
	import flash.net.URLRequest;
	import flash.sampler.NewObjectSample;
	import flash.utils.Dictionary;
	import ppp.IPPP;
	import ppp.it;
	import ppp.pp2.CP;
	import system.DateTimeKind;
	import system.Int64;
	import system.TimeSpan;
	
	[Doc]
	/**
	 * ...
	 * @author 
	 */
	public class Main extends Sprite 
	{
		[fffmeta]
		public var fff:int;
		
		public function get a()
		{
			return 1;
		}
		public function set a(v:int)
		{
			trace(v);
		}
		
		private function bb() { trace("bb") };
		
		
		public function Main() 
		{
			//var dk = DateTimeKind.Local;
			//trace( int(DateTimeKind.Local));
			
			//var i:Int64 = new Int64(4);
			//
			//var k:Int64 = i;
			//
			//i = Int64(4);
			//
			//trace(i == k);
			//
			//k = Int64(i + i)  ;
			//
			//trace(i, k);
			//
			//k = 99;
			//
			//trace(i, k);
			//
			trace(Int64.MaxValue);
			trace(Int64.MinValue);
			//
			//trace(k.equals(Int64.MaxValue));
			//
			//
			//var j = Int64.parse("998");
			
			//trace( system.Object.referenceEquals( Int64( k+1), Int64(i+1)) );
			
			//a b;
			
			var ts:TimeSpan = new TimeSpan(-1, 2, 3);
			trace(ts);
			
			trace(ts.equals( new TimeSpan(0, -60 + 2, 3) ));
			
			
			var a = ts.add( new TimeSpan(3,5,0) );
			trace(a.minutes);
			
			trace( ts.compareTo_TimeSpan( ts) );
			
			trace(ts.duration().negate().seconds,TimeSpan(a).subtract(a).milliseconds, TimeSpan.compare(ts.duration().negate(), TimeSpan(a).subtract(a)) );
			
			trace(  ts.totalSeconds);
			
			var tp:TimeSpan = TimeSpan.constructor__(1, 2, 3, 4,6);
			trace(tp.totalDays);
			
			var tt:TimeSpan = TimeSpan.constructor___(Int64(999));
			trace(tt);
			trace( tt.ticks.valueOf()===uint( 999 ) );
			
			try 
			{
				tt = TimeSpan.fromDays(3.3).add( TimeSpan.fromMinutes( -3000000) );
			}
			catch (e:ArgumentError)
			{
				
			}
			catch (e:AneError)
			{
				trace(e.message,'\n',e.getStackTrace());
			}
			finally
			{
				trace(tt);
			}
			
			var v:Vector.<Vector.<TimeSpan>> = Vector.<Vector.<TimeSpan>>( [Vector.<TimeSpan>([tt,tt,tt])]);
			trace(v);
			
			v[0][0]=v[0][0].add(TimeSpan.fromDays(1));
			v[0][1]=v[0][1].add(TimeSpan.fromDays(2));
			v[0][2]=v[0][2].add(TimeSpan.fromSeconds(1));
			
			
			trace( v[0][0] == v[0][1] );
			trace( v[0][0] == tt.add(TimeSpan.fromTicks(Int64(60*60*24))) );
			trace( v[0][0] == v[0][2] );
			trace(v);
			
			
			var values = [ "000000006", "12.12:12:12.12345678" ];
			for each (var s:String in values)
			{
			   try {
				  var interval = TimeSpan.parse(s);
				  trace("{0} --> {1}", s, interval);
			   }   
			   catch (e:Error) {
				  trace("{0}: Bad Format", s);
			   }   
			   
			}

			
			trace(TimeSpan.Zero);
			
			trace(TimeSpan.greaterThan( TimeSpan.Zero,tt ));
			
			
			/*
			yield 是从.net2.0语法中移植过来，可自动生成一个可枚举对象的语法。每次yield return都可以返回一个值
			yield break可停止枚举。
			*/
			//var yieldtest=function(a):*
			//{
				//for (var i:int = 0; i < 100; i++ )
				//{
					//if(i>=a)
					//{
						//trace("停止枚举");
						//yield break;
					//}
					//trace("当前输出值：",i);
					//yield return i;
				//}
			//}
			//
			//for (var k in yieldtest(4))
			//{
				//trace("获取到:",k);
			//}
			
			//var m:Object = 1;
			
			
			
			//for each(var k in new ia())
			//{
				//trace(k);
			//}
			
		}
		
		
		
		
		

	}
	
}
//import system.collections.IEnumerable;
//import system.collections.IEnumerator;
//
//class ia implements IEnumerable
//{
	///* INTERFACE system.collections.IEnumerable */
	//
	//public function getEnumerator():IEnumerator 
	//{
		//for (var i:int = 0; i < 10; i++) 
		//{
			//yield return i;
		//}
	//}
//}


//var opp=0;
//
//function select( a:Vector.<int> )
//{
	//var i:int; var j:int; var x:int; var tmp:int;
//
	//var len:int = a.length;
	//
	//var count:int = 0;
	//
	//for( i=0; i<len; i++)
	//{
		//
		//var min:int = i;
		//var amin = a[min];
		//for( j=i+1; j<len; j++ )
		//{
			//if ( a[j] < amin )
			//{
				//min = j;
				//amin =a[min];
			//}
			//count++;
		//}
		//
		//tmp = amin;
		//a[min] = a[i];
		//a[i] = tmp;
		//
		//
	//}
	//
	//trace(count);
	//
//}
//
//
//
//var a:Vector.<int> = new Vector.<int>();
//var b:int = 0;
//
//
//
//for(var i:int = 0; i < 5000; ++i )
//{
	//
	//++b;
	//a.unshift(Math.random() * 10000);
//}
//
//var t = new Date().getTime();
//
//select(a);
//
//trace(new Date().getTime()-t);
//
//trace(b);

//function Emitter() {
    //this._listener = {}; //_listener[自定义的事件名] = [所用执行的匿名函数1, 所用执行的匿名函数2]
//}
//
////注册事件
//Emitter.prototype.bind = function(eventName, funCallback) {
        //var listenersArr = this._listener[eventName] || []; ////this._listener[eventName]没有值则将listener定义为[](数组)。
        //listenersArr.push(funCallback);
        //this._listener[eventName] = listenersArr;
    //}
    ////触发事件
//Emitter.prototype.trigger = function(eventName,...args) {
        //
	//
	////未绑定事件    
        //if (!this._listener.hasOwnProperty(eventName)) {
            //trace('you do not bind this event');
            //return;
        //}
        ////var args = Array.prototype.slice.call(arguments, 1); ////args为获得除了eventName后面的参数(最后被用作注册事件的参数)
        //var listenersArr = this._listener[eventName];
        //var _this = this;
        //if (!(listenersArr is Array)) return; ////自定义事件名不存在
//
        //listenersArr.forEach(Object(function(callback) {
            //try {
				//trace(this);
				//
                //callback.call(_this, args);
            //} catch (e) {
                //trace(e);
            //}
        //}));
    //}
    ////解绑
//Emitter.prototype.unbind = function(eventName, callback) {
    //this._listener.hasOwnProperty(eventName) && delete this._listener[eventName];
    //callback && callback();
//}
//
//var emitter = new Emitter();
//emitter.bind("selfEvent", function(...args) {
    //trace("第一个绑定");
	//
    //args.forEach(function(item) {
		//trace(this.Emitter);
        //trace(item);
    //});
//});
//emitter.bind("selfEvent", function(...args) {
    //trace("第二个绑定");
    //args.forEach(function(item) {
        //trace(item);
    //});
//});
//emitter.trigger('selfEvent', 'a', 'b', 'c');
//emitter.unbind('selfEvent', function() {
    //trace("解除绑定");
//});
//emitter.trigger('selfEvent', 'a', 'b', 'c');


