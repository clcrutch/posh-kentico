---
external help file: PoshKentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Add-CMSSiteCulture

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

### Object
```
Add-CMSSiteCulture [-SiteName] <String> [-SiteToAdd] <SiteInfo> [-CultureCode] <String> [<CommonParameters>]
```

### Property
```
Add-CMSSiteCulture [-SiteName] <String> [-Exact] [-CultureCode] <String> [<CommonParameters>]
```

### ID
```
Add-CMSSiteCulture [-SiteName] <String> [-ID] <Int32[]> [-CultureCode] <String> [<CommonParameters>]
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

### -CultureCode
{{Fill CultureCode Description}}

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Exact
{{Fill Exact Description}}

```yaml
Type: SwitchParameter
Parameter Sets: Property
Aliases:

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
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SiteName
{{Fill SiteName Description}}

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SiteToAdd
{{Fill SiteToAdd Description}}

```yaml
Type: SiteInfo
Parameter Sets: Object
Aliases: Site

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### CMS.SiteProvider.SiteInfo

## OUTPUTS

### System.Object
## NOTES

## RELATED LINKS
