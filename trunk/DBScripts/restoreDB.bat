cd db_scripts 
for /F "" %%i in (files.txt) do sqlcmd -S localhost -d IUDICO -f 65001 -i %%i -E

