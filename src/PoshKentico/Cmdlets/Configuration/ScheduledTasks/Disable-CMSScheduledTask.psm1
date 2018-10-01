Add-Type -Path "$PSScriptRoot/../../../CMS.Scheduler.dll"

<#
.SYNOPSIS
Disables the scheduled tasks for the provided input.

.DESCRIPTION
Disables the scheduled tasks for the provided input.  This command automatically initializes the connection to Kentico if not already initialized.

This command with parameters disables only the specified scheduled task.
Without parameters, this command disables all of the scheduled tasks in Kentico.

.PARAMETER ScheduledTask
The scheduled task to disable in Kentico.

.PARAMETER PassThru
Tell the cmdlet to return the scheduled task.

.EXAMPLE
Disables all scheduled tasks.
Disable-CMSScheduledTask

.EXAMPLE
Disable a specified scheduled task.
$scheduledTask | Disable-CMSScheduledTask
#>
function Disable-CMSScheduledTask {
    [CmdletBinding(
        DefaultParameterSetName='NONE'
    )]
    [OutputType('CMS.Scheduler.TaskInfo')]
	param (
        [Parameter(Mandatory = $true, ValueFromPipeline = $true, ParameterSetName = "Scheduled Task")]
        [Alias("Task", "TaskInfo")]
        [CMS.Scheduler.TaskInfo]$ScheduledTask,
        [Parameter(ParameterSetName = "Scheduled Task")]
        [switch]$PassThru
    )

    BEGIN {
        # Initailize the connection to Kentico.
        Initialize-CMSApplication -Cached
    }

    PROCESS {
        switch ($PSCmdlet.ParameterSetName)
        {
            "None" {
                # Act on all of the scheduled tasks.
                foreach ($scheduledTask in Get-CMSScheduledTask) {
                    $scheduledTask.TaskEnabled = $false
                    
                    $scheduledTask | Set-CMSScheduledTask
                }
            }
            "Scheduled Task" {
                # Act on only the scheduled tasks passed in.
                $ScheduledTask.TaskEnabled = $false

                $ScheduledTask | Set-CMSScheduledTask

                if ($PassThru.ToBool()) {
                    $ScheduledTask
                }
            }
        }
    }
}