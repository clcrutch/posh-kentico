---
external help file: posh-kentico.dll-help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Enable-CMSScheduledTask

## SYNOPSIS
Enables the scheduled tasks for the provided input.

## SYNTAX

### NONE (Default)
```
Enable-CMSScheduledTask [<CommonParameters>]
```

### Scheduled Task
```
Enable-CMSScheduledTask -ScheduledTask <TaskInfo> [-PassThru] [<CommonParameters>]
```

## DESCRIPTION
Enables the scheduled tasks for the provided input. 
This command automatically initializes the connection to Kentico if not already initialized.

This command with parameters disables only the specified scheduled task.
Without parameters, this command enables all of the scheduled tasks in Kentico.

## EXAMPLES

### EXAMPLE 1
```
Enables all scheduled tasks.
```

Enable-CMSScheduledTask

### EXAMPLE 2
```
Enable a specified scheduled task.
```

$scheduledTask | Enable-CMSScheduledTask

## PARAMETERS

### -ScheduledTask
The scheduled task to enable in Kentico.

```yaml
Type: TaskInfo
Parameter Sets: Scheduled Task
Aliases: Task, TaskInfo

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -PassThru
Tell the cmdlet to return the scheduled task.

```yaml
Type: SwitchParameter
Parameter Sets: Scheduled Task
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

## OUTPUTS

### CMS.Scheduler.TaskInfo

## NOTES

## RELATED LINKS
