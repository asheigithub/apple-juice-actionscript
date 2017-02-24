using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime
{
    using rt = ASBinCode.RunTimeDataType;
    public class TypeConverter
    {
        public static ASBinCode.IRightValue getDefaultValue(ASBinCode.RunTimeDataType type)
        {
            switch (type)
            {
                case rt.rt_boolean:
                    return new ASBinCode.rtData.RightValue(ASBinCode.rtData.rtBoolean.False);
                case rt.rt_int:
                    return new ASBinCode.rtData.RightValue(new ASBinCode.rtData.rtInt(0));
                case rt.rt_uint:
                    return new ASBinCode.rtData.RightValue(new ASBinCode.rtData.rtUInt(0));
                case rt.rt_number:
                    return new ASBinCode.rtData.RightValue(new ASBinCode.rtData.rtNumber(0));
                case rt.rt_string:
                    return new ASBinCode.rtData.RightValue(ASBinCode.rtData.rtNull.nullptr);
                case rt.rt_void:
                    return new ASBinCode.rtData.RightValue(ASBinCode.rtData.rtUndefined.undefined);
                case rt.rt_null:
                    return new ASBinCode.rtData.RightValue(ASBinCode.rtData.rtNull.nullptr);
                case rt.unknown:
                default:
                    return null;
            }
        }

        #region 类型转换

        public static ASBinCode.rtData.rtBoolean ConvertToBoolean(ASBinCode.IRunTimeValue src, Player player, ASBinCode.SourceToken token, bool isthrow = false)
        {
            switch (src.rtType)
            {
                case rt.rt_boolean:
                    return (ASBinCode.rtData.rtBoolean)src;
                case rt.rt_int:
                    if (((ASBinCode.rtData.rtInt)src).value == 0)
                    {
                        return ASBinCode.rtData.rtBoolean.False;
                    }
                    else
                    {
                        return ASBinCode.rtData.rtBoolean.True;
                    }
                case rt.rt_uint:
                    if (((ASBinCode.rtData.rtUInt)src).value == 0)
                    {
                        return ASBinCode.rtData.rtBoolean.False;
                    }
                    else
                    {
                        return ASBinCode.rtData.rtBoolean.True;
                    }

                case rt.rt_number:
                    {
                        double r = ((ASBinCode.rtData.rtNumber)src).value;
                        if (double.IsNaN(r) || r==0)
                        {
                            return ASBinCode.rtData.rtBoolean.False;
                        }
                        else
                        {
                            return ASBinCode.rtData.rtBoolean.True;
                        }
                    }
                case rt.rt_string:
                    {
                        if (string.IsNullOrEmpty(((ASBinCode.rtData.rtString)src).value))
                        {
                            return ASBinCode.rtData.rtBoolean.False;
                        }
                        else
                        {
                            return ASBinCode.rtData.rtBoolean.True;
                        }
                    }
                case rt.rt_void:
                    return ASBinCode.rtData.rtBoolean.False;

                case rt.rt_null:
                    return ASBinCode.rtData.rtBoolean.False;
                case rt.unknown:
                default:
                    if (isthrow)
                    {
                        player.throwCastException(token, rt.unknown, rt.rt_int);
                    }
                    return null;

            }
        }


        public static int ConvertToInt(ASBinCode.IRunTimeValue src,Player player, ASBinCode.SourceToken token , bool isthrow=false)
        {
            switch (src.rtType)
            {
                case rt.rt_boolean:
                    if (((ASBinCode.rtData.rtBoolean)src).value == true)
                    {
                        return 1;// new ASBinCode.rtData.rtInt(1);
                    }
                    else
                    {
                        return 0;// ASBinCode.rtData.rtInt.zero;
                    }
                case rt.rt_int:
                    return ((ASBinCode.rtData.rtInt)src).value; //(ASBinCode.rtData.rtInt)src;
                case rt.rt_uint:
                    return (int)((ASBinCode.rtData.rtUInt)src).value; //new ASBinCode.rtData.rtInt((int)((ASBinCode.rtData.rtUInt)src).value  );
                case rt.rt_number:
                    {
                        double r = ((ASBinCode.rtData.rtNumber)src).value;
                        if (double.IsNaN(r) || double.IsInfinity(r))
                        {
                            return 0; //new ASBinCode.rtData.rtInt(0);
                        }
                        else
                        {
                            return (int)((long)r);
                             //new ASBinCode.rtData.rtInt((int)r);
                        }
                    }
                case rt.rt_string:
                    {
                        double r = 0;
                        if (((ASBinCode.rtData.rtString)src).value == null)
                        {
                            return 0;// new ASBinCode.rtData.rtInt(0);
                        }
                        else if (double.TryParse(((ASBinCode.rtData.rtString)src).value, out r))
                        {
                            if (double.IsNaN(r) || double.IsInfinity(r))
                            {
                                return 0;// new ASBinCode.rtData.rtInt(0);
                            }
                            else
                            {
                                return (int)((long)r);// new ASBinCode.rtData.rtInt((int)r);
                            }
                        }
                        else
                        {
                            return 0;// new ASBinCode.rtData.rtInt(0);
                        }
                    }
                case rt.rt_void:
                    return 0;// new ASBinCode.rtData.rtInt(0);
                    
                case rt.rt_null:
                    return 0;// new ASBinCode.rtData.rtInt(0);
                case rt.unknown: 
                default:
                    //if (isthrow)
                    //{
                    //    player.throwCastException(token, rt.unknown, rt.rt_int);
                    //}
                    return 0;
                    
            }
        }

        public static uint ConvertToUInt(ASBinCode.IRunTimeValue src, Player player, ASBinCode.SourceToken token, bool isthrow = false)
        {
            switch (src.rtType)
            {
                case rt.rt_boolean:
                    if (((ASBinCode.rtData.rtBoolean)src).value == true)
                    {
                        return 1;// new ASBinCode.rtData.rtUInt(1);
                    }
                    else
                    {
                        return 0;// ASBinCode.rtData.rtUInt.zero;
                    }

                case rt.rt_int:
                    return (uint)((ASBinCode.rtData.rtInt)src).value;// new ASBinCode.rtData.rtUInt((uint)((ASBinCode.rtData.rtInt)src).value);
                case rt.rt_uint:
                    return ((ASBinCode.rtData.rtUInt)src).value; //(ASBinCode.rtData.rtUInt)src;

                case rt.rt_number:
                    {
                        double r = ((ASBinCode.rtData.rtNumber)src).value;
                        if (double.IsNaN(r) || double.IsInfinity(r))
                        {
                            return 0;// new ASBinCode.rtData.rtUInt(0);
                        }
                        else
                        {
                            return (uint)((long)r);// new ASBinCode.rtData.rtUInt((uint)r);
                        }
                    }

                case rt.rt_string:
                    {
                        double r = 0;
                        if (((ASBinCode.rtData.rtString)src).value == null)
                        {
                            return 0;// new ASBinCode.rtData.rtUInt(0);
                        }
                        else if (double.TryParse(((ASBinCode.rtData.rtString)src).value, out r))
                        {
                            if (double.IsNaN(r) || double.IsInfinity(r))
                            {
                                return 0;// new ASBinCode.rtData.rtUInt(0);
                            }
                            else
                            {
                                return (uint)((long)r);// new ASBinCode.rtData.rtUInt((uint)r);
                            }
                        }
                        else
                        {
                            return 0;// new ASBinCode.rtData.rtUInt(0);
                            
                        }
                    }
                case rt.rt_void:
                    return 0;// new ASBinCode.rtData.rtUInt(0);

                case rt.rt_null:
                    return 0;// new ASBinCode.rtData.rtUInt(0);
                case rt.unknown:
                default:
                    return 0;

            }
        }


        public static double ConvertToNumber(ASBinCode.IRunTimeValue src, Player player, ASBinCode.SourceToken token, bool isthrow = false)
        {
            switch (src.rtType)
            {
                case rt.rt_boolean:
                    if (((ASBinCode.rtData.rtBoolean)src).value == true)
                    {
                        return 1;// new ASBinCode.rtData.rtNumber(1);
                    }
                    else
                    {
                        return 0;// ASBinCode.rtData.rtNumber.zero;
                    }
                case rt.rt_int:
                    return ((ASBinCode.rtData.rtInt)src).value;// new ASBinCode.rtData.rtNumber(((ASBinCode.rtData.rtInt)src).value);
                case rt.rt_uint:
                    return ((ASBinCode.rtData.rtUInt)src).value;// new ASBinCode.rtData.rtNumber(((ASBinCode.rtData.rtUInt)src).value);

                case rt.rt_number:
                    return ((ASBinCode.rtData.rtNumber)src).value; //(ASBinCode.rtData.rtNumber)src;

                case rt.rt_string:
                    {
                        double r = 0;
                        if (((ASBinCode.rtData.rtString)src).value == null)
                        {
                            return 0;// new ASBinCode.rtData.rtNumber(0);
                        }
                        else if (double.TryParse(((ASBinCode.rtData.rtString)src).value, out r))
                        {
                            return r;//new ASBinCode.rtData.rtNumber(r);
                        }
                        else
                        {
                            return double.NaN; //new ASBinCode.rtData.rtNumber(double.NaN);
                        }
                    }
                case rt.rt_void:
                    return double.NaN; //new ASBinCode.rtData.rtNumber(double.NaN);

                case rt.rt_null:
                    return 0;// new ASBinCode.rtData.rtNumber(0);
                case rt.unknown:
                default:
                    return 0;// null;

            }
        }


        public static string ConvertToString(ASBinCode.IRunTimeValue src, Player player, ASBinCode.SourceToken token, bool isthrow = false)
        {
            switch (src.rtType)
            {
                case rt.rt_boolean:
                    return ((ASBinCode.rtData.rtBoolean)src).ToString();// new ASBinCode.rtData.rtString(((ASBinCode.rtData.rtBoolean)src).ToString());
                case rt.rt_int:
                    return ((ASBinCode.rtData.rtInt)src).ToString();// new ASBinCode.rtData.rtString(((ASBinCode.rtData.rtInt)src).value.ToString());
                case rt.rt_uint:
                    return ((ASBinCode.rtData.rtUInt)src).value.ToString();// new ASBinCode.rtData.rtString(((ASBinCode.rtData.rtUInt)src).value.ToString());
                case rt.rt_number:
                    return ((ASBinCode.rtData.rtNumber)src).ToString();// new ASBinCode.rtData.rtString(((ASBinCode.rtData.rtNumber)src).value.ToString());

                case rt.rt_string:
                    return ((ASBinCode.rtData.rtString)src).value;//(ASBinCode.rtData.rtString)src;
                case rt.rt_void:
                    return null; //new ASBinCode.rtData.rtString(null);
                case rt.rt_null:
                    return null;//new ASBinCode.rtData.rtString(null);
                case rt.unknown:
                default:
                    return null;

            }
        }
        #endregion



            /// <summary>
            /// 隐式类型转换表
            /// </summary>
            /// <param name="f"></param>
            /// <param name="t"></param>
            /// <returns></returns>
        public static bool testImplicitConvert(ASBinCode.RunTimeDataType f, ASBinCode.RunTimeDataType t)
        {
            return implicitcoverttable[(int)f, (int)t];
        }

        private static bool[,] implicitcoverttable =
            {
            /*------*/  //bool  int   uint    number  string  var_void  null  unknown
            /*bool   */ { true  ,true  ,true   ,true   ,false   ,true  ,false  ,false  }, 
            /*int   */  { true  ,true  ,true   ,true   ,false   ,true  ,true   ,false  },  
            /*uint  */  { true  ,true  ,true   ,true   ,false   ,true  ,true   ,false  },  
            /*number*/  { true  ,true  ,true   ,true   ,false   ,true  ,true   ,false  },
            /*string*/  { true  ,false ,false  ,false  ,true   ,true  ,true   ,false  },
            /*var_void*/{ true  ,true  ,true   ,true   ,true   ,true  ,true   ,false  },
            /*null*/    { true  ,true  ,true   ,true   ,true   ,true  ,true   ,false  },
            /*unknown*/ { false ,false ,false  ,false  ,false  ,false ,false  ,false  }
            };
        
        public static rt getImplicitOpType(ASBinCode.RunTimeDataType v1, ASBinCode.RunTimeDataType v2, ASBinCode.OpCode op)
        {
            if (op == ASBinCode.OpCode.add)
            {
                return implicit_opadd_coverttable[(int)v1, (int)v2];
            }
            else if (op == ASBinCode.OpCode.sub)
            {
                return implicit_opsub_coverttable[(int)v1, (int)v2];
            }
            else if (op == ASBinCode.OpCode.multi || op== ASBinCode.OpCode.div || op== ASBinCode.OpCode.mod)
            {
                return implicit_opmulti_coverttable[(int)v1, (int)v2];
            }
            else
            {
                return rt.unknown;
            }
        }

        //+ 操作隐式类型转换表
        private static rt[,] implicit_opadd_coverttable =
            {
            /*------*/    //bool        int             uint            number          string          var_void        null      unknown
            /*boolean*/ { rt.rt_number  ,rt.rt_number  ,rt.rt_number   ,rt.rt_number   ,rt.rt_string   ,rt.rt_void     ,rt.rt_int,rt.unknown  },
            /*int*/     { rt.rt_number  ,rt.rt_number  ,rt.rt_number   ,rt.rt_number   ,rt.rt_string   ,rt.rt_void     ,rt.rt_int,rt.unknown  },
            /*uint*/    { rt.rt_number  ,rt.rt_number  ,rt.rt_number   ,rt.rt_number   ,rt.rt_string   ,rt.rt_void     ,rt.rt_uint,rt.unknown  },
            /*number*/  { rt.rt_number  ,rt.rt_number  ,rt.rt_number   ,rt.rt_number   ,rt.rt_string   ,rt.rt_void     ,rt.rt_number,rt.unknown  },
            /*string*/  { rt.rt_string  ,rt.rt_string  ,rt.rt_string   ,rt.rt_string   ,rt.rt_string   ,rt.rt_void     ,rt.rt_string,rt.unknown  },
            /*var_void*/{ rt.rt_void    ,rt.rt_void    ,rt.rt_void     ,rt.rt_void     ,rt.rt_void     ,rt.rt_void     ,rt.rt_void ,rt.unknown  },
            /*null*/    { rt.rt_boolean ,rt.rt_int     ,rt.rt_uint     ,rt.rt_number   ,rt.rt_string   ,rt.rt_void     ,rt.rt_number ,rt.unknown  },
            /*unknown*/ { rt.unknown    ,rt.unknown    ,rt.unknown     ,rt.unknown     ,rt.unknown     ,rt.unknown     ,rt.unknown,rt.unknown  }
            };
        //- 操作隐式类型转换表
        private static rt[,] implicit_opsub_coverttable =
            {
            /*------*/    //bool        int             uint            number          string          var_void       null         unknown
            /*boolean*/ { rt.unknown    ,rt.unknown     ,rt.unknown   ,rt.unknown   ,rt.unknown     ,rt.unknown     ,rt.unknown      ,rt.unknown  },
            /*int*/     { rt.unknown    ,rt.rt_number  ,rt.rt_number   ,rt.rt_number   ,rt.unknown   ,rt.rt_void     ,rt.rt_int      ,rt.unknown  },
            /*uint*/    { rt.unknown    ,rt.rt_number  ,rt.rt_number   ,rt.rt_number   ,rt.unknown   ,rt.rt_void     ,rt.rt_uint     ,rt.unknown  },
            /*number*/  { rt.unknown    ,rt.rt_number  ,rt.rt_number   ,rt.rt_number   ,rt.unknown   ,rt.rt_void     ,rt.rt_number   ,rt.unknown  },
            /*string*/  { rt.unknown    ,rt.unknown  ,rt.unknown   ,rt.unknown   ,rt.unknown     ,rt.unknown     ,rt.unknown     ,rt.unknown  },
            /*var_void*/{ rt.unknown    ,rt.rt_void    ,rt.rt_void     ,rt.rt_void     ,rt.unknown     ,rt.rt_void     ,rt.rt_void     ,rt.unknown  },
            /*null*/    { rt.unknown    ,rt.rt_number  ,rt.rt_number   ,rt.rt_number   ,rt.unknown     ,rt.rt_void     ,rt.rt_number     ,rt.unknown  },
            /*unknown*/ { rt.unknown    ,rt.unknown    ,rt.unknown     ,rt.unknown     ,rt.unknown     ,rt.unknown     ,rt.unknown     ,rt.unknown  }
            };


        //* / % 操作隐式类型转换表
        private static rt[,] implicit_opmulti_coverttable =
            {
            /*------*/    //bool        int             uint            number          string          var_void       null         unknown
            /*boolean*/ { rt.unknown    ,rt.unknown     ,rt.unknown   ,rt.unknown   ,rt.unknown     ,rt.unknown     ,rt.unknown      ,rt.unknown  },
            /*int*/     { rt.unknown    ,rt.rt_number  ,rt.rt_number   ,rt.rt_number   ,rt.unknown   ,rt.rt_void     ,rt.rt_int      ,rt.unknown  },
            /*uint*/    { rt.unknown    ,rt.rt_number  ,rt.rt_number   ,rt.rt_number   ,rt.unknown   ,rt.rt_void     ,rt.rt_uint     ,rt.unknown  },
            /*number*/  { rt.unknown    ,rt.rt_number  ,rt.rt_number   ,rt.rt_number   ,rt.unknown   ,rt.rt_void     ,rt.rt_number   ,rt.unknown  },
            /*string*/  { rt.unknown    ,rt.unknown  ,rt.unknown   ,rt.unknown   ,rt.unknown     ,rt.unknown     ,rt.unknown     ,rt.unknown  },
            /*var_void*/{ rt.unknown    ,rt.rt_void    ,rt.rt_void     ,rt.rt_void     ,rt.unknown     ,rt.rt_void     ,rt.rt_void     ,rt.unknown  },
            /*null*/    { rt.unknown    ,rt.rt_number  ,rt.rt_number   ,rt.rt_number   ,rt.unknown     ,rt.rt_void     ,rt.rt_number     ,rt.unknown  },
            /*unknown*/ { rt.unknown    ,rt.unknown    ,rt.unknown     ,rt.unknown     ,rt.unknown     ,rt.unknown     ,rt.unknown     ,rt.unknown  }
            };

    }
}
