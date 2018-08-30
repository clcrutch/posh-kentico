---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Get-CMSServer

## SYNOPSIS
Gets the servers selected by the provided input.

## SYNTAX

### None (Default)
```
Get-CMSServer [<CommonParameters>]
```

### Dislpay Name
```
Get-CMSServer [[-SiteID] <Int32>] [[-DisplayName] <String>] [-Exact] [<CommonParameters>]
```

### Object
```
Get-CMSServer [[-DisplayName] <String>] [-Exact] [[-Site] <SiteInfo>] [<CommonParameters>]
```

### ID
```
Get-CMSServer [[-ID] <Int32[]>] [<CommonParameters>]
```

## DESCRIPTION
Gets the servers selected by the provided input.
This command automatically initializes the connection to Kentico if not already initialized.

Without parameters, this command returns all servers.

With parameters, this command returns the servers that match the criteria.

## EXAMPLES

### EXAMPLE 1
```
Get-CMSServer
```

### EXAMPLE 2
```
Get-CMSServer bas
```

### EXAMPLE 3
```
Get-CMSServer -SiteID 5 -ServerName "bas"
```

### EXAMPLE 4
```
$site | Get-CMSServer bas
```

### EXAMPLE 5
```
Get-CMSServer basic -Exact
```

### EXAMPLE 6
```
Get-CMSServer 5 basic -Exact
```

### EXAMPLE 7
```
$site | Get-CMSServer basic -Exact
```

### EXAMPLE 8
```
Get-CMSServer -ID 5,304,5
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
If set, the match is exact,

else the match performs a contains for display name and category name and starts with for path.

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### CMS.SiteProvider.SiteInfo
The associalted site for the server to retrieve.

## OUTPUTS

### CMS.Synchronization.ServerInfo[]

## NOTES

## RELATED LINKS
