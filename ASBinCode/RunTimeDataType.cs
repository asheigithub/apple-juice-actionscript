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
