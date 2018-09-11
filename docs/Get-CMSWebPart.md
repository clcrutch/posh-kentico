---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Get-CMSWebPart

## SYNOPSIS
Gets the web parts selected by the provided input.

## SYNTAX

### None (Default)
```
Get-CMSWebPart [<CommonParameters>]
```

### Category Name
```
Get-CMSWebPart -CategoryName <String> [-RegularExpression] [<CommonParameters>]
```

### Field
```
Get-CMSWebPart -Field <FormFieldInfo> [<CommonParameters>]
```

### Name
```
Get-CMSWebPart [-RegularExpression] [-WebPartName] <String> [<CommonParameters>]
```

### Path
```
Get-CMSWebPart -WebPartPath <String> [<CommonParameters>]
```

### Category
```
Get-CMSWebPart [-WebPartCategory] <WebPartCategoryInfo> [<CommonParameters>]
```

## DESCRIPTION
Gets the web parts selected by the provided input.

This command automatically initializes the connection to Kentico if not already initialized.

Without parameters, this command returns all webparts.

With parameters, this command returns the webparts that match the criteria.

## EXAMPLES

### EXAMPLE 1
```
Get-CMSWebPart
```

### EXAMPLE 2
```
Get-CMSWebPartCategory | Get-CMSWebPart
```

### EXAMPLE 3
```
Get-CMSWebPart -Category *test*
```

### EXAMPLE 4
```
Get-CMSWebPart -WebPartName *webpartname*
```

### EXAMPLE 5
```
Get-CMSWebPart -Path /path/to/webpart
```

## PARAMETERS

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

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
