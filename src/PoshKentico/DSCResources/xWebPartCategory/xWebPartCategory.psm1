Import-Module posh-kentico

function Get-TargetResource
{
    [CmdletBinding()]
    [OutputType([System.Collections.Hashtable])]
    param
    (
        [parameter(Mandatory = $true)]
        [System.String]
        $Name,

        [parameter(Mandatory = $true)]
        [System.String]
        $Path
    )

	$fullPath = "$Path\$Name"

	if (Test-Path $fullPath) {
		$Ensure = "Present"
	}
	else {
		$Ensure = "Absent"
	}
	
	$properties = Get-ItemProperty -Path $fullPath
    $returnValue = @{
		Name = $Name
		Path = $Path
		Ensure = $Ensure
		DisplayName = $properties.DisplayName
		ImagePath = $properties.ImagePath
    }

    $returnValue
}


function Set-TargetResource
{
    [CmdletBinding()]
    param
    (
        [parameter(Mandatory = $true)]
        [System.String]
        $Name,

        [parameter(Mandatory = $true)]
        [System.String]
        $Path,
		
        [ValidateSet("Present","Absent")]
        [System.String]
        $Ensure,

        [System.String]
        $DisplayName,

        [System.String]
        $ImagePath
    )

	$fullPath = "$Path\$Name"

	if ([string]::IsNullOrEmpty($Ensure)) {
		$Ensure = "Present"
	}

	if ([string]::IsNullOrEmpty($DisplayName)) {
		$DisplayName = $Name
	}

	if ($Ensure -eq "Present") {
		if (Test-Path $fullPath) {
			Set-ItemProperty -Path $Path -Name DisplayName -Value $DisplayName

			if (-not [string]::IsNullOrEmpty($ImagePath)) {
				Set-ItemProperty -Path $Path -Name ImagePath -Value $ImagePath
			}
		}
		else {
			New-Item -ItemType WebPartCategory -Path $Path -Name $Name -DisplayName $DisplayName -ImagePath $ImagePath
		}
	}
	elseif ($Ensure -eq "Absent") {
		if (Test-Path $fullPath) {
			Remove-Item $fullPath -Recurse
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
        [System.String]
        $Name,

        [parameter(Mandatory = $true)]
        [System.String]
        $Path,
		
        [ValidateSet("Present","Absent")]
        [System.String]
        $Ensure,

        [System.String]
        $DisplayName,

        [System.String]
        $ImagePath
    )

	$fullPath = "$Path\$Name"

	if ([string]::IsNullOrEmpty($DisplayName)) {
		$DisplayName = $Name
	}

	if (Test-Path $fullPath) {
		$properties = Get-ItemProperty -Path $fullPath

		$Ensure -eq "Present" -and
		$DisplayName -eq $properties.DisplayName -and
		$ImagePath -eq $properties.ImagePath
	}
	else {
		$Ensure -eq "Absent"
	}
}


Export-ModuleMember -Function *-TargetResource

