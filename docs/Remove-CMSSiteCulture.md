---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Remove-CMSSiteCulture

## SYNOPSIS
Removes a culture to a specified site.

## SYNTAX

### Object
```
Remove-CMSSiteCulture [-SiteName] <String> [-SiteToRemove] <SiteInfo> [-CultureCode] <String>
 [<CommonParameters>]
```

### Property
```
Remove-CMSSiteCulture [-SiteName] <String> [-Exact] [-CultureCode] <String> [<CommonParameters>]
```

### ID
```
Remove-CMSSiteCulture [-SiteName] <String> [-ID] <Int32[]> [-CultureCode] <String> [<CommonParameters>]
```

## DESCRIPTION
Removes a culture to a specified site based off of the provided input.

## EXAMPLES

### EXAMPLE 1
```
Remove-CMSSiteCulture -SiteName "*bas*" -CultureCode "cul"
```

### EXAMPLE 2
```
Remove-CMSSiteCulture -SiteName "basic" -EXACT -CultureCode "cul"
```

### EXAMPLE 3
```
$site | Remove-CMSSiteCulture
```

### EXAMPLE 4
```
Remove-CMSSiteCulture -ID 1,2,3
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

### -SiteToRemove
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
