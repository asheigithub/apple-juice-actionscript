:: Set working dir
cd %~dp0 & cd ..

:user_configuration

:: Static path to ASRuntime SDK
set ASRuntime_SDK={SDKPATH}

:validation
if not exist "%ASRuntime_SDK%\bin" goto asruntimesdk

if not exist "%ASRuntime_SDK%\linkcodegencli" goto asruntimesdk


goto succeed




:asruntimesdk
echo.
echo ERROR: incorrect path to ASRuntime SDK in 'bat\SetupSDK.bat'
echo.
echo Looking for: %ASRuntime_SDK%\bin
echo.
if %PAUSE_ERRORS%==1 pause
exit

:succeed
