---
external help file: PoshKentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Get-CMSWidgetCategory

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

### None (Default)
```
Get-CMSWidgetCategory [<CommonParameters>]
```

### Parent Category
```
Get-CMSWidgetCategory [-ParentControlCategory] <WidgetCategoryInfo> [-Recurse] [<CommonParameters>]
```

### Category Name
```
Get-CMSWidgetCategory [-Recurse] [-CategoryName] <String> [-RegularExpression] [<CommonParameters>]
```

### ID
```
Get-CMSWidgetCategory [-Recurse] [-ID] <Int32[]> [<CommonParameters>]
```

### Path
```
Get-CMSWidgetCategory [-Recurse] -CategoryPath <String> [<CommonParameters>]
```

### Control
```
Get-CMSWidgetCategory -Control <WidgetInfo> [<CommonParameters>]
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
Type: WidgetInfo
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
Type: WidgetCategoryInfo
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

### CMS.PortalEngine.WidgetCategoryInfo

### CMS.PortalEngine.WidgetInfo

## OUTPUTS

### CMS.PortalEngine.WidgetCategoryInfo[]

## NOTES

## RELATED LINKS
