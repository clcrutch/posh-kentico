---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Remove-CMSUser

## SYNOPSIS
Removes the users selected by the provided input.

## SYNTAX

### None (Default)
```
Remove-CMSUser [-Exact] [-WhatIf] [-Confirm] [<CommonParameters>]
```

### User
```
Remove-CMSUser -User <UserInfo> [-Exact] [-WhatIf] [-Confirm] [<CommonParameters>]
```

### User Name
```
Remove-CMSUser [[-UserName] <String>] [-Exact] [-WhatIf] [-Confirm] [<CommonParameters>]
```

### ID
```
Remove-CMSUser [[-ID] <Int32[]>] [-Exact] [-WhatIf] [-Confirm] [<CommonParameters>]
```

## DESCRIPTION
Removes the users selected by the provided input.
This command automatically initializes the connection to Kentico if not already initialized.

Without parameters, this command removes all users.

With parameters, this command removes the users that match the criteria.

## EXAMPLES

### EXAMPLE 1
```
Remove-CMSUser
```

### EXAMPLE 2
```
Remove-CMSUser user
```

### EXAMPLE 3
```
Remove-CMSUser -UserName "NewUser" -Exact
```

### EXAMPLE 4
```
Remove-CMSUser -ID 1,3
```

### EXAMPLE 5
```
Remove-CMSUser -User $user
```

### EXAMPLE 6
```
$user | Remove-CMSUser
```

## PARAMETERS

### -User
The display name of the user to retrive.

```yaml
Type: UserInfo
Parameter Sets: User
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -UserName
The display name of the user to retrive.

```yaml
Type: String
Parameter Sets: User Name
Aliases:

Required: False
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ID
The IDs of the user to retrieve.

```yaml
Type: Int32[]
Parameter Sets: ID
Aliases:

Required: False
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Exact
If set, the match is exact, else the match performs a contains for display name and category name and starts with for path.

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

### CMS.Membership.UserInfo
The display name of the user to retrive.

## OUTPUTS

### CMS.Membership.UserInfo[]
## NOTES

## RELATED LINKS
