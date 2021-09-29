package 
{
	
	/**
	 * 表示 IEEE-754 双精度浮点数的数据类型。使用与 Number 类关联的方法和属性可以操作基元数值。此类与 JavaScript 的 Number 类完全相同。
	 * <p class="- topic/p ">但 Number 类的属性是静态属性，这意味着无需对象就可以使用这些属性，因此您不需要使用构造函数。</p><p class="- topic/p ">Number 数据类型符合双精度 IEEE-754 标准。 </p><p class="- topic/p ">需要使用浮点值时，Number 数据类型很有用。Flash 运行时处理 int 和 uint 数据类型的效率高于处理 Number 数据类型的效率，但当所需值的范围超过 int 和 uint 数据类型的有效范围时，Number 数据类型很有用。Number 类可用于表示超出 int 和 uint 数据类型有效范围的整数值。Number 数据类型可使用多达 53 位来表示整数值，而 int 和 uint 则只能使用 32 位。Number 类型的变量的默认值为 <codeph class="+ topic/ph pr-d/codeph ">NaN</codeph>（非数字）。</p>
	 * 
	 *   EXAMPLE:
	 * 
	 *   下面的示例说明如何将一个具有六位小数的数字截断（使用舍入）为一个具有两位小数的数字。
	 * <codeblock xml:space="preserve" class="+ topic/pre pr-d/codeblock ">
	 * 
	 *   package {
	 * import flash.display.Sprite;
	 * 
	 *   public class NumberExample extends Sprite {
	 * public function NumberExample() {
	 * var num:Number = new Number(10.456345);
	 * var str:String = num.toFixed(2);
	 * trace(num); // 10.456345
	 * trace(str); // 10.46
	 * }
	 * }
	 * }
	 * </codeblock>
	 * @langversion	3.0
	 * @playerversion	Flash 9
	 * @playerversion	Lite 4
	 */
	public final class Number extends Object
	{
		public static const E : Number = 2.718281828459045;
		public static const LN10 : Number = 2.302585092994046;
		public static const LN2 : Number = 0.6931471805599453;
		public static const LOG10E : Number = 0.4342944819032518;
		public static const LOG2E : Number =1.4426950408889634;

		/**
		 * 最大可表示数（双精度 IEEE-754）。此数字大约为 1.79e+308。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public static const MAX_VALUE : Number=1.79769313486231e+308;

		/**
		 * 可表示的最小非负非零数（双精度 IEEE-754）。此数字大约为 5e-324。可表示的全部数字中，最小数字实际为 -Number.MAX_VALUE。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public static const MIN_VALUE : Number=4.9406564584124654e-324;

		/**
		 * 表示“非数字”(NaN) 的 IEEE-754 值。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public static const NaN : Number = 0/0;

		/**
		 * 指定表示负无穷大的 IEEE-754 值。此属性的值与常数 -Infinity 的值相同。
		 * 
		 *   负无穷大是当数学运算或函数返回的值超过可表示的负值时，返回的一个特殊数值。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public static const NEGATIVE_INFINITY : Number = -Infinity;
		public static const PI : Number =3.141592653589793;

		/**
		 * 指定表示正无穷大的 IEEE-754 值。此属性的值与常数 Infinity 的值相同。
		 * 
		 *   正无穷大是当数学运算或函数返回的值大于可表示的值时，返回的一个特殊数值。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public static const POSITIVE_INFINITY : Number =Infinity;
		public static const SQRT1_2 : Number =0.7071067811865476;
		public static const SQRT2 : Number =1.4142135623730951;

		
		
		public static function abs (x:Number) : Number{
			return 0;
		}
		
		public static function acos (x:Number) : Number{
			return 0;
		}
		
		public static function asin (x:Number) : Number{
			return 0;
		}
		
		public static function atan (x:Number) : Number{
			return 0;
		}
		
		public static function atan2 (y:Number, x:Number) : Number{
			return 0;
		}
		
		public static function ceil (x:Number) : Number{
			return 0;
		}
		
		public static function cos (x:Number) : Number{
			return 0;
		}
		
		public static function exp (x:Number) : Number{
			return 0;
		}
		
		public static function floor (x:Number) : Number{
			return 0;
		}
		
		public static function log (x:Number) : Number{
			return 0;
		}
		
		public static function max (x:Number=-1 / 0, y:Number=-1 / 0, ...rest) : Number
		{
			return 0;
		}
		
		public static function min (x:Number=1 / 0, y:Number=1 / 0, ...rest) : Number
		{
			return 0;
		}

		
		/**
		 * 用指定值创建一个 Number 对象。此构造函数与 Number() 公共本机函数效果相同，后者可将其他类型的对象转换为基元数值。
		 * @param	num	已创建的 Number 实例的数值，或者转换为 Number 的值。如果未指定 num，则默认值为 0。使用此构造函数时不指定 num 参数与声明 Number 类型的变量时不赋值（如 var myNumber:Number）不同，后者的默认值为 NaN。未赋值的数字是未定义的，与 new Number(undefined) 等效。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public function Number (value:*=0){}

		
		
		public static function pow (x:Number, y:Number) : Number{
			return 0;
		}
		
		public static function random () : Number{
			return 0;
		}
		
		public static function round (x:Number) : Number{
			return 0;
		}
		
		public static function sin (x:Number) : Number{
			return 0;
		}
		
		public static function sqrt (x:Number) : Number{
			return 0;
		}
		
		public static function tan (x:Number) : Number{
			return 0;
		}
		

		/**
		 * 返回数字的字符串表示形式（采用指数表示法）。字符串在小数点前面包含一位，在小数点后面最多包含 20 位（在 fractionDigits 参数中指定）。
		 * @param	fractionDigits	介于 0 和 20（含）之间的整数，表示所需的小数位数。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 * @throws	RangeError 如果 fractionDigits 参数不在 0 到 20 的范围内，则会引发异常。
		 */
		public function toExponential (p:int=0) : String{
			return null;
		}

		/**
		 * 返回数字的字符串表示形式（采用定点表示法）。定点表示法是指字符串的小数点后面包含特定的位数（在 fractionDigits 参数中指定）。fractionDigits 参数的有效范围为 0 到 20。如果指定的值在此范围外，则会引发异常。
		 * @param	fractionDigits	介于 0 和 20（含）之间的整数，表示所需的小数位数。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 * @throws	RangeError 如果 fractionDigits 参数不在 0 到 20 的范围内，则会引发异常。
		 */
		
		public function toFixed (p:int=0) : String{
			return null;
		}

		
		/**
		 * 返回数字的字符串表示形式（采用指数表示法或定点表示法）。字符串将包含 precision 参数中指定的位数。
		 * @param	precision	介于 1 和 21（含）之间的整数，表示结果字符串中所需的位数。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 * @throws	RangeError 如果 precision 参数不在 1 到 21 的范围内，则会引发异常。
		 */
		public function toPrecision (p:int=0) : String{
			return null;
		}

		/**
		 * 返回指定的 Number 对象 (myNumber) 的字符串表示形式。如果 Number 对象的值是没有前导零的小数（如 .4），则 Number.toString() 将添加一个前导零 (0.4)。
		 * @param	radix	指定要用于数字到字符串的转换的基数（从 2 到 36）。如果未指定 radix 参数，则默认值为 10。
		 * @return	Number 对象作为字符串的数值表示形式。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public function toString (radix:int=10) : String{
			return null;
		}

		

		/**
		 * 返回指定的 Number 对象的基元值类型。
		 * @return	Number 对象的基元类型的值。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public function valueOf () : Number{
			return 0;
		}
	}

}