---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# New-CMSMediaLibraryFile

## SYNOPSIS
Creates a media library file.

## SYNTAX

### Object
```
New-CMSMediaLibraryFile [-Library] <MediaLibraryInfo> -LocalFile <FileInfo> -FileName <String>
 -FileTitle <String> -FilePath <String> [-FileDescription <String>] [-PassThru] [<CommonParameters>]
```

### Property
```
New-CMSMediaLibraryFile [-SiteID] <Int32> [-LibraryName] <String> -LocalFile <FileInfo> -FileName <String>
 -FileTitle <String> -FilePath <String> [-FileDescription <String>] [-PassThru] [<CommonParameters>]
```

## DESCRIPTION
Creates a media library file based off of the provided input.

## EXAMPLES

### EXAMPLE 1
```
New-CMSMediaLibraryFile -SiteID 1 -LibraryName "Name" -LocalFile $file -FileName "Image" -FileTitle "File title" -FilePath "NewFolder/Image/" -FileDescription "Description"
```

### EXAMPLE 2
```
$file | New-CMSMediaLibraryFile -Library $library -FileName "Image" -FileTitle "File title" -FilePath "NewFolder/Image/" -FileDescription "Description"
```

## PARAMETERS

### -Library
The associalted library for the new media file.

```yaml
Type: MediaLibraryInfo
Parameter Sets: Object
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SiteID
The associalted library site id for the new media file.

```yaml
Type: Int32
Parameter Sets: Property
Aliases:

Required: True
Position: 0
Default value: 0
Accept pipeline input: False
Accept wildcard characters: False
```

### -LibraryName
The library name for the new media file.

```yaml
Type: String
Parameter Sets: Property
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -LocalFile
The file name for the new media file.

```yaml
Type: FileInfo
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -FileName
The file name for the new media file.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FileTitle
The file title for the new media file.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FilePath
The file path for the new media file.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FileDescription
The file description for the new media file.

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

### -PassThru
Tell the cmdlet to return the newly created library.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.IO.FileInfo
The file name for the new media file.

## OUTPUTS

### CMS.MediaLibrary.MediaFileInfo
## NOTES

## RELATED LINKS
