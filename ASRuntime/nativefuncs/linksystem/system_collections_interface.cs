using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.nativefuncs.linksystem
{
    class system_collections_interface
    {
        public static void regNativeFunctions(CSWC bin)
        {
			system_collections_ICollection_buildin.regNativeFunctions(bin);
			system_collections_IEnumerable_buildin.regNativeFunctions(bin);
			system_collections_IEnumerator_buildin.regNativeFunctions(bin);


			bin.regNativeFunction(LinkSystem_Buildin.getCreator(
                "system_collections_ilist_creator_", default(System.Collections.IList)));

            bin.regNativeFunction(LinkSystem_Buildin.getCreator(
                "system_collections_idictionary_creator_", default(System.Collections.IDictionary)));

            //bin.regNativeFunction(
            //    LinkSystem_Buildin.getCreator(
            //        "system_collections_idictionaryenumerator_creator_",
            //        default(System.Collections.IDictionaryEnumerator)
            //        )
            //    );

            
            bin.regNativeFunction(new system_collections_ilist_getthisitem());
            bin.regNativeFunction(new system_collections_ilist_setthisitem());
            bin.regNativeFunction(new system_collections_ilist_isfixedsize());
            bin.regNativeFunction(new system_collections_ilist_isreadonly());
            bin.regNativeFunction(new system_collections_ilist_add());
            bin.regNativeFunction(new system_collections_ilist_clear());
            bin.regNativeFunction(new system_collections_ilist_contains());
            bin.regNativeFunction(new system_collections_ilist_indexof());
            bin.regNativeFunction(new system_collections_ilist_insert());
            bin.regNativeFunction(new system_collections_ilist_remove());
            bin.regNativeFunction(new system_collections_ilist_removeAt());

            bin.regNativeFunction(new system_collections_idictionary_isfixedsize());
            bin.regNativeFunction(new system_collections_idictionary_isreadonly());
            bin.regNativeFunction(new system_collections_idictionary_getthisitem());
            bin.regNativeFunction(new system_collections_idictionary_setthisitem());
            bin.regNativeFunction(new system_collections_idictionary_keys());
            bin.regNativeFunction(new system_collections_idictionary_values());
            bin.regNativeFunction(new system_collections_idictionary_add());
            bin.regNativeFunction(new system_collections_idictionary_clear());
            bin.regNativeFunction(new system_collections_idictionary_contains());
            bin.regNativeFunction(new system_collections_idictionary_remove());
            bin.regNativeFunction(new system_collections_idictionary_getienumerator());

			//bin.regNativeFunction(new system_collections_idictonaryenumerator_entry());
			system_collections_IDictionaryEnumerator_buildin.regNativeFunctions(bin);
        }
    }


	class system_collections_ICollection_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_collections_ICollection_creator", default(System.Collections.ICollection)));
			bin.regNativeFunction(new system_collections_ICollection_copyTo());
			bin.regNativeFunction(new system_collections_ICollection_get_Count());
			bin.regNativeFunction(new system_collections_ICollection_get_SyncRoot());
			bin.regNativeFunction(new system_collections_ICollection_get_IsSynchronized());
		}

		class system_collections_ICollection_copyTo : NativeConstParameterFunction
		{
			public system_collections_ICollection_copyTo() : base(2)
			{
				para = new List<RunTimeDataType>();
				para.Add(RunTimeDataType.rt_void);
				para.Add(RunTimeDataType.rt_int);

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
					return "system_collections_ICollection_copyTo";
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
					return RunTimeDataType.fun_void;
				}
			}

			public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
			{

				System.Collections.ICollection _this =
					(System.Collections.ICollection)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

				try
				{
					System.Array arg0;
					{
						object _temp;
						if (!stackframe.player.linktypemapper.rtValueToLinkObject(
							argements[0],

							stackframe.player.linktypemapper.getLinkType(argements[0].rtType)
							,
							bin, true, out _temp
							))
						{
							stackframe.throwCastException(token, argements[0].rtType,

								functionDefine.signature.parameters[0].type
								);
							success = false;
							return;
						}
						arg0 = (System.Array)_temp;
					}
					int arg1 = TypeConverter.ConvertToInt(argements[1]);

					_this.CopyTo((System.Array)arg0, (System.Int32)arg1)
					;
					returnSlot.directSet(ASBinCode.rtData.rtUndefined.undefined);
					success = true;
				}
				catch (RuntimeLinkTypeMapper.TypeLinkClassException tlc)
				{
					success = false;
					stackframe.throwAneException(token, tlc.Message);
				}
				catch (InvalidCastException ic)
				{
					success = false;
					stackframe.throwAneException(token, ic.Message);
				}
				catch (ArgumentException a)
				{
					success = false;
					stackframe.throwAneException(token, a.Message);
				}
				catch (IndexOutOfRangeException i)
				{
					success = false;
					stackframe.throwAneException(token, i.Message);
				}
				catch (NotSupportedException n)
				{
					success = false;
					stackframe.throwAneException(token, n.Message);
				}

			}
		}

		class system_collections_ICollection_get_Count : NativeConstParameterFunction
		{
			public system_collections_ICollection_get_Count() : base(0)
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
					return "system_collections_ICollection_get_Count";
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
					return RunTimeDataType.rt_int;
				}
			}

			public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
			{

				System.Collections.ICollection _this =
					(System.Collections.ICollection)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

				try
				{

					int _result_ = (int)(_this.Count
					)
					;
					returnSlot.setValue(_result_);

					success = true;
				}
				catch (RuntimeLinkTypeMapper.TypeLinkClassException tlc)
				{
					success = false;
					stackframe.throwAneException(token, tlc.Message);
				}
				catch (InvalidCastException ic)
				{
					success = false;
					stackframe.throwAneException(token, ic.Message);
				}
				catch (ArgumentException a)
				{
					success = false;
					stackframe.throwAneException(token, a.Message);
				}
				catch (IndexOutOfRangeException i)
				{
					success = false;
					stackframe.throwAneException(token, i.Message);
				}
				catch (NotSupportedException n)
				{
					success = false;
					stackframe.throwAneException(token, n.Message);
				}

			}
		}

		class system_collections_ICollection_get_SyncRoot : NativeConstParameterFunction
		{
			public system_collections_ICollection_get_SyncRoot() : base(0)
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
					return "system_collections_ICollection_get_SyncRoot";
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

				System.Collections.ICollection _this =
					(System.Collections.ICollection)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

				try
				{

					object _result_ = _this.SyncRoot
					;
					stackframe.player.linktypemapper.storeLinkObject_ToSlot(_result_, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);
					success = true;
				}
				catch (RuntimeLinkTypeMapper.TypeLinkClassException tlc)
				{
					success = false;
					stackframe.throwAneException(token, tlc.Message);
				}
				catch (InvalidCastException ic)
				{
					success = false;
					stackframe.throwAneException(token, ic.Message);
				}
				catch (ArgumentException a)
				{
					success = false;
					stackframe.throwAneException(token, a.Message);
				}
				catch (IndexOutOfRangeException i)
				{
					success = false;
					stackframe.throwAneException(token, i.Message);
				}
				catch (NotSupportedException n)
				{
					success = false;
					stackframe.throwAneException(token, n.Message);
				}

			}
		}

		class system_collections_ICollection_get_IsSynchronized : NativeConstParameterFunction
		{
			public system_collections_ICollection_get_IsSynchronized() : base(0)
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
					return "system_collections_ICollection_get_IsSynchronized";
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

			public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
			{

				System.Collections.ICollection _this =
					(System.Collections.ICollection)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

				try
				{

					bool _result_ = _this.IsSynchronized
					;
					if (_result_)
					{
						returnSlot.setValue(ASBinCode.rtData.rtBoolean.True);
					}
					else
					{
						returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
					}

					success = true;
				}
				catch (RuntimeLinkTypeMapper.TypeLinkClassException tlc)
				{
					success = false;
					stackframe.throwAneException(token, tlc.Message);
				}
				catch (InvalidCastException ic)
				{
					success = false;
					stackframe.throwAneException(token, ic.Message);
				}
				catch (ArgumentException a)
				{
					success = false;
					stackframe.throwAneException(token, a.Message);
				}
				catch (IndexOutOfRangeException i)
				{
					success = false;
					stackframe.throwAneException(token, i.Message);
				}
				catch (NotSupportedException n)
				{
					success = false;
					stackframe.throwAneException(token, n.Message);
				}

			}
		}

	}

	class system_collections_IEnumerable_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_collections_IEnumerable_creator", default(System.Collections.IEnumerable)));
			bin.regNativeFunction(new system_collections_IEnumerable_getEnumerator());
		}

		class system_collections_IEnumerable_getEnumerator : NativeConstParameterFunction
		{
			public system_collections_IEnumerable_getEnumerator() : base(0)
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
					return "system_collections_IEnumerable_getEnumerator";
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

				System.Collections.IEnumerable _this =
					(System.Collections.IEnumerable)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

				try
				{

					object _result_ = _this.GetEnumerator()
					;
					stackframe.player.linktypemapper.storeLinkObject_ToSlot(_result_, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);
					success = true;
				}
				catch (RuntimeLinkTypeMapper.TypeLinkClassException tlc)
				{
					success = false;
					stackframe.throwAneException(token, tlc.Message);
				}
				catch (InvalidCastException ic)
				{
					success = false;
					stackframe.throwAneException(token, ic.Message);
				}
				catch (ArgumentException a)
				{
					success = false;
					stackframe.throwAneException(token, a.Message);
				}
				catch (IndexOutOfRangeException i)
				{
					success = false;
					stackframe.throwAneException(token, i.Message);
				}
				catch (NotSupportedException n)
				{
					success = false;
					stackframe.throwAneException(token, n.Message);
				}

			}
		}

	}

	class system_collections_IEnumerator_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_collections_IEnumerator_creator", default(System.Collections.IEnumerator)));
			bin.regNativeFunction(new system_collections_IEnumerator_moveNext());
			bin.regNativeFunction(new system_collections_IEnumerator_get_Current());
			bin.regNativeFunction(new system_collections_IEnumerator_reset());
		}

		class system_collections_IEnumerator_moveNext : NativeConstParameterFunction
		{
			public system_collections_IEnumerator_moveNext() : base(0)
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
					return "system_collections_IEnumerator_moveNext";
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

			public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
			{

				System.Collections.IEnumerator _this =
					(System.Collections.IEnumerator)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

				try
				{

					bool _result_ = _this.MoveNext()
					;
					if (_result_)
					{
						returnSlot.setValue(ASBinCode.rtData.rtBoolean.True);
					}
					else
					{
						returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
					}

					success = true;
				}
				catch (RuntimeLinkTypeMapper.TypeLinkClassException tlc)
				{
					success = false;
					stackframe.throwAneException(token, tlc.Message);
				}
				catch (InvalidCastException ic)
				{
					success = false;
					stackframe.throwAneException(token, ic.Message);
				}
				catch (ArgumentException a)
				{
					success = false;
					stackframe.throwAneException(token, a.Message);
				}
				catch (IndexOutOfRangeException i)
				{
					success = false;
					stackframe.throwAneException(token, i.Message);
				}
				catch (NotSupportedException n)
				{
					success = false;
					stackframe.throwAneException(token, n.Message);
				}

			}
		}

		class system_collections_IEnumerator_get_Current : NativeConstParameterFunction
		{
			public system_collections_IEnumerator_get_Current() : base(0)
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
					return "system_collections_IEnumerator_get_Current";
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

				System.Collections.IEnumerator _this =
					(System.Collections.IEnumerator)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

				try
				{

					object _result_ = _this.Current
					;
					stackframe.player.linktypemapper.storeLinkObject_ToSlot(_result_, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);
					success = true;
				}
				catch (RuntimeLinkTypeMapper.TypeLinkClassException tlc)
				{
					success = false;
					stackframe.throwAneException(token, tlc.Message);
				}
				catch (InvalidCastException ic)
				{
					success = false;
					stackframe.throwAneException(token, ic.Message);
				}
				catch (ArgumentException a)
				{
					success = false;
					stackframe.throwAneException(token, a.Message);
				}
				catch (IndexOutOfRangeException i)
				{
					success = false;
					stackframe.throwAneException(token, i.Message);
				}
				catch (NotSupportedException n)
				{
					success = false;
					stackframe.throwAneException(token, n.Message);
				}

			}
		}

		class system_collections_IEnumerator_reset : NativeConstParameterFunction
		{
			public system_collections_IEnumerator_reset() : base(0)
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
					return "system_collections_IEnumerator_reset";
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
					return RunTimeDataType.fun_void;
				}
			}

			public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
			{

				System.Collections.IEnumerator _this =
					(System.Collections.IEnumerator)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

				try
				{

					_this.Reset()
					;
					returnSlot.directSet(ASBinCode.rtData.rtUndefined.undefined);
					success = true;
				}
				catch (RuntimeLinkTypeMapper.TypeLinkClassException tlc)
				{
					success = false;
					stackframe.throwAneException(token, tlc.Message);
				}
				catch (InvalidCastException ic)
				{
					success = false;
					stackframe.throwAneException(token, ic.Message);
				}
				catch (ArgumentException a)
				{
					success = false;
					stackframe.throwAneException(token, a.Message);
				}
				catch (IndexOutOfRangeException i)
				{
					success = false;
					stackframe.throwAneException(token, i.Message);
				}
				catch (NotSupportedException n)
				{
					success = false;
					stackframe.throwAneException(token, n.Message);
				}

			}
		}

	}


	class system_collections_ilist_getthisitem : NativeConstParameterFunction
    {
        public system_collections_ilist_getthisitem() : base(1)
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_int);
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
                return "system_collections_IList_getThisItem";
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


            int index = TypeConverter.ConvertToInt(argements[0]);

            System.Collections.IList ilist =
                (System.Collections.IList)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

            try
            {
                object obj = ilist[index];
                stackframe.player.linktypemapper.storeLinkObject_ToSlot(obj, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);
                //returnSlot.setValue((int)array.GetValue(index));
                success = true;
            }
			catch (RuntimeLinkTypeMapper.TypeLinkClassException tlc)
			{
				success = false;
				stackframe.throwAneException(token, tlc.Message);
			}
			catch (KeyNotFoundException)
            {
                success = false;
                stackframe.throwAneException(token, ilist.ToString() + "没有链接到脚本");
            }
            catch (ArgumentException a)
            {
                success = false;
                stackframe.throwAneException(token, a.Message);
            }
            catch (IndexOutOfRangeException i)
            {
                success = false;
                stackframe.throwAneException(token, i.Message);
            }


        }


    }

    class system_collections_ilist_setthisitem : NativeConstParameterFunction
    {
        public system_collections_ilist_setthisitem() : base(2)
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_void);
            para.Add(RunTimeDataType.rt_int);
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
                return "system_collections_IList_setThisItem";
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
                return RunTimeDataType.fun_void;
            }
        }

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {
            int index = TypeConverter.ConvertToInt(argements[1]);

            System.Collections.IList ilist =
                (System.Collections.IList)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

            try
            {

                object lo;
                if (stackframe.player.linktypemapper.rtValueToLinkObject(
                    argements[0], 
                    
                    (ilist is Array) ? ilist.GetType().GetElementType(): 
                    stackframe.player.linktypemapper.getLinkType(argements[0].rtType)
                    , 
                    
                    bin, true, out lo
                    ))
                {
                    ilist[index]=lo;
                    returnSlot.setValue(ASBinCode.rtData.rtUndefined.undefined);
                    success = true;
                }
                else
                {
                    stackframe.throwCastException(token, argements[0].rtType,
                        (ilist is Array)?
                        stackframe.player.linktypemapper.getRuntimeDataType(       
                            ilist.GetType().GetElementType()
                            )
                            :
                            argements[0].rtType
                        );
                    success = false;
                }

            }
            catch (InvalidCastException ic)
            {
                success = false;
                stackframe.throwAneException(token, ic.Message);
            }
            catch (ArgumentException a)
            {
                success = false;
                stackframe.throwAneException(token, a.Message);
            }
            catch (IndexOutOfRangeException i)
            {
                success = false;
                stackframe.throwAneException(token, i.Message);
            }


        }
    }

    class system_collections_ilist_isfixedsize : NativeConstParameterFunction
    {
        public system_collections_ilist_isfixedsize() : base(0)
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
                return "system_collections_IList_get_IsFixedSize";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {

            

            System.Collections.IList ilist =
                (System.Collections.IList)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

            try
            {
                if (ilist.IsFixedSize)
                {
                    returnSlot.setValue(ASBinCode.rtData.rtBoolean.True);
                }
                else
                {
                    returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
                }
                //stackframe.player.linktypemapper.storeLinkObject_ToSlot(obj, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);
                //returnSlot.setValue((int)array.GetValue(index));
                success = true;
            }
            catch (KeyNotFoundException)
            {
                success = false;
                stackframe.throwAneException(token, ilist.ToString() + "没有链接到脚本");
            }
            catch (ArgumentException a)
            {
                success = false;
                stackframe.throwAneException(token, a.Message);
            }
            catch (IndexOutOfRangeException i)
            {
                success = false;
                stackframe.throwAneException(token, i.Message);
            }


        }


    }

    class system_collections_ilist_isreadonly : NativeConstParameterFunction
    {
        public system_collections_ilist_isreadonly() : base(0)
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
                return "system_collections_IList_get_IsReadOnly";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {



            System.Collections.IList ilist =
                (System.Collections.IList)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

            try
            {
                if (ilist.IsReadOnly)
                {
                    returnSlot.setValue(ASBinCode.rtData.rtBoolean.True);
                }
                else
                {
                    returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
                }
                //stackframe.player.linktypemapper.storeLinkObject_ToSlot(obj, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);
                //returnSlot.setValue((int)array.GetValue(index));
                success = true;
            }
            catch (KeyNotFoundException)
            {
                success = false;
                stackframe.throwAneException(token, ilist.ToString() + "没有链接到脚本");
            }
            catch (ArgumentException a)
            {
                success = false;
                stackframe.throwAneException(token, a.Message);
            }
            catch (IndexOutOfRangeException i)
            {
                success = false;
                stackframe.throwAneException(token, i.Message);
            }


        }


    }

    class system_collections_ilist_add : NativeConstParameterFunction
    {
        public system_collections_ilist_add() : base(1)
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
                return "system_collections_IList_add";
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
                return RunTimeDataType.rt_int;
            }
        }

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {
            
            System.Collections.IList ilist =
                (System.Collections.IList)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

            try
            {

                object lo;
                if (stackframe.player.linktypemapper.rtValueToLinkObject(
                    argements[0],
                     (ilist is Array) ? ilist.GetType().GetElementType() :
                    stackframe.player.linktypemapper.getLinkType(argements[0].rtType)
                    ,
                    bin, true, out lo
                    ))
                {

                    returnSlot.setValue(ilist.Add(lo));
                    success = true;
                }
                else
                {
                    stackframe.throwCastException(token, argements[0].rtType,
                        (ilist is Array)?
                        stackframe.player.linktypemapper.getRuntimeDataType(ilist.GetType().GetElementType()):
                        argements[0].rtType
                        );
                    success = false;
                }

            }
            catch (InvalidCastException ic)
            {
                success = false;
                stackframe.throwAneException(token, ic.Message);
            }
            catch (ArgumentException a)
            {
                success = false;
                stackframe.throwAneException(token, a.Message);
            }
            catch (IndexOutOfRangeException i)
            {
                success = false;
                stackframe.throwAneException(token, i.Message);
            }
            catch (NotSupportedException n)
            {
                success = false;
                stackframe.throwAneException(token, n.Message);
            }

        }
    }

    class system_collections_ilist_clear : NativeConstParameterFunction
    {
        public system_collections_ilist_clear() : base(0)
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
                return "system_collections_IList_clear";
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
                return RunTimeDataType.fun_void;
            }
        }

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {

            System.Collections.IList ilist =
                (System.Collections.IList)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

            try
            {
                ilist.Clear();
                success = true;
                returnSlot.directSet(ASBinCode.rtData.rtUndefined.undefined);
            }
            catch (InvalidCastException ic)
            {
                success = false;
                stackframe.throwAneException(token, ic.Message);
            }
            catch (ArgumentException a)
            {
                success = false;
                stackframe.throwAneException(token, a.Message);
            }
            catch (IndexOutOfRangeException i)
            {
                success = false;
                stackframe.throwAneException(token, i.Message);
            }
            catch (NotSupportedException n)
            {
                success = false;
                stackframe.throwAneException(token, n.Message);
            }

        }
    }

    class system_collections_ilist_contains : NativeConstParameterFunction
    {
        public system_collections_ilist_contains() : base(1)
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
                return "system_collections_IList_contains";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {

            System.Collections.IList ilist =
                (System.Collections.IList)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

            try
            {
                object lo;
                if (stackframe.player.linktypemapper.rtValueToLinkObject(
                    argements[0],
                     (ilist is Array) ? ilist.GetType().GetElementType() :
                    stackframe.player.linktypemapper.getLinkType(argements[0].rtType)
                    ,
                    bin, false, out lo
                    ))
                {
                    if (ilist.Contains(lo))
                    {
                        returnSlot.setValue(ASBinCode.rtData.rtBoolean.True);
                    }
                    else
                    {
                        returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
                    }
                    success = true;
                }
                else
                {
                    stackframe.throwCastException(token, argements[0].rtType,
                        (ilist is Array)?
                        stackframe.player.linktypemapper.getRuntimeDataType(ilist.GetType().GetElementType())
                        :
                        functionDefine.signature.parameters[0].type
                        );
                    success = false;
                }
            }
            catch (InvalidCastException ic)
            {
                success = false;
                stackframe.throwAneException(token, ic.Message);
            }
            catch (ArgumentException a)
            {
                success = false;
                stackframe.throwAneException(token, a.Message);
            }
            catch (IndexOutOfRangeException i)
            {
                success = false;
                stackframe.throwAneException(token, i.Message);
            }
            catch (NotSupportedException n)
            {
                success = false;
                stackframe.throwAneException(token, n.Message);
            }

        }
    }

    class system_collections_ilist_indexof : NativeConstParameterFunction
    {
        public system_collections_ilist_indexof() : base(1)
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
                return "system_collections_IList_indexOf";
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
                return RunTimeDataType.rt_int;
            }
        }

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {

            System.Collections.IList ilist =
                (System.Collections.IList)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

            try
            {
                object lo;
                if (stackframe.player.linktypemapper.rtValueToLinkObject(
                    argements[0],
                     (ilist is Array) ? ilist.GetType().GetElementType() :
                    stackframe.player.linktypemapper.getLinkType(argements[0].rtType)
                    ,
                    bin, false, out lo
                    ))
                {
                    returnSlot.setValue(ilist.IndexOf(lo));
                    success = true;
                }
                else
                {
                    stackframe.throwCastException(token, argements[0].rtType,
                        (ilist is Array)?
                        stackframe.player.linktypemapper.getRuntimeDataType(ilist.GetType().GetElementType())
                        :
                        functionDefine.signature.parameters[0].type
                        );
                    success = false;
                }
            }
            catch (InvalidCastException ic)
            {
                success = false;
                stackframe.throwAneException(token, ic.Message);
            }
            catch (ArgumentException a)
            {
                success = false;
                stackframe.throwAneException(token, a.Message);
            }
            catch (IndexOutOfRangeException i)
            {
                success = false;
                stackframe.throwAneException(token, i.Message);
            }
            catch (NotSupportedException n)
            {
                success = false;
                stackframe.throwAneException(token, n.Message);
            }

        }
    }

    class system_collections_ilist_insert : NativeConstParameterFunction
    {
        public system_collections_ilist_insert() : base(2)
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_int);
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
                return "system_collections_IList_insert";
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
                return RunTimeDataType.fun_void;
            }
        }

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {

            System.Collections.IList ilist =
                (System.Collections.IList)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

            try
            {
                int index = TypeConverter.ConvertToInt(argements[0]);

                object lo;
                if (stackframe.player.linktypemapper.rtValueToLinkObject(
                    argements[1],
                     (ilist is Array) ? ilist.GetType().GetElementType() :
                    stackframe.player.linktypemapper.getLinkType(argements[1].rtType)
                    ,
                    bin, true, out lo
                    ))
                {
                    ilist.Insert(index, lo);
                    success = true;
                }
                else
                {
                    stackframe.throwCastException(token, argements[1].rtType,
                        (ilist is Array)?
                        stackframe.player.linktypemapper.getRuntimeDataType(ilist.GetType().GetElementType())
                        :
                        functionDefine.signature.parameters[1].type
                        );
                    success = false;
                }
            }
            catch (InvalidCastException ic)
            {
                success = false;
                stackframe.throwAneException(token, ic.Message);
            }
            catch (ArgumentException a)
            {
                success = false;
                stackframe.throwAneException(token, a.Message);
            }
            catch (IndexOutOfRangeException i)
            {
                success = false;
                stackframe.throwAneException(token, i.Message);
            }
            catch (NotSupportedException n)
            {
                success = false;
                stackframe.throwAneException(token, n.Message);
            }

        }
    }

    class system_collections_ilist_remove : NativeConstParameterFunction
    {
        public system_collections_ilist_remove() : base(1)
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
                return "system_collections_IList_remove";
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
                return RunTimeDataType.fun_void;
            }
        }

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {

            System.Collections.IList ilist =
                (System.Collections.IList)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

            try
            {
                object lo;
                if (stackframe.player.linktypemapper.rtValueToLinkObject(
                    argements[0],
                     (ilist is Array) ? ilist.GetType().GetElementType() :
                    stackframe.player.linktypemapper.getLinkType(argements[0].rtType)
                    ,
                    bin, false, out lo
                    ))
                {
                    ilist.Remove(lo);
                    returnSlot.setValue(ASBinCode.rtData.rtUndefined.undefined);
                    success = true;
                }
                else
                {
                    stackframe.throwCastException(token, argements[0].rtType,
                        (ilist is Array)?
                        stackframe.player.linktypemapper.getRuntimeDataType(ilist.GetType().GetElementType())
                        :
                        functionDefine.signature.parameters[0].type
                        );
                    success = false;
                }
            }
            catch (InvalidCastException ic)
            {
                success = false;
                stackframe.throwAneException(token, ic.Message);
            }
            catch (ArgumentException a)
            {
                success = false;
                stackframe.throwAneException(token, a.Message);
            }
            catch (IndexOutOfRangeException i)
            {
                success = false;
                stackframe.throwAneException(token, i.Message);
            }
            catch (NotSupportedException n)
            {
                success = false;
                stackframe.throwAneException(token, n.Message);
            }

        }
    }

    class system_collections_ilist_removeAt : NativeConstParameterFunction
    {
        public system_collections_ilist_removeAt() : base(1)
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_int);
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
                return "system_collections_IList_removeAt";
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
                return RunTimeDataType.fun_void;
            }
        }

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {

            System.Collections.IList ilist =
                (System.Collections.IList)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

            try
            {
                int index = TypeConverter.ConvertToInt(argements[0]);
                ilist.RemoveAt(index);
                success = true;
            }
            catch (InvalidCastException ic)
            {
                success = false;
                stackframe.throwAneException(token, ic.Message);
            }
            catch (ArgumentException a)
            {
                success = false;
                stackframe.throwAneException(token, a.Message);
            }
            catch (IndexOutOfRangeException i)
            {
                success = false;
                stackframe.throwAneException(token, i.Message);
            }
            catch (NotSupportedException n)
            {
                success = false;
                stackframe.throwAneException(token, n.Message);
            }

        }
    }

    class system_collections_idictionary_isfixedsize : NativeConstParameterFunction
    {
        public system_collections_idictionary_isfixedsize() : base(0)
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
                return "system_collections_IDictionary_get_IsFixedSize";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {



            System.Collections.IDictionary idictionary =
                (System.Collections.IDictionary)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

            try
            {
                if (idictionary.IsFixedSize)
                {
                    returnSlot.setValue(ASBinCode.rtData.rtBoolean.True);
                }
                else
                {
                    returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
                }
                //stackframe.player.linktypemapper.storeLinkObject_ToSlot(obj, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);
                //returnSlot.setValue((int)array.GetValue(index));
                success = true;
            }
            catch (KeyNotFoundException)
            {
                success = false;
                stackframe.throwAneException(token, idictionary.ToString() + "没有链接到脚本");
            }
            catch (ArgumentException a)
            {
                success = false;
                stackframe.throwAneException(token, a.Message);
            }
            catch (IndexOutOfRangeException i)
            {
                success = false;
                stackframe.throwAneException(token, i.Message);
            }


        }


    }

    class system_collections_idictionary_isreadonly : NativeConstParameterFunction
    {
        public system_collections_idictionary_isreadonly() : base(0)
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
                return "system_collections_IDictionary_get_IsReadOnly";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {



            System.Collections.IDictionary idictionary =
                (System.Collections.IDictionary)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

            try
            {
                if (idictionary.IsReadOnly)
                {
                    returnSlot.setValue(ASBinCode.rtData.rtBoolean.True);
                }
                else
                {
                    returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
                }
                //stackframe.player.linktypemapper.storeLinkObject_ToSlot(obj, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);
                //returnSlot.setValue((int)array.GetValue(index));
                success = true;
            }
            catch (KeyNotFoundException)
            {
                success = false;
                stackframe.throwAneException(token, idictionary.ToString() + "没有链接到脚本");
            }
            catch (ArgumentException a)
            {
                success = false;
                stackframe.throwAneException(token, a.Message);
            }
            catch (IndexOutOfRangeException i)
            {
                success = false;
                stackframe.throwAneException(token, i.Message);
            }


        }


    }

    class system_collections_idictionary_getthisitem : NativeConstParameterFunction
    {
        public system_collections_idictionary_getthisitem() : base(1)
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType._OBJECT);
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
                return "system_collections_IDictionary_getThisItem";
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


            
            System.Collections.IDictionary idictionary =
                (System.Collections.IDictionary)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

            RunTimeValueBase kv = argements[0];
            if (kv.rtType > RunTimeDataType.unknown)
            {
                RunTimeDataType ot;
                if (TypeConverter.Object_CanImplicit_ToPrimitive(
                    bin.getClassByRunTimeDataType(kv.rtType), out ot
                    ))
                {
                    kv = TypeConverter.ObjectImplicit_ToPrimitive((ASBinCode.rtData.rtObjectBase)kv);
                }
            }
            DictionaryKey key = new DictionaryKey(kv,false);

            try
            {
                
                
                object obj = idictionary[key];
                stackframe.player.linktypemapper.storeLinkObject_ToSlot(obj, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);
                //returnSlot.setValue((int)array.GetValue(index));
                success = true;
                    
                
                

            }
			catch (RuntimeLinkTypeMapper.TypeLinkClassException tlc)
			{
				success = false;
				stackframe.throwAneException(token, tlc.Message);
			}
			catch (KeyNotFoundException)
            {
                success = false;
                stackframe.throwAneException(token, argements[0] + "没有链接到脚本");
            }
            catch (ArgumentException a)
            {
                success = false;
                stackframe.throwAneException(token, a.Message);
            }
            catch (IndexOutOfRangeException i)
            {
                success = false;
                stackframe.throwAneException(token, i.Message);
            }


        }


    }

    class system_collections_idictionary_setthisitem : NativeConstParameterFunction
    {
        public system_collections_idictionary_setthisitem() : base(2)
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType.rt_void);
            para.Add(RunTimeDataType._OBJECT);
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
                return "system_collections_IDictionary_setThisItem";
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
                return RunTimeDataType.fun_void;
            }
        }

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {
            RunTimeValueBase kv = argements[1];
            if (kv.rtType > RunTimeDataType.unknown)
            {
                RunTimeDataType ot;
                if (TypeConverter.Object_CanImplicit_ToPrimitive(
                    bin.getClassByRunTimeDataType(kv.rtType), out ot
                    ))
                {
                    kv = TypeConverter.ObjectImplicit_ToPrimitive((ASBinCode.rtData.rtObjectBase)kv);
                }
            }
            DictionaryKey key = new DictionaryKey(kv,false);

            System.Collections.IDictionary idictionary =
                (System.Collections.IDictionary)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

            try
            {

                object lo;
                if (stackframe.player.linktypemapper.rtValueToLinkObject(
                    argements[0],

                    stackframe.player.linktypemapper.getLinkType( argements[0].rtType), 
                    
                    bin, true, out lo
                    ))
                {
                    idictionary[key] = lo;
                    returnSlot.setValue(ASBinCode.rtData.rtUndefined.undefined);
                    success = true;
                }
                else
                {
                    stackframe.throwCastException(token, argements[0].rtType,
                        functionDefine.signature.parameters[0].type
                        );
                    success = false;
                }

            }
            catch (InvalidCastException ic)
            {
                success = false;
                stackframe.throwAneException(token, ic.Message);
            }
            catch (ArgumentException a)
            {
                success = false;
                stackframe.throwAneException(token, a.Message);
            }
            catch (IndexOutOfRangeException i)
            {
                success = false;
                stackframe.throwAneException(token, i.Message);
            }


        }
    }

    class system_collections_idictionary_keys : NativeConstParameterFunction
    {
        public system_collections_idictionary_keys() : base(0)
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
                return "system_collections_IDictionary_get_Keys";
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



            System.Collections.IDictionary idictionary =
                (System.Collections.IDictionary)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

            try
            {
                
                stackframe.player.linktypemapper.storeLinkObject_ToSlot(
                    idictionary.Keys, 
                    functionDefine.signature.returnType, 
                    returnSlot, bin, stackframe.player);
                //returnSlot.setValue((int)array.GetValue(index));
                success = true;
            }
			catch (RuntimeLinkTypeMapper.TypeLinkClassException tlc)
			{
				success = false;
				stackframe.throwAneException(token, tlc.Message);
			}
			catch (KeyNotFoundException)
            {
                success = false;
                stackframe.throwAneException(token, idictionary.ToString() + "没有链接到脚本");
            }
            catch (ArgumentException a)
            {
                success = false;
                stackframe.throwAneException(token, a.Message);
            }
            catch (IndexOutOfRangeException i)
            {
                success = false;
                stackframe.throwAneException(token, i.Message);
            }


        }


    }

    class system_collections_idictionary_values : NativeConstParameterFunction
    {
        public system_collections_idictionary_values() : base(0)
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
                return "system_collections_IDictionary_get_Values";
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



            System.Collections.IDictionary idictionary =
                (System.Collections.IDictionary)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

            try
            {

                stackframe.player.linktypemapper.storeLinkObject_ToSlot(
                    idictionary.Values,
                    functionDefine.signature.returnType,
                    returnSlot, bin, stackframe.player);
                //returnSlot.setValue((int)array.GetValue(index));
                success = true;
            }
			catch (RuntimeLinkTypeMapper.TypeLinkClassException tlc)
			{
				success = false;
				stackframe.throwAneException(token, tlc.Message);
			}
			catch (KeyNotFoundException)
            {
                success = false;
                stackframe.throwAneException(token, idictionary.ToString() + "没有链接到脚本");
            }
            catch (ArgumentException a)
            {
                success = false;
                stackframe.throwAneException(token, a.Message);
            }
            catch (IndexOutOfRangeException i)
            {
                success = false;
                stackframe.throwAneException(token, i.Message);
            }


        }


    }

    class system_collections_idictionary_add : NativeConstParameterFunction
    {
        public system_collections_idictionary_add() : base(2)
        {
            para = new List<RunTimeDataType>();          
            para.Add(RunTimeDataType._OBJECT);
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
                return "system_collections_IDictionary_add";
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
                return RunTimeDataType.fun_void;
            }
        }

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {
            RunTimeValueBase kv = argements[0];
            if (kv.rtType > RunTimeDataType.unknown)
            {
                RunTimeDataType ot;
                if (TypeConverter.Object_CanImplicit_ToPrimitive(
                    bin.getClassByRunTimeDataType(kv.rtType), out ot
                    ))
                {
                    kv = TypeConverter.ObjectImplicit_ToPrimitive((ASBinCode.rtData.rtObjectBase)kv);
                }
            }
            DictionaryKey key = new DictionaryKey(kv,true);

            System.Collections.IDictionary idictionary =
                (System.Collections.IDictionary)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

            try
            {

                object lo;
                if (stackframe.player.linktypemapper.rtValueToLinkObject(
                    argements[1],

                    stackframe.player.linktypemapper.getLinkType(argements[1].rtType),

                    bin, true, out lo
                    ))
                {
                    idictionary.Add(key, lo);
                    returnSlot.setValue(ASBinCode.rtData.rtUndefined.undefined);
                    success = true;
                }
                else
                {
                    stackframe.throwCastException(token, argements[1].rtType,
                        functionDefine.signature.parameters[1].type
                        );
                    success = false;
                }

            }
            catch (InvalidCastException ic)
            {
                success = false;
                stackframe.throwAneException(token, ic.Message);
            }
            catch (ArgumentException a)
            {
                success = false;
                stackframe.throwAneException(token, a.Message);
            }
            catch (IndexOutOfRangeException i)
            {
                success = false;
                stackframe.throwAneException(token, i.Message);
            }


        }
    }

    class system_collections_idictionary_clear : NativeConstParameterFunction
    {
        public system_collections_idictionary_clear() : base(0)
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
                return "system_collections_IDictionary_clear";
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
                return RunTimeDataType.fun_void;
            }
        }

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {

            System.Collections.IDictionary idictionary =
                (System.Collections.IDictionary)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

            try
            {
                idictionary.Clear();
                success = true;
                returnSlot.directSet(ASBinCode.rtData.rtUndefined.undefined);
            }
            catch (InvalidCastException ic)
            {
                success = false;
                stackframe.throwAneException(token, ic.Message);
            }
            catch (ArgumentException a)
            {
                success = false;
                stackframe.throwAneException(token, a.Message);
            }
            catch (IndexOutOfRangeException i)
            {
                success = false;
                stackframe.throwAneException(token, i.Message);
            }
            catch (NotSupportedException n)
            {
                success = false;
                stackframe.throwAneException(token, n.Message);
            }

        }
    }

    class system_collections_idictionary_contains : NativeConstParameterFunction
    {
        public system_collections_idictionary_contains() : base(1)
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType._OBJECT);
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
                return "system_collections_IDictionary_contains";
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

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {

            System.Collections.IDictionary idictionary =
                (System.Collections.IDictionary)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

            try
            {
                RunTimeValueBase kv = argements[0];
                if (kv.rtType > RunTimeDataType.unknown)
                {
                    RunTimeDataType ot;
                    if (TypeConverter.Object_CanImplicit_ToPrimitive(
                        bin.getClassByRunTimeDataType(kv.rtType), out ot
                        ))
                    {
                        kv = TypeConverter.ObjectImplicit_ToPrimitive((ASBinCode.rtData.rtObjectBase)kv);
                    }
                }
                DictionaryKey key = new DictionaryKey(kv,false);

                if (idictionary.Contains(key))
                {
                    returnSlot.directSet(ASBinCode.rtData.rtBoolean.True);
                }
                else
                {
                    returnSlot.directSet(ASBinCode.rtData.rtBoolean.False);
                }
                success = true;
            }
            catch (InvalidCastException ic)
            {
                success = false;
                stackframe.throwAneException(token, ic.Message);
            }
            catch (ArgumentException a)
            {
                success = false;
                stackframe.throwAneException(token, a.Message);
            }
            catch (IndexOutOfRangeException i)
            {
                success = false;
                stackframe.throwAneException(token, i.Message);
            }
            catch (NotSupportedException n)
            {
                success = false;
                stackframe.throwAneException(token, n.Message);
            }

        }
    }

    class system_collections_idictionary_remove : NativeConstParameterFunction
    {
        public system_collections_idictionary_remove() : base(1)
        {
            para = new List<RunTimeDataType>();
            para.Add(RunTimeDataType._OBJECT);
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
                return "system_collections_IDictionary_remove";
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
                return RunTimeDataType.fun_void;
            }
        }

        public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
        {

            System.Collections.IDictionary idictionary =
                (System.Collections.IDictionary)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

            try
            {
                RunTimeValueBase kv = argements[0];
                if (kv.rtType > RunTimeDataType.unknown)
                {
                    RunTimeDataType ot;
                    if (TypeConverter.Object_CanImplicit_ToPrimitive(
                        bin.getClassByRunTimeDataType(kv.rtType), out ot
                        ))
                    {
                        kv = TypeConverter.ObjectImplicit_ToPrimitive((ASBinCode.rtData.rtObjectBase)kv);
                    }
                }
                DictionaryKey key = new DictionaryKey(kv,false);
                idictionary.Remove(key);

                returnSlot.directSet(ASBinCode.rtData.rtUndefined.undefined);

                success = true;
            }
            catch (InvalidCastException ic)
            {
                success = false;
                stackframe.throwAneException(token, ic.Message);
            }
            catch (ArgumentException a)
            {
                success = false;
                stackframe.throwAneException(token, a.Message);
            }
            catch (IndexOutOfRangeException i)
            {
                success = false;
                stackframe.throwAneException(token, i.Message);
            }
            catch (NotSupportedException n)
            {
                success = false;
                stackframe.throwAneException(token, n.Message);
            }

        }
    }

    class system_collections_idictionary_getienumerator : NativeConstParameterFunction
    {
        public system_collections_idictionary_getienumerator() : base(0)
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
                return "system_collections_IDictionary_getEnumerator_";
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



            System.Collections.IDictionary idictionary =
                (System.Collections.IDictionary)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

            
            try
            {


                object obj = idictionary.GetEnumerator();
                stackframe.player.linktypemapper.storeLinkObject_ToSlot(
                    obj, 
                    stackframe.player.linktypemapper.getRuntimeDataType(typeof(System.Collections.IDictionaryEnumerator))
                    , returnSlot, bin, stackframe.player);
                //returnSlot.setValue((int)array.GetValue(index));
                success = true;




            }
			catch (RuntimeLinkTypeMapper.TypeLinkClassException tlc)
			{
				success = false;
				stackframe.throwAneException(token, tlc.Message);
			}
			catch (KeyNotFoundException)
            {
                success = false;
                stackframe.throwAneException(token, argements[0] + "没有链接到脚本");
            }
            catch (ArgumentException a)
            {
                success = false;
                stackframe.throwAneException(token, a.Message);
            }
            catch (IndexOutOfRangeException i)
            {
                success = false;
                stackframe.throwAneException(token, i.Message);
            }


        }


    }



	// class system_collections_idictonaryenumerator_entry : NativeConstParameterFunction
	// {
	//     public system_collections_idictonaryenumerator_entry() : base(0)
	//     {
	//         para = new List<RunTimeDataType>();

	//     }

	//     public override bool isMethod
	//     {
	//         get
	//         {
	//             return true;
	//         }
	//     }

	//     public override string name
	//     {
	//         get
	//         {
	//             return "system_collections_idictionaryenumerator_entry";
	//         }
	//     }

	//     List<RunTimeDataType> para;
	//     public override List<RunTimeDataType> parameters
	//     {
	//         get
	//         {
	//             return para;
	//         }
	//     }

	//     public override RunTimeDataType returnType
	//     {
	//         get
	//         {
	//             return RunTimeDataType.rt_void;
	//         }
	//     }

	//     public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
	//     {



	//         System.Collections.IDictionaryEnumerator enumerator =
	//             (System.Collections.IDictionaryEnumerator)((LinkObj<object>)((ASBinCode.rtData.rtObjectBase)thisObj).value).value;

	//         try
	//         {
	//             object obj = enumerator.Entry;

	//             stackframe.player.linktypemapper.storeLinkObject_ToSlot(obj, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);
	//             //returnSlot.setValue((int)array.GetValue(index));
	//             success = true;
	//         }
	//catch (RuntimeLinkTypeMapper.TypeLinkClassException tlc)
	//{
	//	success = false;
	//	stackframe.throwAneException(token, tlc.Message);
	//}
	//catch (KeyNotFoundException)
	//         {
	//             success = false;
	//             stackframe.throwAneException(token, (enumerator.Current != null ? enumerator.Current.ToString() : (enumerator.ToString() + ".current的值")) + "没有链接到脚本");
	//         }
	//         catch (ArgumentException a)
	//         {
	//             success = false;
	//             stackframe.throwAneException(token, a.Message);
	//         }
	//         catch (IndexOutOfRangeException i)
	//         {
	//             success = false;
	//             stackframe.throwAneException(token, i.Message);
	//         }
	//         catch (InvalidOperationException io)
	//         {
	//             success = false;
	//             stackframe.throwAneException(token, io.Message);
	//         }

	//     }


	// }

	class system_collections_IDictionaryEnumerator_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_collections_IDictionaryEnumerator_creator", default(System.Collections.IDictionaryEnumerator)));
			bin.regNativeFunction(new system_collections_IDictionaryEnumerator_get_Key());
			bin.regNativeFunction(new system_collections_IDictionaryEnumerator_get_Value());
			bin.regNativeFunction(new system_collections_IDictionaryEnumerator_get_Entry());
		}

		class system_collections_IDictionaryEnumerator_get_Key : NativeConstParameterFunction
		{
			public system_collections_IDictionaryEnumerator_get_Key() : base(0)
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
					return "system_collections_IDictionaryEnumerator_get_Key";
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

				System.Collections.IDictionaryEnumerator _this =
					(System.Collections.IDictionaryEnumerator)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

				try
				{

					object _result_ = _this.Key
					;
					stackframe.player.linktypemapper.storeLinkObject_ToSlot(_result_, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);
					success = true;
				}
				catch (RuntimeLinkTypeMapper.TypeLinkClassException tlc)
				{
					success = false;
					stackframe.throwAneException(token, tlc.Message);
				}
				catch (InvalidCastException ic)
				{
					success = false;
					stackframe.throwAneException(token, ic.Message);
				}
				catch (ArgumentException a)
				{
					success = false;
					stackframe.throwAneException(token, a.Message);
				}
				catch (IndexOutOfRangeException i)
				{
					success = false;
					stackframe.throwAneException(token, i.Message);
				}
				catch (NotSupportedException n)
				{
					success = false;
					stackframe.throwAneException(token, n.Message);
				}

			}
		}

		class system_collections_IDictionaryEnumerator_get_Value : NativeConstParameterFunction
		{
			public system_collections_IDictionaryEnumerator_get_Value() : base(0)
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
					return "system_collections_IDictionaryEnumerator_get_Value";
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

				System.Collections.IDictionaryEnumerator _this =
					(System.Collections.IDictionaryEnumerator)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

				try
				{

					object _result_ = _this.Value
					;
					stackframe.player.linktypemapper.storeLinkObject_ToSlot(_result_, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);
					success = true;
				}
				catch (RuntimeLinkTypeMapper.TypeLinkClassException tlc)
				{
					success = false;
					stackframe.throwAneException(token, tlc.Message);
				}
				catch (InvalidCastException ic)
				{
					success = false;
					stackframe.throwAneException(token, ic.Message);
				}
				catch (ArgumentException a)
				{
					success = false;
					stackframe.throwAneException(token, a.Message);
				}
				catch (IndexOutOfRangeException i)
				{
					success = false;
					stackframe.throwAneException(token, i.Message);
				}
				catch (NotSupportedException n)
				{
					success = false;
					stackframe.throwAneException(token, n.Message);
				}

			}
		}

		class system_collections_IDictionaryEnumerator_get_Entry : NativeConstParameterFunction
		{
			public system_collections_IDictionaryEnumerator_get_Entry() : base(0)
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
					return "system_collections_IDictionaryEnumerator_get_Entry";
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

				System.Collections.IDictionaryEnumerator _this =
					(System.Collections.IDictionaryEnumerator)((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();

				try
				{

					System.Collections.DictionaryEntry _result_ = _this.Entry
					;
					((StackSlot)returnSlot).setLinkObjectValue(bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, _result_);
					success = true;
				}
				catch (RuntimeLinkTypeMapper.TypeLinkClassException tlc)
				{
					success = false;
					stackframe.throwAneException(token, tlc.Message);
				}
				catch (InvalidCastException ic)
				{
					success = false;
					stackframe.throwAneException(token, ic.Message);
				}
				catch (ArgumentException a)
				{
					success = false;
					stackframe.throwAneException(token, a.Message);
				}
				catch (IndexOutOfRangeException i)
				{
					success = false;
					stackframe.throwAneException(token, i.Message);
				}
				catch (NotSupportedException n)
				{
					success = false;
					stackframe.throwAneException(token, n.Message);
				}

			}
		}

	}


}
