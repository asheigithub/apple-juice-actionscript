using System;
using System.Collections.Generic;
using System.Text;

namespace LinkCodeGen
{
	class StaticFieldGetterNativeCodeCreator : NativeCodeCreatorBase
	{
		private string classname;
		private System.Reflection.FieldInfo field;
		private Type type;

		public StaticFieldGetterNativeCodeCreator(string classname,Type classtype, System.Reflection.FieldInfo field)
		{
			this.classname = classname;
			this.type = classtype;
			this.field = field;
		}

		public string GetCode()
		{
			
			string funccode = Properties.Resources.StaticFieldGetter;

			funccode = funccode.Replace("[classname]", classname);
			funccode = funccode.Replace("[returntype]", GetAS3RuntimeTypeString(field.FieldType));
			string pushparas = string.Empty;

			string getter = string.Empty;

			var rettype = GetAS3Runtimetype(field.FieldType);
			if (rettype == ASBinCode.RunTimeDataType.fun_void)
			{
				string storeresult = string.Empty;
				//***调用方法****
				storeresult += "\t\t\t\t\t;\n";
				storeresult = storeresult + "\t\t\t\t\treturnSlot.directSet(ASBinCode.rtData.rtUndefined.undefined);";
				funccode = funccode.Replace("[storeresult]", storeresult);
			}
			else if (rettype == ASBinCode.RunTimeDataType.rt_int)
			{
				string storeresult = string.Empty;
				//***调用方法****
				storeresult = "int _result_ = (int)("+ NativeCodeCreatorBase.GetTypeFullName(type) + "." + field.Name ;
				
				storeresult += "\t\t\t\t\t)\n";
				storeresult += "\t\t\t\t\t;\n";
				storeresult += "\t\t\t\t\treturnSlot.setValue(_result_);\n";
				funccode = funccode.Replace("[storeresult]", storeresult);
			}
			else if (rettype == ASBinCode.RunTimeDataType.rt_uint)
			{
				string storeresult = string.Empty;
				//***调用方法****
				storeresult = "uint _result_ = (uint)("+ NativeCodeCreatorBase.GetTypeFullName(type) + "." + field.Name;
				
				storeresult += "\t\t\t\t\t)\n";
				storeresult += "\t\t\t\t\t;\n";
				storeresult += "\t\t\t\t\treturnSlot.setValue(_result_);\n";
				funccode = funccode.Replace("[storeresult]", storeresult);
			}
			else if (rettype == ASBinCode.RunTimeDataType.rt_number)
			{
				string storeresult = string.Empty;
				//***调用方法****
				storeresult = "double _result_ = (double)("+ NativeCodeCreatorBase.GetTypeFullName(type) + "." + field.Name ;
				
				storeresult += "\t\t\t\t\t)\n";
				storeresult += "\t\t\t\t\t;\n";
				storeresult += "\t\t\t\t\treturnSlot.setValue(_result_);\n";
				funccode = funccode.Replace("[storeresult]", storeresult);
			}
			else if (rettype == ASBinCode.RunTimeDataType.rt_string)
			{
				string storeresult = string.Empty;
				//***调用方法****
				storeresult = "string _result_ = (string)("+ NativeCodeCreatorBase.GetTypeFullName(type) + "." + field.Name;
				
				storeresult += "\t\t\t\t\t)\n";
				storeresult += "\t\t\t\t\t;\n";
				storeresult += "\t\t\t\t\treturnSlot.setValue(_result_);\n";
				funccode = funccode.Replace("[storeresult]", storeresult);
			}
			else if (rettype == ASBinCode.RunTimeDataType.rt_boolean)
			{
				string storeresult = string.Empty;
				//***调用方法****
				storeresult = "bool _result_ = (bool)"+ NativeCodeCreatorBase.GetTypeFullName(type) + "." + field.Name;
				
				storeresult += "\t\t\t\t\t;\n";
				storeresult += "\t\t\t\t\tif(_result_)\n";
				storeresult += "\t\t\t\t\t{\n";
				storeresult += "\t\t\t\t\t\treturnSlot.setValue(ASBinCode.rtData.rtBoolean.True);\n";
				storeresult += "\t\t\t\t\t}\n";
				storeresult += "\t\t\t\t\telse\n";
				storeresult += "\t\t\t\t\t{\n";
				storeresult += "\t\t\t\t\t\treturnSlot.setValue(ASBinCode.rtData.rtBoolean.False);\n";
				storeresult += "\t\t\t\t\t}\n";


				funccode = funccode.Replace("[storeresult]", storeresult);
			}
			else if (rettype > ASBinCode.RunTimeDataType.unknown)
			{
				if (field.FieldType.IsValueType)
				{
					string storeresult = string.Empty;
					//***调用方法****
					storeresult = GetTypeFullName( field.FieldType) + " _result_ = "+ NativeCodeCreatorBase.GetTypeFullName(type) + "." + field.Name;
					
					storeresult += "\t\t\t\t\t;\n";
					//storeresult += "\t\t\t\t\tstackframe.player.linktypemapper.storeLinkObject_ToSlot(_result_, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);";
					storeresult += "\t\t\t\t\t((StackSlot)returnSlot).setLinkObjectValue(bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, _result_);";

					funccode = funccode.Replace("[storeresult]", storeresult);
				}
				else
				{
					string storeresult = string.Empty;
					//***调用方法****
					storeresult = "object _result_ = " + NativeCodeCreatorBase.GetTypeFullName(type) + "." + field.Name;
					
					storeresult += "\t\t\t\t\t;\n";
					storeresult += "\t\t\t\t\tstackframe.player.linktypemapper.storeLinkObject_ToSlot(_result_, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);";
					funccode = funccode.Replace("[storeresult]", storeresult);

				}
			}
			else
			{
				funccode = funccode.Replace("[storeresult]", "代码生成错误，不能转换返回类型");
			}


			

			return funccode;

		}



	}
}
