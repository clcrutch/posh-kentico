---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Remove-CMSMediaLibrary

## SYNOPSIS
Deletes a library.

## SYNTAX

### Media Library
```
Remove-CMSMediaLibrary [[-MediaLibrary] <MediaLibraryInfo>] [-WhatIf] [-Confirm] [<CommonParameters>]
```

### Object
```
Remove-CMSMediaLibrary [[-Site] <SiteInfo>] [-WhatIf] [-Confirm] [<CommonParameters>]
```

### Library Name
```
Remove-CMSMediaLibrary [-SiteID] <Int32> [-LibraryName] <String> [-Exact] [-WhatIf] [-Confirm]
 [<CommonParameters>]
```

### ID
```
Remove-CMSMediaLibrary [-ID] <Int32[]> [-WhatIf] [-Confirm] [<CommonParameters>]
```

## DESCRIPTION
Deletes a library.

## EXAMPLES

### EXAMPLE 1
```
Remove-CMSMediaLibrary -SiteID 1 -LibraryName tes
```

### EXAMPLE 2
```
Remove-CMSMediaLibrary -SiteID 1 -LibraryName test -Exact
```

### EXAMPLE 3
```
$library | Remove-CMSMediaLibrary
```

### EXAMPLE 4
```
Remove-CMSMediaLibrary -ID 1,2,3
```

## PARAMETERS

### -MediaLibrary
The associalted site for the library to retrieve.

```yaml
Type: MediaLibraryInfo
Parameter Sets: Media Library
Aliases:

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
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

### CMS.MediaLibrary.MediaLibraryInfo
The associalted site for the library to retrieve.

### CMS.SiteProvider.SiteInfo
The associalted site for the server to retrieve.

### System.Int32
The site name of the library to retrive.

## OUTPUTS

### CMS.MediaLibrary.MediaLibraryInfo[]
## NOTES

## RELATED LINKS
