---
external help file: PoshKentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Set-CMSRoleLog

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

### Object
```
Set-CMSRoleLog [-RoleToSet] <RoleInfo> [-TaskGroupName] <String> [-PassThru] [<CommonParameters>]
```

### Property
```
Set-CMSRoleLog [-RoleName] <String> [-SiteID] <Int32> [-DisplayName] <String> [-TaskGroupName] <String>
 [-PassThru] [<CommonParameters>]
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

### -DisplayName
{{Fill DisplayName Description}}

```yaml
Type: String
Parameter Sets: Property
Aliases:

Required: True
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PassThru
{{Fill PassThru Description}}

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RoleName
{{Fill RoleName Description}}

```yaml
Type: String
Parameter Sets: Property
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RoleToSet
{{Fill RoleToSet Description}}

```yaml
Type: RoleInfo
Parameter Sets: Object
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -SiteID
{{Fill SiteID Description}}

```yaml
Type: Int32
Parameter Sets: Property
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TaskGroupName
{{Fill TaskGroupName Description}}

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### CMS.Membership.RoleInfo

## OUTPUTS

### CMS.Membership.RoleInfo

## NOTES

## RELATED LINKS
