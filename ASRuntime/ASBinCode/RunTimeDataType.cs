using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{

	/// <summary>
	/// 凡大与unknow的都是对象类型
	/// </summary>
	public struct RunTimeDataType : IEquatable<RunTimeDataType>
	{
		public override string ToString()
		{
			switch (RTTI)
			{
				case rt_boolean:
					return "rt_boolean";
				case rt_int:
					return "rt_int";
				case rt_uint:
					return "rt_uint";
				case rt_number:
					return "rt_number";
				case rt_string:
					return "rt_string";
				case rt_void:
					return "rt_void";
				case rt_null:
					return "rt_null";
				case rt_function:
					return "rt_function";
				case fun_void:
					return "return_void";
				case rt_array:
					return "rt_array";
				case unknown:
					return "unknown";
				default:
					return "rt_object";   //RTTI.ToString();
			}



		}

		public string toAS3Name()
		{
			switch (RTTI)
			{
				case rt_boolean:
					return "Boolean";
				case rt_int:
					return "int";
				case rt_uint:
					return "uint";
				case rt_number:
					return "Number";
				case rt_string:
					return "String";
				case rt_void:
					return "*";
				case rt_null:
					return "null";
				case rt_function:
					return "Function";
				case fun_void:
					return "void";
				case rt_array:
					return "Array";
				case unknown:
					return "unknown";
				default:
					return "rt_object";   //RTTI.ToString();
			}
		}



		public const int rt_boolean = 0;
		public const int rt_int = 1;
		public const int rt_uint = 2;
		public const int rt_number = 3;
		public const int rt_string = 4;
		public const int rt_void = 5;
		public const int rt_null = 6;
		public const int rt_function = 7;
		public const int fun_void = 8;
		public const int rt_array = 9;
		public const int unknown = 10;


		public const int _OBJECT = unknown + 1;

		public readonly int RTTI;
		public RunTimeDataType(int rtti)
		{
			RTTI = rtti;
		}

		public static bool operator ==(RunTimeDataType v1, int v2)
		{
			return v1.RTTI == v2;
		}
		public static bool operator !=(RunTimeDataType v1, int v2)
		{
			return v1.RTTI != v2;
		}

		public static bool operator ==(int v1, RunTimeDataType v2)
		{
			return v1 == v2.RTTI;
		}
		public static bool operator !=(int v1, RunTimeDataType v2)
		{
			return v1 != v2.RTTI;
		}

		public static bool operator ==(RunTimeDataType v1, RunTimeDataType v2)
		{
			return v1.RTTI == v2.RTTI;
		}

		public static bool operator !=(RunTimeDataType v1, RunTimeDataType v2)
		{
			return v1.RTTI != v2.RTTI;
		}

		public static implicit operator RunTimeDataType(int v)
		{
			return new RunTimeDataType(v);
		}

		public static implicit operator int(RunTimeDataType t)
		{
			return t.RTTI;
		}

		public override bool Equals(object obj)
		{
			if (obj is RunTimeDataType)
			{
				return this == ((RunTimeDataType)obj);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return RTTI.GetHashCode();
		}

		public bool Equals(RunTimeDataType other)
		{
			return this == other;
		}
	}
}
