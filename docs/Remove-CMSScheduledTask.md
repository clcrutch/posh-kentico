---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Remove-CMSScheduledTask

## SYNOPSIS
Removes the scheduled task for the provided input.

## SYNTAX

### None (Default)
```
Remove-CMSScheduledTask [-WhatIf] [-Confirm] [<CommonParameters>]
```

### Scheduled Task
```
Remove-CMSScheduledTask [-ScheduledTask] <TaskInfo> [-WhatIf] [-Confirm] [<CommonParameters>]
```

### Assembly Name
```
Remove-CMSScheduledTask -AssemblyName <String> [-RegularExpression] [-WhatIf] [-Confirm] [<CommonParameters>]
```

### Site and Assembly Name
```
Remove-CMSScheduledTask -AssemblyName <String> [-RegularExpression] -Site <SiteInfo> [-WhatIf] [-Confirm]
 [<CommonParameters>]
```

### Name
```
Remove-CMSScheduledTask [-Name] <String> [-RegularExpression] [-WhatIf] [-Confirm] [<CommonParameters>]
```

### Site and Name
```
Remove-CMSScheduledTask [-Name] <String> [-RegularExpression] -Site <SiteInfo> [-WhatIf] [-Confirm]
 [<CommonParameters>]
```

### Site
```
Remove-CMSScheduledTask -Site <SiteInfo> [-WhatIf] [-Confirm] [<CommonParameters>]
```

## DESCRIPTION
Removes the scheduled tasks selected by the provided input.
This command automatically initializes the connection to Kentico if not already initialized.

Without parameters, this command removes all scheduled tasks.

With parameters, this command removes the scheduled tasks that match the criteria.

## EXAMPLES

### EXAMPLE 1
```
Remove-CMSScheduledTask
```

### EXAMPLE 2
```
Remove-CMSScheduledTask -AssemblyName *test*
```

### EXAMPLE 3
```
Remove-CMSScheduledTask -Name *test*
```

### EXAMPLE 4
```
$site | Remove-CMSScheduledTask
```

### EXAMPLE 5
```
$scheduledTask | Remove-CMSScheduleTask
```

## PARAMETERS

### -ScheduledTask
The scheduled task to remove.

```yaml
Type: TaskInfo
Parameter Sets: Scheduled Task
Aliases: Task, TaskInfo

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -AssemblyName
The assembly name for the scheduled task.

```yaml
Type: String
Parameter Sets: Assembly Name, Site and Assembly Name
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
The name or display name for scheduled task.

```yaml
Type: String
Parameter Sets: Name, Site and Name
Aliases: DisplayName

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RegularExpression
Indicates if the CategoryName supplied is a regular expression.

```yaml
Type: SwitchParameter
Parameter Sets: Assembly Name, Site and Assembly Name, Name, Site and Name
Aliases: Regex

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Site
The site to get the scheduled tasks for.

```yaml
Type: SiteInfo
Parameter Sets: Site and Assembly Name, Site and Name, Site
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
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

### CMS.Scheduler.TaskInfo
The scheduled task to remove.

### CMS.SiteProvider.SiteInfo
The site to get the scheduled tasks for.

## OUTPUTS

### CMS.Scheduler.TaskInfo[]

## NOTES

## RELATED LINKS
