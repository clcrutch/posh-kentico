---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Remove-CMSSiteDomainAlias

## SYNOPSIS
Removes a domain alias to a specified site.

## SYNTAX

### Object
```
Remove-CMSSiteDomainAlias [-SiteToRemove] <SiteInfo> [-AliasName] <String> [-WhatIf] [-Confirm]
 [<CommonParameters>]
```

### Property
```
Remove-CMSSiteDomainAlias [-SiteName] <String> [-Exact] [-AliasName] <String> [-WhatIf] [-Confirm]
 [<CommonParameters>]
```

### ID
```
Remove-CMSSiteDomainAlias [-ID] <Int32[]> [-AliasName] <String> [-WhatIf] [-Confirm] [<CommonParameters>]
```

## DESCRIPTION
Removes a domain alias to a specified site based off of the provided input.

## EXAMPLES

### EXAMPLE 1
```
Remove-CMSSiteDomainAlias -SiteName "*bas*" -AliasName "alias"
```

### EXAMPLE 2
```
Remove-CMSSiteDomainAlias -SiteName "basic" -EXACT -AliasName "alias"
```

### EXAMPLE 3
```
$site | Remove-CMSSiteDomainAlias -AliasName "alias"
```

### EXAMPLE 4
```
Remove-CMSSiteDomainAlias -ID 1,2,3 -AliasName "alias"
```

## PARAMETERS

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

### -AliasName
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
A reference to the site.

## OUTPUTS

## NOTES

## RELATED LINKS
