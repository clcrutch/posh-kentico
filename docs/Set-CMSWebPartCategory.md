---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Set-CMSWebPartCategory

## SYNOPSIS
Sets a web part category.

## SYNTAX

```
Set-CMSWebPartCategory [-WebPartCategory] <WebPartCategoryInfo> [-PassThru] [<CommonParameters>]
```

## DESCRIPTION
Sets a web part category.

## EXAMPLES

### EXAMPLE 1
```
$webPartCategory | Set-CMSWebPartCategory
```

### EXAMPLE 2
```
$webPartCategory | Set-CMSWebPartCategory -Passthru
```

## PARAMETERS

### -WebPartCategory
A reference to the WebPart category to update.

```yaml
Type: WebPartCategoryInfo
Parameter Sets: (All)
Aliases: Category

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
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

### CMS.PortalEngine.WebPartCategoryInfo
A reference to the WebPart category to update.

## OUTPUTS

### CMS.PortalEngine.WebPartCategoryInfo
## NOTES

## RELATED LINKS
