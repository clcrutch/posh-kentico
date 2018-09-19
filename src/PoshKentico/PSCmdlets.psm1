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

Export-ModuleMember -Function 'Set-CMSUserPrivilegeLevel'
