---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Set-CMSWebPart

## SYNOPSIS
Sets a web part.

## SYNTAX

```
Set-CMSWebPart [-PassThru] -WebPart <WebPartInfo> [<CommonParameters>]
```

## DESCRIPTION
Sets a web part.

This command automatically initializes the connection to Kentico if not already initialized.

## EXAMPLES

### EXAMPLE 1
```
$webPart | Set-CMSWebPart
```

### EXAMPLE 2
```
$webPart | Set-CMSWebPart -PassThru
```

## PARAMETERS

### -PassThru
Tell the cmdlet to return the web part.

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

### -WebPart
The web part to set.

```yaml
Type: WebPartInfo
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

### CMS.PortalEngine.WebPartInfo[]
## NOTES

## RELATED LINKS
