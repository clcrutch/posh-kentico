---
external help file: PoshKentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Get-CMSWidgetField

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

### No Name (Default)
```
Get-CMSWidgetField -Control <WidgetInfo> [<CommonParameters>]
```

### Name
```
Get-CMSWidgetField [-Name] <String> [-RegularExpression] -Control <WidgetInfo> [<CommonParameters>]
```

## DESCRIPTION
{{Fill in the Description}}

## EXAMPLES

### Example 1
```powershell
PS C:\> {{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

### -Control
{{Fill Control Description}}

```yaml
Type: WidgetInfo
Parameter Sets: (All)
Aliases: WebPart, Widget

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Name
{{Fill Name Description}}

```yaml
Type: String
Parameter Sets: Name
Aliases: Caption

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RegularExpression
{{Fill RegularExpression Description}}

```yaml
Type: SwitchParameter
Parameter Sets: Name
Aliases: Regex

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### CMS.PortalEngine.WidgetInfo

## OUTPUTS

### CMS.FormEngine.FormFieldInfo[]

## NOTES

## RELATED LINKS
