---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Remove-CMSMediaLibraryFile

## SYNOPSIS
Removes a list of library files.

## SYNTAX

### Media File
```
Remove-CMSMediaLibraryFile -MediaFile <MediaFileInfo> [-Extension <String>] [-FilePath <String>] [-WhatIf]
 [-Confirm] [<CommonParameters>]
```

### Object
```
Remove-CMSMediaLibraryFile [-MediaLibrary] <MediaLibraryInfo> [-Extension <String>] [-FilePath <String>]
 [-Exact] [-WhatIf] [-Confirm] [<CommonParameters>]
```

### Property
```
Remove-CMSMediaLibraryFile [-LibraryID] <Int32> [-Extension <String>] [-FilePath <String>] [-Exact] [-WhatIf]
 [-Confirm] [<CommonParameters>]
```

### ID
```
Remove-CMSMediaLibraryFile [-Extension <String>] [-FilePath <String>] [-ID] <Int32[]> [-WhatIf] [-Confirm]
 [<CommonParameters>]
```

## DESCRIPTION
Removes a list of library files based off of the provided input.

## EXAMPLES

### EXAMPLE 1
```
$libraryFile | Remove-CMSMediaLibraryFile
```

### EXAMPLE 2
```
Remove-CMSMediaLibraryFile -MediaFile $libraryFile
```

### EXAMPLE 3
```
Remove-CMSMediaLibraryFile -MediaLibrary $library -Extension ".png" -FilePath "NewFolder"
```

### EXAMPLE 4
```
Remove-CMSMediaLibraryFile -MediaLibrary $library -Extension ".png" -FilePath "NewFolder" -EXACT
```

### EXAMPLE 5
```
$library | Remove-CMSMediaLibraryFile -Extension ".png" -FilePath "NewFolder"
```

### EXAMPLE 6
```
$library | Remove-CMSMediaLibraryFile -Extension ".png" -FilePath "NewFolder" -EXACT
```

### EXAMPLE 7
```
Remove-CMSMediaLibraryFile -LibraryID 1 -Extension ".png" -FilePath "NewFolder"
```

### EXAMPLE 8
```
Remove-CMSMediaLibraryFile -LibraryID 1 -Extension ".png" -FilePath "NewFolder" -EXACT
```

### EXAMPLE 9
```
Remove-CMSMediaLibraryFile -ID 1,3
```

## PARAMETERS

### -MediaFile
The field to remove from Kentico.

```yaml
Type: MediaFileInfo
Parameter Sets: Media File
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -MediaLibrary
The associalted library for the library files to retrieve.

```yaml
Type: MediaLibraryInfo
Parameter Sets: Object
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -LibraryID
The library ID of the library files to retrive.

```yaml
Type: Int32
Parameter Sets: Property
Aliases:

Required: True
Position: 0
Default value: 0
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Extension
The file extension of the library files to retrive.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FilePath
The file path of the library files to retrive.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Exact
If set, the match is exact, else the match performs a contains for file extension and file path.

```yaml
Type: SwitchParameter
Parameter Sets: Object, Property
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -ID
The IDs of the library file to retrieve.

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

### CMS.MediaLibrary.MediaFileInfo
The field to remove from Kentico.

### CMS.MediaLibrary.MediaLibraryInfo
The associalted library for the library files to retrieve.

### System.Int32
The library ID of the library files to retrive.

## OUTPUTS

### CMS.MediaLibrary.MediaFileInfo[]
## NOTES

## RELATED LINKS
