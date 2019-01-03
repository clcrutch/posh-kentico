---
external help file: PoshKentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Remove-CMSScheduledTask

## SYNOPSIS
{{Fill in the Synopsis}}

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
{{Fill in the Description}}

## EXAMPLES

### Example 1
```powershell
PS C:\> {{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

### -AssemblyName
{{Fill AssemblyName Description}}

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

### -Name
{{Fill Name Description}}

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
{{Fill RegularExpression Description}}

```yaml
Type: SwitchParameter
Parameter Sets: Assembly Name, Site and Assembly Name, Name, Site and Name
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
Parameter Sets: Scheduled Task
Aliases: Task, TaskInfo

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
Parameter Sets: Site and Assembly Name, Site and Name, Site
Aliases:

Required: True
Position: Named
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

### CMS.Scheduler.TaskInfo

### CMS.SiteProvider.SiteInfo

## OUTPUTS

### System.Object
## NOTES

## RELATED LINKS
