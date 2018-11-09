Add-Type -Path "./PoshKentico.AssemblyBinding.dll"

# Attempts to emulate Binding Redirects.
[PoshKentico.AssemblyBinding.PowerShellAssemblyBindingHelper]::Initialize()