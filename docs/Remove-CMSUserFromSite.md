---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Remove-CMSUserFromSite

## SYNOPSIS
Removes the users from the site selected by the provided input.

## SYNTAX

### Site Name
```
Remove-CMSUserFromSite [-User] <UserInfo> [-SiteName] <String> [<CommonParameters>]
```

### Site
```
Remove-CMSUserFromSite [-User] <UserInfo> [-Site] <SiteInfo> [<CommonParameters>]
```

## DESCRIPTION
Removes the users from the site selected by the provided input.
This command automatically initializes the connection to Kentico if not already initialized.

## EXAMPLES

### EXAMPLE 1
```
$user | Remove-CMSUserFromSite -Site $site
```

### EXAMPLE 2
```
$user | Remove-CMSUserFromSite -SiteName "MySite"
```

### EXAMPLE 3
```
Remove-CMSUserFromSite -User $user -Site $site
```

### EXAMPLE 4
```
Remove-CMSUserFromSite -User $user -SiteName "MySite"
```

## PARAMETERS

### -User
The display name of the user to retrive.

```yaml
Type: UserInfo
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -SiteName
The display name of the user to retrive.

```yaml
Type: String
Parameter Sets: Site Name
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Site
The IDs of the user to retrieve.

```yaml
Type: SiteInfo
Parameter Sets: Site
Aliases:

Required: True
Position: 1
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

## NOTES

## RELATED LINKS
