using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{


    //public enum RunTimeDataType
    //{
    //    rt_boolean=0,
    //    rt_int=1,
    //    rt_uint=2,
    //    rt_number=3,
    //    rt_string=4,
    //    rt_void=5,
    //    rt_null=6,
    //    unknown=7
    //}


    public struct RunTimeDataType
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
                case unknown:
                    return "unknown";
                default:
                    return RTTI.ToString();
            }


            
        }

        public const int rt_boolean = 0;
        public const int rt_int = 1;
        public const int rt_uint = 2;
        public const int rt_number = 3;
        public const int rt_string = 4;
        public const int rt_void = 5;
        public const int rt_null = 6;
        public const int unknown = 7;

        public readonly int RTTI;
        public RunTimeDataType(int rtti)
        {
            RTTI = rtti;
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
    }
}
