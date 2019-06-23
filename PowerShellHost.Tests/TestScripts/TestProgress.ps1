echo 'Hello from echo'

Write-Host "Never use Write-Host method - it is not intercepted"
Write-Output "This is the default Write-Output method"

start-sleep -milli 500
Write-Information "Information"


#start-sleep -milli 500
#Write-Error "This was an error"


start-sleep -milli 500
Write-Warning "This was a warning"

start-sleep -milli 500
Write-Progress -Activity "Finding user" -CurrentOperation "TEST1" -PercentComplete 0

start-sleep -milli 500
Write-Progress -Activity "Finding user" -CurrentOperation "TEST1" -PercentComplete 10

start-sleep -milli 500
Write-Progress -Activity "Finding user" -CurrentOperation "TEST2" -PercentComplete 20

start-sleep -milli 500
Write-Progress -Activity "Finding user" -CurrentOperation "TEST3" -PercentComplete 30

start-sleep -milli 500
Write-Progress -Activity "Finding user" -CurrentOperation "TEST4" -PercentComplete 40

start-sleep -milli 500
Write-Progress -Activity "Finding user" -CurrentOperation "TEST5" -PercentComplete 50

start-sleep -milli 500
Write-Progress -Activity "Finding user" -CurrentOperation "TEST6" -PercentComplete 60

start-sleep -milli 500
Write-Progress -Activity "Finding user" -CurrentOperation "TEST7" -PercentComplete 70

start-sleep -milli 500
Write-Progress -Activity "Finding user" -CurrentOperation "TEST8" -PercentComplete 80

start-sleep -milli 500
Write-Progress -Activity "Finding user" -CurrentOperation "TEST9" -PercentComplete 90

start-sleep -milli 500
Write-Progress -Activity "Finding user" -CurrentOperation "TEST10" -PercentComplete 100
