---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Get-CMSWebPartField

## SYNOPSIS
Gets the web part fields selected by the provided input.

## SYNTAX

### No Name (Default)
```
Get-CMSWebPartField -WebPart <WebPartInfo> [<CommonParameters>]
```

### Name
```
Get-CMSWebPartField [-Name] <String> [-RegularExpression] -WebPart <WebPartInfo> [<CommonParameters>]
```

## DESCRIPTION
Gets the web part fields selected by the provided input.

This command automatically initializes the connection to Kentico if not already initialized.

## EXAMPLES

### EXAMPLE 1
```
$webPart | Get-CMSWebPartField
```

### EXAMPLE 2
```
$webPart | Get-CMSWebPartField -Name Test*
```

## PARAMETERS

### -Name
The name for the field to search for.

```yaml
Type: String
Parameter Sets: Name
Aliases: Caption

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RegularExpression
Indicates if the CategoryName supplied is a regular expression.

```yaml
Type: SwitchParameter
Parameter Sets: Name
Aliases: Regex

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -WebPart
The web part to get the fields for.

```yaml
Type: WebPartInfo
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### CMS.PortalEngine.WebPartInfo
The web part to get the fields for.

## OUTPUTS

### CMS.FormEngine.FormFieldInfo[]
## NOTES

## RELATED LINKS
