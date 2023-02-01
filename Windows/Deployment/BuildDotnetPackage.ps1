############################################################################################
# Scriptfile BuildDotnetPackage
# ------------------------------------------------------------------------------------------
# Version: 1.23.032, 01.02.2023, Querplex GmbH, Oliver Klepach
# ------------------------------------------------------------------------------------------
# Build Dotnet Package
# ------------------------------------------------------------------------------------------
# $currectFolder: script file folder
# $configFile: Path to the BuildDotnetPackage Config File
#
# Package Config Sample:
#
#<Package>
#    <UpdateVersionNumber>true</UpdateVersionNumber>
#    <!-- CSProjFile>C:\TestProjects\AutoUpdateThis\AutoUpdateThis.csproj</CSProjFile-->
#    <!-- VersionType: assembly|file -->
#    <!-- VersionType>file</VersionType-->
#    <DotnetPublish>
#      <Options>-r win-x64 --no-self-contained</Options>
#      <DeleteAfterBuild>%PackageDir%\appsettings.json</DeleteAfterBuild>
#      <DeleteAfterBuild>%PackageDir%\appsettings.local.json</DeleteAfterBuild>
#    </DotnetPublish>
#    <AutoUpdate>
#        <!--LocalFile>D:\TestProjects\AutoUpdater\AutoUpdateThis\Deployment\AutoUpdateThis-BuildDotnetPackage.xml</LocalFile-->
#        <!-- %packagename% will replace by the Package file name-->
#        <DownloadUrl>http://progupdate.webserver.com/download/AutoUpdateThis/%packagename%</DownloadUrl>
#        <!--ChangeLogUrl>http://progupdate.webserver.com/download/AutoUpdateThis/changelog.html</ChangeLogUrl-->
#        <DownloadFolder>\\webserverfileshare\UpdateShare\AutoUpdateThis</DownloadFolder>
#    </AutoUpdate>
#</Package>
############################################################################################
param([string]$currectFolder, [string]$configFile)

if ($currectFolder -eq $null -or $currectFolder -eq "") { $currectFolder = $PSScriptRoot }

$global:configXml = New-Object XML;
$global:projectXml = New-Object XML;
$global:projectName = "";
$global:projectFilePath = "";
$global:projectRoot = "";
$global:projectVersion = "";
$global:packageName = "";
$global:packageFolder = "";
$global:publishFolder = "";

# load the config file xml
function LoadConfig($configFile)
{
    if ($configFile -eq $null -or $configFile -eq "") { $configFile = "BuildDotnetPackage.xml"; }

    write-host "Load Config '$configFile'";
    $configFilePath = $null;
    if ((Test-Path $configFile) -eq $True) { $configFilePath = $configFile };
    if ($configFilePath -eq $null -and (Test-Path "$currectFolder\$configFile") -eq $True) { $configFilePath = "$currectFolder\$configFile"}
    
    if ($configFilePath -eq $null) { throw new Excception "BuildDotnetPackage configFile not found!" }

    $configXml.Load($configFile);
}
# load the CSProject file
function LoadCSProject()
{
    write-host "Load Project File";
    $projectFile = GetConfigValue "CSProjFile";
    if ($projectFile -eq "")
    {
        $projectFiles = Get-ChildItem -Path $currectFolder -File -Filter *.csproj;
        if ($projectFiles.Count -eq 0) { $projectFiles = Get-ChildItem -Path "$currectFolder\.." -File -Filter *.csproj; }
        if ($projectFiles.Count -ne 0) {
            $projectFile = ($projectFiles | sort | Select-Object -First 1).FullName;
        }
    }

    $global:projectFilePath = $null;
    if ((Test-Path $projectFile) -eq $True) { $global:projectFilePath = $projectFile };
    if ($projectFilePath -eq $null -and (Test-Path "$currectFolder\$projectFile") -eq $True) { $global:projectFilePath = "$currectFolder\$projectFile"}

    if ($projectFile -eq "") { throw [Exception] "Project file not found!" }
    
    write-host "Project File found $projectFilePath";
    
    $global:projectName = $projectFilePath.Substring($projectFilePath.LastIndexOf("\")+1).replace(".csproj", "");
    $global:projectRoot = $projectFilePath.Substring(0, $projectFilePath.LastIndexOf("\"));

    $projectXml.Load($projectFilePath);
}
# get the value of a config node
function GetConfigValue([string]$xPath, [string]$defaultValue)
{
    write-host "GetConfigValue $xPath";
    $foundNode = $configXml.DocumentElement.SelectSingleNode($xPath);
    write-host "GetConfigValue found '$($foundNode.innerText)'";

    if ($foundNode -eq $null -and $defaultValue -eq $null) { return ""}
    if ($foundNode -eq $null -and $defaultValue -ne $null) { return $defaultValue}
    return $foundNode.innerText;
}
# get the value of a project node
function GetProjectValue([string]$name)
{
    write-host "GetProjectValue $name";
    $foundNode = $projectXml.DocumentElement.SelectSingleNode("PropertyGroup/$name");
    write-host "GetProjectValue found '$($foundNode.innerText)'";

    if ($foundNode -eq $null) { return ""}
    return $foundNode.innerText;
}
# Update Version Number
# <UpdateVersionNumber>true</UpdateVersionNumber>
function UpdateVersionNumber()
{
    if ((GetConfigValue "UpdateVersionNumber").ToLowerInvariant() -ne "true") { return; };
    
    if (GetConfigValue "VersionType" -eq "file") { $versionNode = $projectXml.DocumentElement.SelectSingleNode("PropertyGroup/FileVersion"); }
    else { $versionNode = $projectXml.DocumentElement.SelectSingleNode("PropertyGroup/AssemblyVersion"); }

    if ($versionNode -eq $null) {
        throw [Exception] "Version node in project file not found";
    };

    $oldVersionNumber = $versionNode.InnerText;
    $now = Get-Date;
    # automatic build and version number
    $secondsSinceMidnightDivivedBy2 = [int]([int]($now - $now.Date).TotalSeconds / 2);
    $daysSinceJan1st2000 = [int]($now - (get-Date -Year 2000 -Month 1 -Day 1)).TotalDays;
    $newVersionNumber=$oldVersionNumber.split(".")[0] + "." + $oldversionNumber.split(".")[1] + "." + $daysSinceJan1st2000 + "." + $secondsSinceMidnightDivivedBy2;
    $versionNode.InnerText = $newVersionNumber;
    # save project file
    $projectXml.Save($projectFilePath);
    write-host "Update Version number from $oldVersionNumber to $newVersionNumber" -ForegroundColor Yellow;
}

# build the Package Parameter
function SetPackageParameter()
{
    $now = Get-Date;
    if (GetConfigValue "VersionType" -eq "file") { $global:projectVersion = GetProjectValue "FileVersion"; }
    else { $global:projectVersion = GetProjectValue "AssemblyVersion"; }

    $global:packageName = "$($now.ToString("yyyyMMdd"))-$($now.ToString("HHmm"))-$projectVersion-$projectName";
    $global:packageFolder = GetConfigValue "PackageFolder" "$projectRoot\Deployment\Package";
    $global:publishFolder = GetConfigValue "PublishFolder" "$projectRoot\bin\Publish";
}

# dotnet build package
function BuildPublishPackage()
{
    write-host "Start dotnet Publish" -ForegroundColor yellow;
    $dotnet = "C:\Program Files\dotnet\dotnet.exe"
    $publishOut = "$publishFolder\$projectName";
    $zipOut = "$packageFolder\$packageName.zip";

    # TODO: find out: set options within one field
    $publishOptions = GetConfigValue "DotnetPublish/Options";
    $publishOption2 = GetConfigValue "DotnetPublish/Option2";
    
    #& $dotnet publish $projectRoot\$projectName.csproj -c Release -r $publishOption1 $publishOption2 -o $publishOut; # -v n=normal, d=detailed
    $command = """$dotnet"" publish ""$projectRoot\$projectName.csproj"" -c Release $publishOptions -o ""$publishOut""";
    write-host "execute $command";
    iex "& $command";
    if ($LASTEXITCODE -gt 0) { throw [Exception] "Build failed!"; }

    write-host "Pack Deployment package $zipOut";
    $removeNodes = $configXml.DocumentElement.SelectNodes("DotnetPublish/DeleteAfterBuild");
    foreach($removeNode in $removeNodes)
    {
        $removeFile = $removeNode.innerText.replace("%PackageDir%", "$publishOut");
        write-host "Remove $removeFile";
        # TODO: Remove directories
        #write-host "Remove $removeItems.Substring(0, $removeItems.LastIndexOf("\"))";
        #write-host "Remove $removeItems.Substring($removeItems.LastIndexOf("\")+1)";
        #Get-ChildItem -Path $removeItems.Substring(0, $removeItems.LastIndexOf("\")) -Filter $removeItems.Substring($removeItems.LastIndexOf("\")+1) | foreach { $_.Delete()}
        if ((Test-Path -path $removeFile) -eq $True) { Remove-Item $removeFile; }
    }


    # check and create package folder
    if ((Test-Path -path $packageFolder) -ne $True) { New-Item $packageFolder -type directory | out-null; }
    # delete old versions
    Get-ChildItem -Path $packageFolder -File -Filter "*$projectName.zip" | foreach { $_.Delete()}
    # pack new version
    Compress-Archive -Path $publishOut\* -DestinationPath $zipOut -Force;
}
# build version file and Copy Package
function BuildVersionAndCopyPackage()
{
    if ($configXml.DocumentElement.SelectSingleNode("AutoUpdate") -eq $False) { return; }
    
    write-host "Create Version file" -ForegroundColor yellow;
    $versionXml = New-Object XML;
    $versionXml.LoadXml("<item><mandatory>false</mandatory></item>");

    # set version
    $itemNode = $versionXml.CreateElement("version");
    $versionXml.DocumentElement.AppendChild($itemNode) | out-null;
    $itemNode.InnerText = $global:projectVersion;

    # set download file url
    $itemNode = $versionXml.CreateElement("url");
    $versionXml.DocumentElement.AppendChild($itemNode) | out-null;
    $itemNode.InnerText = (GetConfigValue "AutoUpdate/DownloadUrl").Replace("%packagename%", "$packageName.zip");

    # set change log
    $itemValue = GetConfigValue "AutoUpdate/ChangeLogUrl";
    if ($itemValue -ne $null -and $itemValue -ne "")
    {
	    $itemNode = $versionXml.CreateElement("changelog");
	    $versionXml.DocumentElement.AppendChild($itemNode) | out-null;
	    $itemNode.InnerText = $itemValue;
    }

    $versionFile = GetConfigValue "AutoUpdate/LocalFile";
    if ($versionFile -eq $null -or $versionFile -eq "")
    {
        $versionFile = "$projectRoot\Deployment\AutoUpdate-$projectName.xml";
    }
    if (!$versionFile.Endswith(".xml"))
    {
        $versionFile = "$versionFile\AutoUpdate-$projectName.xml";
    }
    $versionXml.Save($versionFile);

    # copy package and version to download folder
    $downloadFolder = GetConfigValue "AutoUpdate/DownloadFolder";
    if ($downloadFolder -ne $null -and $downloadFolder -ne "")
    {
        # check and create package folder
        if ((Test-Path -path $downloadFolder) -ne $True) { New-Item $downloadFolder -type directory | out-null; }
        write-host "copy Version to $downloadFolder";
        copy-item $versionFile $downloadFolder;
        write-host "copy Package to $downloadFolder";
        copy-item "$packageFolder\$packageName.zip" $downloadFolder;
    }
}

Start-Transcript -path "$currectFolder\BuildDotnetPackage.log" -append -IncludeInvocationHeader;
Try
{
    write-host "Build Package - Config '$configFile'" -ForegroundColor Yellow;
    LoadConfig $configFile;
    LoadCSProject;
    UpdateVersionNumber;
    SetPackageParameter;
    BuildPublishPackage;
    BuildVersionAndCopyPackage;
    write-host "Build Package done" -ForegroundColor Yellow;
}
Catch
{
    $ErrorMessage = $_.Exception.Message;
    $FailedItem = $_.Exception.ItemName;
    write-host "Error Item: $FailedItem. The error message was '$ErrorMessage'" -ForegroundColor Red;
}  

Stop-Transcript;