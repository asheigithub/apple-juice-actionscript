using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntimeUnityPluginTool
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				FrmFindSDK frmFindSDK = new FrmFindSDK();
				if (frmFindSDK.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					System.Console.WriteLine(System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(frmFindSDK.SDKPath)));
					System.Environment.Exit(0);
				}
				else
				{
					System.Environment.Exit(1);
				}
			}
			else if (args.Length == 1)
			{
				string unityInstallEditorDir;
				string unityProjectDir;
				string sdkpath;

				string[] argpaths = args[0].Split(',');

				unityInstallEditorDir = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(argpaths[0]));
				unityProjectDir = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(argpaths[1]));
				sdkpath = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(argpaths[2]));

				FrmSetAS3HotFixProjPath frmsethotfixprojpath = new FrmSetAS3HotFixProjPath(unityProjectDir);
				if (frmsethotfixprojpath.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					string as3projfile = frmsethotfixprojpath.AS3ProjFile;
					System.Console.WriteLine(System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(as3projfile)));
					System.Environment.Exit(0);
				}
				else
				{
					System.Environment.Exit(1);
				}
			}
			else
			{
				System.Environment.Exit(1);
			}
		}
	}
}
