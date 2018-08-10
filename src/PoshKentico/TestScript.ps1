# Requires -RunAsAdministrator

$VerbosePreference = "Continue"
$DebugPreference = "Continue"
Import-Module posh-kentico

Configuration KenticoTest
{
	Import-DscResource -Name xWebPartCategory
	Import-DscResource -Name xWebPart
	Import-DscResource -Name xSite

	Node localhost
	{
		#xSite TestSite
		#{
		#	SiteName = "TestSite"
		#	DomainName = "localhost"
		#	DisplayName = "TestSite"
		#	Status = "Running"
		#	Ensure = "Present"
		#}

	}
}

KenticoTest -OutputPath .\Temp

Start-DscConfiguration -Wait -Force -Path .\Temp
