---
external help file: PoshKentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Get-CMSWebPart

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

### None (Default)
```
Get-CMSWebPart [<CommonParameters>]
```

### Category Name
```
Get-CMSWebPart -CategoryName <String> [-RegularExpression] [<CommonParameters>]
```

### Field
```
Get-CMSWebPart -Field <FormFieldInfo> [<CommonParameters>]
```

### Name
```
Get-CMSWebPart [-RegularExpression] [-WebPartName] <String> [<CommonParameters>]
```

### Path
```
Get-CMSWebPart -WebPartPath <String> [<CommonParameters>]
```

### Category
```
Get-CMSWebPart [-WebPartCategory] <WebPartCategoryInfo> [<CommonParameters>]
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

### -WebPartCategory
{{Fill WebPartCategory Description}}

```yaml
Type: WebPartCategoryInfo
Parameter Sets: Category
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -WebPartName
{{Fill WebPartName Description}}

```yaml
Type: String
Parameter Sets: Name
Aliases: Name

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -WebPartPath
{{Fill WebPartPath Description}}

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### CMS.FormEngine.FormFieldInfo

### System.String

### CMS.PortalEngine.WebPartCategoryInfo

## OUTPUTS

### CMS.PortalEngine.WebPartInfo[]

## NOTES

## RELATED LINKS
