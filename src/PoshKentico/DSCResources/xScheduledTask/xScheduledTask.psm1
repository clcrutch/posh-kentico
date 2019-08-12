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

	$interval = $scheduledTask | Get-CMSScheduledTaskInterval
	$site = $scheduledTask | Get-CMSSite
	$siteName = $null

	if ($null -ne $site) {
		$siteName = $site.SiteName
	}

	$returnValue = @{
		Name = $Name
		AssemblyName = $scheduledTask.TaskAssemblyName
		ClassName = $scheduledTask.ClassName
		TaskData = $scheduledTask.TaskData
		DisplayName = $scheduledTask.TaskDisplayName
		Ensure = $Ensure
		Interval = $interval
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

		[parameter(Mandatory = $true)]
		[System.String]
		$TaskData,

		[parameter(Mandatory = $true)]
		[System.String]
		$DisplayName,

        [ValidateSet("Present","Absent")]
        [System.String]
        $Ensure,

		[parameter(Mandatory = $true)]
		[CMS.Scheduler.TaskInterval]
		$Interval,

		[parameter(Mandatory = $false)]
		[System.String]
		$SiteName
    )

	if ([string]::IsNullOrEmpty($Ensure)) {
		$Ensure = "Present"
	}

	$scheduledTask = Get-CMSScheduledTask -Name $Name

	if ($Ensure -eq "Present") {

		if ($null -ne $scheduledTask) {
			$scheduledTask.TaskAssemblyName = $AssemblyName
			$scheduledTask.TaskClass = $ClassName
			$scheduledTask.TaskData = $TaskData
			$scheduledTask.TaskDisplayName = $DisplayName
			
			if ($null -ne $Site) {
				$scheduledTask.TaskSiteID = $Site.SiteID
			}

			$scheduledTask | Set-CMSScheduledTask -Interval $Interval
		}
		else {
			if ($null -ne $Site) {
				New-CMSScheduledTask -AssemblyName $AssemblyName -Class $ClassName -Data $TaskData -DisplayName $DisplayName -Interval $Interval -Site $Site
			} else  {
				New-CMSScheduledTask -AssemblyName $AssemblyName -Class $ClassName -Data $TaskData -DisplayName $DisplayName -Interval $Interval
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

		[parameter(Mandatory = $true)]
		[System.String]
		$TaskData,

		[parameter(Mandatory = $true)]
		[System.String]
		$DisplayName,

        [ValidateSet("Present","Absent")]
        [System.String]
        $Ensure,

		[parameter(Mandatory = $true)]
		[CMS.Scheduler.TaskInterval]
		$Interval,

		[parameter(Mandatory = $false)]
		[CMS.SiteProvider.SiteInfo]
		$Site
    )

	$scheduledTask = Get-CMSScheduledTask -Name $Name
	$intervalString = [CMS.Scheduler.SchedulingHelper]::EncodeInterval($Interval)
	$site = $scheduledTask | Get-CMSSite

	if ($null -ne $scheduledTask) {
		$Ensure -eq "Present" -and
		$AssemblyName -eq $scheduledTask.TaskAssemblyName -and
		$ClassName -eq $scheduledTask.TaskClass -and
		$TaskData -eq $scheduledTask.TaskData -and
		$DisplayName -eq $scheduledTask.TaskDisplayName -and
		$intervalString -eq $scheduledTask.TaskInterval -and
		$Site.SiteID -eq $scheduledTask.TaskSiteID
	}
	else {
		$Ensure -eq "Absent"
	}
}


Export-ModuleMember -Function *-TargetResource

