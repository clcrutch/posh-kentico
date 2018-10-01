---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Set-CMSScheduledTask

## SYNOPSIS
Sets a scheduled task in Kentico.

## SYNTAX

```
Set-CMSScheduledTask [[-Interval] <TaskInterval>] [-PassThru] [-ScheduledTask] <TaskInfo> [<CommonParameters>]
```

## DESCRIPTION
Sets a scheduled task in Kentico.
This command automatically initializes the connection to Kentico if not already initialized.

## EXAMPLES

### EXAMPLE 1
```
$scheduledTask | Set-CMSScheduledTask
```

### EXAMPLE 2
```
$scheduledTask | Set-CMSScheduledTask -Interval $interval
```

## PARAMETERS

### -Interval
The interval for the scheduled task.

```yaml
Type: TaskInterval
Parameter Sets: (All)
Aliases:

Required: False
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PassThru
Tell the cmdlet to return the scheduled task.

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

### -ScheduledTask
The scheduled task to set.

```yaml
Type: TaskInfo
Parameter Sets: (All)
Aliases: Task, TaskInfo

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### CMS.Scheduler.TaskInfo
The scheduled task to set.

## OUTPUTS

### CMS.Scheduler.TaskInfo[]

## NOTES

## RELATED LINKS
