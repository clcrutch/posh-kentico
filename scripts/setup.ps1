$buildPath = Join-Path $PSScriptRoot "..\build"
$modulePath = Join-Path "C:\Program Files\WindowsPowerShell\Modules" posh-kentico

if (-not (Test-Path $buildPath))
{
    New-Item -Path $buildPath -ItemType Directory
}

if (-not (Test-Path $modulePath))
{
    New-Item -Path $modulePath -Value $buildPath -ItemType Junction
}