---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# New-CMSMediaLibraryFolder

## SYNOPSIS
Creates a media library folder.

## SYNTAX

### Object
```
New-CMSMediaLibraryFolder [-Library] <MediaLibraryInfo> [-Folder] <String> [<CommonParameters>]
```

### Property
```
New-CMSMediaLibraryFolder [-SiteID] <Int32> [-LibraryName] <String> [-Folder] <String> [<CommonParameters>]
```

## DESCRIPTION
Creates a media library folder based off of the provided input.

## EXAMPLES

### EXAMPLE 1
```
New-CMSMediaLibraryFolder -SiteID 1 -LibraryName "Name" -Folder "Images"
```

### EXAMPLE 2
```
$library | New-CMSMediaLibraryFolder -Folder "Images"
```

## PARAMETERS

### -Library
The associalted library for the new folder.

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

### -SiteID
The associalted library site id for the new folder.

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
The library name for the new folder.

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

### -Folder
The folder name for the new folder.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### CMS.MediaLibrary.MediaLibraryInfo
The associalted library for the new folder.

## OUTPUTS

## NOTES

## RELATED LINKS
