---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Get-CMSUser

## SYNOPSIS
Gets the users selected by the provided input.

## SYNTAX

### None (Default)
```
Get-CMSUser [-Exact] [<CommonParameters>]
```

### User Name
```
Get-CMSUser [[-UserName] <String>] [-Exact] [<CommonParameters>]
```

### ID
```
Get-CMSUser [[-ID] <Int32[]>] [-Exact] [<CommonParameters>]
```

## DESCRIPTION
Gets the users selected by the provided input.
This command automatically initializes the connection to Kentico if not already initialized.

Without parameters, this command returns all users.

With parameters, this command returns the users that match the criteria.

## EXAMPLES

### EXAMPLE 1
```
Get-CMSUser
```

### EXAMPLE 2
```
Get-CMSUser user
```

### EXAMPLE 3
```
Get-CMSUser -UserName "NewUser" -Exact
```

### EXAMPLE 4
```
Get-CMSUser -ID 1,3
```

## PARAMETERS

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

### CMS.Membership.UserInfo[]
## NOTES

## RELATED LINKS
