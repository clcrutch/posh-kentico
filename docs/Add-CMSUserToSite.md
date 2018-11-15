---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Add-CMSUserToSite

## SYNOPSIS
Adds the user to the site selected by the provided input.

## SYNTAX

### Site Name
```
Add-CMSUserToSite [-User] <UserInfo> [-SiteName] <String> [<CommonParameters>]
```

### Site
```
Add-CMSUserToSite [-User] <UserInfo> [-Site] <SiteInfo> [<CommonParameters>]
```

## DESCRIPTION
Adds the user user to the site selected by the provided input.
This command automatically initializes the connection to Kentico if not already initialized.

## EXAMPLES

### EXAMPLE 1
```
$user | Add-CMSUserToSite -Site $site
```

### EXAMPLE 2
```
$user | Add-CMSUserToSite -SiteName "MySite"
```

### EXAMPLE 3
```
Add-CMSUserToSite -User $user -Site $site
```

### EXAMPLE 4
```
Add-CMSUserToSite -User $user -SiteName "MySite"
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
