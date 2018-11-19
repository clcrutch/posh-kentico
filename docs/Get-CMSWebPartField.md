---
external help file: PoshKentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Get-CMSWebPartField

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

### No Name (Default)
```
Get-CMSWebPartField -WebPart <WebPartInfo> [<CommonParameters>]
```

### Name
```
Get-CMSWebPartField [-Name] <String> [-RegularExpression] -WebPart <WebPartInfo> [<CommonParameters>]
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

### -WebPart
{{Fill WebPart Description}}

```yaml
Type: WebPartInfo
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### CMS.PortalEngine.WebPartInfo

## OUTPUTS

### CMS.FormEngine.FormFieldInfo[]

## NOTES

## RELATED LINKS
