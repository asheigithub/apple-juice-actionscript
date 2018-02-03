using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LinkCodeGen
{
	class MethodNativeCodeCreator : MethodNativeCodeCreatorBase
	{
		



		

		

		public MethodNativeCodeCreator(string classname,System.Reflection.MethodInfo method,Type methodAtType)
		{
			this.classname = classname;
			this.method = method;
			this.methodAtType = methodAtType;
		}






		

		public string GetCode()
		{
			
				var paras = method.GetParameters();

			bool ismakedelegate = true;

			if (CreatorBase.IsDelegate(methodAtType))
			{
				ismakedelegate = false;
			}

			if (method is MethodInfo)
			{
				PropertyInfo propertyInfo;
				if (MethodNativeCodeCreator.CheckIsIndexerSetter((MethodInfo)method, method.DeclaringType, out propertyInfo))
				{
					var temp = paras[0];
					paras[0] = new myparainfo( paras[1],0);
					paras[1] = new myparainfo( temp,1);


					ismakedelegate = false;
				}

				if (MethodNativeCodeCreator.CheckIsIndexerGetter((MethodInfo)method, method.DeclaringType, out propertyInfo))
				{					
					ismakedelegate = false;
				}
				if (MethodNativeCodeCreator.CheckIsSetter((MethodInfo)method, method.DeclaringType, out propertyInfo))
				{
					ismakedelegate = false;
				}
				if (MethodNativeCodeCreator.CheckIsGetter((MethodInfo)method, method.DeclaringType, out propertyInfo))
				{
					ismakedelegate = false;
				}

				if (method.IsSpecialName)
				{
					ismakedelegate = false;
				}

				if (method.IsGenericMethodDefinition)
				{
					ismakedelegate = false;
				}

			}




			int paracount = paras.Length;
			bool hasref = false;
			foreach (var item in paras)
			{
				if (item.ParameterType.IsByRef)
				{
					paracount += 1;
					hasref = true;
					break;
				}
			}

			string funccode;
			if (ismakedelegate)
			{
				funccode = Properties.Resources.IMethodGetterMethodFunc;
			}
			else
			{
				funccode= Properties.Resources.MethodFunc;
			}

			
			

			

				funccode = funccode.Replace("[classname]", classname);
				funccode = funccode.Replace("[paracount]", paracount.ToString());

				string pushparas = string.Empty;

				for (int i = 0; i < paras.Length; i++)
				{
					var para = paras[i];

					pushparas = pushparas + "\t\t\t\t" + string.Format("para.Add({0});\n", GetAS3RuntimeTypeString(para.ParameterType));

				}

			if (hasref)
			{
				pushparas = pushparas + "\t\t\t\t" + string.Format("para.Add({0});\n", GetAS3RuntimeTypeString(typeof(ASRuntime.nativefuncs.linksystem.RefOutStore)));
			}


				funccode = funccode.Replace("[pushparas]", pushparas);
				funccode = funccode.Replace("[returntype]", GetAS3RuntimeTypeString(method.ReturnType));

				if (methodAtType.IsValueType)
				{
					string loadthis = Properties.Resources.LoadStructThis;
					loadthis = loadthis.Replace("[thisObjtype]",GetTypeFullName( methodAtType));
					funccode = funccode.Replace("[loadthis]",loadthis);
				}
				else
				{
					string loadthis = Properties.Resources.LoadThis;
					loadthis = loadthis.Replace("[thisObjtype]", GetTypeFullName(methodAtType));
					funccode = funccode.Replace("[loadthis]", loadthis);
				}


				string loadargs = string.Empty;

				for (int i = 0; i < paras.Length; i++)
				{
					var argement = paras[i];

					loadargs = loadargs + GetLoadArgementString(argement);
					loadargs = loadargs + "\n";


				}
			if (hasref)
			{
				loadargs = loadargs + GetLoadArgementString(typeof(ASRuntime.nativefuncs.linksystem.RefOutStore),paras.Length);
				loadargs = loadargs + "\n";
			}


				funccode = funccode.Replace("[loadargement]", loadargs);

				var rettype = GetAS3Runtimetype(method.ReturnType);
				if (rettype == ASBinCode.RunTimeDataType.fun_void)
				{
					string storeresult = string.Empty;
					//***调用方法****
					storeresult = "_this";
					storeresult = GetInvokeMethodString(storeresult, paras);

					storeresult += "\t\t\t\t\t;\n";
					storeresult = storeresult + "\t\t\t\t\treturnSlot.directSet(ASBinCode.rtData.rtUndefined.undefined);";
					funccode = funccode.Replace("[storeresult]", storeresult);
				}
				else if (rettype == ASBinCode.RunTimeDataType.rt_int)
				{
					string storeresult = string.Empty;
					//***调用方法****
					storeresult = "int _result_ = (int)(_this";
					storeresult = GetInvokeMethodString(storeresult, paras);

					storeresult += "\t\t\t\t\t)\n";
					storeresult += "\t\t\t\t\t;\n";
					storeresult += "\t\t\t\t\treturnSlot.setValue(_result_);\n";
					funccode = funccode.Replace("[storeresult]", storeresult);
				}
				else if (rettype == ASBinCode.RunTimeDataType.rt_uint)
				{
					string storeresult = string.Empty;
					//***调用方法****
					storeresult = "uint _result_ = (uint)(_this";
					storeresult = GetInvokeMethodString(storeresult, paras);

					storeresult += "\t\t\t\t\t)\n";
					storeresult += "\t\t\t\t\t;\n";
					storeresult += "\t\t\t\t\treturnSlot.setValue(_result_);\n";
					funccode = funccode.Replace("[storeresult]", storeresult);
				}
				else if (rettype == ASBinCode.RunTimeDataType.rt_number)
				{
					string storeresult = string.Empty;
					//***调用方法****
					storeresult = "double _result_ = (double)(_this";
					storeresult = GetInvokeMethodString(storeresult, paras);

					storeresult += "\t\t\t\t\t)\n";
					storeresult += "\t\t\t\t\t;\n";
					storeresult += "\t\t\t\t\treturnSlot.setValue(_result_);\n";
					funccode = funccode.Replace("[storeresult]", storeresult);
				}
				else if (rettype == ASBinCode.RunTimeDataType.rt_string)
				{
					string storeresult = string.Empty;
					//***调用方法****
					storeresult = "string _result_ = (string)(_this";
					storeresult = GetInvokeMethodString(storeresult, paras);

					storeresult += "\t\t\t\t\t)\n";
					storeresult += "\t\t\t\t\t;\n";
					storeresult += "\t\t\t\t\treturnSlot.setValue(_result_);\n";
					funccode = funccode.Replace("[storeresult]", storeresult);
				}
				else if (rettype == ASBinCode.RunTimeDataType.rt_boolean)
				{
					string storeresult = string.Empty;
					//***调用方法****
					storeresult = "bool _result_ = _this";
					storeresult = GetInvokeMethodString(storeresult, paras);

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
					if (method.ReturnType.IsValueType)
					{
						string storeresult = string.Empty;
						//***调用方法****
						storeresult = GetTypeFullName( method.ReturnType) + " _result_ = _this";
						storeresult = GetInvokeMethodString(storeresult, paras);

						storeresult += "\t\t\t\t\t;\n";
						//storeresult += "\t\t\t\t\tstackframe.player.linktypemapper.storeLinkObject_ToSlot(_result_, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);";
						storeresult += "\t\t\t\t\t((StackSlot)returnSlot).setLinkObjectValue(bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, _result_);";

						funccode = funccode.Replace("[storeresult]", storeresult);
					}
					else
					{
						string storeresult = string.Empty;
						//***调用方法****
						storeresult = "object _result_ = _this";
						storeresult = GetInvokeMethodString(storeresult, paras);

						storeresult += "\t\t\t\t\t;\n";
						storeresult += "\t\t\t\t\tstackframe.player.linktypemapper.storeLinkObject_ToSlot(_result_, functionDefine.signature.returnType, returnSlot, bin, stackframe.player);";
						funccode = funccode.Replace("[storeresult]", storeresult);

					}
				}
				else
				{
					funccode = funccode.Replace("[storeresult]", "代码生成错误，不能转换返回类型");
				}

			if (!hasref)
			{
				funccode = funccode.Replace("[storeref]", string.Empty);
			}
			else
			{
				string storetemplate = @"
					if (arg[storeidx] != null)
					{
						arg[storeidx].SetValue(functionDefine.signature.parameters[[argidx]].name, arg[argidx]);
					}
";

				string toreplace = @"
					if (arg[storeidx] != null)
					{
						arg[storeidx].Clear();
					}
"; ;
				toreplace = toreplace.Replace("[storeidx]", paras.Length.ToString());


				for (int i = 0; i < paras.Length; i++)
				{
					if (paras[i].ParameterType.IsByRef)
					{
						toreplace += storetemplate.Replace("[storeidx]",paras.Length.ToString()).Replace("[argidx]",i.ToString());
					}
				}

				funccode = funccode.Replace("[storeref]",toreplace);
			}


			if (ismakedelegate)
			{
				//typeof(AutoGenCodeLib.Testobj).GetMethod("TestType");

				string types = "Type.EmptyTypes";

				if (paras.Length > 0)
				{
					types = "new Type[] {";

					for (int i = 0; i < paras.Length; i++)
					{
						types += "typeof(" + GetTypeFullName(paras[i].ParameterType) + ")";
						if (i < paras.Length - 1)
						{
							types += ",";
						}
					}

					types += "}";

				}


				string findmethod = string.Empty;
				findmethod += "typeof("+ GetTypeFullName(methodAtType) +")";
				findmethod += ".GetMethod(\""+method.Name+"\","+types+");";


				funccode = funccode.Replace("[createmethod]", findmethod);

			}



			return funccode;
			
		}

	}
}
