using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.nativefuncs.linksystem
{
    class system_object_ctor : NativeConstParameterFunction
    {
        public system_object_ctor():base(0)
        {
            para = new List<RunTimeDataType>();
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
                return "_system_Object_ctor";
            }
        }

        List<RunTimeDataType> para;
        public override List<RunTimeDataType> parameters
        {
            get
            {
                return para;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.rt_void;
            }
        }

		//public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
		//{
		//    errormessage = null; errorno = 0;

		//    ((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value = new object(); 

		//    return ASBinCode.rtData.rtUndefined.undefined;

		//}
		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			success = true;

			((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value = new object();

			returnSlot.directSet( ASBinCode.rtData.rtUndefined.undefined);
		}
	}

    class object_static_equals : NativeConstParameterFunction //NativeFunctionBase
    {
       
        public object_static_equals():base(2)
        {
            
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_void);
            para.Add(RunTimeDataType.rt_void);
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
                return "_system_Object_static_equals";
            }
        }

        List<RunTimeDataType> para;
        public override List<RunTimeDataType> parameters
        {
            get
            {
                return para;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.rt_boolean;
            }
        }

		//public override NativeFunctionMode mode
		//{
		//    get
		//    {
		//        return NativeFunctionMode.normal_1;
		//    }
		//}

		//public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
		//{
		//    throw new NotImplementedException();
		//}

		//public override void execute2(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT[] argements, SLOT returnSlot, SourceToken token, object stackframe, out bool success)
		//{
		//    //base.execute2(thisObj, functionDefine, argements, returnSlot, stackframe, out success);
		//    success = true;

		//    ASBinCode.rtData.rtObjectBase obj = argements[0].getValue() as ASBinCode.rtData.rtObjectBase;
		//    if (obj == null)
		//    {
		//        returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
		//        return;
		//    }

		//    LinkSystemObject o1 = obj.value as LinkSystemObject;
		//    if (o1 == null)
		//    {
		//        returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
		//        return;
		//    }


		//    ASBinCode.rtData.rtObjectBase obj2 = argements[1].getValue() as ASBinCode.rtData.rtObjectBase;
		//    if (obj2 == null)
		//    {
		//        returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
		//        return;
		//    }

		//    LinkSystemObject o2 = obj2.value as LinkSystemObject;
		//    if (o2 == null)
		//    {
		//        returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
		//        return;
		//    }

		//    if (System.Object.Equals(o1, o2))
		//    {
		//        returnSlot.setValue(ASBinCode.rtData.rtBoolean.True);
		//        return;
		//    }
		//    else
		//    {
		//        returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
		//        return;
		//    }

		//}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			//base.execute2(thisObj, functionDefine, argements, returnSlot, stackframe, out success);
			success = true;

			ASBinCode.rtData.rtObjectBase obj = argements[0] as ASBinCode.rtData.rtObjectBase;
			if (obj == null)
			{
				returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
				return;
			}

			LinkSystemObject o1 = obj.value as LinkSystemObject;
			if (o1 == null)
			{
				returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
				return;
			}


			ASBinCode.rtData.rtObjectBase obj2 = argements[1] as ASBinCode.rtData.rtObjectBase;
			if (obj2 == null)
			{
				returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
				return;
			}

			LinkSystemObject o2 = obj2.value as LinkSystemObject;
			if (o2 == null)
			{
				returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
				return;
			}

			if (System.Object.Equals(o1, o2))
			{
				returnSlot.setValue(ASBinCode.rtData.rtBoolean.True);
				return;
			}
			else
			{
				returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
				return;
			}
		}

	}

    class object_static_referenceEquals : NativeConstParameterFunction //NativeFunctionBase
    {

        public object_static_referenceEquals():base(2)
        {

            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_void);
            para.Add(RunTimeDataType.rt_void);
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
                return "_system_Object_static_referenceEquals";
            }
        }

        List<RunTimeDataType> para;
        public override List<RunTimeDataType> parameters
        {
            get
            {
                return para;
            }
        }

        public override RunTimeDataType returnType
        {
            get
            {
                return RunTimeDataType.rt_boolean;
            }
        }

		//public override NativeFunctionMode mode
		//{
		//    get
		//    {
		//        return NativeFunctionMode.normal_1;
		//    }
		//}

		//public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
		//{
		//    throw new NotImplementedException();
		//}

		//public override void execute2(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT[] argements, SLOT returnSlot, SourceToken token, object stackframe, out bool success)
		//{
		//    //base.execute2(thisObj, functionDefine, argements, returnSlot, stackframe, out success);
		//    success = true;

		//    var a1 = argements[0].getValue();
		//    var a2 = argements[1].getValue();

		//    if (a1.rtType == a2.rtType && a1.rtType == RunTimeDataType.rt_null)
		//    {
		//        returnSlot.setValue(ASBinCode.rtData.rtBoolean.True);
		//        return;
		//    }


		//    ASBinCode.rtData.rtObjectBase obj = a1 as ASBinCode.rtData.rtObjectBase;
		//    if (obj == null)
		//    {
		//        returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
		//        return;
		//    }

		//    LinkSystemObject o1 = obj.value as LinkSystemObject;
		//    if (o1 == null)
		//    {
		//        returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
		//        return;
		//    }


		//    ASBinCode.rtData.rtObjectBase obj2 = a2 as ASBinCode.rtData.rtObjectBase;
		//    if (obj2 == null)
		//    {
		//        returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
		//        return;
		//    }

		//    LinkSystemObject o2 = obj2.value as LinkSystemObject;
		//    if (o2 == null)
		//    {
		//        returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
		//        return;
		//    }

		//    if (object.ReferenceEquals(o1.GetLinkData(), o2.GetLinkData()))
		//    {
		//        returnSlot.setValue(ASBinCode.rtData.rtBoolean.True);
		//        return;
		//    }
		//    else
		//    {
		//        returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
		//        return;
		//    }

		//}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			//base.execute2(thisObj, functionDefine, argements, returnSlot, stackframe, out success);
			success = true;

			var a1 = argements[0];
			var a2 = argements[1];

			if (a1.rtType == a2.rtType && a1.rtType == RunTimeDataType.rt_null)
			{
				returnSlot.setValue(ASBinCode.rtData.rtBoolean.True);
				return;
			}


			ASBinCode.rtData.rtObjectBase obj = a1 as ASBinCode.rtData.rtObjectBase;
			if (obj == null)
			{
				returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
				return;
			}

			LinkSystemObject o1 = obj.value as LinkSystemObject;
			if (o1 == null)
			{
				returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
				return;
			}


			ASBinCode.rtData.rtObjectBase obj2 = a2 as ASBinCode.rtData.rtObjectBase;
			if (obj2 == null)
			{
				returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
				return;
			}

			LinkSystemObject o2 = obj2.value as LinkSystemObject;
			if (o2 == null)
			{
				returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
				return;
			}

			if (object.ReferenceEquals(o1.GetLinkData(), o2.GetLinkData()))
			{
				returnSlot.setValue(ASBinCode.rtData.rtBoolean.True);
				return;
			}
			else
			{
				returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
				return;
			}
		}

	}


	class system_Object_explicit_from : NativeConstParameterFunction
	{
		public system_Object_explicit_from() : base(1)
		{
			para = new List<RunTimeDataType>();
			para.Add(RunTimeDataType.rt_void);
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
				return "_system_Object_explicit_from_";
			}
		}

		List<RunTimeDataType> para;
		public override List<RunTimeDataType> parameters
		{
			get
			{
				return para;
			}
		}

		public override RunTimeDataType returnType
		{
			get
			{
				return RunTimeDataType.rt_void;
			}
		}

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			//var v = (stackframe.player.alloc_pureHostedOrLinkedObject(((ASBinCode.rtData.rtObjectBase)thisObj).value._class.instanceClass));
			var cls = stackframe.player.swc.getClassByRunTimeDataType(functionDefine.signature.returnType);


			if (argements[0].rtType > RunTimeDataType.unknown)
			{
				var vcls = stackframe.player.swc.getClassByRunTimeDataType(argements[0].rtType);
				if (vcls.staticClass == null)
				{
					((StackSlot)returnSlot).setLinkObjectValue(cls, stackframe.player, argements[0]);
					success = false;
					return;
				}
			}


			object lo;
			if (stackframe.player.linktypemapper.rtValueToLinkObject(
				argements[0],

				stackframe.player.linktypemapper.getLinkType(cls.getRtType()),

				bin, true, out lo
				))
			{

				((StackSlot)returnSlot).setLinkObjectValue(cls, stackframe.player, lo);

				success = true;
			}
			else
			{
				((StackSlot)returnSlot).setLinkObjectValue(cls, stackframe.player, argements[0]);
				success = false;

				//stackframe.throwCastException(token, argements[0].rtType,
				//	cls.getRtType()
				//	);
				//success = false;
			}

		}


	}

}
