
# DSC uses the Get-TargetResource function to fetch the status of the resource instance specified in the parameters for the target machine
function Get-TargetResource
{
	[CmdletBinding()]
    [OutputType([System.Collections.Hashtable])]
    param
    (
		[ValidateSet("Present", "Absent")]
        [string]$Ensure = "Present",

        [parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$ServerName,

		[parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$ServerSiteName,

        [Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$ServerDisplayName,

        [Parameter(Mandatory)]
        [bool]$ServerEnabled = "True",

		[ValidateSet("UserName", "X509")]
        [string]$ServerAuthentication = "UserName",

		[Parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$ServerURL,

		[Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$ServerUsername,

		[Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$ServerPassword
    )

	<# Insert logic that uses the mandatory parameter values to get the server and assign it to a variable called $Server #>
    <# Set $ensureResult to "Present" if the requested server exists and to "Absent" otherwise #>
	$getTargetResourceResult = @{
									Ensure = "Absent";
									ServerName = $null;
									ServerSiteName = $null;
									ServerDisplayName = $null;
									ServerEnabled = $null;
									ServerURL = $null;
									ServerAuthentication = $null;
									ServerUsername = $null;
									ServerPassword = $null;
								}
	$site = Get-CMSSite -SiteName $ServerSiteName

	if ($site -ne $null)
	{
        Write-Verbose -Message "The Site with SiteName $($ServerSiteName) exist"
		$Server = Get-CMSServer -SiteID $site.SiteID -ServerName $ServerName
		
		if ($Server -ne $null)
		{
			# $Server is not null, Add all server properties to the hash table
			$getTargetResourceResult = @{
											Ensure = "Present";
											ServerName = $Server.ServerName;
											ServerSiteName = $ServerSiteName;
											ServerDisplayName = $Server.ServerDisplayName;
											ServerEnabled = $Server.ServerEnabled;
											ServerURL = $Server.ServerURL;
											ServerAuthentication = $Server.ServerAuthentication;
											ServerUsername = $Server.ServerUsername;
											ServerPassword = $Server.ServerPassword;
										}
		}
	}
	else
	{
		Write-Verbose -Message "The Site with SiteName $($ServerSiteName) does not exist"
	}

    $getTargetResourceResult;
}

# The Set-TargetResource function is used to create, delete or configure a server on the target machine.
function Set-TargetResource
{
    [CmdletBinding(SupportsShouldProcess=$true)]
    param
    (
        [ValidateSet("Present", "Absent")]
        [string]$Ensure,

        [parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$ServerName,

		[parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$ServerSiteName,

        [Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$ServerDisplayName,

        [Parameter(Mandatory)]
        [bool]$ServerEnabled,

		[ValidateSet("UserName", "X509")]
        [string]$ServerAuthentication,

		[Parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$ServerURL,

		[Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$ServerUsername,

		[Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$ServerPassword
    )

    <# If Ensure is set to "Present" and the server specified in the mandatory input parameters does not exist, then create it using the specified parameter values #>
    <# Else, if Ensure is set to "Present" and the server does exist, then update its properties to match the values provided in the non-mandatory parameter values #>
    <# Else, if Ensure is set to "Absent" and the server does not exist, then do nothing #>
    <# Else, if Ensure is set to "Absent" and the server does exist, then delete the server #>
	$site = Get-CMSSite -SiteName $ServerSiteName

	if ($site -ne $null)
	{
        Write-Verbose -Message "The Site with SiteName $($ServerSiteName) exist"
	    $server = Get-CMSServer -SiteID $site.SiteID -ServerName $ServerName

	    if ($Ensure -eq "Present")
        {
            if($server -eq $null)
            {
			    Write-Verbose -Message "Creating the server $($ServerName)"
                New-CMSServer -SiteID $site.SiteID -DisplayName $ServerDisplayName -ServerName $ServerName -URL $ServerURL -Authentication $ServerAuthentication -Enabled $ServerEnabled -UserName $ServerUsername -Password $ServerPassword
            }
		    else
		    {
			    Write-Verbose -Message "Updating the server $($ServerName)"
			    Set-CMSServer -ServerName $ServerName -SiteID $site.SiteID -DisplayName $ServerDisplayName -URL $ServerURL -ServerName $ServerName -Authentication $ServerAuthentication -Enabled $ServerEnabled -UserName $ServerUsername -Password $ServerPassword
		    }
        }
        else
        {
            if ($server -ne $null)
            {
                Write-Verbose -Message "Deleting the server $($ServerName)"
                Remove-CMSServer -SiteID $site.SiteID -ServerName $ServerName
            }
        }
    }
	else
	{
		Write-Verbose -Message "The Site with SiteName $($ServerSiteName) does not exist"
	}
}

function Test-TargetResource
{
	[CmdletBinding()]
	[OutputType([System.Boolean])]
	 param
    (
        [ValidateSet("Present", "Absent")]
        [string]$Ensure,

        [parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$ServerName,

		[parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$ServerSiteName,

        [Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$ServerDisplayName,

        [Parameter(Mandatory)]
        [bool]$ServerEnabled,

		[ValidateSet("UserName", "X509")]
        [string]$ServerAuthentication,

		[Parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$ServerURL,

		[Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$ServerUsername,

		[Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$ServerPassword
    )

	Write-Verbose "Use this cmdlet to deliver information about command processing."

	Write-Debug "Use this cmdlet to write debug information while troubleshooting."
	$site = Get-CMSSite -SiteName $ServerSiteName

	if ($site -ne $null)
	{
		Write-Verbose -Message "The Site with SiteName $($ServerSiteName) exist"

	    $server = Get-CMSServer -SiteID $site.SiteID -ServerName $ServerName

        if ($Ensure -eq "Present")
        {
           Write-Verbose -Message "The Server with ServerName $($ServerName) exist"
           return $null -ne $server
        }
        else
        {
            return $null -eq $server
        }
    }
	else
	{
		Write-Verbose -Message "The Site with SiteName $($ServerSiteName) does not exist"
	}
    return $false
}

Export-ModuleMember -Function *-TargetResource