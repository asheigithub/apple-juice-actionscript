@echo off

:: Set working dir
cd %~dp0 & cd ..

call bat\SetupSDK.bat

:: If this is the first time, configure the DLL to generate the API.
:: Modify the Hotfixproj/genapi.config configuration file to specify the DLL to build


set str=as3_unity
del /s /q "%str%\*.*" 
for /f "tokens=* delims=" %%i in ('dir /ad /s /b "%str%"^|sort /r') do (
   rd "%%i"
)


del /q "{UNITYPROJPATH}\Assets\Standard Assets\ASRuntime\ScriptSupport\Generated\*.*"


%ASRuntime_SDK%\linkcodegencli\LinkCodeGenCLI.exe config=genapi.config.xml



if %errorlevel% NEQ 0 goto error


goto end



:error


pause



:end



