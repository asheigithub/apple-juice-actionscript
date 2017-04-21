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
                    return new ASBinCode.rtData.RightValue(new ASBinCode.rtData.rtNumber(double.NaN));
                case rt.rt_string:
                    return new ASBinCode.rtData.RightValue(ASBinCode.rtData.rtNull.nullptr);
                case rt.rt_void:
                case rt.fun_void:
                    return new ASBinCode.rtData.RightValue(ASBinCode.rtData.rtUndefined.undefined);
                case rt.rt_null:
                case rt.rt_function:
                case rt.rt_array:
                    return new ASBinCode.rtData.RightValue(ASBinCode.rtData.rtNull.nullptr);
                case rt.unknown:
                    return null;
                default:
                    return new ASBinCode.rtData.RightValue(ASBinCode.rtData.rtNull.nullptr);
            }
        }

        #region 类型转换

        internal static ASBinCode.rtData.rtBoolean ConvertToBoolean(ASBinCode.IRunTimeValue src, StackFrame frame, ASBinCode.SourceToken token, bool isthrow = false)
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
                case rt.fun_void:
                    return ASBinCode.rtData.rtBoolean.False;

                case rt.rt_null:
                    return ASBinCode.rtData.rtBoolean.False;
                case rt.rt_function:
                case rt.rt_array:
                    return ASBinCode.rtData.rtBoolean.True;
                case rt.unknown:
                    if (isthrow)
                    {
                        frame.throwCastException(token, rt.unknown, rt.rt_boolean);
                    }
                    return ASBinCode.rtData.rtBoolean.False;
                default:
                    //凡是大于unknown的都是对象
                    return ASBinCode.rtData.rtBoolean.True;

            }
        }


        internal static int ConvertToInt(ASBinCode.IRunTimeValue src,StackFrame frame, ASBinCode.SourceToken token , bool isthrow=false)
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
                case rt.fun_void:
                    return 0;// new ASBinCode.rtData.rtInt(0);
                    
                case rt.rt_null:
                case rt.rt_function:
                case rt.rt_array:
                    return 0;// new ASBinCode.rtData.rtInt(0);
                case rt.unknown: 
                default:
                    //if (isthrow)
                    //{
                    //    frame.throwCastException(token, rt.unknown, rt.rt_int);
                    //}
                    return 0;
                    
            }
        }

        internal static uint ConvertToUInt(ASBinCode.IRunTimeValue src, StackFrame frame, ASBinCode.SourceToken token, bool isthrow = false)
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
                case rt.fun_void:
                    return 0;// new ASBinCode.rtData.rtUInt(0);

                case rt.rt_null:
                    return 0;// new ASBinCode.rtData.rtUInt(0);
                case rt.unknown:
                case rt.rt_function:
                case rt.rt_array:
                default:
                    return 0;

            }
        }


        internal static double ConvertToNumber(ASBinCode.IRunTimeValue src, StackFrame frame, ASBinCode.SourceToken token, bool isthrow = false)
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
                case rt.fun_void:
                    return double.NaN; //new ASBinCode.rtData.rtNumber(double.NaN);

                case rt.rt_null:
                    return 0;// new ASBinCode.rtData.rtNumber(0);
                case rt.rt_function:
                case rt.rt_array:
                    return double.NaN;
                case rt.unknown:
                    return double.NaN;
                default:
                    return double.NaN;// null;

            }
        }


        internal static string ConvertToString(ASBinCode.IRunTimeValue src, StackFrame frame, ASBinCode.SourceToken token, bool isthrow = false)
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
                case rt.fun_void:
                    return ASBinCode.rtData.rtUndefined.undefined.ToString();
                case rt.rt_null:
                    return null;//new ASBinCode.rtData.rtString(null);
                case rt.rt_function:
                    return "function Function() {}";
                case rt.rt_array:
                    return ((ASBinCode.rtData.rtArray)src).ToString();
                case rt.unknown:
                    return null;
                default:
                    {
                        //ASBinCode.rtData.rtObject obj = (ASBinCode.rtData.rtObject)src;
                        //var toStr = (ASBinCode.ClassMemberFinder.find(obj.value._class, "toString", obj.value._class));
                        //if (toStr.type == rt.rt_function && !toStr.isStatic && toStr.isPublic
                        //    && !toStr.isConstructor
                        //    )
                        //{
                        //    operators.FunctionCaller fc = new operators.FunctionCaller(frame.player, frame, token);
                        //    fc.function = (ASBinCode.rtData.rtFunction)obj.value.memberData[toStr.index].getValue();
                        //    fc.loadDefineFromFunction();
                        //    fc.createParaScope();
                        //    fc.returnSlot = frame._tempSlot;
                        //    fc.call();

                        //    if (frame.player.step_toblockend())
                        //    {
                        //        return ConvertToString(fc.returnSlot.getValue(), frame, token, isthrow);
                        //    }
                        //    else
                        //    {
                        //        return null;
                        //    }
                        //}
                        //else
                        {
                            return ((ASBinCode.rtData.rtObject)src).value.ToString();
                        }
                    }

                    
                    //return "object " + ((ASBinCode.rtData.rtObject)src).value._class.name;

            }
        }
        #endregion

        /// <summary>
        /// 比较值src是否和值dst类型匹配
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        /// <returns></returns>
        public static bool testTypeMatch(ASBinCode.IRunTimeValue src,ASBinCode.RunTimeDataType dsttype)
        {
            if (src.rtType >rt.unknown || dsttype >rt.unknown)
            {
#if DEBUG
                throw new NotImplementedException();
#else
                return false;
#endif
            }

            if (dsttype == rt.rt_void || src.rtType == dsttype)
                return true;

            if (src.rtType == rt.rt_number && dsttype == rt.rt_int)
            {
                double v = ((ASBinCode.rtData.rtNumber)src).value;
                if (v == (int)(long)v)
                {
                    return true;
                }
            }
            if (src.rtType == rt.rt_number && dsttype == rt.rt_uint)
            {
                double v = ((ASBinCode.rtData.rtNumber)src).value;
                if (v == (uint)(long)v)
                {
                    return true;
                }
            }
            if (src.rtType == rt.rt_int || src.rtType == rt.rt_uint)
            {
                return dsttype == rt.rt_number;
            }


            return false;
        }

       

            /// <summary>
            /// 隐式类型转换表
            /// </summary>
            /// <param name="f"></param>
            /// <param name="t"></param>
            /// <returns></returns>
        public static bool testImplicitConvert(ASBinCode.RunTimeDataType f, ASBinCode.RunTimeDataType t,ASBinCode.IClassFinder classfinder)
        {
            if (f == t)
            {
                return true;
            }

            if (f > rt.unknown && t == ASBinCode.RunTimeDataType.rt_void)
            {
                return true;
            }

            if (f > rt.unknown && t == ASBinCode.RunTimeDataType.rt_string)
            {
                return false;
            }

            if (f == rt.rt_void && t > rt.unknown)
            {
                return true;
            }
            if (f == rt.rt_null && t > rt.unknown)
            {
                return true;
            }

            if (f > rt.unknown && t < rt.unknown)
            {
                rt outtype;

                if (Object_CanImplicit_ToPrimitive(f, classfinder, out outtype))
                {
                    f = outtype;
                }
                else
                {
                    return false;
                } 
            }

            if (f < rt.unknown && t > rt.unknown)
            {
                var tc = classfinder.getClassByRunTimeDataType(t);
                if (tc.implicit_from != null)
                {
                    return testImplicitConvert(f, tc.implicit_from_type,classfinder);
                }
                else
                {
                    return false;
                }
            }


            if (f > rt.unknown && t > rt.unknown)
            {
                ASBinCode.rtti.Class cls1 = classfinder.getClassByRunTimeDataType(f);
                ASBinCode.rtti.Class cls2 = classfinder.getClassByRunTimeDataType(t);
                //检查继承关系
                
            }

            if (f == rt.unknown || t == rt.unknown)
            {
                return false;
            }

            if (f > rt.unknown || t > rt.unknown)
            {
                //var c1 = classfinder.getClassByRunTimeDataType(f);
                //var c2 = classfinder.getClassByRunTimeDataType(t);

                //if (ReferenceEquals(c1.staticClass, c2)
                //    ||
                //    ReferenceEquals(c2.staticClass,c1)
                //    )
                //{
                //    return false;
                //}

                if (ASBinCode.ClassMemberFinder.check_isinherits(f, t, classfinder)) //检查集成关系
                {
                    return true;
                }


                return false;
#if DEBUG
                throw new NotImplementedException();
#else
                return false;
#endif

            }

            return implicitcoverttable[(int)f, (int)t];
        }

        private static bool[,] implicitcoverttable =
            {
            /*------*/  //bool  int   uint    number  string  var_void  null   function  fun_void   array   unknown
            /*bool   */ { true  ,true  ,true   ,true   ,false   ,true  ,false  ,false    ,false,    false   ,false  }, 
            /*int   */  { true  ,true  ,true   ,true   ,false   ,true  ,true   ,false    ,false,    false   ,false  },  
            /*uint  */  { true  ,true  ,true   ,true   ,false   ,true  ,true   ,false   ,false,     false   ,false  },  
            /*number*/  { true  ,true  ,true   ,true   ,false   ,true  ,true   ,false   ,false,     false   ,false  },
            /*string*/  { true  ,false ,false  ,false  ,true   ,true  ,true   ,false    ,false,     false   ,false  },
            /*var_void*/{ true  ,true  ,true   ,true   ,true   ,true  ,true   ,true     ,false,     true   ,false  },
            /*null*/    { true  ,true  ,true   ,true   ,true   ,true  ,true   ,true     ,false,     true   ,false  },
            /*function*/{ true ,false ,false  ,false  ,false  ,true ,true  ,true     ,false,        false   ,false  },
            /*fun_void*/{ false ,false ,false  ,false  ,false  ,true ,false  ,false     ,false,     false   , false  },
            /*array*/   { true ,false ,false  ,false  ,false  ,true ,true  ,false     ,false,     true   , false  },
            /*unknown*/ { false ,false ,false  ,false  ,false  ,false ,false  ,false    ,false,     false   ,false  }
            };
        
        public static rt getImplicitOpType(ASBinCode.RunTimeDataType v1, ASBinCode.RunTimeDataType v2, 
            ASBinCode.OpCode op , ASBinCode.IClassFinder lib)
        {
            if (v1 > rt.unknown)
            {
                if (ObjectImplicit_ToNumber(v1, lib))
                {
                    v1 = rt.rt_number;
                }
            }
            if (v2 > rt.unknown)
            {
                if (ObjectImplicit_ToNumber(v2, lib))
                {
                    v2 = rt.rt_number;
                }
            }

            if (op == ASBinCode.OpCode.add)
            {
                if (v1 > rt.unknown || v2 > rt.unknown)
                {
                    //**执行动态计算
                    return rt.rt_void;
                }

                return implicit_opadd_coverttable[(int)v1, (int)v2];
            }
            else if (op == ASBinCode.OpCode.sub)
            {
                if (v1 > rt.unknown || v2 > rt.unknown)
                {
                    return rt.unknown;
                }

                return implicit_opsub_coverttable[(int)v1, (int)v2];
            }
            else if (op == ASBinCode.OpCode.multi || op== ASBinCode.OpCode.div || op== ASBinCode.OpCode.mod)
            {
                if (v1 > rt.unknown || v2 > rt.unknown)
                {
                    return rt.unknown;
                }

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
            /*------*/    //bool        int             uint            number          string          var_void        null        function    fun_void    array    unknown
            /*boolean*/ { rt.rt_number  ,rt.rt_number  ,rt.rt_number   ,rt.rt_number   ,rt.rt_string   ,rt.rt_void     ,rt.rt_int   ,rt.unknown ,rt.unknown,rt.unknown,rt.unknown  },
            /*int*/     { rt.rt_number  ,rt.rt_number  ,rt.rt_number   ,rt.rt_number   ,rt.rt_string   ,rt.rt_void     ,rt.rt_int   ,rt.unknown ,rt.unknown,rt.unknown,rt.unknown  },
            /*uint*/    { rt.rt_number  ,rt.rt_number  ,rt.rt_number   ,rt.rt_number   ,rt.rt_string   ,rt.rt_void     ,rt.rt_uint  ,rt.unknown ,rt.unknown,rt.unknown,rt.unknown  },
            /*number*/  { rt.rt_number  ,rt.rt_number  ,rt.rt_number   ,rt.rt_number   ,rt.rt_string   ,rt.rt_void     ,rt.rt_number,rt.unknown ,rt.unknown,rt.unknown,rt.unknown  },
            /*string*/  { rt.rt_string  ,rt.rt_string  ,rt.rt_string   ,rt.rt_string   ,rt.rt_string   ,rt.rt_void     ,rt.rt_string,rt.unknown ,rt.unknown,rt.rt_string,rt.unknown  },
            /*var_void*/{ rt.rt_void    ,rt.rt_void    ,rt.rt_void     ,rt.rt_void     ,rt.rt_void     ,rt.rt_void     ,rt.rt_void  ,rt.unknown ,rt.unknown,rt.unknown,rt.unknown  },
            /*null*/    { rt.rt_boolean ,rt.rt_int     ,rt.rt_uint     ,rt.rt_number   ,rt.rt_string   ,rt.rt_void     ,rt.rt_number ,rt.unknown ,rt.unknown,rt.unknown,rt.unknown  },
            /*function*/{ rt.unknown    ,rt.unknown    ,rt.unknown     ,rt.unknown     ,rt.unknown     ,rt.unknown     ,rt.unknown  ,rt.unknown ,rt.unknown,rt.unknown,rt.unknown  },
            /*fun_void*/{ rt.unknown    ,rt.unknown    ,rt.unknown     ,rt.unknown     ,rt.unknown     ,rt.unknown     ,rt.unknown  ,rt.unknown ,rt.unknown,rt.unknown,rt.unknown  },
            /*array*/   { rt.unknown    ,rt.unknown    ,rt.unknown     ,rt.unknown     ,rt.rt_string   ,rt.unknown     ,rt.unknown  ,rt.unknown ,rt.unknown,rt.unknown,rt.unknown  },
            /*unknown*/ { rt.unknown    ,rt.unknown    ,rt.unknown     ,rt.unknown     ,rt.unknown     ,rt.unknown     ,rt.unknown  ,rt.unknown ,rt.unknown,rt.unknown,rt.unknown  }
            };
        //- 操作隐式类型转换表
        private static rt[,] implicit_opsub_coverttable =
            {
            /*------*/    //bool        int             uint            number          string          var_void       null          function    fun_void   array   unknown
            /*boolean*/ { rt.unknown    ,rt.unknown     ,rt.unknown   ,rt.unknown   ,rt.unknown     ,rt.unknown     ,rt.unknown      ,rt.unknown,rt.unknown,rt.unknown,rt.unknown  },
            /*int*/     { rt.unknown    ,rt.rt_number  ,rt.rt_number   ,rt.rt_number   ,rt.unknown   ,rt.rt_void     ,rt.rt_int      ,rt.unknown,rt.unknown,rt.unknown,rt.unknown  },
            /*uint*/    { rt.unknown    ,rt.rt_number  ,rt.rt_number   ,rt.rt_number   ,rt.unknown   ,rt.rt_void     ,rt.rt_uint     ,rt.unknown,rt.unknown,rt.unknown,rt.unknown  },
            /*number*/  { rt.unknown    ,rt.rt_number  ,rt.rt_number   ,rt.rt_number   ,rt.unknown   ,rt.rt_void     ,rt.rt_number   ,rt.unknown,rt.unknown,rt.unknown,rt.unknown  },
            /*string*/  { rt.unknown    ,rt.unknown  ,rt.unknown   ,rt.unknown   ,rt.unknown     ,rt.unknown     ,rt.unknown        ,rt.unknown ,rt.unknown,rt.unknown,rt.unknown  },
            /*var_void*/{ rt.unknown    ,rt.rt_void    ,rt.rt_void     ,rt.rt_void     ,rt.unknown     ,rt.rt_void     ,rt.rt_void  ,rt.unknown ,rt.unknown,rt.unknown,rt.unknown  },
            /*null*/    { rt.unknown    ,rt.rt_number  ,rt.rt_number   ,rt.rt_number   ,rt.unknown     ,rt.rt_void     ,rt.rt_number,rt.unknown ,rt.unknown,rt.unknown,rt.unknown  },
            /*function*/{ rt.unknown    ,rt.unknown  ,rt.unknown   ,rt.unknown   ,rt.unknown     ,rt.unknown     ,rt.unknown        ,rt.unknown ,rt.unknown,rt.unknown,rt.unknown  },
            /*fun_void*/{ rt.unknown    ,rt.unknown    ,rt.unknown     ,rt.unknown     ,rt.unknown     ,rt.unknown     ,rt.unknown  ,rt.unknown,rt.unknown,rt.unknown,rt.unknown  },
            /*array*/   { rt.unknown    ,rt.unknown    ,rt.unknown     ,rt.unknown     ,rt.unknown     ,rt.unknown     ,rt.unknown  ,rt.unknown,rt.unknown,rt.unknown,rt.unknown  },
            /*unknown*/ { rt.unknown    ,rt.unknown    ,rt.unknown     ,rt.unknown     ,rt.unknown     ,rt.unknown     ,rt.unknown  ,rt.unknown,rt.unknown,rt.unknown,rt.unknown  }
            };


        //* / % 操作隐式类型转换表
        private static rt[,] implicit_opmulti_coverttable =
            {
            /*------*/    //bool        int             uint            number          string          var_void       null          function    fun_void       array   unknown
            /*boolean*/ { rt.unknown    ,rt.unknown     ,rt.unknown   ,rt.unknown   ,rt.unknown     ,rt.unknown     ,rt.unknown      ,rt.unknown    ,rt.unknown,rt.unknown,rt.unknown  },
            /*int*/     { rt.unknown    ,rt.rt_number  ,rt.rt_number   ,rt.rt_number   ,rt.unknown   ,rt.rt_void     ,rt.rt_int      ,rt.unknown    ,rt.unknown,rt.unknown,rt.unknown  },
            /*uint*/    { rt.unknown    ,rt.rt_number  ,rt.rt_number   ,rt.rt_number   ,rt.unknown   ,rt.rt_void     ,rt.rt_uint     ,rt.unknown    ,rt.unknown,rt.unknown,rt.unknown  },
            /*number*/  { rt.unknown    ,rt.rt_number  ,rt.rt_number   ,rt.rt_number   ,rt.unknown   ,rt.rt_void     ,rt.rt_number   ,rt.unknown    ,rt.unknown,rt.unknown,rt.unknown  },
            /*string*/  { rt.unknown    ,rt.unknown  ,rt.unknown   ,rt.unknown   ,rt.unknown     ,rt.unknown     ,rt.unknown        ,rt.unknown     ,rt.unknown,rt.unknown,rt.unknown  },
            /*var_void*/{ rt.unknown    ,rt.rt_void    ,rt.rt_void     ,rt.rt_void     ,rt.unknown     ,rt.rt_void     ,rt.rt_void      ,rt.unknown ,rt.unknown,rt.unknown,rt.unknown  },
            /*null*/    { rt.unknown    ,rt.rt_number  ,rt.rt_number   ,rt.rt_number   ,rt.unknown     ,rt.rt_void     ,rt.rt_number    ,rt.unknown ,rt.unknown,rt.unknown,rt.unknown  },
            /*function*/{ rt.unknown    ,rt.unknown    ,rt.unknown     ,rt.unknown     ,rt.unknown     ,rt.unknown     ,rt.unknown     ,rt.unknown  ,rt.unknown,rt.unknown,rt.unknown  },
            /*fun_void*/{ rt.unknown    ,rt.unknown    ,rt.unknown     ,rt.unknown     ,rt.unknown     ,rt.unknown     ,rt.unknown     ,rt.unknown  ,rt.unknown,rt.unknown,rt.unknown  },
            /*array*/   { rt.unknown    ,rt.unknown    ,rt.unknown     ,rt.unknown     ,rt.unknown     ,rt.unknown     ,rt.unknown     ,rt.unknown  ,rt.unknown,rt.unknown,rt.unknown  },
            /*unknown*/ { rt.unknown    ,rt.unknown    ,rt.unknown     ,rt.unknown     ,rt.unknown     ,rt.unknown     ,rt.unknown     ,rt.unknown  ,rt.unknown,rt.unknown,rt.unknown  }
            };


       
        public static bool ObjectImplicit_ToNumber(rt classtype ,ASBinCode.IClassFinder bin)
        {
            if (classtype < rt.unknown)
            {
                return false;
            }

            var _class = bin.getClassByRunTimeDataType(classtype);

            if (_class.staticClass == null)
            {
                return false;
            }

            if (_class.staticClass.implicit_to != null)
            {
                var to = _class.staticClass.implicit_to_type;
                return (to == rt.rt_int || to == rt.rt_uint || to == rt.rt_number);
            }


            return false;
        }


        public static bool Object_CanImplicit_ToPrimitive(rt classtype, ASBinCode.IClassFinder bin,out rt primitiveType)
        {
            if (classtype < rt.unknown)
            {
                primitiveType = rt.unknown;
                return false;
            }


            var _class = bin.getClassByRunTimeDataType(classtype);

            if (_class.staticClass == null)
            {
                primitiveType = rt.unknown;
                return false;
            }

            if (_class.staticClass.implicit_to != null)
            {
                primitiveType = _class.staticClass.implicit_to_type;
                return true;
            }

            primitiveType = rt.unknown;
            return false;
        }

        /// <summary>
        /// 对于可以隐式转换到基本类型的对象，执行隐式转换
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal static ASBinCode.IRunTimeValue ObjectImplicit_ToPrimitive(ASBinCode.rtData.rtObject obj)
        {
            //return ASBinCode.rtData.rtUndefined.undefined;

            return obj.value.memberData[0].getValue();

        }

    }

    
}
