Import-Module ./posh-kentico.psd1

<#
Import-Module posh-kentico

Configuration KenticoTest
{
	Import-DscResource -Name xWebPartCategory

	Node localhost
	{
		xWebPartCategory TestCategory
		{
			Name = "Test"
            #DisplayName = "Test"
			Path = "Kentico:\Development\WebParts"
			Ensure = "Present"
		}
	}
}

KenticoTest -OutputPath .\Temp

Start-DscConfiguration -Wait -Force -Path .\Temp

Test-Path Kentico:\Development\WebParts\Test


#Remove-Item Kentico:\Development\WebParts\Test
#>

cd Kentico: