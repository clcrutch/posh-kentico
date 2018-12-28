---
external help file: PoshKentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Set-CMSSettingValue

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

### None (Default)
```
Set-CMSSettingValue [-Key] <String> [-Value] <Object> [<CommonParameters>]
```

### Object
```
Set-CMSSettingValue [[-Site] <SiteInfo>] [-Key] <String> [-Value] <Object> [<CommonParameters>]
```

### Property
```
Set-CMSSettingValue [[-SiteName] <String>] [-Key] <String> [-Value] <Object> [<CommonParameters>]
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

### -Key
{{Fill Key Description}}

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Site
{{Fill Site Description}}

```yaml
Type: SiteInfo
Parameter Sets: Object
Aliases:

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -SiteName
{{Fill SiteName Description}}

```yaml
Type: String
Parameter Sets: Property
Aliases:

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Value
{{Fill Value Description}}

```yaml
Type: Object
Parameter Sets: (All)
Aliases:

Required: True
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### CMS.SiteProvider.SiteInfo

### System.String

## OUTPUTS

### System.Object
## NOTES

## RELATED LINKS
