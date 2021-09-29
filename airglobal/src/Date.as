package
{
	public final dynamic class Date
	{
		public function get date () : Number{ return 0; }

		public function set date (value:Number) : void{  }
		
		
		public function get dateUTC () : Number{ return 0; }
		public function set dateUTC (value:Number) : void { }
		
		public function get day () : Number{ return 0; }
		
		public function get dayUTC () : Number{ return 0; }
		
		public function get fullYear () : Number{ return 0; }

		public function set fullYear (value:Number) : void{  }

		
		public function get fullYearUTC () : Number{ return 0; }

		public function set fullYearUTC (value:Number) : void{ }
		
		public function get hours () : Number{ return 0; }
		public function set hours (value:Number) : void{  }
		
		public function get hoursUTC () : Number{ return 0; }
		public function set hoursUTC (value:Number) : void { }

		
		public function get milliseconds () : Number{ return 0; }
		public function set milliseconds (value:Number) : void{  }
		
		public function get millisecondsUTC () : Number{ return 0; }
		public function set millisecondsUTC (value:Number) : void{ }
		
		public function get minutes () : Number{ return 0; }
		public function set minutes (value:Number) : void{  }
		
		public function get minutesUTC () : Number{ return 0; }
		public function set minutesUTC (value:Number) : void{  }
		
		public function get month () : Number{ return 0; }
		public function set month (value:Number) : void{}
		
		public function get monthUTC () : Number{ return 0; }
		public function set monthUTC (value:Number) : void{}
		
		public function get seconds () : Number{ return 0; }
		public function set seconds (value:Number) : void{}
		
		public function get secondsUTC () : Number{ return 0; }
		public function set secondsUTC (value:Number) : void{}
		
		public function get time () : Number{ return 0; }
		public function set time (value:Number) : void{}
		
		public function get timezoneOffset () : Number{ return 0; }
		

		
		public function Date(yearOrTimevalue:*=null, 
			month:Number =NaN, 
			date:Number = 1, 
			hour:Number = 0, 
			minute:Number = 0, 
			second:Number = 0, 
			millisecond:Number = 0){}


		/**
		 * 按照本地时间返回 Date 对象指定的月中某天的值（1 到 31 之间的一个整数）。本地时间由运行 Flash 运行时的操作系统确定。
		 * @return	Date 对象代表的月中某天的值 (1 - 31)。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function getDate () : Number{ return 0; }

		/**
		 * 按照本地时间返回该 Date 所指定的星期值（0 代表星期日，1 代表星期一，依此类推）。本地时间由运行 Flash 运行时的操作系统确定。
		 * @return	Date 对象代表的星期值的数字版本 (0 - 6)。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function getDay () : Number{ return 0; }

		/**
		 * 按照本地时间返回 Date 对象中的完整年份值（一个 4 位数，如 2000）。本地时间由运行 Flash 运行时的操作系统确定。
		 * @return	Date 对象代表的完整年份值。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function getFullYear () : Number{ return 0; }

		/**
		 * 按照本地时间返回 Date 对象中一天的小时值（0 到 23 之间的一个整数）。本地时间由运行 Flash 运行时的操作系统确定。
		 * @return	Date 对象代表的一天中的小时值 (0 - 23)。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function getHours () : Number{ return 0; }

		/**
		 * 按照本地时间返回 Date 对象中的毫秒值（0 到 999 之间的一个整数）部分。本地时间由运行 Flash 运行时的操作系统确定。
		 * @return	Date 对象中的毫秒值部分。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function getMilliseconds () : Number{ return 0; }

		/**
		 * 按照本地时间返回 Date 对象中的分钟值（0 到 59 之间的一个整数）部分。本地时间由运行 Flash 运行时的操作系统确定。
		 * @return	Date 对象中的分钟值部分。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function getMinutes () : Number{ return 0; }

		/**
		 * 按照本地时间返回该  Date 中的月份值（0 代表一月，1 代表二月，依此类推）部分。本地时间由运行 Flash 运行时的操作系统确定。
		 * @return	Date 对象中的月份值 (0 - 11) 部分。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function getMonth () : Number{ return 0; }

		/**
		 * 按照本地时间返回 Date 对象中的秒值（0 到 59 之间的一个整数）部分。本地时间由运行 Flash 运行时的操作系统确定。
		 * @return	Date 对象中的秒值（0 到 59）部分。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function getSeconds () : Number{ return 0; }

		/**
		 * 按照通用时间返回 Date 对象中自 1970 年 1 月 1 日午夜以来的毫秒数。在比较两个或更多个 Date 对象时，可使用此方法表示某一特定时刻。
		 * @return	Date 对象代表的自 1970 年 1 月 1 日以来的毫秒数。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function getTime () : Number{ return 0; }
		

		/**
		 * 以分钟为单位，返回计算机的本地时间和通用时间 (UTC) 之间的差值。
		 * @return	需要添加到计算机本地时间值中以与 UTC 相等的分钟数。如果计算机时间设置晚于 UTC，则返回值将为负数。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function getTimezoneOffset () : Number{ return 0; }

		/**
		 * 按照通用时间 (UTC) 返回 Date 对象中表示月中某天的值（1 到 31 之间的一个整数）。
		 * @return	Date 对象代表的 UTC 时间月中某天的值（1 到 31）。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function getUTCDate () : Number{ return 0; }

		/**
		 * 按照通用时间 (UTC) 返回该 Date  中表示星期的值（0 代表星期日，1 代表星期一，依此类推）。
		 * @return	Date 对象代表的 UTC 时间的星期值（0 到 6）。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function getUTCDay () : Number{ return 0; }

		/**
		 * 按照通用时间 (UTC) 返回 Date 对象中的四位数年份值。
		 * @return	Date 对象代表的 UTC 时间的四位数年份值。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function getUTCFullYear () : Number{ return 0; }

		/**
		 * 按照通用时间 (UTC) 返回 Date 对象中一天的小时值（0 到 23 之间的一个整数）。
		 * @return	Date 对象代表的一天中的 UTC 小时值（0 到 23）。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function getUTCHours () : Number{ return 0; }

		/**
		 * 按照通用时间 (UTC) 返回 Date 对象中的毫秒值（0 到 999 之间的一个整数）部分。
		 * @return	Date 对象中的 UTC 毫秒值部分。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function getUTCMilliseconds () : Number{ return 0; }

		/**
		 * 按照通用时间 (UTC) 返回 Date 对象中的分钟值（0 到 59 之间的一个整数）部分。
		 * @return	Date 对象中的 UTC 分钟值部分。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function getUTCMinutes () : Number{ return 0; }

		/**
		 * 按照通用时间 (UTC) 返回 Date 对象中的月份值（0 [一月] 到 11 [十二月]）部分。
		 * @return	Date 对象中的 UTC 月份值部分。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function getUTCMonth () : Number{ return 0; }

		/**
		 * 按照通用时间 (UTC) 返回 Date 对象中的秒值（0 到 59 之间的一个整数）部分。
		 * @return	Date 对象中的 UTC 秒值部分。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function getUTCSeconds () : Number{ return 0; }

		/**
		 * 按照 UTC 将表示日期的字符串转换为一个数字，它等于自 1970 年 1 月 1 日起已经过的毫秒数。
		 * @param	date	日期的字符串表示形式，符合 Date.toString() 的输出格式。Date.toString() 输出的日期格式为：
		 *   
		 *     Day Mon DD HH:MM:SS TZD YYYY
		 *   例如： 
		 *   Wed Apr 12 15:30:17 GMT-0700 2006
		 *   Time Zone Designation (TZD) 的形式始终为 GMT-HHMM 或 UTC-HHMM，这表明小时和分钟偏移相对于格林尼治平均时（GMT，现也称通用时间 (UTC)）。年月日之间可用正斜杠 (/) 或空格隔开，一定不要用短划线 (-) 隔开。下面是受支持的其他格式（可以包括这些格式的部分表示形式，即，只包括月、日和年）：
		 *   MM/DD/YYYY HH:MM:SS TZD
		 *   HH:MM:SS TZD Day Mon/DD/YYYY 
		 *   Mon DD YYYY HH:MM:SS TZD
		 *   Day Mon DD HH:MM:SS TZD YYYY
		 *   Day DD Mon HH:MM:SS TZD YYYY
		 *   Mon/DD/YYYY HH:MM:SS TZD
		 *   YYYY/MM/DD HH:MM:SS TZD
		 * @return	表示按 UTC 自 1970 年 1 月 1 日起已经过的毫秒数。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		public static function parse (s:String) : Number
		{
			return 0;
		}

		/**
		 * 按照本地时间设置月中的某天，并以毫秒为单位返回新时间。本地时间由运行 Flash 运行时的操作系统确定。
		 * @param	day	1 到 31 之间的一个整数。
		 * @return	新时间，以毫秒为单位。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function setDate (day:Number) : Number{ return 0; }

		/**
		 * 按照本地时间设置年份值，并以毫秒为单位返回新时间。如果指定了 month 和 day 参数，则将它们设置为本地时间。本地时间由运行 Flash 运行时的操作系统确定。
		 * 
		 *   调用此方法不会修改 Date 的其他字段，但如果因调用该方法而导致星期值发生变化，则 Date.getUTCDay() 和 Date.getDay() 会报告一个新值。
		 * @param	year	指定年份的 4 位数。两位数的数字不代表四位数的年份；例如，99 不代表 1999 年，而是表示 99 年。
		 * @param	month	0（一月）到 11（十二月）之间的一个整数。
		 * @param	day	1 到 31 之间的一个数字。
		 * @return	新时间，以毫秒为单位。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function setFullYear (year:Number, month:Number=NaN, date:Number=NaN) : Number{ return 0; }

		/**
		 * 按照本地时间设置小时值，并以毫秒为单位返回新时间。本地时间由运行 Flash 运行时的操作系统确定。
		 * @param	hour	0（午夜）到 23（晚上 11 点）之间的一个整数。
		 * @param	minute	0 到 59 之间的一个整数。
		 * @param	second	0 到 59 之间的一个整数。
		 * @param	millisecond	0 到 999 之间的一个整数。
		 * @return	新时间，以毫秒为单位。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function setHours (hour:Number, min:Number=NaN, sec:Number=NaN, ms:Number=NaN) : Number{ return 0; }

		/**
		 * 按照本地时间设置毫秒值，并以毫秒为单位返回新时间。本地时间由运行 Flash 运行时的操作系统确定。
		 * @param	millisecond	0 到 999 之间的一个整数。
		 * @return	新时间，以毫秒为单位。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function setMilliseconds (ms:Number) : Number{ return 0; }

		/**
		 * 按照本地时间设置分钟值，并以毫秒为单位返回新时间。本地时间由运行 Flash 运行时的操作系统确定。
		 * @param	minute	0 到 59 之间的一个整数。
		 * @param	second	0 到 59 之间的一个整数。
		 * @param	millisecond	0 到 999 之间的一个整数。
		 * @return	新时间，以毫秒为单位。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function setMinutes (min:Number, sec:Number=NaN, ms:Number=NaN) : Number{ return 0; }

		/**
		 * 按照本地时间设置月份值以及（可选）日期值，并以毫秒为单位返回新时间。本地时间由运行 Flash 运行时的操作系统确定。
		 * @param	month	0（一月）到 11（十二月）之间的一个整数。
		 * @param	day	1 到 31 之间的一个整数。
		 * @return	新时间，以毫秒为单位。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function setMonth (month:Number, date:Number=NaN) : Number{ return 0; }

		/**
		 * 按照本地时间设置秒值，并以毫秒为单位返回新时间。本地时间由运行 Flash 运行时的操作系统确定。
		 * @param	second	0 到 59 之间的一个整数。
		 * @param	millisecond	0 到 999 之间的一个整数。
		 * @return	新时间，以毫秒为单位。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function setSeconds (sec:Number, ms:Number=NaN) : Number{ return 0; }

		/**
		 * 以毫秒为单位设置自 1970 年 1 月 1 日午夜以来的日期，并以毫秒为单位返回新时间。
		 * @param	millisecond	一个整数值，其中 0 表示通用时间 (UTC) 1 月 1 日午夜。
		 * @return	新时间，以毫秒为单位。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function setTime (t:Number) : Number{ return 0; }

		/**
		 * 按照通用时间 (UTC) 设置日期值，并以毫秒为单位返回新时间。调用此方法不会修改 Date  对象的其他字段，但如果因调用该方法而导致星期值发生变化，则 Date.getUTCDay() 和 Date.getDay() 方法会报告一个新值。
		 * @param	day	一个数字；1 到 31 之间的一个整数。
		 * @return	新时间，以毫秒为单位。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function setUTCDate (date:Number) : Number{ return 0; }

		/**
		 * 按照通用时间 (UTC) 设置年份值，并以毫秒为单位返回新时间。
		 * 
		 *   另外，还可以使用该方法设置月份和月中的某一天。调用此方法不会修改其他字段，但如果因调用该方法而导致星期值发生变化，则 Date.getUTCDay() 和 Date.getDay() 方法会报告一个新值。
		 * @param	year	一个整数，表示以完整的四位数年份形式指定的年份，如 2000。
		 * @param	month	0（一月）到 11（十二月）之间的一个整数。
		 * @param	day	1 到 31 之间的一个整数。
		 * @return	一个整数。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function setUTCFullYear (year:Number, month:Number=NaN, date:Number=NaN) : Number{ return 0; }

		/**
		 * 按照通用时间 (UTC) 设置小时值，并以毫秒为单位返回新时间。还可指定分钟值、秒值和毫秒值。
		 * @param	hour	0（午夜）到 23（晚上 11 点）之间的一个整数。
		 * @param	minute	0 到 59 之间的一个整数。
		 * @param	second	0 到 59 之间的一个整数。
		 * @param	millisecond	0 到 999 之间的一个整数。
		 * @return	新时间，以毫秒为单位。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function setUTCHours (hour:Number, min:Number=NaN, sec:Number=NaN, ms:Number=NaN) : Number{ return 0; }

		/**
		 * 按照通用时间 (UTC) 设置毫秒值，并以毫秒为单位返回新时间。
		 * @param	millisecond	0 到 999 之间的一个整数。
		 * @return	新时间，以毫秒为单位。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function setUTCMilliseconds (ms:Number) : Number{ return 0; }

		/**
		 * 按照通用时间 (UTC) 设置分钟值，并以毫秒为单位返回新时间。还可以指定秒值和毫秒值。
		 * @param	minute	0 到 59 之间的一个整数。
		 * @param	second	0 到 59 之间的一个整数。
		 * @param	millisecond	0 到 999 之间的一个整数。
		 * @return	新时间，以毫秒为单位。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function setUTCMinutes (min:Number, sec:Number=NaN, ms:Number=NaN) : Number{ return 0; }

		/**
		 * 按照通用时间 (UTC) 设置月份值及（可选）日期值，并以毫秒为单位返回新时间。调用此方法不会修改其他字段，但如果因调用该方法而导致星期值发生变化，则 Date.getUTCDay() 和 Date.getDay() 方法可能会报告一个新值。
		 * @param	month	0（一月）到 11（十二月）之间的一个整数。
		 * @param	day	1 到 31 之间的一个整数。
		 * @return	新时间，以毫秒为单位。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function setUTCMonth (month:Number, date:Number=NaN) : Number{ return 0; }

		/**
		 * 按照通用时间 (UTC) 设置秒值以及（可选）毫秒值，并以毫秒为单位返回新时间。
		 * @param	second	0 到 59 之间的一个整数。
		 * @param	millisecond	0 到 999 之间的一个整数。
		 * @return	新时间，以毫秒为单位。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function setUTCSeconds (sec:Number, ms:Number=NaN) : Number{ return 0; }

		/**
		 * 仅返回星期值和日期值的字符串表示形式，而不返回时间或时区。对比下列方法：
		 * Date.toTimeString() 仅返回时间和时区Date.toString() 不仅返回星期值和日期值，还返回时间和时区。
		 * @return	仅返回星期值和日期值的 String 表示形式。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function toDateString () : String{return null; }

		/**
		 * 仅返回星期值和日期值的字符串表示形式，而不返回时间或时区。该方法返回与 Date.toDateString 相同的值。对比下列方法：
		 * Date.toTimeString() 仅返回时间和时区Date.toString() 不仅返回星期值和日期值，还返回时间和时区。
		 * @return	仅返回星期值和日期值的 String 表示形式。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function toLocaleDateString () : String{return null; }

		/**
		 * 按本地时间返回星期值、日期值以及时间的字符串表示形式。对比 Date.toString() 方法，该方法返回相同的信息（包括时区），且年份值列在字符串的末尾。
		 * @return	Date 对象在本地时区中的字符串表示形式。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function toLocaleString () : String{return null; }

		/**
		 * 仅返回时间的字符串表示形式，而不返回星期值、日期值、年份或时区。对比 Date.toTimeString() 方法，该方法返回时间和时区。
		 * @return	仅返回时间和时区的字符串表示形式。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function toLocaleTimeString () : String{return null; }

		/**
		 * 返回星期值、日期值、时间和时区的字符串表示形式。输出的日期格式为：
		 * 
		 *   Day Mon Date HH:MM:SS TZD YYYY
		 * 例如：
		 * Wed Apr 12 15:30:17 GMT-0700 2006
		 * @return	Date 对象的字符串表示形式。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function toString () : String{ return null; }

		/**
		 * 仅返回时间和时区的字符串表示形式，而不返回星期值和日期值。对比 Date.toDateString() 方法，该方法只返回星期值和日期值。
		 * @return	仅返回时间和时区的字符串表示形式。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function toTimeString () : String{ return null; }

		/**
		 * 按照通用时间 (UTC) 返回星期值、日期值以及时间的字符串表示形式。例如，对于日期 2005 年 2 月 1 日，返回的字符串表示形式为 Tue Feb 1 00:00:00 2005 UTC。
		 * @return	Date 对象的字符串表示形式（采用 UTC 时间）。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function toUTCString () : String{ return null; }

		/**
		 * 返回 1970 年 1 月 1 日午夜（通用时间）与参数中指定的时间之间相差的毫秒数。该方法使用通用时间，而 Date 构造函数使用本地时间。
		 * 如果要将一个 UTC 日期传递给 Date 类构造函数，则该方法很有用。因为 Date 类构造函数接受参数形式的毫秒偏移，所以可以使用 Date.UTC() 方法将 UTC 日期转换成相应的毫秒偏移，然后将该偏移作为参数发送给 Date 类构造函数：
		 * @param	year	表示年份的四位数整数（例如，2000）。
		 * @param	month	0（一月）到 11（十二月）之间的一个整数。
		 * @param	date	1 到 31 之间的一个整数。
		 * @param	hour	0（午夜）到 23（晚上 11 点）之间的一个整数。
		 * @param	minute	0 到 59 之间的一个整数。
		 * @param	second	0 到 59 之间的一个整数。
		 * @param	millisecond	0 到 999 之间的一个整数。
		 * @return	自 1970 年 1 月 1 日起的毫秒数以及指定日期和时间。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public static function UTC (year:Number, 
			month:Number, 
			date:Number = 1, 
			hour:Number = 0, 
			minute:Number = 0, 
			second:Number = 0, 
			millisecond:Number = 0) : Number{ return 0; }

		/**
		 * 按照通用时间返回 Date 对象中自 1970 年 1 月 1 日午夜以来的毫秒数。
		 * @return	Date 对象表示的自 1970 年 1 月 1 日以来的毫秒数。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	AIR 1.0
		 * @playerversion	Lite 4
		 */
		
		public function valueOf () : Number{ return 0; }
	}
}
