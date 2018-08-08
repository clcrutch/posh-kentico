---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Remove-CMSSite

## SYNOPSIS
Deletes a site.

## SYNTAX

### Object
```
Remove-CMSSite [-SiteToRemove] <SiteInfo> [-WhatIf] [-Confirm] [<CommonParameters>]
```

### Property
```
Remove-CMSSite [-SiteName] <String> [-Exact] [-WhatIf] [-Confirm] [<CommonParameters>]
```

### ID
```
Remove-CMSSite [-ID] <Int32[]> [-WhatIf] [-Confirm] [<CommonParameters>]
```

## DESCRIPTION
Deletes a site.

## EXAMPLES

### EXAMPLE 1
```
Remove-CMSSite -SiteName "bas"
```

### EXAMPLE 2
```
Remove-CMSSite -SiteName "basic" -Exact
```

### EXAMPLE 3
```
$site | Remove-CMSSite
```

### EXAMPLE 4
```
Remove-CMSSite -ID 1,2,3
```

## PARAMETERS

### -SiteToRemove
A reference to the site to remove.

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
The site name for the site to remove.

Site name cannot be blank.

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
The IDs of the web part category to delete.

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

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs.
The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

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

### CMS.SiteProvider.SiteInfo
A reference to the site to remove.

### System.String
The site name for the site to remove.

Site name cannot be blank.

## OUTPUTS

## NOTES

## RELATED LINKS
