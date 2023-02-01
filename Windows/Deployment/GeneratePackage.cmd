@echo off

echo "use BuildPackage!!!!"
pause
exit 1

set ThisSolution=HttpMessager

set pwd=%~dp0
set PackageDir=%pwd%Packages
set Timestamp=%date:~6,8%%date:~3,2%%date:~0,2%-%time:~0,2%%time:~3,2%
set Timestamp=%Timestamp: =0%
set ZipFileName=%Timestamp%-%ThisSolution%
set ZipFile=%pwd%%ZipFileName%.zip


cd %pwd%..

rem clean bin''s
dotnet clean

rem create Package Folder
if exist %PackageDir% rmdir /s /q %PackageDir%
mkdir %PackageDir%
echo created directory %PackageDir%

rem dotnet publish HttpMessager.csproj -c Release -r win-x64 --self-contained -o %PackageDir%

dotnet publish HttpMessager.csproj -c Release -r win-x64 --no-self-contained -o %PackageDir%
rem remove appsettings.json
if exist %PackageDir%\appsettings.json del %PackageDir%\appsettings.json

rem generate zip
powershell.exe -Command "& '\\muchvintern1\_scripts\!PSToolBox\TB-Archive.ps1' 'Compress' -CompressedFile '%ZipFile%' -FilesToCompress '%PackageDir%' "


cd %pwd%
