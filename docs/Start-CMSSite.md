---
external help file: PoshKentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# Start-CMSSite

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

### Object
```
Start-CMSSite [-SiteToStart] <SiteInfo> [<CommonParameters>]
```

### Dislpay Name
```
Start-CMSSite [[-DisplayName] <String>] [-RegularExpression] [<CommonParameters>]
```

### ID
```
Start-CMSSite [[-SiteIds] <Int32[]>] [<CommonParameters>]
```

### User
```
Start-CMSSite [[-User] <UserInfo>] [<CommonParameters>]
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

### -DisplayName
{{Fill DisplayName Description}}

```yaml
Type: String
Parameter Sets: Dislpay Name
Aliases: SiteName, DomainName

Required: False
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RegularExpression
{{Fill RegularExpression Description}}

```yaml
Type: SwitchParameter
Parameter Sets: Dislpay Name
Aliases: Regex

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SiteIds
{{Fill SiteIds Description}}

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

### -SiteToStart
{{Fill SiteToStart Description}}

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

### -User
{{Fill User Description}}

```yaml
Type: UserInfo
Parameter Sets: User
Aliases:

Required: False
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

### CMS.Membership.UserInfo

## OUTPUTS

### System.Object
## NOTES

## RELATED LINKS
