---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Set-CMSRoleNoLog

## SYNOPSIS
Sets a new role without logging any staging tasks.

## SYNTAX

### Object
```
Set-CMSRoleNoLog [-RoleToSet] <RoleInfo> [<CommonParameters>]
```

### Property
```
Set-CMSRoleNoLog [-RoleName] <String> [-SiteID] <Int32> [-DisplayName] <String> [<CommonParameters>]
```

### PassThru
```
Set-CMSRoleNoLog [-PassThru] [<CommonParameters>]
```

## DESCRIPTION
{{Fill in the Description}}

## EXAMPLES

### EXAMPLE 1
```
Set-CMSRoleNoLog -Role $role
```

### EXAMPLE 2
```
$role | Set-CMSRoleNoLog
```

### EXAMPLE 3
```
Set-CMSRoleNoLog -RoleDisplayName "Role Display Name" -RoleName "Role Name" -SiteID "Site Id"
```

## PARAMETERS

### -RoleToSet
A reference to the role to set.

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

### -RoleName
The role name for the role to set.

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

### -SiteID
The role site id for the role to set.

```yaml
Type: Int32
Parameter Sets: Property
Aliases:

Required: True
Position: 1
Default value: 0
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisplayName
The display name for the role to set.

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
Tell the cmdlet to return the role to set.

```yaml
Type: SwitchParameter
Parameter Sets: PassThru
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

### CMS.Membership.RoleInfo
A reference to the role to set.

## OUTPUTS

### CMS.Membership.RoleInfo
## NOTES

## RELATED LINKS
