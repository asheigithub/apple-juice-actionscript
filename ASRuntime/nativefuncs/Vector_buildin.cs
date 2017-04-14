using ASBinCode;
using ASBinCode.rtData;
using ASBinCode.rtti;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.nativefuncs
{
    class Vector_Util
    {
        public static RunTimeDataType getVectorType(IRunTimeValue thisObj, IClassFinder bin)
        {
            ASBinCode.rtti.UnmanagedObject rtObj = (ASBinCode.rtti.UnmanagedObject)((rtObject)thisObj).value;

            var t = rtObj.memberData[0].getValue();

            RunTimeDataType vector_type = t.rtType;

            if (t.rtType > RunTimeDataType.unknown)
            {
                var cls = bin.getClassByRunTimeDataType(t.rtType);
                vector_type = cls.instanceClass.getRtType();

                RunTimeDataType ot;
                if (TypeConverter.Object_CanImplicit_ToPrimitive(vector_type, bin, out ot))
                {
                    vector_type = ot;
                }
            }

            return vector_type;
        }
    }


    class Vector_constructor : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Vector_constructor()
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_uint);
            _paras.Add(RunTimeDataType.rt_boolean);
        }

        public override bool isMethod
        {
            get
            {
                return true;
            }
        }

        public override string name
        {
            get
            {
                return "_vector_constructor";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.rt_void;
            }
        }

        public override IRunTimeValue execute(IRunTimeValue thisObj, ISLOT[] argements, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;


            uint length = ((rtUInt)argements[0].getValue()).value;
            bool isfixed = ( (rtBoolean)argements[1].getValue()).value;

            //throw new NotImplementedException();

            ASBinCode.rtti.UnmanagedObject rtObj = (ASBinCode.rtti.UnmanagedObject)((rtObject)thisObj).value;

            RunTimeDataType vector_type = Vector_Util.getVectorType(thisObj, bin);


            Vector_Data data = new Vector_Data(vector_type);
            data.isFixed = isfixed;
            data.innnerList = new List<IRunTimeValue>();

            while (data.innnerList.Count < length)
            {
                data.innnerList.Add( TypeConverter.getDefaultValue( vector_type ).getValue(null));
            }

            rtObj.unmanaged_object = data;

            return ASBinCode.rtData.rtUndefined.undefined;
        }
    }



    class Vector_getIsFixed : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Vector_getIsFixed()
        {
            _paras = new List<RunTimeDataType>();
            
        }

        public override bool isMethod
        {
            get
            {
                return true;
            }
        }

        public override string name
        {
            get
            {
                return "_vector_getfixed";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.rt_boolean;
            }
        }

        public override IRunTimeValue execute(IRunTimeValue thisObj, ISLOT[] argements, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;


           
            ASBinCode.rtti.UnmanagedObject rtObj = (ASBinCode.rtti.UnmanagedObject)((rtObject)thisObj).value;


            if (((Vector_Data)rtObj.unmanaged_object).isFixed)
            {
                return rtBoolean.True;
            }
            else
            {
                return rtBoolean.False;
            }
           
        }
    }

    class Vector_setIsFixed : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Vector_setIsFixed()
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_boolean);

        }

        public override bool isMethod
        {
            get
            {
                return true;
            }
        }

        public override string name
        {
            get
            {
                return "_vector_setfixed";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.fun_void;
            }
        }

        public override IRunTimeValue execute(IRunTimeValue thisObj, ISLOT[] argements, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;


            ASBinCode.rtti.UnmanagedObject rtObj = (ASBinCode.rtti.UnmanagedObject)((rtObject)thisObj).value;

            ((Vector_Data)rtObj.unmanaged_object).isFixed = ((rtBoolean)argements[0].getValue()).value;

            return rtUndefined.undefined;
        }
    }



    class Vector_getLength : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Vector_getLength()
        {
            _paras = new List<RunTimeDataType>();
        }

        public override bool isMethod
        {
            get
            {
                return true;
            }
        }

        public override string name
        {
            get
            {
                return "_vector_getlength";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.rt_uint;
            }
        }

        public override IRunTimeValue execute(IRunTimeValue thisObj, ISLOT[] argements, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            return
                new rtUInt(
                    (uint)((Vector_Data)
                    ((UnmanagedObject)(((rtObject)thisObj).value)).unmanaged_object).innnerList.Count
                )
                ;
        }
    }

    class Vector_setLength : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Vector_setLength()
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_uint);
        }

        public override bool isMethod
        {
            get
            {
                return true;
            }
        }

        public override string name
        {
            get
            {
                return "_vector_setlength";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.fun_void;
            }
        }

        public override IRunTimeValue execute(IRunTimeValue thisObj, ISLOT[] argements, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            var vd = ((Vector_Data)((UnmanagedObject)((rtObject)thisObj).value).unmanaged_object);
            if (vd.isFixed)
            {
                errormessage = "Cannot change the length of a fixed Vector";
                errorno = 1126;

                return rtUndefined.undefined;
            }



            var list = vd.innnerList;

            

            uint newlen = ((rtUInt)argements[0].getValue()).value;

            if (newlen > list.Count)
            {
                var t = Vector_Util.getVectorType(thisObj, bin);

                while (list.Count < newlen)
                {
                    list.Add( TypeConverter.getDefaultValue( t ).getValue(null) );
                }
            }
            else if (newlen < list.Count)
            {
                list.RemoveRange((int)newlen, list.Count - (int)newlen);
            }


            return rtUndefined.undefined;

        }
    }




    class Vector_toString : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Vector_toString()
        {
            _paras = new List<RunTimeDataType>();
        }

        public override bool isMethod
        {
            get
            {
                return true;
            }
        }

        public override string name
        {
            get
            {
                return "_vector_toString";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.rt_string;
            }
        }



        public override IRunTimeValue execute(IRunTimeValue thisObj, ISLOT[] argements, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            var vd = ((Vector_Data)((UnmanagedObject)((rtObject)thisObj).value).unmanaged_object);
            
            var list = vd.innnerList;

            StringBuilder asb = new StringBuilder();

            for (int i = 0; i < list.Count; i++)
            {
                asb.Append(
                    list[i].rtType != RunTimeDataType.rt_void ?
                    (list[i].rtType == RunTimeDataType.rt_string ?
                    ((rtString)list[i]).valueString() :
                    (list[i].rtType == RunTimeDataType.rt_null ? String.Empty : list[i].ToString())) : String.Empty);
                asb.Append(",");
            }
            if (asb.Length > 0)
            {
                asb.Remove(asb.Length - 1, 1);
            }
            return new rtString(asb.ToString());
        }
    }


    class Vector__concat : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Vector__concat()
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_void);
            _paras.Add(RunTimeDataType.rt_void);
        }

        public override bool isMethod
        {
            get
            {
                return true;
            }
        }

        public override string name
        {
            get
            {
                return "_vector__concat";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.fun_void;
            }
        }



        public override IRunTimeValue execute(IRunTimeValue thisObj, ISLOT[] argements, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            var vd = ((Vector_Data)((UnmanagedObject)((rtObject)argements[0].getValue()).value).unmanaged_object);

            if (argements[1].getValue().rtType == RunTimeDataType.rt_null)
            {
                errormessage = "Cannot access a property or method of a null object reference.";
                errorno = 1009;
                return rtUndefined.undefined;
            }

            var vs = ((Vector_Data)((UnmanagedObject)((rtObject)argements[1].getValue()).value).unmanaged_object);

            vd.innnerList.AddRange(vs.innnerList);


            return rtUndefined.undefined;
        }
    }



    class Vector_insertAt : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Vector_insertAt()
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_int);
            _paras.Add(RunTimeDataType.rt_void);
        }

        public override bool isMethod
        {
            get
            {
                return true;
            }
        }

        public override string name
        {
            get
            {
                return "_vector_insertat";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.fun_void;
            }
        }

        public override IRunTimeValue execute(IRunTimeValue thisObj, ISLOT[] argements, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            var vd = ((Vector_Data)((UnmanagedObject)((rtObject)thisObj).value).unmanaged_object);
            if (vd.isFixed)
            {
                errorno = 1126;
                errormessage = "Cannot change the length of a fixed Vector.";
                return rtUndefined.undefined;
            }

            var arr = vd.innnerList;

            int idx = ((rtInt)argements[0].getValue()).value;
            var toinsert = argements[1].getValue();
            if (idx < 0)
            {
                idx = arr.Count + idx;
                if (idx < 0) { idx = 0; }
            }
            else if (idx > arr.Count)
            {
                idx = arr.Count;
            }

            arr.Insert(idx, toinsert);


            return rtUndefined.undefined;

        }
    }

    class Vector_join : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Vector_join()
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_string);
        }

        public override bool isMethod
        {
            get
            {
                return true;
            }
        }

        public override string name
        {
            get
            {
                return "_vector_join";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.rt_string;
            }
        }

        public override bool isAsync
        {
            get
            {
                return true;
            }
        }

        public override IRunTimeValue execute(IRunTimeValue thisObj, ISLOT[] argements, out string errormessage, out int errorno)
        {
            throw new InvalidOperationException();
        }

        public override void executeAsync(IRunTimeValue thisObj, ISLOT[] argements,
            ISLOT resultSlot, object callbacker, object stackframe, SourceToken token, IRunTimeScope scope)
        {
            IBlockCallBack cb = (IBlockCallBack)callbacker;
            StackFrame frame = (StackFrame)stackframe;

            var arr = ((Vector_Data)((UnmanagedObject)((rtObject)thisObj).value).unmanaged_object).innnerList;

            if (arr.Count == 0)
            {
                cb.call(cb.args);
                resultSlot.directSet(new rtString(string.Empty));
                return;
            }


            BlockCallBackBase sepcb = new BlockCallBackBase();
            sepcb.scope = scope;
            sepcb._intArg = 0;

            object[] sendargs = new object[8];
            sendargs[0] = cb;
            sendargs[1] = arr;
            sendargs[2] = frame;
            sendargs[3] = token;
            sendargs[4] = scope;
            sendargs[5] = argements[0].getValue();
            sendargs[6] = new StringBuilder();
            sendargs[7] = resultSlot;

            sepcb.args = sendargs;
            sepcb.setCallBacker(_SeptoString_CB);

            operators.OpCast.CastValue(argements[0].getValue(), RunTimeDataType.rt_string,
                frame, token, scope, frame._tempSlot1, sepcb, false);


        }

        private void _SeptoString_CB(BlockCallBackBase sender, object args)
        {
            object[] receiveArgs = (object[])sender.args;
            StackFrame frame = (StackFrame)receiveArgs[2];
            IRunTimeValue aSep = (IRunTimeValue)receiveArgs[5];
            if (aSep.rtType != RunTimeDataType.rt_null)
            {
                string sep = TypeConverter.ConvertToString(frame._tempSlot1.getValue(), frame, (SourceToken)receiveArgs[3]);
                receiveArgs[5] = sep;
            }
            else
            {
                receiveArgs[5] = ",";
            }

            List<IRunTimeValue> array = (List<IRunTimeValue>)receiveArgs[1];

            BlockCallBackBase valueCB = new BlockCallBackBase();
            valueCB._intArg = sender._intArg + 1;
            valueCB.args = receiveArgs;
            valueCB.setCallBacker(_ValueToString_CB);

            operators.OpCast.CastValue(array[sender._intArg], RunTimeDataType.rt_string,
                frame,
                (SourceToken)receiveArgs[3],
                (IRunTimeScope)receiveArgs[4],
                frame._tempSlot1,
                valueCB, false
                );

        }

        private void _ValueToString_CB(BlockCallBackBase sender, object args)
        {
            object[] receiveArgs = (object[])sender.args;
            StackFrame frame = (StackFrame)receiveArgs[2];
            List<IRunTimeValue> array = (List<IRunTimeValue>)receiveArgs[1];

            StringBuilder sb = (StringBuilder)receiveArgs[6];
            SourceToken token = (SourceToken)receiveArgs[3];

            string aSep = (string)receiveArgs[5];
            string toappend = TypeConverter.ConvertToString(frame._tempSlot1.getValue(), frame, token);
            sb.Append(toappend== null?"null":toappend);
            if (sender._intArg < array.Count)
            {
                sb.Append(aSep);


                BlockCallBackBase valueCB = new BlockCallBackBase();
                valueCB._intArg = sender._intArg + 1;
                valueCB.args = receiveArgs;
                valueCB.setCallBacker(_ValueToString_CB);

                operators.OpCast.CastValue(array[sender._intArg], RunTimeDataType.rt_string,
                    frame,
                    (SourceToken)receiveArgs[3],
                    (IRunTimeScope)receiveArgs[4],
                    frame._tempSlot1,
                    valueCB, false
                    );

            }
            else
            {
                ISLOT result = (ISLOT)receiveArgs[7];

                result.directSet(new rtString(sb.ToString()));

                IBlockCallBack cb = (IBlockCallBack)receiveArgs[0];
                cb.call(cb.args);
            }

        }
    }

    class Vector_pop : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Vector_pop()
        {
            _paras = new List<RunTimeDataType>();
        }

        public override bool isMethod
        {
            get
            {
                return true;
            }
        }

        public override string name
        {
            get
            {
                return "_vector_pop";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.rt_void;
            }
        }

        public override IRunTimeValue execute(IRunTimeValue thisObj, ISLOT[] argements, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            var vd = ((Vector_Data)((UnmanagedObject)((rtObject)thisObj).value).unmanaged_object);
            if (vd.isFixed)
            {
                errorno = 1126;
                errormessage = "Cannot change the length of a fixed Vector.";
                return rtUndefined.undefined;
            }

            var arr = vd.innnerList;

            if (arr.Count > 0)
            {
                var result = arr[arr.Count - 1];
                arr.RemoveAt(arr.Count - 1);
                return result;
            }
            else
            {
                return rtUndefined.undefined;
            }

        }
    }

    class Vector_push : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Vector_push()
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_void);
        }

        public override bool isMethod
        {
            get
            {
                return true;
            }
        }

        public override string name
        {
            get
            {
                return "_vector_push";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.rt_uint;
            }
        }

        public override IRunTimeValue execute(IRunTimeValue thisObj, ISLOT[] argements, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            var vd = ((Vector_Data)((UnmanagedObject)((rtObject)thisObj).value).unmanaged_object);
            if (vd.isFixed)
            {
                errorno = 1126;
                errormessage = "Cannot change the length of a fixed Vector.";
                return rtUndefined.undefined;
            }

            var arr = vd.innnerList;

            arr.Add(argements[0].getValue());

            return new rtUInt((uint)arr.Count);
        }
    }


    class Vector_removeAt : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Vector_removeAt()
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_int);
        }

        public override bool isMethod
        {
            get
            {
                return true;
            }
        }

        public override string name
        {
            get
            {
                return "_vector_removeAt";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.rt_void;
            }
        }

        public override IRunTimeValue execute(IRunTimeValue thisObj, ISLOT[] argements, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            var vd = ((Vector_Data)((UnmanagedObject)((rtObject)thisObj).value).unmanaged_object);
            if (vd.isFixed)
            {
                errorno = 1126;
                errormessage = "Cannot change the length of a fixed Vector.";
                return rtUndefined.undefined;
            }

            var arr = vd.innnerList;

            int idx = ((rtInt)argements[0].getValue()).value;
            if (idx < 0)
            {
                idx = arr.Count + idx;
                if (idx < 0) { idx = 0; }
            }
            else if (idx > arr.Count - 1)
            {
                errorno = 1125;
                errormessage = "The index "+idx+" is out of range "+arr.Count+".";

                return rtUndefined.undefined;
            }

            var r = arr[idx];
            arr.RemoveAt(idx);
            return r;

        }
    }


    class Vector_reverse : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Vector_reverse()
        {
            _paras = new List<RunTimeDataType>();
        }

        public override bool isMethod
        {
            get
            {
                return true;
            }
        }

        public override string name
        {
            get
            {
                return "_vector_reverse";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.rt_void;
            }
        }

        public override IRunTimeValue execute(IRunTimeValue thisObj, ISLOT[] argements, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            var vd = ((Vector_Data)((UnmanagedObject)((rtObject)thisObj).value).unmanaged_object);
            

            var arr = vd.innnerList;

            arr.Reverse();

            return thisObj;
        }
    }



    class Vector_shift : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Vector_shift()
        {
            _paras = new List<RunTimeDataType>();
        }

        public override bool isMethod
        {
            get
            {
                return true;
            }
        }

        public override string name
        {
            get
            {
                return "_vector_shift";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.rt_void;
            }
        }

        public override IRunTimeValue execute(IRunTimeValue thisObj, ISLOT[] argements, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            var vd = ((Vector_Data)((UnmanagedObject)((rtObject)thisObj).value).unmanaged_object);
            if (vd.isFixed)
            {
                errorno = 1126;
                errormessage = "Cannot change the length of a fixed Vector.";
                return rtUndefined.undefined;
            }

            var arr = vd.innnerList;

            if (arr.Count > 0)
            {
                var result = arr[0];
                arr.RemoveAt(0);
                return result;
            }
            else
            {
                return rtUndefined.undefined;
            }

        }
    }


    class Vector_slice : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Vector_slice()
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_int);
            _paras.Add(RunTimeDataType.rt_int);
            _paras.Add(RunTimeDataType.rt_void);
        }

        public override bool isMethod
        {
            get
            {
                return true;
            }
        }

        public override string name
        {
            get
            {
                return "_vector_slice";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.rt_void;
            }
        }

        public override IRunTimeValue execute(IRunTimeValue thisObj, ISLOT[] argements, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            var vd = ((Vector_Data)((UnmanagedObject)((rtObject)thisObj).value).unmanaged_object);

            var source = ((Vector_Data)((UnmanagedObject)((rtObject)argements[2].getValue()).value).unmanaged_object).innnerList;

            var arr = source;

            int startindex = ((rtInt)argements[0].getValue()).value;
            int endindex = ((rtInt)argements[1].getValue()).value;

            if (startindex >= arr.Count)
            {
                return new rtArray();
            }
            else if (startindex < 0)
            {
                startindex = arr.Count + startindex;
                if (startindex < 0)
                {
                    startindex = 0;
                }
            }

            if (endindex > arr.Count)
            {
                endindex = arr.Count;
            }
            else if (endindex < 0)
            {
                endindex = arr.Count + endindex;
                if (endindex < 0)
                {
                    endindex = 0;
                }
            }

            for (int i = startindex; i < endindex; i++)
            {
                vd.innnerList.Add(arr[i]);
            }

            return thisObj;

        }
    }

    class Vector_splice : NativeFunctionBase
    {
        private List<RunTimeDataType> _paras;

        public Vector_splice()
        {
            _paras = new List<RunTimeDataType>();
            _paras.Add(RunTimeDataType.rt_int);
            _paras.Add(RunTimeDataType.rt_uint);
            _paras.Add(RunTimeDataType.rt_void);
        }

        public override bool isMethod
        {
            get
            {
                return true;
            }
        }

        public override string name
        {
            get
            {
                return "_vector_splice";
            }
        }

        public override List<RunTimeDataType> parameters
        {
            get
            {
                return _paras;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.rt_int;
            }
        }

        public override IRunTimeValue execute(IRunTimeValue thisObj, ISLOT[] argements, out string errormessage, out int errorno)
        {
            errormessage = null;
            errorno = 0;

            var vd = ((Vector_Data)((UnmanagedObject)((rtObject)thisObj).value).unmanaged_object);

            var source = ((Vector_Data)((UnmanagedObject)((rtObject)argements[2].getValue()).value).unmanaged_object);
            
            var arr = source.innnerList;

            int startindex = ((rtInt)argements[0].getValue()).value;
            uint deleteCount = ((rtUInt)argements[1].getValue()).value;

            List<IRunTimeValue> insert = null;
            if (argements[2].getValue().rtType == RunTimeDataType.rt_array)
            {
                insert = ((rtArray)argements[2].getValue()).innerArray;
            }

            if (startindex < 0)
            {
                startindex = arr.Count + startindex;
                if (startindex < 0)
                {
                    startindex = 0;
                }
            }

            var newArray = vd.innnerList;

            int st = startindex;
            while (deleteCount > 0 && st < arr.Count)
            {
                if (source.isFixed)
                {
                    errorno = 1126;
                    errormessage = "Cannot change the length of a fixed Vector.";
                    return rtUndefined.undefined;
                }

                newArray.Add(arr[st]);
                st++;
                deleteCount--;
            }

            if (newArray.Count > 0)
            {
                arr.RemoveRange(startindex, newArray.Count);
            }

            return new rtInt(startindex);

        }
    }
}
