---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Stop-CMSSite

## SYNOPSIS
Stops a site.

## SYNTAX

### Object
```
Stop-CMSSite [-SiteToStart] <SiteInfo> [<CommonParameters>]
```

### Property
```
Stop-CMSSite [-SiteName] <String> [-Exact] [<CommonParameters>]
```

### ID
```
Stop-CMSSite [-ID] <Int32[]> [<CommonParameters>]
```

## DESCRIPTION
Stops a site.

## EXAMPLES

### EXAMPLE 1
```
Stop-CMSSite -SiteName "bas"
```

### EXAMPLE 2
```
Stop-CMSSite -Site "basic" -EXACT
```

### EXAMPLE 3
```
$site| Stop-CMSSite
```

### EXAMPLE 4
```
Stop-CMSSite -ID 1,2,3
```

## PARAMETERS

### -SiteToStart
A reference to the site to stop.

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

### -SiteName
The site name for the site to stop.

Site name cannot be blank.

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

### -Exact
If set, the match is exact,

else the match performs a contains for site name.

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
The IDs of the web part category to stop.

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### CMS.SiteProvider.SiteInfo
A reference to the site to stop.

## OUTPUTS

## NOTES

## RELATED LINKS
