---
external help file: PoshKentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Connect-CMSApplication

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

### None (Default)
```
Connect-CMSApplication [-Cached] [<CommonParameters>]
```

### ConnectionString
```
Connect-CMSApplication [-ConnectionString] <String> [-WebRoot] <String> [<CommonParameters>]
```

### ServerAndDatabase
```
Connect-CMSApplication [-DatabaseServer] <String> [-Database] <String> [-Timeout <Int32>] [-WebRoot] <String>
 [<CommonParameters>]
```

## DESCRIPTION
{{Fill in the Description}}

## EXAMPLES

### Example 1
```powershell
PS C:\> {{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

### -Cached
{{Fill Cached Description}}

```yaml
Type: SwitchParameter
Parameter Sets: None
Aliases: UseCached

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ConnectionString
{{Fill ConnectionString Description}}

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

### -Database
{{Fill Database Description}}

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

### -DatabaseServer
{{Fill DatabaseServer Description}}

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

### -Timeout
{{Fill Timeout Description}}

```yaml
Type: Int32
Parameter Sets: ServerAndDatabase
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WebRoot
{{Fill WebRoot Description}}

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

### None

## OUTPUTS

### System.Object
## NOTES

## RELATED LINKS
