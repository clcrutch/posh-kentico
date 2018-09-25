---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Set-CMSSite

## SYNOPSIS
Sets a site.

## SYNTAX

### Object
```
Set-CMSSite [-SiteToSet] <SiteInfo> [-PassThru] [<CommonParameters>]
```

### Property
```
Set-CMSSite [-DisplayName] <String> [-SiteName] <String> [-Status] <SiteStatusEnum> [-DomainName] <String>
 [-PassThru] [<CommonParameters>]
```

## DESCRIPTION
Sets a new site based off of the provided input.

This cmdlet returns the site to update when the -PassThru switch is used.

## EXAMPLES

### EXAMPLE 1
```
Set-CMSSite -Site $site
```

### EXAMPLE 2
```
$site | Set-CMSSite
```

### EXAMPLE 3
```
Set-CMSSite -DisplayName "My Test Name" -SiteName "My Site Name" -Status "Running or Stopped" -DomainName "My Domain Name"
```

## PARAMETERS

### -SiteToSet
A reference to the site to update.

```yaml
Type: SiteInfo
Parameter Sets: Object
Aliases: Site

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -DisplayName
The display name for the site to update.

Site display name cannot be blank.

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

### -SiteName
The site name for the site to update.

Site name cannot be blank.

```yaml
Type: String
Parameter Sets: Property
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Status
The status for the site to update.

Possible values: Running, Stopped

```yaml
Type: SiteStatusEnum
Parameter Sets: Property
Aliases:
Accepted values: Running, Stopped

Required: True
Position: 2
Default value: Running
Accept pipeline input: False
Accept wildcard characters: False
```

### -DomainName
The domain name for the site to update.

```yaml
Type: String
Parameter Sets: Property
Aliases:

Required: True
Position: 3
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PassThru
Tell the cmdlet to return the site to update.

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

### CMS.SiteProvider.SiteInfo
A reference to the site to update.

## OUTPUTS

### CMS.SiteProvider.SiteInfo[]

## NOTES

## RELATED LINKS
