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

### None (Default)
```
Remove-CMSWebPartCategory [-WhatIf] [-Confirm] [<CommonParameters>]
```

### WebPartCategory
```
Remove-CMSWebPartCategory -WebPartCategory <WebPartCategoryInfo> [-WhatIf] [-Confirm] [<CommonParameters>]
```

### Category Name
```
Remove-CMSWebPartCategory [-CategoryName] <String> [-Recurse] [-RegularExpression] [-WhatIf] [-Confirm]
 [<CommonParameters>]
```

### Path
```
Remove-CMSWebPartCategory -CategoryPath <String> [-Recurse] [-WhatIf] [-Confirm] [<CommonParameters>]
```

### ID
```
Remove-CMSWebPartCategory [-ID] <Int32[]> [-Recurse] [-WhatIf] [-Confirm] [<CommonParameters>]
```

### Web Part
```
Remove-CMSWebPartCategory -WebPart <WebPartInfo> [-WhatIf] [-Confirm] [<CommonParameters>]
```

## DESCRIPTION
Deletes the web part categories selected by the provided input.

This command automatically initializes the connection to Kentico if not already initialized.

This command deletes the webpart categories that match the criteria.

## EXAMPLES

### EXAMPLE 1
```
Remove-CMSWebPartCategory
```

### EXAMPLE 2
```
Remove-CMSWebPartCategory *bas*
```

### EXAMPLE 3
```
Remove-CMSWebPartCategory basic
```

### EXAMPLE 4
```
Remove-CMSWebPartCategory -ID 5,304,5
```

### EXAMPLE 5
```
Remove-CMSWebPartCategory basic -Recurse
```

### EXAMPLE 6
```
$webPart | Remove-WebPartCategory
```

### EXAMPLE 7
```
$webPartCategory | Remove-WebPartCategory
```

## PARAMETERS

### -WebPartCategory
The web part category to remove from the system.

```yaml
Type: WebPartCategoryInfo
Parameter Sets: WebPartCategory
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -CategoryName
The category name or display name the webpart category.

```yaml
Type: String
Parameter Sets: Category Name
Aliases: DisplayName, Name

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CategoryPath
The path to get the web part category at.

```yaml
Type: String
Parameter Sets: Path
Aliases: Path

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ID
The IDs of the web part category to retrieve.

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

### -Recurse
Indiciates if the cmdlet should look recursively for web part categories.

```yaml
Type: SwitchParameter
Parameter Sets: Category Name, Path, ID
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -RegularExpression
Indicates if the CategoryName supplied is a regular expression.

```yaml
Type: SwitchParameter
Parameter Sets: Category Name
Aliases: Regex

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -WebPart
The webpart to get the web part category for.

```yaml
Type: WebPartInfo
Parameter Sets: Web Part
Aliases:

Required: True
Position: Named
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

### CMS.PortalEngine.WebPartCategoryInfo
The web part category to remove from the system.

### CMS.PortalEngine.WebPartInfo
The webpart to get the web part category for.

## OUTPUTS

## NOTES

## RELATED LINKS
