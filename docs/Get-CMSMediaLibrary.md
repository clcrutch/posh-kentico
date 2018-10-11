---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Get-CMSMediaLibrary

## SYNOPSIS
Gets the media libraries by the provided input.

## SYNTAX

### None (Default)
```
Get-CMSMediaLibrary [<CommonParameters>]
```

### Object
```
Get-CMSMediaLibrary [[-Site] <SiteInfo>] [<CommonParameters>]
```

### Library Name
```
Get-CMSMediaLibrary [-SiteID] <Int32> [-LibraryName] <String> [-Exact] [<CommonParameters>]
```

### ID
```
Get-CMSMediaLibrary [-ID] <Int32[]> [<CommonParameters>]
```

## DESCRIPTION
Gets the media libraries by the provided input.

Without parameters, this command returns all libraries.

With parameters, this command returns the libraries that match the criteria.

## EXAMPLES

### EXAMPLE 1
```
Get-CMSMediaLibrary
```

### EXAMPLE 2
```
Get-CMSMediaLibrary -SiteID 1 -LibraryName bas
```

### EXAMPLE 3
```
Get-CMSMediaLibrary -SiteID 1 -LibraryName "test" -Exact
```

### EXAMPLE 4
```
$site | Get-CMSMediaLibrary -LibraryName bas
```

### EXAMPLE 5
```
$site | Get-CMSMediaLibrary -LibraryName "test" -Exact
```

### EXAMPLE 6
```
Get-CMSMediaLibrary -ID 1,3
```

## PARAMETERS

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

### CMS.SiteProvider.SiteInfo
The associalted site for the server to retrieve.

### System.Int32
The site name of the library to retrive.

## OUTPUTS

### CMS.MediaLibrary.MediaLibraryInfo[]
## NOTES

## RELATED LINKS
