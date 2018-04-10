using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode;
using ASBinCode.rtData;

namespace ASRuntime
{
    /// <summary>
    /// 程序执行栈的存储结构
    /// </summary>
    public sealed class StackSlot : SLOT
    {
		/// <summary>
		/// 所有引用类型都用一个槽
		/// </summary>
		internal const int COMMREFTYPEOBJ = RunTimeDataType._OBJECT;

		internal Player player;

        public StackSlot(CSWC classfinder,Player player)
        {
			this.player = player;
            store = new RunTimeValueBase[(int)RunTimeDataType._OBJECT+1];
            index = (int)RunTimeDataType.unknown;

			_cache_arraySlot = new operators.OpAccess_Dot.arraySlot(null,0,classfinder);
            _cache_vectorSlot = new operators.OpVector.vectorSLot(null, 0,classfinder);
            _cache_prototypeSlot = new operators.OpAccess_Dot.prototypeSlot(null, null, null);
            _cache_setthisslot = new SetThisItemSlot();
            
            //存储器设置初始值
            for (int i = 0; i < RunTimeDataType._OBJECT+1; i++)
            {
                RunTimeDataType t = (RunTimeDataType)i;
                if(t != RunTimeDataType.unknown)
                {
                    store[i] = TypeConverter.getDefaultValue(t).getValue(null,null);
                }
            }

			store[RunTimeDataType.rt_array] = null;
			store[RunTimeDataType.rt_string] = null;
			store[RunTimeDataType.rt_function] = null;


			_numberValue = (rtNumber)store[RunTimeDataType.rt_number];
            _intValue = (rtInt)store[RunTimeDataType.rt_int];
            _uintValue = (rtUInt)store[RunTimeDataType.rt_uint];
			_stringValue = new rtString(string.Empty);

			_functionValue = new rtFunction(-1, false);
			_functon_result = new rtFunction(-1, false);
        }

        private rtNumber _numberValue;
        private rtInt _intValue;
        private rtUInt _uintValue;		
		private rtString _stringValue;
#if DEBUG
		private 
#else
		internal
#endif 
			rtFunction _functionValue;

#if DEBUG
		private
#else
		internal
#endif
			rtFunction _functon_result;



		internal StackObjects stackObjects;

		internal operators.OpAccess_Dot.arraySlot _cache_arraySlot;
        internal operators.OpVector.vectorSLot _cache_vectorSlot;
        internal operators.OpAccess_Dot.prototypeSlot _cache_prototypeSlot;
        internal SetThisItemSlot _cache_setthisslot;

        

        internal StackLinkObjectCache _linkObjCache;
		
#if DEBUG
		private
#else
		internal
#endif 
			SLOT linktarget;

		public void linkTo(SLOT linktarget)
        {
			needclear = true;refPropChanged = true;
            this.linktarget = linktarget;index = -1;
        }

		public SLOT getLinkSlot()
		{
			return linktarget;
		}

#if DEBUG
		private 
#else
		internal
#endif
			int index;

#if DEBUG
		private 
#else
		internal
#endif
			RunTimeValueBase[] store;

        public sealed override bool isPropGetterSetter
        {
            get
            {
                if (linktarget != null)
                {
					return linktarget.isPropGetterSetter;
                }
                else
                {
                    return false;
                }
            }
        }

        public sealed override bool isSetThisItem
        {
            get
            {
                if (linktarget != null)
                {
					return linktarget.isSetThisItem;
                }
                else
                {
                    return false;
                }
            }
        }

		public override SLOT assign(RunTimeValueBase value, out bool success)
		{
			
			if (linktarget != null)
			{
				linktarget.assign(value,out success);

				var result = linktarget;

				linktarget = null;

				return result;
			}
			else
			{
				success = directSet(value);
				return this;
			}
		}


		public sealed override bool directSet(RunTimeValueBase value)
        {
			
            if (linktarget != null)
            {
                return  linktarget.directSet(value);
            }
            else
            {

                index = (int)value.rtType;
                //if (index > RunTimeDataType.unknown) //若大于unknown,则说明是一个对象
                //{
                //    index = RunTimeDataType._OBJECT;
                //}               
                //值类型必须拷贝!!否则值可能被其他引用而导致错误
                //私有构造函数的数据可以直接传引用，否则必须拷贝赋值。
                switch (value.rtType)
                {
                    case RunTimeDataType.rt_boolean:
                        store[0] = value;
                        break;
                    case RunTimeDataType.rt_int:
                        //setValue(((rtInt)value).value);
                        _intValue.value = ((rtInt)value).value;
                        break;
                    case RunTimeDataType.rt_uint:
                        //setValue(((rtUInt)value).value);
                        _uintValue.value = ((rtUInt)value).value;
                        break;
                    case RunTimeDataType.rt_number:
                        
                        _numberValue.value= ((rtNumber)value).value;
                        
                        break;
                    case RunTimeDataType.rt_string:
                        setValue(((rtString)value).value);
                        break;
                    case RunTimeDataType.rt_void:
                        store[index] = value;
                        break;
                    case RunTimeDataType.rt_null:
                        store[6] = value;
                        break;
                    case RunTimeDataType.rt_function:
                        {//Function需要保存上下文环境。因此需要像值类型那样进行拷贝
							_functionValue.CopyFrom((rtFunction)value);
							//store[index] = _functionValue;
							store[COMMREFTYPEOBJ] = _functionValue;
							needclear = true; refPropChanged = true;
						}
                        break;
                    case RunTimeDataType.fun_void:
                        store[8] = value;
                        break;
                    case RunTimeDataType.rt_array:
                        {
							//store[index] = value;
							store[COMMREFTYPEOBJ] = value; refPropChanged = true;
						}
                        break;
                    case RunTimeDataType.unknown:
                        store[10] = null;
                        break;
                    default:
                        {
							index = RunTimeDataType._OBJECT;
							rtObjectBase obj = (rtObjectBase)value;refPropChanged = true;
                            if (obj.value._class.isLink_System)
                            {
								if (obj is StackLinkObjectCache.StackCacheObject)
								{
									needclear = true; 
									//链接到系统的对象。这里需要用到缓存的rtObject，以避免当调用链接对象的方法并返回的也是链接对象时，
									//要重新创建rtObject,而是直接更新缓存的rtObject.
									var cacheobj = _linkObjCache.getCacheObj(obj.value._class);
									ASBinCode.rtti.LinkSystemObject link = (ASBinCode.rtti.LinkSystemObject)cacheobj.value;

									if (obj.value._class.isStruct)
									{
										link.CopyStructData((ASBinCode.rtti.LinkSystemObject)obj.value);

									}
									else
									{
										link._class = obj.value._class;
										link.SetLinkData(((ASBinCode.rtti.LinkSystemObject)obj.value).GetLinkData());
										cacheobj.rtType = obj.rtType;
										cacheobj.objScope.blockId = obj.value._class.blockid;

									}




									//rtObject srcObj = ((rtObject)value);
									//StackLinkObjectCache.StackCacheObject ss = srcObj as StackLinkObjectCache.StackCacheObject;
									//if (ss != null)
									//{
									//	srcObj = ss.getSrcObject();
									//}

									//if (!(srcObj is StackLinkObjectCache.StackCacheObject))
									//{
									//	//当对象方法调用已绑定了这个槽中的链接对象，然后下面又复用了这个槽时
									//	//为了避免下面寄存器复用破坏缓存中的LinkSystemObject对象，缓存一份
									//	_linkObjCache.srcObject = srcObj;
									//}


									//store[RunTimeDataType._OBJECT] = cacheobj;
									store[COMMREFTYPEOBJ] = cacheobj;
								}
								else
								{
									store[COMMREFTYPEOBJ] = value;

								}
							}
                            else
                            {
								//store[RunTimeDataType._OBJECT] = value;
								store[COMMREFTYPEOBJ] = value;
                            }
                        }
                        break;
                }
                return true;
            }
            
        }

		//仅用于链接对象的new时，直接new到栈中,此时返回缓存对象再执行后续的构造函数操作
		internal StackLinkObjectCache.StackCacheObject getStackCacheObject(ASBinCode.rtti.Class clsType)
		{
			needclear = true; refPropChanged = true;
			index = RunTimeDataType._OBJECT;

			var cacheobj = _linkObjCache.getCacheObj(clsType);

			if (clsType.isStruct)
			{
			}
			else
			{
				var link = (ASBinCode.rtti.LinkSystemObject)cacheobj.value;
				link._class = clsType;
				link.SetLinkData(null);
				cacheobj.rtType = clsType.getRtType();
				cacheobj.objScope.blockId = clsType.blockid;
			}

			//store[RunTimeDataType._OBJECT] = cacheobj;
			store[COMMREFTYPEOBJ] = cacheobj;


			return cacheobj;
		}


        //仅用于链接对象的赋值更新
        public void setLinkObjectValue<T>(ASBinCode.rtti.Class clsType, Player player ,T value)
        {
			
			needclear = true; refPropChanged = true;
			index = RunTimeDataType._OBJECT;

            var cacheobj = _linkObjCache.getCacheObj(clsType);
			
            if (clsType.isStruct )
            {
                var link = (ASBinCode.rtti.LinkObj<T>)cacheobj.value;
                link.value = value;
            }
            else
            {
                var link = (ASBinCode.rtti.LinkSystemObject)cacheobj.value;
                link._class = clsType;
                link.SetLinkData(value);
                cacheobj.rtType = clsType.getRtType();
                cacheobj.objScope.blockId = clsType.blockid;

            }

			//store[RunTimeDataType._OBJECT] = cacheobj;
			store[COMMREFTYPEOBJ] = cacheobj;
        }


        public sealed override RunTimeValueBase getValue()
        {
			
			//if (linktarget != null)
   //         {
   //             return linktarget.getValue();
   //         }
   //         else
            {
				switch (index)
				{
					case -1:
						return linktarget.getValue();
					
					case RunTimeDataType.rt_array:
					case RunTimeDataType.rt_string:					
						//return store[COMMREFTYPEOBJ];
					case RunTimeDataType._OBJECT:
						return store[COMMREFTYPEOBJ];
					case RunTimeDataType.rt_function:
						_functon_result.CopyFrom(_functionValue);
						return _functon_result;


					//var k = store[COMMREFTYPEOBJ];
					//if (k is StackLinkObjectCache.StackCacheObject)
					//{
					//	return ((StackLinkObjectCache.StackCacheObject)k).getSrcObject();
					//}
					//else
					//{
					//	return (RunTimeValueBase)k;
					//}
					default:
						return store[index];
						
				}


				//var k = store[index];
				//if (k is StackLinkObjectCache.StackCacheObject)
				//{
				//	return ((StackLinkObjectCache.StackCacheObject)k).getSrcObject();
				//}
				//else
				//{
				//	return (RunTimeValueBase)k;
				//}

			}
            //throw new NotImplementedException();
        }

        public sealed override void setValue(string value)
        {
			
            if (linktarget != null)
            {
                linktarget.setValue(value);
            }
            else
            {
                index = RunTimeDataType.rt_string;
                if (value == null)
                {
					//store[(int)RunTimeDataType.rt_string] = rtNull.nullptr;
					store[COMMREFTYPEOBJ] = rtNull.nullptr;
                }
                else
                {
					_stringValue.value =  value;
					//store[RunTimeDataType.rt_string] = _stringValue;
					store[COMMREFTYPEOBJ] = _stringValue;
					
				}
            }
        }

        public sealed override void setValue(rtUndefined value)
        {
			
            if (linktarget != null)
            {
                linktarget.setValue(value);
            }
            else
            {
                index = RunTimeDataType.rt_void;
                store[index] = value;
            }
        }

        public sealed override void setValue(rtNull value)
        {
			
			if (linktarget != null)
            {
                linktarget.setValue(value);
            }
            else
            {
                index = RunTimeDataType.rt_null;
                store[index] = value;
            }
        }

        public sealed override void setValue(uint value)
        {
			
			if (linktarget != null)
            {
                linktarget.setValue(value);
            }
            else
            {
                index = RunTimeDataType.rt_uint;
                _uintValue.value = value;
                //((rtUInt)store[index]).value = value;
            }
        }

        public sealed override void setValue(int value)
        {
			
			if (linktarget != null)
            {
                linktarget.setValue(value);
            }
            else
            {
                index = RunTimeDataType.rt_int;
                _intValue.value = value;
                //((rtInt)store[index]).value = value;
            }
        }

        public sealed override void setValue(double value)
        {
			
			if (linktarget != null)
            {
                linktarget.setValue(value);
            }
            else
            {
                index = RunTimeDataType.rt_number;
                _numberValue.value = value;
                //((rtNumber)store[index]).value = value;
            }
        }

        public sealed override void setValue(rtBoolean value)
        {
			
			if (linktarget != null)
            {
                linktarget.setValue(value);
            }
            else
            {
                index = RunTimeDataType.rt_boolean;
                store[index] = value;
            }
        }

#if DEBUG

#else
	internal
#endif 
		bool needclear;

		internal bool refPropChanged;

        public sealed override void clear()
        {
			if (refPropChanged)
			{
				refPropChanged = false;
				stackObjects = StackObjects.EMPTY;


				if (needclear)
				{
					linktarget = null;
					_cache_arraySlot.clear();
					_cache_vectorSlot.clear();
					_cache_prototypeSlot.clear();
					_cache_setthisslot.clear();
					_linkObjCache.clearRefObj();
					_functionValue.Clear();
					_functon_result.Clear();
					needclear = false;
				}



				store[COMMREFTYPEOBJ] = rtNull.nullptr;
			}
			index = (int)RunTimeDataType.unknown;
        }

		
		

		public struct StackObjects
		{
			public static readonly StackObjects EMPTY = new StackObjects();

			
			public ASBinCode.ClassPropertyGetter propGetSet;
			public ASBinCode.rtData.rtObjectBase propBindObj;
			public ASBinCode.rtti.Class superPropBindClass;
			public SetThisItemSlot _temp_try_write_setthisitem;
		}



    }
}
