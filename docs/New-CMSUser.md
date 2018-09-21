---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# New-CMSUser

## SYNOPSIS
Creates a new user.

## SYNTAX

```
New-CMSUser [-UserName] <String> [[-FullName] <String>] [[-Email] <String>] [[-PreferredCultureCode] <String>]
 [[-SiteIndependentPrivilegeLevel] <UserPrivilegeLevelEnum>] [-PassThru] [<CommonParameters>]
```

## DESCRIPTION
Creates a new user based off of the provided input.

This cmdlet returns the newly created user when the -PassThru switch is used.

## EXAMPLES

### EXAMPLE 1
```
New-CMSUser -UserName "NewUser"
```

### EXAMPLE 2
```
New-CMSUser -UserName "NewUser" -FullName "New user" -Email "new.user@domain.com" -PreferredCultureCode "en-us" -SiteIndependentPrivilegeLevel UserPrivilegeLevelEnum.Editor
```

## PARAMETERS

### -UserName
The User name for the newly created user.

User name cannot be blank.

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

### -FullName
The full name for the newly created user.

```yaml
Type: String
Parameter Sets: (All)
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
Parameter Sets: (All)
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
Parameter Sets: (All)
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
Parameter Sets: (All)
Aliases:
Accepted values: None, Editor, Admin, GlobalAdmin

Required: False
Position: 4
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PassThru
Tell the cmdlet to return the newly created user.

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

## OUTPUTS

### CMS.Membership.UserInfo
## NOTES

## RELATED LINKS
