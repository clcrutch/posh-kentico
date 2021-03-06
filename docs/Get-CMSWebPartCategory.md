---
external help file: PoshKentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Get-CMSWebPartCategory

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

### None (Default)
```
Get-CMSWebPartCategory [<CommonParameters>]
```

### Parent Category
```
Get-CMSWebPartCategory [-ParentControlCategory] <WebPartCategoryInfo> [-Recurse] [<CommonParameters>]
```

### Category Name
```
Get-CMSWebPartCategory [-Recurse] [-CategoryName] <String> [-RegularExpression] [<CommonParameters>]
```

### ID
```
Get-CMSWebPartCategory [-Recurse] [-ID] <Int32[]> [<CommonParameters>]
```

### Path
```
Get-CMSWebPartCategory [-Recurse] -CategoryPath <String> [<CommonParameters>]
```

### Control
```
Get-CMSWebPartCategory -Control <WebPartInfo> [<CommonParameters>]
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

### -Control
{{Fill Control Description}}

```yaml
Type: WebPartInfo
Parameter Sets: Control
Aliases: WebPart, Widget

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -ID
{{Fill ID Description}}

```yaml
Type: Int32[]
Parameter Sets: ID
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ParentControlCategory
{{Fill ParentControlCategory Description}}

```yaml
Type: WebPartCategoryInfo
Parameter Sets: Parent Category
Aliases: Parent, ParentCategory, ParentWebPartCategory, ParentWidgetCategory

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Recurse
{{Fill Recurse Description}}

```yaml
Type: SwitchParameter
Parameter Sets: Parent Category, Category Name, ID, Path
Aliases:

Required: False
Position: Named
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
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### CMS.PortalEngine.WebPartCategoryInfo

### CMS.PortalEngine.WebPartInfo

## OUTPUTS

### CMS.PortalEngine.WebPartCategoryInfo[]

## NOTES

## RELATED LINKS
