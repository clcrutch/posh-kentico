---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# New-CMSMediaLibrary

## SYNOPSIS
Creates a new library.

## SYNTAX

### Object
```
New-CMSMediaLibrary [-Site] <SiteInfo> [-DisplayName] <String> [-LibraryName] <String>
 [[-Description] <String>] [-Folder] <String> [-PassThru] [<CommonParameters>]
```

### Property
```
New-CMSMediaLibrary [-SiteID] <Int32> [-DisplayName] <String> [-LibraryName] <String> [[-Description] <String>]
 [-Folder] <String> [-PassThru] [<CommonParameters>]
```

## DESCRIPTION
Creates a new library based off of the provided input.

This cmdlet returns the newly created library when the -PassThru switch is used.

## EXAMPLES

### EXAMPLE 1
```
New-CMSMediaLibrary -SiteID 1 -DisplayName "My Test Name" -LibraryName "Name" -Description "Library description" -Folder "Images"
```

### EXAMPLE 2
```
$site | New-CMSMediaLibrary -DisplayName "My Test Name" -LibraryName "Name" -Description "Library description" -Folder "Images"
```

## PARAMETERS

### -Site
The associalted site for the newly created library.

```yaml
Type: SiteInfo
Parameter Sets: Object
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -SiteID
The library site id for the library to update.

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

### -DisplayName
The display name for the newly created library.

The Media Library display name cannot be blank.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -LibraryName
The library name for the newly created library.

The library name cannot be blank.

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

### -Description
The library description for the newly created library.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 3
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Folder
The library folder for the newly created library.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 4
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

### CMS.SiteProvider.SiteInfo
The associalted site for the newly created library.

## OUTPUTS

### CMS.MediaLibrary.MediaLibraryInfo
## NOTES

## RELATED LINKS
