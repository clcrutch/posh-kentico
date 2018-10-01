---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Get-CMSScheduledTaskInterval

## SYNOPSIS
Gets the interval for the supplied scheduled task.

## SYNTAX

```
Get-CMSScheduledTaskInterval -ScheduledTask <TaskInfo> [<CommonParameters>]
```

## DESCRIPTION
Gets the interval for the supplied scheduled task.
This command automatically initializes the connection to Kentico if not already initialized.

## EXAMPLES

### EXAMPLE 1
```
$scheduledTask | Get-CMSScheduledTaskInterval
```

## PARAMETERS

### -ScheduledTask
The scheduled task to get the interval for.

```yaml
Type: TaskInfo
Parameter Sets: (All)
Aliases: Task, TaskInfo

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### CMS.Scheduler.TaskInfo
The scheduled task to get the interval for.

## OUTPUTS

### CMS.Scheduler.TaskInterval[]

## NOTES

## RELATED LINKS
