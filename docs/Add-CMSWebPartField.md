---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Add-CMSWebPartField

## SYNOPSIS
Adds a field to a web part.

## SYNTAX

```
Add-CMSWebPartField [[-Caption] <String>] [-DataType] <FieldDataType> [[-DefaultValue] <Object>]
 [-Name] <String> [-PassThru] [-Required] [-Size <Int32>] -WebPart <WebPartInfo> [<CommonParameters>]
```

## DESCRIPTION
Adds a field to the web part and then immediately saves the additional field in Kentico.

This cmdlet returns the newly created web part field when the -PassThru switch is used.

This command automatically initializes the connection to Kentico if not already initialized.

## EXAMPLES

### EXAMPLE 1
```
$webPart | Add-CMSWebPartField -DataType Text -Name TestProp -required -size 150 -defaultvalue TestValue
```

## PARAMETERS

### -Caption
The caption for the new field.

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

### -DataType
The data type for the new field.

Possible values: Text

```yaml
Type: FieldDataType
Parameter Sets: (All)
Aliases: Type
Accepted values: Text

Required: True
Position: 0
Default value: Text
Accept pipeline input: False
Accept wildcard characters: False
```

### -DefaultValue
The default value for the new field.

```yaml
Type: Object
Parameter Sets: (All)
Aliases:

Required: False
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
The default value for the new field.

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

### -PassThru
Tell the cmdlet to return the newly created web part field.

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

### -Required
Indicates if a value is required for the field.

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

### -Size
The size to make the new field.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: 0
Accept pipeline input: False
Accept wildcard characters: False
```

### -WebPart
The web part to add the field to.

```yaml
Type: WebPartInfo
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

### CMS.PortalEngine.WebPartInfo
The web part to add the field to.

## OUTPUTS

### CMS.FormEngine.FormInfo
## NOTES

## RELATED LINKS
