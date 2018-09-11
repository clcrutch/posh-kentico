---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Get-CMSSettingValue

## SYNOPSIS
Gets the setting values by the provided setting key.

## SYNTAX

### None (Default)
```
Get-CMSSettingValue [-Key] <String> [<CommonParameters>]
```

### Object
```
Get-CMSSettingValue [-Site] <SiteInfo> [-Key] <String> [<CommonParameters>]
```

### Property
```
Get-CMSSettingValue [-SiteName] <String> [-Key] <String> [<CommonParameters>]
```

## DESCRIPTION
Gets the setting values by the provided setting key.

## EXAMPLES

### EXAMPLE 1
```
$site | Get-CMSSettingValue -Key "my key"
```

### EXAMPLE 2
```
Get-CMSSettingValue -SiteName "my site" -Key "my key"
```

## PARAMETERS

### -Site
A reference to the site to get setting from.

```yaml
Type: SiteInfo
Parameter Sets: Object
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -SiteName
The site name of the site to get setting from.

```yaml
Type: String
Parameter Sets: Property
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Key
The key of the setting to get value from.

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### CMS.SiteProvider.SiteInfo
A reference to the site to get setting from.

### System.String
The site name of the site to get setting from.

## OUTPUTS

## NOTES

## RELATED LINKS
