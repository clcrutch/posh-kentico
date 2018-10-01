---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# New-CMSScheduledTask

## SYNOPSIS
Creates a new scheduled task with the provided input.

## SYNTAX

```
New-CMSScheduledTask [-AssemblyName] <String> [-Class] <String> [-Data] <String> [-DisplayName] <String>
 [-Interval] <TaskInterval> [-Name] <String> [-Site <SiteInfo>] [<CommonParameters>]
```

## DESCRIPTION
Creates a new scheduled task with the provided input.
This command automatically initializes the connection to Kentico if not already initialized.

## EXAMPLES

### EXAMPLE 1
```
# Creates the scheduling interval for the task
$interval = New-Object -TypeName CMS.Scheduler.TaskInterval

# Sets the interval properties
$interval.Period = [CMS.Scheduler.SchedulingHelper]::PERIOD_DAY
$interval.StartTime = Get-Date
$interval.Every = 2
$interval.Days = @([DayOfWeek]::Monday)

New-CMSScheduledTask -AssemblyName Assembly.Name -Class Class.Name -Data TaskData -DisplayName "Display Name" -Interval $interval -Name Task.Name -Site $site
```

## PARAMETERS

### -AssemblyName
The assembly name for the scheduled task.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Class
The class name for the scheduled task.

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

### -Data
The data for the scheduled task.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 4
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisplayName
The display name for the scheduled task.

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

### -Interval
The interval for the scheduled task.

```yaml
Type: TaskInterval
Parameter Sets: (All)
Aliases:

Required: True
Position: 5
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
The name for the scheduled task.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Site
The site for the scheduled task.

```yaml
Type: SiteInfo
Parameter Sets: (All)
Aliases:

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

## OUTPUTS

### CMS.Scheduler.TaskInfo[]

## NOTES

## RELATED LINKS
