@ECHO OFF
echo Install DB
sqlcmd -i ScriptBookStore.sql
echo Install Data 
sqlcmd -i InsertBook.sql
echo Done
pause
