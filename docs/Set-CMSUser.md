---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Set-CMSUser

## SYNOPSIS
Sets a user.

## SYNTAX

### Object
```
Set-CMSUser [-User] <UserInfo> [-PassThru] [<CommonParameters>]
```

### Property
```
Set-CMSUser [-UserName] <String> [[-FullName] <String>] [[-Email] <String>] [[-PreferredCultureCode] <String>]
 [[-SiteIndependentPrivilegeLevel] <UserPrivilegeLevelEnum>] [-PassThru] [<CommonParameters>]
```

## DESCRIPTION
Sets a user based off of the provided input.

This cmdlet returns the updated user when the -PassThru switch is used.

## EXAMPLES

### EXAMPLE 1
```
Set-CMSUser -User $user
```

### EXAMPLE 2
```
$user | Set-CMSUser
```

### EXAMPLE 3
```
Set-CMSUser -UserName "NewUser" -FullName "New user" -Email "new.user@domain.com" -PreferredCultureCode "en-us" -SiteIndependentPrivilegeLevel UserPrivilegeLevelEnum.Editor
```

## PARAMETERS

### -User
A reference to the user to update.

```yaml
Type: UserInfo
Parameter Sets: Object
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -UserName
The User name for the newly created user.

User name cannot be blank.

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

### -FullName
The full name for the newly created user.

```yaml
Type: String
Parameter Sets: Property
Aliases:

Required: False
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Email
The email for the newly created user.

```yaml
Type: String
Parameter Sets: Property
Aliases:

Required: False
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PreferredCultureCode
The preferred culture code for the newly created user.

```yaml
Type: String
Parameter Sets: Property
Aliases:

Required: False
Position: 3
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SiteIndependentPrivilegeLevel
The preferred culture code for the newly created user.

Possible values: None, Editor, Admin, GlobalAdmin

```yaml
Type: UserPrivilegeLevelEnum
Parameter Sets: Property
Aliases:
Accepted values: None, Editor, Admin, GlobalAdmin

Required: False
Position: 4
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PassThru
Tell the cmdlet to return the user to update.

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### CMS.Membership.UserInfo
A reference to the user to update.

## OUTPUTS

### CMS.Membership.UserInfo
## NOTES

## RELATED LINKS
