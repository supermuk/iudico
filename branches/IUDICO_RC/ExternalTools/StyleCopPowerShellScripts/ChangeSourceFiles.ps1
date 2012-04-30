$original_file = 'FullStyleCopViolations.xml'
$destination_file =  'FullStyleCopViolationsModified.xml'
(Get-Content $original_file) | Foreach-Object {
    $_ -replace 'C:\\Users\\Administrator\\.hudson\\jobs\\IUDICO\\workspace\\', ''
    } | Set-Content $destination_file