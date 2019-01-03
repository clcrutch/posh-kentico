Import-Module posh-kentico

$docs = (Join-Path $PSScriptRoot ..\docs)

Remove-Item $docs -Recurse

New-MarkdownHelp -Module "posh-kentico" -OutputFolder $docs -Force