---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Set-CMSWebPartField

## SYNOPSIS
Sets a web part field.

## SYNTAX

```
Set-CMSWebPartField -Field <FormFieldInfo> [-PassThru] [<CommonParameters>]
```

## DESCRIPTION
Sets a web part field.

This command automatically initializes the connection to Kentico if not already initialized.

## EXAMPLES

### EXAMPLE 1
```
$webPartField | Set-CMSWebPartField
```

### EXAMPLE 2
```
$webPartField | Set-CMSWebPartField -PassThru
```

## PARAMETERS

### -Field
The field to set in Kentico.

```yaml
Type: FormFieldInfo
Parameter Sets: (All)
Aliases: Property

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -PassThru
Tell the cmdlet to return the web part.

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

### CMS.FormEngine.FormFieldInfo
The field to set in Kentico.

## OUTPUTS

### CMS.FormEngine.FormFieldInfo

## NOTES

## RELATED LINKS
