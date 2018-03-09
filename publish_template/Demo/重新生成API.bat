@echo off

@echo 如果是第一次执行，请先配置要生成api的dll. 编辑linkcodegencli\genapi.config 配置api的dll
@pause

@echo 删除所有文件

rd /s /q linkcodegencli\as3_unity
md linkcodegencli\as3_unity
del /q AS3Hotfix_U56\Assets\ScriptSupport\Generated\*.*


..\SDK1.0.0\linkcodegencli\LinkCodeGenCLI.exe config=linkcodegencli/genapi.config

if %errorlevel% NEQ 0 goto error


goto end



:error


pause



:end

