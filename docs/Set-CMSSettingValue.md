---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Set-CMSSettingValue

## SYNOPSIS
Sets the setting values by the provided setting key.

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
Sets the setting values by the provided setting key.

## EXAMPLES

### EXAMPLE 1
```
$site | Set-CMSSettingValue -Key "my key" -Value "new val"
```

### EXAMPLE 2
```
Set-CMSSettingValue -SiteName "my site" -Key "my key" -Value "new val"
```

## PARAMETERS

### -Site
A reference to the site to set setting for.

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
The site name of the site to set setting for.

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

### -Key
The key of the setting to set value for.

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

### -Value
The new value of the setting to set value for.

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
A reference to the site to set setting for.

### System.String
The site name of the site to set setting for.

## OUTPUTS

## NOTES

## RELATED LINKS
