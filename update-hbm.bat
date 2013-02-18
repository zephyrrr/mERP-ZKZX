cd .\Reference
mkdir hbm

del /Q ..\Reference\hbm\*
..\Reference\ADInfosUtil.exe -hbm Zkzx.Model
copy /B /Y ..\Reference\hbm\* ..\Zkzx.Model\Domain.hbm.xml


IF ERRORLEVEL 1 pause

cd ..