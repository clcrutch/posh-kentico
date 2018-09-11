---
external help file: posh-kentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# New-CMSSite

## SYNOPSIS
Creates a new site.

## SYNTAX

```
New-CMSSite [-DisplayName] <String> [[-SiteName] <String>] [[-Status] <SiteStatusEnum>] [-DomainName] <String>
 [-PassThru] [<CommonParameters>]
```

## DESCRIPTION
Creates a new site based off of the provided input.

This cmdlet returns the newly created site when the -PassThru switch is used.

## EXAMPLES

### EXAMPLE 1
```
New-CMSSite -DisplayName "My Test Name" -DomainName "My Domain Name"
```

### EXAMPLE 2
```
New-CMSSite -DisplayName "My Test Name" -SiteName "My Site Name" -Status "Running or Stopped" -DomainName "My Domain Name"
```

## PARAMETERS

### -DisplayName
The display name for the newly created site.

Site display name cannot be blank.

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

### -SiteName
The site name for the newly created site.

If null, then the display name is used for the site name.

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

### -Status
The status for the newly created site.

Possible values: Running, Stopped

```yaml
Type: SiteStatusEnum
Parameter Sets: (All)
Aliases:
Accepted values: Running, Stopped

Required: False
Position: 2
Default value: Running
Accept pipeline input: False
Accept wildcard characters: False
```

### -DomainName
The domain name for the newly created site.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 3
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PassThru
Tell the cmdlet to return the newly created site.

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

### CMS.SiteProvider.SiteInfo[]

## NOTES

## RELATED LINKS
