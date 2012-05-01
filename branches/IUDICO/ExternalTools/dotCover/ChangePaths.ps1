$original_file = 'C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\ExternalTools\dotCover\dotcover-configuration.xml'
$destination_file =  'C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\ExternalTools\dotCover\dotcover-configuration.xml'
$currentDirectory = Get-Location
$currentPath = $currentDirectory.Path

(Get-Content $original_file) | Foreach-Object {
    $_ -replace '<!-- Program working directory. -->', $currentPath
    } | Set-Content $destination_file