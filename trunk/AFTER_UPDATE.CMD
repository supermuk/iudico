SET GU="c:\Program Files\Microsoft SDKs\Windows\v6.0A\Bin\gacutil.exe"

%GU% /i "%CD%\Lib\LEX.CONTROLS.dll" /f
%GU% /i "%CD%\Lib\ICSharpCode.SharpZipLib.dll" /f
%GU% /i "%CD%\Lib\nunit.framework.dll" /f
%GU% /i "%CD%\Lib\nunit.framework.extensions.dll" /f
%GU% /i "%CD%\Lib\GMDatePicker.dll" /f