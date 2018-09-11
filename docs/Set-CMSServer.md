---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Set-CMSServer

## SYNOPSIS
Sets a server.

## SYNTAX

### Object
```
Set-CMSServer [-ServerToSet] <ServerInfo> [-PassThru] [<CommonParameters>]
```

### Property
```
Set-CMSServer [-ServerName] <String> [-SiteID] <Int32> [[-DisplayName] <String>] [[-URL] <String>]
 [[-Authentication] <ServerAuthenticationEnum>] [[-Enabled] <Boolean>] [[-UserName] <String>]
 [[-Password] <String>] [-PassThru] [<CommonParameters>]
```

## DESCRIPTION
Sets a new server based off of the provided input.

This cmdlet returns the server to update when the -PassThru switch is used.

## EXAMPLES

### EXAMPLE 1
```
Set-CMSServer -Server $server
```

### EXAMPLE 2
```
$server | Set-CMSServer
```

### EXAMPLE 3
```
Set-CMSServer -ServerName "Server Name to find" -SiteID "Site ID to find" -DisplayName "New Display Name" -URL "New URL"
                -Authentication "UserName or X509" -Enabled "True or False" -UserName "New User Name" -Password "New Password"
```

## PARAMETERS

### -ServerToSet
A reference to the server to update.

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
The server name for the server to update.

If null, then the display name is used for the server name.

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
The server site id for the server to update.

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

### -DisplayName
The display name for the server to update.

Server display name cannot be blank.

```yaml
Type: String
Parameter Sets: Property
Aliases:

Required: False
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -URL
The server url for the server to update.

```yaml
Type: String
Parameter Sets: Property
Aliases:

Required: False
Position: 3
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Authentication
The authentication for the server to update.

Possible values: UserName, X509

```yaml
Type: ServerAuthenticationEnum
Parameter Sets: Property
Aliases:
Accepted values: UserName, X509

Required: False
Position: 4
Default value: 0
Accept pipeline input: False
Accept wildcard characters: False
```

### -Enabled
The enabled status for the server to update.

```yaml
Type: Boolean
Parameter Sets: Property
Aliases:

Required: False
Position: 5
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UserName
The user name for the server to update.

```yaml
Type: String
Parameter Sets: Property
Aliases:

Required: False
Position: 6
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Password
The password for the server to update.

```yaml
Type: String
Parameter Sets: Property
Aliases:

Required: False
Position: 7
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PassThru
Tell the cmdlet to return the server to update.

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

### CMS.Synchronization.ServerInfo
A reference to the server to update.

## OUTPUTS

### CMS.Synchronization.ServerInfo

## NOTES

## RELATED LINKS
