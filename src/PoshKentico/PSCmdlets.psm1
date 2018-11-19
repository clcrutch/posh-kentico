# Requires -RunAsAdministrator

Import-Module posh-kentico

function Set-CMSUserPrivilegeLevel {
	[CmdletBinding()]
    param (
		[parameter(Mandatory=$true, ValueFromPipeline=$true)]
        [CMS.Membership.UserInfo]$User,

        [parameter(Mandatory=$true, Position = 0)]
        [CMS.Base.UserPrivilegeLevelEnum]$PrivilegeLevel
    )
    PROCESS {
        Write-Verbose "Starting cmdlet"
		Set-CMSUser -UserName $User.UserName -SiteIndependentPrivilegeLevel $PrivilegeLevel
    }
}

function Enable-CMSUser {
	[CmdletBinding()]
    param (
		[parameter(Mandatory=$true, ValueFromPipeline=$true)]
        [CMS.Membership.UserInfo]$User
    )
    PROCESS {
        Write-Verbose "Starting cmdlet"
		$User.Enabled = $true
		$User| Set-CMSUser
    }
}

function Disable-CMSUser {
	[CmdletBinding()]
    param (
		[parameter(Mandatory=$true, ValueFromPipeline=$true)]
        [CMS.Membership.UserInfo]$User
    )
    PROCESS {
        Write-Verbose "Starting cmdlet"
		$User.Enabled = $false
		$User| Set-CMSUser
    }
}

Export-ModuleMember -Function 'Set-CMSUserPrivilegeLevel'
Export-ModuleMember -Function 'Enable-CMSUser'
Export-ModuleMember -Function 'Disable-CMSUser'
