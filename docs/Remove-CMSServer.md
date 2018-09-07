---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Remove-CMSServer

## SYNOPSIS
Removes the servers selected by the provided input.

## SYNTAX

### Dislpay Name
```
Remove-CMSServer [[-SiteID] <Int32>] [[-DisplayName] <String>] [-Exact] [-WhatIf] [-Confirm]
 [<CommonParameters>]
```

### Object
```
Remove-CMSServer [[-Site] <SiteInfo>] [[-DisplayName] <String>] [-Exact] [-WhatIf] [-Confirm]
 [<CommonParameters>]
```

### ID
```
Remove-CMSServer [[-ID] <Int32[]>] [-WhatIf] [-Confirm] [<CommonParameters>]
```

## DESCRIPTION
Removes the servers selected by the provided input.
This command automatically initializes the connection to Kentico if not already initialized.

## EXAMPLES

### EXAMPLE 1
```
Remove-CMSServer bas
```

### EXAMPLE 2
```
Remove-CMSServer -SiteID 5 -ServerName "bas"
```

### EXAMPLE 3
```
$site | Remove-CMSServer bas
```

### EXAMPLE 4
```
Remove-CMSServer basic -Exact
```

### EXAMPLE 5
```
Remove-CMSServer -SiteID 5 -ServerName "basic" -Exact
```

### EXAMPLE 6
```
$site | Remove-CMSServer basic -Exact
```

### EXAMPLE 7
```
Remove-CMSServer -ID 5,304,5
```

## PARAMETERS

### -SiteID
The server site id for the server to update.

```yaml
Type: Int32
Parameter Sets: Dislpay Name
Aliases:

Required: False
Position: 0
Default value: 0
Accept pipeline input: False
Accept wildcard characters: False
```

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

### -DisplayName
The display name of the server to retrive.

```yaml
Type: String
Parameter Sets: Dislpay Name, Object
Aliases: ServerName

Required: False
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Exact
If set, the match is exact, else the match performs a contains for display name and category name and starts with for path.

```yaml
Type: SwitchParameter
Parameter Sets: Dislpay Name, Object
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -ID
The IDs of the server to retrieve.

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

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs.
The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
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

## OUTPUTS

## NOTES

## RELATED LINKS
