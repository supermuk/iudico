New-Item -ItemType file "FullStyleCopViolations.xml" -force

(Get-Content "IUDICO.Analytics\obj\Release\StyleCopViolations.xml") | 
Foreach-Object { $_ -replace '<StyleCopViolations />', '' } | 
Foreach-Object { $_ -replace '<StyleCopViolations>', '' } |
Foreach-Object { $_ -replace '</StyleCopViolations>', '' } |
Set-Content "IUDICO.Analytics\obj\Release\StyleCopViolations.xml"
$file1 = Get-Content "IUDICO.Analytics\obj\Release\StyleCopViolations.xml"

(Get-Content "IUDICO.Common\obj\Release\StyleCopViolations.xml") | 
Foreach-Object { $_ -replace '<StyleCopViolations />', '' } | 
Foreach-Object { $_ -replace '<StyleCopViolations>', '' } |
Foreach-Object { $_ -replace '</StyleCopViolations>', '' } |
Set-Content "IUDICO.Common\obj\Release\StyleCopViolations.xml"
$file2 = Get-Content "IUDICO.Common\obj\Release\StyleCopViolations.xml"


(Get-Content "IUDICO.CompileSystem\obj\Release\StyleCopViolations.xml") | 
Foreach-Object { $_ -replace '<StyleCopViolations />', '' } | 
Foreach-Object { $_ -replace '<StyleCopViolations>', '' } |
Foreach-Object { $_ -replace '</StyleCopViolations>', '' } |
Set-Content "IUDICO.CompileSystem\obj\Release\StyleCopViolations.xml"
$file3 = Get-Content "IUDICO.CompileSystem\obj\Release\StyleCopViolations.xml"

(Get-Content "IUDICO.CourseManagement\obj\Release\StyleCopViolations.xml") | 
Foreach-Object { $_ -replace '<StyleCopViolations />', '' } | 
Foreach-Object { $_ -replace '<StyleCopViolations>', '' } |
Foreach-Object { $_ -replace '</StyleCopViolations>', '' } |
Set-Content "IUDICO.CourseManagement\obj\Release\StyleCopViolations.xml"
$file4 = Get-Content "IUDICO.CourseManagement\obj\Release\StyleCopViolations.xml"

(Get-Content "IUDICO.CurriculumManagement\obj\Release\StyleCopViolations.xml") | 
Foreach-Object { $_ -replace '<StyleCopViolations />', '' } | 
Foreach-Object { $_ -replace '<StyleCopViolations>', '' } |
Foreach-Object { $_ -replace '</StyleCopViolations>', '' } |
Set-Content "IUDICO.CurriculumManagement\obj\Release\StyleCopViolations.xml"
$file5 = Get-Content "IUDICO.CurriculumManagement\obj\Release\StyleCopViolations.xml"


(Get-Content "IUDICO.DisciplineManagement\obj\Release\StyleCopViolations.xml") | 
Foreach-Object { $_ -replace '<StyleCopViolations />', '' } | 
Foreach-Object { $_ -replace '<StyleCopViolations>', '' } |
Foreach-Object { $_ -replace '</StyleCopViolations>', '' } |
Set-Content "IUDICO.DisciplineManagement\obj\Release\StyleCopViolations.xml"
$file6 = Get-Content "IUDICO.DisciplineManagement\obj\Release\StyleCopViolations.xml"

(Get-Content "IUDICO.LMS\obj\Release\StyleCopViolations.xml") | 
Foreach-Object { $_ -replace '<StyleCopViolations />', '' } | 
Foreach-Object { $_ -replace '<StyleCopViolations>', '' } |
Foreach-Object { $_ -replace '</StyleCopViolations>', '' } |
Set-Content "IUDICO.LMS\obj\Release\StyleCopViolations.xml"
$file7 = Get-Content "IUDICO.LMS\obj\Release\StyleCopViolations.xml"

(Get-Content "IUDICO.Search\obj\Release\StyleCopViolations.xml") | 
Foreach-Object { $_ -replace '<StyleCopViolations />', '' } | 
Foreach-Object { $_ -replace '<StyleCopViolations>', '' } |
Foreach-Object { $_ -replace '</StyleCopViolations>', '' } |
Set-Content "IUDICO.Search\obj\Release\StyleCopViolations.xml"
$file8 = Get-Content "IUDICO.Search\obj\Release\StyleCopViolations.xml"

(Get-Content "IUDICO.Security\obj\Release\StyleCopViolations.xml") | 
Foreach-Object { $_ -replace '<StyleCopViolations />', '' } | 
Foreach-Object { $_ -replace '<StyleCopViolations>', '' } |
Foreach-Object { $_ -replace '</StyleCopViolations>', '' } |
Set-Content "IUDICO.Security\obj\Release\StyleCopViolations.xml"
$file9 = Get-Content "IUDICO.Security\obj\Release\StyleCopViolations.xml"

(Get-Content "IUDICO.Statistics\obj\Release\StyleCopViolations.xml") | 
Foreach-Object { $_ -replace '<StyleCopViolations />', '' } | 
Foreach-Object { $_ -replace '<StyleCopViolations>', '' } |
Foreach-Object { $_ -replace '</StyleCopViolations>', '' } |
Set-Content "IUDICO.Statistics\obj\Release\StyleCopViolations.xml"
$file10 = Get-Content "IUDICO.Statistics\obj\Release\StyleCopViolations.xml"

(Get-Content "IUDICO.TestingSystem\obj\Release\StyleCopViolations.xml") | 
Foreach-Object { $_ -replace '<StyleCopViolations />', '' } | 
Foreach-Object { $_ -replace '<StyleCopViolations>', '' } |
Foreach-Object { $_ -replace '</StyleCopViolations>', '' } |
Set-Content "IUDICO.TestingSystem\obj\Release\StyleCopViolations.xml"
$file11 = Get-Content "IUDICO.TestingSystem\obj\Release\StyleCopViolations.xml"

(Get-Content "IUDICO.UnitTests\obj\Release\StyleCopViolations.xml") | 
Foreach-Object { $_ -replace '<StyleCopViolations />', '' } | 
Foreach-Object { $_ -replace '<StyleCopViolations>', '' } |
Foreach-Object { $_ -replace '</StyleCopViolations>', '' } |
Set-Content "IUDICO.UnitTests\obj\Release\StyleCopViolations.xml"
$file12 = Get-Content "IUDICO.UnitTests\obj\Release\StyleCopViolations.xml"


(Get-Content "IUDICO.UserManagement\obj\Release\StyleCopViolations.xml") | 
Foreach-Object { $_ -replace '<StyleCopViolations />', '' } | 
Foreach-Object { $_ -replace '<StyleCopViolations>', '' } |
Foreach-Object { $_ -replace '</StyleCopViolations>', '' } |
Set-Content "IUDICO.UserManagement\obj\Release\StyleCopViolations.xml"
$file13 = Get-Content "IUDICO.UserManagement\obj\Release\StyleCopViolations.xml"


Add-Content "FullStyleCopViolations.xml" $file1
Add-Content "FullStyleCopViolations.xml" $file2
Add-Content "FullStyleCopViolations.xml" $file3
Add-Content "FullStyleCopViolations.xml" $file4
Add-Content "FullStyleCopViolations.xml" $file5
Add-Content "FullStyleCopViolations.xml" $file6
Add-Content "FullStyleCopViolations.xml" $file7
Add-Content "FullStyleCopViolations.xml" $file8
Add-Content "FullStyleCopViolations.xml" $file9
Add-Content "FullStyleCopViolations.xml" $file10
Add-Content "FullStyleCopViolations.xml" $file11
Add-Content "FullStyleCopViolations.xml" $file12
Add-Content "FullStyleCopViolations.xml" $file13

$file14 = Get-Content "FullStyleCopViolations.xml"
Set-Content "FullStyleCopViolations.xml" –value '<StyleCopViolations>', $file14, '</StyleCopViolations>' 
