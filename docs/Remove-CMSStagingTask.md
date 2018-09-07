---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Remove-CMSStagingTask

## SYNOPSIS
Removehronize the staging tasks that target the given server.

## SYNTAX

### Object
```
Remove-CMSStagingTask [-ServerToRemove] <ServerInfo> [<CommonParameters>]
```

### Property
```
Remove-CMSStagingTask [-ServerName] <String> [-SiteID] <Int32> [<CommonParameters>]
```

## DESCRIPTION
{{Fill in the Description}}

## EXAMPLES

### EXAMPLE 1
```
Remove-CMSStagingTask -Server $server
```

### EXAMPLE 2
```
$server | Remove-CMSStagingTask
```

### EXAMPLE 3
```
Remove-CMSStagingTask -ServerName "Server Name to Find" -SiteID "Site Id to Find"
```

## PARAMETERS

### -ServerToRemove
A reference to the server to remove all related staging tasks.

```yaml
Type: ServerInfo
Parameter Sets: Object
Aliases: Server

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -ServerName
The server name for the server to remove all related staging tasks.

```yaml
Type: String
Parameter Sets: Property
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SiteID
The server site id for the server to remove all related staging tasks.

```yaml
Type: Int32
Parameter Sets: Property
Aliases:

Required: True
Position: 1
Default value: 0
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### CMS.Synchronization.ServerInfo
A reference to the server to remove all related staging tasks.

## OUTPUTS

## NOTES

## RELATED LINKS
