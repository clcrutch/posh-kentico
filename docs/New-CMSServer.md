---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# New-CMSServer

## SYNOPSIS
Creates a new server.

## SYNTAX

### Object
```
New-CMSServer [-Site] <SiteInfo> [-DisplayName] <String> [[-ServerName] <String>] [-URL] <String>
 [[-Authentication] <ServerAuthenticationEnum>] [[-Enabled] <Boolean>] [[-UserName] <String>]
 [[-Password] <String>] [-PassThru] [<CommonParameters>]
```

### Property
```
New-CMSServer [-SiteID] <Int32> [-DisplayName] <String> [[-ServerName] <String>] [-URL] <String>
 [[-Authentication] <ServerAuthenticationEnum>] [[-Enabled] <Boolean>] [[-UserName] <String>]
 [[-Password] <String>] [-PassThru] [<CommonParameters>]
```

## DESCRIPTION
Creates a new server based off of the provided input.

This cmdlet returns the newly created server when the -PassThru switch is used.

## EXAMPLES

### EXAMPLE 1
```
New-CMSServer -DisplayName "My Test Name" -URL "My Server Url" -SiteID 5
```

### EXAMPLE 2
```
$site | New-CMSServer -DisplayName "My Test Name" -URL "My Server Url"
```

### EXAMPLE 3
```
New-CMSServer -DisplayName "My Test Name" -ServerName "My Server Name" -URL "My Server Url"
                        -Authentication UserName -Enabled True -UserName "My User Name" -Password "My Password" -SiteID 5
```

### EXAMPLE 4
```
$site | New-CMSServer -DisplayName "My Test Name" -ServerName "My Server Name" -URL "My Server Url"
                        -Authentication UserName -Enabled True -UserName "My User Name" -Password "My Password"
```

## PARAMETERS

### -Site
The associalted site for the newly created server.

```yaml
Type: SiteInfo
Parameter Sets: Object
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -SiteID
The server site id for the server to update.

```yaml
Type: Int32
Parameter Sets: Property
Aliases:

Required: True
Position: 0
Default value: 0
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisplayName
The display name for the newly created server.

Server display name cannot be blank.

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

### -ServerName
The server name for the newly created server.

If null, then the display name is used for the server name.

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

### -URL
The server url for the newly created server.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 3
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Authentication
The authentication for the newly created server.

Possible values: UserName, X509

```yaml
Type: ServerAuthenticationEnum
Parameter Sets: (All)
Aliases:
Accepted values: UserName, X509

Required: False
Position: 4
Default value: 0
Accept pipeline input: False
Accept wildcard characters: False
```

### -Enabled
The enabled status for the newly created server.

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases:

Required: False
Position: 5
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UserName
The user name for the newly created server.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 6
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Password
The password for the newly created server.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 7
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PassThru
Tell the cmdlet to return the newly created server.

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

### CMS.SiteProvider.SiteInfo
The associalted site for the newly created server.

## OUTPUTS

### CMS.Synchronization.ServerInfo
## NOTES

## RELATED LINKS
