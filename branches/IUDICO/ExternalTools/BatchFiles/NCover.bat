set COMPLUS_ProfAPI_ProfilerCompatibilitySetting=EnableV2Profiler

"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\x64\CorFlags.exe" "C:\Program Files (x86)\TestDriven.NET 3\NCover\1.5.8\NCover.Console.exe" /FORCE /32BIT+ 


"C:\Program Files (x86)\TestDriven.NET 3\NCover\1.5.8\NCover.Console.exe" //reg "C:\Program Files (x86)\NUnit 2.6\bin\nunit-console-x86.exe" C:\Users\Administrator\.hudson\jobs\IUDICO\workspace\IUDICO.UnitTests\bin\Debug\IUDICO.UnitTests.dll

"C:\Program Files (x86)\TestDriven.NET 3\NCoverExplorer\NCoverExplorer.Console.exe" Coverage.Xml /html:CoverageReport.html /r:ModuleClassFunctionSummary /p:IUDICO /so:FunctionCoverageDescending

Taskkill /IM firefox.exe /F