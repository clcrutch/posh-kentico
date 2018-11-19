---
external help file: PoshKentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Get-CMSPageTemplate

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

### None (Default)
```
Get-CMSPageTemplate [<CommonParameters>]
```

### Category Name
```
Get-CMSPageTemplate -CategoryName <String> [-RegularExpression] [<CommonParameters>]
```

### Name
```
Get-CMSPageTemplate [-RegularExpression] [-PageTemplateName] <String> [<CommonParameters>]
```

### Path
```
Get-CMSPageTemplate -PageTemplatePath <String> [<CommonParameters>]
```

### Category
```
Get-CMSPageTemplate [-PageTemplateCategory] <PageTemplateCategoryInfo> [<CommonParameters>]
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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String

### CMS.PortalEngine.PageTemplateCategoryInfo

## OUTPUTS

### CMS.PortalEngine.PageTemplateInfo[]

## NOTES

## RELATED LINKS
