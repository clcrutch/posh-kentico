
# DSC uses the Get-TargetResource function to fetch the status of the resource instance specified in the parameters for the target machine
function Get-TargetResource
{
	[CmdletBinding()]
    [OutputType([System.Collections.Hashtable])]
    param
    (
		[ValidateSet("Present", "Absent")]
		[string]$Ensure = "Present",

		[Parameter(Mandatory = $true)]
		[ValidateNotNullOrEmpty()]
		[string]$SiteName,

		[Parameter(Mandatory)]
		[ValidateNotNullOrEmpty()]
		[string]$DomainName,

		[ValidateSet("Running", "Stopped")]
		[string]$Status = "Running",

		[Parameter(Mandatory)]
		[ValidateNotNullOrEmpty()]
		[string]$DisplayName,

		[Parameter(Mandatory)]
		[ValidateNotNullOrEmpty()]
		[string[]]$DomainAliases
    )

	<# Insert logic that uses the mandatory parameter values to get the site and assign it to a variable called $Site #>
    <# Set $ensureResult to "Present" if the requested site exists and to "Absent" otherwise #>
	$site = Get-CMSSite -SiteName $SiteName
	$siteAliases = Get-CMSSiteDomainAlias -SiteName $SiteName

	if ($siteAlias.Count() -ne 0)
	{
		foreach ($alias in $siteAliases)
		{
			Write-Verbose -Message "Site $($SiteName) has alias $($alias.AliasName)"
		}
	}

    $getTargetResourceResult = $null;
    
    if ($site -ne $null)
    {
		# $Site is not null, Add all Website properties to the hash table
		$getTargetResourceResult = @{
										Ensure = "Present";
										SiteName = $site.SiteName;
										DisplayName = $site.DisplayName;
										DomainName = $site.DomainName;
										Status = $site.Status;
										DomainAliases = $siteAliases;
									}
	}
	else
	{
		# $Site is null, Add all Website properties to the hash table
		$getTargetResourceResult = @{
										Ensure = "Absent";
										SiteName = $null;
										DisplayName = $null;
										DomainName = $null;
										Status = $null;
										DomainAliases = $null;
									}
	}

    $getTargetResourceResult;
}

# The Set-TargetResource function is used to create, delete or configure a site on the target machine.
function Set-TargetResource
{
    [CmdletBinding(SupportsShouldProcess=$true)]
    param
    (
        [ValidateSet("Present", "Absent")]
        [string]$Ensure = "Present",

        [parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$SiteName,

        [Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$DomainName,

        [ValidateSet("Running", "Stopped")]
        [string]$Status = "Running",

		[Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$DisplayName,

		[Parameter(Mandatory)]
		[ValidateNotNullOrEmpty()]
		[string[]]$DomainAliases
    )

    <# If Ensure is set to "Present" and the site specified in the mandatory input parameters does not exist, then create it using the specified parameter values #>
    <# Else, if Ensure is set to "Present" and the site does exist, then update its properties to match the values provided in the non-mandatory parameter values #>
    <# Else, if Ensure is set to "Absent" and the site does not exist, then do nothing #>
    <# Else, if Ensure is set to "Absent" and the site does exist, then delete the site #>
	$site = Get-CMSSite -SiteName $SiteName
	
	if ($Ensure -eq "Present")
    {
        if ($site -eq $null)
        {
			Write-Verbose -Message "Creating the site $($SiteName)"
            New-CMSSite -DisplayName $DisplayName -SiteName $SiteName -Status $Status -DomainName $DomainName
			Write-Verbose -Message "Adding the site alias $($DomainAliases)"
			Add-CMSSiteDomainAlias -SiteName $SiteName -AliasNames $DomainAliases
        }
		else
		{
			Write-Verbose -Message "Updating the site $($SiteName)"
			Set-CMSSite -DisplayName $DisplayName -SiteName $SiteName -Status $Status -DomainName $DomainName
			Write-Verbose -Message "Adding the site alias $($DomainAliases)"
			Add-CMSSiteDomainAlias -SiteName $SiteName -AliasNames $DomainAliases
		}
    }
    else
    {
        if ($site -ne $null)
        {
            Write-Verbose -Message "Deleting the site alias $($DomainAliases)"
			Remove-CMSSiteDomainAlias -SiteName $SiteName -AliasNames $DomainAliases
            Write-Verbose -Message "Deleting the site $($SiteName)"
			Remove-CMSSite -SiteName $SiteName
        }
    }
}

function Test-TargetResource
{
	[CmdletBinding()]
	[OutputType([System.Boolean])]
	 param
    (
        [ValidateSet("Present", "Absent")]
        [string]$Ensure = "Present",

        [parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$SiteName,

        [Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$DomainName,

        [ValidateSet("Running", "Stopped")]
        [string]$Status = "Running",

		[Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$DisplayName,

		[Parameter(Mandatory)]
		[ValidateNotNullOrEmpty()]
		[string[]]$DomainAliases
    )

	Write-Verbose "Use this cmdlet to deliver information about command processing."

	Write-Debug "Use this cmdlet to write debug information while troubleshooting."

	$site = Get-CMSSite -SiteName $SiteName

	if ($null -ne $site) {
		$Ensure -eq "Present" -and
		$SiteName -eq $site.SiteName -and
		$DomainName -eq $site.DomainName -and
		$DisplayName -eq $site.DisplayName -and
		$DomainAliases -eq $site.DomainAliases -and
		$Status -eq $site.Status
	}
	else {
		$site -eq "Absent"
	}
}

Export-ModuleMember -Function *-TargetResource