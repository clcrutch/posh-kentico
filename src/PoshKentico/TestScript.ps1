# Requires -RunAsAdministrator

#$VerbosePreference = "Continue"
#$DebugPreference = "Continue"

Import-Module ./posh-kentico.psd1
Import-Module ./PSCmdlets.psm1

Push-Location C:\git\Kentico\src\CMS\App_Data\CIRepository\@global\cms.widget

add-type -path "C:\git\posh-kentico\build\posh-kentico\PoshKentico.Core.dll"

$xmlFiles = gci | % { gci $_ }

$widgetCategoryNames = (Get-CMSWidgetCategory _LightStream -Recurse).WidgetCategoryName
$lightStreamWidgetXmls = $xmlFiles | ? { $widgetCategoryNames.Contains(([xml](gc $_)).'cms.widget'.WidgetCategoryID.CodeName) }

foreach ($xmlFile in $lightStreamWidgetXmls) {
    $xml = [xml](gc $xmlFile)

    $widget = Get-CMSWidget $xml.'cms.widget'.WidgetName

    if ($null -eq $xml.'cms.widget'.WidgetProperties.form -and [string]::IsNullOrWhiteSpace($widget.WidgetProperties)) {
        continue;
    }

    $xml.'cms.widget'.WidgetProperties.InnerXml = (new-object poshkentico.core.providers.development.widgets.widget -argumentlist $widget).Properties

    $settings = New-Object -TypeName System.Xml.XmlWriterSettings
    $settings.Indent = $true

    $writer = [System.Xml.XmlWriter]::Create($xmlFile.FullName, $settings)
    $xml.Save($writer)

    $writer.Close()
}

Pop-Location