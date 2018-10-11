---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Set-CMSMediaLibrary

## SYNOPSIS
Sets a library.

## SYNTAX

### Object
```
Set-CMSMediaLibrary [-LibraryToSet] <MediaLibraryInfo> [-PassThru] [<CommonParameters>]
```

### Property
```
Set-CMSMediaLibrary [-SiteID] <Int32> [-LibraryName] <String> [[-DisplayName] <String>]
 [[-Description] <String>] [[-Folder] <String>] [-PassThru] [<CommonParameters>]
```

## DESCRIPTION
Sets a library based off of the provided input.

This cmdlet returns the updated library when the -PassThru switch is used.

## EXAMPLES

### EXAMPLE 1
```
Set-CMSMediaLibrary -MediaLibrary $library
```

### EXAMPLE 2
```
$library | Set-CMSMediaLibrary
```

### EXAMPLE 3
```
Set-CMSMediaLibrary -SiteID 1 -LibraryName "Name" -DisplayName "My Test Name" -Description "Library description" -Folder "Images"
```

## PARAMETERS

### -LibraryToSet
A reference to the updated library.

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
The site id for the updated library.

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
The library name for the updated library.

The library name cannot be blank.

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

### -DisplayName
The display name for the updated library.

The Media Library display name cannot be blank.

```yaml
Type: String
Parameter Sets: Property
Aliases:

Required: False
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Description
The library description for the updated library.

```yaml
Type: String
Parameter Sets: Property
Aliases:

Required: False
Position: 3
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Folder
The library folder for the updated library.

```yaml
Type: String
Parameter Sets: Property
Aliases:

Required: False
Position: 4
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PassThru
Tell the cmdlet to return the updated library.

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

### CMS.MediaLibrary.MediaLibraryInfo
A reference to the updated library.

## OUTPUTS

### CMS.MediaLibrary.MediaLibraryInfo
## NOTES

## RELATED LINKS
