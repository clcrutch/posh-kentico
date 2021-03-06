function Get-DataTypeEnum
{
    [OutputType([PoshKentico.Business.Development.WebParts.FieldDataType])]
    param
    (
        [parameter(Mandatory = $true, Position = 0)]
        [string]
        $DataType
    )

    switch ($DataType)
    {
        "text" {
            [PoshKentico.Business.Development.WebParts.FieldDataType]::Text
        }
    }
}

function Get-DataTypeString
{
    [OutputType([string])]
    param
    (
        [parameter(Mandatory = $true, Position = 0)]
        [PoshKentico.Business.Development.WebParts.FieldDataType]
        $DataType
    )

    [PoshKentico.Business.Development.WebParts.FieldDataType].GetMembers()
}

function Get-TargetResource
{
    [CmdletBinding()]
    [OutputType([System.Collections.Hashtable])]
    param
    (
        [parameter(Mandatory = $true)]
        $Name,
        [parameter(Mandatory = $true)]
        [System.String]
        $WebPartPath
    )
    
	$field = Get-CMSWebPart -WebPartPath $WebPartPath | Get-CMSWebPartField -Name $Name

	if ($null -ne $webPart) {
		$Ensure = "Present"
	}
	else {
		$Ensure = "Absent"
	}
	
    $returnValue = @{
        Name = $Name
		Ensure = $Ensure
		WebPartPath = $WebPartPath
        Caption = $field.Caption
        DataType = $(Get-DataTypeEnum $field.DataType)
        DefaultValue = $field.DefaultValue
        Required = -not $field.AllowEmpty
        Size = $field.Size
    }

    $returnValue
}


function Set-TargetResource
{
    [CmdletBinding()]
    param
    (
        [parameter(Mandatory = $true)]
        $Name,

        [ValidateSet("Present","Absent")]
        [System.String]
        $Ensure,

        [System.String]
        $Caption,

        [PoshKentico.Business.Development.WebParts.FieldDataType]
        $DataType,

        [System.Object]
        $DefaultValue,

        [System.Boolean]
        $Required,

        [System.Int32]
        $Size,

        [parameter(Mandatory = $true)]
        [System.String]
        $WebPartPath
    )

	if ([string]::IsNullOrEmpty($Ensure)) {
		$Ensure = "Present"
	}

    $webPart = Get-CMSWebPart -WebPartPath $WebPartPath
    $field = $webPart | Get-CMSWebPartField -Name $Name

	if ($Ensure -eq "Present") {
		if ($null -ne $field) {
            $dataTypeString = [PoshKentico.Business.Development.WebParts.FieldDataType].GetMembers() | `
                Where-Object { 
                    $_.GetCustomAttributes([PoshKentico.Business.Development.WebParts.ValueAttribute])[0].Value -eq $DataType 
                }

            $field.AllowEmpty = -not $Required
            $field.Caption = $Caption
            $field.DataType = $dataTypeString
            $field.DefaultValue = $DefaultValue
            $field.Size = $Size

			$field | Set-CMSWebPartField
		}
		else {
            $webPart | Add-CMSWebPartField -Name $Name -DataType $DataType -Caption $Caption -DefaultValue $DefaultValue -Size $Size
		}
	}
	elseif ($Ensure -eq "Absent") {
		if ($null -ne $field) {
			$field | Remove-CMSWebPartField -Confirm:$false
		}
	}
}


function Test-TargetResource
{
    [CmdletBinding()]
    [OutputType([System.Boolean])]
    param
    (
        [parameter(Mandatory = $true)]
        $Name,

        [ValidateSet("Present","Absent")]
        [System.String]
        $Ensure,

        [System.String]
        $Caption,

        [PoshKentico.Business.Development.WebParts.FieldDataType]
        $DataType,

        [System.Object]
        $DefaultValue,

        [System.Boolean]
        $Required,

        [System.Int32]
        $Size,

        [parameter(Mandatory = $true)]
        [System.String]
        $WebPartPath
    )

    $field = Get-CMSWebPart -WebPartPath $WebPartPath | Get-CMSWebPartField -Name $Name

	if ($null -ne $field) {
		$Ensure -eq "Present" -and
		$Caption -eq $field.Caption -and
        $(Get-DataTypeString $DataType) -eq $field.DataType -and
        $DefaultValue -eq $field.DefaultValue -and 
        $Required -eq -not $field.AllowEmpty -and
        $Size -eq $field.Size
	}
	else {
		$Ensure -eq "Absent"
	}
}


Export-ModuleMember -Function *-TargetResource

