---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Remove-CMSWebPart

## SYNOPSIS
Removes the web parts selected by the provided input.

## SYNTAX

### None (Default)
```
Remove-CMSWebPart [-WhatIf] [-Confirm] [<CommonParameters>]
```

### WebPart
```
Remove-CMSWebPart -WebPart <WebPartInfo> [-WhatIf] [-Confirm] [<CommonParameters>]
```

### Category Name
```
Remove-CMSWebPart -CategoryName <String> [-RegularExpression] [-WhatIf] [-Confirm] [<CommonParameters>]
```

### Field
```
Remove-CMSWebPart -Field <FormFieldInfo> [-WhatIf] [-Confirm] [<CommonParameters>]
```

### Name
```
Remove-CMSWebPart [-RegularExpression] [-WebPartName] <String> [-WhatIf] [-Confirm] [<CommonParameters>]
```

### Path
```
Remove-CMSWebPart -WebPartPath <String> [-WhatIf] [-Confirm] [<CommonParameters>]
```

### Category
```
Remove-CMSWebPart [-WebPartCategory] <WebPartCategoryInfo> [-WhatIf] [-Confirm] [<CommonParameters>]
```

## DESCRIPTION
Removes the web parts selected by the provided input.

This command automatically initializes the connection to Kentico if not already initialized.

Without parameters, this command removes all webparts.

With parameters, this command removes the webparts that match the criteria.

## EXAMPLES

### EXAMPLE 1
```
Remove-CMSWebPart
```

### EXAMPLE 2
```
Get-CMSWebPartCategory | Remove-CMSWebPart
```

### EXAMPLE 3
```
Remove-CMSWebPart -Category *test*
```

### EXAMPLE 4
```
Remove-CMSWebPart -WebPartName *webpartname*
```

### EXAMPLE 5
```
Remove-CMSWebPart -Path /path/to/webpart
```

### EXAMPLE 6
```
Remove web parts through the pipeline.
```

## PARAMETERS

### -WebPart
The web part to remove from the system.

```yaml
Type: WebPartInfo
Parameter Sets: WebPart
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -CategoryName
The category name or display name of the webpart category that contains the webparts.

```yaml
Type: String
Parameter Sets: Category Name
Aliases: Category

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Field
The field to get the associated web part for.

```yaml
Type: FormFieldInfo
Parameter Sets: Field
Aliases: Property

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -RegularExpression
Indicates if the CategoryName or Name supplied is a regular expression.

```yaml
Type: SwitchParameter
Parameter Sets: Category Name, Name
Aliases: Regex

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -WebPartName
The name or display name of the webpart.

```yaml
Type: String
Parameter Sets: Name
Aliases: Name

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -WebPartPath
The path to the webpart.

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

### -WebPartCategory
An object that represents the webpart category that contains the webparts.

```yaml
Type: WebPartCategoryInfo
Parameter Sets: Category
Aliases:

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

### CMS.PortalEngine.WebPartInfo
The web part to remove from the system.

### CMS.FormEngine.FormFieldInfo
The field to get the associated web part for.

### System.String
The name or display name of the webpart.

### CMS.PortalEngine.WebPartCategoryInfo
An object that represents the webpart category that contains the webparts.

## OUTPUTS

### CMS.PortalEngine.WebPartInfo[]

## NOTES

## RELATED LINKS
