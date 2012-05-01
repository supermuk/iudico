$original_file = 'ExternalTools\dotCover\dotcover-configuration.xml'
$destination_file =  'ExternalTools\dotCover\dotcover-configuration.xml'
$currentDirectory = Get-Location
$currentPath = $currentDirectory.Path
$currentPath = $currentPath.Replace('\','\\')

(Get-Content $original_file) | Foreach-Object {
    $_ -replace $currentPath, '<!-- Program working directory. -->'
    } | Set-Content $destination_file