Import-Module posh-kentico

Remove-Item (Join-Path $PSScriptRoot ..\docs) -Recurse

New-MarkdownHelp -Module "posh-kentico" -OutputFolder ..\docs -Force