---
external help file: PoshKentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Remove-CMSPageTemplateCategory

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

### None (Default)
```
Remove-CMSPageTemplateCategory [-WhatIf] [-Confirm] [<CommonParameters>]
```

### PageTemplateCategory
```
Remove-CMSPageTemplateCategory -PageTemplateCategory <PageTemplateCategoryInfo> [-WhatIf] [-Confirm]
 [<CommonParameters>]
```

### Category Name
```
Remove-CMSPageTemplateCategory [-CategoryName] <String> [-Recurse] [-RegularExpression] [-WhatIf] [-Confirm]
 [<CommonParameters>]
```

### Path
```
Remove-CMSPageTemplateCategory -CategoryPath <String> [-Recurse] [-WhatIf] [-Confirm] [<CommonParameters>]
```

### ID
```
Remove-CMSPageTemplateCategory [-ID] <Int32[]> [-Recurse] [-WhatIf] [-Confirm] [<CommonParameters>]
```

### Page Template
```
Remove-CMSPageTemplateCategory -PageTemplate <PageTemplateInfo> [-WhatIf] [-Confirm] [<CommonParameters>]
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
Aliases: DisplayName, Name

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CategoryPath
{{Fill CategoryPath Description}}

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

### -ID
{{Fill ID Description}}

```yaml
Type: Int32[]
Parameter Sets: ID
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PageTemplate
{{Fill PageTemplate Description}}

```yaml
Type: PageTemplateInfo
Parameter Sets: Page Template
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
Parameter Sets: PageTemplateCategory
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Recurse
{{Fill Recurse Description}}

```yaml
Type: SwitchParameter
Parameter Sets: Category Name, Path, ID
Aliases:

Required: False
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RegularExpression
{{Fill RegularExpression Description}}

```yaml
Type: SwitchParameter
Parameter Sets: Category Name
Aliases: Regex

Required: False
Position: 3
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

### CMS.PortalEngine.PageTemplateCategoryInfo

### CMS.PortalEngine.PageTemplateInfo

## OUTPUTS

### System.Object
## NOTES

## RELATED LINKS
