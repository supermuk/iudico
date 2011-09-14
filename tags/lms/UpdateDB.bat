sqlcmd -S .\SQLEXPRESS -E -Q "CREATE DATABASE IUDICO"
sqlcmd -S .\SQLEXPRESS -E -d IUDICO -i "DBScripts\iudico.sql"
sqlcmd -S .\SQLEXPRESS -E -Q "CREATE DATABASE IUDICO_TEST"
sqlcmd -S .\SQLEXPRESS -E -d IUDICO_TEST -i "DBScripts\iudico.sql"
cd DataModel
GenerateLinqClasses.cmd

PAUSE 