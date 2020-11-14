
<#
# Dependency Versions
#>

$Versions = @{
    DotNetCore = '3.1.5'
}

<#
# Helper Functions
#>

function Test-PodeBuildIsWindows
{
    $v = $PSVersionTable
    return ($v.Platform -ilike '*win*' -or ($null -eq $v.Platform -and $v.PSEdition -ieq 'desktop'))
}

function Test-PodeBuildCommand($cmd)
{
    $path = $null

    if (Test-PodeBuildIsWindows) {
        $path = (Get-Command $cmd -ErrorAction Ignore)
    }
    else {
        $path = (which $cmd)
    }

    return (![string]::IsNullOrWhiteSpace($path))
}

function Invoke-PodeBuildInstall($name, $version)
{
    [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12

    if (Test-PodeBuildIsWindows) {
        if (Test-PodeBuildCommand 'choco') {
            choco install $name --version $version -y
        }
    }
    else {
        if (Test-PodeBuildCommand 'brew') {
            brew install $name
        }
        elseif (Test-PodeBuildCommand 'apt-get') {
            sudo apt-get install $name -y
        }
        elseif (Test-PodeBuildCommand 'yum') {
            sudo yum install $name -y
        }
    }
}


<#
# Dependencies
#>


# Synopsis: Install dependencies for compiling/building
task BuildDeps {
    # install dotnet
    if (!(Test-PodeBuildCommand 'dotnet')) {
        Invoke-PodeBuildInstall 'dotnetcore' $Versions.DotNetCore
    }
}

<#
# Packaging
#>

# Synopsis: Package up the Module
task Pack -If (Test-PodeBuildIsWindows) Build, {
    $path = './pkg'
    if (Test-Path $path) {
        Remove-Item -Path $path -Recurse -Force | Out-Null
    }

    # create the pkg dir
    New-Item -Path $path -ItemType Directory -Force | Out-Null

    # which folders do we need?
    $folders = @('Public', 'Libs')

    # create the directories, then copy the source
    $folders | ForEach-Object {
        New-Item -ItemType Directory -Path (Join-Path $path $_) -Force | Out-Null
        Copy-Item -Path "./src/$($_)/*" -Destination (Join-Path $path $_) -Force | Out-Null
    }

    # copy general files
    Copy-Item -Path ./src/Pode.Kestrel.psm1 -Destination $path -Force | Out-Null
    Copy-Item -Path ./src/Pode.Kestrel.psd1 -Destination $path -Force | Out-Null
    Copy-Item -Path ./LICENSE.txt -Destination $path -Force | Out-Null
}


<#
# Building
#>

# Synopsis: Build the .NET Core Listener
task Build BuildDeps, {
    if (Test-Path ./src/Libs) {
        Remove-Item -Path ./src/Libs -Recurse -Force | Out-Null
    }

    Push-Location ./src/Listener

    try {
        dotnet build --configuration Release
        if (!$?) {
            throw 'Build Failed'
        }

        dotnet publish --configuration Release --self-contained --output ../Libs
        if (!$?) {
            throw 'Publish Failed'
        }

        exec { nuget install Microsoft.AspNetCore.App -Version 2.2.5 -OutputDirectory ../Listener/nuget }
        (Get-ChildItem ../Listener/nuget -Filter *.dll -Recurse) | ForEach-Object { Copy-Item -Path $_.FullName -Destination ../Libs -Force }
    }
    finally {
        Pop-Location
    }
}