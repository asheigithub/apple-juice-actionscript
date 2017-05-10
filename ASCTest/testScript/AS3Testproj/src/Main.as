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
	
	[Doc]
	/**
	 * ...
	 * @author 
	 */
	public class Main extends Sprite 
	{
		
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
			/*
			yield 是从.net2.0语法中移植过来，可自动生成一个可枚举对象的语法。每次yield return都可以返回一个值
			yield break可停止枚举。
			*/
		
		
			var yieldtest=function(a):*
			{
				for (var i:int = 0; i < 100; i++ )
				{
					if(i>=a)
					{
						trace("停止枚举");
						yield break;
					}
					trace("当前输出值：",i);
					yield return i;
				}
			}
			
			for (var k in yieldtest(4))
			{
				
				trace("获取到:",k);
			}
			
			
		}
		
		
		
		
		

	}
	
}

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


