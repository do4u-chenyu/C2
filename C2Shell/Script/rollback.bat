tasklist|findstr C2.exe
cd /d %~dp0
if  not exist .\backup\C2.exe  (EXIT) 
if  not exist .\C2.exe  (EXIT) 
if  %errorlevel%==1 (del .\C2.exe & copy .\update\backup\C2.exe  .\  & EXIT )