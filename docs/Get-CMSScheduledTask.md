---
external help file: PoshKentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Get-CMSScheduledTask

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

### None (Default)
```
Get-CMSScheduledTask [<CommonParameters>]
```

### Assembly Name
```
Get-CMSScheduledTask -AssemblyName <String> [-RegularExpression] [<CommonParameters>]
```

### Site and Assembly Name
```
Get-CMSScheduledTask -AssemblyName <String> [-RegularExpression] -Site <SiteInfo> [<CommonParameters>]
```

### Name
```
Get-CMSScheduledTask [-Name] <String> [-RegularExpression] [<CommonParameters>]
```

### Site and Name
```
Get-CMSScheduledTask [-Name] <String> [-RegularExpression] -Site <SiteInfo> [<CommonParameters>]
```

### Site
```
Get-CMSScheduledTask -Site <SiteInfo> [<CommonParameters>]
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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### CMS.SiteProvider.SiteInfo

## OUTPUTS

### CMS.Scheduler.TaskInfo[]

## NOTES

## RELATED LINKS
