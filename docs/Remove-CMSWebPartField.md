---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Remove-CMSWebPartField

## SYNOPSIS
Remove the web part fields selected by the provided input.

## SYNTAX

### Field
```
Remove-CMSWebPartField -Field <FormFieldInfo> [-WhatIf] [-Confirm] [<CommonParameters>]
```

### Name
```
Remove-CMSWebPartField [-Name] <String> [-RegularExpression] -WebPart <WebPartInfo> [-WhatIf] [-Confirm]
 [<CommonParameters>]
```

### No Name
```
Remove-CMSWebPartField -WebPart <WebPartInfo> [-WhatIf] [-Confirm] [<CommonParameters>]
```

## DESCRIPTION
Remove the web part fields selected by the provided input.

This command automatically initializes the connection to Kentico if not already initialized.

## EXAMPLES

### EXAMPLE 1
```
$webPart | Remove-CMSWebPartField
```

### EXAMPLE 2
```
$webPart | Remove-CMSWebPartField -Name Test*
```

### EXAMPLE 3
```
$webPartField | Remove-WebPartField
```

## PARAMETERS

### -Field
The field to remove from Kentico.

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
Parameter Sets: Name, No Name
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

### CMS.FormEngine.FormFieldInfo
The field to remove from Kentico.

### CMS.PortalEngine.WebPartInfo
The web part to get the fields for.

## OUTPUTS

### CMS.FormEngine.FormFieldInfo[]
## NOTES

## RELATED LINKS
