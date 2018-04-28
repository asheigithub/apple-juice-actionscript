package
{
	/**
	 * Math 类包含表示常用数学函数和值的方法和常数。 
	 * <p class="- topic/p ">使用此类的方法和属性可以访问和处理数学常数和函数。Math 类的所有属性和方法都是静态的，而且必须使用 <codeph class="+ topic/ph pr-d/codeph ">Math.method(</codeph><codeph class="+ topic/ph pr-d/codeph "><i class="+ topic/ph hi-d/i ">parameter</i></codeph><codeph class="+ topic/ph pr-d/codeph ">)</codeph> 或 <codeph class="+ topic/ph pr-d/codeph ">Math.constant</codeph> 语法才能调用。在 ActionScript 中，使用双精度 IEEE-754 浮点数的最高精度定义常数。</p><p class="- topic/p ">若干 Math 类方法使用以弧度为单位的角度测量值作为参数。在调用此方法之前，您可以使用以下等式计算弧度值，并使用计算得出的值作为参数，您还可以将等式右侧的整个部分（用以度为单位的角度测量值代替 <codeph class="+ topic/ph pr-d/codeph ">degrees</codeph>）作为弧度参数。</p><p class="- topic/p ">要计算弧度值，请使用以下公式：</p><pre xml:space="preserve" class="- topic/pre ">
	 * radians = degrees ~~ Math.PI/180
	 * </pre><p class="- topic/p ">要由弧度计算出度，请使用以下公式：</p><pre xml:space="preserve" class="- topic/pre ">
	 * degrees = radians ~~ 180/Math.PI
	 * </pre><p class="- topic/p ">下面是将等式作为参数进行传递以计算 45° 角的正弦值的示例：</p><p class="- topic/p "><codeph class="+ topic/ph pr-d/codeph ">Math.sin(45 * Math.PI/180)</codeph> 等同于 <codeph class="+ topic/ph pr-d/codeph ">Math.sin(.7854)</codeph></p><p class="- topic/p "><b class="+ topic/ph hi-d/b ">注意：</b>由于 CPU 或操作系统所使用的算法不同，Math 函数 acos、asin、atan、atan2、cos、exp、log、pow、sin 和 sqrt 得出的值可能会稍有不同。Flash 运行时在计算列出的函数时会调用 CPU（如果 CPU 不支持浮点计算，则调用操作系统），而且显示结果会根据所使用的 CPU 或操作系统稍有不同。
	 * </p>
	 * @langversion	3.0
	 * @playerversion	Flash 9
	 * @playerversion	Lite 4
	 */
	public final class Math
	{
		/**
		 * 代表自然对数的底的数学常数，表示为 e。e 的近似值为 2.71828182845905。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public static const E : Number = 2.71828182845905;

		/**
		 * 10 的自然对数的数学常数，表示为 loge10，其近似值为 2.302585092994046。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public static const LN10 : Number = 2.302585092994046;

		/**
		 * 2 的自然对数的数学常数，表示为 loge2，其近似值为 0.6931471805599453。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public static const LN2 : Number = 0.6931471805599453;

		/**
		 * 常数 e (Math.E) 以 10 为底的对数的数学常数，表示为 log10e，其近似值为 0.4342944819032518。 
		 * Math.log() 方法计算数字的自然对数。将 Math.log() 的结果与 Math.LOG10E 相乘得到以 10 为底的对数。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public static const LOG10E : Number = 0.4342944819032518;

		/**
		 * 常数 e 以 2 为底的对数的数学常数，表示为 log2e，其近似值为 1.442695040888963387。
		 * 
		 *   Math.log 方法计算数字的自然对数。将 Math.log() 的结果与 Math.LOG2E 相乘得到以 2 为底的对数。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public static const LOG2E : Number = 1.442695040888963387;

		/**
		 * 代表一个圆的周长与其直径的比值的数学常数，表示为 pi，其近似值为 3.141592653589793。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public static const PI : Number = 3.141592653589793;

		/**
		 * 代表 1/2 的平方根的数学常数，其近似值为 0.7071067811865476。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public static const SQRT1_2 : Number = 0.7071067811865476;

		/**
		 * 代表 2 的平方根的数学常数，其近似值为 1.4142135623730951。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public static const SQRT2 : Number = 1.4142135623730951;

		/**
		 * 计算并返回由参数 val 指定的数字的绝对值。
		 * @param	val	已返回绝对值的数字。
		 * @return	指定参数的绝对值。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public static function abs (val:Number) : Number{ return 0; }

		/**
		 * 以弧度为单位计算并返回由参数 val 指定的数字的反余弦值。
		 * @param	val	-1.0 到 1.0 之间的一个数字。
		 * @return	参数 val 的反余弦值。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		
		public static function acos (val:Number) : Number{ return 0; }

		/**
		 * 以弧度为单位计算并返回由参数 val 指定的数字的反正弦值。
		 * @param	val	-1.0 到 1.0 之间的一个数字。
		 * @return	介于负二分之 pi 和正二分之 pi 之间的一个数字。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		
		public static function asin (val:Number) : Number{ return 0; }

		/**
		 * 以弧度为单位计算并返回角度值，该角度的正切值已由参数 val 指定。返回值介于负二分之 pi 和正二分之 pi 之间。
		 * @param	val	表示角的正切值的一个数字。
		 * @return	介于负二分之 pi 和正二分之 pi 之间的一个数字。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		
		public static function atan (val:Number) : Number{ return 0; }

		/**
		 * 以弧度为单位计算并返回点 y/x 的角度，该角度从圆的 x 轴（0 点在其上，0 表示圆心）沿逆时针方向测量。返回值介于正 pi 和负 pi 之间。请注意，atan2 的第一个参数始终是 y 坐标。
		 * @param	y	该点的 y 坐标。
		 * @param	x	该点的 x 坐标。
		 * @return	一个数字。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		
		public static function atan2 (y:Number, x:Number) : Number{ return 0; }

		/**
		 * 返回指定数字或表达式的上限值。数字的上限值是大于等于该数字的最接近的整数。
		 * @param	val	一个数字或表达式。
		 * @return	最接近且大于等于参数 val 的整数。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		
		public static function ceil (val:Number) : Number{ return 0; }

		/**
		 * 以弧度为单位计算并返回指定角度的余弦值。要计算弧度，请参阅 Math 类的概述。
		 * @param	angleRadians	一个数字，它表示一个以弧度为单位的角度。
		 * @return	-1.0 到 1.0 之间的一个数字。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		
		public static function cos (angleRadians:Number) : Number{ return 0; }

		/**
		 * 返回自然对数的底 (e) 的 x 次幂的值，x 由参数 x 指定。常量 Math.E 可以提供 e 的值。
		 * @param	val	指数；一个数字或表达式。
		 * @return	e 的 x 次幂，x 由参数 val 指定。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		
		public static function exp (val:Number) : Number{ return 0; }

		/**
		 * 返回由参数 val 指定的数字或表达式的下限值。下限值是小于等于指定数字或表达式的最接近的整数。
		 * @param	val	一个数字或表达式。
		 * @return	最接近且小于等于参数 val 的整数。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		
		public static function floor (val:Number) : Number{ return 0; }

		/**
		 * 返回参数 val 的自然对数。
		 * @param	val	其值大于 0 的数字或表达式。
		 * @return	参数 val 的自然对数。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		
		public static function log (val:Number) : Number{ return 0; }

		
		/**
		 * 计算 val1 和 val2（或更多的值）并返回最大值。
		 * @param	val1	一个数字或表达式。
		 * @param	val2	一个数字或表达式。
		 * @param	rest	一个数字或表达式。Math.max() 可以接受多个参数。
		 * @return	参数 val1 和 val2（或更多值）的最大值。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public static function max (val1:Number=-1 / 0, val2:Number=-1 / 0, ...rest) : Number
		{
			return 0;
		}


		/**
		 * 计算 val1 和 val2（或更多的值）并返回最小值。
		 * @param	val1	一个数字或表达式。
		 * @param	val2	一个数字或表达式。
		 * @param	rest	一个数字或表达式。Math.min() 可以接受多个参数。
		 * @return	参数 val1 和 val2（或更多值）的最小值。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public static function min (val1:Number=1 /0, val2:Number=1 /0, ...rest) : Number
		{
			return 0;
		}

		/**
		 * 计算并返回 base 的 pow 次幂。
		 * @param	base	将自乘参数 pow 次的数字。
		 * @param	pow	指定参数 base 的自乘次数的数字。
		 * @return	base 的 pow 次幂的值。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		
		public static function pow (base:Number, pow:Number) : Number{ return 0; }

		/**
		 * 返回一个伪随机数 n，其中 0 <= n < 1。因为该计算不可避免地包含某些非随机的成分，所以返回的数字以保密方式计算且为“伪随机数”。
		 * @return	一个伪随机数。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		
		public static function random () : Number{ return 0; }

		/**
		 * 将参数 val 的值向上或向下舍入为最接近的整数并返回该值。如果 val 与最接近的两个整数等距离（即该数字以 .5 结尾），则该值向上舍入为下一个较大的整数。
		 * @param	val	要舍入的数字。
		 * @return	参数 val 舍入为最近的整数。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		
		public static function round (val:Number) : Number{ return 0; }

		/**
		 * 以弧度为单位计算并返回指定角度的正弦值。要计算弧度，请参阅 Math 类的概述。
		 * @param	angleRadians	一个数字，它表示一个以弧度为单位的角度。
		 * @return	一个数字；指定角度的正弦值（介于 -1.0 和 1.0 之间）。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		
		public static function sin (angleRadians:Number) : Number{ return 0; }

		/**
		 * 计算并返回指定数字的平方根。
		 * @param	val	一个大于等于 0 的数字或表达式。
		 * @return	如果参数 val 大于等于 0，则为一个数字，否则为 NaN（非数字）。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		
		public static function sqrt (val:Number) : Number{ return 0; }

		/**
		 * 计算并返回指定角度的正切值。要计算弧度，请参阅 Math 类的概述。
		 * @param	angleRadians	一个数字，它表示一个以弧度为单位的角度。
		 * @return	参数 angleRadians 的正切值。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		
		public static function tan (angleRadians:Number) : Number{ return 0; }
	}
}
