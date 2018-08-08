---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Initialize-CMSApplication

## SYNOPSIS
Initializes a connection to the Kentico CMS Server.

## SYNTAX

### None (Default)
```
Initialize-CMSApplication [-Cached] [<CommonParameters>]
```

### ConnectionString
```
Initialize-CMSApplication [-ConnectionString] <String> [-WebRoot] <String> [<CommonParameters>]
```

### ServerAndDatabase
```
Initialize-CMSApplication [-DatabaseServer] <String> [-Database] <String> [-Timeout <Int32>]
 [-WebRoot] <String> [<CommonParameters>]
```

## DESCRIPTION
The Initialize-CMSApplication cmdlet initializes a connection to the Kentico CMS server.

If this cmdlet is run without parameters, then it requires administrator permissions to find the Kentico site.

It does so by performing the following steps:

1.
Get a list of all the sites from IIS

2.
Get a list of all applications from the sites

3.
Get a list of all the virtual directories from the applications

4.
Continue processing virtual directory if a web.config file exits

5.
Parse the document and find an "add" node with name="CMSConnectionString"

6.
If the connection string is valid, then stop processing

## EXAMPLES

### EXAMPLE 1
```
Initialize-CMSApplication
```

### EXAMPLE 2
```
Initialize-CMSApplication -DatabaseServer KenticoServer -Database Kentico -WebRoot C:\kentico
```

## PARAMETERS

### -Cached
Use the previous successful site and connection string information found.

If none, then this is the same as supplying no parameters.

```yaml
Type: SwitchParameter
Parameter Sets: None
Aliases: UseCached

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -ConnectionString
The connection string for the database connection.

```yaml
Type: String
Parameter Sets: ConnectionString
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DatabaseServer
The database server to use for generating the connection string.

```yaml
Type: String
Parameter Sets: ServerAndDatabase
Aliases: SQLServer

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Database
The database to use for generating the connection string.

```yaml
Type: String
Parameter Sets: ServerAndDatabase
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Timeout
The timeout to use for generating the connection string.

```yaml
Type: Int32
Parameter Sets: ServerAndDatabase
Aliases:

Required: False
Position: Named
Default value: 60
Accept pipeline input: False
Accept wildcard characters: False
```

### -WebRoot
The root directory for the Kentico site.

```yaml
Type: String
Parameter Sets: ConnectionString, ServerAndDatabase
Aliases: KenticoRoot

Required: True
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS
