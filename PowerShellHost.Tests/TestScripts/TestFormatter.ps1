# Script test credentials to your SQL Server
# Returns DataRow object with current time, if connection is successful

param(
	[string]$ServerInstance = '.',
	[string]$Username,
	[string]$Password
)

$output = [string]"Connecting remote SQL Server $ServerInstance"
Write-Output $output

Invoke-Sqlcmd -ServerInstance "$ServerInstance" `
    -Username "$Username" `
    -Password "$Password" `
-Query "SELECT GETDATE() AS TimeOfQuery"