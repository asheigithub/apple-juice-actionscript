@echo off

:: Set working dir
cd %~dp0 & cd ..

set PAUSE_ERRORS=1
call bat\SetupSDK.bat

::@echo If FlashDevelop and Java are not installed, you can execute this bat, compile the code and publish it to unity.


%ASRuntime_SDK%\bin\CMXMLCCLI.exe -load-config+=obj\{HotFixProj}Config.xml -o "{UNITYPROJPATH}\Assets\StreamingAssets\hotfix.cswc"

IF ERRORLEVEL 1 goto error
@echo Compilation succeeded. Click the Unity Play button to see the change
pause
exit
:error
pause




