#### ASTool
### .net2.0实现的ActionScript3 编译器和解释器
>支持除了 namespace 和 with 和 E4X XML操作外的一切actionscript3语法特性。   

>可以将actionscript3代码编译为字节码，然后加载并动态执行。VM由纯C# 2.0代码实现，可以直接让Unity来读取并执行生成的字节码，就是可用Unity完成热更新操作。  
编译器部分实现了完整的编译期类型检查。并且有完整的错误提示。已经和FlashDevelop完成了集成，可以直接在FlashDevelop中开发并一键编译发布到Unity。   
Unity的API或者自己开发的C# API提供了工具直接转换为actionscript api文件和对接代码，并且直接注册到FlashDevelop工程中。详见Demo。




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
