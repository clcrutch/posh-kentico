---
external help file: PoshKentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Remove-CMSSite

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

### Object
```
Remove-CMSSite [-SiteToRemove] <SiteInfo> [-WhatIf] [-Confirm] [<CommonParameters>]
```

### Dislpay Name
```
Remove-CMSSite [-DisplayName] <String> [-RegularExpression] [-WhatIf] [-Confirm] [<CommonParameters>]
```

### ID
```
Remove-CMSSite [-ID] <Int32[]> [-WhatIf] [-Confirm] [<CommonParameters>]
```

### User
```
Remove-CMSSite [[-User] <UserInfo>] [-WhatIf] [-Confirm] [<CommonParameters>]
```

### Task
```
Remove-CMSSite -ScheduledTask <TaskInfo> [-WhatIf] [-Confirm] [<CommonParameters>]
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
Parameter Sets: Dislpay Name
Aliases: SiteName, DomainName

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -ID
{{Fill ID Description}}

```yaml
Type: Int32[]
Parameter Sets: ID
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RegularExpression
{{Fill RegularExpression Description}}

```yaml
Type: SwitchParameter
Parameter Sets: Dislpay Name
Aliases: Regex

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ScheduledTask
{{Fill ScheduledTask Description}}

```yaml
Type: TaskInfo
Parameter Sets: Task
Aliases: Task, TaskInfo

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -SiteToRemove
{{Fill SiteToRemove Description}}

```yaml
Type: SiteInfo
Parameter Sets: Object
Aliases: Site

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -User
{{Fill User Description}}

```yaml
Type: UserInfo
Parameter Sets: User
Aliases:

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
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

### System.String

### CMS.Membership.UserInfo

### CMS.Scheduler.TaskInfo

## OUTPUTS

### System.Object
## NOTES

## RELATED LINKS
