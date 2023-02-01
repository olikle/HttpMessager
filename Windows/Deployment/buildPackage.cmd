@echo off
set pwd=%~dp0
powershell.exe -Command "& '%pwd%\BuildDotnetPackage.ps1' "
echo.
if not "%1"=="nopause" pause

