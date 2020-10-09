# root path
$root = Split-Path -Parent -Path $MyInvocation.MyCommand.Path

if ([string]::IsNullOrWhiteSpace($env:ASPNETCORE_SUPPRESSSTATUSMESSAGES)) {
    $env:ASPNETCORE_SUPPRESSSTATUSMESSAGES = 'true'
}

# load binaries
Add-Type -AssemblyName System.Web
Add-Type -AssemblyName System.Net.Http
Add-Type -LiteralPath "$($root)/Libs/Listener.dll"

# load public functions
$sysfuncs = Get-ChildItem Function:
Get-ChildItem "$($root)/Public/*.ps1" | Resolve-Path | ForEach-Object { . $_ }

# get functions from memory and compare to existing to find new functions added
$funcs = Get-ChildItem Function: | Where-Object { $sysfuncs -notcontains $_ }

# export the module's public functions
Export-ModuleMember -Function ($funcs.Name)