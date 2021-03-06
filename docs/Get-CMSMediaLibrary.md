---
external help file: PoshKentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Get-CMSMediaLibrary

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

### None (Default)
```
Get-CMSMediaLibrary [<CommonParameters>]
```

### Object
```
Get-CMSMediaLibrary [[-Site] <SiteInfo>] [<CommonParameters>]
```

### Library Name
```
Get-CMSMediaLibrary [-SiteID] <Int32> [-LibraryName] <String> [-Exact] [<CommonParameters>]
```

### ID
```
Get-CMSMediaLibrary [-ID] <Int32[]> [<CommonParameters>]
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

### -Exact
{{Fill Exact Description}}

```yaml
Type: SwitchParameter
Parameter Sets: Library Name
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

### -LibraryName
{{Fill LibraryName Description}}

```yaml
Type: String
Parameter Sets: Library Name
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Site
{{Fill Site Description}}

```yaml
Type: SiteInfo
Parameter Sets: Object
Aliases:

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -SiteID
{{Fill SiteID Description}}

```yaml
Type: Int32
Parameter Sets: Library Name
Aliases:

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

### System.Int32

## OUTPUTS

### CMS.MediaLibrary.MediaLibraryInfo[]

## NOTES

## RELATED LINKS
