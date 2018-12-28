---
external help file: PoshKentico.dll-Help.xml
Module Name: posh-kentico
online version:
schema: 2.0.0
---

# New-CMSUser

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

```
New-CMSUser [-UserName] <String> [[-FullName] <String>] [[-Email] <String>] [[-PreferredCultureCode] <String>]
 [[-SiteIndependentPrivilegeLevel] <UserPrivilegeLevelEnum>] [-PassThru] [<CommonParameters>]
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

### -Email
{{Fill Email Description}}

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

### -FullName
{{Fill FullName Description}}

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

### -PassThru
{{Fill PassThru Description}}

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PreferredCultureCode
{{Fill PreferredCultureCode Description}}

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
{{Fill SiteIndependentPrivilegeLevel Description}}

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

### -UserName
{{Fill UserName Description}}

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

### None

## OUTPUTS

### CMS.Membership.UserInfo

## NOTES

## RELATED LINKS
