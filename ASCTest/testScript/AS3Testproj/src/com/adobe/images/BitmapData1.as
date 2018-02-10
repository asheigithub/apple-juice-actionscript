package com.adobe.images 
{
	/**
	 * ...
	 * @author 
	 */
	public class BitmapData1 
	{
		
		public function get width():int
		{
			return w;
		}
		
		public function get height():int
		{
			return h;
		}
		
		private var w:int;
		private var h:int;
		
		public var transparent:Boolean;
		
		public function BitmapData1(w:int,h:int) 
		{
			this.w = w;
			this.h = h;
			this.transparent = false;
		}
		
		public function getPixel(x:int,y:int):uint
		{
			return (0xff << 16) | ((int)( x / w * 255)<<8) |((int)( y /h * 255 ));
			
		}
		public function getPixel32(x:int,y:int):uint
		{
			return (0xff << 24) |(0xff << 16) | ((int)( x / w * 255)<<8) |((int)( y /h * 255 ));
			
		}
	}

}