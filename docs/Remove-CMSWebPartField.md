---
external help file: PoshKentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Remove-CMSWebPartField

## SYNOPSIS
{{Fill in the Synopsis}}

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
{{Fill in the Description}}

## EXAMPLES

### Example 1
```powershell
PS C:\> {{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

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

### -Field
{{Fill Field Description}}

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
{{Fill Name Description}}

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
{{Fill RegularExpression Description}}

```yaml
Type: SwitchParameter
Parameter Sets: Name
Aliases: Regex

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WebPart
{{Fill WebPart Description}}

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

### CMS.PortalEngine.WebPartInfo

## OUTPUTS

### System.Object
## NOTES

## RELATED LINKS
