New-Item -ItemType file "FullStyleCopViolations.xml" -force
$file1 = Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.Analytics\obj\Debug\StyleCopViolations.xml"
$file1 = $file1[0..($file1.length-2)]

$file2 = Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.Common\obj\Debug\StyleCopViolations.xml"
$file2 = $file2[1..($file2.length-2)]

$file3 = Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.CompileSystem\obj\Debug\StyleCopViolations.xml"
$file3 = $file3[1..($file3.length-2)]

$file4 = Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.CourseManagement\obj\Debug\StyleCopViolations.xml"
$file4 = $file4[1..($file4.length-2)]

$file5 = Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.CurriculumManagement\obj\Debug\StyleCopViolations.xml"
$file5 = $file5[1..($file5.length-2)]

$file6 = Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.DisciplineManagement\obj\Debug\StyleCopViolations.xml"
$file6 = $file6[1..($file6.length-2)]

$file7 = Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.LMS\obj\Debug\StyleCopViolations.xml"
$file7 = $file7[1..($file7.length-2)]

$file8 = Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.Search\obj\Debug\StyleCopViolations.xml"
$file8 = $file8[1..($file8.length-2)]

$file9 = Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.Security\obj\Debug\StyleCopViolations.xml"
$file9 = $file9[1..($file9.length-2)]

$file10 = Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.Statistics\obj\Debug\StyleCopViolations.xml"
$file10 = $file10[1..($file10.length-2)]

$file11 = Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.TestingSystem\obj\Debug\StyleCopViolations.xml"
$file11 = $file11[1..($file11.length-2)]

$file12 = Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.UnitTests\obj\Debug\StyleCopViolations.xml"
$file12 = $file12[1..($file12.length-2)]

$file13 = Get-Content "C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.UserManagement\obj\Debug\StyleCopViolations.xml"
$file13 = $file13[1..($file13.length)]

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