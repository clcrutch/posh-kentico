
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
        [string]$LibraryName,

		[parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$LibrarySiteName,

        [Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$LibraryDisplayName,

		[Parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$LibraryFolder,

		[Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$LibraryDescription
    )

	<# Insert logic that uses the mandatory parameter values to get the library and assign it to a variable called $Library #>
    <# Set $ensureResult to "Present" if the requested library exists and to "Absent" otherwise #>
	$getTargetResourceResult = @{
									Ensure = "Absent";
									LibraryName = $null;
									LibrarySiteName = $null;
									LibraryDisplayName = $null;
									LibraryFolder = $null;
									LibraryDescription = $null;
								}
	$site = Get-CMSSite -SiteName $LibrarySiteName -Exact

	if ($site -ne $null)
	{
        Write-Verbose -Message "The Site with SiteName $($LibrarySiteName) exist"
		$library = Get-CMSMediaLibrary -SiteID $site.SiteID -LibraryName $LibraryName -Exact
		
		if ($library -ne $null)
		{
			# $Library is not null, Add all library properties to the hash table
			$getTargetResourceResult = @{
											Ensure = "Present";
											LibraryName = $Library.LibraryName;
											LibrarySiteName = $LibrarySiteName;
											LibraryDisplayName = $Library.LibraryDisplayName;
											LibraryFolder = $Library.LibraryFolder;
											LibraryDescription = $Library.LibraryDescription;
										}
		}
	}
	else
	{
		Write-Verbose -Message "The Site with SiteName $($LibrarySiteName) does not exist"
	}

    $getTargetResourceResult;
}

# The Set-TargetResource function is used to create, delete or configure a library on the target machine.
function Set-TargetResource
{
    [CmdletBinding(SupportsShouldProcess=$true)]
    param
    (
        [ValidateSet("Present", "Absent")]
        [string]$Ensure,

        [parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$LibraryName,

		[parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$LibrarySiteName,

        [Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$LibraryDisplayName,

		[Parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$LibraryFolder,

		[Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$LibraryDescription
    )

    <# If Ensure is set to "Present" and the library specified in the mandatory input parameters does not exist, then create it using the specified parameter values #>
    <# Else, if Ensure is set to "Present" and the library does exist, then update its properties to match the values provided in the non-mandatory parameter values #>
    <# Else, if Ensure is set to "Absent" and the library does not exist, then do nothing #>
    <# Else, if Ensure is set to "Absent" and the library does exist, then delete the library #>
	$site = Get-CMSSite -SiteName $LibrarySiteName -Exact

	if ($site -ne $null)
	{
        Write-Verbose -Message "The Site with SiteName $($LibrarySiteName) exist"
	    $library = Get-CMSMediaLibrary -SiteID $site.SiteID -LibraryName $LibraryName -Exact

	    if ($Ensure -eq "Present")
        {
            if($library -eq $null)
            {
			    Write-Verbose -Message "Creating the library $($LibraryName)"
				New-CMSMediaLibrary -SiteID $site.SiteID -DisplayName $LibraryDisplayName -LibraryName $LibraryName -Description $LibraryDescription -Folder $LibraryFolder
            }
		    else
		    {
			    Write-Verbose -Message "Updating the library $($LibraryName)"
			    Set-CMSMediaLibrary -SiteID $site.SiteID -DisplayName $LibraryDisplayName -LibraryName $LibraryName -Description $LibraryDescription -Folder $LibraryFolder
		    }
        }
        else
        {
            if ($library -ne $null)
            {
                Write-Verbose -Message "Deleting the library $($LibraryName)"
                Remove-CMSMediaLibrary -SiteID $site.SiteID -LibraryName $LibraryName -Exact
            }
        }
    }
	else
	{
		Write-Verbose -Message "The Site with SiteName $($LibrarySiteName) does not exist"
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
        [string]$LibraryName,

		[parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$LibrarySiteName,

        [Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$LibraryDisplayName,

		[Parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$LibraryFolder,

		[Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]$LibraryDescription
    )

	Write-Verbose "Use this cmdlet to deliver information about command processing."

	Write-Debug "Use this cmdlet to write debug information while troubleshooting."
	$site = Get-CMSSite -SiteName $LibrarySiteName -Exact

	if ($site -ne $null)
	{
		Write-Verbose -Message "The Site with SiteName $($LibrarySiteName) exist"

	    $library = Get-CMSMediaLibrary -SiteID $site.SiteID -LibraryName $LibraryName -Exact

        if ($Ensure -eq "Present")
        {
           Write-Verbose -Message "The Library with LibraryName $($LibraryName) exist"
           return $null -ne $library
        }
        else
        {
            return $null -eq $library
        }
    }
	else
	{
		Write-Verbose -Message "The Site with SiteName $($LibrarySiteName) does not exist"
	}
    return $false
}

Export-ModuleMember -Function *-TargetResource