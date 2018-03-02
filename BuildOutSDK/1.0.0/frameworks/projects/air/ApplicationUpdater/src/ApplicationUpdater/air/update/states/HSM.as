/*
ADOBE SYSTEMS INCORPORATED
Copyright 2008 Adobe Systems Incorporated. All Rights Reserved.
 
NOTICE:   Adobe permits you to modify and distribute this file only in accordance with
the terms of Adobe AIR SDK license agreement.  You may have received this file from a
source other than Adobe.  Nonetheless, you may modify or distribute this file only in 
accordance with such agreement.
*/

package air.update.states 
{
	import flash.errors.IllegalOperationError;
    import flash.events.ErrorEvent;
    import flash.events.Event;
    import flash.events.EventDispatcher;
	import flash.events.TimerEvent;
	import flash.utils.Timer;
	
	[ExcludeClass]
	public class HSM extends EventDispatcher 
	{
		private var _hsmState:Function;
		private var asyncTimer:Timer;		
		
		public function HSM(initialState:Function):void 
		{
			_hsmState = initialState;
		}
		
		public function init():void 
		{
			try 
			{
				_hsmState(new HSMEvent(HSMEvent.ENTER));
			} catch(e:Error) 
			{
				_hsmState(new ErrorEvent(ErrorEvent.ERROR, false, false, e.message, e.errorID));
			}
		}

		public function dispatch(event:Event):void 
		{
			try 
			{
				_hsmState(event);
			} catch(e:Error) 
			{
				_hsmState(new ErrorEvent(ErrorEvent.ERROR, false, false, e.message, e.errorID));
			}
		}
        
        protected function get stateHSM():Function 
        {
            return _hsmState;
        }
        
		protected function transition(state:Function):void 
		{
			// If we got here from a timer, clear it
			asyncTimer = null;
			try 
			{
				_hsmState(new HSMEvent(HSMEvent.EXIT));
				_hsmState = state;
				_hsmState(new HSMEvent(HSMEvent.ENTER));
			} catch(e:Error) 
			{
				_hsmState(new ErrorEvent(ErrorEvent.ERROR, false, false, "Unhandled exception " + e.name + ": " + e.message, e.errorID));
			}
        }
		
		/** Transitions asynchronously to the specified function. */
		
		protected function transitionAsync(state:Function):void 
		{
			if (asyncTimer) throw new IllegalOperationError("async transition already queued");
			
			asyncTimer = new Timer(0, 1);
			asyncTimer.addEventListener(TimerEvent.TIMER, function(event:Event):void { transition(state); });
			asyncTimer.start();
		}
	}
}
