# ASTool
### .net2.0实现的ActionScript3 编译器和解释器
支持除了 namespace 和 with 和 E4X XML操作外的一切actionscript3语法特性。   

可以将actionscript3代码编译为字节码，然后加载并动态执行。VM由纯C# 2.0代码实现，可以直接让Unity来读取并执行生成的字节码，就是可用Unity完成热更新操作。  
编译器部分实现了完整的编译期类型检查。并且有完整的错误提示。已经和FlashDevelop完成了集成，可以直接在FlashDevelop中开发并一键编译发布到Unity。   
Unity的API或者自己开发的C# API提供了工具直接转换为actionscript api文件和对接代码，并且直接注册到FlashDevelop工程中。详见Demo。




### 下载地址
[as3_unity预览测试包0.9.1](publish/v0.9.1/as3_unity预览测试包0.9.1.zip)

内容说明：
##### SDK1.0.0
是一个自定义的 AIRSDK.可被FlashDevelop识别并加载，使用它来进行代码的编译和发布。

##### Demo
热更新示例工程。


