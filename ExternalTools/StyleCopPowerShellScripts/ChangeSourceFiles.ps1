$original_file = 'FullStyleCopViolations.xml'
$destination_file =  'FullStyleCopViolationsModified.xml'
$currentDirectory = Get-Location
$currentPath = $currentDirectory.Path
$currentPath = $currentPath.Replace('\', '\\')
$currentPath = $currentPath += '\\'
echo $currentPath
(Get-Content $original_file) | Foreach-Object {
    $_ -replace $currentPath, ''
    } | Set-Content $destination_file