"c:\Windows\Microsoft.NET\Framework\v3.5\MSBuild.exe" iudico.sln /t:Rebuild /p:Configuration=RELEASE

xcopy Site\Compilers PrecompiledWeb\Site\Compilers /s /e /y

PAUSE