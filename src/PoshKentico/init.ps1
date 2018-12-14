Add-Type -Path "$PSScriptRoot/PoshKentico.AssemblyBinding.dll"

Write-Verbose "Initializing posh-kentico..."

# Attempts to emulate Binding Redirects.
[PoshKentico.AssemblyBinding.PowerShellAssemblyBindingHelper]::Initialize()