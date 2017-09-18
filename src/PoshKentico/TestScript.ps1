# Requires -RunAsAdministrator

Import-Module posh-kentico

Configuration KenticoTest
{
	Import-DscResource -Name xWebPartCategory
	Import-DscResource -Name xWebPart

	Node localhost
	{
		xWebPartCategory TestCategory
		{
			Name = "TestCategory"
			Path = "Kentico:\Development\WebParts"
			Ensure = "Present"
		}

		xWebPart TestWebPart
		{
			Name = "TestWebPart"
			Path = "Kentico:\Development\WebParts\TestCategory"
			Ensure = "Absent"
			FileName = "AbuseReport/AbuseReport.ascx"
		}
	}
}

KenticoTest -OutputPath .\Temp

Start-DscConfiguration -Wait -Force -Path .\Temp

cd Kentico: