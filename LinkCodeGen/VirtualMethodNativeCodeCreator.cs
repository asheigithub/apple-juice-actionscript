using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LinkCodeGen
{
	class VirtualMethodNativeCodeCreator : MethodNativeCodeCreatorBase
	{
		public VirtualMethodNativeCodeCreator(System.Reflection.MethodInfo method, Type methodAtType,int vmidx,string as3name)
		{
			this.method = method;
			this.methodAtType = methodAtType;
			this.vmidx = vmidx;
			this.classname = as3name;
		}
		int vmidx;

		public string GetCode()
		{
			var paras = method.GetParameters();
			int paracount = paras.Length;



			string code = "";

			string as3functionvariable = "_as3function_" + vmidx;
			string as3functionvariableid = "_as3functionId_" + vmidx;


			



			code += "\t\t\tprivate ASBinCode.rtData.rtFunction " + as3functionvariable + ";\n";
			code += "\t\t\tprivate int " + as3functionvariableid + " =-1;\n";

			code += "\t\t\t";
			code += "public override ";

			if (!method.IsSpecialName)
			{

				if (method.ReturnType == typeof(void))
				{
					code += "void";
				}
				else
				{
					code += GetTypeFullName(method.ReturnType);
				}

				code += " " + method.Name;

				code += "(";
				foreach (var item in paras)
				{
					code += GetTypeFullName(item.ParameterType) + " ";
					code += item.Name;

					if (item != paras[paras.Length - 1])
					{
						code += ",";
					}
				}
				code += ")";
				code += "\n";

			}
			else
			{
				PropertyInfo p;
				MethodNativeCodeCreator.CheckIsGetter(method, methodAtType, out p);

				
				
				code += GetTypeFullName(method.ReturnType);
				code += " ";

				code += p.Name;
				code += "\n\t\t\t{";
				code += "\n\t\t\t\tget";
				code += "\n";
			}

			code += "\t\t\t{\n";


			string prepareAS3Method = @"
				if ([as3functionvariable] == null)
					[as3functionvariable] = (ASBinCode.rtData.rtFunction)player.getMethod(bindAS3Object, ""[as3methodname]"");
				if ([as3functionvariableid] == -1)
				{
					[as3functionvariableid] = ((ClassMethodGetter)typeclass.getBaseLinkSystemClass().classMembers.FindByName(""[as3methodname]"").bindField).functionId;
				}
";

			code += prepareAS3Method.Replace("[as3functionvariable]", as3functionvariable).Replace("[as3methodname]", classname).Replace("[as3functionvariableid]",as3functionvariableid);

			if (!method.IsAbstract)
			{
				string invokesupercondition = @"
				if ([as3functionvariable] != null &&
				(player == null || (player != null && NativeConstParameterFunction.checkToken(
				new NativeConstParameterFunction.ExecuteToken(
				player.ExecuteToken.tokenid, [as3functionvariableid]
				)
				))
				)
				)
";

				code += invokesupercondition.Replace("[as3functionvariable]", as3functionvariable).Replace("[as3functionvariableid]", as3functionvariableid);

				code += "\t\t\t\t{\n";

				if (method.ReturnType != typeof(void))
				{
					code += "\t\t\t\t\treturn ";
				}

				code += "base." + method.Name + "(";

				foreach (var item in paras)
				{
					code += item.Name;
					if (item != paras[paras.Length - 1])
					{
						code += ",";
					}
				}


				code += ");\n";

				code += "\t\t\t\t}\n";

				code += "\t\t\t\telse\n";
			}
			else
			{
				string invokesupercondition = @"
				if ([as3functionvariable] != null &&
					(player == null || (player != null && NativeConstParameterFunction.checkToken(
					new NativeConstParameterFunction.ExecuteToken(
					player.ExecuteToken.tokenid, [as3functionvariableid]
					)
					))
				))
				{
					throw new ASRunTimeException(""不能调用抽象方法"",new Exception());
				}
				else
";
				code += invokesupercondition.Replace("[as3functionvariable]", as3functionvariable).Replace("[as3functionvariableid]", as3functionvariableid);

			}

			code += "\t\t\t\t{\n";


			string invoke = "player.InvokeFunction";
			invoke += "(";
			invoke += as3functionvariable + ",";
			invoke += paracount + ",";

			for (int i = 0; i < 5; i++)
			{
				if (i < paracount)
				{
					invoke += paras[i].Name + ",";
				}
				else
				{
					invoke += "null,";
				}
			}
			if (paracount > 5)
			{
				invoke += "new object[]{";

				for (int i = 5; i < paracount; i++)
				{
					invoke += paras[i].Name;
					if (i < paracount - 1)
					{
						invoke += ",";
					}
				}

				invoke += "}";
				//new object[] { }
			}
			else
			{
				invoke += "null";
			}

			invoke += ");\n";


			if (method.ReturnType != typeof(void))
			{
				code += "\t\t\t\t\treturn (" + GetTypeFullName(method.ReturnType) + ")";
			}
			else
			{
				code += "\t\t\t\t\t";
			}

			code += invoke;


			code += "\t\t\t\t}\n";


			code += "\t\t\t}\n";

			if (method.IsSpecialName)
			{
				code += "\t\t\t}\n";
			}

			return code;

		}
	}

}
