## Apple Juice
  
.net2.0实现的ActionScript3 编译器和虚拟机。可以在任意支持.net2.0的平台上（例如Unity）执行ActionScript3脚本。为不支持JIT的环境提供动态更新脚本的功能。
>支持除了 namespace 和 with 和 E4X XML操作外的一切actionscript3语法特性。   

>可以将actionscript3代码编译为字节码，然后加载并动态执行。VM由纯C# 2.0代码实现，可以直接让Unity来读取并执行生成的字节码，就是可用Unity完成热更新操作。  
编译器部分实现了完整的编译期类型检查。并且有完整的错误提示。已经和FlashDevelop完成了集成，可以直接在FlashDevelop中开发并一键编译发布到Unity。   
Unity的API或者自己开发的C# API提供了工具直接转换为actionscript api文件和对接代码，并且直接注册到FlashDevelop工程中。


### 文档地址
- 中文文档
  [https://asheigithub.github.io/apple-juice-actionscript/doc_cn/](https://asheigithub.github.io/apple-juice-actionscript/doc_cn/ "中文文档")
  

### 下载地址
[as3_unity预览测试包0.9.4f2](https://github.com/asheigithub/ASTool/raw/master/publish/lastrelease/as3_unity.zip)

#### 特色 ####
- 对.net的结构体有特殊优化。在脚本中只要在方法的局部变量中使用，可以任意的new而不会导致gc.因此使用UnityEngine.Vector3之类类型时，可以放心使用。
- 对导出的类型有特殊优化。只要在方法的局部变量中使用，则脚本本身不产生额外的对象分配。
- 实现操作符重载。导出的对象如果有操作符重载，则在脚本中同样可以使用。
- 可以直接在脚本中继承MonoBehaviour。并且还可以将它挂载到GameObject上。
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
- 自动将.net 委托对应到ActionScript3的function对象。
- 自动处理.net 类库中的 out ref类型的参数。
- 可以在ActionScript3脚本中继承.net类库。还可以覆盖基类的虚方法。
- 完整的编译期类型检查。脚本有完整的编译时类型检查，利于错误排查。
- 完全实现的面向对象支持。完整支持类继承和接口。
- FlashDevelop IDE支持。可以完全利用IDE的智能提示，编译错误也可在IDE中得到反馈。可在IDE中直接编译热更新代码成到Unity项目
- api全自动导出 自动将.net类库导出给脚本使用。并且保留有原始类型信息。
> 自动导出的API为actionscript3风格。比如**UnityEngine.UI.Button** 将被导出为 **unityengine.ui.Button**
> 能将.net 类库中的类型包含继承关系和接口实现关系的导出。例如，**UnityEngine.MeshRenderer**  继承自 **UnityEngine.Renderer**  。那么导出后的as3类型也会保持以上的关系。


