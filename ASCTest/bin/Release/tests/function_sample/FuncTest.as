/*
下例使用 FunctionExample、SimpleCollection、EventBroadcaster 和 EventListener 类显示 ActionScript 中函数的各种用法。这是由以下步骤完成的： 
FunctionExample 的构造函数创建一个名为 simpleColl 的局部变量，该变量用一个整数数组填充，其范围从 1 到 8。 
使用 trace() 打印 simpleColl 对象。
将 EventListener 对象 listener 添加到 simpleColl 中。
调用 insert() 和 remove() 函数时，侦听器响应其事件。
创建第二个 SimpleCollection 对象，名为 greaterThanFourColl。
赋予 greaterThanFourColl 对象 simpleColl.select() 的结果，后者包含参数 4 和一个匿名函数。SimpleCollection 对象的选择方法是一个内部迭代器，它使用匿名函数参数作为块。
*/

package {
	[Doc]
	public class FuncTest{
		public function FuncTest() {
			var simpleColl:SimpleCollection;
            simpleColl = new SimpleCollection(0, 1, 2, 3, 4, 5, 6, 7, 8);
            trace(simpleColl);        // 0, 1, 2, 3, 4, 5, 6, 7, 8

            var listener:EventListener = new EventListener();
            simpleColl.addListener(listener);
            simpleColl.insert(9);        // itemInsertedHandler: 9
            simpleColl.remove(8);        // itemRemovedHandler: 8
            trace(simpleColl);        // 0, 1, 2, 3, 4, 5, 6, 7, 9

            var greaterThanFourColl:SimpleCollection;
            greaterThanFourColl = simpleColl.select(4, function(item:int, value:int){ return item > value });
            trace(greaterThanFourColl);    // 5, 6, 7, 9

			
		}
	}
}
import flash.display.Sprite;
    
class EventBroadcaster {
    private var listeners:Array;

    public function EventBroadcaster() {
        listeners = new Array();
    }
        
    public function addListener(obj:Object):void {
        removeListener(obj);
        listeners.push(obj);
    }
        
    public function removeListener(obj:Object):void {
        for(var i:uint = 0; i < listeners.length; i++) {
            if(listeners[i] == obj) {
                listeners.splice(i, 1);
            }
        }
    }
    
    public function broadcastEvent(evnt:String, ...args):void {
        for(var i:uint = 0; i < listeners.length; i++) {
            listeners[i][evnt].apply(listeners[i], args);
        }
    }    
}
    
class SimpleCollection extends EventBroadcaster {
    private var arr:Array;
        public function SimpleCollection(... args) {
        arr = (args.length == 1 && !isNaN(args[0])) ? new Array(args[0]) : args;
    }
        
    public function insert(obj:Object):void {
        remove(obj);
        arr.push(obj);
        broadcastEvent("itemInsertedHandler", obj);
    }
        
    public function remove(obj:Object):void {
        for(var i:uint = 0; i < arr.length; i++) {
            if(arr[i] == obj) {
                var obj:Object = arr.splice(i, 1)[0];
                broadcastEvent("itemRemovedHandler", obj);
            }
        }
    }

    public function select(val:int, fn:Function):SimpleCollection {
        var col:SimpleCollection = new SimpleCollection();
        for(var i:uint = 0; i < arr.length; i++) {
            if(fn.call(this, arr[i], val)) {
                col.insert(arr[i]);
            }
        }
        return col;
    }
        
    public function toString():String {
        return arr.join();
    }
}

class EventListener {
    public function EventListener() {
    }
    
    public function itemInsertedHandler(obj:Object):void {
        trace("itemInsertedHandler: " + obj);
    }
    
    public function itemRemovedHandler(obj:Object):void {
        trace("itemRemovedHandler: " + obj);        
    }
}

