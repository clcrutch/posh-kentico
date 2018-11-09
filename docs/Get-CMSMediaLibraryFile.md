---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Get-CMSMediaLibraryFile

## SYNOPSIS
Gets a list of library files.

## SYNTAX

### None (Default)
```
Get-CMSMediaLibraryFile [-Extension <String>] [-FilePath <String>] [<CommonParameters>]
```

### Object
```
Get-CMSMediaLibraryFile [-MediaLibrary] <MediaLibraryInfo> [-Extension <String>] [-FilePath <String>] [-Exact]
 [<CommonParameters>]
```

### Property
```
Get-CMSMediaLibraryFile [-LibraryID] <Int32> [-Extension <String>] [-FilePath <String>] [-Exact]
 [<CommonParameters>]
```

### ID
```
Get-CMSMediaLibraryFile [-Extension <String>] [-FilePath <String>] [-ID] <Int32[]> [<CommonParameters>]
```

## DESCRIPTION
Gets a list of library files based off of the provided input.

This cmdlet returns the library files when the -PassThru switch is used.

## EXAMPLES

### EXAMPLE 1
```
Get-CMSMediaLibraryFile -MediaLibrary $library -Extension ".png" -FilePath "NewFolder"
```

### EXAMPLE 2
```
Get-CMSMediaLibraryFile -MediaLibrary $library -FilePath "NewFolder/Image.png" -EXACT
```

### EXAMPLE 3
```
$library | Get-CMSMediaLibraryFile -Extension ".png" -FilePath "NewFolder"
```

### EXAMPLE 4
```
$library | Get-CMSMediaLibraryFile -FilePath "NewFolder/Image.png" -EXACT
```

### EXAMPLE 5
```
Get-CMSMediaLibraryFile -LibraryID 1 -Extension ".png" -FilePath "NewFolder"
```

### EXAMPLE 6
```
Get-CMSMediaLibraryFile -LibraryID 1 -FilePath "NewFolder/Image.png" -EXACT
```

### EXAMPLE 7
```
Get-CMSMediaLibraryFile -ID 1,3
```

## PARAMETERS

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### CMS.MediaLibrary.MediaLibraryInfo
The associalted library for the library files to retrieve.

### System.Int32
The library ID of the library files to retrive.

## OUTPUTS

### CMS.MediaLibrary.MediaFileInfo[]
## NOTES

## RELATED LINKS
