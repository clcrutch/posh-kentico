---
external help file: PoshKentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Remove-CMSServer

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

### Object
```
Remove-CMSServer [-ServerToRemove] <ServerInfo> [[-DisplayName] <String>] [-RegularExpression]
 [[-Site] <SiteInfo>] [-WhatIf] [-Confirm] [<CommonParameters>]
```

### Dislpay Name
```
Remove-CMSServer [[-SiteID] <Int32>] [[-DisplayName] <String>] [-RegularExpression] [-WhatIf] [-Confirm]
 [<CommonParameters>]
```

### ID
```
Remove-CMSServer [[-ID] <Int32[]>] [-WhatIf] [-Confirm] [<CommonParameters>]
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

### -DisplayName
{{Fill DisplayName Description}}

```yaml
Type: String
Parameter Sets: Object, Dislpay Name
Aliases: ServerName

Required: False
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ID
{{Fill ID Description}}

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

### -RegularExpression
{{Fill RegularExpression Description}}

```yaml
Type: SwitchParameter
Parameter Sets: Object, Dislpay Name
Aliases: Regex

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ServerToRemove
{{Fill ServerToRemove Description}}

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

### -Site
{{Fill Site Description}}

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

### -SiteID
{{Fill SiteID Description}}

```yaml
Type: Int32
Parameter Sets: Dislpay Name
Aliases:

Required: False
Position: 0
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

### CMS.Synchronization.ServerInfo

### CMS.SiteProvider.SiteInfo

## OUTPUTS

### System.Object
## NOTES

## RELATED LINKS
