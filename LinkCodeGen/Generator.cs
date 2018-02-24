using System;
using System.Collections.Generic;
using System.Text;

namespace LinkCodeGen
{
	public class Generator
	{
		/// <summary>
		/// 添加已经手工生成过代码的类型
		/// </summary>
		/// <param name="types"></param>
		public void AddSkipCreateTypes(IList<string> types)
		{
			CreatorBase.SkipCreateTypes.AddRange(types);
		}

		/// <summary>
		/// 添加不生成代码的命名空间
		/// </summary>
		/// <param name="namespaces"></param>
		public void AddNotCreateNameSpace(IList<string> namespaces)
		{
			CreatorBase.NotCreateNameSpace.AddRange(namespaces);
		}

		/// <summary>
		/// 添加不生成代码类型
		/// </summary>
		/// <param name="namespaces"></param>
		public void AddNotCreateTypes(IList<string> types)
		{
			CreatorBase.NotCreateTypes.AddRange(types);
		}

		/// <summary>
		/// 添加要生成代码的类型
		/// </summary>
		/// <param name="types"></param>
		public void AddTypes(IList<Type> types)
		{
			this.types.AddRange(types);
		}

		List<Type> types;

		public Generator()
		{
			types = new List<Type>();
			CreatorBase.NotCreateNameSpace.Clear();
			CreatorBase.NotCreateTypes.Clear();
			CreatorBase.SkipCreateTypes.Clear();

		}

		public void MakeCode(System.IO.Stream combiedcodestm, string as3apipath, string csharpcodepath, string chsharpcodenamespace,
			string regfunctioncodenamespace, out string regfunctioncode)
		{

			Dictionary<Type, CreatorBase> creators = new Dictionary<Type, CreatorBase>();
			foreach (var item in types)
			{
				var classtype = item;

				if (!CreatorBase.IsSkipType(classtype) && !CreatorBase.IsSkipCreator(classtype)

					&& (classtype.IsClass || classtype.IsValueType) && classtype.IsPublic
					)
				{
					CreatorBase.MakeCreator(classtype, creators, as3apipath, csharpcodepath, chsharpcodenamespace);
				}
			}

			StringBuilder regclassSb = new StringBuilder();

			using (System.IO.StreamWriter sw = new System.IO.StreamWriter(combiedcodestm, System.Text.Encoding.UTF8))
			{
				StringBuilder usingcode = new StringBuilder();
				CreatorBase.GenNativeFuncImport(usingcode);

				sw.WriteLine(usingcode.ToString());

				regclassSb.AppendLine("using System;");
				regclassSb.AppendLine("using System.Collections.Generic;");
				regclassSb.AppendLine("using System.Text;");
				regclassSb.AppendLine("using ASBinCode;");
				//regclassSb.AppendLine("using ASCTest.regNativeFunctions;");
				regclassSb.AppendLine("using ASRuntime.nativefuncs;");

				regclassSb.AppendLine("namespace " + regfunctioncodenamespace);
				regclassSb.AppendLine("{");

				regclassSb.AppendLine("\tpartial class extFunctions : INativeFunctionRegister");
				regclassSb.AppendLine("\t{");
				regclassSb.AppendLine("\t\tprivate void regAutoCreateCodes(CSWC bin)");
				regclassSb.AppendLine("\t\t{");

				foreach (var item in creators.Values)
				{
					Console.WriteLine("building:" + item.BuildIngType);
					string code = item.Create().Replace("\r", "").Replace("\n", "\r\n");


					regclassSb.AppendLine("\t\t\t" + item.linkcodenamespace + "." + CreatorBase.GetNativeFunctionClassName(item.BuildIngType) + ".regNativeFunctions(bin);");
					sw.WriteLine(code);

				}

				regclassSb.AppendLine("\t\t}");
				regclassSb.AppendLine("\t}");
				regclassSb.AppendLine("}");
				regclassSb.AppendLine();
			}

			regfunctioncode = regclassSb.ToString().Replace("\r", "").Replace("\n", "\r\n");
		}



	}
}
