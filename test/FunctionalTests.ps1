﻿Import-Module Pester
Import-Module posh-kentico

Describe 'Kentico:' {
    It 'Test-Path returns True' {
        $result = Test-Path Kentico:

        $result | Should -Be $true
    }

    It 'ls returns 1 item when path = "Kentico:"' {
        Push-Location Kentico:\

        $result = Get-ChildItem -Path Kentico:

        $result.Count | Should -Be 1
        $result.Name -eq "Development"

        Pop-Location
    }
}

Describe 'WebPart Categories' {
    It 'Test-Path returns True if exists' {

        $result = Test-Path Kentico:\Development\WebParts\Maps

        $result | Should -Be $true
    }

    It 'Test-Path returns false if not exists' {
        $result = Test-Path Kentico:\Development\WebParts\DoesNotExist

        $result | Should -Be $false
    }

    It 'New-Item should create item' {
        New-Item -ItemType WebPartCategory -Path Kentico:\Development\WebParts -Name Test

        $result = Test-Path Kentico:\Development\WebParts\Test

        $result | Should -Be $true
    }

    It 'Get-Item should return the item' {
        $result = Get-Item Kentico:\Development\WebParts\Test

        $result | Should -Not -Be $null
        $result.CategoryName | Should -Be "Test"
    }

    It 'Remove-Item should remove item' {
        Remove-Item Kentico:\Development\WebParts\Test

        $result = Test-Path Kentico:\Development\WebParts\Test

        $result | Should -Be $false
    }

    It 'New-Item should set DisplayName to Name if not specified' {
        New-Item -ItemType WebPartCategory -Path Kentico:\Development\WebParts -Name Test

        $result = Get-Item Kentico:\Development\WebParts\Test
        
        $result.CategoryDisplayName | Should -Be "Test"
        
        Remove-Item Kentico:\Development\WebParts\Test
    }

    It 'New-Item should set DisplayName' {
        New-Item -ItemType WebPartCategory -Path Kentico:\Development\WebParts -Name Test -DisplayName "DisplayName"

        $result = Get-Item Kentico:\Development\WebParts\Test
        
        $result.CategoryDisplayName | Should -Be "DisplayName"
        
        Remove-Item Kentico:\Development\WebParts\Test
    }

    It 'Get-ItemProperty should return the item properties' {
        New-Item -ItemType WebPartCategory -Path Kentico:\Development\WebParts -Name Test -DisplayName "DisplayName"

        $result = Get-ItemProperty Kentico:\Development\WebParts\Test

        $result | Should -Not -Be $null
        $result.displayname | Should -Be "DisplayName"
        $result.imagepath | Should -Be ""
        
        Remove-Item Kentico:\Development\WebParts\Test
    }

    It 'Test WebPart Category Present with DSC (true)' {
        Configuration Test
        {
            Import-DscResource -Name xWebPartCategory

            Node $env:COMPUTERNAME
            {
                xWebPartCategory Maps
                {
                    Name = "Maps"
                    Path = "Kentico:\Development\WebParts"
                    Ensure = "Present"
                }
            }
        }

        Test -OutputPath .\Temp

        $result = Test-DscConfiguration .\Temp

        $result.InDesiredState | Should -Be $true
    }

    It 'Test WebPart Category Present with DSC (false)' {
        Configuration Test
        {
            Import-DscResource -Name xWebPartCategory

            Node $env:COMPUTERNAME
            {
                xWebPartCategory Maps
                {
                    Name = "Maps"
                    Path = "Kentico:\Development\WebParts"
                    Ensure = "Absent"
                }
            }
        }

        Test -OutputPath .\Temp

        $result = Test-DscConfiguration .\Temp

        $result.InDesiredState | Should -Be $false
    }

    It 'Test WebPart Category Absent with DSC (true)' {
        Configuration Test
        {
            Import-DscResource -Name xWebPartCategory

            Node $env:COMPUTERNAME
            {
                xWebPartCategory DoesNotExist
                {
                    Name = "DoesNotExist"
                    Path = "Kentico:\Development\WebParts"
                    Ensure = "Absent"
                }
            }
        }

        Test -OutputPath .\Temp

        $result = Test-DscConfiguration .\Temp

        $result.InDesiredState | Should -Be $true
    }

    It 'Test WebPart Category Absent with DSC (false)' {
        Configuration Test
        {
            Import-DscResource -Name xWebPartCategory

            Node $env:COMPUTERNAME
            {
                xWebPartCategory DoesNotExist
                {
                    Name = "DoesNotExist"
                    Path = "Kentico:\Development\WebParts"
                    Ensure = "Present"
                }
            }
        }

        Test -OutputPath .\Temp

        $result = Test-DscConfiguration .\Temp

        $result.InDesiredState | Should -Be $false
    }

    It 'Test WebPart Category Present with DSC with DisplayName (true)' {
        Configuration Test
        {
            Import-DscResource -Name xWebPartCategory

            Node $env:COMPUTERNAME
            {
                xWebPartCategory AbuseReport
                {
                    Name = "AbuseReport"
                    DisplayName = "Abuse report"
                    Path = "Kentico:\Development\WebParts"
                    Ensure = "Present"
                }
            }
        }

        Test -OutputPath .\Temp

        $result = Test-DscConfiguration .\Temp

        $result.InDesiredState | Should -Be $true
    }

    It 'Test WebPart Category Present with DSC with DisplayName (false)' {
        Configuration Test
        {
            Import-DscResource -Name xWebPartCategory

            Node $env:COMPUTERNAME
            {
                xWebPartCategory AbuseReport
                {
                    Name = "AbuseReport"
                    DisplayName = "Abuse report"
                    Path = "Kentico:\Development\WebParts"
                    Ensure = "Absent"
                }
            }
        }

        Test -OutputPath .\Temp

        $result = Test-DscConfiguration .\Temp

        $result.InDesiredState | Should -Be $false
    }

    It 'Test WebPart Category Present with DSC with wrong DisplayName (false)' {
        Configuration Test
        {
            Import-DscResource -Name xWebPartCategory

            Node $env:COMPUTERNAME
            {
                xWebPartCategory AbuseReport
                {
                    Name = "AbuseReport"
                    DisplayName = "Abuse report2"
                    Path = "Kentico:\Development\WebParts"
                    Ensure = "Present"
                }
            }
        }

        Test -OutputPath .\Temp

        $result = Test-DscConfiguration .\Temp

        $result.InDesiredState | Should -Be $false
    }

    It 'Create WebPart Category with DSC' {
        Configuration Test
        {
            Import-DscResource -Name xWebPartCategory

            Node $env:COMPUTERNAME
            {
                xWebPartCategory Test
                {
                    Name = "Test"
                    Path = "Kentico:\Development\WebParts"
                    Ensure = "Present"
                }
            }
        }

        Test -OutputPath .\Temp

        Start-DscConfiguration .\Temp -Wait -Force
        $result = Test-DscConfiguration .\Temp

        $result.InDesiredState | Should -Be $true

        Remove-Item Kentico:\Development\WebParts\Test
    }

    It 'Remove WebPart Category with DSC' {
        Configuration Test
        {
            Import-DscResource -Name xWebPartCategory

            Node $env:COMPUTERNAME
            {
                xWebPartCategory Test
                {
                    Name = "Test"
                    Path = "Kentico:\Development\WebParts"
                    Ensure = "Present"
                }
            }
        }

        Test -OutputPath .\Temp

        Start-DscConfiguration .\Temp -Wait -Force
        
        Configuration Test
        {
            Import-DscResource -Name xWebPartCategory

            Node $env:COMPUTERNAME
            {
                xWebPartCategory Test
                {
                    Name = "Test"
                    Path = "Kentico:\Development\WebParts"
                    Ensure = "Absent"
                }
            }
        }

        Test -OutputPath .\Temp

        Start-DscConfiguration .\Temp -Wait -Force
        $result = Test-DscConfiguration .\Temp

        $result.InDesiredState | Should -Be $true
    }
}

Describe 'WebPart' {
    New-Item -ItemType WebPartCategory -Path Kentico:\Development\WebParts -Name Test

    It 'Test-Path returns True if exists' {
        $result = Test-Path Kentico:\Development\WebParts\Maps\Basic\BasicBingMaps

        $result | Should -Be $true
    }

    It 'Test-Path returns False if not exists' {
        $result = Test-Path Kentico:\Development\WebParts\Maps\Basic\DoesNotExist

        $result | Should -Be $false
    }

    It 'New-Item should create item' {
        New-Item -ItemType WebPart -Path Kentico:\Development\WebParts\Test -Name TestWebPart -FileName PoshKentico/Test.ascx

        $result = Test-Path Kentico:\Development\WebParts\Test\TestWebPart

        $result | Should -Be $true
    }

    It 'Remove-Item should remove item' {
        Remove-Item Kentico:\Development\WebParts\Test\TestWebPart

        $result = Test-Path Kentico:\Development\WebParts\Test\TestWebPart
        
        $result | Should -Be $false
    }

    Remove-Item Kentico:\Development\WebParts\Test -Recurse
}