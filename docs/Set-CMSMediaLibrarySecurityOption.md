---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Set-CMSMediaLibrarySecurityOption

## SYNOPSIS
Sets the media libraries security option by the provided input.

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
Sets the media libraries security option by the provided input.

Without parameters, this command sets all libraries.

With parameters, this command sets the libraries that match the criteria.

## EXAMPLES

### EXAMPLE 1
```
Set-CMSMediaLibrarySecurityOption -SecurityProperty Access -SecurityAccess AllUsers
```

### EXAMPLE 2
```
Set-CMSMediaLibrarySecurityOption -SiteID 1 -DisplayName bas -SecurityProperty Access -SecurityAccess AllUsers
```

### EXAMPLE 3
```
Set-CMSMediaLibrarySecurityOption -SiteID 1 -DisplayName "test" -Exact -SecurityProperty Access -SecurityAccess AllUsers
```

### EXAMPLE 4
```
Set-CMSMediaLibrarySecurityOption -Site $site -DisplayName bas -SecurityProperty Access -SecurityAccess AllUsers
```

### EXAMPLE 5
```
Set-CMSMediaLibrarySecurityOption -Site $site -DisplayName "test" -Exact -SecurityProperty Access -SecurityAccess AllUsers
```

### EXAMPLE 6
```
Set-CMSMediaLibrarySecurityOption -ID 1,3 -SecurityProperty Access -SecurityAccess AllUsers
```

### EXAMPLE 7
```
Set-CMSMediaLibrarySecurityOption -MediaLibrary $library -SecurityProperty Access -SecurityAccess AllUsers
```

## PARAMETERS

### -MediaLibrary
The field to remove from Kentico.

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

### -SecurityProperty
The security property enum for the media library.

Possible values: Access, FileCreate, FolderCreate, FileDelete, FolderDelete, FileModify, FolderModify

```yaml
Type: SecurityPropertyEnum
Parameter Sets: (All)
Aliases:
Accepted values: Access, FileCreate, FolderCreate, FileDelete, FolderDelete, FileModify, FolderModify

Required: True
Position: Named
Default value: Access
Accept pipeline input: False
Accept wildcard characters: False
```

### -SecurityAccess
The security access enum for the media library.

Possible values: AllUsers, AuthenticatedUsers, AuthorizedRoles, GroupMembers, Nobody, Owner, GroupAdmin, GlobalAdmin

```yaml
Type: SecurityAccessEnum
Parameter Sets: (All)
Aliases:
Accepted values: AllUsers, AuthenticatedUsers, AuthorizedRoles, GroupMembers, Nobody, Owner, GroupAdmin, GlobalAdmin

Required: True
Position: Named
Default value: AllUsers
Accept pipeline input: False
Accept wildcard characters: False
```

### -Site
The associalted site for the server to retrieve.

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
The site name of the library to retrive.

```yaml
Type: Int32
Parameter Sets: Library Name
Aliases:

Required: True
Position: 0
Default value: 0
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -LibraryName
The library name of the library to retrive.

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

### -Exact
If set, the match is exact,else the match performs a contains for library name and category name and starts with for path.

```yaml
Type: SwitchParameter
Parameter Sets: Library Name
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -ID
The IDs of the library to retrieve.

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### CMS.MediaLibrary.MediaLibraryInfo
The field to remove from Kentico.

### CMS.SiteProvider.SiteInfo
The associalted site for the server to retrieve.

### System.Int32
The site name of the library to retrive.

## OUTPUTS

### CMS.MediaLibrary.MediaLibraryInfo[]
## NOTES

## RELATED LINKS
