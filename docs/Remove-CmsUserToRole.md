---
external help file: PoshKentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Remove-CmsUserToRole

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

### None
```
Remove-CmsUserToRole [-UserNameToRemove] <String> [-RegularExpression] [<CommonParameters>]
```

### Role Name
```
Remove-CmsUserToRole [-UserNameToRemove] <String> [-RoleName] <String> [[-SiteName] <String>]
 [-RegularExpression] [<CommonParameters>]
```

### ID
```
Remove-CmsUserToRole [-UserNameToRemove] <String> [-RoleIds] <Int32[]> [-RegularExpression]
 [<CommonParameters>]
```

### User Name
```
Remove-CmsUserToRole [-UserNameToRemove] <String> [-UserName] <String> [-RegularExpression]
 [<CommonParameters>]
```

### User
```
Remove-CmsUserToRole [-UserNameToRemove] <String> [-User] <UserInfo> [-RegularExpression] [<CommonParameters>]
```

### ROLE
```
Remove-CmsUserToRole [-UserNameToRemove] <String> [-Role] <RoleInfo> [-RegularExpression] [<CommonParameters>]
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

### -RegularExpression
{{Fill RegularExpression Description}}

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: Regex

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Role
{{Fill Role Description}}

```yaml
Type: RoleInfo
Parameter Sets: ROLE
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -RoleIds
{{Fill RoleIds Description}}

```yaml
Type: Int32[]
Parameter Sets: ID
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RoleName
{{Fill RoleName Description}}

```yaml
Type: String
Parameter Sets: Role Name
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SiteName
{{Fill SiteName Description}}

```yaml
Type: String
Parameter Sets: Role Name
Aliases:

Required: False
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -User
{{Fill User Description}}

```yaml
Type: UserInfo
Parameter Sets: User
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -UserName
{{Fill UserName Description}}

```yaml
Type: String
Parameter Sets: User Name
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UserNameToRemove
{{Fill UserNameToRemove Description}}

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### CMS.Membership.RoleInfo

### CMS.Membership.UserInfo

## OUTPUTS

### System.Object
## NOTES

## RELATED LINKS
