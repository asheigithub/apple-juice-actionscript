using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMXMLCCLI
{
	class Program
	{
		static void Main(string[] args)
		{
			bool argloaded = false;
			bool receiveo = false;

			string outputfile = null;

			List<string> srcpathList = new List<string>();


			foreach (var arg in args)
			{
				//Console.WriteLine(arg);
				if (arg.StartsWith("-load-config+=obj"))
				{
					string configpath = arg.Substring(14);

					System.Xml.Linq.XDocument document = System.Xml.Linq.XDocument.Load(configpath);
					//***读取源代码类路径***

					var srcpath = document.Descendants("source-path").First().Descendants("path-element").Select((o)=>o.Value).ToArray();

					foreach (var p in srcpath)
					{
						if (!p.Replace('\\','/').EndsWith("FlashDevelop/Library/AS3/classes")
							
							//&&
							//!p.Replace('\\', '/').EndsWith("/as3_commapi/api")
							//&&
							//!p.Replace('\\', '/').EndsWith("/as3_commapi/sharpapi")
							//&&
							//!p.Replace('\\', '/').EndsWith("/as3_unityapi")
							)
						{
							srcpathList.Add(p);
						}
					}



					argloaded = true;

				}
				else if (arg == "-o")
				{
					receiveo = true;
				}
				else if (receiveo)
				{
					receiveo = false;
					outputfile = arg;
				}
				
			}


			if (!argloaded)
			{
				Console.Error.WriteLine("没有找到配置文件");

				Environment.Exit(1);

				return ;
			}

			string libcswc;

			if (outputfile == null)
			{
				Console.Error.WriteLine("没有配置输出文件");
				Environment.Exit(1);

				return;
			}

			//***输出文件在obj下面，所以libcswc就在lib下面***
			libcswc =
				System.IO.Path.GetDirectoryName(
					System.IO.Path.GetDirectoryName(System.IO.Path.GetFullPath(outputfile))
				) + "/lib/as3unitylib.cswc";

			HashSet<string> inlibclass = new HashSet<string>();
			{
				byte[] bin = System.IO.File.ReadAllBytes(libcswc);
				var library = ASBinCode.CSWC.loadFromBytes(bin);
				foreach (var item in library.classes)
				{
					if (item.instanceClass == null)
					{
						if (string.IsNullOrEmpty(item.package))
						{
							inlibclass.Add(item.name + ".as");
						}
						else
						{
							inlibclass.Add((item.package.Replace('.','/') + "/" + item.name + ".as"));
						}
					}
				}
			}

			ASTool.Grammar grammar = ASCompiler.Grammar.getGrammar();
			string[] files = null;
			List<string> loadedfiles = new List<string>();
			Dictionary<string, string> srcFileProjFile = new Dictionary<string, string>();

			foreach (var path_ in srcpathList)
			{
				

				string path = path_;
				path = path.Replace('\\', '/');
				string[] ps = path.Split('/');

				if (System.IO.Directory.Exists(path))
				{
					files = System.IO.Directory.GetFiles(path, "*.as", System.IO.SearchOption.AllDirectories);

					foreach (var item in files)
					{
						
						string projfile = item.Replace("\\", "/").Replace(path.Replace("\\", "/"), "");
						if (projfile.StartsWith("/"))
							projfile = projfile.Substring(1);

						if (!inlibclass.Contains(projfile))
						{
							loadedfiles.Add(item);
							srcFileProjFile.Add(item, projfile);

							Console.WriteLine("load file: " + projfile);

						}
						
					}
				}
			}


			Dictionary<string, string> fileFullPath = new Dictionary<string, string>();
			foreach (var item in srcFileProjFile)
			{
				if (!fileFullPath.ContainsKey(item.Value))
				{
					fileFullPath.Add(item.Value, item.Key);
				}
				else
				{
					Console.Error.WriteLine(item.Key + ":" + 1 + ":Error: " + "同名文件重复编译");
					Environment.Exit(1);

					return;
				}
			}


			files = loadedfiles.ToArray();

			var proj = new ASTool.AS3.AS3Proj();
			var srcout = new ASTool.ConSrcOut();

			for (int i = 0; i < files.Length; i++)
			{
				grammar.hasError = false;
				string teststring = System.IO.File.ReadAllText(files[i]);
				if (string.IsNullOrEmpty(teststring))
				{
					continue;
				}

				var tree = grammar.ParseTree(teststring, ASTool.AS3LexKeywords.LEXKEYWORDS,
							ASTool.AS3LexKeywords.LEXSKIPBLANKWORDS, 
							srcFileProjFile[files[i]],
							files[i].Replace('\\','/')
							
							);

				//System.IO.File.WriteAllText("d:\\" + System.IO.Path.GetFileName(files[i]), tree.GetTreeString());

				if (grammar.hasError)
				{

					//Console.WriteLine(files[i]);
					//Console.WriteLine("解析语法树失败!");

					Environment.Exit(1);

					return;
				}
				var analyser = new ASTool.AS3FileGrammarAnalyser(proj, srcFileProjFile[files[i]]);
				if (!analyser.Analyse(tree)) //生成项目的语法树
				{
					Console.WriteLine(analyser.err.ToString());
					//Console.WriteLine("语义分析失败!");

					Environment.Exit(1);

					return;
				}

			}
			
			ASCompiler.compiler.Builder builder = new ASCompiler.compiler.Builder();
			
			builder.LoadLibrary(System.IO.File.ReadAllBytes(libcswc));

			builder.options.CheckNativeFunctionSignature = false;
			builder.options.isConsoleOut = false;
			builder.Build(proj, null);

			if (builder.buildErrors.Count == 0)
			{
				ASBinCode.CSWC swc = builder.getBuildOutSWC();
				byte[] bin = swc.toBytes();

				//string as3libfile = @"F:\AS3Hotfix_Unity\AS3Hotfix_U56\Assets\StreamingAssets\hotfix.cswc";

				System.IO.File.WriteAllBytes(
					//as3libfile
					outputfile
					, 
					bin);

				Console.WriteLine("写入文件" + 
					//as3libfile 
					outputfile
					+ " 共" + bin.Length + "字节" );

				
				return;
			}
			else
			{
				foreach (var err in builder.buildErrors)
				{

					//Console.Error.WriteLine("file :" + err.srcFile);
					//Console.Error.WriteLine("line :" + (err.line+1) + " ptr :" + (err.ptr+1));
					//Console.Error.WriteLine(err.errorMsg);
					Console.Error.WriteLine(fileFullPath[ err.srcFile] + ":" + (err.line + 1) + ":Error: " + err.errorMsg);
					//input = input.Replace(vbCrLf, vbLf)
					//input = input.Replace(vbCr, vbLf)
					//Dim lines = input.Split(vbLf)
					Console.Error.WriteLine();
				}



				Environment.Exit(1);

				return;
			}
			
		}
	}
}
