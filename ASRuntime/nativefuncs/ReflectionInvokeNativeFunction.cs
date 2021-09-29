using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode;
using ASBinCode.rtti;
using System.Reflection;
using System.Globalization;
using ASBinCode.rtData;

namespace ASRuntime.nativefuncs
{
	public abstract class ReflectionInvokeNativeFunction : NativeConstParameterFunction
	{
		public const int LINKTYPE = 0;
		public const int SHORT = 1;
		public const int USHORT = 2;
		public const int INT = 3;
		public const int UINT = 4;
		public const int FLOAT = 5;
		public const int NUMBER = 6;
		public const int BOOLEAN = 7;
		public const int STRING = 8;
		public const int VOID = 9;

		public ReflectionInvokeNativeFunction(int totalargs):base(totalargs)
		{

		}

		
	}

	/// <summary>
	/// 通过反射调用本地函数。
	/// </summary>
	public sealed class ReflectionInvokeNativeFunction<T> : ReflectionInvokeNativeFunction,IMethodGetter
	{
		


		private string functionname;
		private string reflectionname;
		private RunTimeDataType rttype;

		private int returncode;
		private int[] parametertypecodes;

		private bool isstatic;
		private bool needswap;
		public ReflectionInvokeNativeFunction(bool isstatic, bool needswap, int totalargs, string functionname, string reflectionname, int rtcode, int[] paras) : base(totalargs)
		{
			this.isstatic = isstatic;
			this.needswap = needswap;

			this.functionname = functionname;
			this.reflectionname = reflectionname;

			this.rttype = CodeToRuntimeDataType(rtcode);
			this.returncode = rtcode;

			this.parametertypecodes = new int[paras.Length];
			paras.CopyTo(parametertypecodes, 0);

			para = new List<RunTimeDataType>();

			for (int i = 0; i < paras.Length; i++)
			{
				para.Add(CodeToRuntimeDataType(paras[i]));
			}

		}

		private RunTimeDataType CodeToRuntimeDataType(int code)
		{
			switch (code)
			{
				case 0:
					return RunTimeDataType.rt_void;
				case 1:
					return RunTimeDataType.rt_int;
				case 2:
					return RunTimeDataType.rt_uint;
				case 3:
					return RunTimeDataType.rt_int;
				case 4:
					return RunTimeDataType.rt_uint;
				case 5:
				case 6:
					return RunTimeDataType.rt_number;
				case 7:
					return RunTimeDataType.rt_boolean;
				case 8:
					return RunTimeDataType.rt_string;
				case 9:
					return RunTimeDataType.fun_void;
				default:
					throw new ArgumentOutOfRangeException();
			}
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
				return functionname;
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
				return rttype;
			}
		}

		private Type[] getNativeFunctionParameterTypes(FunctionDefine functionDefine, CSWC swc, ASRuntime.Player player)
		{
			Type[] result = new Type[functionDefine.signature.parameters.Count];

			if (functionDefine.signature.parameters.Count != this.parametertypecodes.Length)
			{
				throw new ArgumentException("parameter count dismatch");
			}

			for (int i = 0; i < functionDefine.signature.parameters.Count; i++)
			{
				int code = parametertypecodes[i];

				switch (code)
				{
					case LINKTYPE:
						var scls = swc.getClassByRunTimeDataType(functionDefine.signature.parameters[i].type);

						//if (scls.isInterface)
						{
							result[i] = player.linktypemapper.getLinkType(functionDefine.signature.parameters[i].type);
						}
						//else
						//{
						//	var cextend = scls.staticClass.linkObjCreator;
						//	var func = swc.getNativeFunction(((ClassMethodGetter)cextend.bindField).functionId);
						//	result[i] = ((ILinkSystemObjCreator)func).getLinkSystemObjType();
						//}
						break;
					case VOID:
						throw new ArgumentException("Parameter type cannot be void");
					case SHORT:
						result[i] = typeof(short);
						break;
					case USHORT:
						result[i] = typeof(ushort);
						break;
					case INT:
						result[i] = typeof(int);
						break;
					case UINT:
						result[i] = typeof(uint);
						break;
					case FLOAT:
						result[i] = typeof(float);
						break;
					case NUMBER:
						result[i] = typeof(double);
						break;
					case BOOLEAN:
						result[i] = typeof(bool);
						break;
					case STRING:
						result[i] = typeof(string);
						break;
				}


			}

			if (needswap)
			{
				var temp = result[0];
				result[0] = result[1];
				result[1] = temp;
			}

			return result;

		}
		private object _cthis;
		private RunTimeValueBase _cthisobj;

		private MethodInfo _cachemethod;
		private object[] sendparameters;
		private bool isreflectionfailed;
		private string referrormsg;
		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			//System.Reflection.MethodInfo method =
			//		typeof(AutoGenCodeLib.Testobj).GetMethod("GetTS", System.Reflection.BindingFlags.Public | BindingFlags.Instance, new MyBinder(), new Type[] { typeof(Decimal) }, null);

			//throw new NotImplementedException();
			if (isreflectionfailed)
			{
				stackframe.throwAneException(token, referrormsg);
				success = false;
				return;
			}


			object _this;
			if (isstatic)
			{
				_this = null;
			}
			else
			{
				_this =
					   ((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).GetLinkData();
			}



			if (_cachemethod == null)
			{
				try
				{
					_cthis = _this;_cthisobj = thisObj;
					GetMethodInfo(functionDefine, stackframe.player.swc, stackframe.player);
					
					if (_cachemethod == null)
					{
						isreflectionfailed = true;
						referrormsg = _this.GetType().FullName + ":: Method " + reflectionname + " not found";
						success = false;
						stackframe.throwAneException(token, referrormsg);
						return;
					}


				}
				catch (AmbiguousMatchException ex)
				{
					isreflectionfailed = true;
					referrormsg = _this.GetType().FullName + ":: Method " + reflectionname + " find failed。 " + ex.Message;
					success = false;
					stackframe.throwAneException(token, referrormsg);
					return;
				}
				catch (ArgumentNullException ex)
				{
					isreflectionfailed = true;
					referrormsg = _this.GetType().FullName + ":: Method " + reflectionname + " find failed。 " + ex.Message;
					success = false;
					stackframe.throwAneException(token, referrormsg);
					return;
				}
				catch (ArgumentException ex)
				{
					isreflectionfailed = true;
					referrormsg = _this.GetType().FullName + ":: Method " + reflectionname + " find failed。 " + ex.Message;
					success = false;
					stackframe.throwAneException(token, referrormsg);
					return;
				}
				finally
				{
					_cthis = null; _cthisobj = null;
				}


				sendparameters = new object[functionDefine.signature.parameters.Count];
			}


			try
			{


				for (int i = 0; i < this.parametertypecodes.Length; i++)
				{

					int code = parametertypecodes[i];

					switch (code)
					{
						case LINKTYPE:
							if (argements[i].rtType == RunTimeDataType.rt_null)
							{
								sendparameters[i] = null;
							}
							else
							{
								sendparameters[i] = ((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)argements[i]).value).GetLinkData();
							}
							break;
						case VOID:
							throw new ArgumentException("Parameter type cannot be void");
						case SHORT:
							sendparameters[i] = (short)TypeConverter.ConvertToInt(argements[i]);
							break;
						case USHORT:
							sendparameters[i] = (ushort)TypeConverter.ConvertToUInt(argements[i], stackframe, token);
							break;
						case INT:
							sendparameters[i] = TypeConverter.ConvertToInt(argements[i]);
							break;
						case UINT:
							sendparameters[i] = TypeConverter.ConvertToUInt(argements[i], stackframe, token);
							break;
						case FLOAT:
							sendparameters[i] = (float)TypeConverter.ConvertToNumber(argements[i]);
							break;
						case NUMBER:
							sendparameters[i] = TypeConverter.ConvertToNumber(argements[i]);
							break;
						case BOOLEAN:
							sendparameters[i] = TypeConverter.ConvertToBoolean(argements[i], stackframe, token);
							break;
						case STRING:
							sendparameters[i] = TypeConverter.ConvertToString(argements[i], stackframe, token);
							break;
					}
				}
				if (needswap)
				{
					var temp = sendparameters[0];
					sendparameters[0] = sendparameters[1];
					sendparameters[1] = temp;
				}

				

				object _result_ = _cachemethod.Invoke(_this, sendparameters);
				if (!isstatic)
				{
					((LinkSystemObject)((ASBinCode.rtData.rtObjectBase)thisObj).value).SetLinkData(_this);
				}
				switch (returncode)
				{
					case LINKTYPE:

						if (_result_ == null)
						{
							returnSlot.directSet(ASBinCode.rtData.rtNull.nullptr);
						}
						else
						{
							((StackSlot)returnSlot).setLinkObjectValue<T>(bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, (T)_result_);
						}
						break;
					case VOID:
						returnSlot.directSet(ASBinCode.rtData.rtUndefined.undefined);
						break;
					case SHORT:
						returnSlot.setValue((int)((short)_result_));
						break;
					case USHORT:
						returnSlot.setValue((uint)((ushort)_result_));
						break;
					case INT:
						returnSlot.setValue((int)_result_);
						break;
					case UINT:
						returnSlot.setValue((uint)_result_);
						break;
					case FLOAT:
						returnSlot.setValue((double)((float)_result_));
						break;
					case NUMBER:
						returnSlot.setValue((double)_result_);
						break;
					case BOOLEAN:
						if ((bool)_result_)
						{
							returnSlot.setValue(ASBinCode.rtData.rtBoolean.True);
						}
						else
						{
							returnSlot.setValue(ASBinCode.rtData.rtBoolean.False);
						}
						break;
					case STRING:
						if (_result_ == null)
						{
							returnSlot.setValue(ASBinCode.rtData.rtNull.nullptr);
						}
						else
						{
							returnSlot.setValue((string)_result_);
						}
						break;
				}

				success = true;
			}
			catch (ASRunTimeException tlc)
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
			finally
			{
				for (int i = 0; i < sendparameters.Length; i++)
				{
					sendparameters[i] = null;
				}
			}


		}

		public MethodInfo GetMethodInfo(ASBinCode.rtti.FunctionDefine functionDefine, ASBinCode.CSWC swc, ASRuntime.Player player)
		{
			if (!isstatic)
			{
				_cachemethod =
					_cthis.GetType().GetMethod(reflectionname, System.Reflection.BindingFlags.Public | BindingFlags.Instance, new MyBinder(),
					getNativeFunctionParameterTypes(functionDefine, swc, player), null);
			}
			else
			{
				var cls = ((ASBinCode.rtData.rtObjectBase)_cthisobj).value._class;

				var cextend = cls.linkObjCreator;
				var func = swc.getNativeFunction(((ClassMethodGetter)cextend.bindField).functionId);
				var type = ((ILinkSystemObjCreator)func).getLinkSystemObjType();

				_cachemethod =
					type.GetMethod(reflectionname, System.Reflection.BindingFlags.Public | BindingFlags.Static, new MyBinder(),
					getNativeFunctionParameterTypes(functionDefine, swc, player), null);

			}

			return _cachemethod;
		}

		class MyBinder : Binder
		{
			public MyBinder() : base()
			{
			}
			private class BinderState
			{
				public object[] args;
			}
			public override FieldInfo BindToField(
				BindingFlags bindingAttr,
				FieldInfo[] match,
				object value,
				CultureInfo culture
				)
			{
				if (match == null)
					throw new ArgumentNullException("match");
				// Get a field for which the value parameter can be converted to the specified field type.
				for (int i = 0; i < match.Length; i++)
					if (ChangeType(value, match[i].FieldType, culture) != null)
						return match[i];
				return null;
			}
			public override MethodBase BindToMethod(
				BindingFlags bindingAttr,
				MethodBase[] match,
				ref object[] args,
				ParameterModifier[] modifiers,
				CultureInfo culture,
				string[] names,
				out object state
				)
			{
				// Store the arguments to the method in a state object.
				BinderState myBinderState = new BinderState();
				object[] arguments = new System.Object[args.Length];
				args.CopyTo(arguments, 0);
				myBinderState.args = arguments;
				state = myBinderState;
				if (match == null)
					throw new ArgumentNullException();
				// Find a method that has the same parameters as those of the args parameter.
				for (int i = 0; i < match.Length; i++)
				{
					if (match[i].IsGenericMethodDefinition)
						continue;

					// Count the number of parameters that match.
					int count = 0;
					ParameterInfo[] parameters = match[i].GetParameters();
					// Go on to the next method if the number of parameters do not match.
					if (args.Length != parameters.Length)
						continue;
					// Match each of the parameters that the user expects the method to have.
					for (int j = 0; j < args.Length; j++)
					{
						// If the names parameter is not null, then reorder args.
						if (names != null)
						{
							if (names.Length != args.Length)
								throw new ArgumentException("names and args must have the same number of elements.");
							for (int k = 0; k < names.Length; k++)
								if (String.Compare(parameters[j].Name, names[k].ToString()) == 0)
									args[j] = myBinderState.args[k];
						}
						// Determine whether the types specified by the user can be converted to the parameter type.
						if (ChangeType(args[j], parameters[j].ParameterType, culture) != null)
							count += 1;
						else
							break;
					}
					// Determine whether the method has been found.
					if (count == args.Length)
						return match[i];
				}
				return null;
			}

			public override object ChangeType(object value, Type type, CultureInfo culture)
			{


				// Determine whether the value parameter can be converted to a value of type myType.
				if (CanConvertFrom(value.GetType(), type))
					// Return the converted object.
					return Convert.ChangeType(value, type);
				else
					// Return null.
					return null;

			}

			public override void ReorderArgumentArray(
				ref object[] args,
				object state
				)
			{
				// Return the args that had been reordered by BindToMethod.
				((BinderState)state).args.CopyTo(args, 0);
			}
			public override MethodBase SelectMethod(
				BindingFlags bindingAttr,
				MethodBase[] match,
				Type[] types,
				ParameterModifier[] modifiers
				)
			{
				if (match == null)
					throw new ArgumentNullException("match");
				for (int i = 0; i < match.Length; i++)
				{
					// Count the number of parameters that match.
					int count = 0;
					ParameterInfo[] parameters = match[i].GetParameters();
					// Go on to the next method if the number of parameters do not match.
					if (types.Length != parameters.Length)
						continue;
					// Match each of the parameters that the user expects the method to have.
					for (int j = 0; j < types.Length; j++)
						// Determine whether the types specified by the user can be converted to parameter type.
						if ((types[j].Equals(parameters[j].ParameterType)))
							count += 1;
						else
							break;
					// Determine whether the method has been found.
					if (count == types.Length)
						return match[i];
				}
				return null;
			}
			public override PropertyInfo SelectProperty(
				BindingFlags bindingAttr,
				PropertyInfo[] match,
				Type returnType,
				Type[] indexes,
				ParameterModifier[] modifiers
				)
			{
				if (match == null)
					throw new ArgumentNullException("match");
				for (int i = 0; i < match.Length; i++)
				{
					// Count the number of indexes that match.
					int count = 0;
					ParameterInfo[] parameters = match[i].GetIndexParameters();
					// Go on to the next property if the number of indexes do not match.
					if (indexes.Length != parameters.Length)
						continue;
					// Match each of the indexes that the user expects the property to have.
					for (int j = 0; j < indexes.Length; j++)
						// Determine whether the types specified by the user can be converted to index type.
						if ((indexes[j].Equals(parameters[j].ParameterType)))
							count += 1;
						else
							break;
					// Determine whether the property has been found.
					if (count == indexes.Length)
						// Determine whether the return type can be converted to the properties type.
						if ((returnType.Equals(match[i].PropertyType)))
							return match[i];
						else
							continue;
				}
				return null;
			}
			// Determines whether type1 can be converted to type2. Check only for primitive types.
			private bool CanConvertFrom(Type type1, Type type2)
			{
				if (type1.IsPrimitive && type2.IsPrimitive)
				{
					TypeCode typeCode1 = Type.GetTypeCode(type1);
					TypeCode typeCode2 = Type.GetTypeCode(type2);
					// If both type1 and type2 have the same type, return true.
					if (typeCode1 == typeCode2)
						return true;
					// Possible conversions from Char follow.
					if (typeCode1 == TypeCode.Char)
						switch (typeCode2)
						{
							case TypeCode.UInt16: return true;
							case TypeCode.UInt32: return true;
							case TypeCode.Int32: return true;
							case TypeCode.UInt64: return true;
							case TypeCode.Int64: return true;
							case TypeCode.Single: return true;
							case TypeCode.Double: return true;
							default: return false;
						}
					// Possible conversions from Byte follow.
					if (typeCode1 == TypeCode.Byte)
						switch (typeCode2)
						{
							case TypeCode.Char: return true;
							case TypeCode.UInt16: return true;
							case TypeCode.Int16: return true;
							case TypeCode.UInt32: return true;
							case TypeCode.Int32: return true;
							case TypeCode.UInt64: return true;
							case TypeCode.Int64: return true;
							case TypeCode.Single: return true;
							case TypeCode.Double: return true;
							default: return false;
						}
					// Possible conversions from SByte follow.
					if (typeCode1 == TypeCode.SByte)
						switch (typeCode2)
						{
							case TypeCode.Int16: return true;
							case TypeCode.Int32: return true;
							case TypeCode.Int64: return true;
							case TypeCode.Single: return true;
							case TypeCode.Double: return true;
							default: return false;
						}
					// Possible conversions from UInt16 follow.
					if (typeCode1 == TypeCode.UInt16)
						switch (typeCode2)
						{
							case TypeCode.UInt32: return true;
							case TypeCode.Int32: return true;
							case TypeCode.UInt64: return true;
							case TypeCode.Int64: return true;
							case TypeCode.Single: return true;
							case TypeCode.Double: return true;
							default: return false;
						}
					// Possible conversions from Int16 follow.
					if (typeCode1 == TypeCode.Int16)
						switch (typeCode2)
						{
							case TypeCode.Int32: return true;
							case TypeCode.Int64: return true;
							case TypeCode.Single: return true;
							case TypeCode.Double: return true;
							default: return false;
						}
					// Possible conversions from UInt32 follow.
					if (typeCode1 == TypeCode.UInt32)
						switch (typeCode2)
						{
							case TypeCode.UInt64: return true;
							case TypeCode.Int64: return true;
							case TypeCode.Single: return true;
							case TypeCode.Double: return true;
							default: return false;
						}
					// Possible conversions from Int32 follow.
					if (typeCode1 == TypeCode.Int32)
						switch (typeCode2)
						{
							case TypeCode.Int64: return true;
							case TypeCode.Single: return true;
							case TypeCode.Double: return true;
							default: return false;
						}
					// Possible conversions from UInt64 follow.
					if (typeCode1 == TypeCode.UInt64)
						switch (typeCode2)
						{
							case TypeCode.Single: return true;
							case TypeCode.Double: return true;
							default: return false;
						}
					// Possible conversions from Int64 follow.
					if (typeCode1 == TypeCode.Int64)
						switch (typeCode2)
						{
							case TypeCode.Single: return true;
							case TypeCode.Double: return true;
							default: return false;
						}
					// Possible conversions from Single follow.
					if (typeCode1 == TypeCode.Single)
						switch (typeCode2)
						{
							case TypeCode.Double: return true;
							default: return false;
						}
				}
				return false;
			}

		}



	}
}
