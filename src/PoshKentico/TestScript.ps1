# Requires -RunAsAdministrator

$VerbosePreference = "Continue"
$DebugPreference = "Continue"

$env:PSModulePath="$env:PSModulePath;..\"
Import-Module posh-kentico