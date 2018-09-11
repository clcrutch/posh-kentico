---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Get-CMSWebConfigValue

## SYNOPSIS
Gets the web.config setting value by the provided setting key.

## SYNTAX

```
Get-CMSWebConfigValue [-Key] <String> [[-Default] <String>] [<CommonParameters>]
```

## DESCRIPTION
Gets the web.config setting values by the provided setting key or default value if not.

## EXAMPLES

### EXAMPLE 1
```
Get-CMSSettingValue -Key "my key"
```

### EXAMPLE 2
```
Get-CMSSettingValue -Key "my key" -Default "the default value"
```

## PARAMETERS

### -Key
The key of the web.config setting to retrive value from.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Default
The default value for the setting.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String
The key of the web.config setting to retrive value from.

## OUTPUTS

## NOTES

## RELATED LINKS
