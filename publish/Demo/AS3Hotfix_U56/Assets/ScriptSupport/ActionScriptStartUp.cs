using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionScriptStartUp : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start()
	{
		DontDestroyOnLoad(this);
		
		return LoadHotFixAssembly();
	}

	IEnumerator LoadHotFixAssembly()
	{
		//***创建flashplayer***
		//***建议全局只使用一个as3运行时。因此将此脚本设置为一直存在，并将flashplayer保存在此对象中。

		var flashplayer = new ASRuntime.Player();

		//此处从StreamingAssetsPath中加载字节码，实际热更可从网络下载后执行。

#if UNITY_ANDROID
		
		WWW www = new WWW(Application.streamingAssetsPath + "/hotfix.cswc");
#else
		WWW www = new WWW("file:///" + Application.streamingAssetsPath + "/hotfix.cswc");
#endif
		while (!www.isDone)
			yield return null;
		if (!string.IsNullOrEmpty(www.error))
			UnityEngine.Debug.LogError(www.error);

		//加载as3编译器生成的字节码。
		ASBinCode.CSWC swc = ASBinCode.CSWC.loadFromBytes(www.bytes);
		www.Dispose();
		//加载本地代码的链接
		ASRuntime.nativefuncs.BuildInFunctionLoader.loadBuildInFunctions(swc);
		(new ASRuntime.extFunctions()).registrationFunction(swc);
		

		this.player = flashplayer;

		flashplayer.loadCode(swc);

		main = flashplayer.createInstance("Main");
		updatemethod= flashplayer.getMethod(main, "update");
		
	}



	private ASRuntime.Player player;
	
	public ASRuntime.Player AS3Player
	{
		get
		{
			return player;
		}
	}

	ASBinCode.rtData.rtObject main;
	ASBinCode.rtData.rtFunction updatemethod;
	// Update is called once per frame
	void Update () {
		if (player != null)
		{
			player.invokeMethod(main, updatemethod);
		}
	}
}
