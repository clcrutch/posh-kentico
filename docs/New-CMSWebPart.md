---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# New-CMSWebPart

## SYNOPSIS
Creates a new web part.

## SYNTAX

### Path (Default)
```
New-CMSWebPart [[-DisplayName] <String>] [-FileName] <String> [-Path] <String> [-PassThru] [<CommonParameters>]
```

### Category
```
New-CMSWebPart [[-DisplayName] <String>] [-FileName] <String> [-Name] <String> [-PassThru]
 -WebPartCategory <WebPartCategoryInfo> [<CommonParameters>]
```

## DESCRIPTION
Creates a new web part category based off of the provided input.

This cmdlet returns the newly created web part when the -PassThru switch is used.

This command automatically initializes the connection to Kentico if not already initialized.

## EXAMPLES

### EXAMPLE 1
```
New-CMSWebPart -Path /TestCategory/TestWebPart -FileName Test.ascx
```

### EXAMPLE 2
```
New-CMSWebPart -Path /TestCategory/TestWebPart -FileName Test.ascx -DisplayName TestDisplayName
```

### EXAMPLE 3
```
$category | New-CMSWebPart -Name TestWebPart -FileName Test.ascx
```

### EXAMPLE 4
```
$category | New-CMSWebPart -Name TestWebPart -FileName Test.ascx -DisplayName TestDisplayName
```

## PARAMETERS

### -DisplayName
The display name for the newly created webpart.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FileName
The file name for the webpart code behind.

```yaml
Type: String
Parameter Sets: (All)
Aliases: File

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
The Code Name for the webpart.

```yaml
Type: String
Parameter Sets: Category
Aliases: CodeName

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
The path to the webpart.

```yaml
Type: String
Parameter Sets: Path
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PassThru
Tell the cmdlet to return the newly created web part.

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

### -WebPartCategory
The webpart category to add the webpart under.

```yaml
Type: WebPartCategoryInfo
Parameter Sets: Category
Aliases: Category, Parent

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

### CMS.PortalEngine.WebPartCategoryInfo
The webpart category to add the webpart under.

## OUTPUTS

### CMS.PortalEngine.WebPartInfo[]

## NOTES

## RELATED LINKS
