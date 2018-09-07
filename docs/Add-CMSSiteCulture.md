---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Add-CMSSiteCulture

## SYNOPSIS
Adds a culture to a specified site.

## SYNTAX

### Object
```
Add-CMSSiteCulture [-SiteName] <String> [-SiteToAdd] <SiteInfo> [-CultureCode] <String> [<CommonParameters>]
```

### Property
```
Add-CMSSiteCulture [-SiteName] <String> [-Exact] [-CultureCode] <String> [<CommonParameters>]
```

### ID
```
Add-CMSSiteCulture [-SiteName] <String> [-ID] <Int32[]> [-CultureCode] <String> [<CommonParameters>]
```

## DESCRIPTION
Adds a culture to a specified site based off of the provided input.

This cmdlet returns the newly modified site when the -PassThru switch is used.

## EXAMPLES

### EXAMPLE 1
```
Add-CMSSiteCulture -SiteName "*bas*" -CultureCode "cul"
```

### EXAMPLE 2
```
Add-CMSSiteCulture -SiteName "basic" -EXACT -CultureCode "cul"
```

### EXAMPLE 3
```
$site | Add-CMSSiteCulture -CultureCode "cul"
```

### EXAMPLE 4
```
Add-CMSSiteCulture -ID 1,2,3 -CultureCode "cul"
```

## PARAMETERS

### -SiteName
The site name for the site.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SiteToAdd
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

### -Exact
If set, the match is exact, else the match performs a contains for site name.

```yaml
Type: SwitchParameter
Parameter Sets: Property
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -ID
The IDs of the site.

```yaml
Type: Int32[]
Parameter Sets: ID
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CultureCode
The IDs of the site.

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
A reference to the site.

## OUTPUTS

## NOTES

## RELATED LINKS
