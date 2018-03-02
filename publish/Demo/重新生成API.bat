@echo off

@echo 如果是第一次执行，请先配置要生成api的dll. 编辑linkcodegencli\LinkCodeGenCLI.exe.config 配置api的dll
@pause

@echo 删除所有文件

rd /s /q linkcodegencli\as3_unity
md linkcodegencli\as3_unity
del /q AS3Hotfix_U56\Assets\ScriptSupport\Generated\*.*


linkcodegencli\LinkCodeGenCLI.exe

