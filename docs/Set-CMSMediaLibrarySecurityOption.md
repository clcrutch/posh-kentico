---
external help file: PoshKentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Set-CMSMediaLibrarySecurityOption

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

### Media Library
```
Set-CMSMediaLibrarySecurityOption [-MediaLibrary] <MediaLibraryInfo> -SecurityProperty <SecurityPropertyEnum>
 -SecurityAccess <SecurityAccessEnum> [<CommonParameters>]
```

### Object
```
Set-CMSMediaLibrarySecurityOption -SecurityProperty <SecurityPropertyEnum> -SecurityAccess <SecurityAccessEnum>
 [[-Site] <SiteInfo>] [<CommonParameters>]
```

### Library Name
```
Set-CMSMediaLibrarySecurityOption -SecurityProperty <SecurityPropertyEnum> -SecurityAccess <SecurityAccessEnum>
 [-SiteID] <Int32> [-LibraryName] <String> [-Exact] [<CommonParameters>]
```

### ID
```
Set-CMSMediaLibrarySecurityOption -SecurityProperty <SecurityPropertyEnum> -SecurityAccess <SecurityAccessEnum>
 [-ID] <Int32[]> [<CommonParameters>]
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

### -MediaLibrary
{{Fill MediaLibrary Description}}

```yaml
Type: MediaLibraryInfo
Parameter Sets: Media Library
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -SecurityAccess
{{Fill SecurityAccess Description}}

```yaml
Type: SecurityAccessEnum
Parameter Sets: (All)
Aliases:
Accepted values: AllUsers, AuthenticatedUsers, AuthorizedRoles, GroupMembers, Nobody, Owner, GroupAdmin, GlobalAdmin

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SecurityProperty
{{Fill SecurityProperty Description}}

```yaml
Type: SecurityPropertyEnum
Parameter Sets: (All)
Aliases:
Accepted values: Access, FileCreate, FolderCreate, FileDelete, FolderDelete, FileModify, FolderModify

Required: True
Position: Named
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

### CMS.MediaLibrary.MediaLibraryInfo

### CMS.SiteProvider.SiteInfo

### System.Int32

## OUTPUTS

### System.Object
## NOTES

## RELATED LINKS
