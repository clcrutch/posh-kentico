---
external help file: PoshKentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Remove-CMSPageTemplate

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

### None (Default)
```
Remove-CMSPageTemplate [-WhatIf] [-Confirm] [<CommonParameters>]
```

### PageTemplate
```
Remove-CMSPageTemplate -PageTemplate <PageTemplateInfo> [-WhatIf] [-Confirm] [<CommonParameters>]
```

### Category Name
```
Remove-CMSPageTemplate -CategoryName <String> [-RegularExpression] [-WhatIf] [-Confirm] [<CommonParameters>]
```

### Name
```
Remove-CMSPageTemplate [-RegularExpression] [-PageTemplateName] <String> [-WhatIf] [-Confirm]
 [<CommonParameters>]
```

### Path
```
Remove-CMSPageTemplate -PageTemplatePath <String> [-WhatIf] [-Confirm] [<CommonParameters>]
```

### Category
```
Remove-CMSPageTemplate [-PageTemplateCategory] <PageTemplateCategoryInfo> [-WhatIf] [-Confirm]
 [<CommonParameters>]
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

### -CategoryName
{{Fill CategoryName Description}}

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

### -PageTemplate
{{Fill PageTemplate Description}}

```yaml
Type: PageTemplateInfo
Parameter Sets: PageTemplate
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -PageTemplateCategory
{{Fill PageTemplateCategory Description}}

```yaml
Type: PageTemplateCategoryInfo
Parameter Sets: Category
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -PageTemplateName
{{Fill PageTemplateName Description}}

```yaml
Type: String
Parameter Sets: Name
Aliases: Name

Required: True
Position: 1
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -PageTemplatePath
{{Fill PageTemplatePath Description}}

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

### -RegularExpression
{{Fill RegularExpression Description}}

```yaml
Type: SwitchParameter
Parameter Sets: Category Name, Name
Aliases: Regex

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

### CMS.PortalEngine.PageTemplateInfo

### System.String

### CMS.PortalEngine.PageTemplateCategoryInfo

## OUTPUTS

### System.Object
## NOTES

## RELATED LINKS
