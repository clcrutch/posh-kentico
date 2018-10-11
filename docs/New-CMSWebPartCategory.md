---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# New-CMSWebPartCategory

## SYNOPSIS
Creates a new web part category.

## SYNTAX

```
New-CMSWebPartCategory [[-DisplayName] <String>] [-Path] <String> [-ImagePath <String>] [-PassThru]
 [<CommonParameters>]
```

## DESCRIPTION
Creates a new web part category based off of the provided input.

This cmdlet returns the newly created web part category when the -PassThru switch is used.

This command automatically initializes the connection to Kentico if not already initialized.

## EXAMPLES

### EXAMPLE 1
```
New-CMSWebPartCategory -Path /Test/Test1
```

### EXAMPLE 2
```
New-CMSWebPartCategory -Path /Test/Test1 -DisplayName "My Test Category"
```

## PARAMETERS

### -DisplayName
The display name for the newly created web part category.

If null, then the name portion of the path is used for the display name.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
The path to create the new web part category at.

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

### -ImagePath
The path for the icon for the newly created web part category.

```yaml
Type: String
Parameter Sets: (All)
Aliases: IconPath

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PassThru
Tell the cmdlet to return the newly created web part category.

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

## OUTPUTS

### CMS.PortalEngine.WebPartCategoryInfo[]
## NOTES

## RELATED LINKS
