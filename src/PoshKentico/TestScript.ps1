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
		xWebPart TestWebPart
		{
			Name = "TestWebPart"
			Path = "Kentico:\Development\WebParts\TestCategory"
			Ensure = "Absent"
			FileName = "AbuseReport/AbuseReport.ascx"
		}
		
		xSite TestSite
		{
			SiteName = "LightStream"
			DomainName = "localhost"
			DisplayName = "LightStream.com"
			Status = "Running"
			Ensure = "Present"
		}

	}
}

KenticoTest -OutputPath .\Temp

Start-DscConfiguration -Wait -Force -Path .\Temp

cd Kentico: