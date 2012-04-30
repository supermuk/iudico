New-Item -ItemType file "FullStyleCopViolations.xml" -force

(Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.Analytics\obj\Debug\StyleCopViolations.xml") | 
Foreach-Object { $_ -replace '<StyleCopViolations />', '' } | 
Foreach-Object { $_ -replace '<StyleCopViolations>', '' } |
Foreach-Object { $_ -replace '</StyleCopViolations>', '' } |
Set-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.Analytics\obj\Debug\StyleCopViolations.xml"
$file1 = Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.Analytics\obj\Debug\StyleCopViolations.xml"

(Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.Common\obj\Debug\StyleCopViolations.xml") | 
Foreach-Object { $_ -replace '<StyleCopViolations />', '' } | 
Foreach-Object { $_ -replace '<StyleCopViolations>', '' } |
Foreach-Object { $_ -replace '</StyleCopViolations>', '' } |
Set-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.Common\obj\Debug\StyleCopViolations.xml"
$file2 = Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.Common\obj\Debug\StyleCopViolations.xml"


(Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.CompileSystem\obj\Debug\StyleCopViolations.xml") | 
Foreach-Object { $_ -replace '<StyleCopViolations />', '' } | 
Foreach-Object { $_ -replace '<StyleCopViolations>', '' } |
Foreach-Object { $_ -replace '</StyleCopViolations>', '' } |
Set-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.CompileSystem\obj\Debug\StyleCopViolations.xml"
$file3 = Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.CompileSystem\obj\Debug\StyleCopViolations.xml"

(Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.CourseManagement\obj\Debug\StyleCopViolations.xml") | 
Foreach-Object { $_ -replace '<StyleCopViolations />', '' } | 
Foreach-Object { $_ -replace '<StyleCopViolations>', '' } |
Foreach-Object { $_ -replace '</StyleCopViolations>', '' } |
Set-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.CourseManagement\obj\Debug\StyleCopViolations.xml"
$file4 = Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.CourseManagement\obj\Debug\StyleCopViolations.xml"

(Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.CurriculumManagement\obj\Debug\StyleCopViolations.xml") | 
Foreach-Object { $_ -replace '<StyleCopViolations />', '' } | 
Foreach-Object { $_ -replace '<StyleCopViolations>', '' } |
Foreach-Object { $_ -replace '</StyleCopViolations>', '' } |
Set-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.CurriculumManagement\obj\Debug\StyleCopViolations.xml"
$file5 = Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.CurriculumManagement\obj\Debug\StyleCopViolations.xml"


(Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.DisciplineManagement\obj\Debug\StyleCopViolations.xml") | 
Foreach-Object { $_ -replace '<StyleCopViolations />', '' } | 
Foreach-Object { $_ -replace '<StyleCopViolations>', '' } |
Foreach-Object { $_ -replace '</StyleCopViolations>', '' } |
Set-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.DisciplineManagement\obj\Debug\StyleCopViolations.xml"
$file6 = Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.DisciplineManagement\obj\Debug\StyleCopViolations.xml"

(Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.LMS\obj\Debug\StyleCopViolations.xml") | 
Foreach-Object { $_ -replace '<StyleCopViolations />', '' } | 
Foreach-Object { $_ -replace '<StyleCopViolations>', '' } |
Foreach-Object { $_ -replace '</StyleCopViolations>', '' } |
Set-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.LMS\obj\Debug\StyleCopViolations.xml"
$file7 = Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.LMS\obj\Debug\StyleCopViolations.xml"

(Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.Search\obj\Debug\StyleCopViolations.xml") | 
Foreach-Object { $_ -replace '<StyleCopViolations />', '' } | 
Foreach-Object { $_ -replace '<StyleCopViolations>', '' } |
Foreach-Object { $_ -replace '</StyleCopViolations>', '' } |
Set-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.Search\obj\Debug\StyleCopViolations.xml"
$file8 = Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.Search\obj\Debug\StyleCopViolations.xml"

(Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.Security\obj\Debug\StyleCopViolations.xml") | 
Foreach-Object { $_ -replace '<StyleCopViolations />', '' } | 
Foreach-Object { $_ -replace '<StyleCopViolations>', '' } |
Foreach-Object { $_ -replace '</StyleCopViolations>', '' } |
Set-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.Security\obj\Debug\StyleCopViolations.xml"
$file9 = Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.Security\obj\Debug\StyleCopViolations.xml"

(Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.Statistics\obj\Debug\StyleCopViolations.xml") | 
Foreach-Object { $_ -replace '<StyleCopViolations />', '' } | 
Foreach-Object { $_ -replace '<StyleCopViolations>', '' } |
Foreach-Object { $_ -replace '</StyleCopViolations>', '' } |
Set-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.Statistics\obj\Debug\StyleCopViolations.xml"
$file10 = Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.Statistics\obj\Debug\StyleCopViolations.xml"

(Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.TestingSystem\obj\Debug\StyleCopViolations.xml") | 
Foreach-Object { $_ -replace '<StyleCopViolations />', '' } | 
Foreach-Object { $_ -replace '<StyleCopViolations>', '' } |
Foreach-Object { $_ -replace '</StyleCopViolations>', '' } |
Set-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.TestingSystem\obj\Debug\StyleCopViolations.xml"
$file11 = Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.TestingSystem\obj\Debug\StyleCopViolations.xml"

(Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.UnitTests\obj\Debug\StyleCopViolations.xml") | 
Foreach-Object { $_ -replace '<StyleCopViolations />', '' } | 
Foreach-Object { $_ -replace '<StyleCopViolations>', '' } |
Foreach-Object { $_ -replace '</StyleCopViolations>', '' } |
Set-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.UnitTests\obj\Debug\StyleCopViolations.xml"
$file12 = Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.UnitTests\obj\Debug\StyleCopViolations.xml"


(Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.UserManagement\obj\Debug\StyleCopViolations.xml") | 
Foreach-Object { $_ -replace '<StyleCopViolations />', '' } | 
Foreach-Object { $_ -replace '<StyleCopViolations>', '' } |
Foreach-Object { $_ -replace '</StyleCopViolations>', '' } |
Set-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.UserManagement\obj\Debug\StyleCopViolations.xml"
$file13 = Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.UserManagement\obj\Debug\StyleCopViolations.xml"


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
