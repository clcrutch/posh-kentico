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