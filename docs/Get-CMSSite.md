---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Get-CMSSite

## SYNOPSIS
Gets the sites selected by the provided input.

## SYNTAX

### None (Default)
```
Get-CMSSite [<CommonParameters>]
```

### Dislpay Name
```
Get-CMSSite [[-DisplayName] <String>] [-Exact] [<CommonParameters>]
```

### ID
```
Get-CMSSite [[-ID] <Int32[]>] [<CommonParameters>]
```

## DESCRIPTION
Gets the sites selected by the provided input.
This command automatically initializes the connection to Kentico if not already initialized.

Without parameters, this command returns all sites.

With parameters, this command returns the sites that match the criteria.

## EXAMPLES

### EXAMPLE 1
```
Get-CMSSite
```

### EXAMPLE 2
```
Get-CMSSite bas
```

### EXAMPLE 3
```
Get-CMSSite basic -Exact
```

### EXAMPLE 4
```
Get-CMSSite -ID 5,304,5
```

## PARAMETERS

### -DisplayName
The display name of the site to retrive.

```yaml
Type: String
Parameter Sets: Dislpay Name
Aliases: SiteName, DomainName

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Exact
If set, the match is exact, else the match performs a contains for display name and category name and starts with for path.

```yaml
Type: SwitchParameter
Parameter Sets: Dislpay Name
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -ID
The IDs of the site to retrieve.

```yaml
Type: Int32[]
Parameter Sets: ID
Aliases:

Required: False
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String
The display name of the site to retrive.

## OUTPUTS

### CMS.SiteProvider.SiteInfo[]

## NOTES

## RELATED LINKS
