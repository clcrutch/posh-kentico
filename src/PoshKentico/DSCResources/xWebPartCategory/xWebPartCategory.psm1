function Get-TargetResource
{
    [CmdletBinding()]
    [OutputType([System.Collections.Hashtable])]
    param
    (
        [parameter(Mandatory = $true)]
        [System.String]
        $Path
    )

    $webPartCategory = Get-CMSWebPartCategory -Path $Path
    if ($null -ne $webPartCategory) {
        $Ensure = "Present"
    }
    else {
        $Ensure = "Absent"
    }
    
    $returnValue = @{
        Path = $Path
        Ensure = $Ensure
        DisplayName = $webPartCategory.CategoryDisplayName
        ImagePath = $webPartCategory.CategoryImagePath
    }

    $returnValue
}


function Set-TargetResource
{
    [CmdletBinding()]
    param
    (
        [parameter(Mandatory = $true)]
        [System.String]
        $Path,
        
        [ValidateSet("Present","Absent")]
        [System.String]
        $Ensure,

        [System.String]
        $DisplayName,

        [System.String]
        $ImagePath
    )

    if ([string]::IsNullOrEmpty($Ensure)) {
        $Ensure = "Present"
    }

    $webPartCategory = Get-CMSWebPartCategory -Path $Path

    if ($Ensure -eq "Present") {
        if ($null -ne $webPartCategory) {
            $webPartCategory.CategoryDisplayName = $DisplayName

            if (-not [string]::IsNullOrEmpty($ImagePath)) {
                $webPartCategory.CategoryImagePath = $ImagePath
            }
            
            $webPartCategory | Set-CMSWebPartCategory
        }
        else {
            New-CMSWebPartCategory -Path $Path -DisplayName $DisplayName -ImagePath $ImagePath
        }
    }
    elseif ($Ensure -eq "Absent") {
        if ($null -ne $webPartCategory) {
            $webPartCategory | Remove-CMSWebPartCategory -Confirm:$false -Recurse
        }
    }
}


function Test-TargetResource
{
    [CmdletBinding()]
    [OutputType([System.Boolean])]
    param
    (
        [parameter(Mandatory = $true)]
        [System.String]
        $Path,
        
        [ValidateSet("Present","Absent")]
        [System.String]
        $Ensure,

        [System.String]
        $DisplayName,

        [System.String]
        $ImagePath
    )

    if ([string]::IsNullOrEmpty($DisplayName)) {
        $DisplayName = $Path.Substring($Path.LastIndexOf('/') + 1)
    }

    $webPartCategory = Get-CMSWebPartCategory -Path $Path
    if ($null -ne $webPartCategory) {
        $Ensure -eq "Present" -and
        $DisplayName -eq $webPartCategory.CategoryDisplayName -and
        $ImagePath -eq $webPartCategory.CategoryImagePath
    }
    else {
        $Ensure -eq "Absent"
    }
}


Export-ModuleMember -Function *-TargetResource

