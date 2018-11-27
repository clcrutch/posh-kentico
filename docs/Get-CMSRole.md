---
external help file: PoshKentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Get-CMSRole

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

### None (Default)
```
Get-CMSRole [-RegularExpression] [<CommonParameters>]
```

### Role Name
```
Get-CMSRole [-RoleName] <String> [[-SiteName] <String>] [-RegularExpression] [<CommonParameters>]
```

### ID
```
Get-CMSRole [-RoleIds] <Int32[]> [-RegularExpression] [<CommonParameters>]
```

### User Name
```
Get-CMSRole [-UserName] <String> [-RegularExpression] [<CommonParameters>]
```

### User
```
Get-CMSRole [-User] <UserInfo> [-RegularExpression] [<CommonParameters>]
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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### CMS.Membership.UserInfo

## OUTPUTS

### CMS.Membership.RoleInfo[]

## NOTES

## RELATED LINKS
