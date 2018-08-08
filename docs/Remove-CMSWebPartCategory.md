---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Remove-CMSWebPartCategory

## SYNOPSIS
Deletes the web part categories selected by the provided input.

## SYNTAX

### Category Name
```
Remove-CMSWebPartCategory [-CategoryName] <String> [-Exact] [-WhatIf] [-Confirm] [<CommonParameters>]
```

### ID
```
Remove-CMSWebPartCategory [-ID] <Int32[]> [-WhatIf] [-Confirm] [<CommonParameters>]
```

### Web Part Category
```
Remove-CMSWebPartCategory [-WebPartCategory] <WebPartCategoryInfo> [-WhatIf] [-Confirm] [<CommonParameters>]
```

## DESCRIPTION
Deletes the web part categories selected by the provided input.
This command automatically initializes the connection to Kentico if not already initialized.

This command deletes the webpart categories that match the criteria.

## EXAMPLES

### EXAMPLE 1
```
Get-CMSWebPartCategory | Remove-CMSWebPartCategory
```

### EXAMPLE 2
```
Remove-CMSWebPartCategory bas
```

### EXAMPLE 3
```
Remove-CMSWebPartCategory basic -Exact
```

### EXAMPLE 4
```
Remove-CMSWebPartCategory -ID 5,304,5
```

## PARAMETERS

### -CategoryName
The category name, display name, or path of the webpart category.

```yaml
Type: String
Parameter Sets: Category Name
Aliases: DisplayName, Name, Path

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Exact
If set, the match is exact,

else the match performs a contains for display name and category name and starts with for path.

```yaml
Type: SwitchParameter
Parameter Sets: Category Name
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

### -WebPartCategory
A reference to the WebPart category to delete.

```yaml
Type: WebPartCategoryInfo
Parameter Sets: Web Part Category
Aliases: Category

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
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

### System.String
The category name, display name, or path of the webpart category.

### CMS.PortalEngine.WebPartCategoryInfo
A reference to the WebPart category to delete.

## OUTPUTS

## NOTES

## RELATED LINKS
