---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Get-CMSWebPartCategory

## SYNOPSIS
Gets the web part categories selected by the provided input.

## SYNTAX

### None (Default)
```
Get-CMSWebPartCategory [-Recurse] [<CommonParameters>]
```

### Category Name
```
Get-CMSWebPartCategory [-CategoryName] <String> [-RegularExpression] [-Recurse] [<CommonParameters>]
```

### ID
```
Get-CMSWebPartCategory [-ID] <Int32[]> [-Recurse] [<CommonParameters>]
```

### Parent Category
```
Get-CMSWebPartCategory [-ParentWebPartCategory] <IWebPartCategory> [-Recurse] [<CommonParameters>]
```

## DESCRIPTION
Gets the web part categories selected by the provided input.
This command automatically initializes the connection to Kentico if not already initialized.

Without parameters, this command returns all webpart categories.

With parameters, this command returns the webpart categories that match the criteria.

## EXAMPLES

### EXAMPLE 1
```
Get-CMSWebPartCategory
```

### EXAMPLE 2
```
Get-CMSWebPartCategory *bas*
```

### EXAMPLE 3
```
Get-CMSWebPartCategory basic
```

### EXAMPLE 4
```
Get-CMSWebPartCategory -ID 5,304,5
```

## PARAMETERS

### -CategoryName
The category name or display name the webpart category.

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

### -ParentWebPartCategory
The webpart category that contains the webpart categories.

```yaml
Type: IWebPartCategory
Parameter Sets: Parent Category
Aliases: Parent, ParentCategory

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Recurse
Indiciates if the cmdlet should look recursively for web part categories.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String
The category name or display name the webpart category.

### PoshKentico.Core.Services.Development.WebParts.IWebPartCategory
The webpart category that contains the webpart categories.

## OUTPUTS

### CMS.PortalEngine.WebPartCategoryInfo
## NOTES

## RELATED LINKS
