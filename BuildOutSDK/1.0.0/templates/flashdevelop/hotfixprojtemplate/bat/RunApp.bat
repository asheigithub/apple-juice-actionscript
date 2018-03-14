@echo off

:: Set working dir
cd %~dp0 & cd ..


set PAUSE_ERRORS=1
call bat\SetupSDK.bat

echo.
echo Click UnityEditor "Play" Button.
echo.

pause

goto end

::adl "%APP_XML%" "%APP_DIR%"
::if errorlevel 1 goto error
::goto end

:: :error
::pause

:end
