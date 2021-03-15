cd /d %~dp0
:: 创建备份路径
if not exist .\update\backup (md .\update\backup)
:: 替换新版本C2.exe
copy .\C2.exe .\update\backup\C2.exe
del .\C2.exe
copy .\update\setup\C2.exe .\