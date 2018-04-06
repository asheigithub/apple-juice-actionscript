using System;
using System.Collections.Generic;
using System.Text;



namespace ASCTest
{
    class Program
    {
        

        static void Main(string[] args)
        {
			//{
			//	ASRuntime.Player player = new ASRuntime.Player();

			//	byte[] bytecode = System.IO.File.ReadAllBytes("../Debug/as3unitylib.cswc");
			//	ASBinCode.CSWC swc2 = ASBinCode.CSWC.loadFromBytes(bytecode);
			//	ASRuntime.nativefuncs.BuildInFunctionLoader.loadBuildInFunctions(swc2);
			//	(new extFunctions()).registrationFunction(swc2);

			//	player.loadCode(swc2);

			//	player.run(null);
			//	return;
			//}




			//ASCompiler.compiler.Builder bu = new ASCompiler.compiler.Builder();
			//byte[] b = bu.BuildLibBin();
			//System.IO.File.WriteAllBytes("astoolglobal.swc", b);
			ASTool.Grammar grammar = ASCompiler.Grammar.getGrammar();

            //string teststring = "package{}var a:String = \"first\";var b:String = \"First\"; var c=a==b;";
            string teststring = "package{}";//System.IO.File.ReadAllText("../../testScript/AS3Testproj/src/Main.as");

            string[] files =null;

			Dictionary<string, string> srcFileProjFile = new Dictionary<string, string>();
            
            if (args.Length > 0)
            {
                string path = args[0]; //path = @"F:\ASTool\ASCTest\bin\Release\tests\2_managed_array\";
									   //path = @"F:\ASTool\ASCTest\testScript\AS3Testproj\src\";
									   //path = @"E:\Manju-pc\as3protobuf\AS3ProtoBuf\src";
									   //path = @"E:\Manju-pc\as3protobuf\AS3ProtoBuf\protobuflib";
									   //path = @"../..\testScript\AS3Testproj\amd";
				path = @"../..\testScript\AS3Testproj\src";



				if (path.EndsWith(".as"))
                {
                    path = System.IO.Path.GetDirectoryName(path);
                }

                if (string.IsNullOrEmpty(path))
                {
                    path=".\\";
                }

				//path = "";
				//files =new string[] { "E:/Manju-pc/as3protobuf/AS3ProtoBuf/src/com/netease/protobuf/Message.as" };

				path=path.Replace('\\', '/');
                string[] ps = path.Split('/');
                if (ps.Length == 2 && string.IsNullOrEmpty(ps[1])  && ps[0].IndexOf( System.IO.Path.VolumeSeparatorChar)>0)
                {
                    Console.WriteLine("无法在根目录下搜索.请将as源代码放到一个文件夹内");
                    return;
                }
                else if (System.IO.Directory.Exists(path))
                {
                    //Console.WriteLine(path);
                    //teststring = System.IO.File.ReadAllText(args[0]);
                    files = System.IO.Directory.GetFiles(path, "*.as", System.IO.SearchOption.AllDirectories );

					foreach (var item in files)
					{
						string projfile = item.Replace("\\", "/").Replace(path.Replace("\\","/"), "");
						if (projfile.StartsWith("/"))
							projfile = projfile.Substring(1);
						srcFileProjFile.Add(item, projfile);
					}

                }
            }
            else
            {
                Console.Write("输入as文件所在路径");
                return;
            }

            if (files == null)
            {
                Console.Write("输入as文件所在路径");
                return;
            }


			//*********加入API*****
			{
				string apidir = @"../../..\LinkCodeGenCLI\bin\Debug\as3api";
				if (System.IO.Directory.Exists(apidir))
				{
					var linkapi = System.IO.Directory.GetFiles(apidir, "*.as", System.IO.SearchOption.AllDirectories);

					foreach (var item in linkapi)
					{
						string projfile = item.Replace("\\", "/").Replace(apidir.Replace("\\", "/"), "");
						if (projfile.StartsWith("/"))
							projfile = projfile.Substring(1);
						srcFileProjFile.Add(item, projfile);
					}

					string[] n = new string[files.Length + linkapi.Length];
					linkapi.CopyTo(n, 0);
					files.CopyTo(n, linkapi.Length);
					files = n;
				}
			}
			{
				string apidir = @"..\..\..\as3_commapi\sharpapi";
				if (System.IO.Directory.Exists(apidir))
				{
					var linkapi = System.IO.Directory.GetFiles(apidir, "*.as", System.IO.SearchOption.AllDirectories);

					foreach (var item in linkapi)
					{
						string projfile = item.Replace("\\", "/").Replace(apidir.Replace("\\", "/"), "");
						if (projfile.StartsWith("/"))
							projfile = projfile.Substring(1);
						srcFileProjFile.Add(item, projfile);
					}

					string[] n = new string[files.Length + linkapi.Length];
					linkapi.CopyTo(n, 0);
					files.CopyTo(n, linkapi.Length);
					files = n;
				}
			}
			//*********************

			//*********加入ProtoBuf API*****
			//string apidir = @"E:\Manju-pc\as3protobuf\AS3ProtoBuf\protobuflib";
			//if (System.IO.Directory.Exists(apidir))
			//{
			//	var linkapi = System.IO.Directory.GetFiles(apidir, "*.as", System.IO.SearchOption.AllDirectories);
			//	foreach (var item in linkapi)
			//	{
			//		string projfile = item.Replace("\\", "/").Replace(apidir.Replace("\\", "/"), "");
			//		if (projfile.StartsWith("/"))
			//			projfile = projfile.Substring(1);
			//		srcFileProjFile.Add(item, projfile);
			//	}


			//	string[] n = new string[files.Length + linkapi.Length];
			//	linkapi.CopyTo(n, 0);
			//	files.CopyTo(n, linkapi.Length);
			//	files = n;
			//}
			//*********************

			var proj = new ASTool.AS3.AS3Proj();
            var srcout = new ASTool.ConSrcOut();

            for (int i = 0; i < files.Length; i++)
            {
                grammar.hasError = false;
                teststring = System.IO.File.ReadAllText(files[i]);
                if (string.IsNullOrEmpty(teststring))
                {
                    continue;
                }
				


				var tree = grammar.ParseTree(teststring, ASTool.AS3LexKeywords.LEXKEYWORDS , 
                            ASTool.AS3LexKeywords.LEXSKIPBLANKWORDS  , srcFileProjFile[files[i]]);

                //System.IO.File.WriteAllText("d:\\" + System.IO.Path.GetFileName(files[i]), tree.GetTreeString());

                if (grammar.hasError)
                {
                    Console.WriteLine(files[i]);
                    Console.WriteLine("解析语法树失败!");
                    Console.ReadLine();
                    return;
                }

                

                var analyser = new ASTool.AS3FileGrammarAnalyser(proj, srcFileProjFile[files[i]]);
                if (!analyser.Analyse( tree)) //生成项目的语法树
                {
                    Console.WriteLine(analyser.err.ToString());
                    Console.WriteLine("语义分析失败!");
                    Console.ReadLine();
                    return;
                }
#if DEBUG
                //Console.Clear();
#endif
            }

#if DEBUG

            Console.WriteLine();
            Console.WriteLine("====语法树翻译====");

            //runtimeCompiler rtLoader = new runtimeCompiler();
            foreach (var p in proj.SrcFiles)
            {
				p.Write(0, srcout);
            }
            
#endif
            //Console.Read();
            //return;
            ASCompiler.compiler.Builder builder = new ASCompiler.compiler.Builder();
			//builder.LoadLibrary( System.IO.File.ReadAllBytes("as3protobuf.swc") );

			//builder.LoadLibrary(System.IO.File.ReadAllBytes("F:/ASTool/LinkCodeGenCLI/bin/Debug/as3unitylib.cswc"));

			//builder.LoadLibrary(System.IO.File.ReadAllBytes("astoolglobal.swc"));
			//builder.Build(proj, new ASBinCode.INativeFunctionRegister[] { new extFunctions() } );

			builder.options.CheckNativeFunctionSignature = false;
			builder.Build(proj,null);

			if (builder.buildErrors.Count == 0)
            {
                ASBinCode.CSWC swc = builder.getBuildOutSWC();

				byte[] bin = swc.toBytes();


				
				swc = ASBinCode.CSWC.loadFromBytes(bin);
				ASRuntime.nativefuncs.BuildInFunctionLoader.loadBuildInFunctions(swc);
				(new extFunctions()).registrationAllFunction(swc);
				
				//System.IO.File.WriteAllBytes("astoolglobal.swc", swc.toBytes());
				//System.IO.File.WriteAllBytes("as3protobuf.swc", swc.toBytes());
				//System.IO.File.WriteAllBytes("as3test.cswc", swc.toBytes());
				//System.IO.File.WriteAllBytes("as3unitylib.cswc", swc.toBytes());

				if (swc != null)
                {
#if DEBUG

					for (int i = 0; i < swc.blocks.Count; i++)
                    {
                        var block = swc.blocks[i];
						if (block != null && block.name.EndsWith("::Main"))// "CRC32::update"))
                        {
                            Console.WriteLine();
                            Console.WriteLine("====操作指令 block " + block.name + " " + block.id + "====");
                            Console.WriteLine();
                            Console.WriteLine("total registers:" + block.totalStackSlots);
                            Console.WriteLine(block.GetInstruction());
                        }
                    }

#endif

                    if (swc.blocks.Count > 0)
                    {
						
						ASRuntime.Player player = new ASRuntime.Player();						
						player.loadCode(swc);

						//byte[] bytecode = System.IO.File.ReadAllBytes("as3test.cswc");
						//ASBinCode.CSWC swc2 = ASBinCode.CSWC.loadFromBytes(bytecode);
						//ASRuntime.nativefuncs.BuildInFunctionLoader.loadBuildInFunctions(swc2);

						//player.loadCode(swc2);



						//var d = player.createInstance("SProtoSpace.group_area_info");
						//uint len = (uint)player.getMemberValue(d, "groupids.length");
						//player.setMemberValue(d, "groupids.length", 3);
						//player.setMemberValue(d, "areaGroupName", null);

						//for (int i = 0; i < 3; i++)
						//{
						//	player.setMemberValue(d, "groupids", i + 5, i);
						//}

						////var d = player.createInstance("SProtoSpace.role_base_info");
						//ASRuntime.flash.utils.ByteArray array;
						//var byteArray = player.createByteArrayObject(out array);
						////player.setMemberValue(d, "groupName", "账号你二大爷");



						//var r = player.invokeMethod(d, "writeTo", byteArray);
						//var d2 = player.createInstance("SProtoSpace.group_area_info");

						//player.setMemberValue(byteArray, "position", 0);
						//var k = player.invokeMethod(d2, "mergeFrom", byteArray);
						//var m = player.getMemberValue(d2, "groupids.length");

						//var ts = player.invokeMethod(byteArray, "toString");

						//var messageUnion = player.getMemberValue("SProtoSpace.base_msg_id", "name_check_ack_id");

						//try
						//{
						//	player.setMemberValue("SProtoSpace.base_msg_id", "name_check_ack_id", 5);
						//}
						//catch (ASBinCode.ASRunTimeException e)
						//{
						//	Console.WriteLine(e.ToString());
						//}

						//var s = player.invokeMethod("Test", "TTT", 3, 4);

						//***zip***
						//ASRuntime.flash.utils.ByteArray array;
						//var byteArray = player.createByteArrayObject(out array);

						//////var bytes = System.IO.File.ReadAllBytes(@"F:/code/Protobuf-as3-ILRuntime-master.zip");
						//var bytes = System.IO.File.ReadAllBytes(@"F:/3STOOGES.zip");
						//array.writeBytes(bytes, 0, bytes.Length);
						//array.position = 0;

						//player.invokeMethod("Main", "showzip", byteArray);
						////var by = player.invokeMethod("Main", "saveZip", byteArray);
						////System.IO.File.WriteAllBytes("e:/kkk.zip", array.ToArray());


						Console.WriteLine();
						Console.WriteLine("====程序输出====");

						player.run(null);



					}
                    Console.WriteLine();
                }

            }

#if DEBUG
            Console.WriteLine("按任意键结束");
            Console.ReadLine();
#endif

        }


		



    }
}
