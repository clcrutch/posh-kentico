---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Get-CMSSiteDomainAlias

## SYNOPSIS
Gets the domain aliases of the specified site.

## SYNTAX

### Property
```
Get-CMSSiteDomainAlias [-SiteName] <String> [<CommonParameters>]
```

### Object
```
Get-CMSSiteDomainAlias [-SiteToWork] <SiteInfo> [<CommonParameters>]
```

## DESCRIPTION
Gets the domain aliases of the specified site based off of the provided input.

This cmdlet returns the list of domain aliases when the -PassThru switch is used.

## EXAMPLES

### EXAMPLE 1
```
Get-CMSSiteDomainAlias -SiteName "basic"
```

### EXAMPLE 2
```
$site | Get-CMSSiteDomainAlias
```

## PARAMETERS

### -SiteName
The site name for the site.

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

### -SiteToWork
A reference to the site.

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### CMS.SiteProvider.SiteInfo
A reference to the site.

## OUTPUTS

## NOTES

## RELATED LINKS
