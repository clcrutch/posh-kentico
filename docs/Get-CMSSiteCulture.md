---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Get-CMSSiteCulture

## SYNOPSIS
Gets the cultures of the specified site.

## SYNTAX

### Property
```
Get-CMSSiteCulture [-SiteName] <String> [<CommonParameters>]
```

### Object
```
Get-CMSSiteCulture [-SiteToWork] <SiteInfo> [<CommonParameters>]
```

## DESCRIPTION
Gets the cultures of the specified site based off of the provided input.

## EXAMPLES

### EXAMPLE 1
```
Get-CMSSiteCulture -SiteName "basic"
```

### EXAMPLE 2
```
$site | Get-CMSSiteCulture
```

## PARAMETERS

### -SiteName
The site name for the site.

```yaml
Type: String
Parameter Sets: Property
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SiteToWork
A reference to the site.

```yaml
Type: SiteInfo
Parameter Sets: Object
Aliases: Site

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### CMS.SiteProvider.SiteInfo
A reference to the site.

## OUTPUTS

### CMS.Localization.CultureInfo[]
## NOTES

## RELATED LINKS
