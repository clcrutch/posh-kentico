Import-Module posh-kentico

function Get-TargetResource
{
    [CmdletBinding()]
    [OutputType([System.Collections.Hashtable])]
    param
    (
        [parameter(Mandatory = $true)]
        [System.String]
        $Name
    )

	$scheduledTask = Get-CMSScheduledTask -Name $Name

	if ($null -ne $scheduledTask) {
		$Ensure = "Present"
	}
	else {
		$Ensure = "Absent"
	}

	$encodedInterval = $scheduledTask | Get-CMSScheduledTaskInterval | ConvertFrom-CMSScheduledTaskInterval

	$taskData = $scheduledTask.TaskData
	if ($taskData -eq '') {
		$taskData = $null
	}

	$site = $scheduledTask | Get-CMSSite
	$siteName = $null
	if ($null -ne $site) {
		$siteName = $site.SiteName
	}

	$returnValue = @{
		Name = $Name
		AssemblyName = $scheduledTask.TaskAssemblyName
		ClassName = $scheduledTask.ClassName
		TaskData = $taskData
		DisplayName = $scheduledTask.TaskDisplayName
		Ensure = $Ensure
		EncodedInterval = $encodedInterval
		ServerName = $scheduledTask.TaskServerName
		SiteName = $siteName
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
		$AssemblyName,

		[parameter(Mandatory = $true)]
		[System.String]
		$ClassName,

		[parameter(Mandatory = $false)]
		[System.String]
		$TaskData,

		[parameter(Mandatory = $true)]
		[System.String]
		$DisplayName,

        [ValidateSet("Present","Absent")]
        [System.String]
        $Ensure,

		[parameter(Mandatory = $true)]
		[System.String]
		$EncodedInterval,

		[parameter(Mandatory = $false)]
		[System.String]
		$ServerName,

		[parameter(Mandatory = $false)]
		[System.String]
		$SiteName
    )

	if ([string]::IsNullOrEmpty($Ensure)) {
		$Ensure = "Present"
	}

	if ($null -eq $TaskData) {
		$TaskData = ''
	}

	if (-not [string]::IsNullOrEmpty($SiteName)) {
		$site = Get-CMSSite -SiteName $SiteName
	}

	$scheduledTask = Get-CMSScheduledTask -Name $Name
	$interval = $EncodedInterval | ConvertTo-CMSScheduledTaskInterval

	if ($Ensure -eq "Present") {

		if ($null -ne $scheduledTask) {
			$scheduledTask.TaskAssemblyName = $AssemblyName
			$scheduledTask.TaskClass = $ClassName
			$scheduledTask.TaskData = $TaskData
			$scheduledTask.TaskDisplayName = $DisplayName
			$scheduledTask.TaskServerName = $ServerName
			
			if ($null -ne $site) {
				$scheduledTask.TaskSiteID = $site.SiteID
			}

			$scheduledTask | Set-CMSScheduledTask -Interval $interval
		}
		else {
			$dataEmpty = [string]::IsNullOrEmpty($TaskData)

			if ($null -ne $site) {
				if ($dataEmpty) {
					New-CMSScheduledTask -AssemblyName $AssemblyName -Class $ClassName -DisplayName $DisplayName -Name $Name -Interval $interval -Site $site
				}
				else {
					New-CMSScheduledTask -AssemblyName $AssemblyName -Class $ClassName -Data $TaskData -DisplayName $DisplayName -Name $Name -Interval $interval -Site $site
				}
			} else  {
				if ($dataEmpty) {
					New-CMSScheduledTask -AssemblyName $AssemblyName -Class $ClassName -DisplayName $DisplayName -Name $Name -Interval $interval
				}
				else {
					New-CMSScheduledTask -AssemblyName $AssemblyName -Class $ClassName -Data $TaskData -DisplayName $DisplayName -Name $Name -Interval $interval
				}
			}
		}
	}
	elseif ($Ensure -eq "Absent") {
		if ($null -ne $scheduledTask) {
			$scheduledTask | Remove-CMSScheduledTask -Confirm:$false
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
		$AssemblyName,

		[parameter(Mandatory = $true)]
		[System.String]
		$ClassName,

		[parameter(Mandatory = $false)]
		[System.String]
		$TaskData,

		[parameter(Mandatory = $true)]
		[System.String]
		$DisplayName,

        [ValidateSet("Present","Absent")]
        [System.String]
        $Ensure,

		[parameter(Mandatory = $true)]
		[System.String]
		$EncodedInterval,

		[parameter(Mandatory = $false)]
		[System.String]
		$ServerName,

		[parameter(Mandatory = $false)]
		[System.String]
		$SiteName
    )

	$scheduledTask = Get-CMSScheduledTask -Name $Name

	if (-not [string]::IsNullOrEmpty($SiteName)) {
		$site = Get-CMSSite -SiteName $SiteName
	}

	if ($null -eq $TaskData) {`
		$TaskData = ''
	}

	if ($null -ne $scheduledTask) {
		$result = $Ensure -eq "Present" -and
		$AssemblyName -eq $scheduledTask.TaskAssemblyName -and
		$ClassName -eq $scheduledTask.TaskClass -and
		$TaskData -eq $scheduledTask.TaskData -and
		$DisplayName -eq $scheduledTask.TaskDisplayName -and
		$EncodedInterval -eq $scheduledTask.TaskInterval -and
		$ServerName -eq $scheduledTask.TaskServerName
		
		if ($null -ne $site ) {
			$result = $result -and $site.SiteID -eq $scheduledTask.TaskSiteID
		}

		$result
	}
	else {
		$Ensure -eq "Absent"
	}
}


Export-ModuleMember -Function *-TargetResource

