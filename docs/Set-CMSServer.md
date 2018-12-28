---
external help file: PoshKentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Set-CMSServer

## SYNOPSIS
{{Fill in the Synopsis}}

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
{{Fill in the Description}}

## EXAMPLES

### Example 1
```powershell
PS C:\> {{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

### -Authentication
{{Fill Authentication Description}}

```yaml
Type: ServerAuthenticationEnum
Parameter Sets: Property
Aliases:
Accepted values: UserName, X509

Required: False
Position: 4
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisplayName
{{Fill DisplayName Description}}

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

### -Enabled
{{Fill Enabled Description}}

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

### -PassThru
{{Fill PassThru Description}}

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Password
{{Fill Password Description}}

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

### -ServerName
{{Fill ServerName Description}}

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

### -ServerToSet
{{Fill ServerToSet Description}}

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

### -SiteID
{{Fill SiteID Description}}

```yaml
Type: Int32
Parameter Sets: Property
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -URL
{{Fill URL Description}}

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

### -UserName
{{Fill UserName Description}}

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### CMS.Synchronization.ServerInfo

## OUTPUTS

### CMS.Synchronization.ServerInfo

## NOTES

## RELATED LINKS
