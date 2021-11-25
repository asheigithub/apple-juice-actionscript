using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class ASRuntimeMenus
{
	static ASRuntimeSettings LoadOrCreateSetting()
	{
		var settings = AssetDatabase.LoadAssetAtPath("Assets/ASRuntimePlayer/settings.asset", typeof(ASRuntimeSettings));
		if (settings == null)
		{
			settings = ScriptableObject.CreateInstance(typeof(ASRuntimeSettings));

			if (settings == null)
			{
				Debug.LogError("make settings failed");
			}

			AssetDatabase.CreateAsset(settings, "Assets/ASRuntimePlayer/settings.asset");
			AssetDatabase.SaveAssets();
		}
		return (ASRuntimeSettings)settings;
	}

	static void SaveSetting(ASRuntimeSettings settings)
	{
		EditorUtility.SetDirty(settings);
		AssetDatabase.SaveAssets();
	}

	static bool VerifySDK(string sdkpath)
	{
		if (System.IO.Directory.Exists(sdkpath))
		{
			if (System.IO.File.Exists(sdkpath + "/air-sdk-description.xml")
				&&
				System.IO.File.Exists(sdkpath + "/bin/CMXMLCCLI.exe")
				&&
				System.IO.File.Exists(sdkpath + "/unity/ASRuntimeUnityPluginTool.exe")
				)
			{
				return true;
			}
		}

		return false;
	}

	[MenuItem("ASRuntime/Set up the ASRuntime SDK")]
	static void SetUpSDK()
	{
		var settings = LoadOrCreateSetting();
		if (!EditorUtility.DisplayDialog("Set up SDK", "Set the path to the ASRuntime SDK.", "Browse", "Cancel"))
		{
			return;
		}
		string sdkpath = UnityEditor.EditorUtility.OpenFolderPanel("Set up SDK", System.IO.Path.GetDirectoryName(Application.dataPath), "SDK");
		if (!VerifySDK(sdkpath))
		{
			EditorUtility.DisplayDialog("Set up SDK", @"Incorrect SDK path. Please select the correct path,The latest SDK can be downloaded here: https://github.com/asheigithub/apple-juice-actionscript", "OK");
			return;
		}
		settings.SDKPath = sdkpath;
		SaveSetting(settings);
	}


	[MenuItem("ASRuntime/Create ActionScript3 FlashDevelop HotFixProject")]
	static void MakeHotFixProject()
	{
		//***检测Unity安装目录***
		string testlocation = typeof(UnityEditorInternal.AssetStore).Assembly.Location;
		string dir = System.IO.Path.GetDirectoryName(testlocation).Replace('\\', '/');
		string unityInstallEditorDir = string.Empty;
		int pathidx = dir.IndexOf("Editor/Data/Managed");
		if (pathidx>0)
		{
			dir = System.IO.Path.GetDirectoryName( dir.Substring(0,pathidx + 11) );

			if (System.IO.File.Exists(dir + "/Unity.exe"))
			{
				unityInstallEditorDir = dir;
			}
		}

		ASRuntimeSettings settings = LoadOrCreateSetting();

		string unityInstallPath = System.IO.Path.GetDirectoryName(unityInstallEditorDir);

		//if (!EditorUtility.DisplayDialog("Set up SDK", "Set the path to the ASRuntime SDK.", "Browse", "Cancel"))
		//{
		//	return;
		//}

		//string sdkpath = UnityEditor.EditorUtility.OpenFolderPanel("Set up SDK", System.IO.Path.GetDirectoryName(Application.dataPath), "SDK");
		//if (System.IO.Directory.Exists(sdkpath))
		//{
		//	if (System.IO.File.Exists(sdkpath + "/air-sdk-description.xml")
		//		&&
		//		System.IO.File.Exists(sdkpath + "/bin/CMXMLCCLI.exe")
		//		&&
		//		System.IO.File.Exists(sdkpath + "/unity/ASRuntimeUnityPluginTool.exe")
		//		)
		//	{
		//		goto lblsdkselectok;
		//	}
		//}

		//EditorUtility.DisplayDialog("Set up SDK", @"Incorrect SDK path. Please select the correct path,The latest SDK can be downloaded here: https://github.com/asheigithub/apple-juice-actionscript", "OK");

		//return;
		//lblsdkselectok:

		string sdkpath = settings.SDKPath;
		if (!VerifySDK(sdkpath))
		{
			SetUpSDK();

			sdkpath = settings.SDKPath;
			if (!VerifySDK(sdkpath))
			{
				return;
			}
		}


		settings.SDKPath = sdkpath;


		//Debug.Log(unityInstallEditorDir);


		//Debug.Log(Application.dataPath);
		//string sdkpath = string.Empty;
		//{
		//	System.Diagnostics.ProcessStartInfo myProcessStartInfo = new System.Diagnostics.ProcessStartInfo(Application.dataPath + "/ASRuntimePlayer/Editor/ASRuntimeUnityPluginTool.exe");

		//	myProcessStartInfo.UseShellExecute = false;
		//	myProcessStartInfo.RedirectStandardOutput = true;

		//	using (System.Diagnostics.Process process = new System.Diagnostics.Process())
		//	{
		//		process.StartInfo = myProcessStartInfo;
		//		process.Start();

		//		process.WaitForExit();
		//		if (process.ExitCode != 0)
		//		{
		//			Debug.LogWarning("没有选择SDK目录");
		//			return;
		//		}

		//		sdkpath = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(process.StandardOutput.ReadLine()));
		//	}
		//}

		//***创建项目***
		string as3projpath;
		{
			string arg = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(unityInstallEditorDir))
					+ "," +
					System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(System.IO.Path.GetDirectoryName(Application.dataPath)))
					+ "," +
					System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(sdkpath));


			System.Diagnostics.ProcessStartInfo myProcessStartInfo =
				new System.Diagnostics.ProcessStartInfo(
					sdkpath + "/unity/ASRuntimeUnityPluginTool.exe",
					arg)
					;

			myProcessStartInfo.UseShellExecute = false;
			myProcessStartInfo.RedirectStandardOutput = true;

			string as3projfile;
			using (System.Diagnostics.Process process = new System.Diagnostics.Process())
			{
				process.StartInfo = myProcessStartInfo;
				process.Start();

				process.WaitForExit();
				if (process.ExitCode != 0)
				{
					Debug.LogWarning("取消创建项目。");
					return;
				}

				as3projfile = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(process.StandardOutput.ReadLine()));
			}


			string templatepath = sdkpath + "/templates/flashdevelop/hotfixprojtemplate/";
			if (!System.IO.Directory.Exists(templatepath))
			{
				Debug.LogWarning("SDK中没有找到项目模板");
				return;
			}

			string unityprojectpath = System.IO.Path.GetDirectoryName(Application.dataPath);
			string as3projname = System.IO.Path.GetFileNameWithoutExtension(as3projfile);

			as3projpath = System.IO.Path.GetDirectoryName(as3projfile);
			if (!System.IO.Directory.Exists(as3projpath))
			{
				System.IO.Directory.CreateDirectory(as3projpath);
			}

			settings.AS3ProjectPath = as3projpath;

			SaveSetting(settings);

			//***开始创建项目****
			System.IO.Directory.CreateDirectory(as3projpath + "/as3_unity");
			System.IO.Directory.CreateDirectory(as3projpath + "/bin");
			System.IO.Directory.CreateDirectory(as3projpath + "/lib");
			System.IO.Directory.CreateDirectory(as3projpath + "/obj");
			System.IO.Directory.CreateDirectory(as3projpath + "/bat");
			System.IO.Directory.CreateDirectory(as3projpath + "/src");
			{
				string Main = System.IO.File.ReadAllText(sdkpath + "/templates/flashdevelop/hotfixprojtemplate/src/Main.as");
				System.IO.File.WriteAllText(as3projpath + "/src/Main.as", Main);
			}

			{
				string CreateUnityAPI = System.IO.File.ReadAllText(sdkpath + "/templates/flashdevelop/hotfixprojtemplate/bat/CreateUnityAPI.bat");
				CreateUnityAPI = CreateUnityAPI.Replace("{UNITYPROJPATH}", unityprojectpath);

				string version = "";
				if (System.Environment.Version.Major == 4)
				{
					version = "_v4";
				}
				CreateUnityAPI = CreateUnityAPI.Replace("{VERSION}", version);

				System.IO.File.WriteAllText(as3projpath + "/bat/CreateUnityAPI.bat", CreateUnityAPI);
			}
			{
				string RunApp = System.IO.File.ReadAllText(sdkpath + "/templates/flashdevelop/hotfixprojtemplate/bat/RunApp.bat");
				System.IO.File.WriteAllText(as3projpath + "/bat/RunApp.bat", RunApp);
			}
			{
				string SetupSDK = System.IO.File.ReadAllText(sdkpath + "/templates/flashdevelop/hotfixprojtemplate/bat/SetupSDK.bat");
				SetupSDK = SetupSDK.Replace("{SDKPATH}", sdkpath);
				System.IO.File.WriteAllText(as3projpath + "/bat/SetupSDK.bat", SetupSDK);
			}
			{
				string CompileCode = System.IO.File.ReadAllText(sdkpath + "/templates/flashdevelop/hotfixprojtemplate/bat/CompileCode.bat");
				CompileCode = CompileCode.Replace("{HotFixProj}", as3projname);
				CompileCode = CompileCode.Replace("{UNITYPROJPATH}", unityprojectpath);
				System.IO.File.WriteAllText(as3projpath + "/bat/CompileCode.bat", CompileCode);
			}
			{
				string application = System.IO.File.ReadAllText(sdkpath + "/templates/flashdevelop/hotfixprojtemplate/application.xml");
				application = application.Replace("{HotFixProj}", as3projname);
				System.IO.File.WriteAllText(as3projpath + "/application.xml", application);
			}
			{
				string ASRuntime_readme = System.IO.File.ReadAllText(sdkpath + "/templates/flashdevelop/hotfixprojtemplate/ASRuntime_readme.txt");
				System.IO.File.WriteAllText(as3projpath + "/ASRuntime_readme.txt", ASRuntime_readme);
			}
			{
				string genapi = System.IO.File.ReadAllText(sdkpath + "/templates/flashdevelop/hotfixprojtemplate/genapi.config.xml");
				genapi = genapi.Replace("{TEMPPATH}", System.IO.Path.GetTempPath());
				genapi = genapi.Replace("{UNITYPROJPATH}", unityprojectpath);
				genapi = genapi.Replace("{UNITYINSTALLPATH}", unityInstallPath);
				genapi = genapi.Replace("{SYSTEMDLL}", typeof(System.Diagnostics.Process).Assembly.Location);

				string assembly = string.Empty;

				List<string> assemblylists = new List<string>();
				assemblylists.Add(typeof(UnityEngine.GameObject).Assembly.Location);
				assemblylists.Add(typeof(UnityEngine.UI.Image).Assembly.Location);

				//***获取热更工程里用到的dll****
				var hotassembly = typeof(ActionScriptStartUp).Assembly;
				assemblylists.Add(hotassembly.Location);

				List<string> resolvepaths = new List<string>();

				foreach (var item in hotassembly.GetReferencedAssemblies())
				{
					var a = System.Reflection.Assembly.Load(item);
					string p = System.IO.Path.GetDirectoryName(a.Location);
					if (!resolvepaths.Contains(p))
						resolvepaths.Add(p);
				}

				string RESOLVEPATH = "";
				foreach (var item in resolvepaths)
				{
					RESOLVEPATH += string.Format("<item value=\"{0}\"></item>\n", item);
				}
				genapi = genapi.Replace("{RESOLVEPATH}", RESOLVEPATH);


				Dictionary<string, object> dictAssembly = new Dictionary<string, object>();
				for (int i = 0; i < assemblylists.Count; i++)
				{
					if (!dictAssembly.ContainsKey(assemblylists[i]))
					{
						dictAssembly.Add(assemblylists[i], null);
					}
				}

				foreach (var item in dictAssembly.Keys)
				{
					assembly += string.Format("<assembly value=\"{0}\"></assembly>\n", item);
				}
				//assembly += string.Format("<assembly value=\"{0}\"></assembly>\n", typeof(UnityEngine.GameObject).Assembly.Location);
				//assembly += string.Format("<assembly value=\"{0}\"></assembly>\n", typeof(UnityEngine.UI.Image).Assembly.Location);

				genapi = genapi.Replace("{UNITYDLLS}", assembly);



				System.IO.File.WriteAllText(as3projpath + "/genapi.config.xml", genapi);
			}

			{
				string HotFixProj = System.IO.File.ReadAllText(sdkpath + "/templates/flashdevelop/hotfixprojtemplate/HotFixProj.as3proj");
				HotFixProj = HotFixProj.Replace("{UNITYPROJPATH}", unityprojectpath);
				HotFixProj = HotFixProj.Replace("{SDKPATH}", sdkpath);
				System.IO.File.WriteAllText(as3projpath + "/" + as3projname + ".as3proj", HotFixProj);
			}

			{
				string as3hotfixConfig = System.IO.File.ReadAllText(sdkpath + "/templates/flashdevelop/hotfixprojtemplate/obj/as3hotfixConfig.xml");
				as3hotfixConfig = as3hotfixConfig.Replace("{HOTFIXPROJPATH}", as3projpath);
				as3hotfixConfig = as3hotfixConfig.Replace("{SDKPATH}", sdkpath);
				System.IO.File.WriteAllText(as3projpath + "/obj/" + as3projname + "Config.xml", as3hotfixConfig);
			}

		}

		//生成API
		{
			System.Diagnostics.ProcessStartInfo myProcessStartInfo =
				new System.Diagnostics.ProcessStartInfo(
					as3projpath + "/bat/CreateUnityAPI.bat")
					;

			myProcessStartInfo.UseShellExecute = false;
			myProcessStartInfo.RedirectStandardInput = true;
			//myProcessStartInfo.RedirectStandardOutput = true;

			using (System.Diagnostics.Process process = new System.Diagnostics.Process())
			{
				process.StartInfo = myProcessStartInfo;
				process.Start();

				process.StandardInput.WriteLine();

				process.WaitForExit();

			}
		}


		System.Diagnostics.Process.Start(as3projpath);

	}

	[MenuItem("ASRuntime/Open ActionScript3 Project Folder")]
	static void OpProjFolder()
	{

		var settings = LoadOrCreateSetting();
		if (string.IsNullOrEmpty(settings.AS3ProjectPath) || !System.IO.Directory.Exists(settings.AS3ProjectPath))
		{
			if (!EditorUtility.DisplayDialog("", "Path setting not found. Do you want to find the project location manually?", "YES", "NO"))
			{
				return;
			}

			string dir = EditorUtility.OpenFolderPanel("Find ActionScript Project", System.IO.Path.GetDirectoryName(Application.dataPath), string.Empty);
			if (!string.IsNullOrEmpty(dir) && System.IO.Directory.Exists(dir))
			{
				settings.AS3ProjectPath = dir;
				SaveSetting(settings);
			}

		}

		EditorUtility.OpenWithDefaultApp(((ASRuntimeSettings)settings).AS3ProjectPath);
	}


	[MenuItem("ASRuntime/Clean API Code")]
	static void CleanAPI()
	{
		if (!EditorUtility.DisplayDialog("ASRuntime", "Are you sure you want to clean up all your API code ? ", "Yes", "No"))
		{
			return;
		}

		UnityEditor.FileUtil.DeleteFileOrDirectory(Application.dataPath + "/ASRuntimePlayer/RegCode");
		UnityEditor.FileUtil.DeleteFileOrDirectory(Application.dataPath + "/Standard Assets/ASRuntime/ScriptSupport/Generated");
		EditorUtility.DisplayDialog("ASRuntime", "API code cleanup completed", "OK");
		AssetDatabase.Refresh(ImportAssetOptions.Default);

	}



}
