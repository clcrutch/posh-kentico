---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Set-CMSMediaLibraryFile

## SYNOPSIS
Sets a media file.

## SYNTAX

```
Set-CMSMediaLibraryFile [-PassThru] -MediaFile <MediaFileInfo> [<CommonParameters>]
```

## DESCRIPTION
Sets a media file based off of the provided input.

This cmdlet returns the updated library file when the -PassThru switch is used.

## EXAMPLES

### EXAMPLE 1
```
Set-CMSMediaLibraryFile -MediaFile $libraryFile
```

### EXAMPLE 2
```
$libraryFile | Set-CMSMediaLibraryFile
```

## PARAMETERS

### -PassThru
Tell the cmdlet to return the media library file.

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

### -MediaFile
The media library file to set.

```yaml
Type: MediaFileInfo
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### CMS.MediaLibrary.MediaFileInfo
The media library file to set.

## OUTPUTS

### CMS.MediaLibrary.MediaFileInfo
## NOTES

## RELATED LINKS
