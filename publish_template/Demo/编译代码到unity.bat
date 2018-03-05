@echo off

@echo 如果没有安装flashdevelop 和java,那么可以执行这个bat编译热更代码到unity.并且会拷贝一份到buildgame中。
@echo 如果连Unity也没有安装，那么可以运行buildgame查看热更效果。

@pause

..\SDK1.0.0\bin\CMXMLCCLI.exe -load-config+=HotFixProj\obj\HotFixProjConfig.cli.xml -o AS3Hotfix_U56\Assets\StreamingAssets\hotfix.cswc



IF ERRORLEVEL 1 goto error

copy AS3Hotfix_U56\Assets\StreamingAssets\hotfix.cswc buildgame\hotfixdemo_Data\StreamingAssets\hotfix.cswc
buildgame\hotfixdemo.exe


exit
:error
@echo 编译错误请检查
pause



