---
external help file: PoshKentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# New-CMSPageTemplate

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

### Path (Default)
```
New-CMSPageTemplate [[-Layout] <String>] [[-IconClass] <String>] [[-CSS] <String>] [[-IsReusable] <Boolean>]
 [-DisplayName] <String> [-FileName] <String> [-Path] <String> [-PassThru] [<CommonParameters>]
```

### Category
```
New-CMSPageTemplate [[-Layout] <String>] [[-IconClass] <String>] [[-CSS] <String>] [[-IsReusable] <Boolean>]
 [-DisplayName] <String> [-FileName] <String> [-Name] <String> [-PassThru]
 -PageTemplateCategory <PageTemplateCategoryInfo> [<CommonParameters>]
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

### -CSS
{{Fill CSS Description}}

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 4
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisplayName
{{Fill DisplayName Description}}

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FileName
{{Fill FileName Description}}

```yaml
Type: String
Parameter Sets: (All)
Aliases: File

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IconClass
{{Fill IconClass Description}}

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 5
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsReusable
{{Fill IsReusable Description}}

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases:

Required: False
Position: 3
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Layout
{{Fill Layout Description}}

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 6
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
{{Fill Name Description}}

```yaml
Type: String
Parameter Sets: Category
Aliases: CodeName

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PageTemplateCategory
{{Fill PageTemplateCategory Description}}

```yaml
Type: PageTemplateCategoryInfo
Parameter Sets: Category
Aliases: Category, Parent

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -PassThru
{{Fill PassThru Description}}

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
{{Fill Path Description}}

```yaml
Type: String
Parameter Sets: Path
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### CMS.PortalEngine.PageTemplateCategoryInfo

## OUTPUTS

### CMS.PortalEngine.PageTemplateInfo[]

## NOTES

## RELATED LINKS
