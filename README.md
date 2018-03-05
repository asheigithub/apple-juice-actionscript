## ASTool
### .net2.0实现的ActionScript3 编译器和解释器
>支持除了 namespace 和 with 和 E4X XML操作外的一切actionscript3语法特性。   

>可以将actionscript3代码编译为字节码，然后加载并动态执行。VM由纯C# 2.0代码实现，可以直接让Unity来读取并执行生成的字节码，就是可用Unity完成热更新操作。  
编译器部分实现了完整的编译期类型检查。并且有完整的错误提示。已经和FlashDevelop完成了集成，可以直接在FlashDevelop中开发并一键编译发布到Unity。   
Unity的API或者自己开发的C# API提供了工具直接转换为actionscript api文件和对接代码，并且直接注册到FlashDevelop工程中。详见Demo。

#### 特色 ####
- 对.net的结构体有特殊优化。在脚本中只要在方法的局部变量中使用，可以任意的new而不会导致gc.因此使用UnityEngine.Vector3之类类型时，可以放心使用。
- 对导出的类型有特殊优化。只要在方法的局部变量中使用，则脚本本身不产生额外的对象分配。
- 实现操作符重载。导出的对象如果有操作符重载，则在脚本中同样可以使用。如下代码是完全合法的。
````actionscript
    cube.transform.localPosition += v * Time.deltaTime;
````
- 可以使用yield语句返回一个IEnumerator,然后用Iterator包装为.net的IEnumerator接口。所以可以直接在脚本中写Unity的协程。如如下代码所示:  
````actionscript
    var mono:MonoBehaviour = GameObject.find("AS3Player").getComponent(MonoBehaviour) as MonoBehaviour;
	trace(mono.name);
	mono.startCoroutine( Iterator(  
		(
		function()
		{
			trace("a",Time.frameCount);

			yield return 1;

			trace("b",Time.frameCount);

			yield return 2;

			trace("c",Time.frameCount);
			yield return 3;
		}
		)()
	));
````
- 自动将.net 委托对应到ActionScript3的function对象。例如:
````actionscript
    var btn:Button = Button( GameObject.find("Button").getComponent(Button));			
	btn.onClick.addListener(			
		onclick			
	);

	btn.onClick.addListener(
		function()
		{
			trace("hahaha",this);					
		}
	);
````
- 自动处理.net 类库中的 out ref类型的参数。这样的方法也可自动导出。例如 long.TryParse:
````actionscript
	/**
	* System.Int64.TryParse
	*parameters:
	*  s : System.String
	*  result : (Out)System.Int64
	*return:
	*   System.Boolean
	*/
	[native,static_system_Int64_tryParse];
	public static function tryParse(s:String,result:Int64,refout:as3runtime.RefOutStore):Boolean;

````

- 完整的编译期类型检查。脚本有完整的编译时类型检查，利于错误排查。
- 完全实现的面向对象支持。完整支持类继承和接口。
- FlashDevelop IDE支持。可以完全利用IDE的智能提示，编译错误也可在IDE中得到反馈。可在IDE中直接编译热更新代码成到Unity项目


#### api全自动导出 ####

> 自动将.net类库导出给脚本使用。并且保留有原始类型信息。例如**UnityEngine.Avatar**导出后的api形式为:
````actionscript
    package unityengine
    {


		/**
		* Sealed
		*  UnityEngine.Avatar
		*/
		[no_constructor]
		[link_system]
		public final class Avatar extends UObject
		{
			[creator];
			[native,unityengine_Avatar_creator];
			private static function _creator(type:Class):*;
			[native,$$_noctorclass];
			public function Avatar();
			//*********公共方法*******
			/**
			* UnityEngine.Avatar.get_isValid
			*return:
			*   System.Boolean
			*/
			[native,unityengine_Avatar_get_isValid]
			public final function get isValid():Boolean;
	
			/**
			* UnityEngine.Avatar.get_isHuman
			*return:
			*   System.Boolean
			*/
			[native,unityengine_Avatar_get_isHuman]
			public final function get isHuman():Boolean;
	
	
		}
    }
````
> IDE能提供智能感知提示。

> 自动导出的API为actionscript3风格。比如**UnityEngine.UI.Button** 将被导出为 **unityengine.ui.Button**

> 能将.net 类库中的类型包含继承关系和接口实现关系的导出。例如，**UnityEngine.MeshRenderer**  继承自 **UnityEngine.Renderer**  。那么导出后的as3类型也会保持以上的关系。



### 下载地址
[as3_unity预览测试包0.9.1](https://github.com/asheigithub/ASTool/raw/master/publish/v0.9.1/as3_unity_0.9.1.zip)

### 内容说明：
##### SDK1.0.0
>是一个自定义的 AIRSDK.可被FlashDevelop识别并加载，使用它来进行代码的编译和发布。

##### Demo
>热更新示例工程。  
>
  -  AS3Hotfix_U56  
Unity5.6的工程  
- HotFixProj  
FlashDevelop的ActionScript3热更代码工程
- linkcodegencli  
生成api和api文档的工具
- unityassembly  
要生成api的unity dll
- buildgame   
Unity导出的windows版示例.


##### 使用说明 #####

> 如果安装了FlashDevelop并且安装了Java，则可以用FlashDevelop打开actionscript3项目，点击编译即可将热更代码发布到Unity.  
如果没有安装FlashDevelop,则可以执行 "编译代码到unity.bat",可以使用其他文本编辑器（比如notepad）来修改代码，并热更发布到Unity.同时会将热更代码发布到 demogame.exe,并立即执行。 
 

> "重新生成API.bat" 会重新生成dll的api文档。如果自己编写了C# dll api,则可将此dll添加到linkcodegencli的配置中，执行一次"重新生成API.bat",即可将api导出到as3以供使用。
> 
FlashDevelop项目的约定：FlashDevelop项目下需要有一个lib文件夹，linkcodegencli会将生成的as3 api生成一个叫做as3unitylib.cswc的二进制文件到里面。编译时会加载此文件以提交编译速度。如果缺少此文件，则编译会失败并提示。

- 环境安装完全时的参考流程  
![](images/as3_unity_demo2.gif)
- 没有安装Unity和FlashDevelop时的体验方法  
![](images/as3_unity_demo3.gif)

##### API生成工具的使用说明 #####
> LinkCodeGenCLI.exe 工具可用于将C#的dll 导出到ActionScript3 API。它会将需要导出的API生成一份 ActionScript3代码文件，一份C#代码文件，最后会将所有C#代码合并为一个单个文件，并生成一个API注册文件，最后还有一个api的二进制字节码文件。
> 
> 如果还配置了一同编译的as3类库路径，则还会一并编译指定的as3类库。当最终编译热更新项目时，会加载此时生成的二进制字节码。
> 
> 约定要求这个文件需要生成到FlashDevelop项目的 lib目录下，名字叫as3unitylib.cswc。
> 
> *buildassemblys*配置节配置想要导出API的dll。每个*assembly*子节点配置一个dll。*assembly*子节点下还可以配置*type*节点，如果这么做了，那么只有配置的类型会被导出，否则将导出所有可以导出的类型。
> 
> 例如
````xml
    <buildassemblys>
    <assembly value="C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v3.5\Profile\Unity Full v3.5\System.dll">
      <type value="aa"></type>
    </assembly>
    </buildassemblys>
````
> 只有名字叫aa的类型才会被导出，因此实际上不会有任何类型被导出。

> *skipcreatortypes*节下配置的是实现已经手工写过api的类型。由于某些类型比较特殊，需要特别对待以满足特殊需求，这些类型被在这里指明。

> *notcreatenamespace*节配置的命名空间下的全部类型都不会被导出。

> *notcreatetypes*节配置的类型不会被导出。

> *notcreatemembers*节配置的成员在遇到时会被跳过。Unity在运行时，某些类型的某些成员会不可用。为此，只能在导出api时跳过这些成员。在这里配置这些成员。

##### Unity API dll的位置 #####
> 要定位Unity的dll,请到Unity的安装目录下查找。Unity工程的Library里面的dll有些读取时会引发BadImageFormatException异常。当发生这种情况时，请到Unity安装目录的/Editor/Data/Managed/目录下加载UnityEngine.dll, /Editor/Data/UnityExtensions/Unity/GUISystem/下加载UnityEngine.UI.dll。
> Unity2017工程中将dll拆成了许多小dll,但导出api时只需到安装目录下去定位这2个dll即可.