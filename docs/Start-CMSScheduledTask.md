---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Start-CMSScheduledTask

## SYNOPSIS
Starts the scheduled task for the provided input.

## SYNTAX

### None (Default)
```
Start-CMSScheduledTask [-PassThru] [<CommonParameters>]
```

### Scheduled Task
```
Start-CMSScheduledTask [-PassThru] [-ScheduledTask] <TaskInfo> [<CommonParameters>]
```

### Assembly Name
```
Start-CMSScheduledTask [-PassThru] -AssemblyName <String> [-RegularExpression] [<CommonParameters>]
```

### Site and Assembly Name
```
Start-CMSScheduledTask [-PassThru] -AssemblyName <String> [-RegularExpression] -Site <SiteInfo>
 [<CommonParameters>]
```

### Name
```
Start-CMSScheduledTask [-PassThru] [-Name] <String> [-RegularExpression] [<CommonParameters>]
```

### Site and Name
```
Start-CMSScheduledTask [-PassThru] [-Name] <String> [-RegularExpression] -Site <SiteInfo> [<CommonParameters>]
```

### Site
```
Start-CMSScheduledTask [-PassThru] -Site <SiteInfo> [<CommonParameters>]
```

## DESCRIPTION
Starts the scheduled tasks selected by the provided input.
This command automatically initializes the connection to Kentico if not already initialized.

Without parameters, this command starts all scheduled tasks.

With parameters, this command starts the scheduled tasks that match the criteria.

## EXAMPLES

### EXAMPLE 1
```
Start-CMSScheduledTask
```

### EXAMPLE 2
```
Start-CMSScheduledTask -AssemblyName *test*
```

### EXAMPLE 3
```
Start-CMSScheduledTask -Name *test*
```

### EXAMPLE 4
```
$site | Start-CMSScheduledTask
```

### EXAMPLE 5
```
$scheduledTask | Start-CMSScheduleTask
```

## PARAMETERS

### -PassThru
Tell the cmdlet to return the web part category.

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
The scheduled task to start.

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### CMS.Scheduler.TaskInfo
The scheduled task to start.

### CMS.SiteProvider.SiteInfo
The site to get the scheduled tasks for.

## OUTPUTS

### CMS.Scheduler.TaskInfo[]

## NOTES

## RELATED LINKS
